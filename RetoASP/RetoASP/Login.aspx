<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Login.aspx.vb" Inherits="RetoASP.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="Iniciar Sesión"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="TBEmail" runat="server" placeholder="Email"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBEmail" ErrorMessage="Escriba un email" Font-Size="Small" ForeColor="Red" ValidationGroup="grupo2"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:TextBox ID="TBPass" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
    <br />
    <br />
&nbsp;<asp:Button ID="btnLogin" runat="server" BorderStyle="None" Text="Login" ValidationGroup="grupo2" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btRegistro" runat="server" BorderStyle="None" Text="Registrarme" />
    <br />
</asp:Content>
