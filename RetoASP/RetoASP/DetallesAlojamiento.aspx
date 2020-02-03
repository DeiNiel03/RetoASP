<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="DetallesAlojamiento.aspx.vb" Inherits="RetoASP.DetallesAlojamiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:HiddenField ID="HiddenLat" runat="server" />
        <asp:HiddenField ID="HiddenLon" runat="server" />
        <div class="row">
            <div class="col-sm-12">
                <asp:Panel ID="Panel1" runat="server" CssClass="space">
                    <asp:Label ID="lblNO" runat="server" Font-Bold="True" ForeColor="Red" Text="Sin Resultados De Busqueda" Font-Size="XX-Large"></asp:Label>
                </asp:Panel>
            </div>
        </div>
    </div>
    <!--<script>
        /*mapboxgl.accessToken = 'pk.eyJ1Ijoiam9zZW1lbG9ycmlldGEiLCJhIjoiY2s2Mjl3MThtMDlobDNsdDZkeHIwcGZ4cCJ9.NyW3KGVedov9bJrd3Wn3JQ';

        var lat = document.getElementByID("HiddenLat").innerHTML;
        var lon = document.getElementByID("HiddenLon").innerHTML;
        var map = new mapboxgl.Map({
            container: 'mapdiv',
            style: 'mapbox://styles/mapbox/streets-v11',
            center: [lat, lon],
            zoom: 15
        });  */
    </script>-->
</asp:Content>

