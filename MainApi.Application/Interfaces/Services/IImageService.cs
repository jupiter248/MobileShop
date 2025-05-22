using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Image;

namespace MainApi.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<List<ImageDto>> GetAllImagesAsync();
        Task<ImageDto> GetImageByIdAsync(int imageId);
        Task<ImageDto> AddImageAsync(int productId, UploadImage uploadImage);
        Task EditImageAsync(int imageId, EditImageRequestDto editImageRequestDto);
        Task RemoveImageAsync(int imageId);
    }
}