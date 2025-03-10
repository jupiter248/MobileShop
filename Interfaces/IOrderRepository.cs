using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models;
using MainApi.Models.Orders;

namespace MainApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>?> GetAllOrdersAsync(string username);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<OrderStatus?> GetOrderStatusByIdAsync(int orderStatusId);
        Task<Order?> AddOrderAsync(Order order);
        Task<Order?> UpdateOrderStatusAsync(int orderId, int statusId);
        Task<Order?> UpdateOrderItemAsync(OrderItem orderItem, int orderId);
        Task<Order?> RemoveOrderAsync(int orderId);
        Task<Order?> RemoveOrderItemsAsync(int orderItemId, int orderId);
    }
}