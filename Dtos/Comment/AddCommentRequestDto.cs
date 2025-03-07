using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Comment
{
    public class AddCommentRequestDto
    {
        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}