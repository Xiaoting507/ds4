<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Laboratorio154.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Laboratorio 15-4</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:40px;">
            <asp:Label ID="Label1" runat="server" Text="Número 1: "></asp:Label>
            <asp:TextBox ID="txtNumero1" runat="server" Width="80px"></asp:TextBox>
            <br /><br />

            <asp:Label ID="Label2" runat="server" Text="Número 2: "></asp:Label>
            <asp:TextBox ID="txtNumero2" runat="server" Width="80px"></asp:TextBox>
            <br /><br />

            <asp:Button ID="btnSumar" runat="server" Text="Sumar" OnClick="btnSumar_Click" />
            <br /><br />

            <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
