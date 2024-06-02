<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineCarRental.View.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="../Assets/Libraries/css/bootstrap.min.css"/>
</head>
<body>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <div class="row mt-5">
                    <div class="col"></div>
                    <div class="col">
                        <h3 class="text-warning">Panel Logowania</h3>
                    </div>
                    <div class="col"></div>
                </div>
                <form runat="server">
                    <div class="form-group">
                        <label for="UserNameTb">Nazwa Użytkownika</label>
                        <input type="text" class="form-control" id="UserNameTb" runat="server" placeholder="Wpisz nazwę użytkownika" required="required">
                    </div>
                    <div class="form-group">
                        <label for="PasswordTb">Hasło</label>
                        <input type="password" class="form-control" id="PasswordTb" runat="server" placeholder="Wpisz hasło" required="required">
                    </div>
                    
                    <br />
                    <div class="form-group d-grid">
                        <label id="InfoMsg" runat="server"></label>
                        <asp:Button type="submit" id="SaveBtn" class="btn btn-warning btn-block" Text="Zaloguj" runat="server" OnClick="SaveBtn_Click" />
                        
                    </div>
                    <br />
                    <div class="form-group d-grid">
    <label id="Label1" runat="server"></label>
    
   <button type="button" id="RegisterRedirectBtn" class="btn btn-secondary btn-sm btn-block mt-3" onclick="redirectRegister()">Rejestracja</button>

</div>
                </form>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>

    <script>
    function redirectRegister() {
        window.location.href = 'Register.aspx';
    }
    </script>

</body>
</html>
