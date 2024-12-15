using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Products;
using MainApi.Models;

namespace MainApi.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ProductDto> Products { get; set; }
    }
}