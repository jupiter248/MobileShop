using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Payment;
using MainApi.Domain.Models.Payments;

namespace MainApi.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<string> RequestPaymentAsync(string userName, AddRequestPaymentDto requestPaymentDto);
        Task<bool> VerifyPaymentAsync(AddVerifyPaymentDto addVerifyPaymentDto);
        Task AddPaymentStatusAsync(string statusName, string description);
        Task<IEnumerable<object>> GetAllPaymentStatusesAsync();
        Task DeletePaymentStatusesAsync(string statusName);
    }
}