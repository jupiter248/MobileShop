using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Orders;

namespace MainApi.Interfaces
{
    public interface IOrderShipmentRepository
    {
        //Shipment 
        Task<List<OrderShipment>> GetAllOrderShipmentAsync(int orderId);
        Task<OrderShipment?> GetShipmentByIdAsync(int shipmentId);
        Task<OrderShipment> AddOrderShipmentAsync(OrderShipment orderShipment);
        Task<bool> DeleteShipmentAsync(int shipmentId);

        //Shipment Item
        Task<List<ShipmentItem>> AddItemToShipmentAsync(List<ShipmentItem> shipmentItems);
        Task<bool> RemoveFromShipmentAsync(int itemId);

        //Shipment Status
        Task<List<ShippingStatus>> GetAllShippingStatusAsync();
        Task<ShippingStatus?> GetShippingStatusByNameAsync(string name);
        Task<ShippingStatus> AddShippingStatusAsync(ShippingStatus shippingStatus);
        Task<bool> RemoveShippingStatusAsync(string shippingStatusName);
    }
}