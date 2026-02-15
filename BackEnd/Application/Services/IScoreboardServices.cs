using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.Scoreboard;

namespace BackEnd.Application.Services
{
    public interface IScoreboardService
    {
        Task<ResponseOutputDto> UpdateAsync(int id, ScoreboardSendInputDto dto, CancellationToken cancellationToken = default);

        Task<ResponseOutputDto> UpdateMyScoreboardAsync(int userId, ScoreboardSendInputDto dto, CancellationToken cancellationToken = default);
    }
}


