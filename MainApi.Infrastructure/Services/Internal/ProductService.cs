using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Address;
using MainApi.Application.Dtos.Filtering;
using MainApi.Application.Dtos.Products;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;

namespace MainApi.Infrastructure.Services.Internal
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<ProductDto> AddProductAsync(CreateProductRequestDto createProductRequestDto, int categoryId)
        {
            Product product = await _productRepo.AddProductAsync(createProductRequestDto.ToProductFromCreateDto(categoryId));
            return product.ToProductDto();
        }

        public async Task DeleteProductAsync(int productId)
        {
            Product currentProduct = await _productRepo.GetProductByIdAsync(productId) ?? throw new KeyNotFoundException("Product not found");
            await _productRepo.DeleteProductAsync(currentProduct);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync(ProductSortingDto productSortingDto, ProductFilteringDto productFilteringDto)
        {
            List<Product>? products = await _productRepo.GetAllProductsAsync(productSortingDto, productFilteringDto) ?? throw new KeyNotFoundException("Products not found");
            List<ProductDto>? productsDto = products.Select(p => p.ToProductDto()).ToList();
            return productsDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            Product product = await _productRepo.GetProductByIdAsync(productId) ?? throw new KeyNotFoundException("Product not found");
            return product.ToProductDto();
        }

        public async Task UpdateProductAsync(int productId, UpdateProductRequestDto updateProductRequestDto, int categoryId)
        {
            Product currentProduct = await _productRepo.GetProductByIdAsync(productId) ?? throw new KeyNotFoundException("Product not found");
            Product newProduct = updateProductRequestDto.ToProductFromUpdateDto(categoryId);
            await _productRepo.UpdateProductAsync(currentProduct, newProduct);
        }
    }
}