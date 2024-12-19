using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Dtos.Image;
using MainApi.Interfaces;
using MainApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
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

        public async Task<Image?> EditImageAsync(Image imageModel, int imageId)
        {
            Image? image = await GetImageByIdAsync(imageId);
            if (image != null)
            {
                image.ImageName = imageModel.ImageName;
                image.Url = imageModel.Url;
                image.IsPrimary = imageModel.IsPrimary;
                image.ProductId = imageModel.ProductId;
                await _context.SaveChangesAsync();
            }
            return image;
        }

        public async Task<List<Image>?> GetAllImagesAsync()
        {
            var images = await _context.Images.ToListAsync();
            return images;
        }

        public async Task<Image?> GetImageByIdAsync(int imageId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == imageId);
            return image;
        }

        public async Task<Image?> RemoveImageAsync(int imageId)
        {
            Image? image = await _context.Images.FirstOrDefaultAsync(i => i.Id == imageId);
            if (image != null)
            {
                _context.Remove(image);
                await _context.SaveChangesAsync();
            }
            return image;
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