using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Payment;

namespace MainApi.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> RequestPayment(string userName, AddRequestPaymentDto requestPaymentDto);
        Task<bool> VerifyPayment(AddVerifyPaymentDto addVerifyPaymentDto);
    }
}