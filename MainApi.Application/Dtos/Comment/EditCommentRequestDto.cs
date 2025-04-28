using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Comment
{
    public class EditCommentRequestDto
    {

        [Required]
        [Range(1, 10, ErrorMessage = "Rate must be between 1 and 10")]
        public int Rating { get; set; }
        [Required]
        [MinLength(40, ErrorMessage = "Text can not be Under 150 characters")]
        public string Text { get; set; } = string.Empty;
    }
}