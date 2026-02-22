using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Models.DTOs;
using RoleBasedAuth.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RoleBasedAuth.Controllers
{
    [ApiController]
    [Route("Auth")]
    [Authorize]
    public class MeController : ControllerBase
    {
        private readonly IAuth auth;

        public MeController(IAuth auth)
        {
            this.auth = auth;
        }

        /// <summary>Bejelentkezett felhasználó jelszavának megváltoztatása.</summary>
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
