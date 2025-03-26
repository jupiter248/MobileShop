using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders.CartItem;
using MainApi.Models.Orders;
using MainApi.Models.Products;
using MainApi.Models.User;

namespace MainApi.Mappers
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
                TotalPrice = addCartItemRequestDto.Price * addCartItemRequestDto.Quantity
            };
        }
    }
}