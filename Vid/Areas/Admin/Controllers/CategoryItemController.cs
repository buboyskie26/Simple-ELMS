using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.Extensions;
using Vid.Models;
using Vid.Repository.Contract;
using Vid.ViewModel.CategoryItemVM;

namespace Vid.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryItemController : Controller
    {
        private readonly ICategoryItem _categoryItem;
        private readonly ApplicationDbContext _context;
        public CategoryItemController(ICategoryItem categoryItem, ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _categoryItem = categoryItem;
        }

        public async Task<IActionResult> Index(int categoryId)
        {
            /*var ojj = await _categoryItem.GetAllItemCategories();
            var hum = _categoryItem.GetContentCatItemId(ojj, categoryId);*/
            var samp = await (from catitem in _context.CategoryItem
                             join content in _context.Content
                             on catitem.Id equals content.CategoryItem.Id
                             into gf
                             from getContentId in gf.DefaultIfEmpty()
                             where catitem.CategoryId == categoryId
                             select new CatItemIndex()
                             {
                                 CategoryId = categoryId,
                                 ContentId = (getContentId == null) ? 0 : getContentId.Id,
                                 MediaTypeId = catitem.MediaTypeId,
                                 Description = catitem.Description,
                                 Id = catitem.Id,
                                 DateTimeItemReleased = catitem.DateTimeItemReleased,
                                 Title = catitem.Title,
                             }).ToListAsync();

/*            var item = await _categoryItem.GetAllItemCategories();
            item = item.Where(q=> q.CategoryId == categoryId).ToList();

            var obj = item.Select(q => new CatItemIndex()
            {
                CategoryId= categoryId,
                ContentId =q.ContentId,
                MediaTypeId=q.MediaTypeId,
                Description=q.Description,
                Id=q.Id,
                DateTimeItemReleased=q.DateTimeItemReleased,
                Title=q.Title
            });
*/
            var model = new CatListItemIndex()
            {
                CatList = samp,
                CategoryId = categoryId
            };

            return View(model);
        }
        public async Task<IActionResult> Create(int categoryId)
        {
            List<MediaType> mediaTypes = await _context.MediaType.ToListAsync();
             
            var obj = new CatItemIndex()
            {
                CategoryId = categoryId,
                MediaTypes = mediaTypes.ConvertToSelectList(0)

            };
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CatItemIndex catItemIndex)
        {
            await _categoryItem.AddCategoryItem(catItemIndex);
            return RedirectToAction("Index", new { categoryId = catItemIndex.CategoryId });
        }


        public async Task<IActionResult> Edit(int id)
        {
            List<MediaType> mediaTypes = await _context.MediaType.ToListAsync();
            var item = await _categoryItem.FindCategoryIdItem(id);

            if (item == null) return NotFound();

            item.MediaTypes = mediaTypes.ConvertToSelectList(item.MediaTypeId);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CatItemIndex catItemIndex)
        {
            await _categoryItem.UpdateCategoryItem(catItemIndex);
            return RedirectToAction("Index", new { categoryId = catItemIndex.CategoryId });
        }
    }
}

