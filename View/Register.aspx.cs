using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace OnlineCarRental.View
{
    public partial class Register : Page
    {
        Models.Functions Conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            string custName = CustNameTb.Value;
            string custAdd = CustAddTb.Value;
            string custPhone = CustPhoneTb.Value;
            string custPassword = CustPasswordTb.Value;

            if (string.IsNullOrEmpty(custName) || string.IsNullOrEmpty(custAdd) || string.IsNullOrEmpty(custPhone) || string.IsNullOrEmpty(custPassword))
            {
                InfoMsg.InnerText = "Proszę wypełnić wszystkie pola.";
                return;
            }

            if (!IsValidPassword(custPassword))
            {
                InfoMsg.InnerText = "Hasło musi zawierać co najmniej 8 znaków, w tym jedną wielką literę, jedną małą literę, jedną cyfrę i jeden znak specjalny.";
                return;
            }

            int result = Conn.AddCustomer(custName, custAdd, custPhone, custPassword);
            if (result > 0)
            {
                InfoMsg.InnerText = "Rejestracja zakończona sukcesem!";
            }
            else
            {
                InfoMsg.InnerText = "Rejestracja nie powiodła się.";
            }
        }

        private bool IsValidPassword(string password)
        {
            var passwordCriteria = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$");
            return passwordCriteria.IsMatch(password);
        }
    }
}
