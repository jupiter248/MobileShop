using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders;
using MainApi.Application.Dtos.Orders.Order;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
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
        private readonly IOrderService _orderService;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IAddressRepository _addressRepo;


        public OrderController(IOrderService orderService, IAddressRepository addressRepo, IProductRepository productRepository, UserManager<AppUser> userManager, ICartItemRepository cartItemRepo)
        {
            _orderService = orderService;
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
            List<OrderDto> orderDtos = await _orderService.GetAllUsersOrdersAsync(username);
            return Ok(orderDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            OrderDto? order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderRequestDto addOrderRequestDto)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return NotFound("Username is invalid");



            OrderDto orderDto = await _orderService.AddOrderAsync(addOrderRequestDto, username);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderDto.Id }, orderDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("status")]
        public async Task<IActionResult> AddOrderStatus([FromBody] AddOrderStatusRequestDto statusRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            OrderStatusesDto orderStatusesDto = await _orderService.AddOrderStatusAsync(statusRequestDto);
            return Created();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("status")]
        public async Task<IActionResult> GetAllOrderStatuses()
        {
            List<OrderStatusesDto> statusesDtos = await _orderService.GetAllOrderStatusesAsync();
            return Ok(statusesDtos);
        }
    }
}