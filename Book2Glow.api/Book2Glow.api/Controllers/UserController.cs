using Book2Glow.Api.Dtos;
using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Service.Service;
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
        private readonly IUserService _userService;

        public UserController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }


        [HttpGet("me")]
        [ServiceFilter(typeof(RoleMiddleware))]
        public async Task<ActionResult<ApplicationUser>> GetUserMe()
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            return Ok(user);
        }

        [HttpPut("me")]
        [ServiceFilter(typeof(RoleMiddleware))]
        public async Task<ActionResult<ApplicationUser>> UpdateUserMe([FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _userService.GetCurrentUserAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.FirstName = updateUserDto.FirstName ?? user.FirstName;
            user.LastName = updateUserDto.LastName ?? user.LastName;
            user.Email = updateUserDto.Email ?? user.Email;
            user.PhoneNumber = updateUserDto.PhoneNumber ?? user.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            user = await _userManager.FindByIdAsync(user.Id);

            return Ok(user);
        }

        
        [HttpPost("updatepassword")]
        [ServiceFilter(typeof(RoleMiddleware))]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
        {
            if (dto == null)
                return BadRequest(new { code = "invalid_request", message = "Invalid Body" });

            var success = await _userService.UpdateUserPassword(dto.OldPassword, dto.NewPassword);
            if (!success)
                return BadRequest(new { code = "invalid_password", message = "Bad password data" });

            return Ok(new { code = "password_updated", message = "Password change successfully" });
        }

    }
}
