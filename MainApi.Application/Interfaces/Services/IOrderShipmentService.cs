using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.OrderShipment;
using MainApi.Domain.Models.Orders;

namespace MainApi.Application.Interfaces.Services
{
    public interface IOrderShipmentService
    {
        Task<OrderShipmentDto> AddShipmentAsync(AddShipmentRequestDto addShipmentRequestDto);
        Task<OrderShipmentDto> GetShipmentByIdAsync(int shipmentId);
        Task<List<OrderShipmentDto>> GetAllUsersOrdersShipmentAsync(int orderId);
        Task DeleteShipmentAsync(int shipmentId);

        Task<ShippingStatusDto> AddShippingStatusAsync(AddShippingStatusRequestDto addShippingStatusRequestDto);
        Task<List<ShippingStatusDto>> GetAllShippingStatusAsync();
        Task DeleteShippingStatusAsync(int shippingStatusId);

        Task<List<OrderShipmentItemDto>> AddItemToShipmentItemAsync(List<int> orderItemIds, int shipmentId);
        Task DeleteShipmentItemAsync(int shipmentItemId);
    }
}