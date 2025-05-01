using System;
using System.Text.RegularExpressions;
using MainApi.Persistence.Data;
using MainApi.Application.Dtos.Filtering;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using MainApi.Application.Interfaces.Repositories;

namespace MainApi.Persistence.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> AddProductAsync(Product products)
    {
        await _context.AddAsync(products);
        await _context.SaveChangesAsync();
        return products;
    }

    public async Task<List<Product>?> GetAllProductsAsync(ProductSortingDto sortingDto, ProductFilteringDto filteringDto)
    {
        var products = _context.Products.Include(a => a.ProductCombination).ThenInclude(a => a.CombinationAttributes).ThenInclude(a => a.AttributeValue).ThenInclude(a => a.ProductAttribute).Include(s => s.Product_SpecificationAttribute_Mappings).ThenInclude(o => o.SpecificationAttributeOption).Include(i => i.Images).Include(c => c.Comments).ThenInclude(u => u.AppUser).Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filteringDto.Name))
        {
            products = products.Where(p => p.ProductName.ToLower().Contains(filteringDto.Name.ToLower().Replace(" ", "")));
        }

        if (!string.IsNullOrWhiteSpace(filteringDto.Category))
        {
            products = products.Where(p => p.Category.CategoryName.ToLower().Contains(filteringDto.Category.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(filteringDto.Brand))
        {
            products = products.Where(p => p.Brand.ToLower().Contains(filteringDto.Brand.ToLower()));
        }

        if (filteringDto.MinPrice.HasValue)
        {
            products = products.Where(p => p.Price >= filteringDto.MinPrice.Value);
        }

        if (filteringDto.MaxPrice.HasValue)
        {
            products = products.Where(p => p.Price <= filteringDto.MaxPrice.Value);
        }


        if (!string.IsNullOrWhiteSpace(filteringDto.BatteryCapacity))
        {
            var matches = Regex.Matches(filteringDto.BatteryCapacity, @"\d+");
            if (matches.Count == 2)
            {
                int min = int.Parse(matches[0].Value);
                int max = int.Parse(matches[1].Value);
                products = products
                    .AsEnumerable()
                    .Where(p => p.Product_SpecificationAttribute_Mappings
                        .Any(mapping =>
                        {
                            var match = Regex.Match(mapping.SpecificationAttributeOption.Name ?? "", @"\d+");
                            if (match.Success && int.TryParse(match.Value, out int value))
                            {
                                return value >= min && value <= max;
                            }
                            return false;
                        }))
                    .AsQueryable();
            }

        }
        if (!string.IsNullOrWhiteSpace(filteringDto.StorageCapacity))
        {
            var matches = Regex.Matches(filteringDto.StorageCapacity, @"\d+");
            if (matches.Count == 2)
            {
                int min = int.Parse(matches[0].Value);
                int max = int.Parse(matches[1].Value);
                products = products
                .AsEnumerable()
                .Where(c => c.ProductCombination
                .Any(c => c.CombinationAttributes
                    .Any(combination =>
                    {
                        if (combination.AttributeValue.ProductAttribute.Name.ToLower().Contains("storage"))
                        {
                            var match = Regex.Match(combination.AttributeValue.Name ?? "", @"\d+");
                            if (match.Success)
                            {
                                return int.Parse(match.Value) >= min && int.Parse(match.Value) <= max;
                            }
                            return false;
                        }
                        return false;
                    }
                )))
                .AsQueryable();
            }
        }
        if (!string.IsNullOrWhiteSpace(filteringDto.RAMSize))
        {
            var matches = Regex.Matches(filteringDto.RAMSize, @"\d+");
            if (matches.Count == 2)
            {
                int min = int.Parse(matches[0].Value);
                int max = int.Parse(matches[1].Value);
                products = products
                .AsEnumerable()
                .Where(c => c.ProductCombination
                .Any(c => c.CombinationAttributes
                    .Any(combination =>
                    {
                        if (combination.AttributeValue.ProductAttribute.Name.ToLower().Contains("ram"))
                        {
                            var match = Regex.Match(combination.AttributeValue.Name ?? "", @"\d+");
                            if (match.Success)
                            {
                                return int.Parse(match.Value) >= min && int.Parse(match.Value) <= max;
                            }
                            return false;
                        }
                        return false;
                    }
                )))
                .AsQueryable();
            }
        }

        if (sortingDto.NewestArrivals)
        {
            products = products.OrderByDescending(p => p.Model);
        }

        if (sortingDto.HighestPrice)
        {
            products = products.OrderByDescending(p => p.Price);
        }

        if (sortingDto.LowestPrice)
        {
            products = products.OrderBy(p => p.Price);
        }


        var skipNumber = (filteringDto.PageNumber - 1) * filteringDto.PageSize;

        return products.Skip(skipNumber).Take(filteringDto.PageSize).ToList();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products.Include(i => i.Images).Include(c => c.Category).FirstAsync(p => p.Id == productId);
    }

    public async Task<bool> ProductExistsAsync(int productId)
    {
        Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
        {
            return false;
        }
        return true;
    }

    public async Task<Product?> RemoveProductAsync(int productId)
    {
        var product = await GetProductByIdAsync(productId);
        if (product != null)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }
        return product;
    }

    public async Task<Product?> UpdateProductAsync(Product productModel, int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(f => f.Id == productId);
        if (product != null)
        {
            product.ProductName = productModel.ProductName;
            product.Brand = productModel.Brand;
            product.Model = productModel.Model;
            product.Price = productModel.Price;
            product.Quantity = productModel.Quantity;
            product.Description = productModel.Description;
            product.CategoryId = productModel.CategoryId;
            await _context.SaveChangesAsync();
        }
        return product;
    }
}
