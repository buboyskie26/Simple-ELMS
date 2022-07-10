using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid.ModelsTwo
{
    public class CategoryItemsDetail
    {
        public int CategoryId { get; set; }
        public int CategoryItemsId { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryItemsTitle { get; set; }
        public string CategoryItemsDescription{ get; set; }
        public string VideoImage { get; set; }
    }
}
