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
        Task<Order?> GetOrderByIdAsync(int productId);
        Task<Order?> AddOrderAsync(Product product);
        Task<Order?> RemoveOrderAsync(int productId);
        Task<Order?> UpdateOrderAsync(Product product, int productId);
    }
}