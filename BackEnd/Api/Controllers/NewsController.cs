using System;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs.News;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        /// <summary>Összes hír / patch notes lekérése (mindenki számára elérhető).</summary>
        [HttpGet]
        [AllowAnonymous]
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
                return Problem(detail: ex?.InnerException?.Message ?? ex.Message);
            }
        }

        /// <summary>Új hír / patch notes létrehozása (csak Admin).</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [RequestSizeLimit(10_485_760)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] NewsCreateInputDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateAsync(dto, cancellationToken);
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

