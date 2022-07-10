using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid.ViewModel.ContentVM
{
    public class ContentIndex
    {
        public int Id { get; set; }

   
        public string Title { get; set; }
 
        public string HTMLContent { get; set; }
 
        public string VideoLink { get; set; }
 
        public int CatItemId { get; set; }

        public int CategoryId { get; set; }
    }
    public class ContentListIndex
    {
        public IEnumerable<ContentIndex> ListContent { get; set; }
    }
}
