using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;

namespace BackEnd.Application.Services
{
    public interface IScoreboardService
    {
        Task<IEnumerable<ScoreboardGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ScoreboardGetDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ScoreboardGetDto> CreateAsync(ScoreboardSendDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, ScoreboardSendDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}