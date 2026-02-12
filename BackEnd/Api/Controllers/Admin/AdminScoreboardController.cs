using BackEnd.Application.DTOs.Scoreboard;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers.Admin
{
    /// <summary>Admin ranglistakezelő: bármely felhasználó ranglistájának frissítése azonosító alapján.</summary>
    [ApiController]
    [Route("Admin/Scoreboard")]
    [Authorize(Roles = "Admin")]
    public class AdminScoreboardController : ControllerBase
    {
        private readonly IScoreboardService _service;

        public AdminScoreboardController(IScoreboardService service)
        {
            _service = service;
        }

        /// <summary>Ranglista frissítése ranglista-azonosító alapján.</summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScoreboardSendInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto, cancellationToken);
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
