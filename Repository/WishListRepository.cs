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
            WishList? wishListModel = await _context.WishLists
            .FirstOrDefaultAsync(i => i.UserId == wishList.UserId && i.ProductId == wishList.ProductId);

            if (wishListModel == null)
            {
                await _context.WishLists.AddAsync(wishList);
                await _context.SaveChangesAsync();
                return wishList;
            }
            return null;

        }

        public async Task<List<WishList>> GetUserPortfolioAsync(string username)
        {
            List<WishList> wishLists = await _context.WishLists.Include(p => p.Product).ThenInclude(c => c.Category).Where(u => u.AppUser.UserName == username).ToListAsync();
            return wishLists;
        }

        public async Task<WishList?> RemoveWishListAsync(int productId, string username)
        {
            WishList? wishList = await _context.WishLists.FirstOrDefaultAsync(x => x.ProductId == productId && x.AppUser.UserName == username);
            if (wishList != null)
            {
                _context.Remove(wishList);
                await _context.SaveChangesAsync();
                return wishList;
            }
            return null;
        }
    }
}