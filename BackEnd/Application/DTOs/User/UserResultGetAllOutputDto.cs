namespace BackEnd.Application.DTOs.User
{
    public class UserResultGetAllOutputDto
    {
        public int? Id;
        public string? Name { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Bio { get; set; } = "";

        public string? UserType { get; set; } = "User";


        public string ProfilePictureUrl { get; set; } = "";

        public int? TotalScore { get; set; }

        public int? TotalKills { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
