using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Image
{
    public class AddImageRequestDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name length can not be over 20")]
        public string ImageName { get; set; } = string.Empty;
        [MinLength(20, ErrorMessage = "Url length can not be under 20")]
        public string Url { get; set; } = string.Empty;
        [Required]
        public Boolean IsPrimary { get; set; }
    }
}