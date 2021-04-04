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
    public class StandingTeeTimeRequestModel : PageModel
    {
        [Required]
        public string RequestedStartDate { get; set; }
        [Required]
        public string RequestedEndDate { get; set; }
        [Required]
        public string RequestedStartTime { get; set; }
        [Required, MinLength(1)]
        public string MemberID1 { get; set; }
        [Required, MinLength(1)]
        public string MemberID2 { get; set; }
        [Required, MinLength(1)]
        public string MemberID3 { get; set; }
        [Required, MinLength(1)]
        public string MemberID4 { get; set; }
        [Required, MinLength(1)]

        public string NumberOfCarts { get; set; }



        public void OnGet()
        {
            string LoginStatus = HttpContext.Session.GetString("MemberID");
            string MemberClass = HttpContext.Session.GetString("MemberClass");
            string AccountLevel = HttpContext.Session.GetString("AccountLevel");

            // change later so this page is not available to non gold members
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

            int outputmin = totalmin / 8; // groups are sent out in 8min intervals , so reducing down to the nearest 8

            MemberID1 = LoginStatus;


            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand MembershipRequestUpdate = new SqlCommand();
            MembershipRequestUpdate.CommandType = CommandType.StoredProcedure;
            MembershipRequestUpdate.Connection = MasterConnection;
            MembershipRequestUpdate.CommandText = "BookTeeTimeRequest";




            SqlParameter RequestedStartDateParam = sQLHelper.CreateParameterStringInt("@RequestedTeeTimeStartDate", 10, RequestedStartDate);
            SqlParameter RequestedEndDateParam = sQLHelper.CreateParameterStringInt("@RequestedTeeTimeEndDate", 10, RequestedStartDate);
            SqlParameter RequstedStartTimeParam = sQLHelper.CreateParameterStringInt("@RequestedTeeTimeTime", 1, outputmin.ToString());
            SqlParameter MainMember = sQLHelper.CreateParameterStringInt("@Member1_ID", 10, LoginStatus);
            SqlParameter AddidtionalMember1 = sQLHelper.CreateParameterStringInt("@Member2_ID", 10, MemberID2);
            SqlParameter AdditionaMember2 = sQLHelper.CreateParameterStringInt("@Member3_ID", 10, MemberID3);
            SqlParameter AdditionalMember3 = sQLHelper.CreateParameterStringInt("@Member4_ID", 10, MemberID4);
            SqlParameter NumberOfGolfCarts = sQLHelper.CreateParameterStringInt("@NumberofCarts", 1, NumberOfCarts);


            SqlParameter[] parameterArray = { RequestedStartDateParam, RequstedStartTimeParam, RequestedEndDateParam, MainMember, AddidtionalMember1, AdditionaMember2, AdditionalMember3, NumberOfGolfCarts };

            sQLHelper.ServerCommand(MembershipRequestUpdate, parameterArray);

            return Page();

        }
    }
}
