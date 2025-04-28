using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Products;
using MainApi.Domain.Models;

namespace MainApi.Application.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ProductDto>? ProductsDtos { get; set; }
    }
}