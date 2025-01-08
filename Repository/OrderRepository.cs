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

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            Order? order = await _context.Orders.Include(i => i.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                return order;
            }
            return null;
        }

        public async Task<Order?> RemoveOrderAsync(int orderId)
        {
            Order? order = await _context.Orders.Include(i => i.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                foreach (var item in order.OrderItems)
                {
                    _context.OrderItems.Remove(item);
                }
                await _context.SaveChangesAsync();
            }
            return order;
        }

        public Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem, int orderItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order?> UpdateOrderStatusAsync(int orderId, int statusId)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.StatusId = statusId;
                await _context.SaveChangesAsync();
            }
            return order;
        }
    }
}