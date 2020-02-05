<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Perfil.aspx.vb" Inherits="RetoASP.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container space">
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblId" runat="server"></asp:Label>
                <asp:Label ID="Label3" runat="server" Text="Municipio"></asp:Label>
                <asp:Label ID="Label7" runat="server" Text="Nº de personas"></asp:Label>
                <asp:Panel ID="Panel1" runat="server">
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
