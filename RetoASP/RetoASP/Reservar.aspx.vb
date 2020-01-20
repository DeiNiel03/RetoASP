Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient


Public Class WebForm3
	Inherits System.Web.UI.Page
	Dim panel
	Dim conexion As New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		panel = Panel1
		deshabilitar()
		If Not IsPostBack Then
			cargarDatos()
			deshabilitarFiltros()
		End If
	End Sub

	Sub cargarDatos()
		cargarTipo()
		cargarProvincias()
		cargarMunicipio()
		sacarNombresSinFiltros()
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
			MessageBox.Show(ex.Message, "ERROR TIPO", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub

	Sub cargarProvincias()
		Dim sqlQuery As String = "SELECT `nombre`, `id` FROM alojamientos_fac.provincias"

		Try

			Dim adapter As New MySqlDataAdapter(sqlQuery, conexion)
			Dim tabla As New DataTable
			adapter.Fill(tabla)

			DropProvincia.DataSource = tabla
			DropProvincia.DataTextField = "nombre"
			DropProvincia.DataValueField = "id"
			DropProvincia.DataBind()
		Catch ex As MySqlException
			MessageBox.Show(ex.Message, "ERROR PROVINCIAS", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
			MessageBox.Show(ex.Message, "ERROR MUNICIPIOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub

	Sub sacarNombresConFiltros()

		Try
			Dim sqlQuery As String = "SELECT documentname, FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND restaurant = @idRest AND autocaravana = @idCaravan AND store = @idTienda"

			Using sqlComm As New MySqlCommand()
				With sqlComm
					.Connection = conexion
					.CommandText = sqlQuery
					.CommandType = CommandType.Text
					.Parameters.AddWithValue("@idTipo", DropTipo.SelectedItem)
					.Parameters.AddWithValue("@idMuni", DropMunicipio.SelectedItem)
					.Parameters.AddWithValue("@idPro", CInt(DropProvincia.SelectedValue))

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
					conexion.Open()
					Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
					If Not sqlReader.HasRows Then
						lblNO.Visible = True
						'listNombres.Visible = False
					End If
					While sqlReader.Read()
						'listNombres.Visible = True
						'listNombres.Items.Add(sqlReader("documentname"))
					End While
				Catch ex As MySqlException
					MessageBox.Show(ex.Message, "ERROR NOMBRE DE ALOJAMIENTOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
		conexion.Close()
	End Sub

	Sub sacarNombresSinfiltros()
		Try

			Dim comando As New MySqlCommand("SELECT `documentname` as 'Nombre', `address` as 'Dirección', `phone` as 'Numero de teléfono', `tourismemail` as 'Correo electrónico', `web` as 'Página Web', `postalcode` as 'Código postal', `capacity` as 'Capacidad' FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro", conexion)
			comando.Parameters.Add("@idTipo", MySqlDbType.VarChar).Value = DropTipo.SelectedItem
			comando.Parameters.Add("@idMuni", MySqlDbType.VarChar).Value = DropMunicipio.SelectedItem
			comando.Parameters.Add("@idPro", MySqlDbType.Int16).Value = DropProvincia.SelectedValue

			Dim tabla As New DataTable()
			Dim adapter As New MySqlDataAdapter(comando)

			adapter.Fill(tabla)

			If tabla.Rows.Count = 0 Then
				lblNO.Visible = True
			Else
				'Dim div As New HtmlGenericControl("div")
				'div.Attributes.Add("class", "item")
				'Dim html As String = ""
				'html = html + "<label class='nombre'>" + tabla + "</label>"
				'form1.Controls.Add(div)

				GridView1.DataSource = tabla
				GridView1.DataBind()
				lblNombre.Text = "documentname"
			End If

		Catch ex As MySqlException
			MessageBox.Show(ex.Message, "ERROR NOMBRE SIN FILTROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	'Sub sacarNombresSinFiltros()
	'	Try
	'		Dim sqlQuery As String = "SELECT documentname, address, phone, tourismemail, web, postalcode, capacity FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND activo = @idactivo"

	'		Using sqlComm As New MySqlCommand()
	'			With sqlComm
	'				.Connection = conexion
	'				.CommandText = sqlQuery
	'				.CommandType = CommandType.Text
	'				.Parameters.AddWithValue("@idTipo", DropTipo.SelectedItem)
	'				.Parameters.AddWithValue("@idMuni", DropMunicipio.SelectedItem)
	'				.Parameters.AddWithValue("@idPro", CInt(DropProvincia.SelectedValue))
	'				.Parameters.AddWithValue("@idactivo", 1)
	'			End With
	'			Try
	'				conexion.Open()
	'				Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()

	'				While sqlReader.Read()
	'					habilitar()

	'					Dim div As New HtmlGenericControl("div")
	'					div.Attributes.Add("class", "item")
	'					Dim html As String = ""
	'					html = html + "<label class='nombre'>" + sqlReader("documentname") + "</label>"
	'					html = html + "<p class='direccion'>" + sqlReader("address") + "</p>"

	'					div.InnerText = html
	'					Me.Controls.Add(div)

	'					'lblNombre.Text = sqlReader("documentname")
	'					'lblDireccion.Text = sqlReader("address")
	'					'lblTelefono.Text = sqlReader("phone")
	'					'lblEmail.Text = sqlReader("tourismemail")
	'					'lblLink.Text = sqlReader("web")
	'					'lblPostal.Text = sqlReader("postalcode")
	'					'lblCapacidad.Text = sqlReader("capacity")
	'				End While
	'				If Not sqlReader.HasRows Then
	'					lblNO.Visible = True
	'				End If
	'			Catch ex As MySqlException
	'				MessageBox.Show(ex.Message, "ERROR NOMBRE DE ALOJAMIENTOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
	'			End Try
	'		End Using
	'	Catch ex As MySql.Data.MySqlClient.MySqlException
	'		MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
	'	End Try
	'	conexion.Close()
	'End Sub

	Sub deshabilitar()
		lblNombre.Visible = False
		lblDireccion.Visible = False
		lblCapacidad.Visible = False
		lblPostal.Visible = False
		lblEmail.Visible = False
		lblInfo.Visible = False
		lblTelefono.Visible = False
		lblLink.Visible = False
		Imagen.Visible = False

		LabelNombre.Visible = False
		LabelDireccion.Visible = False
		LabelInfo.Visible = False
		LabelEmail.Visible = False
		LabelTelefono.Visible = False
		LabelWeb.Visible = False
		LabelPostal.Visible = False
		LabelCapacidad.Visible = False
		LabelImagen.Visible = False

		lblNO.Visible = False
	End Sub

	Sub habilitar()
		lblNombre.Visible = True
		lblDireccion.Visible = True
		lblPostal.Visible = True
		lblEmail.Visible = True
		lblInfo.Visible = True
		lblTelefono.Visible = True
		lblCapacidad.Visible = True
		lblLink.Visible = True
		Imagen.Visible = True

		LabelNombre.Visible = True
		LabelImagen.Visible = True
		LabelPostal.Visible = True
		LabelCapacidad.Visible = True
		LabelDireccion.Visible = True
		LabelInfo.Visible = True
		LabelEmail.Visible = True
		LabelTelefono.Visible = True
		LabelWeb.Visible = True
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

	Protected Sub DropMunicipio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropMunicipio.SelectedIndexChanged, RBsi.CheckedChanged, RBno.CheckedChanged, DropProvincia.SelectedIndexChanged, DropTipo.SelectedIndexChanged, RBcsi.CheckedChanged, RBcno.CheckedChanged, RBtsi.CheckedChanged, RBtno.CheckedChanged, btnMenosfiltros.Click, btnMasfiltros.Click

		If labelFiltros.Text = 1 Then
			'listNombres.Items.Clear()
			sacarNombresSinFiltros()
		ElseIf labelFiltros.Text = 0 Then
			deshabilitarFiltros()
			'listNombres.Items.Clear()
			sacarNombresSinFiltros()
		End If

	End Sub
	Sub cargarInformacion()
		Try
			Dim sqlQuery As String = "SELECT turismdescription, address, phone, tourismemail, web, postalcode, capacity FROM alojamientos_fac.alojamientos WHERE documentname = @idNombre"

			Using sqlComm As New MySqlCommand()
				With sqlComm
					.Connection = conexion
					.CommandText = sqlQuery
					.CommandType = CommandType.Text
					'.Parameters.AddWithValue("@idNombre", listNombres.SelectedItem.ToString)
				End With
				Try
					conexion.Open()
					Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
					While sqlReader.Read()
						lblCapacidad.Text = sqlReader("capacity")
						lblInfo.Text = sqlReader("turismdescription")
						lblDireccion.Text = sqlReader("address")
						lblPostal.Text = sqlReader("postalcode")
						lblTelefono.Text = sqlReader("phone")
						lblEmail.Text = sqlReader("tourismemail")
						lblLink.Text = sqlReader("web")
					End While
				Catch ex As MySqlException
					MessageBox.Show(ex.Message, "ERROR DE INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
		conexion.Close()
	End Sub

	Sub sacarImagen()
		Try
			Dim sqlQuery As String = "SELECT imagen FROM alojamientos_fac.alojamientos WHERE documentname = @idNombre"


			Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select
				With sqlComm
					.Connection = conexion
					.CommandText = sqlQuery
					.CommandType = CommandType.Text
					'.Parameters.AddWithValue("@idNombre", listNombres.SelectedItem.ToString)
				End With
				Try
					conexion.Open()
					Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
					While sqlReader.Read()
						Dim imageUrl As String = "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen"))
						Me.Imagen.ImageUrl = imageUrl
					End While

				Catch ex As MySqlException
					MessageBox.Show(ex.Message, "ERROR DE IMAGEN", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub
End Class