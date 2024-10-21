using System;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models;
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

    public async Task<ProductModel> AddProductAsync(ProductModel productModel)
    {
        await _context.AddAsync(productModel);
        await _context.SaveChangesAsync();
        return productModel;
    }

    public async Task<List<ProductModel>> GetAllProductsAsync()
    {
        var products = await _context.ProductModels.ToListAsync();
        if (products == null)
            return null;
        return products;
    }

    public async Task<ProductModel?> GetProductByIdAsync(int productId)
    {
        return await _context.ProductModels.FindAsync(productId);
    }

    public async Task<ProductModel?> RemoveProductAsync(int productId)
    {
        var product = await GetProductByIdAsync(productId);
        _context.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<ProductModel> UpdateProductAsync(ProductModel productModel, int productId)
    {
        var product = _context.ProductModels.FirstOrDefault(f => f.Id == productId);
        if (product == null)
        {
            return null;
        }
        product.ProductName = productModel.ProductName;
        product.Brand = productModel.Brand;
        product.Model = productModel.Model;
        product.Price = productModel.Price;
        product.Quantity = productModel.Quantity;
        product.Description = productModel.Description;
        product.ImageURL = productModel.ImageURL;
        await _context.SaveChangesAsync();
        return product;
    }
}
