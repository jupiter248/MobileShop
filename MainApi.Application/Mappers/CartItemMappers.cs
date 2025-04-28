using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.CartItem;
using MainApi.Domain.Models.Orders;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;

namespace MainApi.Application.Mappers
{
    public static class CartItemMappers
    {
        public static CartItemDto ToCartItemDto(this CartItem cartItem)
        {
            return new CartItemDto()
            {
                Id = cartItem.Id,
                Username = cartItem.AppUser.UserName,
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.TotalPrice,
                AttributeXml = cartItem.AttributeXml,
                Product = cartItem.Product
            };
        }
        public static CartItem ToCartItemFromAdd(this AddCartItemRequestDto addCartItemRequestDto, Product product, string attributeXml, AppUser appUser)
        {
            return new CartItem()
            {
                ProductId = addCartItemRequestDto.ProductId,
                Quantity = addCartItemRequestDto.Quantity,
                Product = product,
                AttributeXml = attributeXml,
                AppUser = appUser,
                UserId = appUser.Id,
                BasePrice = addCartItemRequestDto.Price,
                TotalPrice = addCartItemRequestDto.Price * addCartItemRequestDto.Quantity
            };
        }
    }
}