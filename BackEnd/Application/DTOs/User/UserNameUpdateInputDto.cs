using System.ComponentModel.DataAnnotations;

namespace BackEnd.Application.DTOs.User
{
    public class UserNameUpdateInputDto
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
    }
}
