<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Returns.aspx.cs" Inherits="OnlineCarRental.View.Admin.Returns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-3">
                <h1>Zwrócone Samochody</h1>
            </div>
            <div class="col-md-3"></div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <asp:GridView runat="server" AutoGenerateColumns="false" ID="RentList" CssClass="table table-hover" AutoGenerateSelectButton="True">
                 <Columns>
                    <asp:BoundField DataField="RetId" HeaderText="Identyfikator" />
                    <asp:BoundField DataField="Car" HeaderText="Samochód" />
                    <asp:BoundField DataField="Customer" HeaderText="Klient" />
                    <asp:BoundField DataField="Date" HeaderText="Data zwrotu" />
                    <asp:BoundField DataField="Delay" HeaderText="Opóżnienie/dni" />
                    <asp:BoundField DataField="Fine" HeaderText="Grzywna" />
                    <asp:BoundField DataField="Repairs" HeaderText="Naprawy" />
                    <asp:BoundField DataField="Fees" HeaderText="Koszt" />
                    <asp:BoundField DataField="Administration" HeaderText="Sprawy administracyjne" />
                </Columns>
                </asp:GridView>
                <asp:Button runat="server" ID="btnGeneratePDF" Text="Generate PDF" OnClick="btnGeneratePDF_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
