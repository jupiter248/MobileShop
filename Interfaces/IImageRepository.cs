using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models;

namespace MainApi.Interfaces
{
    public interface IImageRepository
    {
        Task<List<Image>?> GetAllImagesAsync();
        Task<Image?> GetImageByIdAsync(int imageId);
        Task<Image?> AddImageAsync(Image image);
        Task<Image?> EditImageAsync(Image image, int imageId);
        Task<Image?> RemoveImage(int imageId);
    }
}