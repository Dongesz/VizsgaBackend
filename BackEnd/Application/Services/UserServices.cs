using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersService> _logger;

        public UsersService(DatabaseContext context, IMapper mapper, ILogger<UsersService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UsersGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _context.Users
                                      .Include(u => u.Scoreboard)
                                      .AsNoTracking()
                                      .ToListAsync(cancellationToken);

            return _mapper.Map<List<UsersGetDto>>(users);
        }

        public async Task<UsersGetDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                                     .Include(u => u.Scoreboard)
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (user == null) return null;
            return _mapper.Map<UsersGetDto>(user);
        }

        public async Task<UsersGetDto> CreateAsync(UsersSendDto dto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<User>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.Users.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var scoreboard = new Scoreboard
            {
                UserId = entity.Id,
                TotalScore = 0,
                TotalXp = 0,
                LastUpdated = DateTime.UtcNow
            };
            await _context.Scoreboards.AddAsync(scoreboard, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(entity).Reference(u => u.Scoreboard).LoadAsync(cancellationToken);

            return _mapper.Map<UsersGetDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, UsersSendDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
            if (existing == null) return false;

            var scores = await _context.Scoreboards.Where(s => s.UserId == id).ToListAsync(cancellationToken);
            if (scores.Count > 0)
                _context.Scoreboards.RemoveRange(scores);

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<UserCountDto> GetUserCountAsync(CancellationToken cancellationToken = default)
        {
            var count = await _context.Users.CountAsync(cancellationToken);
            var dto = new UserCountDto
            {
                PlayerCount = count
            };

            
            return dto;

        }

        public async Task<UserScoreboardDto> GetUserScoreboardAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(id, cancellationToken);
            var score = await _context.Scoreboards.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

            if (user == null || score == null) return null;

         
            var dto = new UserScoreboardDto
            {
                Id = id,
                Name = user.Name,
                TotalScore = score.TotalScore,
                TotalXp = score.TotalXp
            };

            return dto;
        }
    }
}
