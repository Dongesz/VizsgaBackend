namespace BackEnd.Application.DTOs
{
    public class UserResultGetAllDto
    {
        public int? Id;
        public string? Name { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public int? TotalScore { get; set; }

        public int? TotalXp { get; set; }
    }
}
