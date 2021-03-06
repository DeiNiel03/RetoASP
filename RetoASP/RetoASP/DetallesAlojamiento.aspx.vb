﻿Imports MySql.Data.MySqlClient

Public Class DetallesAlojamiento
    Inherits System.Web.UI.Page

    Dim conexion As MySqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conexion = New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")
        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If
        mostrarAlojamiento()
        'comprobar si el usuario esta logeado
        If Session("Email") <> Nothing Then
            Master.FindControl("btnLogin").Visible = False
            Master.FindControl("btnRegistro").Visible = False
            Master.FindControl("btnPerfil").Visible = True
            Master.FindControl("btnCerrarSesion").Visible = True
        Else
            Master.FindControl("btnLogin").Visible = True
            Master.FindControl("btnRegistro").Visible = True
            Master.FindControl("btnPerfil").Visible = False
            Master.FindControl("btnCerrarSesion").Visible = False
        End If
    End Sub

    Sub mostrarAlojamiento()
        Try
            Dim sqlQuery As String = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, territory, municipality, postalcode, capacity, imagen, restaurant, store, autocaravana, latwgs84, lonwgs84 FROM alojamientos_fac.alojamientos WHERE signatura = '" + Session("signatura") + "'"
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
		Dim divMapa As New HtmlGenericControl("div")
		Dim botonReserva As New Button
		Dim botonMapa As New Button
		Dim provincia As String = Nothing
		Dim description As String = Nothing
		div.Attributes.Add("class", "item")
		div.Attributes.Add("class", "row")
		botonReserva.Text = "Reservar"
		botonReserva.Attributes.Add("class", "btn")
		AddHandler botonReserva.Click, AddressOf irAReservar
		Panel1.Controls.Clear()
		If Not sqlReader.HasRows Then
			lblNO.Visible = True
		Else
			lblNO.Visible = False
			While sqlReader.Read()
				idAlojamiento = sqlReader("signatura")
				Me.HiddenLat.Value = sqlReader("latwgs84").ToString
				Me.HiddenLon.Value = sqlReader("lonwgs84").ToString
				Me.HiddenNombre.Value = sqlReader("documentname").ToString
				Me.HiddenMunic.Value = sqlReader("municipality").ToString
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
				html = html + "<div class='servicios'>"
				html = html + "<img class='imgservicio' src='assets/images/baseline_face_black_48dp.png'/>"
				html = html + "<p class='textoservicio'>" + sqlReader("capacity").ToString + "</p>"
				html = html + "<img class='imgservicio' src='"
                If sqlReader("restaurant") = 1 Then
                    html = html + "assets/images/baseline_restaurant_black_48dp.png'/>"
                Else
                    html = html + "assets/images/baseline_restaurant_grey_48dp.png'/>"
				End If
				html = html + "<img class='imgservicio' src='"
                If sqlReader("store") = 1 Then
                    html = html + "assets/images/baseline_shopping_cart_black_48dp.png'/>"
                Else
                    html = html + "assets/images/baseline_shopping_cart_grey_48dp.png'/>"
				End If
				html = html + "<img class='imgservicio' src='"
                If sqlReader("autocaravana") = 1 Then
                    html = html + "assets/images/baseline_rv_hookup_black_48dp.png'/>"
                Else
                    html = html + "assets/images/baseline_rv_hookup_grey_48dp.png'/>"
				End If
                html = html + "</div>"
                html = html + "</div>"
				div.InnerHtml = html
				botonReserva.ID = idAlojamiento
				div.Controls.Add(divMapa)
				div.Controls.Add(botonReserva)
				divMapa.ID = "mapDiv"
                divMapa.Attributes.Add("class", "mapa col-sm-12")
                Panel1.Controls.Add(div)
			End While
		End If
		conexion.Close()
	End Sub

    Sub irAReservar(sender As Object, e As EventArgs)
        Response.Redirect("Reservar.aspx")
    End Sub
End Class