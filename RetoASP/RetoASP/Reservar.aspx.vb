Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class WebForm3
	Inherits System.Web.UI.Page
	Dim conexion As New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo")
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

		Dim sqlQuery As String = "SELECT DISTINCT `lodgingtype` FROM prueba.alojamientos"

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
		Dim sqlQuery As String = "SELECT `nombre`, `id` FROM prueba.provincias"

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

			Dim adapter As New MySqlDataAdapter("SELECT DISTINCT `municipality` FROM prueba.alojamientos WHERE territory = " + DropProvincia.SelectedValue + " AND lodgingtype = '" + DropTipo.SelectedItem.Text + "' ORDER BY `municipality` ASC", conexion)
			Dim tabla As New DataTable()
			adapter.Fill(tabla)

			DropMunicipio.DataSource = tabla
			DropMunicipio.DataTextField = "municipality"
			DropMunicipio.DataBind()
		Catch ex As MySqlException
			MessageBox.Show(ex.Message, "ERROR MUNICIPIOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub
	Sub sacarImagen()
		Try
			Dim connString As String = "server=188.213.5.150;Port=3306; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = "SELECT imagen FROM alojamientos WHERE signatura = @idimagen"

			Using sqlConn As New MySqlConnection(connString)
				Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select
					With sqlComm
						.Connection = sqlConn
						.CommandText = sqlQuery
						.CommandType = CommandType.Text
						.Parameters.AddWithValue("@idimagen", 2)
					End With
					Try
						sqlConn.Open()
						Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
						While sqlReader.Read()
							Dim imageUrl As String = "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen"))
							Me.Imagen.ImageUrl = imageUrl
						End While

					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR DE IMAGEN", MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Try
				End Using
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub



	Sub sacarNombres()

		Try
			Dim sqlQuery As String = "SELECT documentname FROM prueba.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND restaurant = @idRest AND autocaravana = @idCaravan AND store = @idTienda"

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
						listNombres.Visible = False
					End If
					While sqlReader.Read()
						listNombres.Visible = True
						listNombres.Items.Add(sqlReader("documentname"))
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

	Sub sacarNombresSinFiltros()
		Try
			Dim sqlQuery As String = "SELECT documentname FROM prueba.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro"

			Using sqlComm As New MySqlCommand()
				With sqlComm
					.Connection = conexion
					.CommandText = sqlQuery
					.CommandType = CommandType.Text
					.Parameters.AddWithValue("@idTipo", DropTipo.SelectedItem)
					.Parameters.AddWithValue("@idMuni", DropMunicipio.SelectedItem)
					.Parameters.AddWithValue("@idPro", CInt(DropProvincia.SelectedValue))
				End With
				Try
					conexion.Open()
					Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
					If Not sqlReader.HasRows Then
						lblNO.Visible = True
						listNombres.Visible = False
					End If
					While sqlReader.Read()
						listNombres.Visible = True
						listNombres.Items.Add(sqlReader("documentname"))
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


	'Protected Sub DropMunicipio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropMunicipio.SelectedIndexChanged, DropTipo.SelectedIndexChanged, DropProvincia.SelectedIndexChanged
	'	cargarMunicipio()
	'	sacarNombres()
	'End Sub



	Sub deshabilitar()
		lblDireccion.Visible = False
		lblEmail.Visible = False
		lblInfo.Visible = False
		lblTelefono.Visible = False
		HyperLinkWeb.Visible = False
		Imagen.Visible = False

		LabelDireccion.Visible = False
		LabelInfo.Visible = False
		LabelEmail.Visible = False
		LabelTelefono.Visible = False
		LabelWeb.Visible = False

		lblNO.Visible = False
	End Sub

	Sub habilitar()
		lblDireccion.Visible = True
		lblEmail.Visible = True
		lblInfo.Visible = True
		lblTelefono.Visible = True
		HyperLinkWeb.Visible = True
		Imagen.Visible = True

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
			listNombres.Items.Clear()
			sacarNombres()
		ElseIf labelFiltros.Text = 0 Then
			deshabilitarFiltros()
			listNombres.Items.Clear()
			sacarNombresSinFiltros()
		End If

	End Sub
End Class