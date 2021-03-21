using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BaistClub.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClub.Pages
{
    [BindProperties]
    public class ManageMembershipApplicationsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<MembershipRequests> Requesters { get; set; }

     

        public string EditedApplicantID { get; set; }
        public string UpdateStatus { get; set; }

       

        public string Msg { get; set; }
       
        public void OnGet()
        {
          
            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand GetMembershipRequests = new SqlCommand();
            GetMembershipRequests.CommandType = CommandType.StoredProcedure;
            GetMembershipRequests.Connection = MasterConnection;
            GetMembershipRequests.CommandText = "GetMembershipRequests";
           
            Requesters = sQLHelper.FetchMemberships(GetMembershipRequests);

            if (Requesters == null)
            {
                Msg = "No current Items";
            }
        }

        public IActionResult OnPost()
        {
            // 2 ways, to do this,must be tested first. 1.- just list the applicantid as well and hope only the updated values post back and not the rest
            // either parse it into a list or figure out the repeating pattern and use that 
            
            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection MasterConnection = sQLHelper.ConnectToServer();

            SqlCommand MembershipRequestUpdate = new SqlCommand();
            MembershipRequestUpdate.CommandType = CommandType.StoredProcedure;
            MembershipRequestUpdate.Connection = MasterConnection;
            MembershipRequestUpdate.CommandText = "UpdateMembershipRequest";


            //int convertedEditedApplicantID = Int32.Parse(EditedApplicantID);

            SqlParameter WhichApplicanthasbeenEdited = sQLHelper.CreateParameterStringInt("@MemberID", 10, EditedApplicantID);
            SqlParameter UpdatedStatusforApplicant = sQLHelper.CreateParameterStringInt("@StatusOfApplication", 1, UpdateStatus);

            SqlParameter[] parameterArray ={WhichApplicanthasbeenEdited,UpdatedStatusforApplicant};

            sQLHelper.ServerCommand(MembershipRequestUpdate,parameterArray);

            return Page();


            // trigger for database to read through all the applications and move the appropreate values to the main users table


        }
    }
}
