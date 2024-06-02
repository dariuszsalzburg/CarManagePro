<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="OnlineCarRental.View.Admin.Customers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <h3 class="text-danger pl-4">Zarządzaj Klientami</h3>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <form>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="CustNameTb">Imię i Nazwisko</label>
                                <input type="text" class="form-control" id="CustNameTb" placeholder="Wprowadź Imię i Nazwisko" runat="server">
                            </div>
                            <div class="form-group">
                                <label for="AddTb">Adres zamieszkania</label>
                                <input type="text" class="form-control" id="AddTb" placeholder="Wprowadź Adres" runat="server">
                            </div>
                            <div class="form-group">
                                <label for="PhoneTb">Numer Telefonu</label>
                                <input type="text" class="form-control" id="PhoneTb" placeholder="Wprowadź Numer" runat="server">
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="PasswordTb">Hasło Klienta</label>
                                <input type="text" class="form-control" id="PasswordTb" placeholder="Wprowadź Hasło" runat="server">
                            </div>
                            <div class="form-group">
                                <label id="ErrorMsg" runat="server"></label>
                            </div>
                            <div class="form-group">
                                <asp:Button type="submit" id="EditBtn" class="btn btn-danger" Text="Edytuj" runat="server" OnClick="EditBtn_Click" />
                                <asp:Button type="submit" id="SaveBtn" class="btn btn-danger" Text="Dodaj" runat="server" OnClick="SaveBtn_Click" />
                                <asp:Button type="submit" id="DeleteBtn" class="btn btn-danger" Text="Usuń" runat="server" OnClick="DeleteBtn_Click" />
                            </div>
                        </div>
                    </div>
                    <input type="hidden" id="CustIdTb" runat="server" />
                </form>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col">
                <h1>Lista Klientów</h1>
                <asp:GridView runat="server" ID="CustomersList" CssClass="table table-hover" AutoGenerateSelectButton="True" AutoGenerateColumns="false" OnSelectedIndexChanged="Customerslist_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="CustId" HeaderText="Identyfikator klienta" />
                        <asp:BoundField DataField="CustName" HeaderText="Nazwa" />
                        <asp:BoundField DataField="CustAdd" HeaderText="Adres" />
                        <asp:BoundField DataField="CustPhone" HeaderText="Numer telefonu" />
                        <asp:BoundField DataField="CustPassword" HeaderText="Hasło" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
