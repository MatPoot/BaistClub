using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaistClub.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {

        public string Msg { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string LoginStatus = HttpContext.Session.GetString("MemberID");
            
           if (LoginStatus == null)
            {
                Msg = "Not Logged In";
            }
           else
            {
                if (LoginStatus.Length >= 1)
                {
                    Msg = "Logged In";
                }
                else
                {
                    Msg = "Not Logged In";
                }
            }
           
        }
    }
}
