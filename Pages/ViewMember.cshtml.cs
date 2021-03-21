using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClub.Pages
{
    [BindProperties(SupportsGet =true)]
    public class ViewMemberModel : PageModel
    {
        public string LoginID { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
       

        public void OnGet()
        {
            LoginID = HttpContext.Session.GetString("MemberID");
            Name = HttpContext.Session.GetString("MemberName");
            Class = HttpContext.Session.GetString("MemberClass");
            
        }
    }
}
