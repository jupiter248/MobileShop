using System;
using MainApi.Data;
using MainApi.Dtos.Filtering;
using MainApi.Interfaces;
using MainApi.Models;
using MainApi.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository;

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
        var products = _context.Products.Include(s => s.Product_SpecificationAttribute_Mappings).ThenInclude(o => o.SpecificationAttributeOption).Include(i => i.Images).Include(c => c.Comments).ThenInclude(u => u.AppUser).Include(p => p.Category).AsQueryable();

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
            products = products
                .Where(p => p.Product_SpecificationAttribute_Mappings
                .Any(mapping => mapping.SpecificationAttributeOption.Name.ToLower().Replace(" ", "") == filteringDto.BatteryCapacity.ToLower().Replace(" ", "")));
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

        return await products.Skip(skipNumber).Take(filteringDto.PageSize).ToListAsync();
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
