using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.CartItem;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Orders;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.ProductAttributes;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Infrastructure.Services.Internal
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IProductAttributeRepository _productAttributeRepo;
        private readonly IXmlService _xmlService;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<AppUser> _userManager;
        public CartItemService(
            ICartItemRepository cartItemRepo,
            IProductAttributeRepository productAttributeRepo,
            IXmlService xmlService,
            IProductRepository productRepository,
            UserManager<AppUser> userManager
            )
        {
            _cartItemRepo = cartItemRepo;
            _productAttributeRepo = productAttributeRepo;
            _xmlService = xmlService;
            _productRepo = productRepository;
            _userManager = userManager;

        }
        public async Task<CartItemDto> AddCartItemAsync(AddCartItemRequestDto addCartItemRequestDto, string username)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) throw new KeyNotFoundException("User not found");

            Product? product = await _productRepo.GetProductByIdAsync(addCartItemRequestDto.ProductId);
            if (product == null) throw new KeyNotFoundException("Product not found");

            List<PredefinedProductAttributeValue> attributes = await _productAttributeRepo.GetAttributeValuesById(addCartItemRequestDto.AttributeIds);
            if (attributes.Count < 1) throw new ValidationException("Attribute ids are invalid");

            string xmlAttributes = _xmlService.GenerateAttributeXml(attributes);

            CartItem newCartItem = addCartItemRequestDto.ToCartItemFromAdd(product, xmlAttributes, appUser);
            CartItem cartItem = await _cartItemRepo.AddCartItemAsync(newCartItem);
            return cartItem.ToCartItemDto();
        }

        public async Task DeleteCartItemAsync(int cartItemId, string username)
        {
            bool cartItemDeleted = await _cartItemRepo.RemoveCartItemAsync(cartItemId, username);
            if (cartItemDeleted == false)
            {
                throw new KeyNotFoundException("CartItem not found");
            }
        }
        public async Task<CartItemDto> GetCartItemByIdAsync(int cartItemId)
        {
            CartItem? cartItem = await _cartItemRepo.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null)
            {
                throw new KeyNotFoundException("CartItem not found");
            }
            return cartItem.ToCartItemDto();
        }

        public async Task<List<CartItemDto>> GetAllCartItemsAsync(string username)
        {

            List<CartItem> cartItems = await _cartItemRepo.GetUserCartItemsAsync(username);
            List<CartItemDto> cartItemDtos = cartItems.Select(c => c.ToCartItemDto()).ToList();
            return cartItemDtos;
        }

        public async Task UpdateCartItemQuantityAsync(int cartItemId, string username, int newQuantity)
        {
            CartItem? cartItem = await _cartItemRepo.UpdateCartItemQuantityAsync(cartItemId, username, newQuantity);
            if (cartItem == null)
            {
                throw new KeyNotFoundException("CartItem not found");
            }
        }
    }
}