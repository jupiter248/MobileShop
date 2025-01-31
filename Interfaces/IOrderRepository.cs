using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models;

namespace MainApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>?> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<Order?> AddOrderAsync(Order order);
        Task<Order?> UpdateOrderStatusAsync(int orderId, int statusId);
        Task<Order?> UpdateOrderItemAsync(OrderItem orderItem, int orderId);
        Task<Order?> RemoveOrderAsync(int orderId);
        Task<Order?> RemoveOrderItemsAsync(int orderItemId, int orderId);
    }
}