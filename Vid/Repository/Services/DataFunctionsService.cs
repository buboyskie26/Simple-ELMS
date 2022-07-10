using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.Models;
using Vid.Repository.Contract;

namespace Vid.Repository.Services
{
    public class DataFunctionsService : IDataFunctions
    {
        private readonly ApplicationDbContext _context;

        public DataFunctionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDeleteUserCategory(List<UserCategory> addCategory, List<UserCategory> deleteCategory)
        {


            using (var dbContent = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.UserCategory.RemoveRange(deleteCategory);
                    await _context.SaveChangesAsync();

                    if (addCategory != null)
                    {
                        await _context.UserCategory.AddRangeAsync(addCategory);
                        await _context.SaveChangesAsync();
                    }

                    await dbContent.CommitAsync();
                }
                catch (Exception)
                {
                    await dbContent.DisposeAsync();
                }
            }
           
        }

        public async Task UpdateUserCategoryEntityAsync(List<UserCategory> AddList,
            List<UserCategory> DeleteList)
        {

            using (var dbContent = await _context.Database.BeginTransactionAsync())
            {
                try

                {
                    _context.UserCategory.RemoveRange(DeleteList);
                    await _context.SaveChangesAsync();
                    if (AddList != null)
                    {
                        _context.UserCategory.AddRange(AddList);
                        await _context.SaveChangesAsync();
                    }

                    await dbContent.CommitAsync();
                }

                catch (Exception)
                {
                    await dbContent.DisposeAsync();
                }
            };
        }

        public async Task UpdateUserCategoryEntityAsync(List<UserCategory> AddList)
        {
            using (var dbContent = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                
                    if (AddList != null)
                    {
                        await _context.UserCategory.AddRangeAsync(AddList);
                        await _context.SaveChangesAsync();
                    }

                    await dbContent.CommitAsync();
                }
                catch (Exception)
                {
                    await dbContent.DisposeAsync();
                }
            };

             
        }
    }
}
