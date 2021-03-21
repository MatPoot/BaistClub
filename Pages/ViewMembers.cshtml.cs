using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BaistClub.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClub.Pages
{
    [BindProperties(SupportsGet =true)]
    public class ViewMembersModel : PageModel
    {
        public string LoginID { get; set; }
        public string AccountLevel { get; set; }

        public string Msg { get; set; }

        public List<Members> Members { get; set; }
        public void OnGet()
        {
            LoginID = HttpContext.Session.GetString("MemberID");
            AccountLevel = HttpContext.Session.GetString("AccountLevel");


            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand GetMembers = new SqlCommand();
            GetMembers.CommandType = CommandType.StoredProcedure;
            GetMembers.Connection = MasterConnection;
            GetMembers.CommandText = "GetMembers";
         
            Members = sQLHelper.FetchMembers(GetMembers);


            if (Members == null)
            {
                Msg = "No current Items";
            }
        }
    }
}
