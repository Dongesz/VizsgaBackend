namespace BackEnd.Application.DTOs
{
    public class UserScoreboardGetAllDto
    {
        public string? Name { get; set; } = null!;

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }
    }
}