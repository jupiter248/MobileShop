using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Products;

namespace MainApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Comment?> AddCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> EditCommentAsycn(int commentId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> GetCommentByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> RemoveCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

    }
}