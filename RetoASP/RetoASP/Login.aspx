<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Login.aspx.vb" Inherits="RetoASP.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login">
        <div class="login-wrap">
            <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="Iniciar Sesión"></asp:Label>
            <div class="login-inner">
                <asp:TextBox ID="TBEmail" runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBEmail" ErrorMessage="Escriba un email" Font-Size="Small" ForeColor="Red" ValidationGroup="grupo2"></asp:RequiredFieldValidator>
                <asp:TextBox ID="TBPass" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnLogin" runat="server" BorderStyle="None" Text="Login" ValidationGroup="grupo2" CssClass="btn" />
            <asp:LinkButton ID="btnRegistro" runat="server" CssClass="ir-a-registro">¿Todavia no tienes una cuenta?</asp:LinkButton>
        </div>
    </div>
</asp:Content>
