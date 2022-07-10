using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.ViewModel.CategoryItemVM;

namespace Vid.Repository.Contract
{
    public interface ICategoryItem
    {
        Task AddCategoryItem(CatItemIndex category);
        Task UpdateCategoryItem(CatItemIndex category);
        Task<CategoryItem> FindCategoryIdItem(int id);
        Task<IEnumerable<CategoryItem>> GetAllItemCategories();

        IEnumerable<CategoryItem> GetContentCatItemId(CategoryItem item,int categId);
    }
}
