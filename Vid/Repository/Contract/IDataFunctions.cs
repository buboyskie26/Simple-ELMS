using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;

namespace Vid.Repository.Contract
{
    public interface IDataFunctions
    {
        Task UpdateUserCategoryEntityAsync(List<UserCategory> addList, List<UserCategory> deleteList);
        Task UpdateUserCategoryEntityAsync(List<UserCategory> addList);

        Task AddDeleteUserCategory(List<UserCategory> addCategory, List<UserCategory> deleteCategory);
    }
}
