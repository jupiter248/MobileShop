using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Image;
using MainApi.Application.Interfaces;
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
        private readonly IImageRepository _imageRepo;
        private readonly IProductRepository _productRepo;

        public ImageController(IImageRepository imageRepo, IProductRepository productRepo)
        {
            _imageRepo = imageRepo;
            _productRepo = productRepo;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            List<Image>? images = await _imageRepo.GetAllImagesAsync();
            if (images == null)
                return BadRequest();
            return Ok(images);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetImageById([FromRoute] int id)
        {
            Image? image = await _imageRepo.GetImageByIdAsync(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] UploadImage uploadImage, int productId)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);
            if (!await _productRepo.ProductExistsAsync(productId)) return NotFound("The product does not exist");
            AddImageRequestDto? addImageRequestDto = await _imageRepo.StoreImage(uploadImage);
            Image? image = addImageRequestDto?.ToImageFromAdd(productId);
            if (image != null)
            {
                await _imageRepo.AddImageAsync(image);
                return CreatedAtAction(nameof(GetImageById), new { id = image.Id }, image.ToImageDto());
            }
            else return BadRequest();

        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> EditImage([FromBody] EditImageRequestDto editImageRequestDto, int imageId)
        {
            Image? image = await _imageRepo.EditImageAsync(editImageRequestDto.ToImageFromEdit(), imageId);
            if (image == null) return NotFound();
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveImage([FromRoute] int id)
        {
            Image? image = await _imageRepo.RemoveImageAsync(id);
            if (image == null) return NotFound();
            return NoContent();
        }
    }
}