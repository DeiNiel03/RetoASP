Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class WebForm1
	Inherits System.Web.UI.Page

	Protected Sub BtnRegistro_Click(sender As Object, e As EventArgs) Handles BtnRegistro.Click

		If comprobarDNI() = False Then
			MessageBox.Show("Este DNI ya esta registrado", "ERROR DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Me.TBDni.Text = String.Empty
		ElseIf comprobarEmail() = False Then
			MessageBox.Show("Este Email ya esta registrado", "ERROR DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Me.TBEmail.Text = String.Empty
		Else
			registro()
		End If
	End Sub

	Protected Sub btVolver_Click(sender As Object, e As EventArgs) Handles btVolver.Click
		Response.Redirect("Login.aspx")
	End Sub

	'Metodo para insertar la informacion introducida por ek usuario en el formulario
	Sub registro()
		Dim dni, email, nom, ape, contra, tel As String

		dni = TBDni.Text
		email = TBEmail.Text
		nom = TBNombre.Text
		ape = TBApellido.Text
		contra = TBPass.Text
		tel = TBTel.Text

		Try
			Dim connString As String = "server=192.168.101.15;Port=3306; user id=ldmj; password=ladamijo; database=prueba"

			'la contraseña se incripta en el propio insert
			Dim sqlQuery As String = "INSERT INTO USUARIOS (DNI, NOMBRE, APELLIDO, EMAIL, CONTRASENA,TELEFONO) VALUES ('" + dni + "','" + nom + "','" + ape + "','" + email + "',MD5('" + contra + "'),'" + tel + "')"

			Using sqlConn As New MySqlConnection(connString)
				Using sqlComm As New MySqlCommand()
					With sqlComm
						.Connection = sqlConn
						.CommandText = sqlQuery
						.CommandType = CommandType.Text
					End With
					Try
						sqlConn.Open()
						Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
						While sqlReader.Read()

						End While
						Response.Write("<script>window.alert('Se ha registrado correctamente');</script>")
						vacio()
					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
						vacio()
					End Try
				End Using
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
			vacio()
		End Try
	End Sub

	Function comprobarDNI()
		Try
			Dim connString As String = "server=192.168.101.15;Port=3306; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = "SELECT dni FROM usuarios WHERE dni = @idni"

			Using sqlConn As New MySqlConnection(connString)
				Using sqlComm As New MySqlCommand()
					With sqlComm
						.Connection = sqlConn
						.CommandText = sqlQuery
						.CommandType = CommandType.Text
						.Parameters.AddWithValue("@idni", Me.TBDni.Text)
					End With
					Try
						sqlConn.Open()
						Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()

						If sqlReader.HasRows Then
							Return False
						Else
							Return True
						End If

					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
						vacio()
					End Try
				End Using
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
			vacio()
		End Try
	End Function

	Function comprobarEmail()
		Try
			Dim connString As String = "server=192.168.101.15;Port=3306; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = "SELECT email FROM usuarios WHERE email = @idemail"

			Using sqlConn As New MySqlConnection(connString)
				Using sqlComm As New MySqlCommand()
					With sqlComm
						.Connection = sqlConn
						.CommandText = sqlQuery
						.CommandType = CommandType.Text
						.Parameters.AddWithValue("@idemail", Me.TBEmail.Text)
					End With
					Try
						sqlConn.Open()
						Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()

						If sqlReader.HasRows Then
							Return False
						Else
							Return True
						End If

					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
						vacio()
					End Try
				End Using
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
			vacio()
		End Try
	End Function

	Public Sub vacio()
		Me.TBDni.Text = String.Empty
		Me.TBEmail.Text = String.Empty
		Me.TBNombre.Text = String.Empty
		Me.TBApellido.Text = String.Empty
		Me.TBPass.Text = String.Empty
		Me.TBPass2.Text = String.Empty
		Me.TBTel.Text = String.Empty
	End Sub

	''convertir binario a imágen
	'Private Function Bytes_Imagen(ByVal Imagen As Byte()) As Image
	'	Try
	'		'si hay imagen
	'		If Not Imagen Is Nothing Then
	'			'caturar array con memorystream hacia Bin
	'			Dim Bin As New MemoryStream(Imagen)
	'			'con el método FroStream de Image obtenemos imagen
	'			Dim Resultado As Image = Image.FromStream(Bin)
	'			'y la retornamos
	'			Return Resultado
	'		Else
	'			Return Nothing
	'		End If
	'	Catch ex As Exception
	'		Return Nothing
	'	End Try
	'End Function

End Class
