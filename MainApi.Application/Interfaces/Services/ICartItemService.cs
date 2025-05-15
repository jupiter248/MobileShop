using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.CartItem;

namespace MainApi.Application.Interfaces.Services
{
    public interface ICartItemService
    {
        Task<CartItemDto> AddCartItemAsync(AddCartItemRequestDto addCartItemRequestDto, string username);
        Task<List<CartItemDto>> GetAllCartItemsAsync(string username);
        Task<CartItemDto> GetCartItemByIdAsync(int cartItemId);
        Task UpdateCartItemQuantityAsync(int cartItemId, string username, int newQuantity);
        Task DeleteCartItemAsync(int cartItemId, string username);
    }
}