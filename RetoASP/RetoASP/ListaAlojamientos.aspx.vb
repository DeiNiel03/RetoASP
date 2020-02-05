Imports MySql.Data.MySqlClient

Public Class WebForm3
    Inherits System.Web.UI.Page
    Dim conexion As New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		lblNO.Visible = True
		If conexion.State = ConnectionState.Closed Then
			conexion.Open()
		End If
		If Not IsPostBack Then
			cargarDatos()
		End If
		mostrarAlojamientos()
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

	Sub irADetalles(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Session("signatura") = btn.ID
        Response.Redirect("DetallesAlojamiento.aspx")
        conexion.Close()
	End Sub

	Protected Sub SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckBoxProvincia.SelectedIndexChanged, CheckBoxTipo.SelectedIndexChanged, CheckBoxCarac.SelectedIndexChanged, RBAsc.CheckedChanged, RBDesc.CheckedChanged
		mostrarAlojamientos()
	End Sub

	Sub cargarDatos()
		cargarTipo()
		cargarProvincias()
	End Sub

	Sub cargarTipo()
		Dim sqlQuery As String = "SELECT DISTINCT `lodgingtype` FROM alojamientos_fac.alojamientos"
		Try
			Dim adapter As New MySqlDataAdapter(sqlQuery, conexion)
			Dim tabla As New DataTable
			adapter.Fill(tabla)
			CheckBoxTipo.DataSource = tabla
			CheckBoxTipo.DataTextField = "lodgingtype"
			CheckBoxTipo.DataBind()
		Catch ex As MySqlException
			'MessageBox.Show(ex.Message, "ERROR TIPO", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Sub cargarProvincias()
		Dim sqlQuery As String = "SELECT `nombre`, `id` FROM alojamientos_fac.provincias ORDER BY `nombre` ASC"
		Try
			Dim adapter As New MySqlDataAdapter(sqlQuery, conexion)
			Dim tabla As New DataTable
			adapter.Fill(tabla)
			CheckBoxProvincia.DataSource = tabla
			CheckBoxProvincia.DataTextField = "nombre"
			CheckBoxProvincia.DataValueField = "id"
			CheckBoxProvincia.DataBind()
		Catch ex As MySqlException
			'MessageBox.Show(ex.Message, "ERROR PROVINCIAS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
		'DropProvincia_SelectedIndexChanged(DropProvincia,)
	End Sub

	Sub mostrarOrden()
		RBAsc.Visible = True
		RBDesc.Visible = True
	End Sub

	Sub ocultarOrden()
		RBAsc.Visible = False
		RBDesc.Visible = False
	End Sub

	Sub mostrarAlojamientos()
		Panel1.Controls.Clear()
		mostrarOrden()
		Try
			Dim sqlQuery As String = crearQuery()
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

	Function crearQuery() As String
		Dim sqlQuery As String = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, territory, municipality, postalcode, capacity, imagen, restaurant, store, autocaravana FROM alojamientos_fac.alojamientos WHERE"
		Dim tipos As String = crearQueryTipos()
		Dim provincias As String = crearQueryProvincias()
		Dim caracteristicas As String = crearQueryCaracteristicas()
		Dim orden As String = crearQueryOrden()
		Dim texto As String = crearQueryTexto()
		If Not tipos.Equals("") Then
			sqlQuery += tipos
		End If
		If Not provincias.Equals("") Then
			sqlQuery += provincias
		End If
		If Not caracteristicas.Equals("") Then
			sqlQuery += caracteristicas
		End If
		If Not Search.Text = "" Then
			sqlQuery += texto
		End If
		If Not orden.Equals("") Then
			sqlQuery += orden
		End If
		Return sqlQuery
	End Function

	Function crearQueryTipos() As String
		Dim sqlQuery As String = ""
		Dim tipos As String = ""
		Dim count As Integer = 0
		For Each i In CheckBoxTipo.Items
			If i.Selected Then
				tipos += "'" & i.Value & "',"
				count += 1
			End If
		Next
		If count > 0 Then
			tipos = tipos.Trim().Substring(0, tipos.Length - 1)
			sqlQuery = " lodgingtype in (" & tipos & ")"
		End If
		If count = 0 Then
			sqlQuery = " lodgingtype in ('Albergues', 'Agroturismos', 'Casas Rurales', 'Campings')"
		End If
		Return sqlQuery
	End Function

	Function crearQueryProvincias() As String
		Dim sqlQuery As String = ""
		Dim provincias As String = ""
		Dim count As Integer = 0
		For Each i In CheckBoxProvincia.Items
			If i.Selected Then
				provincias += i.Value & ","
				count += 1
			End If
		Next
		If count > 0 Then
			sqlQuery = " AND territory in (" & provincias.Trim().Remove(provincias.Length - 1) & ")"
		End If
		If count = 0 Then
			sqlQuery = " AND territory in (1,2,3)"
		End If
		Return sqlQuery
	End Function

	Function crearQueryCaracteristicas() As String
		Dim sqlQuery As String = ""
		For Each i In CheckBoxCarac.Items
			If i.Selected Then
				If i.Selected Then
					If i.value.Equals("restaurante") Then
						sqlQuery += " AND restaurant = 1"
					End If
					If i.value.Equals("autocaravana") Then
						sqlQuery += " AND autocaravana = 1"
					End If
					If i.value.Equals("tienda") Then
						sqlQuery += " AND store = 1"
					End If
				End If
			End If
		Next
		Return sqlQuery
	End Function

	Function crearQueryOrden() As String
		Dim sqlQuery As String = ""
		If RBAsc.Checked = True Then
			sqlQuery = " AND activo = 1 ORDER BY documentname ASC LIMIT 30"
		Else
			sqlQuery = " AND activo = 1 ORDER BY documentname DESC LIMIT 30"
		End If
		Return sqlQuery
	End Function

	Function crearQueryTexto() As String
		Dim sqlQuery As String = ""
		sqlQuery = " AND (UPPER(documentname) LIKE '%" + Search.Text.ToUpper + "%' OR UPPER(municipality) LIKE '%" + Search.Text.ToUpper + "%')"
		Return sqlQuery
	End Function

	Sub renderItems(sqlReader As MySqlDataReader)
		Dim idAlojamiento As String = Nothing
		Dim provincia As String = Nothing
		Dim description As String = Nothing
		Panel1.Controls.Clear()
		If Not sqlReader.HasRows Then
			lblNO.Visible = True
			ocultarOrden()
		Else
			lblNO.Visible = False
			While sqlReader.Read()
				idAlojamiento = sqlReader("signatura")
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
				Dim html As String = ""
				Dim div As New HtmlGenericControl("div")
				div.Attributes.Add("class", "row item")
                html = html + "<div Class='col-md-12 col-lg-5'>"
                html = html + "<img class='lodging-img' src='" + "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen")) + "'>"
				html = html + "</div>"
                html = html + "<div class='col-md-12 col-lg-7'>"
                html = html + "<h2 class='lblnombre'>" + sqlReader("documentname").ToString + "</h2>"
				html = html + "<p class='lbllocalicacion'>" + sqlReader("municipality") + ", " + provincia + "</p>"
				html = html + "<p class='lbldescripcion'>" + description + "</p>"
				html = html + "<div class='servicios'>"
				html = html + "<img class='imgservicio' src='assets/images/baseline_face_black_48dp.png'/>"
				html = html + "<p class='textoservicio'>" + sqlReader("capacity").ToString + "</p>"
				html = html + "<img class='imgservicio' src='"
				If sqlReader("restaurant").ToString = "1" Then
					html = html + "assets/images/baseline_restaurant_black_48dp.png'/>"
				Else
					html = html + "assets/images/baseline_restaurant_grey_48dp.png'/>"
				End If
				html = html + "<img class='imgservicio' src='"
				If sqlReader("store").ToString = "1" Then
					html = html + "assets/images/baseline_shopping_cart_black_48dp.png'/>"
				Else
					html = html + "assets/images/baseline_shopping_cart_grey_48dp.png'/>"
				End If
				html = html + "<img class='imgservicio' src='"
				If sqlReader("autocaravana").ToString = "1" Then
					html = html + "assets/images/baseline_rv_hookup_black_48dp.png'/>"
				Else
					html = html + "assets/images/baseline_rv_hookup_grey_48dp.png'/>"
				End If
				html = html + "</div>"
				html = html + "</div>"
				div.InnerHtml = html
				Dim boton As New Button
				boton.Text = "Ver Detalles"
				boton.Attributes.Add("class", "btn")
				AddHandler boton.Click, AddressOf irADetalles
				boton.ID = idAlojamiento
                div.Controls.Add(boton)
                Panel1.Controls.Add(div)
            End While
        End If
    End Sub

	Protected Sub Search_TextChanged(sender As Object, e As EventArgs) Handles Search.TextChanged
		filtrarPorTexto()
	End Sub

	Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
		filtrarPorTexto()
	End Sub

	Sub filtrarPorTexto()

	End Sub
End Class