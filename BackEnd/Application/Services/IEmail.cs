using BackEnd.Application.DTOs;

namespace BackEnd.Application.Services
{
    public interface IEmail
    {
        Task<ResponseOutputDto> SendMail(SendEmailDto dto);
    }
}
