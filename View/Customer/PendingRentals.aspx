<%@ Page Title="" Language="C#" MasterPageFile="~/View/Customer/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="PendingRentals.aspx.cs" Inherits="OnlineCarRental.View.Customer.PendingRentals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col"></div>
            <div class="col">
                <h2>Twoje bieżące wypożyczenia</h2>
            </div>
            <div class="col"></div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="UwagiLabel" runat="server" Text="Uwagi:" AssociatedControlID="UwagiTextBox"></asp:Label>
                <asp:TextBox ID="UwagiTextBox" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control" AllowMultiple="true" />
            </div>
            <div class="col-md-6 d-flex flex-column justify-content-end">
                <asp:Button type="submit" id="AnulujBTN" class="btn btn-danger mb-2" Text="Anuluj" runat="server" OnClick="AnulujBTN_Click" />
                <asp:Button type="button" id="GeneratePDFBTN" class="btn btn-primary mb-2" Text="Generate PDF" runat="server" OnClick="GeneratePDFBTN_Click" />
                <asp:Button type="submit" id="DodajUwageBTN" class="btn btn-primary mb-2" Text="Dodaj Uwagę" runat="server" OnClick="DodajUwageBTN_Click" />
                <asp:Button type="submit" id="UploadImageBTN" class="btn btn-secondary" Text="Upload Images" runat="server" OnClick="UploadImageBTN_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="CarList" CssClass="table table-hover" AutoGenerateSelectButton="true" OnSelectedIndexChanged="CarList_SelectedIndexChanged" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Rentid" HeaderText="Identyfikator" />
                        <asp:BoundField DataField="Customer" HeaderText="Klient" />
                        <asp:BoundField DataField="Car" HeaderText="Samochód" />
                        <asp:BoundField DataField="RentDate" HeaderText="Data rozpoczęcia" />
                        <asp:BoundField DataField="ReturnDate" HeaderText="Data zakończenia" />
                        <asp:BoundField DataField="Kilometers" HeaderText="Kilometry" />
                        <asp:BoundField DataField="Company_branch" HeaderText="Zwrot w oddziale" />
                        <asp:BoundField DataField="Fees" HeaderText="Koszt" />
                        <asp:BoundField DataField="Warnings" HeaderText="Uwagi" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
