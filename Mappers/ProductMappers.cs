using System;
using MainApi.Dtos.Products;
using MainApi.Models;

namespace MainApi.Mappers;

public static class ProductMappers
{
    public static Product ToProductFromCreateDto(this CreateProductRequestDto createProductRequestDto)
    {
        return new Product()
        {
            ProductName = createProductRequestDto.ProductName,
            Brand = createProductRequestDto.Brand,
            Model = createProductRequestDto.Model,
            Price = createProductRequestDto.Price,
            Quantity = createProductRequestDto.Quantity,
            Description = createProductRequestDto.Description
        };
    }
    public static Product ToProductFromUpdateDto(this UpdateProductRequestDto updateProductRequestDto)
    {
        return new Product()
        {
            ProductName = updateProductRequestDto.ProductName,
            Brand = updateProductRequestDto.Brand,
            Model = updateProductRequestDto.Model,
            Price = updateProductRequestDto.Price,
            Quantity = updateProductRequestDto.Quantity,
            Description = updateProductRequestDto.Description
        };
    }
    public static ProductDto ToProductDto(this Product productModel)
    {
        return new ProductDto()
        {
            Id = productModel.Id,
            ProductName = productModel.ProductName,
            Brand = productModel.Brand,
            Model = productModel.Model,
            Price = productModel.Price,
            Quantity = productModel.Quantity,
            Description = productModel.Description,
            ImagesDto = productModel.Images.Select(s => s.ToImageDto()).ToList()
        };
    }
}
