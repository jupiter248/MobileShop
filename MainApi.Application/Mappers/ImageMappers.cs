using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Image;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;

namespace MainApi.Application.Mappers
{
    public static class ImageMappers
    {
        public static ImageDto ToImageDto(this Image image)
        {
            return new ImageDto()
            {
                Id = image.Id,
                ImageName = image.ImageName,
                IsPrimary = image.IsPrimary,
                ProductId = image.ProductId,
                Url = image.Url
            };
        }
        public static Image ToImageFromAdd(this AddImageRequestDto addImageRequestDto, int productId)
        {
            return new Image()
            {
                ImageName = addImageRequestDto.ImageName,
                Url = addImageRequestDto.path,
                IsPrimary = addImageRequestDto.IsPrimary,
                ProductId = productId
            };
        }
        public static Image ToImageFromEdit(this EditImageRequestDto editImageRequestDto)
        {
            return new Image()
            {
                ImageName = editImageRequestDto.ImageName,
                Url = editImageRequestDto.Url,
                IsPrimary = editImageRequestDto.IsPrimary,
                ProductId = editImageRequestDto.productId
            };
        }
    }
}