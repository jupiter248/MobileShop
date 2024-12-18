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
        private readonly IProductRepository _productRepo;

        public ImageController(IImageRepository imageRepo, IProductRepository productRepo)
        {
            _imageRepo = imageRepo;
            _productRepo = productRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            List<Image>? images = await _imageRepo.GetAllImagesAsync();
            if (images == null)
                return BadRequest();
            return Ok(images);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetImageById([FromRoute] int id)
        {
            Image? image = await _imageRepo.GetImageByIdAsync(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] AddImageRequestDto addImageRequestDto, [FromForm] UploadImage formFile , int productId)
        {
            IFormFile? imageFile = formFile.FormFile;
            if (!ModelState.IsValid) BadRequest(ModelState);
            if (!await _productRepo.ProductExistsAsync(productId)) return NotFound("The product does not exist");
            Image? image = addImageRequestDto.ToImageFromAdd(productId);
            await _imageRepo.AddImageAsync(image);
            return CreatedAtAction(nameof(GetImageById), new { id = image.Id }, image.ToImageDto());
        }
        [HttpPut]
        public async Task<IActionResult> EditImage([FromBody] EditImageRequestDto editImageRequestDto, int imageId)
        {
            Image? image = await _imageRepo.EditImageAsync(editImageRequestDto.ToImageFromEdit(), imageId);
            if (image == null) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveImage([FromRoute] int id)
        {
            Image? image = await _imageRepo.RemoveImageAsync(id);
            if (image == null) return NotFound();
            return NoContent();
        }
    }
}