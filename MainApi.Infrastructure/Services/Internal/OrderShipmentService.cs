using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.OrderShipment;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Orders;

namespace MainApi.Infrastructure.Services.Internal
{
    public class OrderShipmentService : IOrderShipmentService
    {
        private readonly IOrderShipmentRepository _orderShipmentRepo;
        private readonly IOrderRepository _orderRepo;

        public OrderShipmentService(IOrderShipmentRepository orderShipmentRepo, IOrderRepository orderRepo)
        {
            _orderShipmentRepo = orderShipmentRepo;
            _orderRepo = orderRepo;
        }
        public async Task<List<OrderShipmentItemDto>> AddItemToShipmentItemAsync(List<int> orderItemIds, int shipmentId)
        {
            List<OrderItem> orderItems = await _orderRepo.GetOrderItemsByIdAsync(orderItemIds) ?? throw new KeyNotFoundException("No order item has been selected");

            List<ShipmentItem> shipmentItems = orderItems.Select(i => new ShipmentItem()
            {
                OrderItem = i,
                OrderItemId = i.Id,
                OrderShipmentId = shipmentId

            }).ToList();
            await _orderShipmentRepo.AddItemToShipmentAsync(shipmentItems);
            List<OrderShipmentItemDto> orderShipmentItemDtos = shipmentItems.Select(i => i.ToShipmentItemDto()).ToList();
            return orderShipmentItemDtos;
        }

        public async Task<OrderShipmentDto> AddShipmentAsync(AddShipmentRequestDto addShipmentRequestDto)
        {
            Order? order = await _orderRepo.GetOrderByIdAsync(addShipmentRequestDto.OrderId) ?? throw new KeyNotFoundException("Order not found");


            ShippingStatus? shippingStatus = await _orderShipmentRepo.GetShippingStatusByIdAsync(addShipmentRequestDto.StatusId) ?? throw new KeyNotFoundException("Shipping status not found");

            List<OrderItem> orderItems = await _orderRepo.GetOrderItemsByIdAsync(addShipmentRequestDto.OrderItemsIds) ?? throw new KeyNotFoundException("No order item has been selected");

            List<ShipmentItem> shipmentItems = orderItems.Select(i => new ShipmentItem()
            {
                OrderItem = i,
                OrderItemId = i.Id,

            }).ToList();

            OrderShipment orderShipment = await _orderShipmentRepo.AddOrderShipmentAsync(addShipmentRequestDto.ToOrderShipmentFromAdd(shippingStatus, order, shipmentItems));
            return orderShipment.ToShipmentDto();
        }

        public async Task<ShippingStatusDto> AddShippingStatusAsync(AddShippingStatusRequestDto addShippingStatusRequestDto)
        {
            ShippingStatus shippingStatus = await _orderShipmentRepo.AddShippingStatusAsync(addShippingStatusRequestDto.ToShippingStatusFromAdd());
            return shippingStatus.ToShippingStatusDto();
        }

        public async Task DeleteShipmentAsync(int shipmentId)
        {
            OrderShipment orderShipment = await _orderShipmentRepo.GetShipmentByIdAsync(shipmentId) ?? throw new KeyNotFoundException("OrderShipment not found");
            await _orderShipmentRepo.DeleteShipmentAsync(orderShipment);
        }

        public async Task DeleteShipmentItemAsync(int shipmentItemId)
        {
            ShipmentItem shipmentItem = await _orderShipmentRepo.GetShipmentItemByIdAsync(shipmentItemId) ?? throw new KeyNotFoundException("ShipmentItem not found");
            await _orderShipmentRepo.DeleteFromShipmentAsync(shipmentItem);
        }

        public async Task DeleteShippingStatusAsync(int shippingStatusId)
        {
            ShippingStatus shippingStatus = await _orderShipmentRepo.GetShippingStatusByIdAsync(shippingStatusId) ?? throw new KeyNotFoundException("ShippingStatus not found");
            await _orderShipmentRepo.DeleteShippingStatusAsync(shippingStatus);
        }

        public async Task<List<ShippingStatusDto>> GetAllShippingStatusAsync()
        {
            List<ShippingStatus> shippingStatus = await _orderShipmentRepo.GetAllShippingStatusAsync();
            List<ShippingStatusDto> shippingStatusDtos = shippingStatus.Select(s => s.ToShippingStatusDto()).ToList();
            return shippingStatusDtos;
        }

        public async Task<List<OrderShipmentDto>> GetAllUsersOrdersShipmentAsync(int orderId)
        {
            List<OrderShipment> orderShipments = await _orderShipmentRepo.GetAllOrderShipmentAsync(orderId) ?? throw new KeyNotFoundException("No order shipment has been selected");
            List<OrderShipmentDto> orderShipmentDtos = orderShipments.Select(s => s.ToShipmentDto()).ToList();
            return orderShipmentDtos;
        }

        public async Task<OrderShipmentDto> GetShipmentByIdAsync(int shipmentId)
        {
            OrderShipment orderShipment = await _orderShipmentRepo.GetShipmentByIdAsync(shipmentId) ?? throw new KeyNotFoundException("OrderShipment not found");
            return orderShipment.ToShipmentDto();
        }
    }
}