using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using MainApi.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Persistence.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public WishListRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<WishList> AddWishListItemAsync(WishList wishList)
        {
            await _context.WishLists.AddAsync(wishList);
            await _context.SaveChangesAsync();
            return wishList;
        }

        public async Task<List<WishList>> GetUserWishListAsync(string username)
        {
            List<WishList> wishLists = await _context.WishLists.Include(p => p.Product).ThenInclude(c => c.Category).Where(u => u.AppUser.UserName == username).ToListAsync();
            return wishLists;
        }

        public async Task<WishList?> GetWishListItemByIdAsync(int productId, string username)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            WishList? wishListModel = await _context.WishLists
            .FirstOrDefaultAsync(i => i.UserId == appUser.Id && i.ProductId == productId);
            if (wishListModel == null || appUser == null)
            {
                return null;
            }
            return wishListModel;
        }

        public async Task DeleteWishListItemAsync(WishList wishList)
        {
            _context.Remove(wishList);
            await _context.SaveChangesAsync();
        }

    }
}