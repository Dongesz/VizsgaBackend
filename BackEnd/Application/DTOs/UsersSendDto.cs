using System.Text.Json.Serialization;

namespace BackEnd.Application.DTOs
{
    public class UsersSendDto
    {
        public string? Name { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Password { get; set; } = null!;

        [JsonIgnore]
        public string UserType { get; set; } = "player";
    }
}
