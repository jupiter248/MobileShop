using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Order;
using MainApi.Dtos.Orders.Order;
using MainApi.Dtos.Orders.OrderItem;
using MainApi.Extensions;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models;
using MainApi.Models.Orders;
using MainApi.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository, UserManager<AppUser> userManager)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            List<Order>? orders = await _orderRepository.GetAllOrdersAsync();
            if (orders == null)
            {
                return BadRequest();
            }
            List<OrderDto>? ordersDto = orders.Select(o => o.ToOrderDto()).ToList();
            return Ok(ordersDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            Order? order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order.ToOrderDto());
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderRequestDto addOrderRequestDto)
        {
            string? username = User.GetUsername();
            AppUser? appUser = await _userManager.FindByNameAsync(username);

            Order? orderModel = addOrderRequestDto.ToOrderFromAdd();
            if (appUser != null)
            {
                orderModel.UserId = appUser.Id;
                orderModel.User = appUser;
            }
            OrderStatus? orderStatus = await _orderRepository.GetOrderStatusByIdAsync(addOrderRequestDto.StatusId);
            if (orderStatus != null)
            {
                orderModel.OrderStatus = orderStatus;
            }
            decimal totalAmount = 0;

            List<OrderItem> orderItems = orderModel.OrderItems.ToList();
            foreach (var item1 in orderItems)
            {
                var productExists = await _productRepository.ProductExistsAsync(item1.ProductId);
                if (productExists)
                {
                    Product? product = await _productRepository.GetProductByIdAsync(item1.ProductId);
                    if (product != null)
                    {
                        item1.PriceAtPurchase = product.Price * item1.Quantity;
                    }
                }
                totalAmount += item1.PriceAtPurchase;
            }
            orderModel.TotalAmount = totalAmount;

            Order? order = await _orderRepository.AddOrderAsync(orderModel);
            if (order != null)
            {
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order.ToOrderDto());
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> AddOrderItemsToOrder([FromRoute] int id, [FromBody] AddOrderItemRequestDto addOrderItemRequestDto)
        {
            var orderItems = addOrderItemRequestDto.ToOrderItemFromAdd();
            var order = await _orderRepository.UpdateOrderItemAsync(orderItems, id);
            if (order != null)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [Route("OrderStatus/{id:int}")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] int id, [FromBody] int statusId)
        {
            Order? order = await _orderRepository.UpdateOrderStatusAsync(id, statusId);
            if (order == null) return NotFound();

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveOrder([FromRoute] int id)
        {
            Order? order = await _orderRepository.RemoveOrderAsync(id);
            if (order == null) return NotFound();
            return NoContent();
        }
        [Route("OrderItem/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveOrderItem([FromRoute] int id, [FromBody] int orderItemId)
        {
            Order? order = await _orderRepository.RemoveOrderItemsAsync(id, orderItemId);
            if (order == null) return NotFound();
            return NoContent();
        }
    }
}