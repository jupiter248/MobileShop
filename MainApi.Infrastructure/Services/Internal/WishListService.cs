using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Products;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Infrastructure.Services.Internal
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _wishListRepo;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<AppUser> _userManager;
        public WishListService(IWishListRepository wishListRepo, IProductRepository productRepo, UserManager<AppUser> userManager)
        {
            _wishListRepo = wishListRepo;
            _productRepo = productRepo;
            _userManager = userManager;
        }
        public async Task<ProductDto> AddToWishListAsync(int productId, string username)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(username) ?? throw new KeyNotFoundException("User not found");

            Product? product = await _productRepo.GetProductByIdAsync(productId) ?? throw new KeyNotFoundException("Product not found");

            WishList? wishListModel = new WishList()
            {
                ProductId = productId,
                UserId = appUser.Id,
                AppUser = appUser,
                Product = product,
            };
            WishList wishList = await _wishListRepo.AddWishListItemAsync(wishListModel);
            return product.ToProductDto();
        }

        public async Task DeleteFromWishListAsync(int productId, string username)
        {
            WishList wishListItem = await _wishListRepo.GetWishListItemByIdAsync(productId, username) ?? throw new KeyNotFoundException("WishListItem not found");
            await _wishListRepo.DeleteWishListItemAsync(wishListItem);
        }

        public async Task<List<ProductDto>> GetWishListAsync(string username)
        {
            List<WishList>? wishLists = await _wishListRepo.GetUserWishListAsync(username);
            List<ProductDto> productDtos = wishLists
            .Select(p => new ProductDto
            {
                Id = p.ProductId,
                Brand = p.Product.Brand,
                categoryId = p.Product.CategoryId,
                Description = p.Product.Description,
                Model = p.Product.Model,
                Price = p.Product.Price,
                ProductName = p.Product.ProductName,
                Quantity = p.Product.Quantity,
            }).ToList();
            return productDtos;
        }
    }
}