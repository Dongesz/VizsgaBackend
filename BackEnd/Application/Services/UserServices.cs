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
using BackEnd.Application.DTOs.User;
using BackEnd.Application.Helpers;

namespace BackEnd.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly UploadHelper _uploadHelper;

        public UsersService(DatabaseContext context, IMapper mapper, UploadHelper uploadhelper)
        {
            _context = context;
            _mapper = mapper;
            _uploadHelper = uploadhelper;
        }

        public async Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            if (users == null) return new ResponseOutputDto { Message = "Users not found!", Success = false};

            return new ResponseOutputDto { Message = "Successful fetch!", Success = true, Result = _mapper.Map<List<UsersGetOutputDto>>(users) }; 
        }
        public async Task<ResponseOutputDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false};
            return  new ResponseOutputDto { Message = "Successful fetch!", Success = true, Result = _mapper.Map<UsersGetOutputDto>(user) };  
        }
        public async Task<ResponseOutputDto> CreateAsync(UsersSendInputDto dto, CancellationToken cancellationToken = default)
        {
            var userToAdd = _mapper.Map<User>(dto);
            userToAdd.CreatedAt = DateTime.UtcNow;
            userToAdd.UpdatedAt = DateTime.UtcNow;
            userToAdd.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto?.Password);

            await _context.Users.AddAsync(userToAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var scoreToAdd = new Scoreboard
            {
                UserId = userToAdd.Id,
                TotalScore = 0,
                TotalXp = 0,
                LastUpdated = DateTime.UtcNow
            };
            await _context.Scoreboards.AddAsync(scoreToAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(userToAdd).Reference(u => u.Scoreboard).LoadAsync(cancellationToken);

            return new ResponseOutputDto { Message = "User created successfully!", Success = true, Result = _mapper.Map<UsersGetOutputDto>(userToAdd) };
        }
        public async Task<ResponseOutputDto> UpdateAsync(int id, UsersSendInputDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false };

            _mapper.Map(dto, user);
            user.UpdatedAt = DateTime.UtcNow;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto?.Password);

            await _context.SaveChangesAsync(cancellationToken);
            return new ResponseOutputDto { Message = "User updated successfully!", Success = true };
        }
        public async Task<ResponseOutputDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false };

            var scores = await _context.Scoreboards.Where(s => s.UserId == id).ToListAsync(cancellationToken);
            if (scores.Count > 0)
                _context.Scoreboards.RemoveRange(scores);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return new ResponseOutputDto { Message = "User deleted successfuly", Success = true };
        }

        public async Task<ResponseOutputDto> GetUserCountAsync(CancellationToken cancellationToken = default)
        {
            int? count = await _context.Users.CountAsync(cancellationToken);
            if (count == null) return new ResponseOutputDto { Message = "Couldn't fetch player count!", Success = false};
            var playerCount = new UserCountOutputDto
            {
                PlayerCount = count
            };
            return new ResponseOutputDto { Message = "Successful fetch!", Success = true, Result = playerCount };
        }
        public async Task<ResponseOutputDto> GetByIdUserScoreboardAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            var score = await _context.Scoreboards.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

            if (user == null && score == null) return new ResponseOutputDto { Message = "User not found!", Success = false };

            var userScoreboard = new UserScoreboardByIdOutputDto
            {
                Id = id,
                Name = user.Name,
                TotalScore = score.TotalScore,
                TotalXp = score.TotalXp
            };
            return new ResponseOutputDto { Message = "Succesful fetch!", Success = true, Result = userScoreboard};
        }
        public async Task<ResponseOutputDto> GetAllUserScoreboardAsync(CancellationToken cancellationToken = default)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            var scores = await _context.Scoreboards.ToListAsync(cancellationToken);

            if (users == null && scores == null) return new ResponseOutputDto { Message = "User not found!", Success = false };

            var userScoreboardList = new List<UserScoreboardGetOutputAllDto>();
            foreach (var item in users)
            {
                var score = scores.Find(x => x.UserId == item.Id);
                if (score == null) continue;

                var userScoreboard = new UserScoreboardGetOutputAllDto
                {
                    Name = item.Name,
                    TotalScore = score.TotalScore,
                    TotalXp = score.TotalXp
                };
                userScoreboardList.Add(userScoreboard);
            }
            return new ResponseOutputDto { Message = "Succesful fetch!", Success = true, Result = userScoreboardList };
        }
        public async Task<ResponseOutputDto> GetByIdResultAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            var score = await _context.Scoreboards.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

            if (user == null && score == null) return new ResponseOutputDto { Message = "User not found!", Success = false };

            var UserResult = new UserResultGetAllOutputDto
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                TotalScore = score.TotalScore,
                TotalXp = score.TotalXp
            };

            return new ResponseOutputDto { Message = "Succesful fetch!", Success = true, Result = UserResult };
        }

        public async Task<ResponseOutputDto> UpdateUserPasswordAsync(UserPasswordUpdateInputDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email, cancellationToken);

            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false };
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
        public async Task<ResponseOutputDto> UpdateUserNameAsync(int id, UserNameUpdateInputDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false };
            user.Name = dto.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseOutputDto { Message = "User Updated successfully!", Success = true };
        }

        public async Task<ResponseOutputDto> LoginAsync(UserLoginInputDto dto, CancellationToken cancellationToken = default)
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
                    return new ResponseOutputDto { Message = "Successful login!", Success = true };
            }
            return new ResponseOutputDto { Message = "Incorrect password!" ,Success = false };
        }
        public async Task<ResponseOutputDto> RegisterAsync(UserRegisterInputDto dto, CancellationToken cancellationToken = default)
        {
            var userToAdd = _mapper.Map<User>(dto);
            userToAdd.CreatedAt = DateTime.UtcNow;
            userToAdd.UpdatedAt = DateTime.UtcNow;
            userToAdd.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto?.Password);

            await _context.Users.AddAsync(userToAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var scoreToAdd = new Scoreboard
            {
                UserId = userToAdd.Id,
                TotalScore = 0,
                TotalXp = 0,
                LastUpdated = DateTime.UtcNow
            };
            await _context.Scoreboards.AddAsync(scoreToAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(userToAdd).Reference(u => u.Scoreboard).LoadAsync(cancellationToken);

            return new ResponseOutputDto { Message = "Successful registration!", Success = true, Result = _mapper.Map<UsersGetOutputDto>(userToAdd)};
        }

        public async Task<ResponseOutputDto> GetByIdProfilePicture(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (user == null)
                return new ResponseOutputDto { Message = "User not found!", Success = false };

            if (!string.IsNullOrEmpty(user.CustomPictureUrl))
            {
                return new ResponseOutputDto { Message = "Custom Picture!", Success = true, Result = "https://dongesz.com/images/" + user.CustomPictureUrl };
            }

            if (user.DefaultPictureUrl.HasValue)
            {
                var pic = await _context.DefaultPictures.FirstOrDefaultAsync(x => x.Id == user.DefaultPictureUrl.Value, cancellationToken);
                if (pic != null)
                    return new ResponseOutputDto { Message = "Default Picture!", Success = true, Result = pic };
            }

            return new ResponseOutputDto { Message = "Picture not found!", Success = false };
        }

        public async Task<ResponseOutputDto> UploadCustomProfilePicture(int id, IFormFile file, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            string existingFileName = user?.CustomPictureUrl;

            string rootPath = Directory.GetCurrentDirectory();

            var result = await _uploadHelper.UploadFileAsync(file, rootPath, existingFileName);

            if (result.Success == true && result.Result != null)
            {
                user.CustomPictureUrl = result.Result.ToString();
                _context.SaveChangesAsync();
            }

            return new ResponseOutputDto { Message = "Picture not found!", Success = true, Result = existingFileName};
        }
    }
}


