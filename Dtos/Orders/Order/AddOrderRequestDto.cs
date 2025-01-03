using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders.OrderItem;

namespace MainApi.Dtos.Orders.Order
{
    public class AddOrderRequestDto
    {
        public int StatusId { get; set; }
        public List<AddOrderItemRequestDto> OrderItems { get; set; }
    }
}