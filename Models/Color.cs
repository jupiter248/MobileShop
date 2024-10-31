using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string ColorName { get; set; } = string.Empty;
        public string ColorCode { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public List<ProductColor> ProductsColors { get; set; } = new List<ProductColor>();
    }
}