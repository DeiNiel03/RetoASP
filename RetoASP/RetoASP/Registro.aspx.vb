Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class WebForm1
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub
	Private Function CalculaNIF(ByVal strA As String) As String
		'----------------------------------------------------------------------
		' Calcular la letra del NIF y convertido en función que devuelve el NIF correcto
		'----------------------------------------------------------------------
		Const cCADENA As String = "TRWAGMYFPDXBNJZSQVHLCKE"
		Const cNUMEROS As String = "0123456789"
		Dim a, b, c, NIF As Integer
		Dim sb As New StringBuilder

		strA = Trim(strA)
		If Len(strA) = 0 Then Return ""

		' Dejar sólo los números
		For i As Integer = 0 To strA.Length - 1
			If cNUMEROS.IndexOf(strA(i)) > -1 Then
				sb.Append(strA(i))
			End If
		Next

		strA = sb.ToString
		a = 0
		NIF = CInt(Val(strA))
		Do
			b = CInt(Int(NIF / 24))
			c = NIF - (24 * b)
			a = a + c
			NIF = b
		Loop While b <> 0
		b = CInt(Int(a / 23))
		c = a - (23 * b)

		Return strA & Mid(cCADENA, CInt(c + 1), 1)
	End Function

	Public Function Verificar_NIF(ByVal valor As String) As Boolean
		' Comprueba si es un NIF válido
		' No usar espacios ni separadores para la letra
		' Devuelve True si es correcto

		Dim aux As String

		valor = valor.ToUpper ' ponemos la letra en mayúscula
		aux = valor.Substring(0, valor.Length - 1) ' quitamos la letra del NIF

		If aux.Length >= 7 AndAlso IsNumeric(aux) Then
			aux = CalculaNIF(aux) ' calculamos la letra del NIF para comparar con la que tenemos
		Else
			Return False
		End If

		If valor <> aux Then ' comparamos las letras
			Return False
		End If

		Return True
	End Function
	Protected Sub BtnRegistro_Click(sender As Object, e As EventArgs) Handles BtnRegistro.Click
		Dim dni, nom, ape, contra, tel As String
		dni = TBDni.Text
		nom = TBNombre.Text
		ape = TBApellido.Text
		contra = TBPass.Text
		tel = TBTel.Text

		If TBPass.Text = TBPass2.Text And Verificar_NIF(TBDni.Text) Then
			Try
				Dim connString As String = "server=192.168.101.15;Port=3306; user id=ldmj; password=ladamijo; database=prueba"

				Dim sqlQuery As String = "INSERT INTO USUARIOS (DNI, NOMBRE, APELLIDO, CONTRASENA,TELEFONO) VALUES ('" + dni + "','" + nom + "','" + ape + "','" + contra + "','" + tel + "')"
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
								Label1.Text = sqlReader("nombre").ToString()
							End While
						Catch ex As MySqlException
							Me.Label1.Text = ex.Message
						End Try
					End Using
				End Using
			Catch ex As MySql.Data.MySqlClient.MySqlException
				Me.Label1.Text = ex.Message
			End Try
		Else
			MessageBox.Show("DNI no valido o las contraseñas no coinciden", "ERROR DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End If
	End Sub

	Protected Sub btVolver_Click(sender As Object, e As EventArgs) Handles btVolver.Click
		Response.Redirect("Login.aspx")
	End Sub
End Class