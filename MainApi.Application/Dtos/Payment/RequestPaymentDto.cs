using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Payment
{
    public class RequestPaymentDto
    {
        public required string Merchant_id { get; set; }
        public required string Callback_url { get; set; }
        public required decimal Amount { get; set; }
        public required string Description { get; set; }
        public object? MetaData { get; set; }
    }
}