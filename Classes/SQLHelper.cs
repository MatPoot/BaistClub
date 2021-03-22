using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;             
using System.Threading.Tasks;
using BaistClub.Classes;

namespace BaistClub.Classes
{
    public class SQLHelper
    {

        public SqlConnection ConnectToServer()
        {
            SqlConnection MasterConnection = new SqlConnection();
 
            MasterConnection.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=baistclub;server=(local);";
            //MasterConnection.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=aahmad20;server=(local);";
            MasterConnection.Open();

            
            return MasterConnection;
        }



        public SqlParameter CreateParameterStringInt(string name,int size,string value)
        {

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = name;
            sqlParameter.Size = size;
            
            bool isNumeric = int.TryParse(value, out int n);
            if (isNumeric == true)
            {
                sqlParameter.SqlDbType = SqlDbType.Int;
                sqlParameter.Value = n;
            }
            else
            {
                sqlParameter.SqlDbType = SqlDbType.NVarChar;
                sqlParameter.Value = value;

            }


            return sqlParameter;
        }

        public bool ServerCommand(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {

   

            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }

            SendCommand.ExecuteNonQuery();
           
            return true;
        }

        public int ServerCommandSingleReturn(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {

          
            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }

            SqlDataReader rdr = SendCommand.ExecuteReader();


            if (rdr.HasRows)
            {
                int returnvalue;
                while (rdr.Read())
                {
                    returnvalue = rdr.GetInt32(0);
                    rdr.Close();
                    return returnvalue;

                  
                }
                rdr.Close();

                
                //return returnvalue;

            }
           
                rdr.Close();
                return -1;
            

           

            
        }

        public List<AccountSession> CheckLogin(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {


            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }


            SqlDataReader rdr = SendCommand.ExecuteReader();


            List<AccountSession> ReturnStringList = new List<AccountSession>();

            if (rdr.HasRows)
            {


                while (rdr.Read())
                {
                    AccountSession insertItem = new AccountSession();
                    insertItem.MemberID = (int)rdr["MemberID"];
                    insertItem.MemberName = (string)rdr["MemberName"];
                    insertItem.AccountLevel = (string)rdr["AccountLevel"];
                    insertItem.MemberClass = (string)rdr["MemberClass"];
                   
                    ReturnStringList.Add(insertItem);

                }


            }




            rdr.Close();
            return ReturnStringList;
        }

        public List<MembershipRequests> FetchMemberships(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {


            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }


            SqlDataReader rdr = SendCommand.ExecuteReader();


            List<MembershipRequests> ReturnStringList = new List<MembershipRequests>();
          
            if (rdr.HasRows)
            {
                

                while (rdr.Read())
                {
                    MembershipRequests insertItem = new MembershipRequests();
                    insertItem.MemberID = (int)rdr["MemberID"];
                    insertItem.MemberName = (string)rdr["MemberName"];
                    insertItem.ReferringMemberEmail_1 = (string)rdr["ReferringMemberEmail_1"];
                    insertItem.ReferringMemberEmail_2 = (string)rdr["ReferringMemberEmail_2"];
                    insertItem.StatusOfApplication = (string)rdr["StatusOfApplication"];

                    ReturnStringList.Add(insertItem);

                }


            }




            rdr.Close();
            return ReturnStringList;
        }

        public List<Handicap> FetchHandicaps(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {


            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }


            SqlDataReader rdr = SendCommand.ExecuteReader();


            List<Handicap> ReturnStringList = new List<Handicap>();

            if (rdr.HasRows)
            {


                while (rdr.Read())
                {
                    Handicap insertItem = new Handicap();
                   
                    insertItem.HandicapIndex = (Decimal)rdr["HandicapIndex"];
                    insertItem.Last20Avg = (Decimal)rdr["Last20Avg"];
                    insertItem.Best8Avg = (Decimal)rdr["Best8Avg"];
                    

                    ReturnStringList.Add(insertItem);

                }


            }




            rdr.Close();
            return ReturnStringList;
        }

