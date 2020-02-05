<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="ListaAlojamientos.aspx.vb" Inherits="RetoASP.WebForm3" %>

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
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="filtros col-sm-4 col-md-3">
                <asp:Panel ID="Panel2" runat="server" CssClass="space">
                    <!-- tipo de alojamiento -->
                    <asp:Label ID="Label1" runat="server" Text="Tipo de alojamiento" CssClass="filter-title"></asp:Label>
                    <asp:CheckBoxList ID="CheckBoxTipo" runat="server" AutoPostBack="True"></asp:CheckBoxList>
                    <!-- provincias -->
                    <asp:Label ID="Label2" runat="server" Text="Provincia" CssClass="filter-title"></asp:Label>
                    <asp:CheckBoxList ID="CheckBoxProvincia" runat="server" AutoPostBack="True"></asp:CheckBoxList>
                    <!-- caracteristicas -->
                    <asp:Label ID="Label3" runat="server" Text="Caracteristicas" CssClass="filter-title"></asp:Label>
                    <asp:CheckBoxList ID="CheckBoxCarac" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="restaurante">Restaurante</asp:ListItem>
                        <asp:ListItem Value="autocaravana">Autocaravana</asp:ListItem>
                        <asp:ListItem Value="tienda">Tienda</asp:ListItem>
                    </asp:CheckBoxList>
                    <!-- orden -->
                    <asp:Label ID="Label4" runat="server" Text="Ordenar" CssClass="filter-title"></asp:Label>
                    <asp:RadioButton ID="RBAsc" runat="server" AutoPostBack="True" Checked="True" GroupName="orden" Text="Orden ascendente" />
                    <asp:RadioButton ID="RBDesc" runat="server" AutoPostBack="True" GroupName="orden" Text="Orden descendente" />
                </asp:Panel>
            </div>
            <div class="col-sm-8 col-md-9">
                <asp:Panel ID="Panel1" runat="server" CssClass="space">
                    <asp:Label ID="lblNO" runat="server" Font-Bold="True" ForeColor="Red" Text="Sin Resultados De Busqueda" Font-Size="XX-Large"></asp:Label>
                </asp:Panel>
            </div>
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" />
</asp:Content>
