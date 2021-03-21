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

    public class RecordScoresModel : PageModel
    {
        public string Msg { get; set; }


        

        public string TeeTimeStartDate { get; set; }
        public string TeeTiemStartTime { get; set; }
        public string Member1_ID { get; set; }
        public string HoleNumber { get; set; }
        public string GolfCourse { get; set; }
        public string CourseRating { get; set; }
        public string SlopeRating { get; set; }
        public string Par { get; set; }
        public string Red { get; set; }
        public string white { get; set; }
        public string blue { get; set; }
        public string StrokeIndexMan { get; set; }
        public string StrokeIndexWoman { get; set; }


        public void OnGet()
        {
            string LoginStatus = HttpContext.Session.GetString("MemberID");
        }
        public void OnPost()
        {
            string LoginStatus = HttpContext.Session.GetString("MemberID");

            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand SendScores = new SqlCommand();
            SendScores.CommandType = CommandType.StoredProcedure;
            SendScores.Connection = MasterConnection;
            SendScores.CommandText = "EnterScore";

            SqlParameter MemberSQL = sQLHelper.CreateParameterStringInt("@Member1_ID", 20, LoginStatus);
            SqlParameter HoleSQL = sQLHelper.CreateParameterStringInt("@HoleNumber", 20, HoleNumber);
            SqlParameter DateSQL = sQLHelper.CreateParameterStringInt("@TeeTimeStartDate", 20, TeeTimeStartDate);
            SqlParameter TimeSQL = sQLHelper.CreateParameterStringInt("@TeeTimeStartTime", 20, TeeTiemStartTime);
            SqlParameter ParSQL = sQLHelper.CreateParameterStringInt("@Par", 20, Par);
            SqlParameter RedSQL = sQLHelper.CreateParameterStringInt("@Red", 20, Red);
            SqlParameter WhiteSQL = sQLHelper.CreateParameterStringInt("@White", 20, white);
            SqlParameter BlueSQL = sQLHelper.CreateParameterStringInt("@Blue", 20, blue);
            SqlParameter SIMSQL = sQLHelper.CreateParameterStringInt("@StrokeIndexMen", 20, StrokeIndexMan);
            SqlParameter SIWSQL = sQLHelper.CreateParameterStringInt("@StrokeIndexWomen", 20, StrokeIndexWoman);



            SqlParameter[] parameterArray = { MemberSQL,HoleSQL,DateSQL,TimeSQL,ParSQL,RedSQL,WhiteSQL,BlueSQL,SIMSQL,SIWSQL };

            sQLHelper.ServerCommand(SendScores, parameterArray);

           
        }
    }
}
