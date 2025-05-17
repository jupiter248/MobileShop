using MainApi.Domain.Models.Products;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentAsync(string username);
        Task<Comment?> GetCommentByIdAsync(int commentId);
        Task AddCommentAsync(Comment comment);
        Task<Comment?> EditCommentAsync(int commentId, Comment comment, string username);
        Task<Comment?> RemoveCommentAsync(Comment comment);
    }
}