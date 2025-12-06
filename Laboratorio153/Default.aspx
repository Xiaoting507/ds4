<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Laboratorio153.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Laboratorio 15-3</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:40px;">
            <asp:Label ID="Label1" runat="server" Text="Ingrese un texto: "></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Mostrar mensaje" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
