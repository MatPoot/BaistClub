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
    public class SceduleTeeTimeModel : PageModel
    {
        [Required]
        public string RequestedStartDate { get; set; }
        [Required]
        public string RequestedStartTime { get; set; }
      
        [Required, MinLength(1), MaxLength(8)]
        public string MemberID2 { get; set; }
        [ MinLength(1), MaxLength(8)]
        public string MemberID3 { get; set; }
        [ MinLength(1), MaxLength(8)]
        public string MemberID4 { get; set; }
        [Required]

        public string NumberOfCarts { get; set; }
        
        public string Message { get; set; }
      

        public void OnGet()
        {
            string LoginStatus = HttpContext.Session.GetString("MemberID");
            string MemberClass = HttpContext.Session.GetString("MemberClass");
            string AccountLevel = HttpContext.Session.GetString("AccountLevel");
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
             


            DateTime submittedTime = Convert.ToDateTime(RequestedStartTime);
            DateTime submitteddate = Convert.ToDateTime(RequestedStartDate);
            int Weekday = (int)submitteddate.DayOfWeek;
            bool IsWorkDay = false;
            if (Weekday>=1 && Weekday <= 5){
                IsWorkDay = true;
            }

            if (MemberClass == "B")
            {
                TimeSpan WeekStartDenial = new TimeSpan(15, 0, 0);
                TimeSpan WeekEndDenial = new TimeSpan(18, 0, 0);
                TimeSpan WeekendBeforeDenial = new TimeSpan(13, 0, 0);

                if (IsWorkDay == true)
                {
                    if (submittedTime.TimeOfDay >= WeekStartDenial && submittedTime.TimeOfDay <= WeekEndDenial)
                    {
                        Message = "Invalid Input date or time for your class of membership";
                        return Page();
                    }
                }else if (IsWorkDay == false)
                {
                    if (submittedTime.TimeOfDay <= WeekendBeforeDenial)
                    {
                        Message = "Invalid Input date or time for your class of membership";
                        return Page();
                    }
                }

            }
            if (MemberClass == "S")
            {
                TimeSpan WeekStartDenial = new TimeSpan(15, 0, 0);
                TimeSpan WeekEndDenial = new TimeSpan(17, 30, 0);
                TimeSpan WeekendBeforeDenial = new TimeSpan(11, 0, 0);

                if (IsWorkDay == true)
                {
                    if (submittedTime.TimeOfDay >= WeekStartDenial && submittedTime.TimeOfDay <= WeekEndDenial)
                    {
                        Message = "Invalid Input date or time for your class of membership";
                        return Page();
                    }
                }
                else if (IsWorkDay == false)
                {
                    if (submittedTime.TimeOfDay <= WeekendBeforeDenial)
                    {
                        Message = "Invalid Input date or time for your class of membership";
                        return Page();
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            } else
            {
                SQLHelper sQLHelper = new SQLHelper();

                SqlConnection MasterConnection = sQLHelper.ConnectToServer();

                SqlCommand MembershipRequestUpdate = new SqlCommand();
                MembershipRequestUpdate.CommandType = CommandType.StoredProcedure;
                MembershipRequestUpdate.Connection = MasterConnection;
                MembershipRequestUpdate.CommandText = "BookTeeTime";




                SqlParameter RequestedStartDateParam = sQLHelper.CreateParameterStringInt("@TeeTimeStartDate", 10, RequestedStartDate);
                SqlParameter RequstedStartTimeParam = sQLHelper.CreateParameterStringInt("@TeeTimeStartTime", 3, stringmin);
                SqlParameter MainMember = sQLHelper.CreateParameterStringInt("@Member1_ID", 10, LoginStatus);
                SqlParameter AddidtionalMember1 = sQLHelper.CreateParameterStringInt("@Member2_ID", 10, MemberID2);
                SqlParameter AdditionaMember2 = sQLHelper.CreateParameterStringInt("@Member3_ID", 10, MemberID3);
                SqlParameter AdditionalMember3 = sQLHelper.CreateParameterStringInt("@Member4_ID", 10, MemberID4);
                SqlParameter NumberOfGolfCarts = sQLHelper.CreateParameterStringInt("@NumberofCarts", 1, NumberOfCarts);


                SqlParameter[] parameterArray = { RequestedStartDateParam, RequstedStartTimeParam, MainMember, AddidtionalMember1, AdditionaMember2, AdditionalMember3, NumberOfGolfCarts };

                sQLHelper.ServerCommand(MembershipRequestUpdate, parameterArray);

                return Page();
            }

            

        }
    }
}
