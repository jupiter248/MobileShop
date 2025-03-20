using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Orders
{
    public class OrderItemAttribute
    {
        [Key]
        public int Id { get; set; }
        public required int OrderItemId { get; set; }
        public required int ProductAttributeId { get; set; }
        public required string Value { get; set; }
    }
}