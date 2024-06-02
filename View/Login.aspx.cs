using System;
using System.Data;
using System.Web.UI;

namespace OnlineCarRental.View
{
    public partial class Login : Page
    {
        Models.Functions Conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
        }

        public static string CName = "";
        public static int CustId;
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            
            
                if (UserNameTb.Value == "Admin" && PasswordTb.Value == "Admin")
                {
                    CName = "Admin";
                    Session["UserName"] = "Admin";
                    Session["Role"] = "Admin";
                    Response.Redirect("Admin/Home.aspx");
                }
                else
                {
                    InfoMsg.InnerText = "Niewłaściwe dane logowania";
                }
            
            
            
                string sql = "select custname, custpassword, custid from customertbl where custname = '{0}' and custpassword = '{1}'";
                sql = string.Format(sql, UserNameTb.Value, PasswordTb.Value);
                DataTable dt = Conn.GetData(sql);
                if (dt.Rows.Count == 0)
                {
                    InfoMsg.InnerText = "Niewłaściwe dane logowania";
                }
                else
                {
                    CName = dt.Rows[0][0].ToString();
                    CustId = Convert.ToInt32(dt.Rows[0][2].ToString());
                    Session["UserName"] = CName;
                    Session["CustId"] = CustId;
                    Session["Role"] = "Customer";
                    Response.Redirect("Customer/Cars.aspx");
                }
            }
        }

        
    }

