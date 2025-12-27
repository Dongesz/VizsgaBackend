using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.User;

namespace BackEnd.Application.Services
{
    public interface IUsersService
    {
        // Crud
        Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> CreateAsync(UsersSendInputDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UpdateAsync(int id, UsersSendInputDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> DeleteAsync(int id, CancellationToken cancellationToken = default);

        // Gets
        Task<ResponseOutputDto> GetUserCountAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetAllUserScoreboardAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetByIdUserScoreboardAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetByIdResultAsync(int id, CancellationToken cancellationToken = default);
        
        // Updates
        Task<ResponseOutputDto> UpdateUserNameAsync(int id, UserNameUpdateInputDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UpdateUserPasswordAsync(UserPasswordUpdateInputDto dto, CancellationToken cancellationToken = default);

        // Login & register
        Task<ResponseOutputDto> LoginAsync(UserLoginInputDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> RegisterAsync(UserRegisterInputDto dto, CancellationToken cancellationToken = default);

        // Profile Pictures
        Task<ResponseOutputDto> GetByIdProfilePicture(int id, CancellationToken cancellationToken = default);
    }
}
