using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Persistence.Data;
using MainApi.Application.Dtos.Image;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MainApi.Application.Interfaces.Repositories;

namespace MainApi.Persistence.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _webHostEnvironment;
        public ImageRepository(ApplicationDbContext context, IHostingEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Image?> AddImageAsync(Image image)
        {
            await _context.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task EditImageAsync(Image newModel, Image currentImage)
        {
            currentImage.ImageName = newModel.ImageName;
            currentImage.Url = newModel.Url;
            currentImage.IsPrimary = newModel.IsPrimary;
            currentImage.ProductId = newModel.ProductId;
            await _context.SaveChangesAsync();

        }

        public async Task<List<Image>> GetAllImagesAsync()
        {
            var images = await _context.Images.ToListAsync();
            return images;
        }

        public async Task<Image?> GetImageByIdAsync(int imageId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == imageId);
            return image;
        }

        public async Task RemoveImageAsync(Image image)
        {

            _context.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<AddImageRequestDto?> StoreImage(UploadImage uploadImage)
        {
            IFormFile? imageFile = uploadImage.image;
            string? imagePath = null;
            if (imageFile != null)
            {
                var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(imageFile.FileName)}";
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                imagePath = Path.Combine("images", fileName);
                return new AddImageRequestDto()
                {
                    ImageName = imageFile.FileName,
                    path = imagePath,
                    IsPrimary = uploadImage.IsPrimary
                };
            }
            else return null;
        }
    }
}