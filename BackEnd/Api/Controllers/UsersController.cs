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

        /// <summary>Bejelentkezett felhasználó adatainak lekérése.</summary>
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetMeAsync(User, cancellationToken);
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

        /// <summary>Bejelentkezett felhasználó részletes eredménye.</summary>
        [Authorize]
        [HttpGet("me/result")]
        public async Task<IActionResult> MeResult(CancellationToken cancellationToken)
        {
            try
            {
                await _service.EnsureUserExistsAsync(User, cancellationToken);
                var result = await _service.GetMyResultAsync(User, cancellationToken);
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

        /// <summary>Saját név frissítése.</summary>
        [Authorize]
        [HttpPut("me/name")]
        public async Task<IActionResult> UpdateMyName([FromBody] UserNameUpdateInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.UpdateMyNameAsync(User, dto, cancellationToken);
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

        /// <summary>Saját bio frissítése.</summary>
        [Authorize]
        [HttpPut("me/bio")]
        public async Task<IActionResult> UpdateMyBio([FromBody] UserBioUpdateInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.UpdateMyBioAsync(User, dto, cancellationToken);
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

        /// <summary>Saját profilkép feltöltése.</summary>
        [RequestSizeLimit(10_485_760)] 
        [Authorize]
        [HttpPost("me/profile-picture")]
        public async Task<IActionResult> UploadMyProfilePicture(IFormFile file, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _service.UploadMyProfilePictureAsync(User, file, cancellationToken);
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

        /// <summary>Játékosok számának lekérése.</summary>
        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> PlayerCount(CancellationToken cancellationToken)
        {
            try
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    await _service.EnsureUserExistsAsync(User, cancellationToken);
                }
                var result = await _service.GetUserCountAsync(cancellationToken);
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
        [HttpGet("scoreboard")]
        [AllowAnonymous]
        public async Task<IActionResult> UserScoreboardAll(CancellationToken cancellationToken)
        {
            try
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    await _service.EnsureUserExistsAsync(User, cancellationToken);
                }
                var result = await _service.GetAllUserScoreboardAsync(cancellationToken);
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

        /// <summary>Egy adott játékos eredményének lekérése.</summary>
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

        /// <summary>Bejelentkezett felhasználó fiókjának törlése.</summary>
        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteMe(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeleteMeAsync(User, cancellationToken);
                if (result.Success == null)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest("Request cancelled.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex?.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
