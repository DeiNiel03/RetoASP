Imports System.IO
'Imports System.Windows.Forms
Imports MySql.Data.MySqlClient


Public Class WebForm3
	Inherits System.Web.UI.Page
	Dim panel
	Dim conexion As New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")
	Dim id As String


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		lblUsuario.Text = Request.Params("usuario")
		panel = Panel1
		lblNO.Visible = False
		If conexion.State = ConnectionState.Closed Then
			conexion.Open()
		End If
		If Not IsPostBack Then
			cargarDatos()
			sacarNombresSinfiltros()
			deshabilitarFiltros()
		Else
			sacarNombresSinfiltros()
		End If
	End Sub

	Sub cargarDatos()
		cargarTipo()
		cargarProvincias()
		cargarMunicipio()
	End Sub

	Sub cargarTipo()

		Dim sqlQuery As String = "SELECT DISTINCT `lodgingtype` FROM alojamientos_fac.alojamientos"

		Try
			Dim adapter As New MySqlDataAdapter(sqlQuery, conexion)
			Dim tabla As New DataTable
			adapter.Fill(tabla)

			DropTipo.DataSource = tabla
			DropTipo.DataTextField = "lodgingtype"
			DropTipo.DataBind()
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

			DropProvincia.DataSource = tabla
			DropProvincia.DataTextField = "nombre"
			DropProvincia.DataValueField = "id"
			DropProvincia.DataBind()
		Catch ex As MySqlException
			'MessageBox.Show(ex.Message, "ERROR PROVINCIAS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
		'DropProvincia_SelectedIndexChanged(DropProvincia,)
	End Sub


	Sub cargarMunicipio()
		Try
			Dim adapter As New MySqlDataAdapter("SELECT DISTINCT `municipality` FROM alojamientos_fac.alojamientos WHERE territory = " + DropProvincia.SelectedValue + " AND lodgingtype = '" + DropTipo.SelectedItem.Text + "' ORDER BY `municipality` ASC", conexion)
			Dim tabla As New DataTable()
			adapter.Fill(tabla)

			DropMunicipio.DataSource = tabla
			DropMunicipio.DataTextField = "municipality"
			DropMunicipio.DataBind()
		Catch ex As MySqlException
			'MessageBox.Show(ex.Message, "ERROR MUNICIPIOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub
	Sub mostrarOrden()
		RBAsc.Visible = True
		RBDesc.Visible = True
	End Sub
	Sub ocultarOrden()
		RBAsc.Visible = False
		RBDesc.Visible = False
	End Sub
	Sub sacarNombresConFiltros()
		mostrarOrden()
		Try
			Dim sqlQuery As String

			If RBAsc.Checked = True Then
				sqlQuery = "SELECT documentname,turismdescription, address, postalcode, phone, tourismemail, web, capacity,restaurant,autocaravana,store, imagen FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND restaurant = @idRest AND autocaravana = @idCaravan AND store = @idTienda AND activo = @idActivo ORDER BY documentname ASC"
			Else
				sqlQuery = "SELECT documentname,turismdescription, address, postalcode, phone, tourismemail, web, capacity,restaurant,autocaravana,store, imagen FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND restaurant = @idRest AND autocaravana = @idCaravan AND store = @idTienda AND activo = @idActivo ORDER BY documentname DESC"
			End If

			Using sqlComm As New MySqlCommand()
				With sqlComm
					.Connection = conexion
					.CommandText = sqlQuery
					.CommandType = CommandType.Text
					.Parameters.AddWithValue("@idTipo", DropTipo.SelectedItem)
					.Parameters.AddWithValue("@idMuni", DropMunicipio.SelectedItem)
					.Parameters.AddWithValue("@idPro", CInt(DropProvincia.SelectedValue))
					.Parameters.AddWithValue("@idActivo", 1)

					If RBsi.Checked Then
						.Parameters.AddWithValue("@idRest", 1)
					Else
						.Parameters.AddWithValue("@idRest", 0)
					End If

					If RBcsi.Checked Then
						.Parameters.AddWithValue("@idCaravan", 1)
					Else
						.Parameters.AddWithValue("@idCaravan", 0)
					End If

					If RBtsi.Checked Then
						.Parameters.AddWithValue("@idTienda", 1)
					Else
						.Parameters.AddWithValue("@idTienda", 0)
					End If
				End With
				Try
					Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
					If Not sqlReader.HasRows Then
						lblNO.Visible = True
						ocultarOrden()
					End If
					While sqlReader.Read()
						Dim div As New HtmlGenericControl("div")
						div.Attributes.Add("class", "item")
						Dim html As String = ""
						html = html + "<img src='" + "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen")) + "'>"
						html = html + "<label class='lblnombre'>" + sqlReader("documentname").ToString + "</label>"
						html = html + "<label class='lbldescripcion'>" + sqlReader("turismdescription").ToString + "</label>"
						html = html + "<label class='lbldireccion'>" + sqlReader("address").ToString + "</label>"
						html = html + "<label class='lblcodpostal'>" + sqlReader("postalcode").ToString + "</label>"
						html = html + "<label class='lbltelefono'>" + sqlReader("phone").ToString + "</label>"
						html = html + "<label class='lblemail'>" + sqlReader("tourismemail").ToString + "</label>"
						html = html + "<label class='lblweb'>" + sqlReader("web").ToString + "</label>"
						html = html + "<label class='lblrestaurante'>"
						If sqlReader("restaurant") = 1 Then
							html = html + "Si"
						Else
							html = html + "No"
						End If
						html = html + "</label>"

						html = html + "<label class='lblautocaravana'>"
						If sqlReader("autocaravana") = 1 Then
							html = html + "Si"
						Else
							html = html + "No"
						End If

						html = html + "<label class='lblstore'>"
						If sqlReader("store") = 1 Then
							html = html + "Si"
						Else
							html = html + "No"
						End If
						html = html + "</label>"
						html = html + "<label class='lblcapacidad'>" + sqlReader("capacity").ToString + "</label>"

						div.InnerHtml = html
						Panel1.Controls.Add(div)
					End While
				Catch ex As MySqlException
					'MessageBox.Show("El alojamiento no esta disponible", "ERROR DE ALOJAMIENTO", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			'MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
		conexion.Close()
	End Sub

	Sub sacarNombresSinfiltros()
		mostrarOrden()
		Try
			Dim comando As String
			If RBAsc.Checked = True Then
				comando = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, postalcode, capacity, imagen, restaurant, store, autocaravana FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND activo = @idActivo ORDER BY documentname ASC"
			ElseIf RBDesc.Checked = True Then
				comando = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, postalcode, capacity, imagen, restaurant, store, autocaravana FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND activo = @idActivo ORDER BY documentname DESC"
			End If
			Using sqlComm As New MySqlCommand()
				With sqlComm
					.Connection = conexion
					.CommandText = comando
					.CommandType = CommandType.Text
					.Parameters.Add("@idTipo", MySqlDbType.VarChar).Value = DropTipo.SelectedItem
					.Parameters.Add("@idMuni", MySqlDbType.VarChar).Value = DropMunicipio.SelectedItem
					.Parameters.Add("@idPro", MySqlDbType.Int16).Value = DropProvincia.SelectedValue
					.Parameters.Add("@idActivo", MySqlDbType.Int16).Value = 1
				End With
				Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()

				Panel1.Controls.Clear()

				While sqlReader.Read()

					id = sqlReader("signatura")

					Dim div As New HtmlGenericControl("div")
					div.Attributes.Add("class", "item")
					Dim html As String = ""
					html = html + "<img src='" + "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen")) + "'>"
					html = html + "<label class='lblnombre'>" + sqlReader("documentname").ToString + "</label>"
					html = html + "<label class='lbldescripcion'>" + sqlReader("turismdescription") + "</label>"
					html = html + "<label class='lbldireccion'>" + sqlReader("address").ToString + "</label>"
					html = html + "<label class='lblcodpostal'>" + sqlReader("postalcode").ToString + "</label>"
					html = html + "<label class='lbltelefono'>" + sqlReader("phone").ToString + "</label>"
					html = html + "<label class='lblemail'>" + sqlReader("tourismemail").ToString + "</label>"
					html = html + "<label class='lblweb'>" + sqlReader("web").ToString + "</label>"

					If sqlReader("restaurant") = 1 Then
						html = html + "Si"
					Else
						html = html + "No"
					End If
					html = html + "</label>"

					html = html + "<label class='lblautocaravana'>"
					If sqlReader("autocaravana") = 1 Then
						html = html + "Si"
					Else
						html = html + "No"
					End If

					html = html + "<label class='lblstore'>"
					If sqlReader("store") = 1 Then
						html = html + "Si"
					Else
						html = html + "No"
					End If
					html = html + "</label>"
					html = html + "<label class='lblcapacidad'>" + sqlReader("capacity").ToString + "</label>"


					div.InnerHtml = html

					Dim boton As New Button
					boton.ID = id
					boton.Text = "Reservar"
					AddHandler boton.Click, AddressOf irAReservar
					Panel1.Controls.Add(div)
					Panel1.Controls.Add(boton)

				End While
				If Not sqlReader.HasRows Then
					lblNO.Visible = True
					ocultarOrden()
				Else
					lblNO.Visible = False
				End If
			End Using
		Catch ex As MySqlException
			'MessageBox.Show("El alojamiento no esta disponible", "ERROR DE ALOJAMIENTO", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub
	Sub irAReservar(sender As Object, e As EventArgs)
		Dim btn As Button = CType(sender, Button)
		Response.Redirect("Realizar.aspx?usuario=" + lblUsuario.Text + "?id=" + btn.ID)
		conexion.Close()
	End Sub
	Sub sinResultados()

	End Sub

	Protected Sub btnMasfiltros_Click(sender As Object, e As EventArgs) Handles btnMasfiltros.Click
		labelFiltros.Text = 1
		btnMasfiltros.Visible = False
		btnMasfiltros.Enabled = False

		LabelRestaurante.Visible = True
		LabelAutocaravana.Visible = True
		LabelTienda.Visible = True
		RBcno.Visible = True
		RBcsi.Visible = True
		RBno.Visible = True
		RBsi.Visible = True
		RBtno.Visible = True
		RBtsi.Visible = True


		btnMenosfiltros.Visible = True
		btnMenosfiltros.Enabled = True
	End Sub

	Protected Sub btnMenosfiltros_Click(sender As Object, e As EventArgs) Handles btnMenosfiltros.Click
		deshabilitarFiltros()
	End Sub

	Sub deshabilitarFiltros()
		labelFiltros.Text = 0
		btnMasfiltros.Visible = True
		btnMasfiltros.Enabled = True

		LabelRestaurante.Visible = False
		LabelAutocaravana.Visible = False
		LabelTienda.Visible = False
		RBcno.Visible = False
		RBcsi.Visible = False
		RBno.Visible = False
		RBsi.Visible = False
		RBtno.Visible = False
		RBtsi.Visible = False

		btnMenosfiltros.Visible = False
		btnMenosfiltros.Enabled = False
	End Sub

	Protected Sub DropProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropProvincia.SelectedIndexChanged, DropTipo.SelectedIndexChanged
		cargarMunicipio()
	End Sub

	Protected Sub SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropMunicipio.SelectedIndexChanged, RBsi.CheckedChanged, RBno.CheckedChanged, DropProvincia.SelectedIndexChanged, DropTipo.SelectedIndexChanged, RBcsi.CheckedChanged, RBcno.CheckedChanged, RBtsi.CheckedChanged, RBtno.CheckedChanged, btnMenosfiltros.Click, btnMasfiltros.Click
		metodosAEjecutar()
	End Sub

	Protected Sub Ordenacion_CheckedChanged(sender As Object, e As EventArgs) Handles RBAsc.CheckedChanged, RBDesc.CheckedChanged
		metodosAEjecutar()
	End Sub

	Sub metodosAEjecutar()
		If labelFiltros.Text = 1 Then
			sacarNombresConFiltros()
		ElseIf labelFiltros.Text = 0 Then
			deshabilitarFiltros()
			sacarNombresSinfiltros()
		End If
	End Sub
	'Sub sacarImagen()
	'	Try
	'		Dim sqlQuery As String = "SELECT imagen FROM alojamientos_fac.alojamientos WHERE documentname = @idNombre"


	'		Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select
	'			With sqlComm
	'				.Connection = conexion
	'				.CommandText = sqlQuery
	'				.CommandType = CommandType.Text
	'				.Parameters.AddWithValue("@idNombre", listNombres.SelectedItem.ToString)
	'			End With
	'			Try
	'				conexion.Open()
	'				Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
	'				While sqlReader.Read()
	'					Dim imageUrl As String = "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen"))
	'					Me.Imagen.ImageUrl = imageUrl
	'				End While

	'			Catch ex As MySqlException
	'				MessageBox.Show(ex.Message, "ERROR DE IMAGEN", MessageBoxButtons.OK, MessageBoxIcon.Error)
	'			End Try
	'		End Using
	'	Catch ex As MySql.Data.MySqlClient.MySqlException
	'		MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
	'	End Try
	'End Sub
End Class