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
        Task<CartItem?> GetCartItemByIdAsync(int cartItemId);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<bool> RemoveCartItemAsync(int cartItemId, string username);
        Task<CartItem?> UpdateCartItemQuantityAsync(int cartItemId, string username, int quantity);
    }
}