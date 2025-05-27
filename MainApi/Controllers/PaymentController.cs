using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Payment;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Domain.Models.Payments;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
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
            string authorityCode = await _paymentService.RequestPaymentAsync(username, requestPaymentDto);
            return Ok(new { authority = authorityCode });
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyPayment([FromBody] AddVerifyPaymentDto addVerifyPaymentDto)
        {
            var isSuccess = await _paymentService.VerifyPaymentAsync(addVerifyPaymentDto);
            if (isSuccess)
                return Ok(new { message = "Payment verified successfully" });
            else
                return BadRequest(new { message = "Payment verification failed" });
        }
        [HttpPost("status")]
        public async Task<IActionResult> AddPaymentStatus([FromQuery] string name, [FromQuery] string description)
        {

            await _paymentService.AddPaymentStatusAsync(name, description);
            return Created();
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetAllPaymentStatuses()
        {
            IEnumerable<object> paymentStatuses = await _paymentService.GetAllPaymentStatusesAsync();
            return Ok(paymentStatuses);
        }
        [HttpDelete("status")]
        public async Task<IActionResult> DeletePaymentStatuses([FromBody] string statusName)
        {
            await _paymentService.DeletePaymentStatusesAsync(statusName);

            return NoContent();
        }
    }
}