using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoleBasedAuth.Models;
using RoleBasedAuth.Models.DTOs;

namespace RoleBasedAuth.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public TokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);

            // A szerepkörökből egy egyszerű "User" / "Admin" stringet számolunk ki.
            var roleList = roles?.ToList() ?? new List<string>();
            bool isAdmin = roleList.Any(r => string.Equals(r, "Admin", StringComparison.OrdinalIgnoreCase));
            var userType = isAdmin ? "Admin" : "User";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
                new Claim(ClaimTypes.Name, applicationUser.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.UniqueName, applicationUser.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, applicationUser.Email ?? string.Empty),
                // Egyszerűen olvasható típus: "User" vagy "Admin"
                new Claim("userType", userType)
            };

            // Identity-nek továbbra is adjuk a konkrét role-okat (Admin, User, stb.)
            claims.AddRange(roleList.Select(r => new Claim(ClaimTypes.Role, r)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
