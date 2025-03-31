using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Payment
{
    public class VerifyPaymentDto
    {
        public required string Merchant_id { get; set; }
        public required decimal Amount { get; set; }
        public required string Authority { get; set; }
    }
}