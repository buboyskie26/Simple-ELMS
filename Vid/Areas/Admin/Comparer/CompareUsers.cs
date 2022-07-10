using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Vid.Areas.Admin.Models;
using Vid.Areas.Admin.ModelsSamp;

namespace Vid.Areas.Admin.Comparer
{
    public class CompareUsers : IEqualityComparer<UsersModelSamp>
    { 
        public bool Equals(UsersModelSamp x, UsersModelSamp y)
        {
            if (y == null) return false;
            return x.UserId == y.UserId;
        }
        public int GetHashCode([DisallowNull] UsersModelSamp obj)
        {
            return obj.UserId.GetHashCode();
        }

    }
}
 
