using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class RoleMiddleware : IAsyncActionFilter
{
    private readonly IConfiguration _configuration;

    public RoleMiddleware(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized: No token provided");
            return;
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = handler.ValidateToken(token, parameters, out var validatedToken);

            // Récupérer tous les rôles
            var roleClaims = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            if (roleClaims == null || !roleClaims.Any())
            {
                context.Result = new ForbidResult("Access Denied: No valid roles");
                return;
            }

            // Stocker les rôles sous "UserRoles" (une liste)
            context.HttpContext.Items["UserRoles"] = roleClaims;
        }
        catch (Exception)
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized: Invalid token");
            return;
        }

        await next();
    }
}
