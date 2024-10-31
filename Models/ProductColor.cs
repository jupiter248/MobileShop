using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models
{
    public class ProductColor
    {
        public required int ProductId { get; set; }
        public required int ColorId { get; set; }
        public Product? Product { get; set; }
        public Color? Color { get; set; }
    }
}