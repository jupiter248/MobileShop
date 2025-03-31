using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.User;

namespace MainApi.Models.Payments
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public required int OrderId { get; set; }
        public required string UserId { get; set; }
        public required AppUser AppUser { get; set; }
        public required decimal Amount { get; set; }
        public required int StatusId { get; set; }
        public required PaymentStatus PaymentStatus { get; set; }
        public required string AuthorityCode { get; set; }
        public string? TransactionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}