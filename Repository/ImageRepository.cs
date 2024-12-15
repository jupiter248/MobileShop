using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Image?> AddImageAsync(Image image)
        {
            await _context.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public Task<Image?> EditImageAsync(Image image, int imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Image>?> GetAllImagesAsync()
        {
            var images = await _context.Images.ToListAsync();
            if (images == null)
                return null;
            return images;

        }

        public async Task<Image?> GetImageByIdAsync(int imageId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == imageId);
            if (image == null)
            {
                return null;
            }
            return image;
        }

        public Task<Image?> RemoveImage(int imageId)
        {
            throw new NotImplementedException();
        }
    }
}