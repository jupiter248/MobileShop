using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Products;
using MainApi.Models.User;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly ApplicationDbContext _context;
        public WishListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WishList?> AddWishListAsync(WishList wishList)
        {
            await _context.WishLists.AddAsync(wishList);
            await _context.SaveChangesAsync();
            return wishList;
        }

        public async Task<List<WishList>?> GetUserPortfolioAsync(string username)
        {
            List<WishList> wishLists = await _context.WishLists.Where(u => u.AppUser.UserName == username).ToListAsync();
            if (wishLists == null) return null;
            return wishLists;
        }

        public async Task<WishList?> RemoveWishListAsync(WishList wishList, string username)
        {
            if (wishList.AppUser.UserName == username)
            {
                _context.Remove(wishList);
                await _context.SaveChangesAsync();
                return wishList;
            }
            return null;
        }
    }
}