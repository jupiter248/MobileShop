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
    public DbSet<Product> Products { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductColor>(x => x.HasKey(p => new { p.ColorId, p.ProductId }));

        modelBuilder.Entity<ProductColor>()
            .HasOne(u => u.Product)
            .WithMany(u => u.ProductColors)
            .HasForeignKey(u => u.ProductId);

        modelBuilder.Entity<ProductColor>()
            .HasOne(u => u.Color)
            .WithMany(u => u.ProductsColors)
            .HasForeignKey(u => u.ColorId);


        modelBuilder.Entity<Product>().HasData(
            new Product
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
