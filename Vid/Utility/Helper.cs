using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid.Utility
{
    public class Helper
    {
        public const string Admin = "Admin";
        public static string User = "User";


        public static List<SelectListItem> GetRolesForDropDown()
        {
            
            return new List<SelectListItem>
                {

                    new SelectListItem{Value=Helper.Admin,Text=Helper.Admin},
                    new SelectListItem{Value=Helper.User,Text=Helper.User},
                };
        }
    }

}
