using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Payments;

namespace MainApi.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment> GetPaymentByAuthorityAsync(string authority);
        Task UpdatePaymentStatusAsync(string authority, string status, string transactionId);

        // Payment Status
        Task<PaymentStatus> CreatePaymentStatusAsync(PaymentStatus paymentStatus);
        Task<List<PaymentStatus>> GetAllPaymentStatusesAsync();
        Task<PaymentStatus> GetPaymentStatusByNameAsync(string statusName);
        Task<PaymentStatus> DeletePaymentStatusAsync(int id);


    }
}