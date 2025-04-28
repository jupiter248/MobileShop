using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Orders
{
    public class OrderDiscount
    {
        [Key]
        public int Id { get; set; }
        public required int OrderId { get; set; }
        public Order? Order { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}