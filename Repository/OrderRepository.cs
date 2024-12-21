using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models;

namespace MainApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Order?> AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> RemoveOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> UpdateOrderAsync(Order order, int orderId)
        {
            throw new NotImplementedException();
        }
    }
}