using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Web;


namespace OnlineCarRental.Models
{
    public class Functions
    {
        private SqlConnection Conn;
        private SqlCommand cmd;
        private DataTable dt;
        public string ConnStr;
        private SqlDataAdapter sda;

        public Functions() 
        {
            ConnStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dariu\\OneDrive\\Pulpit\\Programowanie IV\\OnlineCarRental\\OnlineCarRental\\CarRentalDbASP.mdf;Integrated Security=True;Connect Timeout=30";
            Conn = new SqlConnection(ConnStr);
            cmd = new SqlCommand();
            cmd.Connection = Conn;
        }
        public DataTable GetData(string Query) 
        {
            dt = new DataTable();
            sda = new SqlDataAdapter(Query, ConnStr);
            sda.Fill(dt);
            return dt;
        }
        public void ExecuteQuery(string query)
        {
            // Add your database connection and execution logic here
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dariu\\OneDrive\\Pulpit\\Programowanie IV\\OnlineCarRental\\OnlineCarRental\\CarRentalDbASP.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void ExecuteQueryy(string Query, params SqlParameter[] parameters)
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }

            cmd.CommandText = Query;
            cmd.Parameters.Clear();
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        public int SetData(string Query)
        {
            int rcnt = 0;
            if(Conn.State == ConnectionState.Closed) 
            {
                Conn.Open();
            }
            cmd.CommandText = Query;
            rcnt = cmd.ExecuteNonQuery();
            Conn.Close();
            return rcnt;
        }

        public int AddCustomer(string custName, string custAdd, string custPhone, string custPassword)
        {
            string query = "INSERT INTO CustomerTbl (CustName, CustAdd, CustPhone, CustPassword) VALUES (@CustName, @CustAdd, @CustPhone, @CustPassword)";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@CustName", custName);
            cmd.Parameters.AddWithValue("@CustAdd", custAdd);
            cmd.Parameters.AddWithValue("@CustPhone", custPhone);
            cmd.Parameters.AddWithValue("@CustPassword", custPassword);

            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            int result = cmd.ExecuteNonQuery();
            Conn.Close();

            return result;
        }

     
    }
}