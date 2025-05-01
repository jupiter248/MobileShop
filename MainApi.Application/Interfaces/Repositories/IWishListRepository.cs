using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface IWishListRepository
    {
        Task<List<WishList>> GetUserWishListAsync(string username);
        Task<WishList?> AddWishListAsync(WishList wishList);
        Task<WishList?> RemoveWishListAsync(int productId, string username);
    }

}