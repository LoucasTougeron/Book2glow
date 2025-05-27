FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copie tous les projets
COPY Book2Glow.api/Book2Glow.api/Book2Glow.api.csproj Book2Glow.api/
COPY Book2Glow.api/Book2Glow.Service/Book2Glow.Service.csproj Book2Glow.Service/
COPY Book2Glow.api/Book2Glow.Infrastructure/Book2Glow.Infrastructure.csproj Book2Glow.Infrastructure/

# Restore des dépendances
RUN dotnet restore Book2Glow.api/Book2Glow.api.csproj

# Copie complète du code
COPY . .

# Build
RUN dotnet publish Book2Glow.api/Book2Glow.api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Book2Glow.Api.dll"]
