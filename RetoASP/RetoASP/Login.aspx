<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="RetoASP.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="Iniciar Sesión"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="TBDni" runat="server" placeholder="Usuario"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBDni" ErrorMessage="Introduzca un usuario" Font-Size="Small" ForeColor="Red" ValidationGroup="grupo2"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:TextBox ID="TBPass" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
        <br />
        <br />
&nbsp;<asp:Button ID="btnLogin" runat="server" BorderStyle="None" Text="Login" ValidationGroup="grupo2" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btRegistro" runat="server" BorderStyle="None" Text="Registrarme" />
        <br />
    </form>
</body>
</html>
