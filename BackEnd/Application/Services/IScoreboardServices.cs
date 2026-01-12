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
    }
}


