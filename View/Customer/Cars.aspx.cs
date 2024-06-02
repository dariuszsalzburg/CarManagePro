using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineCarRental.View.Customer
{
    public partial class Cars : System.Web.UI.Page
    {
        Models.Functions Conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
            if (!IsPostBack)
            {
                ShowCars();
                CustName.InnerText = Login.CName;
                Customer = Login.CustId;
                ShowPromotions(); // Call the method to show promotions
            }

            if (Session["UserName"] == null || Session["Role"] == null || Session["Role"].ToString() != "Customer")
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void ShowCars(string searchCriteria = null, string searchText = null)
        {
            string St = "Dostępny";
            string Query = "SELECT * FROM CarTbl WHERE Status = '" + St + "'";

            if (!string.IsNullOrEmpty(searchCriteria) && !string.IsNullOrEmpty(searchText))
            {
                Query += $" AND {searchCriteria} LIKE '%{searchText}%'";
            }

            CarList.DataSource = Conn.GetData(Query);
            CarList.DataBind();
        }

        private void UpdateCar()
        {
            try
            {
                string Status = "Wypożyczony";
                string Query = "update CarTbl set Status = '{0}' where CplateNum = '{1}'";
                Query = string.Format(Query, Status, CarList.SelectedRow.Cells[2].Text);
                Conn.SetData(Query);
                ShowCars();
            }
            catch (Exception ex)
            {
                InfoMsg.InnerText = ex.Message;
            }
        }

        protected void BookBtn_Click(object sender, EventArgs e)
        {
            if (CarList.SelectedRow == null)
            {
                InfoMsg.InnerText = "Proszę wybrać samochód.";
                return;
            }

            if (string.IsNullOrEmpty(ReturnDate.Value))
            {
                InfoMsg.InnerText = "Proszę wybrać datę zwrotu.";
                return;
            }

            DateTime returnDate;
            if (!DateTime.TryParseExact(ReturnDate.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out returnDate))
            {
                InfoMsg.InnerText = "Nieprawidłowy format daty zwrotu.";
                return;
            }

            if (string.IsNullOrEmpty(Kilometers.Text) || !int.TryParse(Kilometers.Text, out int kilometers))
            {
                InfoMsg.InnerText = "Proszę wprowadzić poprawną ilość kilometrów.";
                return;
            }


            TimeSpan DDays = returnDate - DateTime.Today.Date;
            int Days = DDays.Days;
            int DPrice = Convert.ToInt32(CarList.SelectedRow.Cells[8].Text);
            int Fees = DPrice * Days;
            string Company_branchh = DropDownList4.SelectedValue;

            try
            {
                if (CarList.SelectedRow.Cells[2].Text == "")
                {
                    InfoMsg.InnerText = "Brak informacji.";
                }
                else
                {
                    string Query = "INSERT INTO RentTbl (Car, Customer, RentDate, ReturnDate, Fees, Kilometers, Company_branch) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}')";
                    string formattedQuery = string.Format(Query, CarList.SelectedRow.Cells[2].Text, Login.CustId, DateTime.Today.Date.ToString("yyyy-MM-dd"), returnDate.ToString("yyyy-MM-dd"), Fees, kilometers, Company_branchh);
                    Conn.SetData(formattedQuery);
                    UpdateCar();
                    ShowCars();
                    InfoMsg.InnerText = "Wypożyczono Samochód";
                }
            }
            catch (Exception ex)
            {
                InfoMsg.InnerText = ex.Message;
            }
        }

        string LNumber, RentDate, RetDate, Company_branch;
        int Fees = 0, DPrice, Customer, Kilometerss;

        protected void CarList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LNumber = CarList.SelectedRow.Cells[2].Text;
            RentDate = DateTime.Today.Date.ToString("yyyy-MM-dd");
            DPrice = Convert.ToInt32(CarList.SelectedRow.Cells[8].Text);
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchCriteria = DropDownList3.SelectedValue;
            string searchText = search2.Text.Trim();
            ShowCars(searchCriteria, searchText);
        }

        private void ShowPromotions()
        {
            // Fetch promotion details from the database or any other source
            string promotionMessage = "Specjalna oferta tylko DZIŚ, skorzystaj z kuponu na 10% zniżki na twój wymarzony samochód";
            PromotionHiddenField.Value = promotionMessage;
        }
    }
}
