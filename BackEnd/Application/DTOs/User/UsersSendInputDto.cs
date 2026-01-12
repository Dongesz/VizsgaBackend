using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Application.DTOs.User
{
    public class UsersSendInputDto
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [StringLength(25)]
        public string? Password { get; set; }

        [JsonIgnore]
        public string UserType { get; set; } = "player";
    }
}
