using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.Models;
using Vid.ModelsTwo;
using Vid.Repository.Contract;

namespace Vid.Controllers
{
    [Authorize]
    public class CategoriesToUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataFunctions _dataFunctions;

        public CategoriesToUserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IDataFunctions dataFunctions)
        {
            _context = context;
            _userManager = userManager;
            _dataFunctions = dataFunctions;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserAsync(User).Result?.Id;
 

            var usersCurrentCategory = await CurrentCategory(userId);
            var ListofCourses = await CourseList();

            var model = new CategoryToChooseModel()
            {
                    UserId=userId,
                 Categories=ListofCourses,
                 CategoriesSelected= usersCurrentCategory
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string[] CategoriesSelected)
        {
            var userId = _userManager.GetUserAsync(User).Result?.Id;

            var usersToAdd = (from categoryId in CategoriesSelected
                              select new UserCategory()
                            {
                                UserId = userId,
                                CategoryId = int.Parse(categoryId)
                            }).ToList();

            var usersToDelete = (from users in _context.UserCategory
                                 where users.UserId==userId
                                 select new UserCategory()
                                 {
                                     Id = users.Id,
                                     UserId = userId,
                                     CategoryId = users.CategoryId
                                 }).ToList();

            await _dataFunctions.UpdateUserCategoryEntityAsync(usersToAdd, usersToDelete);

            return RedirectToAction("Index", "Home");
        }
        private async Task<List<Category>> CourseList()
        {
            var ListofCourses = await (from category in _context.Category
                                       join catItem in _context.CategoryItem
                                       on category.Id equals catItem.CategoryId
                                       join content in _context.Content
                                       on catItem.Id equals content.CategoryItem.Id
                                       select new Category()
                                       {
                                           Id = category.Id,
                                           ThumbnailImagePath = category.ThumbnailImagePath,
                                           Description = category.Description,
                                           Title = category.Title,

                                       }).Distinct().ToListAsync();
            return ListofCourses;
        }
        private async Task<List<Category>> CurrentCategory(string userId)
        {
            var usersCurrentCategory = await (from user in _context.UserCategory

                                              where user.UserId == userId
                                              select new Category()
                                              {
                                                  Id = user.CategoryId
                                              }).ToListAsync();
            return usersCurrentCategory;
        }
    }
}
