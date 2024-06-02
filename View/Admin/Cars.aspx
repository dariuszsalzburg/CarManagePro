<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="OnlineCarRental.View.Admin.Cars" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <h3 class="text-danger pl-4">Zarządzaj Samochodami</h3>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <form>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="VINTb">VIN</label>
                                <input limit="17" type="text" class="form-control" id="VINTb" placeholder="Wprowadź VIN" runat="server">
                            </div>
                            <div class="form-group">
                                <label for="LNumberTb">Numer Rejestracyjny</label>
                                <input type="text" class="form-control" id="LNumberTb" placeholder="Wprowadź Numer Rejestracyjny" runat="server">
                            </div>
                            <div class="form-group">
                                <label for="BrandTb">Marka</label>
                                <input type="text" class="form-control" id="BrandTb" placeholder="Wprowadź Markę" runat="server">
                            </div>
                            <div class="form-group">
                                <label for="ModelTb">Model</label>
                                <input type="text" class="form-control" id="ModelTb" placeholder="Wprowadź Model" runat="server">
                            </div>
                        </div>
                        <div class="col-md-6">
                                 <div class="form-group">
                                  <label for="AvailableCbd">Wersja wyposażenia</label>
                                  <asp:DropDownList ID="DropDownList2" runat="server" class="form-control">
                                  <asp:ListItem>Standard</asp:ListItem>
                                  <asp:ListItem>Proffesional</asp:ListItem>
                                  </asp:DropDownList>
                             </div>
                                  <div class="form-group">
                                  <label for="Branch">Oddział firmy</label>
                                  <asp:DropDownList ID="DropDownList3" runat="server" class="form-control">
                                  <asp:ListItem>Wrocław</asp:ListItem>
                                  <asp:ListItem>Katowice</asp:ListItem>
                                  <asp:ListItem>Warszawa</asp:ListItem>
                                <asp:ListItem>Gdańsk</asp:ListItem>
                                  </asp:DropDownList>
                             </div>
                            <div class="form-group">
                                <label for="PriceTb">Cena</label>
                                <input type="text" class="form-control" id="PriceTb" placeholder="Wprowadź Cenę" runat="server">
                            </div>
                            <div class="form-group">
                                <label for="ColorTb">Kolor</label>
                                <input type="text" class="form-control" id="ColorTb" placeholder="Wprowadź Kolor" runat="server">
                            </div>
                            <div class="form-group">
                                <label for="AvailableCb">Dostęność</label>
                                <asp:DropDownList ID="AvailableCb" runat="server" class="form-control">
                                    <asp:ListItem>Dostępny</asp:ListItem>
                                    <asp:ListItem>Wypożyczony</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label id="ErrorMsg" runat="server"></label>
                            <div class="form-group">
                                <asp:Button type="submit" id="EditBtn" class="btn btn-danger" Text="Edytuj" runat="server" OnClick="EditBtn_Click" />
                                <asp:Button type="submit" id="SaveBtn" class="btn btn-danger" Text="Dodaj" runat="server" OnClick="SaveBtn_Click"/>
                                <asp:Button type="submit" id="DeleteBtn" class="btn btn-danger" Text="Usuń" runat="server" OnClick="DeleteBtn_Click" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <h1>Lista Samochodów</h1>
                <asp:GridView runat="server" AutoGenerateColumns="false" ID="CarList" CssClass="table table-hover" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Carlist_SelectedIndexChanged" >
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
</asp:Content>
