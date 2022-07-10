using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.Models;
using Vid.Repository.Contract;
using Vid.ViewModel.ContentVM;

namespace Vid.Repository.Services
{
    public class ContentService : IContent
    {
        private readonly ApplicationDbContext _context;
        public ContentService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddContent(ContentIndex w)
        {
            var obj = new Content()
            {
                Title=w.Title,
                CategoryId=w.CategoryId,
                CatItemId=w.CatItemId,
                HTMLContent=w.HTMLContent,
                VideoLink=w.VideoLink,
                
            };
            obj.CategoryItem = await _context.CategoryItem.FindAsync(w.CatItemId);

            await _context.Content.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Content> FindContentyId(int id)
        {
            return await _context.Content.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Content>> GetAllContent()
        {
            return await _context.Content.ToListAsync();
        }

        public async Task UpdateContent(ContentIndex w)
        {
            var obj = await FindContentyId(w.Id);

            obj.Title = w.Title;
            obj.VideoLink = w.VideoLink;
            obj.HTMLContent = w.HTMLContent;


            _context.Content.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
