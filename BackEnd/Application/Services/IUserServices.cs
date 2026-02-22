using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.User;
using BackEnd.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Application.Services
{
    public interface IUsersService
    {
        // Crud
        Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> DeleteAsync(int id, CancellationToken cancellationToken = default);

        // Gets
        Task<ResponseOutputDto> GetPlayerCountAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetLeaderboardAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> GetByIdResultAsync(int id, CancellationToken cancellationToken = default);
        
        // Updates
        Task<ResponseOutputDto> UpdateUserNameAsync(int id, UserNameUpdateInputDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UpdateUserBioAsync(int id, UserBioUpdateInputDto dto, CancellationToken cancellationToken = default);

        Task<ResponseOutputDto> UpdateMyNameAsync(ClaimsPrincipal userClaims, UserNameUpdateInputDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UpdateMyBioAsync(ClaimsPrincipal userClaims, UserBioUpdateInputDto dto, CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> UploadMyProfilePictureAsync(ClaimsPrincipal userClaims, IFormFile file, CancellationToken cancellationToken = default);

        Task<ResponseOutputDto> UploadCustomProfilePicture(int id, IFormFile file, CancellationToken cancellationToken = default);

        Task<User> EnsureUserExistsAsync(ClaimsPrincipal userClaims, CancellationToken cancellationToken = default);

        Task<ResponseOutputDto> GetMeAsync(ClaimsPrincipal userClaims, CancellationToken cancellationToken = default);

        Task<ResponseOutputDto> GetMyResultAsync(ClaimsPrincipal userClaims, CancellationToken cancellationToken = default);

       
        Task<ResponseOutputDto> DeleteMeAsync(ClaimsPrincipal userClaims, CancellationToken cancellationToken = default);

        Task<ResponseOutputDto> UpdateUserProfileAsync(int id, AdminProfileUpdateInputDto dto, CancellationToken cancellationToken = default);
    }
}
