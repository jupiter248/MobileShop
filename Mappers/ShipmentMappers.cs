using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders.OrderShipment;
using MainApi.Models.Orders;

namespace MainApi.Mappers
{
    public static class ShipmentMappers
    {
        // Shipment
        public static OrderShipmentDto ToShipmentDto(this OrderShipment orderShipment)
        {
            return new OrderShipmentDto()
            {
                Id = orderShipment.Id,
                OrderId = orderShipment.OrderId,
                ShippingStatusName = orderShipment.ShippingStatus.Name,
                TotalWeight = orderShipment.TotalWeight,
                TrackingNumber = orderShipment.TrackingNumber,
                CreatedOn = orderShipment.CreatedOn,
                DeliveredDate = orderShipment.DeliveredDate,
                ShippedDate = orderShipment.ShippedDate,
                ShipmentItemDtos = orderShipment.ShipmentItems.Select(i => i.ToShipmentItemDto()).ToList()
            };
        }
        public static OrderShipment ToOrderShipmentFromAdd(this AddShipmentRequestDto addShipment, ShippingStatus shippingStatus, Order order, List<ShipmentItem> shipmentItems)
        {
            return new OrderShipment()
            {
                OrderId = addShipment.OrderId,
                ShippingStatus = shippingStatus,
                ShippingStatusId = shippingStatus.Id,
                TotalWeight = addShipment.TotalWeight,
                TrackingNumber = addShipment.TrackingNumber,
                Order = order,
                ShippedDate = addShipment.ShippedDate,
                ShipmentItems = shipmentItems
            };
        }

        //Shipment Item
        public static OrderShipmentItemDto ToShipmentItemDto(this ShipmentItem shipmentItem)
        {
            return new OrderShipmentItemDto()
            {
                OrderItemId = shipmentItem.OrderItemId,
                OrderItem = shipmentItem.OrderItem
            };
        }

        //Shipment Status
        public static ShippingStatusDto ToShippingStatusDto(this ShippingStatus shippingStatus)
        {
            return new ShippingStatusDto()
            {
                Id = shippingStatus.Id,
                Name = shippingStatus.Name,
                Description = shippingStatus.Description
            };
        }
        public static ShippingStatus ToShippingStatusFromAdd(this AddShippingStatusRequestDto addShippingStatusDto)
        {
            return new ShippingStatus()
            {
                Name = addShippingStatusDto.Name,
                Description = addShippingStatusDto.Name,
            };
        }
    }
}