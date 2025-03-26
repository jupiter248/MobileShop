using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Orders;

namespace MainApi.Interfaces
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetUserCartItemsAsync(string username);
        Task<CartItem?  > GetCartItemByIdAsync(int cartItemId);
        Task<CartItem> AddToCartItems(CartItem cartItem);
        Task<bool> RemoveCartItem(int cartItemId, string username);
        Task<CartItem?> UpdateCartItemQuantity(int cartItemId, string username, int quantity);
    }
}