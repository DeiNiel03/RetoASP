﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="Reservar.aspx.vb" Inherits="RetoASP.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container space">
        <div class="row">
            <div class="col-sm-5">
                <asp:Image ID="Image1" runat="server" />
            </div>
            <div class="col-sm-7">
                <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>
                <asp:Label ID="lblNombre" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Dirección:"></asp:Label>
                <asp:Label ID="lblDirecion" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Capacidad:"></asp:Label>
                <asp:Label ID="lblCapacidad" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="container space">
        <div class="row">
            <div class="col-sm-3">
                <asp:Label ID="Label5" runat="server" Text="Fecha de entrada:" CssClass="label"></asp:Label>
                <br/>
                <asp:Calendar ID="CalendarEntrada" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
            </div>
            <div class="col-sm-3">
                <asp:Label ID="Label6" runat="server" Text="Fecha de salida:" CssClass="label"></asp:Label>
                <br />
                <asp:Calendar ID="CalendarSalida" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
            </div>
            <div class="col-sm-3">
                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Número de personas"></asp:Label>
                <br />
                <asp:TextBox ID="TBPersonas" runat="server" placeholder=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBPersonas" Font-Size="Small" ForeColor="Red">El campo no puede quedar vacio</asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-12">
                <div class="space">
                    <asp:Button ID="btnRealizar" CssClass="btn" runat="server" Text="Realizar la reserva" />
                </div>
            </div>
         </div>
    </div>
</asp:Content>
