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
    [BindProperties]
    public class ManageStandingTeeTimeRequestsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<TeeTimeRequests> Requesters { get; set; }





        // form values
        public string RequestedStartDate { get; set; }
        public string RequestedStartTime { get; set; }

        public string MemberID1 { get; set; }
        public string MemberID2 { get; set; }
        public string MemberID3 { get; set; }
        public string MemberID4 { get; set; }

        public string NumberOfCarts { get; set; }
        public string EmployeeName { get; set; }


        public string Msg { get; set; }

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
            string MemberClass = HttpContext.Session.GetString("MemberClass");
            string AccountLevel = HttpContext.Session.GetString("AccountLevel");

            string[] HoursMinSplit = RequestedStartTime.Split(':');
            int totalmin = (Int32.Parse(HoursMinSplit[0]) * 60);
            totalmin = totalmin + Int32.Parse(HoursMinSplit[1]);
            // total number of min, since the time value is only stored in min in the DB

            totalmin = totalmin / 8; // groups are sent out in 8min intervals , so reducing down to the nearest 8
            string stringmin = totalmin.ToString();
            


            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand MembershipRequestUpdate = new SqlCommand();
            MembershipRequestUpdate.CommandType = CommandType.StoredProcedure;
            MembershipRequestUpdate.Connection = MasterConnection;
            MembershipRequestUpdate.CommandText = "StandingRequestImplementation";




            SqlParameter RequestedStartDateParam = sQLHelper.CreateParameterStringInt("@TeeTimeStartDate", 10, RequestedStartDate);
            SqlParameter RequstedStartTimeParam = sQLHelper.CreateParameterStringInt("@TeeTimeStartTime", 3, stringmin);
            SqlParameter MainMember = sQLHelper.CreateParameterStringInt("@Member1_ID", 10, MemberID1);
            SqlParameter AddidtionalMember1 = sQLHelper.CreateParameterStringInt("@Member2_ID", 10, MemberID2);
            SqlParameter AdditionaMember2 = sQLHelper.CreateParameterStringInt("@Member3_ID", 10, MemberID3);
            SqlParameter AdditionalMember3 = sQLHelper.CreateParameterStringInt("@Member4_ID", 10, MemberID4);
            SqlParameter NumberOfGolfCarts = sQLHelper.CreateParameterStringInt("@NumberofCarts", 1, NumberOfCarts);
            
            

            SqlParameter[] parameterArray = { RequestedStartDateParam, RequstedStartTimeParam, MainMember, AddidtionalMember1, AdditionaMember2, AdditionalMember3, NumberOfGolfCarts};

            sQLHelper.ServerCommand(MembershipRequestUpdate, parameterArray);

            return Page();

        }
    }
}
