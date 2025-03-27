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
        Task<OrderStatus?> AddOrderStatusAsync(OrderStatus orderStatus);
        Task<OrderStatus?> GetOrderStatusByNameAsync(string statusName);
        Task<Order> AddOrderAsync(Order order);
        Task<Order?> RemoveOrderAsync(int orderId);
        Task<Order?> RemoveOrderItemsAsync(int orderItemId, int orderId);
    }
}