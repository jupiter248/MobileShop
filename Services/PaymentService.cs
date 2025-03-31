using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Payment;
using MainApi.Interfaces;

namespace MainApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IOrderRepository _orderRepo;
        public PaymentService(HttpClient httpClient, IPaymentRepository paymentRepo, IOrderRepository orderRepo)
        {
            _httpClient = httpClient;
            _orderRepo = orderRepo;
            _paymentRepo = paymentRepo;
        }

        public Task<string> RequestPayment(string username, AddRequestPaymentDto requestPaymentDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyPayment(AddVerifyPaymentDto addVerifyPaymentDto)
        {
            throw new NotImplementedException();
        }
    }
}