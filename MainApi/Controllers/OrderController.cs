using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders;
using MainApi.Application.Dtos.Orders.Order;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Mappers;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Orders;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
{

    [ApiController]
    [Route("api/order")]
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
        [Authorize]
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
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            Order? order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound("Order not found");
            }
            return Ok(order.ToOrderDto());
        }
        [Authorize]
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
            if (orderStatus == null) return NotFound("Order status not found");
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
        [Authorize(Roles = "Admin")]
        [HttpPost("status")]
        public async Task<IActionResult> AddOrderStatus([FromBody] AddOrderStatusRequestDto statusRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _orderRepo.AddOrderStatusAsync(statusRequestDto.ToOrderStatus());
            return Created();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("status")]
        public async Task<IActionResult> GetAllOrderStatuses()
        {
            List<OrderStatus> orderStatuses = await _orderRepo.GetAllOrderStatusesAsync();
            List<OrderStatusesDto> statusesDtos = orderStatuses.Select(s => s.ToOrderStatusDto()).ToList();
            return Ok(statusesDtos);
        }
    }
}