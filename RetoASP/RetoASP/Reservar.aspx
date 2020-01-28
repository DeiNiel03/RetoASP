<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reservar.aspx.vb" Inherits="RetoASP.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 500px;
        }
        </style>
</head>
<body style="height: 977px">
    <form id="form1" runat="server" class="auto-style1">
        <asp:Label ID="lblUsuario" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Tipo de alojamieto"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Provincia"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="Municipio"></asp:Label>
        <br />
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
        <asp:Label ID="lblNO" runat="server" Font-Bold="True" ForeColor="Red" Text="Sin Resultados De Busqueda" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="RBAsc" runat="server" AutoPostBack="True" Checked="True" GroupName="orden" Text="Orden ascendente" />
        <asp:RadioButton ID="RBDesc" runat="server" AutoPostBack="True" GroupName="orden" Text="Orden descendente" />
        <br />
        <br />
        <br />
        <asp:Label ID="labelFiltros" runat="server" Text="0" Visible="False"></asp:Label>
        <br />
     <asp:Panel ID="Panel1" runat="server">
     </asp:Panel>

    </form>
     
</body>
</html>
