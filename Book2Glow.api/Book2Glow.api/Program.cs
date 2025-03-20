using Book2Glow.Infrastructure.Data;
using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Infrastructure.Data.Seeder;
using Book2Glow.Service.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Ajouter des services au conteneur
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
string connexionString = configuration["ConnectionStrings:DataModelDevContext"];
builder.Services.AddServices(connexionString);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataModelContext>()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
    options.AddPolicy("Provider", policy => policy.RequireRole("Provider"));
});

builder.Services.AddScoped<RoleMiddleware>();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configurez Swagger et l'authentification
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

// Mappe les contrôleurs
app.MapControllers();

app.Run();
