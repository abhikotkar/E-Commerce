using Ecomm.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.DAL
{
    public class OrderDAL
    {
       
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dr;
            public OrderDAL()
            {
                con = new SqlConnection(Startup.ConnectionString);
            }
            private bool CheckOrderData(Order or)
            {

                return true;
            }
            public int PlaceOrder(Order or)
            {
                bool result = CheckOrderData(or);
                if (result == true)
                {
                    string qry = "insert into Orders(PId,UId) values(@prodid,@userid)";
                    cmd = new SqlCommand(qry, con);
                    cmd.Parameters.AddWithValue("@prodid", or.PId);
                    cmd.Parameters.AddWithValue("@userid", or.UId);
                    
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    return res;
                }
                else
                {
                    return 2;
                }
            }

            public List<Product> ViewProductForOrder(string userid)
            {
                List<Product> plist = new List<Product>();
                string qry = "select p.Id,p.Name,p.Price, o.OId,O.UId from Product p " +
                            " inner join Orders o on o.PId = p.Id " +
                            " where o.UId = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(userid));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Product p = new Product();
                        p.Id = Convert.ToInt32(dr["Id"]);
                        p.Name = dr["Name"].ToString();
                        p.Price = Convert.ToDouble(dr["Price"]);
                        p.OId = Convert.ToInt32(dr["OId"]);
                        p.UId = Convert.ToInt32(dr["UId"]);
                        plist.Add(p);
                    }
                    con.Close();
                    return plist;
                }
                else
                {
                    return plist;
                }
            }
            public int RemoveFromOrders(int id)
            {

                string qry = "delete from Orders where OId=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
        }
    }
