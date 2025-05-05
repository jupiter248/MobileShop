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
        private readonly IPaymentRepository _paymentRepo;

        public PaymentController(IPaymentService paymentService, IPaymentRepository paymentRepo)
        {
            _paymentService = paymentService;
            _paymentService = paymentService;
            _paymentRepo = paymentRepo;

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
        [HttpPost("status")]
        public async Task<IActionResult> AddPaymentStatus([FromQuery] string name, [FromQuery] string description)
        {

            PaymentStatus paymentStatus = new PaymentStatus()
            {
                Description = description,
                Name = name
            };
            PaymentStatus? paymentModel = await _paymentRepo.CreatePaymentStatusAsync(paymentStatus);
            if (paymentModel == null)
            {
                return BadRequest("This status already made");
            }
            return Ok(paymentStatus);
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetAllPaymentStatuses()
        {
            List<PaymentStatus> paymentStatuses = await _paymentRepo.GetAllPaymentStatusesAsync();
            var PaymentStatusDto = paymentStatuses.Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            });
            return Ok(paymentStatuses);
        }
        [HttpDelete("status{id:int}")]
        public async Task<IActionResult> DeletePaymentStatuses([FromRoute] int id)
        {
            PaymentStatus? paymentStatuses = await _paymentRepo.DeletePaymentStatusAsync(id);
            if (paymentStatuses == null) return NotFound("Status not found");

            return NoContent();
        }
    }
}