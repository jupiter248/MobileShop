using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Products
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
        public Boolean IsPrimary { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}