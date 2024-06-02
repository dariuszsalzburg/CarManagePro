using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineCarRental.View.Admin
{
    public partial class Cars : System.Web.UI.Page
    {
        Models.Functions Conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
            ShowCars();

            if (Session["UserName"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }

        }

        

        private void ShowCars()
        {
            string Query = "SELECT * FROM CarTbl";
            CarList.DataSource = Conn.GetData(Query);
            CarList.DataBind();
        }
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(VINTb.Value) || string.IsNullOrEmpty(LNumberTb.Value) || string.IsNullOrEmpty(BrandTb.Value) || string.IsNullOrEmpty(ModelTb.Value) || string.IsNullOrEmpty(PriceTb.Value) || string.IsNullOrEmpty(ColorTb.Value))
                {
                    ErrorMsg.InnerText = "Brak informacji";
                }
                else
                {
                    string VIN = VINTb.Value;
                    string PlateNum = LNumberTb.Value;
                    string Brand = BrandTb.Value;
                    string Model = ModelTb.Value;

                    int Price;
                    if (!int.TryParse(PriceTb.Value, out Price))
                    {
                        ErrorMsg.InnerText = "Nieprawidłowy format ceny";
                        return;
                    }
                    string Color = ColorTb.Value;
                    string Status = AvailableCb.SelectedValue;
                    string Eqp_verison = DropDownList2.SelectedValue;
                    string Company_branch = DropDownList3.SelectedValue;

                    string Query = "INSERT INTO CarTbl (VIN, CPlateNum, Brand, Color, Model, Price, Status, Eqp_version, Company_branch) VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', {5}, N'{6}',N'{7}',N'{8}')";
                    Query = string.Format(Query, VIN, PlateNum, Brand, Color, Model, Price, Status,Eqp_verison,Company_branch);
                    Conn.SetData(Query);
                    ShowCars();
                    ErrorMsg.InnerText = "Dodano Samochód";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.InnerText = ex.Message;
            }
        }



        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LNumberTb.Value))
                {
                    ErrorMsg.InnerText = "Brak informacji";
                }
                else
                {

                    string PlateNum = LNumberTb.Value;


                    string Query = "DELETE from CarTbl where CplateNum='{0}'";
                    Query = string.Format(Query, PlateNum);
                    Conn.SetData(Query);
                    ShowCars();
                    ErrorMsg.InnerText = "Usunięto Samochód";
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
                if (string.IsNullOrEmpty(LNumberTb.Value) || string.IsNullOrEmpty(BrandTb.Value) || string.IsNullOrEmpty(ModelTb.Value) || string.IsNullOrEmpty(PriceTb.Value) || string.IsNullOrEmpty(ColorTb.Value))
                {
                    ErrorMsg.InnerText = "Brak informacji";
                }
                else
                {
                    string VIN = VINTb.Value;
                    string PlateNum = LNumberTb.Value;
                    string Brand = BrandTb.Value;
                    string Model = ModelTb.Value;
                    int Price;
                    if (!int.TryParse(PriceTb.Value, out Price))
                    {
                        ErrorMsg.InnerText = "Nieprawidłowy format ceny";
                        return;
                    }
                    string Color = ColorTb.Value;
                    string Status = AvailableCb.SelectedValue;
                    string Eqp_version = DropDownList2.SelectedValue;
                    string Company_branch = DropDownList3.SelectedValue;

                    string Query = "UPDATE CarTbl SET Brand = N'{0}', Model = N'{1}', Price = {2}, Color = N'{3}', Status = N'{4}', VIN = N'{5}',Eqp_version = N'{7}',Company_branch = N'{8}' WHERE CPlateNum = N'{6}'";
                    Query = string.Format(Query, Brand, Model, Price, Color, Status, VIN, PlateNum,Eqp_version,Company_branch);
                    Conn.SetData(Query);
                    ShowCars();
                    ErrorMsg.InnerText = "Edytowano Samochód";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.InnerText = ex.Message;
            }
        }



        protected void Carlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Zakładam, że pierwsza kolumna (Cells[0]) to kolumna z akcjami lub innymi danymi
            if (CarList.SelectedRow.Cells.Count > 7)
            {
                VINTb.Value = CarList.SelectedRow.Cells[1].Text;
                LNumberTb.Value = CarList.SelectedRow.Cells[2].Text;
                BrandTb.Value = CarList.SelectedRow.Cells[3].Text;
                ColorTb.Value = CarList.SelectedRow.Cells[4].Text;
                ModelTb.Value = CarList.SelectedRow.Cells[5].Text;
                PriceTb.Value = CarList.SelectedRow.Cells[6].Text;
                AvailableCb.SelectedValue = CarList.SelectedRow.Cells[7].Text;
            }
            else
            {
                ErrorMsg.InnerText = "Nieprawidłowa liczba kolumn w wybranym wierszu.";
            }
        }

    }
}