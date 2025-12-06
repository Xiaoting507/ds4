<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Laboratorio203.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sistema de Gestión de Laptops</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            padding: 20px;
        }
        
        .container {
            max-width: 1200px;
            margin: 0 auto;
            background: white;
            border-radius: 15px;
            box-shadow: 0 15px 35px rgba(0,0,0,0.3);
            overflow: hidden;
        }
        
        .header {
            background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
            color: white;
            padding: 25px 30px;
        }
        
        .header h1 {
            font-size: 28px;
            margin-bottom: 5px;
        }
        
        .header p {
            font-size: 14px;
            opacity: 0.9;
        }
        
        .toolbar {
            background: #f8f9fa;
            padding: 15px 30px;
            border-bottom: 2px solid #dee2e6;
            display: flex;
            align-items: center;
            gap: 10px;
            flex-wrap: wrap;
        }
        
        .toolbar-group {
            display: flex;
            gap: 10px;
            align-items: center;
        }
        
        .toolbar-separator {
            width: 1px;
            height: 30px;
            background: #dee2e6;
            margin: 0 10px;
        }
        
        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 14px;
            font-weight: bold;
            transition: all 0.3s;
            display: inline-flex;
            align-items: center;
            gap: 6px;
        }
        
        .btn:disabled {
            opacity: 0.5;
            cursor: not-allowed;
        }
        
        .btn-nuevo {
            background: #28a745;
            color: white;
        }
        
        .btn-nuevo:hover:not(:disabled) {
            background: #218838;
            transform: translateY(-2px);
        }
        
        .btn-guardar {
            background: #007bff;
            color: white;
        }
        
        .btn-guardar:hover:not(:disabled) {
            background: #0056b3;
            transform: translateY(-2px);
        }
        
        .btn-cancelar {
            background: #6c757d;
            color: white;
        }
        
        .btn-cancelar:hover:not(:disabled) {
            background: #545b62;
            transform: translateY(-2px);
        }
        
        .btn-eliminar {
            background: #dc3545;
            color: white;
        }
        
        .btn-eliminar:hover:not(:disabled) {
            background: #c82333;
            transform: translateY(-2px);
        }
        
        .btn-buscar {
            background: #17a2b8;
            color: white;
        }
        
        .btn-buscar:hover:not(:disabled) {
            background: #138496;
            transform: translateY(-2px);
        }
        
        .search-box {
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .search-box label {
            font-weight: bold;
            color: #495057;
        }
        
        .search-box input {
            padding: 8px 12px;
            border: 2px solid #ced4da;
            border-radius: 6px;
            width: 100px;
            font-size: 14px;
        }
        
        .content {
            padding: 30px;
        }
        
        .form-section {
            background: #f8f9fa;
            padding: 25px;
            border-radius: 10px;
            margin-bottom: 30px;
        }
        
        .form-section h3 {
            color: #495057;
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 2px solid #dee2e6;
        }
        
        .form-row {
            display: grid;
            grid-template-columns: 1fr 3fr;
            gap: 20px;
            margin-bottom: 20px;
        }
        
        .form-row-full {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }
        
        .form-group {
            display: flex;
            flex-direction: column;
        }
        
        .form-group label {
            font-weight: bold;
            margin-bottom: 8px;
            color: #495057;
            font-size: 14px;
        }
        
        .form-control {
            padding: 10px 12px;
            border: 2px solid #ced4da;
            border-radius: 6px;
            font-size: 15px;
            transition: border-color 0.3s;
        }
        
        .form-control:focus {
            outline: none;
            border-color: #667eea;
        }
        
        .form-control:disabled {
            background: #e9ecef;
            cursor: not-allowed;
        }
        
        .error {
            color: #dc3545;
            font-size: 13px;
            margin-top: 5px;
        }
        
        .message {
            padding: 15px 20px;
            border-radius: 8px;
            margin-bottom: 20px;
            font-weight: bold;
        }
        
        .message-success {
            background: #d4edda;
            color: #155724;
            border-left: 4px solid #28a745;
        }
        
        .message-error {
            background: #f8d7da;
            color: #721c24;
            border-left: 4px solid #dc3545;
        }
        
        .grid-section {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            overflow-x: auto;
        }
        
        .grid-section h3 {
            color: #495057;
            margin-bottom: 15px;
        }
        
        .gridview {
            width: 100%;
            border-collapse: collapse;
            background: white;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        
        .gridview th {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 12px 15px;
            text-align: left;
            font-weight: bold;
            font-size: 14px;
        }
        
        .gridview td {
            padding: 12px 15px;
            border-bottom: 1px solid #dee2e6;
            font-size: 14px;
        }
        
        .gridview tr:hover {
            background: #f8f9fa;
        }
        
        .gridview tr:last-child td {
            border-bottom: none;
        }
        
        .no-data {
            text-align: center;
            padding: 40px;
            color: #6c757d;
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header -->
            <div class="header">
                <h1>💻 Sistema de Gestión de Laptops</h1>
                <p>Laboratorio 203 - Basado en Laboratorio 14</p>
            </div>
            
            <!-- Toolbar -->
            <div class="toolbar">
                <div class="toolbar-group">
                    <asp:Button ID="btnNuevo" runat="server" Text="📄 Nuevo" 
                                CssClass="btn btn-nuevo" OnClick="btnNuevo_Click" 
                                CausesValidation="false" />
                    
                    <asp:Button ID="btnGuardar" runat="server" Text="💾 Guardar" 
                                CssClass="btn btn-guardar" OnClick="btnGuardar_Click" 
                                ValidationGroup="Laptop" Enabled="false" />
                    
                    <asp:Button ID="btnCancelar" runat="server" Text="✖ Cancelar" 
                                CssClass="btn btn-cancelar" OnClick="btnCancelar_Click" 
                                CausesValidation="false" Enabled="false" />
                    
                    <asp:Button ID="btnEliminar" runat="server" Text="🗑 Eliminar" 
                                CssClass="btn btn-eliminar" OnClick="btnEliminar_Click" 
                                CausesValidation="false" Enabled="false" 
                                OnClientClick="return confirm('¿Está seguro de eliminar este registro?');" />
                </div>
                
                <div class="toolbar-separator"></div>
                
                <div class="search-box">
                    <label>Buscar por ID:</label>
                    <asp:TextBox ID="txtBuscarId" runat="server" TextMode="Number" 
                                 placeholder="ID"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="🔍 Buscar" 
                                CssClass="btn btn-buscar" OnClick="btnBuscar_Click" 
                                CausesValidation="false" />
                </div>
            </div>
            
            <!-- Content -->
            <div class="content">
                <!-- Mensajes -->
                <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="message">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </asp:Panel>
                
                <!-- Formulario -->
                <div class="form-section">
                    <h3>Datos del Laptop</h3>
                    
                    <div class="form-row">
                        <div class="form-group">
                            <label>ID:</label>
                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control" 
                                         Enabled="false" placeholder="Auto"></asp:TextBox>
                        </div>
                        
                        <div class="form-group">
                            <label>Nombre: *</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" 
                                         MaxLength="50" Enabled="false" 
                                         placeholder="Ej: Dell XPS 13"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                ControlToValidate="txtNombre" ValidationGroup="Laptop"
                                ErrorMessage="El nombre es requerido" CssClass="error" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    <div class="form-row-full">
                        <div class="form-group">
                            <label>Precio: *</label>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" 
                                         TextMode="Number" step="0.01" Enabled="false"
                                         placeholder="999.99"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                ControlToValidate="txtPrecio" ValidationGroup="Laptop"
                                ErrorMessage="El precio es requerido" CssClass="error" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvPrecio" runat="server" 
                                ControlToValidate="txtPrecio" ValidationGroup="Laptop"
                                MinimumValue="0" MaximumValue="9999.99" Type="Double"
                                ErrorMessage="Precio debe estar entre 0 y 9999.99" 
                                CssClass="error" Display="Dynamic"></asp:RangeValidator>
                        </div>
                        
                        <div class="form-group">
                            <label>Stock: *</label>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" 
                                         TextMode="Number" Enabled="false"
                                         placeholder="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStock" runat="server" 
                                ControlToValidate="txtStock" ValidationGroup="Laptop"
                                ErrorMessage="El stock es requerido" CssClass="error" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvStock" runat="server" 
                                ControlToValidate="txtStock" ValidationGroup="Laptop"
                                MinimumValue="0" MaximumValue="9999" Type="Integer"
                                ErrorMessage="Stock debe estar entre 0 y 9999" 
                                CssClass="error" Display="Dynamic"></asp:RangeValidator>
                        </div>
                    </div>
                </div>
                
                <!-- Grid de Laptops -->
                <div class="grid-section">
                    <h3>📋 Lista de Laptops Registrados</h3>
                    <asp:GridView ID="gvLaptops" runat="server" CssClass="gridview" 
                                  AutoGenerateColumns="False" 
                                  EmptyDataText="No hay laptops registrados en la base de datos">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" 
                                            DataFormatString="${0:N2}" />
                            <asp:BoundField DataField="stock" HeaderText="Stock" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="no-data">
                                📦 No hay laptops registrados en la base de datos
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
