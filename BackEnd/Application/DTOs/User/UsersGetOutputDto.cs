using System.Security.Cryptography.X509Certificates;

namespace BackEnd.Application.DTOs.User
{
    public class UsersGetOutputDto
    {
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Bio { get; set; } = null;

        public string ProfilePictureUrl { get; set; } = null;

        public string? UserType { get; set; } = null!;
    }
}
