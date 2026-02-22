using Microsoft.AspNetCore.Identity.Data;
using RoleBasedAuth.Models.DTOs;

namespace RoleBasedAuth.Services
{
    public interface IAuth
    {
        Task<object> Register(RegisterRequestDto dto);
        Task<object> AssignRole(AssignRoleDto dto);
        Task<object> Login(LoginDto dto);     
        Task<object> DeleteUserByIdAsync(string authUserId);
        Task<object> ChangePasswordAsync(string userId, ChangePasswordDto dto);
        Task<object> CheckPasswordAsync(string userId, string password);
    }
}
