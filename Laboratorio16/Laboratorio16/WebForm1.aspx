<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Laboratorio16.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .cal
        {
            position: absolute;
            top:50px;
            left:150px;
            right:400px;
            height:600px;
            bottom:100px;
            background-color:dodgerblue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="cal">
            <asp:Label ID="l" Text="CALCULADORA BASICA" runat="server" Style="margin-left:50px"
                Font-Bold="true" Font-Italic="false" ForeColor="White" Font-Size="25px"></asp:Label>
            <asp:TextBox ID="t" runat="server" Style="margin-left: 50px; margin-top:24px;"
                Width="335px" Height="41px"></asp:TextBox>
            <asp:Button ID="b1" Text="1" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b1_Click" />
            <asp:Button ID="b2" Text="2" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b2_Click" />
            <asp:Button ID="b3" Text="3" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b3_Click" />
            <asp:Button ID="b4" Text="4" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b4_Click" />
            <asp:Button ID="b5" Text="5" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b5_Click" />
            <asp:Button ID="b6" Text="6" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b6_Click" />
            <asp:Button ID="b7" Text="7" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b7_Click" />
            <asp:Button ID="b8" Text="8" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b8_Click" />
            <asp:Button ID="b9" Text="9" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b9_Click" />
            <asp:Button ID="mul" Text="*" runat="server" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="mul_Click" />
            <asp:Button ID="b0" runat="server" Text="0" Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="b0_Click" />
            <asp:Button ID="clr" runat="server" Text="CLR"  Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="clr_Click" />
            <asp:Button ID="eql" runat="server" Text="="  Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="eql_Click" />
            <asp:Button ID="div" Text="/" runat="server"  Height="37px" Style="margin-left:0px"
                Width="57px" OnClick="div_Click" />

        </div>
    </form>
</body>
</html>
