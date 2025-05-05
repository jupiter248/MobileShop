using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.OrderShipment;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Orders;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
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
        // Shipment
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
        [HttpGet("find-by-id{id:int}")]

        public async Task<IActionResult> GetShipmentById([FromRoute] int id)
        {
            OrderShipment? orderShipment = await _orderShipmentRepo.GetShipmentByIdAsync(id);
            if (orderShipment == null)
            {
                return NotFound("Shipment not found");
            }
            return Ok(orderShipment);

        }
        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetAllOrderShipment([FromRoute] int orderId)
        {
            List<OrderShipment> orderShipments = await _orderShipmentRepo.GetAllOrderShipmentAsync(orderId);
            if (orderShipments.Count < 1) return BadRequest("No order shipment has been selected");
            List<OrderShipmentDto> orderShipmentDtos = orderShipments.Select(s => s.ToShipmentDto()).ToList();
            return Ok(orderShipmentDtos);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShipment([FromRoute] int id)
        {
            bool orderShipmentDeleted = await _orderShipmentRepo.DeleteShipmentAsync(id);
            if (orderShipmentDeleted == false)
            {
                return NotFound("Shipment not found");
            }
            return NoContent();
        }

        // Shipping Status

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
        [HttpDelete("status{id:int}")]
        public async Task<IActionResult> DeleteShippingStatus([FromRoute] int id)
        {
            bool shippingStatusDeleted = await _orderShipmentRepo.RemoveShippingStatusAsync(id);
            if (shippingStatusDeleted == false)
            {
                return NotFound("Shipping status not found");
            }
            return NoContent();
        }

        // Shipment Item

        [HttpPost("item{shipmentId:int}")]
        public async Task<IActionResult> AddItemToShipmentItem([FromBody] List<int> orderItemIds, [FromRoute] int shipmentId)
        {
            List<OrderItem> orderItems = await _orderRepo.GetOrderItemsByIdAsync(orderItemIds);
            if (orderItems.Count < 1) return BadRequest("No order item has been selected");

            List<ShipmentItem> shipmentItems = orderItems.Select(i => new ShipmentItem()
            {
                OrderItem = i,
                OrderItemId = i.Id,
                OrderShipmentId = shipmentId

            }).ToList();
            await _orderShipmentRepo.AddItemToShipmentAsync(shipmentItems);
            return Created();
        }
        [HttpDelete("item{id:int}")]
        public async Task<IActionResult> DeleteShipmentItem([FromRoute] int id)
        {
            bool shipmentItemDeleted = await _orderShipmentRepo.RemoveFromShipmentAsync(id);
            if (shipmentItemDeleted == false)
            {
                return NotFound("Shipment item not found");
            }
            return NoContent();
        }

    }
}