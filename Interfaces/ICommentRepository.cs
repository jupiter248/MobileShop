using MainApi.Models.Products;

namespace MainApi.Interfaces
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