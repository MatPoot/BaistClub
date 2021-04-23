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
    public class DeleteTeeTimeModel : PageModel
    {
        public string TeeTimeID { get; set; }
        public string LoginID { get; set; }
        public string Msg { get; set; }
        public string ToDeleteTime { get; set; }
        public string ToDeleteDate { get; set; }

        public List<TeeTimes> TeeTimes { get; set; }
        public void OnGet()
        {
            LoginID = HttpContext.Session.GetString("MemberID");

            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand GetTeeTimes = new SqlCommand();
            GetTeeTimes.CommandType = CommandType.StoredProcedure;
            GetTeeTimes.Connection = MasterConnection;
            GetTeeTimes.CommandText = "GetTeeTime";

            SqlParameter ToDeleteTeeTime = sQLHelper.CreateParameterStringInt("@Member1_ID", 10, LoginID);

            SqlParameter[] parameterArray = { ToDeleteTeeTime };

            TeeTimes = sQLHelper.FetchTeeTimes(GetTeeTimes, parameterArray);
            foreach (var record in TeeTimes)
            {
                record.timetimemin = record.TimeTime % 60;
                record.TimeTime = record.TimeTime / 60;
                
            }
            if (TeeTimes == null)
            {
                Msg = "No current Appointments";
            }
        }
        public IActionResult OnPost()
        {
            string LoginStatus = HttpContext.Session.GetString("MemberID");
            

          
            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand DeleteTeeTimeCommand = new SqlCommand();
            DeleteTeeTimeCommand.CommandType = CommandType.StoredProcedure;
            DeleteTeeTimeCommand.Connection = MasterConnection;
            DeleteTeeTimeCommand.CommandText = "DeleteTeeTime";

            
            SqlParameter ToDeleteTeeTimeSQL = sQLHelper.CreateParameterStringInt("@TeeTimeID", 10, TeeTimeID);



            SqlParameter[] parameterArray = {ToDeleteTeeTimeSQL };

            sQLHelper.ServerCommand(DeleteTeeTimeCommand, parameterArray);

            return new RedirectToPageResult("DeleteTeeTime");

        }
    }
}
