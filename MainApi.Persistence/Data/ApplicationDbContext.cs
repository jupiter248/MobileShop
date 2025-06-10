using System;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Orders;
using MainApi.Domain.Models.Payments;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.ProductAttributes;
using MainApi.Domain.Models.Products.SpecificationAttributes;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Persistence.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
      : base(dbContextOptions)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OrderDiscount> OrderDiscounts { get; set; }
    public DbSet<OrderShipment> OrderShipments { get; set; }
    public DbSet<ShipmentItem> ShipmentItems { get; set; }
    public DbSet<ShippingStatus> ShippingStatuses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<WishList> WishLists { get; set; }
    public DbSet<PredefinedProductAttributeValue> PredefinedProductAttributeValues { get; set; }
    public DbSet<ProductAttributeMapping> ProductAttributeMappings { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
    public DbSet<ProductCombination> ProductCombinations { get; set; }
    public DbSet<ProductCombinationAttribute> ProductCombinationsAttribute { get; set; }
    public DbSet<Product_SpecificationAttribute_Mapping> SpecificationAttributeMappings { get; set; }
    public DbSet<SpecificationAttribute> SpecificationAttributes { get; set; }
    public DbSet<SpecificationAttributeOption> SpecificationAttributeOptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentStatus> PaymentStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .HasMany(c => c.Comments)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasMany(i => i.Images)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId)
            .IsRequired();

        modelBuilder.Entity<AppUser>()
            .HasMany(c => c.Comments)
            .WithOne(u => u.AppUser)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        modelBuilder.Entity<AppUser>()
            .HasMany(a => a.Addresses)
            .WithOne(u => u.appUser)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        modelBuilder.Entity<WishList>(x => x.HasKey(p => new { p.UserId, p.ProductId }));
        modelBuilder.Entity<WishList>()
            .HasOne(u => u.AppUser)
            .WithMany(f => f.WishLists)
            .HasForeignKey(u => u.UserId);
        modelBuilder.Entity<WishList>()
            .HasOne(p => p.Product)
            .WithMany(f => f.WishLists)
            .HasForeignKey(p => p.ProductId);



        // List<IdentityRole> roles = new List<IdentityRole>
        // {
        //     new IdentityRole
        //     {
        //         Name = "Admin",
        //         NormalizedName = "ADMIN"
        //     },
        //     new IdentityRole
        //     {
        //         Name = "User",
        //         NormalizedName = "USER"
        //     }
        // };


        // modelBuilder.Entity<IdentityRole>().HasData(roles);
    }

}
