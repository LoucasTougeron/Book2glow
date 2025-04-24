using Book2Glow.Infrastructure.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Book2Glow.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet("me")]
        [ServiceFilter(typeof(RoleMiddleware))]
        public async Task<ActionResult<ApplicationUser>> GetUserMe()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            return Ok(user);
        }


    }
}
