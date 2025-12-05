namespace BackEnd.Application.DTOs.User
{
    public class UserScoreboardGetOutputAllDto
    {
        public string? Name { get; set; } = null!;

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }
    }
}