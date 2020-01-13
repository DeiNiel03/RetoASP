<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reservar.aspx.vb" Inherits="RetoASP.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="DropTipo" runat="server" placeholder="Email" Height="20px" Width="140px">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:DropDownList ID="DropProvincia" runat="server" placeholder="Email" Height="20px" Width="140px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:DropDownList ID="DropMunicipio" runat="server" placeholder="Email" Height="20px" Width="140px">
        </asp:DropDownList>
        <br />
        <br />
        Restaurante:
        <asp:RadioButton ID="RBsi" runat="server" GroupName="restaurante" Text="Si" />
        <asp:RadioButton ID="RBno" runat="server" Checked="True" GroupName="restaurante" Text="No" />
        <br />
        <br />
        Parking para autocaravana:
        <asp:RadioButton ID="RBcsi" runat="server" GroupName="caravana" Text="Si" />
        <asp:RadioButton ID="RBcno" runat="server" Checked="True" GroupName="caravana" Text="No" />
        <br />
        <br />
        <br />
        <asp:ListBox ID="listNombres" runat="server" Height="206px" Width="207px"></asp:ListBox>
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" Height="100px" Width="142px" />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
