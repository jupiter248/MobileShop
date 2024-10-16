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

    public async Task<List<ProductModel>> GetAllProductsAsync()
    {
        var products = await _context.ProductModels.ToListAsync();
        return products;
    }
}
