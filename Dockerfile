# Étape 1 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Étape 2 : Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier les fichiers .csproj dans leurs dossiers respectifs
COPY Book2Glow.api/Book2Glow.api/Book2Glow.Api.csproj Book2Glow.api/Book2Glow.api/
COPY Book2Glow.api/Book2Glow.Service/Book2Glow.Service.csproj Book2Glow.api/Book2Glow.Service/
COPY Book2Glow.api/Book2Glow.Infrastructure/Book2Glow.Infrastructure.csproj Book2Glow.api/Book2Glow.Infrastructure/

# Restaurer les dépendances
RUN dotnet restore Book2Glow.api/Book2Glow.api/Book2Glow.Api.csproj

# Copier tout le reste du code
COPY Book2Glow.api/Book2Glow.api/ Book2Glow.api/Book2Glow.api/
COPY Book2Glow.api/Book2Glow.Service/ Book2Glow.api/Book2Glow.Service/
COPY Book2Glow.api/Book2Glow.Infrastructure/ Book2Glow.api/Book2Glow.Infrastructure/

# Compiler et publier
RUN dotnet publish Book2Glow.api/Book2Glow.api/Book2Glow.Api.csproj -c Release -o /app/publish

# Étape 3 : Runtime final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exposer l'application
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Book2Glow.Api.dll"]
