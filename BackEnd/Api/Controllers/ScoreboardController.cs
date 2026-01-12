using BackEnd.Application.DTOs.Scoreboard;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers
{
    // Controller reteg - itt jonnek letre a tenyleges vegpontok, a muveletek a service retegbol kerulnek ki.
    [ApiController]
    [Route("[controller]")]
    public class ScoreboardController : ControllerBase
    {
        // Mezok a DI hez
        private readonly IScoreboardService _service;
        private readonly ILogger<ScoreboardController> _logger;

        // DI beallitasa a konstruktorban
        public ScoreboardController(IScoreboardService service, ILogger<ScoreboardController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // A controller metodusok meghivjak a hozzajuk tartozo service metodust, majd hibakezelessel kievgeszitve vegpontot keszitenek belole

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

