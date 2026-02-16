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

        [HttpPost]
        public ActionResult SendEmail(SendEmailDto dto)
        {
            _email.SendMail(dto);
            return Ok(new { message = "Sikeres email kuldes" });
        }
    }
}
