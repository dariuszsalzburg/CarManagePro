<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Rents.aspx.cs" Inherits="OnlineCarRental.View.Admin.Rents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <h3>Wypożyczone Samochody</h3>
                 <hr class="my-4">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Search">Wyszukaj po:</label>
                            <asp:DropDownList ID="DropDownList5" runat="server" class="form-control">
                                <asp:ListItem Value="Rentid">Identyfikatorze</asp:ListItem>
                                <asp:ListItem Value="Car">Numerze rejestracyjnym</asp:ListItem>
                                <asp:ListItem Value="Customer">Kliencie</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="Search2">Wyszukaj</label>
                            <asp:TextBox ID="search2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="SearchBtn" runat="server" Text="Szukaj" CssClass="btn btn-primary" OnClick="SearchBtn_Click" />
                        </div>
                    </div>
                 
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Search43">Załaduj zdjęcia:</label> 
                            <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control" AllowMultiple="true" />
                            <asp:Button type="submit" id="UploadImageBTN" class="btn btn-primary" Text="Załaduj zdjęcia" runat="server" OnClick="UploadImageBTN_Click" />
                        </div>
                        <div class="form-group">
                            <asp:Button type="button" id="GeneratePDFBTN" class="btn btn-primary" Text="Generuj protokół" runat="server" OnClick="GeneratePDFBTN_Click" />
                        </div>
                    </div>
                </div>
                <div class="row mt-4">
                           <hr class="my-4">
                    <div class="col-md-6">
                  
                        <div class="form-group">
                               
                            <label for="DelayTb">Opóźnienie</label>
                            <input type="text" class="form-control" id="DelayTb" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="FineTb">Grzywna</label>
                            <input type="text" class="form-control" id="FineTb" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="RepairsTb">Potrzebne Naprawy</label>
                            <input type="text" class="form-control" id="RepairsTb" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="FeesTb">Opłaty</label>
                            <input type="text" class="form-control" id="FeesTb" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="AdminIssuesTb">Sprawy Administracyjne</label>
                            <input type="text" class="form-control" id="AdminIssuesTb" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:Button type="submit" id="SaveBtn" class="btn btn-danger" Text="Zwróć" runat="server" OnClick="SaveBtn_Click" />
                        </div>
                        <div class="form-group d-grid">
                            <label id="InfoMsg" runat="server"></label>
                        </div>
                    </div>
                </div>
                <div class="row mt-4">
                    <div class="col-md-12">
                        <asp:GridView runat="server" ID="RentList" AutoGenerateColumns="false" CssClass="table table-hover" AutoGenerateSelectButton="True">
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
        </div>
    </div>
</asp:Content>
