using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Orders;

namespace MainApi.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync(string username);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<OrderStatus> AddOrderStatusAsync(OrderStatus orderStatus);
        Task<OrderStatus?> GetOrderStatusByNameAsync(string statusName);
        Task<List<OrderStatus>> GetAllOrderStatusesAsync();
        Task<Order> AddOrderAsync(Order order);
        Task<List<OrderItem>> GetOrderItemsByIdAsync(List<int> Ids);
    }
}