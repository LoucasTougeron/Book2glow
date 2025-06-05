using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;

    public AuthController(UserManager<ApplicationUser> userManager,
                          IConfiguration configuration, IAuthService authService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _authService = authService;
    }

    //Post /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName,PhoneNumber = model.PhoneNumber };

        if (!new[] { "Customer", "Provider" }.Contains(model.Role))
            return BadRequest(new { message = "Rôle invalide" });

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, model.Role);
        return Ok(new { message = "Inscription réussie !" });
    }

    //Post /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
            return Unauthorized(new { message = "Identifiants invalides" });

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { token });
    }
  
}
