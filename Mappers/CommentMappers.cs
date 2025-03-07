using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Comment;
using MainApi.Models.Products;

namespace MainApi.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto()
            {
                Rating = comment.Rating,
                Text = comment.Text,
                ProductName = comment.Product.ProductName,
                UserName = comment.AppUser.UserName,
                CreatedTime = comment.CreatedTime,
            };
        }
        public static Comment ToCommentFromAdd(this AddCommentRequestDto addCommentRequestDto, int productId)
        {
            return new Comment()
            {
                Rating = addCommentRequestDto.Rating,
                Text = addCommentRequestDto.Text,
                ProductId = productId
            };
        }
        public static Comment ToCommentFromEdit(this EditCommentRequestDto editCommentRequestDto)
        {
            return new Comment()
            {
                Rating = editCommentRequestDto.Rating,
                Text = editCommentRequestDto.Text,
            };
        }
    }
}