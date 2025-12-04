namespace BackEnd.Application.DTOs
{
    public class UserPasswordUpdateInputDto
    {
        public string? Email { get; set; }

        public string? OldPassword { get; set; }

        public string? NewPassword { get; set; }
    }
}
