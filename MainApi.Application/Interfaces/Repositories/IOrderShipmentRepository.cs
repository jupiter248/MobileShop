using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Orders;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface IOrderShipmentRepository
    {
        //Shipment 
        Task<List<OrderShipment>> GetAllOrderShipmentAsync(int orderId);
        Task<OrderShipment?> GetShipmentByIdAsync(int shipmentId);
        Task<OrderShipment> AddOrderShipmentAsync(OrderShipment orderShipment);
        Task DeleteShipmentAsync(OrderShipment orderShipment);

        //Shipment Item
        Task<List<ShipmentItem>> AddItemToShipmentAsync(List<ShipmentItem> shipmentItems);
        Task<ShipmentItem?> GetShipmentItemByIdAsync(int shipmentItemId);
        Task DeleteFromShipmentAsync(ShipmentItem shipmentItem);

        //Shipment Status
        Task<List<ShippingStatus>> GetAllShippingStatusAsync();
        Task<ShippingStatus?> GetShippingStatusByIdAsync(int shippingStatusId);
        Task<ShippingStatus> AddShippingStatusAsync(ShippingStatus shippingStatus);
        Task DeleteShippingStatusAsync(ShippingStatus shippingStatus);
    }
}