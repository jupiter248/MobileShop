using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Image
{
    public class UploadImage
    {
        public IFormFile? FormFile { get; set; }
    }
}