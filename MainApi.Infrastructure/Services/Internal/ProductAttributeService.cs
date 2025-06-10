using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.CustomException;
using MainApi.Application.Dtos.ProductAttributes;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.ProductAttributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MainApi.Infrastructure.Services.Internal
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeRepository _productAttributeRepo;
        private readonly IProductRepository _productRepo;
        private readonly ISKUService _sKUService;
        public ProductAttributeService(IProductAttributeRepository productAttributeRepo, IProductRepository productRepo, ISKUService sKUService)
        {
            _productAttributeRepo = productAttributeRepo;
            _productRepo = productRepo;
            _sKUService = sKUService;
        }
        public async Task AddAttributeCombinationAsync(AddProductCombinationRequestDto addProductCombinationRequestDto)
        {
            Product? product = await _productRepo.GetProductByIdAsync(addProductCombinationRequestDto.ProductId) ?? throw new KeyNotFoundException("Product not found");

            List<PredefinedProductAttributeValue> selectedValues = await _productAttributeRepo.GetAttributeValuesById(addProductCombinationRequestDto.SelectedValueIds) ?? throw new ValidationException("Invalid attribute selections");

            string Sku = _sKUService.GenerateSKU(product.ProductName, selectedValues.Select(s => s.Name).ToList());

            ProductCombination combination = new ProductCombination()
            {
                ProductId = addProductCombinationRequestDto.ProductId,
                FinalPrice = addProductCombinationRequestDto.FinalPrice,
                Quantity = addProductCombinationRequestDto.Quantity,
                Product = product,
                Sku = Sku,
                CombinationAttributes = selectedValues.Select(a => new ProductCombinationAttribute
                {
                    AttributeValueId = a.Id,
                    AttributeValue = a,
                }).ToList()
            };
            await _productAttributeRepo.AddProductAttributeCombinationAsync(combination);
        }

        public async Task<PredefinedProductAttributeValueDto> AddPredefinedProductAttributeValueAsync(AddPredefinedProductAttributeValueRequestDto addPredefinedValueDto)
        {
            bool attributeModelExists = await _productAttributeRepo.PredefinedProductAttributeValueExistsByName(addPredefinedValueDto.Name);
            if (attributeModelExists == true)
            {
                throw new ConflictException("Data already exists.");
            }
            ProductAttribute productAttribute = await _productAttributeRepo.GetProductAttributeByIdAsync(addPredefinedValueDto.ProductAttributeId) ?? throw new KeyNotFoundException("ProductAttribute not found");
            PredefinedProductAttributeValue predefinedProductAttributeValue = await _productAttributeRepo.AddPredefinedProductAttributeValueAsync(addPredefinedValueDto.ToPredefinedProductAttributeValueFromAdd(productAttribute));
            return predefinedProductAttributeValue.ToPredefinedProductAttributeValueDto();
        }

        public async Task<ProductAttributeDto> AddProductAttributeAsync(AddProductAttributeRequestDto addProductAttributeRequestDto)
        {
            bool attributeModelExists = await _productAttributeRepo.ProductAttributeExistsByName(addProductAttributeRequestDto.Name);
            if (attributeModelExists == true)
            {
                throw new ConflictException("Data already exists.");
            }
            ProductAttribute productAttribute = await _productAttributeRepo.AddProductAttributeAsync(addProductAttributeRequestDto.ToProductAttributeFromAdd());
            return productAttribute.ToProductAttributeDto();
        }

        public async Task<ProductAttributeMappingDto> AssignAttributeToProductAsync(AddProductAttributeMappingRequestDto addProductAttributeMappingRequestDto)
        {
            Product? product = await _productRepo.GetProductByIdAsync(addProductAttributeMappingRequestDto.ProductId) ?? throw new KeyNotFoundException("Product not found");

            ProductAttribute? productAttribute = await _productAttributeRepo.GetProductAttributeByIdAsync(addProductAttributeMappingRequestDto.ProductAttributeId) ?? throw new KeyNotFoundException("ProductAttribute not found");

            ProductAttributeMapping? mappingModel = new ProductAttributeMapping()
            {
                IsRequired = addProductAttributeMappingRequestDto.IsRequired,
                Product = product,
                ProductAttribute = productAttribute,
                ProductAttributeId = addProductAttributeMappingRequestDto.ProductAttributeId,
                ProductId = addProductAttributeMappingRequestDto.ProductId
            };

            ProductAttributeMapping productAttributeMapping = await _productAttributeRepo.AddProductAttributeMappingAsync(mappingModel);
            return productAttributeMapping.ToProductMappingDto();
        }

        public async Task DeletePredefinedProductAttributeValue(int predefinedProductAttributeId)
        {
            PredefinedProductAttributeValue predefinedProductAttributeValue = await _productAttributeRepo.GetPredefinedAttributeValueByIdAsync(predefinedProductAttributeId) ?? throw new KeyNotFoundException("PredefinedProductAttribute not found");
            await _productAttributeRepo.DeletePredefinedProductAttributeValueAsync(predefinedProductAttributeValue);
        }

        public async Task DeleteProductAttributeAsync(int productAttributeId)
        {
            ProductAttribute? value = await _productAttributeRepo.GetProductAttributeByIdAsync(productAttributeId) ?? throw new KeyNotFoundException("ProductAttribute not found");
            await _productAttributeRepo.DeleteProductAttributeAsync(value);

        }

        public async Task DeleteProductAttributeCombination(int combinationId)
        {
            ProductCombination productCombination = await _productAttributeRepo.GetProductCombinationByIdAsync(combinationId) ?? throw new KeyNotFoundException("Combination not found");
            await _productAttributeRepo.DeleteProductAttributeCombinationAsync(productCombination);
        }

        public async Task<List<ProductAttributeMappingDto>> GetAllAssignedProductAttributeAsync(int productId)
        {
            List<ProductAttributeMapping> product_ProductAttribute_Mappings = await _productAttributeRepo.GetAllProductAttributeMappingAsync(productId);
            List<ProductAttributeMappingDto> productAttributeMappingDtos = product_ProductAttribute_Mappings.Select(m =>
            {
                return new ProductAttributeMappingDto()
                {
                    Id = m.Id,
                    IsRequired = m.IsRequired,
                    Attribute = m.ProductAttribute.ToProductAttributeDto()
                };
            }).ToList();
            return productAttributeMappingDtos;
        }

        public async Task<List<ProductCombinationDto>> GetAllAttributeCombinationsAsync(int productId)
        {
            List<ProductCombination> combinations = await _productAttributeRepo.GetAllProductAttributeCombinationAsync(productId);
            // var productCombinationAttribute = combinations.Select(c => c.CombinationAttributes.Select(a => a.AttributeValue.ProductAttribute.Name).ToList()).ToList();
            // var ex = productCombinationAttribute.Select(a => a.Except(new List<string>() { "Color" }).ToList()).ToList();

            // List<ProductCombinationDto> combinationDtos = combinations.GroupBy(c => new
            // {
            //     Storage = c.CombinationAttributes?.FirstOrDefault(a => a.AttributeValue?.ProductAttribute?.Name == "Storage")?.AttributeValue.Name,
            //     RAM = c.CombinationAttributes?.FirstOrDefault(a => a.AttributeValue?.ProductAttribute?.Name == "RAM")?.AttributeValue.Name
            // })
            // .Select(g => new ProductCombinationDto
            // {
            //     Quantity = g.Sum(v => v.Quantity),
            //     ProductId = productId,
            //     Attributes = new Dictionary<string, string>
            //     {
            //         {"Storage" , g.Key.Storage ?? string.Empty},
            //         {"RAM" , g.Key.RAM ?? string.Empty},
            //     },

            //     AvailableColors = g.Select(v => new ColorOptionDto
            //     {
            //         Name = v.CombinationAttributes.FirstOrDefault(a => a.AttributeValue.ProductAttribute.Name == "Color").AttributeValue.Name,
            //         Stock = v.Quantity,
            //         Price = v.FinalPrice,
            //         Sku = v.Sku
            //     }).ToList()
            List<ProductCombinationDto> combinationDtos = combinations.
                Select(g => new ProductCombinationDto
                {
                    Quantity = g.Quantity,
                    ProductId = productId,
                    Attributes = g.CombinationAttributes
                    .Select(a => new Dictionary<string, string>
                    {
                        { a.AttributeValue.ProductAttribute.Name, a.AttributeValue.Name }
                    }).ToList()
                }).ToList();

            // List<ProductCombinationDto> combinationDtos = combinations.Select(c => c.ToProductAttributeCombinationDto()).ToList();
            return combinationDtos;
        }

        public async Task<List<ProductAttributeDto>> GetAllProductAttributesAsync()
        {
            List<ProductAttribute> productAttribute = await _productAttributeRepo.GetAllProductAttributesAsync();
            List<ProductAttributeDto> productAttributeDtos = productAttribute.Select(a => a.ToProductAttributeDto()).ToList();
            return productAttributeDtos;
        }
    }
}