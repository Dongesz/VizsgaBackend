using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.Scoreboard;

namespace BackEnd.Application.Services
{
    // Interface az adott model service retegenek implementalasara - hasznos teszteles, es atlathatosag szempontjabol is
    public interface IScoreboardService
    {
        Task<ResponseOutputDto> UpdateAsync(int id, ScoreboardSendInputDto dto, CancellationToken cancellationToken = default);

        /// <summary>Update scoreboard for the authenticated user (identity from JWT).</summary>
        Task<ResponseOutputDto> UpdateMyScoreboardAsync(int userId, ScoreboardSendInputDto dto, CancellationToken cancellationToken = default);
    }
}


