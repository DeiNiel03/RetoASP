<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Elejir.aspx.vb" Inherits="RetoASP.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnReservar" runat="server" Font-Bold="True" Height="26px" Text="Hacer una reserva" Width="158px" />
    <br />
    <br />
    <asp:Button ID="btnVer" runat="server" Font-Bold="True" Font-Italic="False" Height="26px" Text="Mis reservas" Width="158px" />
</asp:Content>
