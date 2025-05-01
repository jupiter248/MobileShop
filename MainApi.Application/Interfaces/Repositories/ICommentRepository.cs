using MainApi.Domain.Models.Products;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>?> GetAllCommentAsync(string username);
        Task<Comment?> GetCommentByIdAsync(int commentId);
        Task<Comment?> AddCommentAsync(Comment comment, string username);
        Task<Comment?> EditCommentAsync(int commentId, Comment comment, string username);
        Task<Comment?> RemoveCommentAsync(int commentId);
    }
}