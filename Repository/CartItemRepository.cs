using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Orders;
using MainApi.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartItemRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            await _context.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem?> GetCartItemByIdAsync(int cartItemId)
        {
            CartItem? cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return null;
            }
            return cartItem;
        }

        public async Task<List<CartItem>> GetUserCartItemsAsync(string username)
        {

            List<CartItem> cartItems = await _context.CartItems.Include(c => c.AppUser).Where(c => c.AppUser.UserName == username).ToListAsync();
            return cartItems;
        }

        public async Task<bool> RemoveCartItemAsync(int cartItemId, string username)
        {
            CartItem? cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId && c.AppUser.UserName == username);
            if (cartItem == null)
            {
                return false;
            }
            _context.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartItem?> UpdateCartItemQuantityAsync(int cartItemId, string username, int quantity)
        {
            CartItem? cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId && c.AppUser.UserName == username);
            if (cartItem == null)
            {
                return null;
            }
            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();
            return cartItem;
        }
    }
}