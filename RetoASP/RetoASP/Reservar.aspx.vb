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
		End If
		sacarNombres()


	End Sub

	Sub cargarDatos()
		cargarTipo()
		cargarProvincias()
		cargarMunicipio()
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

	Sub restaurante()
		If RBsi.Checked Then

		End If
	End Sub

	Sub carvana()

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
			Dim sqlQuery As String = "SELECT documentname FROM prueba.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND restaurant = @idRest AND autocaravana = @idCaravan"

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
				End With
				Try
					conexion.Open()
					Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
					While sqlReader.Read()
						If sqlReader.HasRows Then
							listNombres.Items.Clear()
							listNombres.Items.Add(sqlReader("documentname"))
						Else
							lblNO.Visible = True
							listNombres.Visible = False
						End If
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

	Protected Sub DropMunicipio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropMunicipio.SelectedIndexChanged, DropTipo.SelectedIndexChanged, DropProvincia.SelectedIndexChanged
		cargarMunicipio()
		sacarNombres()
	End Sub

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


End Class