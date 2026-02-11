using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Connections;
using BackEnd.Application.DTOs.User;
using BackEnd.Application.Helpers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace BackEnd.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly UploadHelper _uploadHelper;
        private readonly ProfilePictureHelper _pictureHelper;
        private readonly IAuthApiClient _authApiClient;

        public UsersService(DatabaseContext context, IMapper mapper, UploadHelper uploadhelper, ProfilePictureHelper pictureHelper, IAuthApiClient authApiClient)
        {
            _context = context;
            _mapper = mapper;
            _uploadHelper = uploadhelper;
            _pictureHelper = pictureHelper;
            _authApiClient = authApiClient;
        }


        public async Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            if (users == null || users.Count == 0) return new ResponseOutputDto { Message = "Users not found!", Success = false };

            var result = new List<UsersGetOutputDto>();

            foreach (var user in users)
            {
                var dto = _mapper.Map<UsersGetOutputDto>(user);
                dto.ProfilePictureUrl = await _pictureHelper.GetProfilePictureUrlAsync(user.Id);

                result.Add(dto);
            }

            return new ResponseOutputDto { Message = "Successful fetch!", Success = true, Result = result };
        }
        public async Task<ResponseOutputDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false };

            var dto = _mapper.Map<UsersGetOutputDto>(user);
            dto.ProfilePictureUrl = await _pictureHelper.GetProfilePictureUrlAsync(user.Id);

            return new ResponseOutputDto { Message = "Successful fetch!", Success = true, Result = dto };
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
            if (count == null) return new ResponseOutputDto { Message = "Couldn't fetch player count!", Success = false };
            var playerCount = new UserCountOutputDto
            {
                PlayerCount = count
            };
            return new ResponseOutputDto { Message = "Successful fetch!", Success = true, Result = playerCount };
        }
        public async Task<ResponseOutputDto> GetAllUserScoreboardAsync(CancellationToken cancellationToken = default)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            var scores = await _context.Scoreboards.ToListAsync(cancellationToken);

            if (users == null && scores == null) return new ResponseOutputDto { Message = "User not found!", Success = false };

            var userScoreboardList = new List<UserScoreboardGetOutputAllDto>();
            foreach (var user in users)
            {
                var score = scores.Find(x => x.UserId == user.Id);
                if (score == null) continue;

                var userScoreboard = new UserScoreboardGetOutputAllDto
                {
                    Name = user.Name,
                    TotalScore = score.TotalScore,
                    TotalXp = score.TotalXp,
                    ProfilePictureUrl = await _pictureHelper.GetProfilePictureUrlAsync(user.Id)
                };
                userScoreboardList.Add(userScoreboard);
            }
            return new ResponseOutputDto { Message = "Succesful fetch!", Success = true, Result = userScoreboardList };
        }
        public async Task<ResponseOutputDto> GetByIdResultAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            var score = await _context.Scoreboards.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

            if (user == null || score == null) return new ResponseOutputDto { Message = "User not found!", Success = false };

            var UserResult = new UserResultGetAllOutputDto
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Bio = user.Bio,
                ProfilePictureUrl = await _pictureHelper.GetProfilePictureUrlAsync(id),
                TotalScore = score.TotalScore,
                TotalXp = score.TotalXp,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return new ResponseOutputDto { Message = "Succesful fetch!", Success = true, Result = UserResult };
        }

        public async Task<ResponseOutputDto> UpdateUserNameAsync(int id, UserNameUpdateInputDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false };
            user.Name = dto.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseOutputDto { Message = "User name Updated successfully!", Success = true };
        }
        public async Task<ResponseOutputDto> UpdateUserBioAsync(int id, UserBioUpdateInputDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false };
            user.Bio = dto.Bio;
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseOutputDto { Message = "User bio Updated successfully!", Success = true };
        }

        public async Task<ResponseOutputDto> UploadCustomProfilePicture(int id, IFormFile file, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (user == null) return new ResponseOutputDto { Message = "User not found!", Success = false };
            else if (file == null) return new ResponseOutputDto { Message = "File not found!", Success = false };
            string existingFileName = user?.CustomPictureUrl;

            string rootPath = Directory.GetCurrentDirectory();

            var result = await _uploadHelper.UploadFileAsync(file, rootPath, existingFileName);

            if (result.Success == true && result.Result != null)
            {
                user.CustomPictureUrl = result.Result.ToString();
                await _context.SaveChangesAsync();
                return new ResponseOutputDto { Message = "Custom picture uploaded successfully!", Success = true, Result = result.Result };
            }
            return new ResponseOutputDto { Message = "Some error occured during file upload!", Success = false, Result = result.Result };
        }


        public async Task<ResponseOutputDto> GetMeAsync(ClaimsPrincipal userClaims, CancellationToken cancellationToken = default)
        {
            var user = await EnsureUserExistsAsync(userClaims, cancellationToken);
            var dto = _mapper.Map<UsersGetOutputDto>(user);
            dto.ProfilePictureUrl = await _pictureHelper.GetProfilePictureUrlAsync(user.Id);
            return new ResponseOutputDto { Message = "OK", Success = true, Result = dto };
        }

        public async Task<ResponseOutputDto> GetMyResultAsync(ClaimsPrincipal userClaims, CancellationToken cancellationToken = default)
        {
            var user = await EnsureUserExistsAsync(userClaims, cancellationToken);
            return await GetByIdResultAsync(user.Id, cancellationToken);
        }

        public async Task<ResponseOutputDto> UpdateMyNameAsync(ClaimsPrincipal userClaims, UserNameUpdateInputDto dto, CancellationToken cancellationToken = default)
        {
            var user = await EnsureUserExistsAsync(userClaims, cancellationToken);
            return await UpdateUserNameAsync(user.Id, dto, cancellationToken);
        }

        public async Task<ResponseOutputDto> UpdateMyBioAsync(ClaimsPrincipal userClaims, UserBioUpdateInputDto dto, CancellationToken cancellationToken = default)
        {
            var user = await EnsureUserExistsAsync(userClaims, cancellationToken);
            return await UpdateUserBioAsync(user.Id, dto, cancellationToken);
        }

        public async Task<ResponseOutputDto> UploadMyProfilePictureAsync(ClaimsPrincipal userClaims, IFormFile file, CancellationToken cancellationToken = default)
        {
            var user = await EnsureUserExistsAsync(userClaims, cancellationToken);
            return await UploadCustomProfilePicture(user.Id, file, cancellationToken);
        }

        public async Task<ResponseOutputDto> DeleteMeAsync(ClaimsPrincipal userClaims, CancellationToken cancellationToken = default)
        {
            // Resolve current user from JWT and local database
            var user = await EnsureUserExistsAsync(userClaims, cancellationToken);

            // First, ask AuthApi to delete the underlying Identity user
            var deletedInAuth = await _authApiClient.DeleteIdentityUserAsync(user.AuthUserId, cancellationToken);
            if (!deletedInAuth)
            {
                return new ResponseOutputDto
                {
                    Message = "Failed to delete Identity user in AuthApi.",
                    Success = false
                };
            }

            // Then delete related scoreboard and local user record
            var scores = await _context.Scoreboards
                .Where(s => s.UserId == user.Id)
                .ToListAsync(cancellationToken);

            if (scores.Count > 0)
            {
                _context.Scoreboards.RemoveRange(scores);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseOutputDto
            {
                Message = "Account deleted successfully.",
                Success = true
            };
        }

        public async Task<User> EnsureUserExistsAsync(
      ClaimsPrincipal userClaims,
      CancellationToken cancellationToken = default)
        {
            var authUserId =
                userClaims.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ??
                userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(authUserId))
                throw new Exception("JWT does not contain sub / nameidentifier claim");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.AuthUserId == authUserId, cancellationToken);

            if (user != null)
                return user;

            var username =
                userClaims.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value
                ?? userClaims.FindFirst(ClaimTypes.Name)?.Value
                ?? userClaims.Identity?.Name
                ?? "New player";

            var email =  userClaims.FindFirst(ClaimTypes.Email)?.Value
                ?? "";

            user = new User
            {
                AuthUserId = authUserId,
                Name = username,
                Email = email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var scoreboard = new Scoreboard
            {
                UserId = user.Id,
                TotalScore = 0,
                TotalXp = 0,
                LastUpdated = DateTime.UtcNow
            };

            _context.Scoreboards.Add(scoreboard);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}