using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.User;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers.Admin
{
    /// <summary>Admin felhasználókezelő: listázás, lekérés, törlés, név/bio/profilkép frissítés.</summary>
    [ApiController]
    [Route("Admin/Users")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : ControllerBase
    {
        private readonly IUsersService _service;

        public AdminUsersController(IUsersService service)
        {
            _service = service;
        }

        /// <summary>Összes felhasználó listázása.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetAllAsync(cancellationToken);
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetByIdAsync(id, cancellationToken);
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeleteAsync(id, cancellationToken);
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

        /// <summary>Felhasználó nevének frissítése.</summary>
        [HttpPut("{id:int}/name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UserNameUpdateInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.UpdateUserNameAsync(id, dto, cancellationToken);
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

        /// <summary>Felhasználó biójának frissítése.</summary>
        [HttpPut("{id:int}/bio")]
        public async Task<IActionResult> UpdateBio(int id, [FromBody] UserBioUpdateInputDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.UpdateUserBioAsync(id, dto, cancellationToken);
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

        /// <summary>Felhasználó egyéni profilképének feltöltése.</summary>
        [RequestSizeLimit(10_485_760)] // 10 MB
        [HttpPost("{id:int}/profile-picture")]
        public async Task<IActionResult> UploadProfilePicture(int id, IFormFile file, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _service.UploadCustomProfilePicture(id, file, cancellationToken);
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

        /// <summary>Admin: a felhasználó nevét, e-mailjét, bióját és profilképét egyszerre módosítja (multipart/form-data: name, email, bio, profilePicture).</summary>
        [RequestSizeLimit(10_485_760)] // 10 MB (profilkép)
        [HttpPut("{id:int}/profile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile(int id, [FromForm] AdminProfileUpdateInputDto? dto, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _service.UpdateUserProfileAsync(id, dto ?? new AdminProfileUpdateInputDto(), cancellationToken);
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
