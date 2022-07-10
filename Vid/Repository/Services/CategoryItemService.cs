using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.Models;
using Vid.Repository.Contract;
using Vid.ViewModel.CategoryItemVM;

namespace Vid.Repository.Services
{
    public class CategoryItemService : ICategoryItem
    {

        private readonly ApplicationDbContext _context;
        public CategoryItemService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddCategoryItem(CatItemIndex q)
        {
            var obj = new CategoryItem()
            {
                CategoryId = q.CategoryId,
                ContentId = q.ContentId,
                DateTimeItemReleased = q.DateTimeItemReleased,
                Description = q.Description,
                MediaTypeId = q.MediaTypeId,
                Title = q.Title
            };

            await _context.CategoryItem.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryItem> FindCategoryIdItem(int id)
        {
            return await _context.CategoryItem.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<CategoryItem>> GetAllItemCategories()
        {

            var catItem = await _context.CategoryItem
                .ToListAsync();

            return catItem;
        }

        public IEnumerable<CategoryItem> GetContentCatItemId(CategoryItem item, int categoryId)
        {
            var content = _context.Content
            .Where(q => q.CategoryItem.Id == item.Id)
            .FirstOrDefault();

            var catItem =  _context.CategoryItem
                .Where(q=> q.ContentId == content.Id && q.CategoryId == categoryId)
               .ToList();


            return catItem;
        }

        public async Task UpdateCategoryItem(CatItemIndex w)
        {
   
            var item = await FindCategoryIdItem(w.Id);

            item.CategoryId = w.CategoryId;
            item.Description = w.Description;
            item.Title = w.Title;
            item.MediaTypeId = w.MediaTypeId;
            item.DateTimeItemReleased = w.DateTimeItemReleased;

            _context.CategoryItem.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
