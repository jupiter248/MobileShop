using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Orders.Order
{
    public class AddOrderStatusRequestDto
    {
        [MaxLength(25, ErrorMessage = "Status name can not be over 25 characters")]
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}