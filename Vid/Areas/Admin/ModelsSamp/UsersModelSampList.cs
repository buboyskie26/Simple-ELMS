using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid.Areas.Admin.ModelsSamp
{
    public class UsersModelSampList
    {
        public int CategoryId { get; set; }
        public ICollection<UsersModelSamp> Users { get; set; }
        public ICollection<UsersModelSamp> UsersSelected { get; set; }
    }
}
