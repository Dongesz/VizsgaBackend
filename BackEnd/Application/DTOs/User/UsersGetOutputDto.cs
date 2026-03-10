using System.Security.Cryptography.X509Certificates;

namespace BackEnd.Application.DTOs.User
{
    public class UsersGetOutputDto
    {
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Bio { get; set; } = "";

        public string ProfilePictureUrl { get; set; } = "";

        public string? UserType { get; set; } = "User";
    }
}
