using System.Threading;
using BackEnd.Application.DTOs;
using BackEnd.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmail _email;
        public EmailController(IEmail email)
        {
            this._email = email;
        }

        /// <summary>Email küldés custom bodyval.</summary>
        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailDto dto)
        {
            try
            {
                var email = await _email.SendMail(dto);
                return Ok(email);
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
