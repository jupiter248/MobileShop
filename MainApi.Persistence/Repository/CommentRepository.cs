using System.Runtime.InteropServices;
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MainApi.Application.Interfaces.Repositories;

namespace MainApi.Persistence.Repository
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
        public async Task AddCommentAsync(Comment comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
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

        public async Task<List<Comment>> GetAllCommentAsync(string username)
        {
            List<Comment> comments = await _context.Comments.Include(u => u.AppUser).Include(p => p.Product).Where(c => c.AppUser.UserName == username).ToListAsync();
            return comments;
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

        public async Task<Comment?> RemoveCommentAsync(Comment comment)
        {
            _context.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

    }
}