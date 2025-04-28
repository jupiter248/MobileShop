using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.User;

namespace MainApi.Domain.Models.Products
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedTime = DateTime.Now;
    }
}