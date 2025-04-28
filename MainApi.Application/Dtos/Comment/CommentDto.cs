using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? UserName { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedTime = DateTime.Now;
    }
}