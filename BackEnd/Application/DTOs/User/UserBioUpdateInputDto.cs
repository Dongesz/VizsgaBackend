using System.ComponentModel.DataAnnotations;

namespace BackEnd.Application.DTOs.User
{
    public class UserBioUpdateInputDto
    {
        [Required]
        [StringLength(255)]
        public string? Bio { get; set; }
    }
}
