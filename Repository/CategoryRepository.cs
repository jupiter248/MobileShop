using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Interfaces;
using MainApi.Models;

namespace MainApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<Category> AddCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Category> RemoveCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategoryAsync(Category category, int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}