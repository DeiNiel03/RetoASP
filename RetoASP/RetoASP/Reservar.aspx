<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reservar.aspx.vb" Inherits="RetoASP.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="DropTipo" runat="server" placeholder="Email" Height="20px" Width="140px" AutoPostBack="True">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropProvincia" runat="server" placeholder="Email" Height="20px" Width="140px" AutoPostBack="True">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropMunicipio" runat="server" placeholder="Email" Height="20px" Width="140px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="LabelRestaurante" runat="server" Text="Restaurante:"></asp:Label>
        <asp:RadioButton ID="RBsi" runat="server" GroupName="restaurante" Text="Si" />
        &nbsp;
        <asp:RadioButton ID="RBno" runat="server" Checked="True" GroupName="restaurante" Text="No" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LabelAutocaravana" runat="server" Text="Parking para autocaravana:"></asp:Label>
        <asp:RadioButton ID="RBcsi" runat="server" GroupName="caravana" Text="Si" />
        &nbsp;
        <asp:RadioButton ID="RBcno" runat="server" Checked="True" GroupName="caravana" Text="No" />
        <br />
        <br />
        <asp:Label ID="lblNO" runat="server" Font-Bold="True" ForeColor="Red" Text="Sin Resultados De Busqueda"></asp:Label>
        <br />
        <asp:ListBox ID="listNombres" runat="server" Height="68px" Width="269px" AutoPostBack="True"></asp:ListBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Información" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reservar" />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="LabelInfo" runat="server" Text="Descripción:"></asp:Label>
        <br />
        <asp:Label ID="lblInfo" runat="server" Height="109px" Text="Label" Width="427px"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelDireccion" runat="server" Text="Dirección:"></asp:Label>
&nbsp;<asp:Label ID="lblDireccion" runat="server" Height="20px" Text="Label" Width="120px"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelTelefono" runat="server" Text="Teléfono:"></asp:Label>
&nbsp;<asp:Label ID="lblTelefono" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelEmail" runat="server" Text="Email:"></asp:Label>
&nbsp;<asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelWeb" runat="server" Text="Pagina Web:"></asp:Label>
&nbsp;<asp:HyperLink ID="HyperLinkWeb" runat="server">HyperLink</asp:HyperLink>
        <br />
        <br />
        <br />
        <asp:Image ID="Imagen" runat="server" Height="134px" Width="215px" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
