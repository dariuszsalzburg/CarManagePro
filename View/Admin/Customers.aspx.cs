using OnlineCarRental.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineCarRental.View.Admin
{
    public partial class Customers : System.Web.UI.Page
    {
        Models.Functions Conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
            ShowCustomers();

            if (Session["UserName"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }

        }

        private void ShowCustomers()
        {
            string Query = "SELECT * FROM CustomerTbl";
            CustomersList.DataSource = Conn.GetData(Query);
            CustomersList.DataBind();
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CustNameTb.Value) || string.IsNullOrEmpty(CustNameTb.Value) || string.IsNullOrEmpty(PhoneTb.Value) || string.IsNullOrEmpty(PasswordTb.Value) || string.IsNullOrEmpty(AddTb.Value))
                {
                    ErrorMsg.InnerText = "Brak informacji";
                }
                else
                {
                    string CustName = CustNameTb.Value;
                    string CustAdd = AddTb.Value;
                    string CustPhone = PhoneTb.Value;
                    string CustPassword = PasswordTb.Value;


                    string Query = "INSERT INTO CustomerTbl (CustName, CustAdd, CustPhone, CustPassword) VALUES (N'{0}', N'{1}', N'{2}', N'{3}')";
                    Query = string.Format(Query, CustName, CustAdd, CustPhone, CustPassword);

                    Conn.SetData(Query);
                    ShowCustomers();
                    ErrorMsg.InnerText = "Dodano Klienta";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.InnerText = ex.Message;
            }
        }

        protected void Customerslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CustomersList.SelectedRow != null)
            {
                // Sprawdzamy, czy wybrany wiersz nie jest pusty
                if (CustomersList.SelectedRow.Cells.Count >= 5)
                {
                    CustIdTb.Value = CustomersList.SelectedRow.Cells[1].Text; // Pobieramy CustId
                    CustNameTb.Value = CustomersList.SelectedRow.Cells[2].Text;
                    AddTb.Value = CustomersList.SelectedRow.Cells[3].Text;
                    PhoneTb.Value = CustomersList.SelectedRow.Cells[4].Text;
                    PasswordTb.Value = CustomersList.SelectedRow.Cells[5].Text;
                }
                else
                {
                    ErrorMsg.InnerText = "Nieprawidłowa liczba kolumn w wybranym wierszu.";
                }
            }
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CustNameTb.Value))
                {
                    ErrorMsg.InnerText = "Brak informacji";
                    return;
                }

                string CustName = CustNameTb.Value;
                string CustPhone = PhoneTb.Value; // Zakładam, że mamy również CustPhone dostępne na formularzu

                Functions dbFunctions = new Functions();

                // Znalezienie CustId na podstawie CustName i CustPhone
                string getCustIdQuery = $"SELECT CustId FROM CustomerTbl WHERE CustName='{CustName}' AND CustPhone='{CustPhone}'";
                DataTable dt = dbFunctions.GetData(getCustIdQuery);

                if (dt.Rows.Count > 0)
                {
                    string CustId = dt.Rows[0]["CustId"].ToString();

                    // Teraz możemy usunąć rekord na podstawie CustId
                    string deleteQuery = $"DELETE FROM CustomerTbl WHERE CustId='{CustId}'";
                    int rowsAffected = dbFunctions.SetData(deleteQuery);

                    if (rowsAffected > 0)
                    {
                        ErrorMsg.InnerText = "Usunięto Klienta";
                        ShowCustomers();
                    }
                    else
                    {
                        ErrorMsg.InnerText = "Nie znaleziono klienta o podanym ID";
                    }
                }
                else
                {
                    ErrorMsg.InnerText = "Nie znaleziono klienta o podanych danych";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.InnerText = ex.Message;
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CustNameTb.Value) || string.IsNullOrEmpty(AddTb.Value) || string.IsNullOrEmpty(PhoneTb.Value) || string.IsNullOrEmpty(PasswordTb.Value) || string.IsNullOrEmpty(CustIdTb.Value))
                {
                    ErrorMsg.InnerText = "Brak informacji";
                }
                else
                {
                    string CustId = CustIdTb.Value; // Pobierz CustId z ukrytego pola
                    string CustName = CustNameTb.Value;
                    string CustAdd = AddTb.Value;
                    string CustPhone = PhoneTb.Value;
                    string CustPassword = PasswordTb.Value;

                    string Query = "UPDATE CustomerTbl SET CustName = N'{0}', CustAdd = N'{1}', CustPhone = N'{2}', CustPassword = N'{3}' WHERE CustId = {4}";
                    Query = string.Format(Query, CustName, CustAdd, CustPhone, CustPassword, CustId);
                    Conn.SetData(Query);
                    ShowCustomers();
                    ErrorMsg.InnerText = "Edytowano Klienta";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.InnerText = ex.Message;
            }
        }

    }
}
