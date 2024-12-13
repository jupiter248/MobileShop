using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Image;
using MainApi.Models;

namespace MainApi.Mappers
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
                Url = addImageRequestDto.Url,
                IsPrimary = addImageRequestDto.IsPrimary,
                ProductId = productId
            };
        }
        public static Image ToImageFromEdit(this EditImageRequestDto editImageRequestDto, int productId)
        {
            return new Image()
            {
                ImageName = editImageRequestDto.ImageName,
                Url = editImageRequestDto.Url,
                IsPrimary = editImageRequestDto.IsPrimary,
                ProductId = productId
            };
        }
    }
}