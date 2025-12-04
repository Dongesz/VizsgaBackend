using BackEnd.Application.DTOs;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUsersService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

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
                _logger.LogInformation("GetAll users cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll users failed.");
                return Problem(detail: ex?.InnerException?.Message);
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
                _logger.LogInformation("GetById users cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetById {Id} failed", id);
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsersSendDto dto, CancellationToken cancellationToken)
        {
            try
            {
                if (dto == null) return BadRequest("Body is null");
                var created = await _service.CreateAsync(dto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Create user cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create user failed.");
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsersSendDto dto, CancellationToken cancellationToken)
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
                _logger.LogInformation("Update user cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update {Id} failed", id);
                return Problem(detail: ex?.InnerException?.Message);
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
                _logger.LogInformation("Delete user cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete {Id} failed", id);
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        [HttpGet("playerCount")]
        public async Task<IActionResult> PlayerCount(CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetUserCountAsync(cancellationToken);
                if (ok == null) return NotFound();
                return Ok(ok);

            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Get PlayerCount cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Player count error: {ex?.InnerException?.Message}");
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        [HttpGet("playerScore")]
        public async Task<IActionResult> UserScoreboardAll(CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetAllUserScoreboardAsync(cancellationToken);
                if (ok == null) return NotFound();
                return Ok(ok);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Get Player-Score cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"All Player Score by error: {ex?.InnerException?.Message}");
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        [HttpGet("playerScore/{id:int}")]
        public async Task<IActionResult> UserScoreboardById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetUserByIdScoreboardAsync(id, cancellationToken);
                if (ok == null) return NotFound();
                return Ok(ok);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Get Player-Score cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Player Score by id error: {ex?.InnerException?.Message}");
                return Problem(detail: ex?.InnerException?.Message);
            }
        }
        
        [HttpPut("playerPasswordUpdate")]
        public async Task<IActionResult> UserPasswordUpdateById(UserPasswordUpdateInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.UpdateUserPasswordAsync(dto, cancellationToken);
                return Ok(ok);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Player password update cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Player password update by id error: {ex?.InnerException?.Message}");
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        [HttpGet("playerResult")]
        public async Task<IActionResult> UserResultGetAll(int id, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetAllResultAsync(id, cancellationToken);
                if (ok == null) return NotFound();
                return Ok(ok);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Get Player-Result cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Player-Result get all error: {ex?.InnerException?.Message}");
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        [HttpPost("playerPasswordVerify")]
        public async Task<IActionResult> UserPasswordVerify(UserPasswordVerifyInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.VerifyPasswordAsync(dto, cancellationToken);
                if (ok == null) return NotFound();
                return Ok(ok);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Player-Password-Verify cancelled.");
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Player-Password-Verify error: {ex?.InnerException?.Message}");
                return Problem(detail: ex?.InnerException?.Message);
            }
        }
    }
}
