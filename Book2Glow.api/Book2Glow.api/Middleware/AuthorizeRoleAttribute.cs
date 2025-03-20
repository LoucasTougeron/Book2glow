using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class AuthorizeRoleAttribute : Attribute, IAsyncActionFilter
{
    private readonly string[] _roles;

    public AuthorizeRoleAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Récupérer la liste des rôles de l'utilisateur depuis HttpContext
        var userRoles = context.HttpContext.Items["UserRoles"] as List<string>;

        if (userRoles == null || !userRoles.Any())
        {
            Console.WriteLine("No roles found in context: " + context.HttpContext.Items["UserRoles"]);
            context.Result = new UnauthorizedObjectResult("Unauthorized: No roles found");
            return;
        }

        // Vérifier si l'un des rôles de l'utilisateur est autorisé
        if (!_roles.Any(role => userRoles.Contains(role)))
        {
            context.Result = new ForbidResult("Access Denied: Insufficient permissions");
            return;
        }

        await next();
    }
}
