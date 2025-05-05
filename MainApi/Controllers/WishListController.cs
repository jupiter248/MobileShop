using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Products;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
{
    [ApiController]
    [Route("api/favorite")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListRepository _wishListRepo;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<AppUser> _userManager;
        public WishListController(IWishListRepository wishListRepo, IProductRepository productRepo, UserManager<AppUser> userManager)
        {
            _wishListRepo = wishListRepo;
            _productRepo = productRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWishLists()
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
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
            return Ok(productDtos);
        }
        [HttpPost("{productId:int}")]
        public async Task<IActionResult> AddToWishList([FromRoute] int productId)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            AppUser? appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest("There is no user with this username");

            Product? product = await _productRepo.GetProductByIdAsync(productId);
            if (product == null) return NotFound("Product not found");

            WishList? wishListModel = new WishList()
            {
                ProductId = productId,
                UserId = appUser.Id,
                AppUser = appUser,
                Product = product,
            };
            await _wishListRepo.AddWishListAsync(wishListModel);
            if (wishListModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteFromWishList([FromRoute] int productId)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            WishList? wishList = await _wishListRepo.RemoveWishListAsync(productId, username);
            if (wishList == null)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}