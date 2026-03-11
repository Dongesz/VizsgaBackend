using System;

namespace BackEnd.Application.DTOs.News
{
    public class NewsGetOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Image { get; set; } = "";
        public DateTime Date { get; set; }
        public string Content { get; set; } = "";
    }
}

