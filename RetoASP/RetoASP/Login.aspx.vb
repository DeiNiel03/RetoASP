﻿Imports System.Security.Cryptography
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class WebForm2
    Inherits System.Web.UI.Page

    Dim page As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        page = Request.Params("page").ToString
    End Sub

    Protected Sub btnRegistro_Click(sender As Object, e As EventArgs) Handles btnRegistro.Click
		Response.Redirect("Registro.aspx?page=" + page)
	End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim connString As String = "server=188.213.5.150;Port=3306; user id=ldmj; password=ladamijo; database=alojamientos_fac"
            Dim sqlQuery As String = "SELECT contrasena, dni FROM usuarios WHERE email = @idemail"
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
                        If (sqlReader.HasRows) Then
                            While sqlReader.Read()
                                If sqlReader("contrasena").ToString().Equals(getMd5Hash(Me.TBPass.Text)) Then
                                    Session("Email") = TBEmail.Text
                                    Session("Dni") = sqlReader("dni")
                                    Response.Redirect(page)
                                Else
                                    MessageBox.Show("Email y/o contraseña incorrectos.", "ERROR DE LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End While
                        Else
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