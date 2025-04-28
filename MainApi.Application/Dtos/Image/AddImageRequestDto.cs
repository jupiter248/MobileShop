using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Image
{
    public class AddImageRequestDto
    {
        [Required]
        public string ImageName { get; set; } = string.Empty;
        [Required]
        public string path { get; set; } = string.Empty;
        [Required]
        public Boolean IsPrimary { get; set; }
    }
}