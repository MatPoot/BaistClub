using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class ApplyForMembershipModel : PageModel
    {
        [Required, MinLength(6)]
        public string Name { get; set; }
        [Required, MinLength(6),MaxLength(50)]
        public string Password { get; set; }
        [Required, MinLength(6), MaxLength(100)]
        public string Address { get; set; }
        [Required, MinLength(6), MaxLength(8)]
        public string PostalCode { get; set; }
        [Required, MinLength(6)]
        public string Phone { get; set; }
        [MinLength(6)]
        public string AltPhone { get; set; }
        [Required, MinLength(6), MaxLength(50)]
        public string Email { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPostalCode { get; set; }
        public string CompanyPhone { get; set; }
        [Required, MinLength(6), MaxLength(50)]
        public string FirstReferrerEmail { get; set; }
        [Required, MinLength(6), MaxLength(50)]
        public string SecondReferrerEmail { get; set; }

        public string Msg { get; set; }
        
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
             return Page();
            }
            else
            {

                SQLHelper sQLHelper = new SQLHelper();

                SqlConnection MasterConnection = sQLHelper.ConnectToServer();

                SqlCommand AddMembershipRequest = new SqlCommand();
                AddMembershipRequest.CommandType = CommandType.StoredProcedure;
                AddMembershipRequest.Connection = MasterConnection;
                AddMembershipRequest.CommandText = "AddMembershipRequest";

                SqlParameter NameSQL = sQLHelper.CreateParameterStringInt("@Name", 25, Name);
                SqlParameter PasswordSQL = sQLHelper.CreateParameterStringInt("@Password", 50, Password);
                SqlParameter AddressSQL = sQLHelper.CreateParameterStringInt("@Address", 100, Address);
                SqlParameter MemberClassSQL = sQLHelper.CreateParameterStringInt("@MemberClass", 1, "G");
                SqlParameter MemberFeesPaidSQL = sQLHelper.CreateParameterStringInt("@MemberFeesPaid", 1, "0");
                SqlParameter StatusOfApplicationSQL = sQLHelper.CreateParameterStringInt("@StatusOfApplication", 1, "H");
                SqlParameter PostalCodeSQL = sQLHelper.CreateParameterStringInt("@PostalCode", 7, PostalCode);
                SqlParameter PhoneSQL = sQLHelper.CreateParameterStringInt("@Phone", 10, Phone);
                SqlParameter AltPhoneSQL = sQLHelper.CreateParameterStringInt("@AltPhone", 10, AltPhone);
                SqlParameter DateOfBirthSQL = sQLHelper.CreateParameterStringInt("@DateOfBirth", 10, DateOfBirth);
                SqlParameter EmailSQL = sQLHelper.CreateParameterStringInt("@Email", 50, Email);
                SqlParameter OccupationSQL = sQLHelper.CreateParameterStringInt("@Occupation", 50, Occupation);
                SqlParameter CompanyNameSQL = sQLHelper.CreateParameterStringInt("@CompanyName", 50, CompanyName);
                SqlParameter CompanyAddressSQL = sQLHelper.CreateParameterStringInt("@CompanyAddress", 100, CompanyAddress);
                SqlParameter CompanyPostalCodeSQL = sQLHelper.CreateParameterStringInt("@CompanyPostalCode", 8, CompanyPostalCode);
                SqlParameter CompanyPhoneSQL = sQLHelper.CreateParameterStringInt("@CompanyPhone", 10, CompanyPhone);
                SqlParameter FirstReferrerEmailSQL = sQLHelper.CreateParameterStringInt("@FirstReferrerEmail", 100, FirstReferrerEmail);
                SqlParameter SecondReferrerEmailSQL = sQLHelper.CreateParameterStringInt("@SecondReferrerEmail", 100, SecondReferrerEmail);


                SqlParameter[] parameterArray ={NameSQL,PasswordSQL,AddressSQL,PostalCodeSQL,PhoneSQL,AltPhoneSQL,EmailSQL,OccupationSQL,CompanyNameSQL,
            CompanyAddressSQL,CompanyPostalCodeSQL,CompanyPhoneSQL,FirstReferrerEmailSQL,SecondReferrerEmailSQL,DateOfBirthSQL,MemberClassSQL,MemberFeesPaidSQL,StatusOfApplicationSQL};

                sQLHelper.ServerCommand(AddMembershipRequest, parameterArray);

                Msg = "Request Submitted";

                return Page();
            }
             

            
        }
    }
}
