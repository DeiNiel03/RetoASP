Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class WebForm3
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'cargarTipo()
		cargarProvincias()
		cargarMunicipio()

		Try
			Dim connString As String = "server=188.213.5.150; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = "SELECT documentname FROM alojamientos WHERE municipality = @idMuni, territory = @idPro"

			Using sqlConn As New MySqlConnection(connString)
				Using sqlComm As New MySqlCommand()
					With sqlComm
						.Connection = sqlConn
						.CommandText = sqlQuery
						.CommandType = CommandType.Text
						'.Parameters.AddWithValue("@idTipo", DropTipo.SelectedItem)
						.Parameters.AddWithValue("@idMuni", DropMunicipio.SelectedItem)
						.Parameters.AddWithValue("@idPro", CInt(DropProvincia.SelectedValue))
					End With
					Try
						sqlConn.Open()
						Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
						While sqlReader.Read()
							listNombres.DataTextField = "documentname"
						End While
					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR NOMBRE DE ALOJAMIENTOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Try
				End Using
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub

	Sub cargarTipo()
		Try
			Dim connString As String = "server=188.213.5.150; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = "SELECT DISTINCT lodgingtype FROM alojamientos"

			Using sqlConn As New MySqlConnection(connString)

				Dim adapter As New MySqlDataAdapter(sqlQuery, sqlConn)
				Dim tabla As New DataTable
				adapter.Fill(tabla)

				Try
					DropMunicipio.DataSource = tabla
					DropMunicipio.DataTextField = "lodgingtype"
					DropMunicipio.DataBind()
				Catch ex As MySqlException
					MessageBox.Show(ex.Message, "ERROR TIPO", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Sub cargarProvincias()
		Try
			Dim connString As String = "server=188.213.5.150; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = "SELECT nombre, id FROM provincias"

			Using sqlConn As New MySqlConnection(connString)

				Dim adapter As New MySqlDataAdapter(sqlQuery, sqlConn)
				Dim tabla As New DataTable
				adapter.Fill(tabla)

				Try
					DropProvincia.DataSource = tabla
					DropProvincia.DataTextField = "nombre"
					DropProvincia.DataValueField = "id"
					DropProvincia.DataBind()
				Catch ex As MySqlException
					MessageBox.Show(ex.Message, "ERROR PROVINCIAS", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Sub cargarMunicipio()
		Try
			Dim connString As String = "server=188.213.5.150; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = "SELECT DISTINCT municipality FROM alojamientos ORDER BY municipality ASC"

			Using sqlConn As New MySqlConnection(connString)

				Dim adapter As New MySqlDataAdapter(sqlQuery, sqlConn)
				Dim tabla As New DataTable
				adapter.Fill(tabla)

				Try
					DropMunicipio.DataSource = tabla
					DropMunicipio.DataTextField = "municipality"
					DropMunicipio.DataBind()
				Catch ex As MySqlException
					MessageBox.Show(ex.Message, "ERROR MUNICIPIOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Sub restaurante()

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
							Me.Image1.ImageUrl = imageUrl
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
			Dim connString As String = "server=188.213.5.150;Port=3306; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = ""

			Using sqlConn As New MySqlConnection(connString)
				Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select
					With sqlComm
						.Connection = sqlConn
						.CommandText = sqlQuery
						.CommandType = CommandType.Text
						.Parameters.AddWithValue("@idtipo", DropTipo.SelectedValue.ToString)
						.Parameters.AddWithValue("@idprovin", 2)
						.Parameters.AddWithValue("@idmuni", 2)
						.Parameters.AddWithValue("@idret", 2)
						.Parameters.AddWithValue("@idcaravana", 2)
					End With
					Try
						sqlConn.Open()
						Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
						While sqlReader.Read()

						End While

					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Try
				End Using
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub
End Class