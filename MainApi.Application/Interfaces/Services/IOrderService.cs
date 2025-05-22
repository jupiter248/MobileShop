using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.Order;

namespace MainApi.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllUsersOrdersAsync(string username);
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task<OrderDto> AddOrderAsync(AddOrderRequestDto addOrderRequestDto, string username);
        Task<List<OrderStatusesDto>> GetAllOrderStatusesAsync();
        Task<OrderStatusesDto> AddOrderStatusAsync(AddOrderStatusRequestDto addOrderStatusRequestDto);
    }
}