using System.ComponentModel.DataAnnotations;

namespace BackEnd.Application.DTOs.News
{
    public class NewsCreateInputDto
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = "";

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Content { get; set; } = "";

        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}

