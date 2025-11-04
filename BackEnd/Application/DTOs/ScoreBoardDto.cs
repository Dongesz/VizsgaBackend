namespace BackEnd.Application.DTOs
{
    public class ScoreBoardDto
    {
        public ulong UserId { get; set; }

        public ulong? TotalScore { get; set; }

        public uint? Wins { get; set; }

        public uint? Rounds { get; set; }

    }
}
