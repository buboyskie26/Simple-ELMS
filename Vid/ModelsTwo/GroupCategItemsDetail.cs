using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid.ModelsTwo
{
    public class GroupCategItemsDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IGrouping<int, CategoryItemsDetail> GroupCategItems { get; set; }
    }
}
