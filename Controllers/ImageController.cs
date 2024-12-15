using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Image;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models;
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
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _imageRepo.GetAllImagesAsync();
            if (images == null)
                return BadRequest();
            return Ok(images);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetImageById([FromRoute] int id)
        {
            var image = _imageRepo.GetImageByIdAsync(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody] AddImageRequestDto addImageRequestDto, int productId)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);
            var image = addImageRequestDto.ToImageFromAdd(productId);
            await _imageRepo.AddImageAsync(image);
            return CreatedAtAction(nameof(GetImageById), new { id = image.Id }, image.ToImageDto());
        }
    }
}