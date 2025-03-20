using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Orders
{
    public class OrderDiscount
    {
        public int Id { get; set; }
        public required int OrderId { get; set; }
        public Order? Order { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}