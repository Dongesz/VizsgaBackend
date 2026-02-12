using System.ComponentModel.DataAnnotations;

namespace BackEnd.Application.DTOs.Scoreboard
{
    // Data transfer az adatok lekeresere
    public class ScoreboardSendInputDto
    {
        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }
    }
}
