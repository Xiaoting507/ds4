<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Laboratorio201.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Tabla de Multiplicar</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
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
            max-width: 600px;
            width: 100%;
        }
        h1 {
            color: #667eea;
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
            border-color: #667eea;
        }
        .btn {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
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
            border-left: 4px solid #667eea;
        }
        .tabla-multiplicar {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 10px;
            margin-top: 15px;
        }
        .fila-tabla {
            padding: 8px;
            background: white;
            border-radius: 5px;
            border: 1px solid #e0e0e0;
            font-size: 14px;
        }
        .error {
            color: #dc3545;
            font-size: 14px;
            margin-top: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>🔢 Tabla de Multiplicar</h1>
            
            <div class="input-group">
                <label for="txtNumero">Ingrese un número:</label>
                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" 
                             placeholder="Ej: 5" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumero" runat="server" 
                    ControlToValidate="txtNumero" 
                    ErrorMessage="Por favor ingrese un número" 
                    CssClass="error" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvNumero" runat="server" 
                    ControlToValidate="txtNumero" 
                    MinimumValue="1" 
                    MaximumValue="999" 
                    Type="Integer"
                    ErrorMessage="Ingrese un número entre 1 y 999" 
                    CssClass="error" Display="Dynamic">
                </asp:RangeValidator>
            </div>
            
            <asp:Button ID="btnGenerar" runat="server" Text="Generar Tabla" 
                        CssClass="btn" OnClick="btnGenerar_Click" />
            
            <asp:Panel ID="pnlResultado" runat="server" Visible="false" CssClass="resultado">
                <h3 style="color: #667eea; margin-top: 0;">
                    Tabla del <asp:Label ID="lblNumero" runat="server"></asp:Label>
                </h3>
                <div class="tabla-multiplicar">
                    <asp:Literal ID="litTabla" runat="server"></asp:Literal>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
