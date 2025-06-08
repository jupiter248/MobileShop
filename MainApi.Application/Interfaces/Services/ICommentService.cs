using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Comment;




namespace MainApi.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllUserCommentsAsync(string username , int pageNumber = 1, int pageSize = 20);
        Task<CommentDto> GetCommentByIdAsync(int commentId);
        Task<CommentDto> AddCommentAsync(int productId, AddCommentRequestDto addCommentRequestDto, string username);
        Task UpdateCommentAsync(int commentId, EditCommentRequestDto editCommentRequestDto, string username);
        Task DeleteCommentAsync(int commentId, string username);
    }
}