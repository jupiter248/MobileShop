using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders.OrderShipment;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models.Orders;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/shipment")]
    public class OrderShipmentController : ControllerBase
    {
        private readonly IOrderShipmentRepository _orderShipmentRepo;
        private readonly IOrderRepository _orderRepo;

        public OrderShipmentController(IOrderShipmentRepository orderShipmentRepo, IOrderRepository orderRepo)
        {
            _orderShipmentRepo = orderShipmentRepo;
            _orderRepo = orderRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] AddShipmentRequestDto shipmentRequestDto)
        {
            Order? order = await _orderRepo.GetOrderByIdAsync(shipmentRequestDto.OrderId);
            if (order == null) return NotFound("order not found");

            ShippingStatus? shippingStatus = await _orderShipmentRepo.GetShippingStatusByNameAsync(shipmentRequestDto.StatusName);
            if (shippingStatus == null) return NotFound("Shipping status not found");

            List<OrderItem> orderItems = await _orderRepo.GetOrderItemsByIdAsync(shipmentRequestDto.OrderItemsIds);
            if (orderItems.Count < 1) return BadRequest("No order item has been selected");

            List<ShipmentItem> shipmentItems = orderItems.Select(i => new ShipmentItem()
            {
                OrderItem = i,
                OrderItemId = i.Id,

            }).ToList();

            OrderShipment orderShipment = await _orderShipmentRepo.AddOrderShipmentAsync(shipmentRequestDto.ToOrderShipmentFromAdd(shippingStatus, order, shipmentItems));
            return Created();
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllOrderShipment([FromRoute] int id)
        {
            List<OrderShipment> orderShipments = await _orderShipmentRepo.GetAllOrderShipmentAsync(id);
            if (orderShipments.Count < 1) return BadRequest("No order shipment has been selected");
            List<OrderShipmentDto> orderShipmentDtos = orderShipments.Select(s => s.ToShipmentDto()).ToList();
            return Ok(orderShipmentDtos);
        }

        [HttpPost("status")]
        public async Task<IActionResult> AddShippingStatus([FromBody] AddShippingStatusRequestDto addShippingStatusRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ShippingStatus shippingStatus = await _orderShipmentRepo.AddShippingStatusAsync(addShippingStatusRequestDto.ToShippingStatusFromAdd());
            return Created();
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetAllShippingStatus()
        {
            List<ShippingStatus> shippingStatus = await _orderShipmentRepo.GetAllShippingStatusAsync();
            if (shippingStatus.Count < 1) return NotFound("There is no shipping status");
            return Ok(shippingStatus);
        }
    }
}