using MainApi.Models.Products;

namespace MainApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> GetCommentByIdAsync(int commentId);
        Task<Comment?> AddCommentAsync(Comment comment);
        Task<Comment?> EditCommentAsync(int commentId, Comment comment);
        Task<Comment?> RemoveCommentAsync(int commentId);
    }
}