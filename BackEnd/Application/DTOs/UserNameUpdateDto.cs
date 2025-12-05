using System.ComponentModel.DataAnnotations;

namespace BackEnd.Application.DTOs
{
    public class UserNameUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
    }
}
