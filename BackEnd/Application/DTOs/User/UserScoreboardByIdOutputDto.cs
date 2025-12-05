namespace BackEnd.Application.DTOs.User
{
    public class UserScoreboardByIdOutputDto
    {
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }
    }
}
