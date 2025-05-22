using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.Order;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Orders;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Infrastructure.Services.Internal
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IAddressRepository _addressRepo;


        public OrderService(IOrderRepository orderRepo, IAddressRepository addressRepo, IProductRepository productRepository, UserManager<AppUser> userManager, ICartItemRepository cartItemRepo)
        {
            _orderRepo = orderRepo;
            _productRepository = productRepository;
            _userManager = userManager;
            _cartItemRepo = cartItemRepo;
            _addressRepo = addressRepo;
        }
        public async Task<OrderDto> AddOrderAsync(AddOrderRequestDto addOrderRequestDto, string username)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(username) ?? throw new KeyNotFoundException("User not found");

            List<CartItem> cartItems = await _cartItemRepo.GetCartItemsById(addOrderRequestDto.CartItemsIds);

            Address? address = await _addressRepo.GetAddressByIdAsync(addOrderRequestDto.AddressId) ?? throw new KeyNotFoundException("Address not found");

            List<OrderItem> orderItems = cartItems.Select(c => c.ToOrderItem()).ToList();
            decimal[] arrays = orderItems.Select(i => i.PriceAtPurchase).ToArray();

            OrderStatus? orderStatus = await _orderRepo.GetOrderStatusByNameAsync("Pending") ?? throw new KeyNotFoundException("OrderStatus not found");
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
            return order.ToOrderDto();
        }

        public async Task<OrderStatusesDto> AddOrderStatusAsync(AddOrderStatusRequestDto addOrderStatusRequestDto)
        {
            OrderStatus orderStatus = await _orderRepo.AddOrderStatusAsync(addOrderStatusRequestDto.ToOrderStatus());
            return orderStatus.ToOrderStatusDto();
        }

        public async Task<List<OrderStatusesDto>> GetAllOrderStatusesAsync()
        {
            List<OrderStatus> orderStatuses = await _orderRepo.GetAllOrderStatusesAsync();
            List<OrderStatusesDto> orderStatusesDtos = orderStatuses.Select(s => s.ToOrderStatusDto()).ToList();
            return orderStatusesDtos;
        }

        public async Task<List<OrderDto>> GetAllUsersOrdersAsync(string username)
        {
            List<Order> orders = await _orderRepo.GetAllOrdersAsync(username);
            List<OrderDto>? ordersDto = orders.Select(o => o.ToOrderDto()).ToList();
            return ordersDto;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            Order order = await _orderRepo.GetOrderByIdAsync(orderId) ?? throw new KeyNotFoundException("Order not found");
            return order.ToOrderDto();
        }
    }
}