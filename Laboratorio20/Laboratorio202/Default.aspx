<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Laboratorio202.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Matriz Diagonal Inversa</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            margin: 0;
            padding: 20px;
        }
        .container {
            background: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.3);
            max-width: 800px;
            width: 100%;
        }
        h1 {
            color: #f5576c;
            text-align: center;
            margin-bottom: 30px;
        }
        .input-group {
            margin-bottom: 20px;
        }
        label {
            display: block;
            margin-bottom: 8px;
            color: #333;
            font-weight: bold;
        }
        .form-control {
            width: 100%;
            padding: 10px;
            border: 2px solid #ddd;
            border-radius: 5px;
            font-size: 16px;
            box-sizing: border-box;
        }
        .form-control:focus {
            outline: none;
            border-color: #f5576c;
        }
        .btn {
            background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            color: white;
            padding: 12px 30px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            width: 100%;
            transition: transform 0.2s;
        }
        .btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0,0,0,0.2);
        }
        .resultado {
            margin-top: 30px;
            padding: 20px;
            background: #f8f9fa;
            border-radius: 10px;
            border-left: 4px solid #f5576c;
            overflow-x: auto;
        }
        .matriz-container {
            display: flex;
            justify-content: center;
            margin-top: 20px;
        }
        .matriz-table {
            border-collapse: collapse;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        .matriz-table td {
            border: 2px solid #dee2e6;
            padding: 15px;
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            min-width: 50px;
            min-height: 50px;
            background: white;
        }
        .matriz-table td.diagonal {
            background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            color: white;
        }
        .error {
            color: #dc3545;
            font-size: 14px;
            margin-top: 5px;
        }
        .info-box {
            background: #e7f3ff;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
            border-left: 4px solid #2196F3;
        }
        .info-box p {
            margin: 5px 0;
            color: #1976D2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>📊 Matriz con Diagonal Inversa</h1>
            
            <div class="info-box">
                <p><strong>ℹ️ Información:</strong></p>
                <p>Esta aplicación genera una matriz N×N donde:</p>
                <p>✓ La diagonal inversa contiene números <strong>1</strong></p>
                <p>✓ El resto de las celdas contienen <strong>0</strong></p>
            </div>
            
            <div class="input-group">
                <label for="txtDimension">Dimensión de la matriz (N):</label>
                <asp:TextBox ID="txtDimension" runat="server" CssClass="form-control" 
                             placeholder="Ej: 5" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDimension" runat="server" 
                    ControlToValidate="txtDimension" 
                    ErrorMessage="Por favor ingrese una dimensión" 
                    CssClass="error" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvDimension" runat="server" 
                    ControlToValidate="txtDimension" 
                    MinimumValue="2" 
                    MaximumValue="15" 
                    Type="Integer"
                    ErrorMessage="Ingrese una dimensión entre 2 y 15" 
                    CssClass="error" Display="Dynamic">
                </asp:RangeValidator>
            </div>
            
            <asp:Button ID="btnGenerar" runat="server" Text="Generar Matriz" 
                        CssClass="btn" OnClick="btnGenerar_Click" />
            
            <asp:Panel ID="pnlResultado" runat="server" Visible="false" CssClass="resultado">
                <h3 style="color: #f5576c; margin-top: 0; text-align: center;">
                    Matriz de <asp:Label ID="lblDimension" runat="server"></asp:Label>×<asp:Label ID="lblDimension2" runat="server"></asp:Label>
                </h3>
                <div class="matriz-container">
                    <asp:Literal ID="litMatriz" runat="server"></asp:Literal>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
