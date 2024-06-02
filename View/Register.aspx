<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnlineCarRental.View.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rejestracja Klienta</title>
    <link rel="stylesheet" href="../Assets/Libraries/css/bootstrap.min.css"/>
    <script>
        function validatePassword() {
            var password = document.getElementById("CustPasswordTb").value;
            var passwordCriteria = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$/;

            if (!passwordCriteria.test(password)) {
                alert("Hasło musi zawierać co najmniej 8 znaków, w tym jedną wielką literę, jedną małą literę, jedną cyfrę i jeden znak specjalny.");
                return false;
            }
            return true;
        }

        function redirectLogin() {
            window.location.href = 'Login.aspx';
        }
    </script>
</head>
<body>
   <div class="container-fluid">
       <div class="row">
           <div class="col-md-4"></div>
           <div class="col-md-4">
               <div class="row mt-5">
                   <div class="col"></div>
                   <div class="col">
                       <h3 class="text-warning">Rejestracja Klienta</h3>
                   </div>
                   <div class="col"></div>
               </div>
               <form runat="server" onsubmit="return validatePassword()">
                   <div class="form-group">
                       <label for="CustNameTb">Nazwa Użytkownika</label>
                       <input type="text" class="form-control" id="CustNameTb" runat="server" placeholder="Wpisz nazwę użytkownika" required="required">
                   </div>
                   <div class="form-group">
                       <label for="CustAddTb">Adres</label>
                       <input type="text" class="form-control" id="CustAddTb" runat="server" placeholder="Wpisz adres" required="required">
                   </div>
                   <div class="form-group">
                       <label for="CustPhoneTb">Telefon</label>
                       <input type="text" class="form-control" id="CustPhoneTb" runat="server" placeholder="Wpisz telefon" required="required">
                   </div>
                   <div class="form-group">
                       <label for="CustPasswordTb">Hasło</label>
                       <input type="password" class="form-control" id="CustPasswordTb" runat="server" placeholder="Wpisz hasło" required="required">
                   </div>
                   <br />
                   <div class="form-group d-grid">
                       <label id="InfoMsg" runat="server"></label>
                       <asp:Button type="submit" id="RegisterBtn" class="btn btn-warning btn-block" Text="Zarejestruj" runat="server" OnClick="RegisterBtn_Click" />
                   </div>
                   <br />
                   <div class="form-group d-grid">
                        <label id="Label1" runat="server"></label>
                        <button type="button" id="LoginRedirectBtn" class="btn btn-secondary btn-sm btn-block mt-3" onclick="redirectLogin()">Logowanie</button>
                   </div>
               </form>
           </div>
           <div class="col-md-4"></div>
       </div>
   </div>
</body>
</html>
