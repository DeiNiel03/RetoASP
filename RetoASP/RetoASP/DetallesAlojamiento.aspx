<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.Master" CodeBehind="DetallesAlojamiento.aspx.vb" Inherits="RetoASP.DetallesAlojamiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src='https://api.mapbox.com/mapbox-gl-js/v1.7.0/mapbox-gl.js'></script>
    <link href='https://api.mapbox.com/mapbox-gl-js/v1.7.0/mapbox-gl.css' rel='stylesheet' />
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
    <script>
        mapboxgl.accessToken = 'pk.eyJ1Ijoiam9zZW1lbG9ycmlldGEiLCJhIjoiY2s2Mjl3MThtMDlobDNsdDZkeHIwcGZ4cCJ9.NyW3KGVedov9bJrd3Wn3JQ';

        var lat = parseFloat(document.getElementById("ContentPlaceHolder1_HiddenLat").value.replace(",", "."));
        var lon = parseFloat(document.getElementById("ContentPlaceHolder1_HiddenLon").value.replace(",", "."));
        var map = new mapboxgl.Map({
            container: 'ContentPlaceHolder1_mapDiv',
            style: 'mapbox://styles/mapbox/satellite-streets-v9',
            center: [lon, lat],
            zoom: 15
        });

        new mapboxgl.Marker()
            .setLngLat([lon, lat])
            .setPopup(new mapboxgl.Popup({ offset: 25 }) // add popups
                .setHTML('<h3>' + 'Alojamiento' + '</h3><p>' + 'Mu gonico' + '</p>'))
            .addTo(map);
    </script>
</asp:Content>

