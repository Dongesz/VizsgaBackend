using System;

namespace BackEnd.Domain.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Image { get; set; } = "";
        public DateTime Date { get; set; }
        public string Content { get; set; } = "";
    }
}

