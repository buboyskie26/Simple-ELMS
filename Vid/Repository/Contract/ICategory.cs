using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.ViewModel.CategoryVM;

namespace Vid.Repository.Contract
{
    public interface ICategory
    {
        Task AddCategory(CategoryIndex category);
        Task UpdateCategory(CategoryIndex category);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> FindCategoryId(int id);

    }
}
