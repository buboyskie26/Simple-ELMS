using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.Models;
using Vid.Repository.Contract;
using Vid.ViewModel.CategoryVM;

namespace Vid.Repository.Services
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddCategory(CategoryIndex q)
        {
            var obj = new Category()
            {
                ThumbnailImagePath=q.ThumbnailImagePath,
                Description=q.Description,
                Title=q.Title,
                
            };
            await _context.Category.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> FindCategoryId(int id)
        {

            return await _context.Category
                .Include(q => q.UserCategory)
                .Include(q => q.CategoryItems)
                .FirstOrDefaultAsync(q => q.Id == id); 
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
           return await _context.Category
                .Include(q=> q.UserCategory)
                .Include(q=> q.CategoryItems)
                .ToListAsync();
        }

        public Task UpdateCategory(CategoryIndex category)
        {
            throw new NotImplementedException();
        }
    }
}
