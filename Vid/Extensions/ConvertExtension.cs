using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Interfaces;

namespace Vid.Extensions
{
    public static class ConvertExtensions
    {
        public static List<SelectListItem> ConvertToSelectList<T>(this IEnumerable<T> collection,
            int selectedValues) where T : IPrimaryProperties
        {
            var obj = (from item in collection
                       select new SelectListItem()
                       {
                           Text = item.Title,
                           Value = item.Id.ToString(),
                           Selected = (item.Id == selectedValues)
                       }
                ).ToList();
            return obj;
        }
    }

}
