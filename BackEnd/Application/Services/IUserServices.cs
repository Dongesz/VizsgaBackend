using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;

namespace BackEnd.Application.Services
{
    public interface IUsersService
    {
        Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> CreateAsync(UsersSendDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UpdateAsync(int id, UsersSendDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetUserCountAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetAllUserScoreboardAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetUserByIdScoreboardAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UpdateUserPasswordAsync(UserPasswordUpdateInputDto dto, CancellationToken cancellationToken = default);   
        Task<ResponseOutputDto> GetAllResultAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> VerifyPasswordAsync(UserPasswordVerifyInputDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UpdateUserNameAsync(int id, UserNameUpdateDto dto, CancellationToken cancellationToken = default);
    }
}
