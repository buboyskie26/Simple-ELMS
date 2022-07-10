using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid.ViewModel.CategoryVM
{
    public class CategoryIndex
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ThumbnailImagePath { get; set; }

/*        public virtual ICollection<CategoryItem> CategoryItems { get; set; }

        public virtual ICollection<UserCategory> UserCategory { get; set; }*/
    }
}
