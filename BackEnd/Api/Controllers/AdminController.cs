using BackEnd.Application.DTOs.Scoreboard;
using BackEnd.Application.DTOs.User;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers.Admin
{
    [ApiController]
    [Route("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IScoreboardService _scoreboardService;

        public AdminController(IUsersService usersService, IScoreboardService scoreboardService)
        {
            _usersService = usersService;
            _scoreboardService = scoreboardService;
        }

        /// <summary>Összes felhasználó listázása.</summary>
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _usersService.GetAllAsync(cancellationToken);
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

        /// <summary>Felhasználó lekérése azonosító alapján.</summary>
        [HttpGet("Users/{id:int}")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _usersService.GetByIdAsync(id, cancellationToken);
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

        /// <summary>Felhasználó törlése azonosító alapján.</summary>
        [HttpDelete("Users/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _usersService.DeleteAsync(id, cancellationToken);
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

        /// <summary>Felhasználó profilképének feltöltése azonosító alapján.</summary>
        [RequestSizeLimit(10_485_760)]
        [HttpPost("Users/{id:int}/profile-picture")]
        public async Task<IActionResult> UploadUserProfilePicture(int id, IFormFile file, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _usersService.UploadCustomProfilePicture(id, file, cancellationToken);
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

        /// <summary>Felhasználó nevét, e-mailjét, bióját és profilképét egyszerre módosítja.</summary>
        [RequestSizeLimit(10_485_760)]
        [HttpPut("Users/{id:int}/profile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromForm] AdminProfileUpdateInputDto? dto, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _usersService.UpdateUserProfileAsync(id, dto ?? new AdminProfileUpdateInputDto(), cancellationToken);
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

        /// <summary>Felhasználó pontszámának frissítése azonosító alapján.</summary>
        [HttpPut("Scoreboard/{id:int}")]
        public async Task<IActionResult> UpdateScoreboard(int id, [FromBody] ScoreboardSendInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _scoreboardService.UpdateAsync(id, dto, cancellationToken);
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
