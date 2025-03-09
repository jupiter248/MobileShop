using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models;
using MainApi.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CommentRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Comment?> AddCommentAsync(Comment comment)
        {
            var commentExists = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);
            if (commentExists == null)
            {
                await _context.AddAsync(comment);
                await _context.SaveChangesAsync();
            }
            else
            {
                return null;
            }
            return comment;
        }

        public async Task<Comment?> EditCommentAsync(int commentId, Comment commentModel, string username)
        {
            Comment? comment = await _context.Comments.Include(u => u.AppUser).FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment != null && comment.AppUser?.UserName == username)
            {
                comment.Text = commentModel.Text;
                comment.Rating = commentModel.Rating;
                await _context.SaveChangesAsync();
                return comment;
            }
            return null;
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            Comment? comment = await _context.Comments.Include(u => u.AppUser).Include(p => p.Product).FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null)
            {
                return null;
            }
            return comment;
        }

        public async Task<Comment?> RemoveCommentAsync(int commentId)
        {
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment != null)
            {
                _context.Remove(comment);
                await _context.SaveChangesAsync();
            }
            return null;
        }

    }
}