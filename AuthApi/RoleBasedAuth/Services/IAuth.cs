using Microsoft.AspNetCore.Identity.Data;
using RoleBasedAuth.Models.DTOs;

namespace RoleBasedAuth.Services
{
    public interface IAuth
    {
        Task<object> Register(RegisterRequestDto dto);
        Task<object> AssignRole(AssignRoleDto dto);
        Task<object> Login(LoginDto dto);

        /// <summary>
        /// Permanently deletes an Identity user by their AuthUserId.
        /// Used by the BackEnd when a player deletes their account.
        /// Returns an anonymous object with success flag and message.
        /// </summary>
        Task<object> DeleteUserByIdAsync(string authUserId);

        /// <summary>
        /// Changes the password for the user identified by userId. Requires current password.
        /// </summary>
        Task<object> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    }
}
