using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Orders;

namespace MainApi.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<CartItem> AddToCartItems(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartItem>> GetUserCartItemsAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCartItem(int cartItemId, string username)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> UpdateCartItemQuantity(int cartItemId, string username)
        {
            throw new NotImplementedException();
        }
    }
}