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
            List<Order>? orders = await _context.Orders.Include(i => i.OrderItems).Include(u => u.User).Include(s => s.OrderStatus).ToListAsync();
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

        public async Task<OrderStatus?> GetOrderStatusByIdAsync(int orderStatusId)
        {
            OrderStatus? orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(s => s.Id == orderStatusId);
            if (orderStatus != null) return orderStatus;
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

        public async Task<Order?> RemoveOrderItemsAsync(int orderId, int orderItemId)
        {
            Order? order = await _context.Orders.Include(i => i.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                OrderItem? orderItem = order.OrderItems.FirstOrDefault(i => i.Id == orderItemId);
                if (orderItem != null)
                {
                    if (orderItem.Quantity > 1)
                    {
                        orderItem.Quantity -= 1;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        order.OrderItems.RemoveAll(i => i.Id == orderItem.Id);
                        await _context.SaveChangesAsync();
                    }
                    return order;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<Order?> UpdateOrderItemAsync(OrderItem orderItem, int orderId)
        {
            Order? order = await _context.Orders.Include(i => i.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.OrderItems.Add(orderItem);
                await _context.SaveChangesAsync();
                return order;
            }
            else
            {
                return null;
            }
        }

        public async Task<Order?> UpdateOrderStatusAsync(int orderId, int statusId)
        {
            Order? order = await _context.Orders.Include(i => i.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.StatusId = statusId;
                if (statusId == 2)
                {
                    foreach (var item in order.OrderItems)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(p => item.ProductId == p.Id);
                        if (product != null)
                        {
                            product.Quantity -= item.Quantity;
                            if (product.Quantity < 0)
                            {
                                return null;
                            }
                            else
                            {
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return order;
        }
    }
}