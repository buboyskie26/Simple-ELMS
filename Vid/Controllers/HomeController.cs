using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.Models;
using Vid.ModelsTwo;

namespace Vid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        private async Task<List<Category>> GetCategoriesThatHaveContent()
        {
            var obj = await (from category in _context.Category
                             join catItem in _context.CategoryItem
                             on category.Id equals catItem.CategoryId
                             join content in _context.Content
                             on catItem.Id equals content.CategoryItem.Id
                             select new Category()
                             {
                                 Id =category.Id,
                                 Title = category.Title,
                                 Description = category.Description,
                                 ThumbnailImagePath = category.ThumbnailImagePath
                             }).Distinct().ToListAsync();

            return obj;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<CategoryItemDetailsModel> categoryDetails = null;
            IEnumerable<GroupedCategoryItemsByCategoryModel> groupCategoryItems = null;

            var categoryDetailModel = new CategoryDetailsModel();

            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    categoryDetails = await GetCategoryItemDetailsForUser(user.Id);
                    groupCategoryItems = GetGroupedCategoryItemsByCategory(categoryDetails);

                    categoryDetailModel.GroupedCategoryItemsByCategoryModels = groupCategoryItems;
                }
            }
            else
            {
                var categ = await GetCategoriesThatHaveContent();

                categoryDetailModel.Categories = categ;
            }


            return View(categoryDetailModel);
        }

        private IEnumerable<GroupedCategoryItemsByCategoryModel>
            GetGroupedCategoryItemsByCategory(IEnumerable<CategoryItemDetailsModel> categoryDetails)
        {
            var obj = from item in categoryDetails
                      group item by item.CategoryId into g
                      select new GroupedCategoryItemsByCategoryModel
                      {
                          Id = g.Key,
                          Title = g.Select(q => q.CategoryTitle).FirstOrDefault(),
                          Items = g
                      };
            // qwe
            return obj;
        }

        private async Task<IEnumerable<CategoryItemDetailsModel>> GetCategoryItemDetailsForUser(string id)
        {
            // If there`s no content, it wont query the data
            var obj = await (from category in _context.Category
                             join catItem in _context.CategoryItem
                             on category.Id equals catItem.CategoryId
                             join content in _context.Content
                             on catItem.Id equals content.CategoryItem.Id
                             join mediaType in _context.MediaType
                             on catItem.MediaTypeId equals mediaType.Id
                             join user in _context.UserCategory
                             on category.Id equals user.CategoryId
                             where user.UserId == id
                             select new CategoryItemDetailsModel()
                             {
                                 CategoryId = category.Id,
                                 CategoryItemDescription = catItem.Description,
                                 CategoryItemId = catItem.Id,
                                 CategoryItemTitle = catItem.Title,
                                 CategoryTitle = category.Title,
                                 MediaImagePath = mediaType.ThumbnailImagePath
                             }).ToListAsync();
            return obj;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
