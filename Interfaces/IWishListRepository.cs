using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products;
using MainApi.Models.User;

namespace MainApi.Interfaces
{
    public interface IWishListRepository
    {
        Task<List<WishList>> GetUserWishListAsync(string username);
        Task<WishList?> AddWishListAsync(WishList wishList);
        Task<WishList?> RemoveWishListAsync(int productId, string username);
    }

}