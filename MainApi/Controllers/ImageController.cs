using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Image;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainApi.Api.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            List<ImageDto> images = await _imageService.GetAllImagesAsync();
            return Ok(images);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetImageById([FromRoute] int id)
        {
            ImageDto? image = await _imageService.GetImageByIdAsync(id);
            return Ok(image);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] UploadImage uploadImage, int productId)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);
            ImageDto image = await _imageService.AddImageAsync(productId, uploadImage);
            return CreatedAtAction(nameof(GetImageById), new { id = image.Id }, image);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> EditImage([FromBody] EditImageRequestDto editImageRequestDto, int imageId)
        {
            await _imageService.EditImageAsync(imageId, editImageRequestDto);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveImage([FromRoute] int id)
        {
            await _imageService.RemoveImageAsync(id);
            return NoContent();
        }
    }
}