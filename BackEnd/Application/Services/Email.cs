using BackEnd.Application.DTOs;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BackEnd.Application.Services
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;
        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseOutputDto> SendMail(SendEmailDto dto)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailSettings:EmailUserName").Value));
                email.To.Add(MailboxAddress.Parse(dto.To));
                email.Subject = dto.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = dto.Body };

                using var smtp = new SmtpClient();

                smtp.Connect(_configuration.GetSection("EmailSettings:EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);

                smtp.Authenticate(_configuration.GetSection("EmailSettings:EmailUserName").Value, _configuration.GetSection("EmailSettings:EmailPassword").Value);
                smtp.Send(email);

                smtp.Disconnect(true);

                return new ResponseOutputDto { Message = "Email Sent successfully!", Success = true };
            }
            catch (Exception ex)
            {

                return new ResponseOutputDto { Message = "Email couldnt be sent!", Success = false };
            }
            
        }
    }
}
