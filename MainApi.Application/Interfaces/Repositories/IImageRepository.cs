using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Image;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<List<Image>?> GetAllImagesAsync();
        Task<Image?> GetImageByIdAsync(int imageId);
        Task<Image?> AddImageAsync(Image image);
        Task<Image?> EditImageAsync(Image image, int imageId);
        Task<Image?> RemoveImageAsync(int imageId);
        Task<AddImageRequestDto?> StoreImage(UploadImage uploadImage);
    }
}