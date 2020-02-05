Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim page As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        page = Request.Params("page").ToString
    End Sub

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

    'Metodo para insertar la informacion introducida por el usuario en el formulario
    Sub registro()
        Dim dni, email, nom, ape, contra, tel As String
        dni = TBDni.Text
		email = TBEmail.Text
		nom = TBNombre.Text
		ape = TBApellido.Text
		contra = TBPass.Text
        tel = TBTel.Text
        Try
            Dim connString As String = "server=188.213.5.150;Port=3306; user id=ldmj; password=ladamijo; database=alojamientos_fac"
            'la contraseña se incripta en el propio insert
            Dim sqlQuery As String = "INSERT INTO usuarios (DNI, NOMBRE, APELLIDO, CONTRASENA,TELEFONO, EMAIL, ADMINISTRADOR, ACTIVO) VALUES ('" + dni + "','" + nom + "','" + ape + "',MD5('" + contra + "'),'" + tel + "','" + email + "', '0', 'activo')"
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
                        Session("Email") = TBEmail.Text
                        Response.Redirect(page)
                        vaciarCampos()
                    Catch ex As MySqlException
                        MessageBox.Show(ex.Message, "ERROR DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        vaciarCampos()
                    End Try
                End Using
            End Using
        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            vaciarCampos()
        End Try
    End Sub

    Function comprobarDNI()
        Try
            Dim connString As String = "server=188.213.5.150;Port=3306; user id=ldmj; password=ladamijo; database=alojamientos_fac"
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
                        vaciarCampos()
                    End Try
                End Using
            End Using
        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            vaciarCampos()
        End Try
    End Function

    Function comprobarEmail()
        Try
            Dim connString As String = "server=188.213.5.150;Port=3306; user id=ldmj; password=ladamijo; database=alojamientos_fac"
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
                        vaciarCampos()
                    End Try
                End Using
            End Using
        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            vaciarCampos()
        End Try
    End Function

    Public Sub vaciarCampos()
        Me.TBDni.Text = String.Empty
        Me.TBEmail.Text = String.Empty
        Me.TBNombre.Text = String.Empty
        Me.TBApellido.Text = String.Empty
        Me.TBPass.Text = String.Empty
        Me.TBPass2.Text = String.Empty
        Me.TBTel.Text = String.Empty
    End Sub
End Class
