using BackEnd.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        /// <summary>Felhasználók számának lekérése.</summary>
        [HttpGet("playerCount")]
        [AllowAnonymous]
        public async Task<IActionResult> PlayerCount(CancellationToken cancellationToken)
        {
            try
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    await _service.EnsureUserExistsAsync(User, cancellationToken);
                }
                var result = await _service.GetPlayerCountAsync(cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        /// <summary>Összes felhasználó ranglistája.</summary>
        [HttpGet("leaderboard")]
        [AllowAnonymous]
        public async Task<IActionResult> Leaderboard(CancellationToken cancellationToken)
        {
            try
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    await _service.EnsureUserExistsAsync(User, cancellationToken);
                }
                var result = await _service.GetLeaderboardAsync(cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        /// <summary>Egy adott felhasználó eredményének lekérése.</summary>
        [HttpGet("{id:int}/result")]
        [AllowAnonymous]
        public async Task<IActionResult> UserResult(int id, CancellationToken cancellationToken)
        {
            try
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    await _service.EnsureUserExistsAsync(User, cancellationToken);
                }
                var result = await _service.GetByIdResultAsync(id, cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex?.InnerException?.Message);
            }
        }
    }
}
