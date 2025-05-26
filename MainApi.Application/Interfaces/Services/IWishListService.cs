using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Products;
using MainApi.Domain.Models.Products;

namespace MainApi.Application.Interfaces.Services
{
    public interface IWishListService
    {
        Task<List<ProductDto>> GetWishListAsync(string username);
        Task<ProductDto> AddToWishListAsync(int productId , string username);
        Task DeleteFromWishListAsync(int productId , string username);
    }
}