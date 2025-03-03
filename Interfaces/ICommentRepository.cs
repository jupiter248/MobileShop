using MainApi.Models.Products;

namespace MainApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> GetCommentByIdAsync();
        Task<Comment?> AddCommentAsync(Comment comment);
        Task<Comment?> EditCommentAsycn(int commentId ,Comment comment);
        Task<Comment?> RemoveCommentAsync(int commentId);
    }
}