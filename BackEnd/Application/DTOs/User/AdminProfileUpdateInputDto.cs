using System.ComponentModel.DataAnnotations;

namespace BackEnd.Application.DTOs.User
{
    public class AdminProfileUpdateInputDto
    {
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        [StringLength(255)]
        public string? Bio { get; set; }

        public IFormFile? ProfilePicture { get; set; }
    }
}
