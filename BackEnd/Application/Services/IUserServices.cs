using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;

namespace BackEnd.Application.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UsersGetDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<UsersGetDto> CreateAsync(UsersSendDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, UsersSendDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<UserCountDto> GetUserCountAsync(CancellationToken cancellationToken = default);
        Task<List<UserScoreboardGetAllDto>> GetAllUserScoreboardAsync(CancellationToken cancellationToken = default);
        Task<UserScoreboardByIdDto> GetUserByIdScoreboardAsync(int id, CancellationToken cancellationToken = default);
        Task<string> UpdateUserPasswordAsync(UserPasswordUpdateDto dto, CancellationToken cancellationToken = default);   
        Task<UserResultGetAllDto> GetAllResultAsync(int id, CancellationToken cancellationToken = default);   
    }
}
