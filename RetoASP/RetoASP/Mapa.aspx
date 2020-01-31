<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Mapa.aspx.vb" Inherits="RetoASP.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src='https://api.mapbox.com/mapbox-gl-js/v1.7.0/mapbox-gl.js'></script>
    <link href='https://api.mapbox.com/mapbox-gl-js/v1.7.0/mapbox-gl.css' rel='stylesheet' />
    
</head>
<body>
    <div id="mapdiv" style="width:80%; height:500px;">  
        </div>
    <form id="form1" runat="server">
          
    </form>
    <script>
        mapboxgl.accessToken = 'pk.eyJ1Ijoiam9zZW1lbG9ycmlldGEiLCJhIjoiY2s2Mjl3MThtMDlobDNsdDZkeHIwcGZ4cCJ9.NyW3KGVedov9bJrd3Wn3JQ';
        var map = new mapboxgl.Map({
            container: 'mapdiv',
            style: 'mapbox://styles/mapbox/streets-v11',
            center: [-2.9646428, 43.2842863],
            zoom: 15
        });
    </script>
</body>
    
</html>
