using System.Security.Cryptography.X509Certificates;

namespace BackEnd.Application.DTOs
{
    public class UsersGetDto
    {
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public string? Email { get; set; } = null!;
     
        public string? UserType { get; set; } = null!;
    }
}
