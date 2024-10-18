using System;
using MainApi.Dtos.Products;
using MainApi.Models;

namespace MainApi.Mappers;

public static class ProductMappers
{
    public static ProductModel ToProductFromCreateDto(this CreateProductRequestDto createProductRequestDto)
    {
        return new ProductModel()
        {
            ProductName = createProductRequestDto.ProductName,
            Brand = createProductRequestDto.Brand,
            Model = createProductRequestDto.Model,
            Price = createProductRequestDto.Price,
            Quantity = createProductRequestDto.Quantity,
            Description = createProductRequestDto.Description
        };
    }
}
