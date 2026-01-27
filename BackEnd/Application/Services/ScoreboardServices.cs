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
        private readonly ILogger<ScoreboardServices> _logger;

        public ScoreboardServices(DatabaseContext context,IMapper mapper, ILogger<ScoreboardServices> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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
