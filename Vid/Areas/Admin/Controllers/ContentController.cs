using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Models;
using Vid.Repository.Contract;
using Vid.ViewModel.ContentVM;

namespace Vid.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        private readonly IContent _content;
        private readonly ApplicationDbContext _context;
        public ContentController(IContent categoryItem, ApplicationDbContext applicationDbContext)
        {
            _content = categoryItem;
            _context = applicationDbContext;
        }
        public IActionResult Create(int categoryItemId, int categoryId)
        {
            var obj = new ContentIndex
            {
                CategoryId = categoryId,
                CatItemId = categoryItemId
            };

            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContentIndex contentIndex)
        {

            await _content.AddContent(contentIndex);
            return RedirectToAction("Index","CategoryItem", new { categoryId=contentIndex.CategoryId });
        }
        public async Task<IActionResult> Edit(int categoryItemId, int categoryId)
        {
            var obj = await _context.Content.FirstOrDefaultAsync(q => q.CategoryItem.Id == categoryItemId);


            obj.CategoryId = categoryId;
            obj.CatItemId = categoryItemId;

            var model = new ContentIndex
            {
                Id = obj.Id,
                CategoryId = obj.CategoryId,
                CatItemId = obj.CatItemId
            };
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ContentIndex contentIndex)
        {

            await _content.UpdateContent(contentIndex);
            return RedirectToAction("Index", "CategoryItem",
                new { categoryId = contentIndex.CategoryId });
        }



    }
}
