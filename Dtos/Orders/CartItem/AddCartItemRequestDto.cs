using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Orders.CartItem
{
    public class AddCartItemRequestDto
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public required List<int> AttributeIds { get; set; }
        public required decimal Price { get; set; }
    }
}