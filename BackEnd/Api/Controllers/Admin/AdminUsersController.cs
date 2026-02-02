using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.User;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Api.Controllers.Admin
{
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
    }
}
