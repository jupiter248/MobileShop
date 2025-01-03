using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Order;
using MainApi.Dtos.Orders.Order;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            List<Order>? orders = await _orderRepository.GetAllOrdersAsync();
            if (orders == null)
            {
                return BadRequest();
            }
            decimal totalAmount = 0;
            foreach (var item in orders)
            {
                List<OrderItem> orderItems = item.OrderItems.ToList();
                foreach (var item1 in orderItems)
                {
                    totalAmount += item1.PriceAtPurchase;
                }
                item.TotalAmount = totalAmount;
            }
            List<OrderDto>? ordersDto = orders.Select(o => o.ToOrderDto()).ToList();
            return Ok(ordersDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderRequestDto addOrderRequestDto)
        {
            var order = addOrderRequestDto.ToOrderFromAdd();
            return Ok(order.ToOrderDto());
        }
    }
}