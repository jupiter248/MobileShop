using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Orders.OrderItem
{
    public class AddOrderItemRequestDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}