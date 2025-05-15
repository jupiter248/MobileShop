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
        private readonly ICartItemService _cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemRequestDto addItemDto)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");


            CartItemDto cartItemDto = await _cartItemService.AddCartItemAsync(addItemDto, username);
            return CreatedAtAction(nameof(GetCartItemById), new { id = cartItemDto.Id }, cartItemDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            List<CartItemDto> cartItemDtos = await _cartItemService.GetAllCartItemsAsync(username);
            return Ok(cartItemDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCartItemById([FromRoute] int id)
        {
            CartItemDto cartItemDto = await _cartItemService.GetCartItemByIdAsync(id);
            return Ok(cartItemDto);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateQuantity([FromRoute] int id, [FromBody] int quantity)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            await _cartItemService.UpdateCartItemQuantityAsync(id, username, quantity);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int id)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            await _cartItemService.DeleteCartItemAsync(id, username);
            return NoContent();
        }
    }
}