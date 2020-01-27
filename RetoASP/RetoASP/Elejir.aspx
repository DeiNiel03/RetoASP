<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Elejir.aspx.vb" Inherits="RetoASP.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:Label ID="lblUsuario" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnReservar" runat="server" Font-Bold="True" Height="26px" Text="Hacer una reserva" Width="158px" />
        <br />
        <br />
        <asp:Button ID="btnVer" runat="server" Font-Bold="True" Font-Italic="False" Height="26px" Text="Mis reservas" Width="158px" />
        
    </form>
</body>
</html>
