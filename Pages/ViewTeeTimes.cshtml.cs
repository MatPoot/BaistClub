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
    public class ViewTeeTimesModel : PageModel
       
    {
        public string LoginID { get; set; }
        public string Msg { get; set; }
        public List<TeeTimes> TeeTimesList { get; set; }
        public void OnGet()
        {

            LoginID = HttpContext.Session.GetString("MemberID");

            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand GetTeeTimes = new SqlCommand();
            GetTeeTimes.CommandType = CommandType.StoredProcedure;
            GetTeeTimes.Connection = MasterConnection;
            GetTeeTimes.CommandText = "GetTeeTime";

            
            SqlParameter MainMember = sQLHelper.CreateParameterStringInt("@Member1_ID", 10, LoginID);
            SqlParameter[] parameterArray = { MainMember };
            TeeTimesList = sQLHelper.FetchTeeTimes(GetTeeTimes, parameterArray);
            if (TeeTimesList == null)
            {
                Msg = "No current Items";
            }
            foreach (var records in TeeTimesList)
            {
                records.StringStartDate = records.StartDate.ToString("dd/MM/yyyy");
                records.timetimemin = records.TimeTime % 60;
                records.TimeTime = records.TimeTime / 60;
                
            }
        }
    }
}
