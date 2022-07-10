using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.ViewModel.ContentVM;

namespace Vid.Repository.Contract
{
    public interface IContent
    {
        Task AddContent(ContentIndex category);
        Task UpdateContent(ContentIndex category);
        Task<IEnumerable<Content>> GetAllContent();
        Task<Content> FindContentyId(int id);
    }
}
