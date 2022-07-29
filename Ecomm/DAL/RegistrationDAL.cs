using Ecomm.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.DAL
{
    public class RegistrationDAL
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public RegistrationDAL()
        {
            con = new SqlConnection(Startup.ConnectionString);
        }

        
        public int AddCustomer(Registration reg)
        {
            string qry = "insert into Users(UName,UEmail,UPassword) values(@name,@email,@password)";
            cmd = new SqlCommand(qry, con);

            cmd.Parameters.AddWithValue("@name", reg.UName);
            cmd.Parameters.AddWithValue("@email", reg.UEmail);
            cmd.Parameters.AddWithValue("@password", reg.UPassword);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public Registration UserLogin(Registration reg)
        {
            Registration p = new Registration();
            string qry = "select * from Users where UEmail=@email";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@email",reg.UEmail);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    p.UId = Convert.ToInt32(dr["UId"]);
                    p.UName = dr["UName"].ToString();
                    p.UEmail = dr["UEmail"].ToString();
                    p.UPassword=dr["UPassword"].ToString();
                    p.URoleId = Convert.ToInt32(dr["URoleId"]);
                }
            }
            con.Close();
            return p;
        }


    }



    }

