using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using MainApi.Dtos.Account;

namespace MainApi.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public UserDto? User { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public int StatusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}