using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Products;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
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
        private readonly IWishListService _wishListService;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<AppUser> _userManager;
        public WishListController(IWishListService wishListService, IProductRepository productRepo, UserManager<AppUser> userManager)
        {
            _wishListService = wishListService;
            _productRepo = productRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWishLists()
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            List<ProductDto>? productDtos = await _wishListService.GetWishListAsync(username);
            return Ok(productDtos);
        }
        [HttpPost("{productId:int}")]
        public async Task<IActionResult> AddToWishList([FromRoute] int productId)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            await _wishListService.AddToWishListAsync(productId, username);
            return Created();
        }
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteFromWishList([FromRoute] int productId)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            await _wishListService.DeleteFromWishListAsync(productId, username);
            return NoContent();

        }
    }
}