using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using RoleBasedAuth.Data;
using RoleBasedAuth.Models;
using RoleBasedAuth.Models.DTOs;

namespace RoleBasedAuth.Services
{
    public class Auth : IAuth
    {
		private readonly AppDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGenerator _tokenGenerator;
        public Auth(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenGenerator tokenGenerator = null)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<object> AssignRole(AssignRoleDto dto)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(x => x.NormalizedUserName == dto.UserName);
                if (user != null)
                {
                    if (!_roleManager.RoleExistsAsync(dto.RoleName).GetAwaiter().GetResult())
                    {
                        _roleManager.CreateAsync(new IdentityRole(dto.RoleName)).GetAwaiter().GetResult();
                    }
                    await _userManager.AddToRoleAsync(user, dto.RoleName);
                    return new { result = user, message = "Role successfully asigned!", success = true };
                }
                return new { result = "", message = "Failed to asign role!", success = false };
            }
            catch (Exception ex)
            {

                return new { result = "", message = ex.Message, success = false };
            }
        }

        public async Task<object> ToggleAdminRole(ToggleAdminRoleDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto?.UserName))
                    return new { result = (object?)null, message = "UserName is required.", success = false };

                var user = await _userManager.FindByNameAsync(dto.UserName);
                if (user == null)
                    return new { result = (object?)null, message = "User not found.", success = false };

                const string adminRole = "Admin";
                const string userRole = "User";

                if (!await _roleManager.RoleExistsAsync(adminRole))
                    await _roleManager.CreateAsync(new IdentityRole(adminRole));
                if (!await _roleManager.RoleExistsAsync(userRole))
                    await _roleManager.CreateAsync(new IdentityRole(userRole));

                var roles = await _userManager.GetRolesAsync(user);
                var hasAdmin = roles.Contains(adminRole, StringComparer.OrdinalIgnoreCase);

                if (hasAdmin)
                {
                    await _userManager.RemoveFromRoleAsync(user, adminRole);
                    await _userManager.AddToRoleAsync(user, userRole);
                    return new { result = user, message = "Role switched to User.", success = true };
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole);
                    await _userManager.AddToRoleAsync(user, adminRole);
                    return new { result = user, message = "Role switched to Admin.", success = true };
                }
            }
            catch (Exception ex)
            {
                return new { result = (object?)null, message = ex.Message, success = false };
            }
        }

        public async Task<object> Login(LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto?.UserName) || string.IsNullOrWhiteSpace(dto?.Password))
            {
                return new { message = "Login (username or email) and password are required!", success = false };
            }

            var login = dto.UserName.Trim();

            // Először próbáljuk felhasználónévként
            var user = await _userManager.FindByNameAsync(login);

            // Ha nem találjuk, próbáljuk e‑mailként
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(login);
            }

            if (user == null)
            {
                return new { message = "Invalid username/email or password!", success = false };
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isValidPassword)
            {
                return new { message = "Invalid password!", success = false };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenGenerator.GenerateToken(user, roles);

            return new
            {
                result = new
                {
                    user.UserName,
                    user.Email
                },
                token,
                success = true
            };
        }

        public async Task<object> Register(RegisterRequestDto registerRequestDto)
        {
			try
			{
                var user = new ApplicationUser
                {
                    UserName = registerRequestDto.UserName,
                    FullName = registerRequestDto.FullName,
                    Email = registerRequestDto.Email
                };
                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

                if (result.Succeeded)
                {
                    var userReturn = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == registerRequestDto.UserName);

                    return new { result = userReturn, message = "Account created successfully!", success = true };
                }

                return new { result = "", message = result.Errors.FirstOrDefault().Description };
            }
			catch (Exception ex)
            {

                return new { result = "", message = ex.Message, success = false };
            }
        }

        public async Task<object> DeleteUserByIdAsync(string authUserId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authUserId))
                {
                    return new { result = (object?)null, success = false, message = "Invalid auth user id." };
                }

                var user = await _userManager.FindByIdAsync(authUserId);
                if (user == null)
                {
                    return new { result = (object?)null, success = false, message = "User not found." };
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return new { result = (object?)null, success = true, message = "User deleted successfully." };
                }

                var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Unknown error during delete.";
                return new { result = (object?)null, success = false, message = errorMessage };
            }
            catch (Exception ex)
            {
                return new { result = (object?)null, success = false, message = ex.Message };
            }
        }

        public async Task<object> UpdateUserNameAsync(string authUserId, string newUserName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authUserId) || string.IsNullOrWhiteSpace(newUserName))
                {
                    return new { result = (object?)null, success = false, message = "Invalid auth user id or new user name." };
                }

                var user = await _userManager.FindByIdAsync(authUserId);
                if (user == null)
                {
                    return new { result = (object?)null, success = false, message = "User not found." };
                }

                user.UserName = newUserName;
                user.NormalizedUserName = newUserName.ToUpperInvariant();

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new { result = user, success = true, message = "Username updated successfully." };
                }

                var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Unknown error during username update.";
                return new { result = (object?)null, success = false, message = errorMessage };
            }
            catch (Exception ex)
            {
                return new { result = (object?)null, success = false, message = ex.Message };
            }
        }

        public async Task<object> ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return new { success = false, message = "User id is required." };
                }

                if (string.IsNullOrWhiteSpace(dto?.CurrentPassword) || string.IsNullOrWhiteSpace(dto?.NewPassword))
                {
                    return new { success = false, message = "Current password and new password are required." };
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new { success = false, message = "User not found." };
                }

                var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
                if (result.Succeeded)
                {
                    return new { success = true, message = "Password changed successfully." };
                }

                var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Failed to change password.";
                return new { success = false, message = errorMessage };
            }
            catch (Exception ex)
            {
                return new { success = false, message = ex.Message };
            }
        }

        public async Task<object> CheckPasswordAsync(string userId, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return new { success = false, message = "User id is required." };
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new { success = false, message = "User not found." };
                }

                var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
                return new { success = isValidPassword };
            }
            catch (Exception ex)
            {
                return new { success = false, message = ex.Message };
            }
        }
       
    }
}
