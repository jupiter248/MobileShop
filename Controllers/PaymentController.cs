using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Payment;
using MainApi.Extensions;
using MainApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> RequestPayment([FromBody] AddRequestPaymentDto requestPaymentDto)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrEmpty(username)) return BadRequest("Username is invalid");
            string authorityCode = await _paymentService.RequestPayment(username, requestPaymentDto);
            return Ok(new { authority = authorityCode });
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyPayment([FromBody] AddVerifyPaymentDto addVerifyPaymentDto)
        {
            var isSuccess = await _paymentService.VerifyPayment(addVerifyPaymentDto);
            if (isSuccess)
                return Ok(new { message = "Payment verified successfully" });
            else
                return BadRequest(new { message = "Payment verification failed" });
        }
    }
}