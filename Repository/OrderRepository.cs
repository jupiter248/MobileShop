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
        public Task<Order?> AddOrderAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetOrderByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> RemoveOrderAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> UpdateOrderAsync(Product product, int productId)
        {
            throw new NotImplementedException();
        }
    }
}