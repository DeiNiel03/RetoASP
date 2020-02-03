Imports MySql.Data.MySqlClient

Public Class DetallesAlojamiento
    Inherits System.Web.UI.Page

    Dim conexion As MySqlConnection
	Dim signatura As String
	'Dim latitud As String
	'Dim longitud As String

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conexion = New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")
        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If
        signatura = Request.Params("signatura").ToString
        mostrarAlojamiento()
    End Sub

    Sub mostrarAlojamiento()
        Try
			Dim sqlQuery As String = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, territory, municipality, postalcode, capacity, imagen, restaurant, store, autocaravana, latwgs84, longwgs84 FROM alojamientos_fac.alojamientos WHERE signatura = '" + signatura + "'"
			Using sqlComm As New MySqlCommand()
                With sqlComm
                    .Connection = conexion
                    .CommandText = sqlQuery
                    .CommandType = CommandType.Text
                End With
                Try
                    Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
                    renderItems(sqlReader)
                Catch ex As MySqlException
                    'MessageBox.Show("El alojamiento no esta disponible", "ERROR DE ALOJAMIENTO", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        Catch ex As MySql.Data.MySqlClient.MySqlException
            'MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub renderItems(sqlReader As MySqlDataReader)
        Dim idAlojamiento As String
        Dim html As String = ""
		Dim div As New HtmlGenericControl("div")
		'Dim divMapa As New HtmlGenericControl("div")
		Dim botonReserva As New Button
		Dim botonMapa As New Button
		Dim provincia As String = Nothing
		Dim description As String = Nothing
		div.Attributes.Add("class", "item")
		div.Attributes.Add("class", "row")
		botonReserva.Text = "Reservar"
		botonReserva.Attributes.Add("class", "btn")
		AddHandler botonReserva.Click, AddressOf irAReservar
		'botonMapa.Text = "Ver en Mapa"
		'botonMapa.Attributes.Add("class", "btn")
		'AddHandler botonMapa.Click, AddressOf irAMapa
		Panel1.Controls.Clear()
		If Not sqlReader.HasRows Then
			lblNO.Visible = True
		Else
			lblNO.Visible = False
			While sqlReader.Read()
				idAlojamiento = sqlReader("signatura")
				Me.HiddenLat = sqlReader("latwgs84")
				Me.HiddenLon = sqlReader("lonwgs84")
				'Territory
				If sqlReader("territory") = 1 Then
					provincia = "Bizkaia/Vizcaya"
				ElseIf sqlReader("territory") = 2 Then
					provincia = "Araba/Alava"
				Else
					provincia = "Gipuzkoa/Guipuzcoa"
				End If
				'Description
				description = HttpUtility.HtmlDecode(sqlReader("turismdescription"))
				description = Regex.Replace(description, "<[^>]*(>|$)", "")
				If description.Length > 400 Then
					description = description.Trim().Remove(400)
				End If
				html = html + "<div class='col-sm-5'>"
				html = html + "<img class='lodging-img' src='" + "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen")) + "'>"
				html = html + "</div>"
				html = html + "<div class='col-sm-7'>"
				html = html + "<h2 class='lblnombre'>" + sqlReader("documentname").ToString + "</h2>"
				html = html + "<p class='lbllocalicacion'>" + sqlReader("municipality") + ", " + provincia + "</p>"
				html = html + "<p class='lbldescripcion'>" + description + "</p>"
				html = html + "<p class='lbldireccion'>Dirección: " + sqlReader("address").ToString + " " + sqlReader("postalcode").ToString + "</p>"
				html = html + "<p class='lbltelefono'>Telefono: " + sqlReader("phone").ToString + "</p>"
				html = html + "<p class='lblemail'> Email: " + sqlReader("tourismemail").ToString + "</p>"
				html = html + "<a class='lblweb' href='" + sqlReader("web").ToString + "'>Web: " + sqlReader("web").ToString + "</a>"
				html = html + "<p class='lblrestaurante'>Restaurante: "
				If sqlReader("restaurant") = 1 Then
					html = html + "Si"
				Else
					html = html + "No"
				End If
				html = html + "</p>"
				html = html + "<p class='lblautocaravana'>Caravana: "
				If sqlReader("autocaravana") = 1 Then
					html = html + "Si"
				Else
					html = html + "No"
				End If
				html = html + "</p>"
				html = html + "<p class='lblstore'>Tienda: "
				If sqlReader("store") = 1 Then
					html = html + "Si"
				Else
					html = html + "No"
				End If
				html = html + "</p>"
				html = html + "<p class='lblcapacidad'>Capacidad: " + sqlReader("capacity").ToString + "</p>"
				html = html + "</div>"
				div.InnerHtml = html
				botonReserva.ID = idAlojamiento
				div.Controls.Add(botonReserva)
				'divMapa.ID = "mapDiv"
				'botonReserva.ID = idAlojamiento
				'div.Controls.Add(botonReserva)

				Panel1.Controls.Add(div)
				'Panel1.Controls.Add(divMapa)
			End While
		End If
		conexion.Close()
	End Sub

	Sub irAReservar(sender As Object, e As EventArgs)
		Response.Redirect("Realizar.aspx")
	End Sub

	'Sub irAMapa(sender As Object, e As EventArgs)
	'	Dim lat As String = "?lat=" + latitud
	'	Dim lon As String = "?lon=" + longitud
	'	Response.Redirect("DetallesAlojamiento.aspx" + latitud + longitud)
	'End Sub

End Class