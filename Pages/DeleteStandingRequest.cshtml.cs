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
    public class DeleteStandingRequestModel : PageModel
    {
        public List<TeeTimeRequests> Requesters { get; set; }
        public string Msg { get; set; }

        public string ToDeleteDate { get; set; }
        public string ToDeleteTime { get; set; }
        public void OnGet()
        {
            string LoginStatus = HttpContext.Session.GetString("MemberID");
            string MemberClass = HttpContext.Session.GetString("MemberClass");
            string AccountLevel = HttpContext.Session.GetString("AccountLevel");

            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand GetTeeTimeRequests = new SqlCommand();
            GetTeeTimeRequests.CommandType = CommandType.StoredProcedure;
            GetTeeTimeRequests.Connection = MasterConnection;
            GetTeeTimeRequests.CommandText = "GetStandingTeeTimeRequests";

            Requesters = sQLHelper.FetchStandingRequests(GetTeeTimeRequests);

            if (Requesters == null)
            {
                Msg = "No current Items";
            }
            else
            {
                foreach (var records in Requesters)
                {
                    records.TimeTime = records.TimeTime / 60;
                }
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
            DeleteTeeTimeCommand.CommandText = "DeleteTeeTimeRequest";

            SqlParameter ToDeleteDateSQL = sQLHelper.CreateParameterStringInt("@TeeTimeStartDate", 20, ToDeleteDate);
            SqlParameter ToDeleteTeeTimeSQL = sQLHelper.CreateParameterStringInt("@TeeTimeStartTime", 20, ToDeleteTime);



            SqlParameter[] parameterArray = { ToDeleteDateSQL, ToDeleteTeeTimeSQL };

            sQLHelper.ServerCommand(DeleteTeeTimeCommand, parameterArray);

            return Page();

        }
    }
}
