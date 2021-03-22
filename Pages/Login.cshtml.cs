using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BaistClub.Classes;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace BaistClub.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {

        
       
        [Required,MinLength(3),MaxLength(50)]
        public string Email { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string Password { get; set; }

        public string Msg { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
           

            SQLHelper sQLHelper = new SQLHelper();

            SqlConnection CheckLoginCommand = sQLHelper.ConnectToServer();

            SqlCommand CheckLogin = new SqlCommand();
            CheckLogin.CommandType = CommandType.StoredProcedure;
            CheckLogin.Connection = CheckLoginCommand;
            CheckLogin.CommandText = "CheckLogin";

            SqlParameter EmailCode = new SqlParameter();
            EmailCode.ParameterName = "@Email";
            EmailCode.SqlDbType = SqlDbType.NVarChar;
            EmailCode.Size = 50;
            EmailCode.Value = Email;

            SqlParameter PasswordCode = new SqlParameter();
            PasswordCode.ParameterName = "@Password";
            PasswordCode.SqlDbType = SqlDbType.NVarChar;
            PasswordCode.Size = 50;
            PasswordCode.Value = Password;

            SqlParameter[] ParameterArray = { EmailCode, PasswordCode };
            List<AccountSession> loginIsvalid = sQLHelper.CheckLogin(CheckLogin, ParameterArray);

            
            if (loginIsvalid.Count<1)
            {
           
                Msg = "Invalid";
                return Page();
            }
            else
            {
                foreach (var record in loginIsvalid)
                {
                    HttpContext.Session.SetString("MemberID", record.MemberID.ToString());
                    HttpContext.Session.SetString("MemberName", record.MemberName.ToString());
                    HttpContext.Session.SetString("AccountLevel", record.AccountLevel.ToString());
                    HttpContext.Session.SetString("MemberClass", record.MemberClass.ToString());
                    
                }
               
               return RedirectToPage("Index");

            }

       
        }
    }
}
