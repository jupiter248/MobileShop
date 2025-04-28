using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Persistence.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category?> AddCategoryAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>?> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.Include(p => p.Products).ThenInclude(p => p.Images).ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.Categories.Include(p => p.Products).ThenInclude(p => p.Images).FirstOrDefaultAsync(c => c.Id == categoryId);
            return category;
        }

        public async Task<Category?> RemoveCategoryAsync(int categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(Category categoryModel, int categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
            if (category != null)
            {
                category.CategoryName = categoryModel.CategoryName;
                category.Description = categoryModel.Description;
                await _context.SaveChangesAsync();
            }
            return category;
        }
    }
}