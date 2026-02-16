using BackEnd.Application.DTOs;

namespace BackEnd.Application.Services
{
    public interface IEmail
    {
        void SendMail(SendEmailDto dto);
    }
}
