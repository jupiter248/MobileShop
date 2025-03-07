using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Comment
{
    public class CommentDto
    {
        public string? ProductName { get; set; }
        public string? UserName { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedTime = DateTime.Now;
    }
}