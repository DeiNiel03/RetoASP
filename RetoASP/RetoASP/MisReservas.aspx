<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MisReservas.aspx.vb" Inherits="RetoASP.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblId" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Municipio"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Nº de personas"></asp:Label>
            <br />
            <br />
        </div>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
    </form>
</body>
</html>
