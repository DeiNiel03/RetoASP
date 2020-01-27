Imports System.Security.Cryptography
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class WebForm2
	Inherits System.Web.UI.Page
	Dim usuario As String

	Protected Sub btRegistro_Click(sender As Object, e As EventArgs) Handles btRegistro.Click
		Response.Redirect("Registro.aspx")
	End Sub

	Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
		usuario = TBEmail.Text
		Login()
	End Sub

	'Metodo para el login
	'Con la primera select compruebo que la contraseña coincidan con el usuario
	'Con la segunda select compruebo que el usuario introducido exista en la BBDD
	Sub Login()
		Try
			Dim connString As String = "server=188.213.5.150;Port=3306; user id=ldmj; password=ladamijo; database=alojamientos_fac"
			Dim sqlQuery As String = "SELECT contrasena FROM usuarios WHERE email = @idemail"

			Using sqlConn As New MySqlConnection(connString)
				Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select
					With sqlComm
						.Connection = sqlConn
						.CommandText = sqlQuery
						.CommandType = CommandType.Text
						.Parameters.AddWithValue("@idemail", Me.TBEmail.Text)
					End With
					Try
						sqlConn.Open()
						Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
						While sqlReader.Read()
							If sqlReader("contrasena").ToString().Equals(getMd5Hash(Me.TBPass.Text)) Then
								'Response.Write("<script>window.alert('Se ha logeado correctamente');</script>" + "<script>window.setTimeout(location.href='Reservar.aspx', 1000);</script>")
								Response.Write("<script>window.alert('Se ha logeado correctamente');</script>")
								Response.Redirect("Elejir.aspx?usuario=" + usuario)
							Else
								MessageBox.Show("Email y/o contraseña incorrectos.", "ERROR DE LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
							End If
						End While

					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR DE LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Try
				End Using

				'Desde aqui comienza la segunta select
				Dim sqlQuery2 As String = "SELECT email FROM usuarios WHERE email = @ideemail"

				Using sqlComm2 As New MySqlCommand()
					With sqlComm2
						.Connection = sqlConn
						.CommandText = sqlQuery2
						.CommandType = CommandType.Text
						.Parameters.AddWithValue("@ideemail", Me.TBEmail.Text)
					End With
					Try
						'no hace falta "sql.Open()" porque la conexion ya se ha abierto antes
						Dim sqlReader2 As MySqlDataReader = sqlComm2.ExecuteReader()
						If Not sqlReader2.HasRows Then
							MessageBox.Show("Email no registrado", "ERROR DE LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Error)

						End If

					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR DE LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Try
				End Using

			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	'Metodo para encriptar la contraseña
	Function getMd5Hash(ByVal input As String) As String
		Dim md5Hasher As MD5 = MD5.Create()

		Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))

		Dim sBuilder As New StringBuilder()

		Dim i As Integer
		For i = 0 To data.Length - 1
			sBuilder.Append(data(i).ToString("x2"))
		Next i

		Return sBuilder.ToString()
	End Function
End Class