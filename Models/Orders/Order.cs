using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using MainApi.Dtos.Account;
using MainApi.Models.Payments;
using MainApi.Models.User;

namespace MainApi.Models.Orders
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public required int StatusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public required int AddressId { get; set; }
        public Address? Address { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public List<OrderDiscount> OrderDiscounts { get; set; } = new List<OrderDiscount>();
        public List<OrderShipment> OrderShipment { get; set; } = new List<OrderShipment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}