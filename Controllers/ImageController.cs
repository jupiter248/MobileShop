using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Image;
using MainApi.Interfaces;
using MainApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainApi.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepo;

        public ImageController(IImageRepository imageRepo)
        {
            _imageRepo = imageRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody] AddImageRequestDto addImageRequestDto, int productId)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);
            var image = addImageRequestDto.ToImageFromAdd(productId);
            await _imageRepo.AddImageAsync(image);
            return Ok(image);
        }
    }
}