namespace BackEnd.Application.DTOs.User
{
    public class UserResultGetAllOutputDto
    {
        public int? Id;
        public string? Name { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
