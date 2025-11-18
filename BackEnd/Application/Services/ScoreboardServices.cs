using AutoMapper;
using BackEnd.Application.DTOs;
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

        public async Task<IEnumerable<ScoreboardGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await _context.Scoreboards
                                     .AsNoTracking()
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

        public async Task<ScoreboardGetDto> CreateAsync(ScoreboardSendDto dto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Scoreboard>(dto);
            entity.LastUpdated = System.DateTime.UtcNow;

            await _context.Scoreboards.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScoreboardGetDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, ScoreboardSendDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Scoreboards.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            existing.LastUpdated = System.DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Scoreboards.FindAsync(new object[] { id }, cancellationToken);
            if (existing == null) return false;

            _context.Scoreboards.Remove(existing);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
