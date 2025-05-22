using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.OrderShipment;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
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
        private readonly IOrderShipmentService _orderShipmentService;
        private readonly IOrderRepository _orderRepo;

        public OrderShipmentController(IOrderShipmentService orderShipmentService, IOrderRepository orderRepo)
        {
            _orderShipmentService = orderShipmentService;
            _orderRepo = orderRepo;
        }
        // Shipment
        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] AddShipmentRequestDto shipmentRequestDto)
        {
            OrderShipmentDto orderShipmentDto = await _orderShipmentService.AddShipmentAsync(shipmentRequestDto);
            return CreatedAtAction(nameof(GetShipmentById), new { id = orderShipmentDto.Id }, orderShipmentDto);
        }
        [HttpGet("find-by-id{id:int}")]

        public async Task<IActionResult> GetShipmentById([FromRoute] int id)
        {
            OrderShipmentDto? orderShipmentDto = await _orderShipmentService.GetShipmentByIdAsync(id);
            return Ok(orderShipmentDto);

        }
        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetAllOrderShipment([FromRoute] int orderId)
        {

            List<OrderShipmentDto> orderShipmentDtos = await _orderShipmentService.GetAllUsersOrdersShipmentAsync(orderId);
            return Ok(orderShipmentDtos);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShipment([FromRoute] int id)
        {
            await _orderShipmentService.DeleteShipmentAsync(id);
            return NoContent();
        }

        // Shipping Status

        [HttpPost("status")]
        public async Task<IActionResult> AddShippingStatus([FromBody] AddShippingStatusRequestDto addShippingStatusRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ShippingStatusDto shippingStatusDto = await _orderShipmentService.AddShippingStatusAsync(addShippingStatusRequestDto);
            return Created();
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetAllShippingStatus()
        {
            List<ShippingStatusDto> shippingStatusDtos = await _orderShipmentService.GetAllShippingStatusAsync();
            return Ok(shippingStatusDtos);
        }
        [HttpDelete("status{id:int}")]
        public async Task<IActionResult> DeleteShippingStatus([FromRoute] int id)
        {
            await _orderShipmentService.DeleteShippingStatusAsync(id);
            return NoContent();
        }

        // Shipment Item

        [HttpPost("item{shipmentId:int}")]
        public async Task<IActionResult> AddItemToShipmentItem([FromBody] List<int> orderItemIds, [FromRoute] int shipmentId)
        {
            await _orderShipmentService.AddItemToShipmentItemAsync(orderItemIds, shipmentId);
            return Created();
        }
        [HttpDelete("item{id:int}")]
        public async Task<IActionResult> DeleteShipmentItem([FromRoute] int id)
        {
            await _orderShipmentService.DeleteShipmentItemAsync(id);
            return NoContent();
        }

    }
}