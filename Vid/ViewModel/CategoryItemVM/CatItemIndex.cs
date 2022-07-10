using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid.ViewModel.CategoryItemVM
{
    public class CatItemIndex
    {
        private DateTime _releaseDate = DateTime.MinValue;

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
 
        public int MediaTypeId { get; set; }

        public ICollection<SelectListItem> MediaTypes { get; set; }


        public DateTime DateTimeItemReleased
        {
            get
            {
                return (_releaseDate == DateTime.MinValue) ? DateTime.Now : _releaseDate;
            }
            set
            {
                _releaseDate = value;
            }

        }

        public int ContentId { get; set; }
    }
    public class CatListItemIndex
    {
        public IEnumerable<CatItemIndex> CatList { get; set; }
        public int CategoryId { get; set; }
    }
}
