using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Payment
{
    public class AddRequestPaymentDto
    {
        public required int OrderId { get; set;}
        public required decimal Amount { get; set; }
        public required string Description { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Mobile { get; set; }
    }
}