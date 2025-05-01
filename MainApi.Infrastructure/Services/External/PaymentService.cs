using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Payment;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Domain.Models.Payments;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace MainApi.Infrastructure.Services.External
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly string _merchantId;
        private readonly string _callbackUrl;

        public PaymentService(HttpClient httpClient, IPaymentRepository paymentRepo, IOrderRepository orderRepo, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _orderRepo = orderRepo;
            _paymentRepo = paymentRepo;
            _userManager = userManager;
            _merchantId = configuration["Zarinpal:MerchantId"] ?? string.Empty;
            _callbackUrl = configuration["Zarinpal:CallbackUrl"] ?? string.Empty;
        }

        public async Task<string> RequestPayment(string username, AddRequestPaymentDto AddPaymentDto)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) throw new Exception("User not found");

            string requestUrl = "https://api.zarinpal.com/pg/v4/payment/request.json";

            RequestPaymentDto requestPaymentDto = new RequestPaymentDto()
            {
                Merchant_id = _merchantId,
                Callback_url = _callbackUrl,
                Amount = AddPaymentDto.Amount * 10,
                Description = AddPaymentDto.Description,
                MetaData = new { AddPaymentDto.Email, AddPaymentDto.Mobile }
            };
            var response = await _httpClient.PostAsJsonAsync(requestUrl, requestPaymentDto);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ZarinpalResponseDto>(jsonResponse);

            if (result?.data?.Authority == null) throw new Exception("Authority not found");

            PaymentStatus? paymentStatus = await _paymentRepo.GetPaymentStatusByNameAsync("Pending");
            if (paymentStatus == null) throw new Exception("Status not found");

            Payment payment = new Payment()
            {
                Amount = AddPaymentDto.Amount,
                AppUser = appUser,
                AuthorityCode = result.data.Authority,
                OrderId = AddPaymentDto.OrderId,
                PaymentStatus = paymentStatus,
                StatusId = paymentStatus.Id,
                UserId = appUser.Id,
            };
            await _paymentRepo.CreatePaymentAsync(payment);

            return result.data.Authority;
        }

        public async Task<bool> VerifyPayment(AddVerifyPaymentDto addVerifyPaymentDto)
        {
            Payment? payment = await _paymentRepo.GetPaymentByAuthorityAsync(addVerifyPaymentDto.Authority);
            if (payment == null) return false;

            string requestUrl = "https://api.zarinpal.com/pg/v4/payment/request.json";


            VerifyPaymentDto verifyPaymentDto = new VerifyPaymentDto()
            {
                Amount = addVerifyPaymentDto.Amount,
                Authority = addVerifyPaymentDto.Authority,
                Merchant_id = _merchantId
            };


            var response = await _httpClient.PostAsJsonAsync(requestUrl, verifyPaymentDto);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ZarinpalResponseDto>(jsonResponse);



            if (result?.data?.Code == 100) // 100 means payment is successful
            {
                PaymentStatus? paymentStatus = await _paymentRepo.GetPaymentStatusByNameAsync("Success");
                await _paymentRepo.UpdatePaymentStatusAsync(addVerifyPaymentDto.Authority, paymentStatus, result?.data?.Transaction_id);

                return true;
            }
            else
            {
                // Payment failed, update status
                PaymentStatus? paymentStatus = await _paymentRepo.GetPaymentStatusByNameAsync("Failed");
                await _paymentRepo.UpdatePaymentStatusAsync(addVerifyPaymentDto.Authority, paymentStatus, null);
                return false;
            }
        }
    }
}