Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class WebForm3
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Try
			Dim connString As String = "server=192.168.101.15;Port=3306; user id=ldmj; password=ladamijo; database=prueba"
			Dim sqlQuery As String = "SELECT imagen FROM alojamientos WHERE signatura = @idemail"

			Using sqlConn As New MySqlConnection(connString)
				Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select
					With sqlComm
						.Connection = sqlConn
						.CommandText = sqlQuery
						.CommandType = CommandType.Text
						.Parameters.AddWithValue("@idemail", 2)
					End With
					Try
						sqlConn.Open()
						Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
						While sqlReader.Read()
							Me.Image1 = bytes_imagen(sqlReader("imagen"))
						End While

					Catch ex As MySqlException
						MessageBox.Show(ex.Message, "ERROR DE LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Try
				End Using
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	'convertir binario a imágen
	Private Function bytes_imagen(ByVal imagen As Byte()) As Image
		Try
			'si hay imagen
			If Not imagen Is Nothing Then
				'caturar array con memorystream hacia bin
				Dim bin As New MemoryStream(imagen)
				'con el método frostream de image obtenemos imagen
				Dim resultado As Image = Image.fromstream(bin)
				'y la retornamos
				Return resultado
			Else
				Return Nothing
			End If
		Catch ex As Exception
			Return Nothing
		End Try
	End Function
End Class