using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;

namespace BackEnd.Application.Services
{
    // Interface az adott model service retegenek implementalasara - hasznos teszteles, es atlathatosag szempontjabol is
    public interface IScoreboardService
    {
        Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UpdateAsync(int id, ScoreboardSendDto dto, CancellationToken cancellationToken = default);
    }
}