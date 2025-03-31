using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Payment;

namespace MainApi.Interfaces
{
    public interface IPaymentService
    {
        Task<string> RequestPayment(string userName, AddRequestPaymentDto requestPaymentDto);
        Task<bool> VerifyPayment(AddVerifyPaymentDto addVerifyPaymentDto);
    }
}