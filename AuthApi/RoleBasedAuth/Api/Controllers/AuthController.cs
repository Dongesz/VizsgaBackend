using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Models.DTOs;
using RoleBasedAuth.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RoleBasedAuth.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;
        private readonly IConfiguration configuration;

        public AuthController(IAuth auth, IConfiguration configuration)
        {
            this.auth = auth;
            this.configuration = configuration;
        }

        /// <summary>Új felhasználó regisztrálása.</summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var user = await auth.Register(dto);
            if (user != null)
            {
                return StatusCode(201, user);
            }
            return BadRequest();
        }

        /// <summary>Szerepkör kiosztása felhasználónak.</summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            var user = await auth.AssignRole(dto);
            if (user != null)
            {
                return StatusCode(201, user);
            }
            return BadRequest();
        }

        /// <summary>Bejelentkezés: e-mail és jelszó alapján.</summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (request == null)
                return BadRequest("Request body (userName, password) is required.");
            var user = await auth.Login(request);
            if (user != null)
            {
                return StatusCode(201, user);
            }
            return BadRequest();
        }

        /// <summary>Identity felhasználó törlése azonosító alapján. Belső használat (X-Internal-Api-Key header kell!!!)</summary>
        [HttpDelete("users/{authUserId}")]
        public async Task<IActionResult> DeleteIdentityUser(string authUserId, [FromHeader(Name = "X-Internal-Api-Key")] string? apiKey)
        {
            var configuredKey = configuration["AuthSettings:InternalApiKey"];
            if (string.IsNullOrWhiteSpace(configuredKey) || string.IsNullOrWhiteSpace(apiKey) || !string.Equals(configuredKey, apiKey))
            {
                return Unauthorized(new { message = "Invalid internal API key." });
            }

            var result = await auth.DeleteUserByIdAsync(authUserId);
            return Ok(result);
        }

        /// <summary>Bejelentkezett felhasználó jelszavának megváltoztatása.</summary>
        [Authorize]
        [HttpPut("me/password")]
        public async Task<IActionResult> ChangeMyPassword([FromBody] ChangePasswordDto dto, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new { message = "Invalid token: user id not found." });
            }

            var result = await auth.ChangePasswordAsync(userId, dto);
            return Ok(result);
        }
    }
}
