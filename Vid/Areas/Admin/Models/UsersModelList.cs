using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid.Areas.Admin.Models
{
    public class UsersModelList
    {
        public int CategoryId { get; set; }
        public ICollection<UsersModel> Users { get; set; }
        public ICollection<UsersModel> UsersSelected { get; set; }
    }
}
