using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Orders;
using MainApi.Models.Products.ProductAttributes;
using MainApi.Models.Products.SpecificationAttributes;
using MainApi.Models.User;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Models.Products
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<WishList> WishLists { get; set; } = new List<WishList>();
        public List<Product_ProductAttribute_Mapping> Product_ProductAttribute_Mappings { get; set; } = new List<Product_ProductAttribute_Mapping>();
        public List<Product_SpecificationAttribute_Mapping> Product_SpecificationAttribute_Mappings { get; set; } = new List<Product_SpecificationAttribute_Mapping>();
        public List<ProductAttributeCombination> ProductAttributeCombination { get; set; } = new List<ProductAttributeCombination>();
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        

    }
}