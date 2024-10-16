using System;
using MainApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }
    public DbSet<ProductModel> ProductModels { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductModel>().HasData(
            new ProductModel
            {
                Id = 1,
                ProductName = "Mobile",
                Brand = "Samsung",
                Model = "A15",
                Price = 15000000,
                Quantity = 8,
                Description = "test test test test"
            }
        );
    }

}
