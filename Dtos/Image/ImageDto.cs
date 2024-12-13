using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Image
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public Boolean IsPrimary { get; set; }
    }
}