namespace BackEnd.Application.DTOs.Scoreboard
{
    // Data transfer az adatok lekeresehez
    public class ScoreboardGetOutputDto
    {
        public int Id { get; set; }

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }

    }
}
