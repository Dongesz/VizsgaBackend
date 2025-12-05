using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.Services
{
    // Service reteg - ez az osztaly vegzi az adat muveleteket
    public class ScoreboardServices : IScoreboardService
    {
        // Mezok a DI hez
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ScoreboardServices> _logger;

        // DI beallitasa a konstruktorban
        public ScoreboardServices(DatabaseContext context,IMapper mapper, ILogger<ScoreboardServices> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // Async muveletek kulonbozo celu vegpontokhoz cancellation tokennel egyut
        public async Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var score = await _context.Scoreboards.ToListAsync(cancellationToken);
            if (score == null) return new ResponseOutputDto { Message = "Scores not found!", Success = false};
            return new ResponseOutputDto { Message = "Successful fetch!", Success = true, Result = _mapper.Map<List<ScoreboardGetDto>>(score) }; 
        }

        public async Task<ResponseOutputDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var score = await _context.Scoreboards.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (score == null) return new ResponseOutputDto { Message = "Score not found!", Success = false };
            return new ResponseOutputDto { Message = "Successful fetch!", Success = true, Result = _mapper.Map<ScoreboardGetDto>(score) };
        }

        public async Task<ResponseOutputDto> UpdateAsync(int id, ScoreboardSendDto dto, CancellationToken cancellationToken = default)
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
