using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Persistence.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            foreach (var item in order.OrderItems)
            {
                await _context.OrderItems.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<OrderStatus> AddOrderStatusAsync(OrderStatus orderStatus)
        {
            await _context.OrderStatuses.AddAsync(orderStatus);
            await _context.SaveChangesAsync();
            return orderStatus;
        }

        public async Task<List<Order>> GetAllOrdersAsync(string username)
        {
            List<Order>? orders = await _context.Orders
            .Include(i => i.OrderItems).Include(u => u.User).Include(s => s.OrderStatus).Include(o => o.Address)
            .Where(o => o.User.UserName == username)
            .ToListAsync();
            return orders;
        }

        public async Task<List<OrderStatus>> GetAllOrderStatusesAsync()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            Order? order = await _context.Orders.Include(o => o.Address).Include(i => i.OrderItems).Include(u => u.User).Include(s => s.OrderStatus).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return null;
            }
            return order;
        }

        public async Task<List<OrderItem>> GetOrderItemsByIdAsync(List<int> Ids)
        {
            return await _context.OrderItems.Where(i => Ids.Contains(i.Id)).ToListAsync();
        }

        public async Task<OrderStatus?> GetOrderStatusByNameAsync(string statusName)
        {
            OrderStatus? orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(s => s.StatusName.ToLower() == statusName.ToLower());
            if (orderStatus == null) return null;
            return orderStatus;
        }
    }
}