using System.ComponentModel.DataAnnotations;

namespace BackEnd.Application.DTOs
{
    public class UserPasswordUpdateInputDto
    {
        [Required]
        [StringLength(50)]
        public string? Email { get; set; }
        [Required]
        [StringLength(25)]
        public string? OldPassword { get; set; }
        [Required]
        [StringLength(25)]
        public string? NewPassword { get; set; }
    }
}
