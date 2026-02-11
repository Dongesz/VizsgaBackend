using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Models.DTOs;
using RoleBasedAuth.Services;

namespace RoleBasedAuth.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await auth.Login(dto);
            if (user != null)
            {
                return StatusCode(201, user);
            }
            return BadRequest();
        }

        /// <summary>
        /// Deletes an Identity user by their id.
        /// Intended for internal use from the BackEnd when a player deletes their account.
        /// Protected by an internal API key configured in AuthSettings:InternalApiKey.
        /// </summary>
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
    }
}
