using Microsoft.AspNetCore.Identity.Data;
using RoleBasedAuth.Models.DTOs;

namespace RoleBasedAuth.Services
{
    public interface IAuth
    {
        Task<object> Register(RegisterRequestDto dto);
        Task<object> AssignRole(AssignRoleDto dto);
        /// <summary>Admin &lt;-&gt; User váltás: ha Admin a user, Userre vált; ha nincs Admin joga, Admint kap.</summary>
        Task<object> ToggleAdminRole(ToggleAdminRoleDto dto);
        Task<object> Login(LoginDto dto);     
        Task<object> DeleteUserByIdAsync(string authUserId);
        Task<object> ChangePasswordAsync(string userId, ChangePasswordDto dto);
        Task<object> CheckPasswordAsync(string userId, string password);
        Task<object> UpdateUserNameAsync(string authUserId, string newUserName);
    }
}
