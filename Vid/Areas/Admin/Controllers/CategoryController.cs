using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Repository.Contract;
using Vid.ViewModel.CategoryVM;

namespace Vid.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }
        public async Task<IActionResult> Index()
        {
            var obj = await _category.GetAllCategories();
            return View(obj);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryIndex categoryIndex)
        {
            
            await _category.AddCategory(categoryIndex);
            return RedirectToAction("Index");
        }
    }
}
