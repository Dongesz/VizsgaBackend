using BackEnd.Application.DTOs.Scoreboard;
using BackEnd.Application.Services;
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

        public ScoreboardController(IScoreboardService service)
        {
            _service = service;
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
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex?.InnerException?.Message);
            }
        }
    }
}

