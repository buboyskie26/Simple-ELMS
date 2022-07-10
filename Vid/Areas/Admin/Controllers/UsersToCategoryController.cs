using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Areas.Admin.Models;
using Vid.Areas.Admin.ModelsSamp;
using Vid.Entities;
using Vid.Models;
using Vid.Repository.Contract;

namespace Vid.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersToCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataFunctions _dataFunctions;

        public UsersToCategoryController(ApplicationDbContext context, IDataFunctions dataFunctions)
        {
            _context = context;
            _dataFunctions = dataFunctions;
        }    
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }



        public async Task<IActionResult> SaveSelectedUserFromCategory([Bind("CategoryId, UsersSelected")]
                    UsersModelSampList model)
        {

            List<UserCategory> userToAdd = null;

            if (model.UsersSelected != null)
            {
                userToAdd = (from u in model.UsersSelected
                             select new UserCategory
                             {
                                 UserId = u.UserId,
                                 CategoryId = model.CategoryId
                             }).ToList();
            }
            var userToDelete = await (from u in _context.UserCategory
                                      where u.CategoryId == model.CategoryId
                                      select new UserCategory()
                                      {
                                          Id=u.Id,
                                          UserId = u.UserId,
                                          CategoryId = model.CategoryId
                                      }).ToListAsync();

            model.Users = await GetAllUsers();

            await _dataFunctions.AddDeleteUserCategory(userToAdd, userToDelete);

            return PartialView("_UsersListViewPartial", model);

        }

        public async Task<IActionResult> UserListPerCategory(int categoryId)
        {
            var users = await GetAllUsers();

            var userHaveCategoryId = await GetAllUserWhoHaveCategory(categoryId);

            var model = new UsersModelSampList()
            {
                Users = users,
                UsersSelected = userHaveCategoryId
            };
            return PartialView("_UsersListViewPartial", model);
        }

        private async Task<List<UsersModelSamp>> GetAllUserWhoHaveCategory(int categoryId)
        {
            return await (from u in _context.UserCategory
                          where u.CategoryId == categoryId
                          select new UsersModelSamp()
                          {
                              UserId = u.UserId,

                          }).ToListAsync();
        }

        private async Task<List<UsersModelSamp>> GetAllUsers()
        {
            return await (from u in _context.Users
                          select new UsersModelSamp
                          {
                              UserId=u.Id,
                              FirstName = u.FirstName,
                              LastName=u.LastName,
                              UserName=u.UserName
                          }).ToListAsync();
        }
    }
}
