<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Reservar.aspx.vb" Inherits="RetoASP.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="searchbox space">
        <div class="container">
            <div class="right">
                <asp:Button ID="btnSearch" runat="server" BorderStyle="None" Text="Buscar" CssClass="btn" />
            </div>
            <div class="left">
                <asp:TextBox ID="Search" runat="server" placeholder="Buscar alojamiento..." CssClass="form-control"></asp:TextBox>
            </div>
            <div class="row">
                <div class="filtros">
                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text="Tipo de alojamieto"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="Provincia"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="Municipio"></asp:Label>
                    <asp:DropDownList ID="DropTipo" runat="server" placeholder="Email" Height="20px" Width="140px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropProvincia" runat="server" placeholder="Email" Height="20px" Width="140px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropMunicipio" runat="server" placeholder="Email" Height="20px" Width="140px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Button ID="btnMasfiltros" runat="server" Text="Más filtros" />
                    <asp:Button ID="btnMenosfiltros" runat="server" Enabled="False" Text="Menos filtros" Visible="False" />
                    <asp:Label ID="LabelRestaurante" runat="server" Text="Restaurante:"></asp:Label>
                    <asp:RadioButton ID="RBsi" runat="server" GroupName="restaurante" Text="Si" AutoPostBack="True" />
                    <asp:RadioButton ID="RBno" runat="server" Checked="True" GroupName="restaurante" Text="No" AutoPostBack="True" />
                    <asp:Label ID="LabelAutocaravana" runat="server" Text="Parking para autocaravana:"></asp:Label>
                    <asp:RadioButton ID="RBcsi" runat="server" GroupName="caravana" Text="Si" AutoPostBack="True" />
                    <asp:RadioButton ID="RBcno" runat="server" Checked="True" GroupName="caravana" Text="No" AutoPostBack="True" />
                    <asp:Label ID="LabelTienda" runat="server" Text="Tienda:"></asp:Label>
                    <asp:RadioButton ID="RBtsi" runat="server" AutoPostBack="True" GroupName="tienda" Text="Si" />
                    <asp:RadioButton ID="RBtno" runat="server" AutoPostBack="True" Checked="True" GroupName="tienda" Text="No" />
                    <asp:Label ID="lblNO" runat="server" Font-Bold="True" ForeColor="Red" Text="Sin Resultados De Busqueda" Font-Size="XX-Large"></asp:Label>
                    <asp:RadioButton ID="RBAsc" runat="server" AutoPostBack="True" Checked="True" GroupName="orden" Text="Orden ascendente" />
                    <asp:RadioButton ID="RBDesc" runat="server" AutoPostBack="True" GroupName="orden" Text="Orden descendente" />
                    <asp:Label ID="labelFiltros" runat="server" Text="0" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
     <asp:Panel ID="Panel1" runat="server" CssClass="space">
     </asp:Panel>
</asp:Content>
