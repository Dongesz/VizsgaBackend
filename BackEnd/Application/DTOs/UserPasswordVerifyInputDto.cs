using System.ComponentModel.DataAnnotations;

namespace BackEnd.Application.DTOs
{
    public class UserPasswordVerifyInputDto
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
    }
}
