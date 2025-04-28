using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Payment
{
    public class AddVerifyPaymentDto
    {
        public required decimal Amount { get; set; }
        public required string Authority { get; set; }
        

    }
}