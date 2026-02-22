using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Models.DTOs;
using RoleBasedAuth.Services;

namespace RoleBasedAuth.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;

        public AuthController(IAuth auth)
        {
            this.auth = auth;
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
    }
}
