<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registro.aspx.vb" Inherits="RetoASP.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="TBDni" runat="server" placeholder="DNI"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:TextBox ID="TBNombre" runat="server" placeholder="Nombre"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="TBApellido" runat="server" placeholder="Apellido"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="TBPass" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="TBPass2" runat="server" TextMode="Password" placeholder="Repita la contraseña"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="TBTel" runat="server" placeholder="Número de teléfono" type="number"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="BtnRegistro" runat="server" BorderStyle="None" Text="Registrarme" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btVolver" runat="server" BorderStyle="None" Text="Volver" Height="22px" />
        </div>
    </form>
</body>
</html>
