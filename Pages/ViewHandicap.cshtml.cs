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
    [BindProperties(SupportsGet = true)]

    public class ViewHandicapModel : PageModel
    {
        public string Msg { get; set; }

        public List<Handicap> HandicapList { get; set; }
        public void OnGet()
        {
            string LoginStatus = HttpContext.Session.GetString("MemberID");
            string MemberClass = HttpContext.Session.GetString("MemberClass");
            string AccountLevel = HttpContext.Session.GetString("AccountLevel");

            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand GetHandicapRecords = new SqlCommand();
            GetHandicapRecords.CommandType = CommandType.StoredProcedure;
            GetHandicapRecords.Connection = MasterConnection;
            GetHandicapRecords.CommandText = "GetHandicapRecord";

            SqlParameter MemberIDParam = sQLHelper.CreateParameterStringInt("@MemberID", 10, LoginStatus);
            SqlParameter[] parameterArray = { MemberIDParam };

            HandicapList = sQLHelper.FetchHandicaps(GetHandicapRecords, parameterArray);

            if (HandicapList == null)
            {
                Msg = "No current Items";
            }
           
        }
    }
}
