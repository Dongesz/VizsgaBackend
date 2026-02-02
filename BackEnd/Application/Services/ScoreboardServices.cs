using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.Scoreboard;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.Services
{
    public class ScoreboardServices : IScoreboardService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ScoreboardServices(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseOutputDto> UpdateAsync(int id, ScoreboardSendInputDto dto, CancellationToken cancellationToken = default)
        {
            var score = await _context.Scoreboards.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (score == null) return new ResponseOutputDto { Message = "Score not found!", Success = false };
            _mapper.Map(dto, score);
            score.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return new ResponseOutputDto { Message = "Score updated successfully!", Success = true};
        }
    }
}
