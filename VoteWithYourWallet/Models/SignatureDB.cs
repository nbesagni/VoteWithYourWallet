using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VoteWithYourWallet.Models
{
    public class EmployeeDB
    {
        //declare connection string
        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //Return list of all Employees
        public List<Signature> ListAll()
        {
            List<Signature> lst = new List<Signature>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectSignature", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new Signature
                    {
                        SignatureID = Convert.ToInt32(rdr["SignatureId"]),
                        ApplicationUserID = rdr["ApplicationUserID"].ToString(),
                        DateTimeSigned = System.DateTime.Now,
                        Message = rdr["Message"].ToString()
                    });
                }
                return lst;
            }
        }
        //Method for Adding an Employee
        public int Add(Signature sig)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", sig.SignatureID);
                com.Parameters.AddWithValue("@User", sig.ApplicationUserID);
                com.Parameters.AddWithValue("@Date", sig.DateTimeSigned);
                com.Parameters.AddWithValue("@Message", sig.Message);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
 
    }
}