using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        [Required]

        public string TeeTimeStartDate { get; set; }
        [Required]
        public string TeeTiemStartTime { get; set; }
        [Required]
        public string Member1_ID { get; set; }
        [Required]
        public string HoleNumber { get; set; }
        
        public string GolfCourse { get; set; }
        [Required]
        public string CourseRating { get; set; }
        [Required]
        public string SlopeRating { get; set; }
        [Required]
        public string Par { get; set; }
        [Required]
        public string Red { get; set; }
        [Required]
        public string White { get; set; }
        [Required]
        public string Blue { get; set; }
        [Required]
        public string StrokeIndexMan { get; set; }
        [Required]
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
            SqlParameter WhiteSQL = sQLHelper.CreateParameterStringInt("@White", 20, White);
            SqlParameter BlueSQL = sQLHelper.CreateParameterStringInt("@Blue", 20, Blue);
            SqlParameter SIMSQL = sQLHelper.CreateParameterStringInt("@StrokeIndexMen", 20, StrokeIndexMan);
            SqlParameter SIWSQL = sQLHelper.CreateParameterStringInt("@StrokeIndexWomen", 20, StrokeIndexWoman);



            SqlParameter[] parameterArray = { MemberSQL,HoleSQL,DateSQL,TimeSQL,ParSQL,RedSQL,WhiteSQL,BlueSQL,SIMSQL,SIWSQL };

            sQLHelper.ServerCommand(SendScores, parameterArray);

           
        }
    }
}
