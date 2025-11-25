using BackEnd.Application.DTOs;
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
    [Route("api/[controller]")]
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
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var list = await _service.GetAllAsync(cancellationToken);
                return Ok(list);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("GetAll was cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll failed");
                return Problem(detail: ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _service.GetByIdAsync(id, cancellationToken);
                if (item == null) return NotFound();
                return Ok(item);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("GetById was cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetById {Id} failed", id);
                return Problem(detail: ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ScoreboardSendDto dto, CancellationToken cancellationToken)
        {
            try
            {
                if (dto == null) return BadRequest("Body is null");
                var created = await _service.CreateAsync(dto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Create was cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create failed");
                return Problem(detail: ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScoreboardSendDto dto, CancellationToken cancellationToken)
        {
            try
            {
                if (dto == null) return BadRequest("Body is null");
                var ok = await _service.UpdateAsync(id, dto, cancellationToken);
                if (!ok) return NotFound();
                return NoContent();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Update was cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update {Id} failed", id);
                return Problem(detail: ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.DeleteAsync(id, cancellationToken);
                if (!ok) return NotFound();
                return NoContent();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Delete   was cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete {Id} failed", id);
                return Problem(detail: ex.Message);
            }
        }
    }
}
