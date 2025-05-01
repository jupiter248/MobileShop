using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Orders;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetUserCartItemsAsync(string username);
        Task<List<CartItem>> GetCartItemsById(List<int> Ids);
        Task<CartItem?> GetCartItemByIdAsync(int cartItemId);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<bool> RemoveCartItemAsync(int cartItemId, string username);
        Task<CartItem?> UpdateCartItemQuantityAsync(int cartItemId, string username, int quantity);
    }
}