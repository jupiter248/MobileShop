using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Image;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;

namespace MainApi.Infrastructure.Services.Internal
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepo;
        private readonly IProductRepository _productRepo;

        public ImageService(IImageRepository imageRepo, IProductRepository productRepo)
        {
            _imageRepo = imageRepo;
            _productRepo = productRepo;
        }
        public async Task<ImageDto> AddImageAsync(int productId, UploadImage uploadImage)
        {
            if (!await _productRepo.ProductExistsAsync(productId)) throw new KeyNotFoundException("The product does not exist");
            AddImageRequestDto? addImageRequestDto = await _imageRepo.StoreImage(uploadImage) ?? throw new KeyNotFoundException("Image file not found");
            Image? image = addImageRequestDto?.ToImageFromAdd(productId);
            await _imageRepo.AddImageAsync(image);
            return image.ToImageDto();
        }

        public async Task EditImageAsync(int imageId, EditImageRequestDto editImageRequestDto)
        {
            Image currentImage = await _imageRepo.GetImageByIdAsync(imageId) ?? throw new KeyNotFoundException("Image not found");
            await _imageRepo.EditImageAsync(editImageRequestDto.ToImageFromEdit(), currentImage);
        }

        public async Task<List<ImageDto>> GetAllImagesAsync()
        {
            List<Image> images = await _imageRepo.GetAllImagesAsync();
            List<ImageDto> imageDtos = images.Select(i => i.ToImageDto()).ToList();
            return imageDtos;
        }

        public async Task<ImageDto> GetImageByIdAsync(int imageId)
        {
            Image image = await _imageRepo.GetImageByIdAsync(imageId) ?? throw new KeyNotFoundException("Image not found");
            return image.ToImageDto();
        }

        public async Task RemoveImageAsync(int imageId)
        {
            Image image = await _imageRepo.GetImageByIdAsync(imageId) ?? throw new KeyNotFoundException("Image not found");
            await _imageRepo.RemoveImageAsync(image);

        }
    }
}