using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders;

namespace MainApi.Dtos.Orders.Order
{
    public class AddOrderRequestDto
    {
        public required int AddressId { get; set; }
        public required List<int> CartItemsIds { get; set; }
    }
}