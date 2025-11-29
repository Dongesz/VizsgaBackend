namespace BackEnd.Application.DTOs
{
    public class UsersSendDto
    {
        public string? Name { get; set; } = null!;

        public string? Email { get; set; } = null!;
        public string? PasswordHash { get; set; } = null!;
        public string UserType { get; set; } = null!;


    }
}
