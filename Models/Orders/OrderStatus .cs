using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string? StatusName { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}