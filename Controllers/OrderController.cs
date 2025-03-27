using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders;
using MainApi.Dtos.Orders.Order;
using MainApi.Extensions;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models;
using MainApi.Models.Orders;
using MainApi.Models.Products;
using MainApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IAddressRepository _addressRepo;


        public OrderController(IOrderRepository orderRepo, IAddressRepository addressRepo, IProductRepository productRepository, UserManager<AppUser> userManager, ICartItemRepository cartItemRepo)
        {
            _orderRepo = orderRepo;
            _productRepository = productRepository;
            _userManager = userManager;
            _cartItemRepo = cartItemRepo;
            _addressRepo = addressRepo;
        }
        // [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return NotFound("Username is invalid");

            List<Order>? orders = await _orderRepo.GetAllOrdersAsync(username);
            if (orders == null)
            {
                return BadRequest();
            }
            List<OrderDto>? ordersDto = orders.Select(o => o.ToOrderDto()).ToList();
            return Ok(ordersDto);
        }
        // [Authorize(Roles = "Admin")]
        // [HttpGet("{id:int}")]
        // public async Task<IActionResult> GetOrderById([FromRoute] int id)
        // {
        //     Order? order = await _orderRepository.GetOrderByIdAsync(id);
        //     if (order == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(order.ToOrderDto());
        // }
        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderRequestDto addOrderRequestDto)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return NotFound("Username is invalid");

            AppUser? appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return NotFound("User not found");

            List<CartItem> cartItems = await _cartItemRepo.GetCartItemsById(addOrderRequestDto.CartItemsIds);
            if (cartItems == null) return BadRequest("Cart item id is invalid");

            Address? address = await _addressRepo.GetAddressByIdAsync(addOrderRequestDto.AddressId);
            if (address == null) return NotFound("Address not found");

            List<OrderItem> orderItems = cartItems.Select(c => c.ToOrderItem()).ToList();
            decimal[] arrays = orderItems.Select(i => i.PriceAtPurchase).ToArray();

            OrderStatus? orderStatus = await _orderRepo.GetOrderStatusByNameAsync("Pending");
            Order newOrder = new Order()
            {
                Address = address,
                AddressId = address.Id,
                User = appUser,
                UserId = appUser.Id,
                TotalAmount = arrays.Sum(),
                StatusId = orderStatus.Id,
                OrderStatus = orderStatus,
                OrderItems = orderItems
            };

            Order order = await _orderRepo.AddOrderAsync(newOrder);
            return Created();
        }
        // [Authorize]
        // [HttpPut("{id:int}")]
        // public async Task<IActionResult> AddOrderItemsToOrder([FromRoute] int id, [FromBody] AddOrderItemRequestDto addOrderItemRequestDto)
        // {
        //     var orderItems = addOrderItemRequestDto.ToOrderItemFromAdd();
        //     var order = await _orderRepository.UpdateOrderItemAsync(orderItems, id);
        //     if (order != null)
        //     {
        //         return NoContent();
        //     }
        //     else
        //     {
        //         return BadRequest();
        //     }
        // }
        // [Authorize]
        // [Route("OrderStatus/{id:int}")]
        // [HttpPut]
        // public async Task<IActionResult> UpdateOrderStatus([FromRoute] int id, [FromBody] int statusId)
        // {
        //     Order? order = await _orderRepository.UpdateOrderStatusAsync(id, statusId);
        //     if (order == null) return NotFound();

        //     return NoContent();
        // }
        // [HttpDelete("{id:int}")]
        // public async Task<IActionResult> RemoveOrder([FromRoute] int id)
        // {
        //     Order? order = await _orderRepository.RemoveOrderAsync(id);
        //     if (order == null) return NotFound();
        //     return NoContent();
        // }
        // [Authorize]
        // [Route("OrderItem/{id:int}")]
        // [HttpDelete]
        // public async Task<IActionResult> RemoveOrderItem([FromRoute] int id, [FromBody] int orderItemId)
        // {
        //     Order? order = await _orderRepository.RemoveOrderItemsAsync(id, orderItemId);
        //     if (order == null) return NotFound();
        //     return NoContent();
        // }
    }
}