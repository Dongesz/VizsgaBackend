using BackEnd.Application.DTOs.Scoreboard;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreboardController : ControllerBase
    {
        private readonly IScoreboardService _service;
        private readonly IUsersService _usersService;

        public ScoreboardController(IScoreboardService service, IUsersService usersService)
        {
            _service = service;
            _usersService = usersService;
        }

        /// <summary>Bejelentkezett felhasználó ranglistájának frissítése.</summary>
        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyScoreboard([FromBody] ScoreboardSendInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _usersService.EnsureUserExistsAsync(User, cancellationToken);
                var result = await _service.UpdateMyScoreboardAsync(user.Id, dto, cancellationToken);
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
