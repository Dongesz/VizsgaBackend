using BackEnd.Application.DTOs.Scoreboard;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreboardController : ControllerBase
    {
        private readonly IScoreboardService _service;
        private readonly ILogger<ScoreboardController> _logger;

        public ScoreboardController(IScoreboardService service, ILogger<ScoreboardController> logger)
        {
            _service = service;
            _logger = logger;
        }


        /// <summary>
        /// Játékos pontszámának frissítése
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScoreboardSendInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.UpdateAsync(id, dto, cancellationToken);
                return Ok(ok);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Update was cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update {Id} failed", id);
                return Problem(detail: ex?.InnerException?.Message);
            }
        }
    }
}

