using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Payments;

namespace MainApi.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        //Payment


        public Task<Payment> CreatePaymentAsync(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetPaymentByAuthorityAsync(string authority)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePaymentStatusAsync(string authority, string status, string transactionId)
        {
            throw new NotImplementedException();
        }


        //Payment Status


        public Task<PaymentStatus> CreatePaymentStatusAsync(PaymentStatus paymentStatus)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentStatus> DeletePaymentStatusAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PaymentStatus>> GetAllPaymentStatusesAsync()
        {
            throw new NotImplementedException();
        }


        public Task<PaymentStatus> GetPaymentStatusByNameAsync(string statusName)
        {
            throw new NotImplementedException();
        }
    }
}