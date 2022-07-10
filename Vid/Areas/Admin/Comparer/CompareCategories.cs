using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;

namespace Vid.Areas.Admin.Comparer
{
    public class CompareCategories:IEqualityComparer<Category>
    {

        public bool Equals(Category x, Category y)
        {

            if (y == null) return false;

            return x.Id == y.Id;
        }
        public int GetHashCode([DisallowNull] Category obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
