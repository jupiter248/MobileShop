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

    public async Task<Products?> AddProductAsync(Products products)
    {
        await _context.AddAsync(products);
        await _context.SaveChangesAsync();
        return products;
    }

    public async Task<List<Products>?> GetAllProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        if (products == null)
            return null;
        return products;
    }

    public async Task<Products?> GetProductByIdAsync(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public async Task<Products?> RemoveProductAsync(int productId)
    {
        var product = await GetProductByIdAsync(productId);
        _context.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Products?> UpdateProductAsync(Products productModel, int productId)
    {
        var product = _context.Products.FirstOrDefault(f => f.Id == productId);
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
