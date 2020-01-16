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
        <asp:DropDownList ID="DropMunicipio" runat="server" placeholder="Email" Height="20px" Width="140px" AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnMasfiltros" runat="server" Text="Más filtros" />
        <br />
        <asp:Button ID="btnMenosfiltros" runat="server" Enabled="False" Text="Menos filtros" Visible="False" />
        <br />
        <br />
        <asp:Label ID="LabelRestaurante" runat="server" Text="Restaurante:"></asp:Label>
        <asp:RadioButton ID="RBsi" runat="server" GroupName="restaurante" Text="Si" AutoPostBack="True" />
        &nbsp;
        <asp:RadioButton ID="RBno" runat="server" Checked="True" GroupName="restaurante" Text="No" AutoPostBack="True" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LabelAutocaravana" runat="server" Text="Parking para autocaravana:"></asp:Label>
        <asp:RadioButton ID="RBcsi" runat="server" GroupName="caravana" Text="Si" AutoPostBack="True" />
        &nbsp;
        <asp:RadioButton ID="RBcno" runat="server" Checked="True" GroupName="caravana" Text="No" AutoPostBack="True" />
        <br />
        <br />
        <asp:Label ID="LabelTienda" runat="server" Text="Tienda:"></asp:Label>
&nbsp;<asp:RadioButton ID="RBtsi" runat="server" AutoPostBack="True" GroupName="tienda" Text="Si" />
&nbsp;<asp:RadioButton ID="RBtno" runat="server" AutoPostBack="True" Checked="True" GroupName="tienda" Text="No" />
        <br />
        <br />
        <asp:Label ID="lblNO" runat="server" Font-Bold="True" ForeColor="Red" Text="Sin Resultados De Busqueda"></asp:Label>
        <br />
        <asp:ListBox ID="listNombres" runat="server" Height="75px" Width="338px"></asp:ListBox>
        <br />
        <br />
        <asp:Button ID="btnInformacion" runat="server" Text="Información" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reservar" />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="LabelInfo" runat="server" Text="Descripción:" Font-Bold="True" Font-Italic="True"></asp:Label>
        <br />
        <asp:Label ID="lblInfo" runat="server" Height="78px" Width="286px"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelCapacidad" runat="server" Font-Bold="True" Font-Italic="True" Text="Capacidad"></asp:Label>
&nbsp;<asp:Label ID="lblCapacidad" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelDireccion" runat="server" Text="Dirección:" Font-Bold="True" Font-Italic="True"></asp:Label>
&nbsp;<asp:Label ID="lblDireccion" runat="server" Height="20px" Width="120px"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelPostal" runat="server" Font-Bold="True" Font-Italic="True" Text="Código Postal:"></asp:Label>
&nbsp;<asp:Label ID="lblPostal" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelTelefono" runat="server" Text="Teléfono:" Font-Bold="True" Font-Italic="True"></asp:Label>
&nbsp;<asp:Label ID="lblTelefono" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelEmail" runat="server" Text="Email:" Font-Bold="True" Font-Italic="True"></asp:Label>
&nbsp;<asp:Label ID="lblEmail" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelWeb" runat="server" Text="Pagina Web:" Font-Bold="True" Font-Italic="True"></asp:Label>
&nbsp;<asp:Label ID="lblLink" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Image ID="Imagen" runat="server" Height="134px" Width="215px" />
        <br />
        <asp:Label ID="labelFiltros" runat="server" Text="0" Visible="False"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
