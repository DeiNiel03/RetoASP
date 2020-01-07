<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="RetoASP.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="TextBox1" runat="server" placeholder="Usuario"></asp:TextBox>
        <br />
        <br />
        <input id="Pass1" type="password" placeholder="Contraseña"/><br />
        <br />
&nbsp;<asp:Button ID="Button1" runat="server" BorderStyle="None" Text="Login" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btRegistro" runat="server" BorderStyle="None" Text="Registrarme" />
        <br />
    </form>
</body>
</html>
