using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.CartItem;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Orders;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.ProductAttributes;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Api.Controllers
{
    [ApiController]
    [Route("api/cart-item")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IProductAttributeRepository _productAttributeRepo;
        private readonly IXmlService _xmlService;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<AppUser> _userManager;
        public CartItemController(
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

        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemRequestDto addItemDto)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
            AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (appUser == null) return NotFound("User is not found");

            Product? product = await _productRepo.GetProductByIdAsync(addItemDto.ProductId);
            if (product == null) return NotFound("Product not found");

            List<PredefinedProductAttributeValue> attributes = await _productAttributeRepo.GetAttributeValuesById(addItemDto.AttributeIds);
            if (attributes.Count < 1) return BadRequest("Attribute ids are invalid");

            string xmlAttributes = _xmlService.GenerateAttributeXml(attributes);

            CartItem newCartItem = addItemDto.ToCartItemFromAdd(product, xmlAttributes, appUser);

            CartItem cartItem = await _cartItemRepo.AddCartItemAsync(newCartItem);
            return Created();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            List<CartItem> cartItems = await _cartItemRepo.GetUserCartItemsAsync(username);
            List<CartItemDto> cartItemDtos = cartItems.Select(c => c.ToCartItemDto()).ToList();
            return Ok(cartItemDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCartItemById([FromRoute] int id)
        {
            CartItem? cartItem = await _cartItemRepo.GetCartItemByIdAsync(id);
            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }
            return Ok(cartItem);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateQuantity([FromRoute] int id, [FromBody] int quantity)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            CartItem? cartItem = await _cartItemRepo.UpdateCartItemQuantityAsync(id, username, quantity);
            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int id)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            bool cartItemDeleted = await _cartItemRepo.RemoveCartItemAsync(id, username);
            if (cartItemDeleted == false)
            {
                return NotFound("Cart item not found");
            }
            return NoContent();
        }
    }
}