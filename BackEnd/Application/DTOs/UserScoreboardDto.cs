namespace BackEnd.Application.DTOs
{
    public class UserScoreboardDto
    {
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }
    }
}
