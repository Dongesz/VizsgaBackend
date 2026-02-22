using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Services;

namespace RoleBasedAuth.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class UsersController : ControllerBase
    {
        private readonly IAuth auth;
        private readonly IConfiguration configuration;

        public UsersController(IAuth auth, IConfiguration configuration)
        {
            this.auth = auth;
            this.configuration = configuration;
        }

        /// <summary>Identity felhasználó törlése azonosító alapján. Helper endpoint, ne nagyon hasznald!</summary>
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
