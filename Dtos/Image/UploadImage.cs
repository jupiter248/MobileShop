using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Image
{
    public class UploadImage
    {
        [Required]
        public IFormFile? image { get; set; }
        [Required]
        public Boolean IsPrimary { get; set; }

    }
}