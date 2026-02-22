namespace BackEnd.Application.DTOs.User
{
    public class UserLeaderboardGetOutputAllDto
    {
        public string? Name { get; set; } = null!;

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }
        public string ProfilePictureUrl { get; set; } = null!;
    }
}
