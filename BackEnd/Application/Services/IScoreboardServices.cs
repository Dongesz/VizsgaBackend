using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;

namespace BackEnd.Application.Services
{
    // Interface az adott model service retegenek implementalasara - hasznos teszteles, es atlathatosag szempontjabol is
    public interface IScoreboardService
    {
        Task<IEnumerable<ScoreboardGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ScoreboardGetDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ScoreboardGetDto> CreateAsync(ScoreboardSendDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, ScoreboardSendDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}