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
        public async Task<IEnumerable<ScoreboardGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await _context.Scoreboards
                                     .AsNoTracking() // Csak olvasunk, nem modositunk, ez gyorsabb mukodeshez vezet
                                     .ToListAsync(cancellationToken);
            return _mapper.Map<List<ScoreboardGetDto>>(list);
        }

        public async Task<ScoreboardGetDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Scoreboards
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (entity == null) return null;
            return _mapper.Map<ScoreboardGetDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, ScoreboardSendDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Scoreboards.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            existing.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