        public List<Members> FetchMembers(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {


            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }


            SqlDataReader rdr = SendCommand.ExecuteReader();


            List<Members> ReturnStringList = new List<Members>();

            if (rdr.HasRows)
            {


                while (rdr.Read())
                {
                    Members insertItem = new Members();
                    insertItem.MemberID = (int)rdr["MemberID"];
                    insertItem.MemberName = (string)rdr["MemberName"];
                    insertItem.AccountLevel = (string)rdr["AccountLevel"];
                    insertItem.MemberClass = (string)rdr["MemberClass"];
                    insertItem.MemberPhone = (long)rdr["MemberPhone"];
                    insertItem.MemberAddress = (string)rdr["Address"];
                    insertItem.PostalCode = (string)rdr["PostalCode"];
                    insertItem.DOB = (DateTime)rdr["DateOfBirth"];
                    insertItem.DateOfApplication = (DateTime)rdr["DateofApplication"];
                    insertItem.Referrer1 = (string)rdr["ReferringMemberEmail_2"];
                    insertItem.Referrer2 = (string)rdr["ReferringMemberEmail_2"];


                    ReturnStringList.Add(insertItem);

                }

                
            }




            rdr.Close();
            return ReturnStringList;
        }

        public List<TeeTimeRequests> FetchRequests(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {


            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }


            SqlDataReader rdr = SendCommand.ExecuteReader();


            List<TeeTimeRequests> ReturnStringList = new List<TeeTimeRequests>();

            if (rdr.HasRows)
            {


                while (rdr.Read())
                {
                    TeeTimeRequests insertItem = new TeeTimeRequests();
                    insertItem.MemberID = (int)rdr["MemberID"];
                    insertItem.MemberName = (string)rdr["MemberName"];
                    insertItem.StartDate = (DateTime)rdr["RequestedTeeTimeStartDate"];
                    insertItem.EndDate = (DateTime)rdr["RequestedTeeTimeEndDate"];
                    insertItem.TimeTime = (int)rdr["RequestedTeeTimeTime"];

                    ReturnStringList.Add(insertItem);

                }


            }




            rdr.Close();
            return ReturnStringList;
        }

        public List<TeeTimeRequests> FetchStandingRequests(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {


            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }


            SqlDataReader rdr = SendCommand.ExecuteReader();


            List<TeeTimeRequests> ReturnStringList = new List<TeeTimeRequests>();

            if (rdr.HasRows)
            {


                while (rdr.Read())
                {
                    TeeTimeRequests insertItem = new TeeTimeRequests();
                    insertItem.MemberID = (int)rdr["Member1_ID"];
                    
                    insertItem.StartDate = (DateTime)rdr["RequestedTeeTimeStartDate"];
                    insertItem.EndDate = (DateTime)rdr["RequestedTeeTimeEndDate"];
                    insertItem.TimeTime = (int)rdr["RequestedTeeTimeTime"];

                    ReturnStringList.Add(insertItem);

                }


            }




            rdr.Close();
            return ReturnStringList;
        }

        public List<TeeTimes> FetchTeeTimes(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {


            foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }


            SqlDataReader rdr = SendCommand.ExecuteReader();


            List<TeeTimes> ReturnStringList = new List<TeeTimes>();

            if (rdr.HasRows)
            {


                while (rdr.Read())
                {
                    TeeTimes insertItem = new TeeTimes();
                    insertItem.MemberID = (int)rdr["Member1_ID"];
                    
                    insertItem.StartDate = (DateTime)rdr["TeeTimeStartDate"];
                    
                    insertItem.TimeTime = (int)rdr["TeeTimeStartTime"];

                    ReturnStringList.Add(insertItem);
                    
                }


            }




            rdr.Close();
            return ReturnStringList;
        }

        public List<string> FetchValues(SqlCommand SendCommand, params SqlParameter[] Parameters)
        {
           

          foreach (SqlParameter CommandParams in Parameters) // add all params to the command
            {
                SendCommand.Parameters.Add(CommandParams);
            }


            SqlDataReader rdr = SendCommand.ExecuteReader();


            List<string> ReturnStringList = new List<string>();
            // There is no explicit naming, so the values are squentially loaded in the interest of
            // making the class more usable generally. Use counters to get the appropreate value out.
            if (rdr.HasRows)
            {
                int counter = 0;
                while (rdr.Read())
                        {

                    ReturnStringList.Add(rdr.GetString(counter));
                    counter++;
                        }
            }
            else
            {
                ReturnStringList.Add("No Results");
            };

            rdr.Close();



            return ReturnStringList;
        }

        public void DisconnectFromServer(SqlConnection MasterConnection)
        {
           
            MasterConnection.Close();
            
        }


    }
}
