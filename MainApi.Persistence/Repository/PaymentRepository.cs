using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Payments;
using Microsoft.EntityFrameworkCore;
using MainApi.Application.Interfaces.Repositories;

namespace MainApi.Persistence.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        //Payment


        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> GetPaymentByAuthorityAsync(string authority)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.AuthorityCode == authority);
        }

        public async Task UpdatePaymentStatusAsync(string authority, PaymentStatus status, string transactionId)
        {
            Payment? payment = await _context.Payments.FirstOrDefaultAsync(p => p.AuthorityCode == authority);
            if (payment == null) return;

            payment.PaymentStatus = status;
            payment.StatusId = status.Id;
            payment.TransactionId = transactionId;
            await _context.SaveChangesAsync();
        }


        //Payment Status


        public async Task<PaymentStatus?> CreatePaymentStatusAsync(PaymentStatus paymentStatus)
        {
            PaymentStatus? status = await _context.PaymentStatuses.FirstOrDefaultAsync(s => s.Name.ToLower() == paymentStatus.Name.ToLower());
            if (status != null)
            {
                return null;
            }
            await _context.AddAsync(paymentStatus);
            await _context.SaveChangesAsync();
            return paymentStatus;
        }

        public async Task<PaymentStatus?> DeletePaymentStatusAsync(int id)
        {
            PaymentStatus? paymentStatus = await _context.PaymentStatuses.FindAsync(id);
            if (paymentStatus != null)
            {
                _context.PaymentStatuses.Remove(paymentStatus);
                await _context.SaveChangesAsync();
            }
            return paymentStatus;
        }

        public async Task<List<PaymentStatus>> GetAllPaymentStatusesAsync()
        {
            return await _context.PaymentStatuses.ToListAsync();

        }


        public async Task<PaymentStatus?> GetPaymentStatusByNameAsync(string statusName)
        {
            return await _context.PaymentStatuses.FirstOrDefaultAsync(s => s.Name.ToLower() == statusName.ToLower());
        }
    }
}