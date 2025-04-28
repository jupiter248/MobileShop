using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MainApi.Application.Dtos.Image
{
    public class UploadImage
    {
        [Required]
        public IFormFile? image { get; set; }
        [Required]
        public Boolean IsPrimary { get; set; }

    }
}