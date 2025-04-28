using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.ProductAttributes
{
    public class AddProductCombinationRequestDto
    {
        public required int ProductId { get; set; }
        public required List<int> SelectedValueIds { get; set; } // List of value IDs
        public required int Quantity { get; set; }
        public decimal FinalPrice { get; set; }
    }
}