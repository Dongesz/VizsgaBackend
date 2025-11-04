namespace BackEnd.Application.DTOs
{
    public class UserSendDto
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;
    }
}
