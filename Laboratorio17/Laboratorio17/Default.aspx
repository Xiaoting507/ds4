<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Laboratorio17._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


        <div class="row">
            <asp:GridView id="MyGridView" DataSourceID="MyDataSource1"
                AllowSorting="true" AllowPaging="true"
                DataKeyNames="ProductID"
                AutoGenerateEditButton="true"
                Runat="server" />

            <asp:SqlDataSource ID="MyDataSource1" runat="server"
                ConnectionString="data source=(local);initial catalog=northwind;persist security info=True;Integrated Security=SSPI;"
                ProviderName="System.Data.SqlClient"
                SelectCommand="SELECT ProductId, ProductName, UnitPrice From Products"
                UpdateCommand="Update Products Set [ProductName]=@ProductName, [UnitPrice]=@UnitPrice Where[ProductId">
            </asp:SqlDataSource>
                
        </div>
 

</asp:Content>
