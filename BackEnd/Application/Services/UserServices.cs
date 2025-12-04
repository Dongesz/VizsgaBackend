using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Connections;

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
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto?.Password);

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
            existing.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto?.Password);

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

        public async Task<UserScoreboardByIdDto> GetUserByIdScoreboardAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            var score = await _context.Scoreboards.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

            if (user == null || score == null) return null;

            var dto = new UserScoreboardByIdDto
            {
                Id = id,
                Name = user.Name,
                TotalScore = score.TotalScore,
                TotalXp = score.TotalXp
            };
            return dto;
        }

        public async Task<List<UserScoreboardGetAllDto>> GetAllUserScoreboardAsync(CancellationToken cancellationToken = default)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            var scores = await _context.Scoreboards.ToListAsync(cancellationToken);

            if (users == null || scores == null) return null;

            var result = new List<UserScoreboardGetAllDto>();
            foreach (var item in users)
            {
                var score = scores.Find(x => x.UserId == item.Id);
                if (score == null) continue;

                var dto = new UserScoreboardGetAllDto
                {
                    Name = item.Name,
                    TotalScore = score.TotalScore,
                    TotalXp = score.TotalXp
                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<ResponseOutputDto> UpdateUserPasswordAsync(UserPasswordUpdateInputDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email, cancellationToken);

            if (user == null) return new ResponseOutputDto { Message = "No user exist with that email adress!", Success = false };
            else if (dto.OldPassword == dto.NewPassword) return new ResponseOutputDto { Message = "Old and new password must be differnt!", Success = false };

            var verify = BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash);
            if (verify)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _context.SaveChangesAsync(cancellationToken);
                return new ResponseOutputDto { Message = "Password changed successfully", Success = true };
            }
            else return new ResponseOutputDto { Message = "User verification denied!", Success = false };
        }

        public async Task<UserResultGetAllDto> GetAllResultAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            var score = await _context.Scoreboards.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

            if (user == null || score == null) return null;

            var dto = new UserResultGetAllDto
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                TotalScore = score.TotalScore,
                TotalXp = score.TotalXp
            };

            return dto;
        }

        public async Task<ResponseOutputDto> VerifyPasswordAsync(UserPasswordVerifyInputDto dto, CancellationToken cancellationToken = default)
        {
            var name = await _context.Users.FirstOrDefaultAsync(x => x.Name == dto.Name, cancellationToken);
            var email = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email, cancellationToken);

            if(name == null && email == null) return new ResponseOutputDto { Message = "Incorrect name or email address!", Success = false };

            if (name != null)
            {
                var verify = BCrypt.Net.BCrypt.Verify(dto.Password, name.PasswordHash);
                if (verify) return new ResponseOutputDto { Message = "Successfull login!" ,Success = true };
            }
            else if (email != null)
            {
                var verify = BCrypt.Net.BCrypt.Verify(dto.Password, email.PasswordHash);
                if (verify)
                    return new ResponseOutputDto { Message = "Successfull login!", Success = true };
            }
            return new ResponseOutputDto { Message = "Incorrect password!" ,Success = false };
        }
    }
}