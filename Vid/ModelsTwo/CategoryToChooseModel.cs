using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;

namespace Vid.ModelsTwo
{
    public class CategoryToChooseModel
    {
        public string UserId { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Category> CategoriesSelected { get; set; }

    }
}
