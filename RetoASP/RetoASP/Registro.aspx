<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registro.aspx.vb" Inherits="RetoASP.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Size="X-Large">Registro</asp:Label>
            <br />
            <br />
            <asp:TextBox ID="TBDni" runat="server" placeholder="DNI"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TBDni" ErrorMessage="DNI no valido" ValidationExpression="^(([A-Z]\d{8})|(\d{8}[A-Z]))$" ValidationGroup="grupo" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBDni" ErrorMessage="DNI obligatorio" ValidationGroup="grupo" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
            &nbsp;&nbsp;&nbsp;&nbsp;<br />
            <br />
            <asp:TextBox ID="TBEmail" runat="server" placeholder="Email"></asp:TextBox>
            <asp:Label ID="Label10" runat="server" ForeColor="Red" Text="*"></asp:Label>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TBEmail" ErrorMessage="Email no valido" Font-Size="Small" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="grupo"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TBEmail" ErrorMessage="Email obligatorio" Font-Size="Small" ForeColor="Red" ValidationGroup="grupo"></asp:RequiredFieldValidator>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:TextBox ID="TBNombre" runat="server" placeholder="Nombre"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TBNombre" ErrorMessage="Nombre obligatorio" ValidationGroup="grupo" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:TextBox ID="TBApellido" runat="server" placeholder="Apellido"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TBApellido" ErrorMessage="Apellido obligatorio" ValidationGroup="grupo" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:TextBox ID="TBPass" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TBPass" ErrorMessage="Contraseña obligatoria" ValidationGroup="grupo" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:TextBox ID="TBPass2" runat="server" TextMode="Password" placeholder="Repita la contraseña"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TBPass" ControlToValidate="TBPass2" ErrorMessage="Las contraseñas no coinciden" ValidationGroup="grupo" Font-Size="Small" ForeColor="Red"></asp:CompareValidator>
            <br />
            <br />
            <asp:TextBox ID="TBTel" runat="server" placeholder="Número de teléfono" type="number" TextMode="Phone"></asp:TextBox>
            <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TBTel" ErrorMessage="Telefono obligatorio" ValidationGroup="grupo" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TBTel" ErrorMessage="Telefono no valido" Font-Size="Small" ForeColor="Red" ValidationExpression="^\d{9}$" ValidationGroup="grupo"></asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
&nbsp;&nbsp;
            <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Campos de obligado cumplimiento."></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnRegistro" runat="server" BorderStyle="None" Text="Registrarme" ValidationGroup="grupo" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btVolver" runat="server" BorderStyle="None" Text="Volver" Height="22px" />
        </div>
    </form>
</body>
</html>
