namespace BackEnd.Application.DTOs
{
    // Data transfer az adatok lekeresehez
    public class ScoreboardGetDto
    {
        public int Id { get; set; }

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }

    }
}
