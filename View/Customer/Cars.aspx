<%@ Page Title="" Language="C#" MasterPageFile="~/View/Customer/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="OnlineCarRental.View.Customer.Cars" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mybody" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            var promotionMessage = '<%= PromotionHiddenField.Value %>';
            if (promotionMessage) {
                alert(promotionMessage);
            }
        };
    </script>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-8"></div>
            <div class="col-md-4">
                <label id="CustName" runat="server">...</label>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="Search">Wyszukaj po:</label>
                    <asp:DropDownList ID="DropDownList3" runat="server" class="form-control">
                        <asp:ListItem Value="Brand">Marce</asp:ListItem>
                        <asp:ListItem Value="Color">Kolorze</asp:ListItem>
                        <asp:ListItem Value="Model">Modelu</asp:ListItem>
                        <asp:ListItem Value="Eqp_version">Wersji wyposażenia</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="Search2">Wyszukaj</label>
                    <asp:TextBox ID="search2" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12">
                <asp:Button ID="SearchBtn" runat="server" Text="Szukaj" CssClass="btn btn-primary" OnClick="SearchBtn_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="date">Data zwrotu</label>
                    <input type="date" class="form-control" id="ReturnDate" runat="server" required="required">
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="Kilometers">Ilość kilometrów</label>
                    <asp:TextBox ID="Kilometers" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="Branchh">Zwrot samochodu w Oddziale firmy w:</label>
                    <asp:DropDownList ID="DropDownList4" runat="server" class="form-control">
                        <asp:ListItem>Wrocław</asp:ListItem>
                        <asp:ListItem>Katowice</asp:ListItem>
                        <asp:ListItem>Warszawa</asp:ListItem>
                        <asp:ListItem>Gdańsk</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group d-grid">
                    <label id="InfoMsg" runat="server"></label>
                    <asp:Button type="submit" id="BookBtn" class="btn btn-warning btn-block" Text="Wypożycz" runat="server" OnClick="BookBtn_Click" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <h3>Lista Samochodów</h3>
                <asp:GridView runat="server" ID="CarList" CssClass="table table-hover" AutoGenerateSelectButton="True" OnSelectedIndexChanged="CarList_SelectedIndexChanged" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="VIN" HeaderText="Numer VIN" />
                        <asp:BoundField DataField="CPlateNum" HeaderText="Numer rejestracyjny" />
                        <asp:BoundField DataField="Brand" HeaderText="Marka" />
                        <asp:BoundField DataField="Color" HeaderText="Kolor" />
                        <asp:BoundField DataField="Model" HeaderText="Model" />
                        <asp:BoundField DataField="Eqp_version" HeaderText="Wersja wyposażenia" />
                        <asp:BoundField DataField="Company_branch" HeaderText="Oddział" />
                        <asp:BoundField DataField="Price" HeaderText="Cena/24h" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="PromotionHiddenField" runat="server" />
</asp:Content>
