using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Comment;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Infrastructure.Services.Internal
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;
        public CommentService(ICommentRepository commentRepository, IProductRepository productRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        public async Task<CommentDto> AddCommentAsync(int productId, AddCommentRequestDto addCommentRequestDto, string username)
        {
            Product? product = await _productRepository.GetProductByIdAsync(productId) ?? throw new KeyNotFoundException("Product not found");
            AppUser? appUser = await _userManager.FindByNameAsync(username) ?? throw new KeyNotFoundException("User not found");

            Comment comment = addCommentRequestDto.ToCommentFromAdd(productId);
            comment.Product = product;
            comment.AppUser = appUser;
            await _commentRepository.AddCommentAsync(comment);

            return comment.ToCommentDto();
        }

        public async Task DeleteCommentAsync(int commentId, string username)
        {
            Comment? comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new KeyNotFoundException("Comment not found");
            if (comment.AppUser?.UserName != username) throw new KeyNotFoundException("Comment with this username not found");
            await _commentRepository.RemoveCommentAsync(comment);
        }

        public async Task<List<CommentDto>> GetAllUserCommentsAsync(string username)
        {
            List<Comment> comments = await _commentRepository.GetAllCommentAsync(username);
            List<CommentDto>? commentDtos = comments.Select(c => c.ToCommentDto()).ToList();
            return commentDtos;
        }

        public async Task<CommentDto> GetCommentByIdAsync(int commentId)
        {
            Comment? comment = await _commentRepository.GetCommentByIdAsync(commentId);
            if (comment == null) throw new KeyNotFoundException("Comment not found");
            return comment.ToCommentDto();
        }

        public async Task UpdateCommentAsync(int commentId, EditCommentRequestDto editCommentRequestDto, string username)
        {
            Comment? commentModel = await _commentRepository.EditCommentAsync(commentId, editCommentRequestDto.ToCommentFromEdit(), username)
            ?? throw new KeyNotFoundException("Comment not found");

        }
    }
}