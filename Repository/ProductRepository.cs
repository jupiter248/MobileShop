using System;
using MainApi.Data;
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

    public async Task<List<Product>?> GetAllProductsAsync()
    {
        var products = await _context.Products.Include(i => i.Images).Include(c => c.Category).Include(c => c.Comments).ThenInclude(u => u.AppUser).ToListAsync();
        return products;
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
