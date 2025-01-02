using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            foreach (var item in order.OrderItems)
            {
                await _context.OrderItems.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>?> GetAllOrdersAsync()
        {
            List<Order>? orders = await _context.Orders.Include(i => i.OrderItems).ToListAsync();
            return orders;
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