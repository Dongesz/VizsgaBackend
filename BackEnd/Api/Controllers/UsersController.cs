using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.User;
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

        /// <summary>
        /// Összes felhasználó lekérdezése
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetAllAsync(cancellationToken);
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

        /// <summary>
        /// Felhasználó lekérdezése azonosító alapján
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetByIdAsync(id, cancellationToken);
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

        /// <summary>
        /// Felhasználó törlése azonosító alapján
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.DeleteAsync(id, cancellationToken);
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

        /// <summary>
        /// Játékosok számának lekérdezése
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpGet("playerCount")]
        public async Task<IActionResult> PlayerCount(CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetUserCountAsync(cancellationToken);
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

        /// <summary>
        /// Összes játékos pontszámának lekérdezése
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpGet("playerScore")]
        public async Task<IActionResult> UserScoreboardAll(CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetAllUserScoreboardAsync(cancellationToken);
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

        /// <summary>
        /// Játékos részletes eredményeinek lekérdezése
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpGet("playerResult/{id:int}")]
        public async Task<IActionResult> UserResultGetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.GetByIdResultAsync(id, cancellationToken);
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

        /// <summary>
        /// Játékos nevének módosítása
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpPut("playerNameUpdate/{id:int}")]
        public async Task<IActionResult> UserNameUpdateById(int id, UserNameUpdateInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.UpdateUserNameAsync(id, dto, cancellationToken);
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


        /// <summary>
        /// Játékos bemutatkozásának módosítása
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpPut("playerBioUpdate/{id:int}")]
        public async Task<IActionResult> UserBioUpdateById(int id ,UserBioUpdateInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var ok = await _service.UpdateUserBioAsync(id ,dto, cancellationToken);
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


        /// <summary>
        /// Felhasználó profilképének feltöltése
        /// </summary>
        /// <remarks>
        /// Frontend usage: TBD
        /// </remarks>
        [HttpPost("playerProfilePictureSet/{id:int}")]
        public async Task<IActionResult> UserProfilePictureSet(int id, IFormFile file, CancellationToken cancellationToken = default)
        {
            try
            {
                var ok = await _service.UploadCustomProfilePicture(id, file, cancellationToken);
                return Ok(ok);
            }
            catch (OperationCanceledException)
            {
                return BadRequest("Profile picture get cancelled.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex?.InnerException?.Message);
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me(CancellationToken cancellationToken)
        {
            var result = await _service.GetMeAsync(User, cancellationToken);
            return Ok(result);
        }

    }
}
