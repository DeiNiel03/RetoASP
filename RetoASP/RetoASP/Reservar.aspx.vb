Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Public Class WebForm4
	Inherits System.Web.UI.Page

    Dim conexion As New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")
    Dim MinDate As Date = Date.MinValue
    Dim MaxDate As Date = Date.MaxValue
    Dim dni, personas As String
    Dim fechaEnt, fechaSal As Date
    Dim fechaEntrada, fechaSalida As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Dni") <> Nothing Then
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
            End If
            If Not IsPostBack Then
                CalendarEntrada.SelectedDate = Date.Today
                CalendarSalida.SelectedDate = Date.Today.AddDays(1)
            End If
            mostrarInfo()
            dni = Session("Dni")
            Master.FindControl("btnLogin").Visible = False
            Master.FindControl("btnRegistro").Visible = False
            Master.FindControl("btnPerfil").Visible = True
            Master.FindControl("btnCerrarSesion").Visible = True
        Else
            Response.Redirect("Login.aspx?page=" + Request.Url.LocalPath)
        End If
    End Sub

    Protected Sub CalendarEntrada_DayRender(sender As Object, e As DayRenderEventArgs) Handles CalendarEntrada.DayRender
        MinDate = Date.Today
        MaxDate = CalendarSalida.SelectedDate
        If e.Day.Date < MinDate OrElse e.Day.Date > MaxDate Then
            e.Day.IsSelectable = False
        End If
    End Sub

    Protected Sub CalendarSalida_DayRender(sender As Object, e As DayRenderEventArgs) Handles CalendarSalida.DayRender
        MinDate = CalendarEntrada.SelectedDate
        If e.Day.Date < MinDate Then
            e.Day.IsSelectable = False
        End If
    End Sub

    Protected Sub btnRealizar_Click(sender As Object, e As EventArgs) Handles btnRealizar.Click

        fechaEnt = CalendarEntrada.SelectedDate.ToShortDateString
        fechaSal = CalendarSalida.SelectedDate.Date.ToShortDateString
        personas = TBPersonas.Text

        fechaEntrada = Format(fechaEnt, "yyyy-MM-dd")
        fechaSalida = Format(fechaSal, "yyyy-MM-dd")
        'Dim sqlQuery As String = SELECT fecha_entrada, fecha_salida, capacity FROM alojamientos_fac.reservas WHERE signatura = idAlojamiento

        Dim sqlQuery As String = "INSERT INTO alojamientos_fac.reservas (dni, fecha_entrada, fecha_salida, alojamiento, personas) VALUES ('" & dni & "','" & fechaEntrada & "', '" & fechaSalida & "', '" & Session("signatura") & "','" & personas & "')"
        Try
            Using sqlComm As New MySqlCommand()
                With sqlComm
                    .Connection = conexion
                    .CommandText = sqlQuery
                    .CommandType = CommandType.Text
                    '.Parameters.AddWithValue("@idUsuario", usuario)
                End With
                Try
                    Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
                    While sqlReader.Read()

                    End While
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message, "ERROR EN LA INSERT", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Response.Write("<script>window.alert('Se ha insertado');</script>")
    End Sub

    Sub mostrarInfo()
        Try
            Dim sqlQuery As String = "SELECT documentname, address, capacity, municipality, imagen FROM alojamientos_fac.alojamientos WHERE signatura = @id"
            Using sqlComm As New MySqlCommand()
                With sqlComm
                    .Connection = conexion
                    .CommandText = sqlQuery
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@id", Session("signatura"))
                End With
                Try
                    Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
                    While sqlReader.Read()
                        lblNombre.Text = sqlReader("documentname").ToString
                        lblDirecion.Text = sqlReader("address").ToString
                        lblCapacidad.Text = sqlReader("capacity").ToString
                        Image1.Attributes.Add("src", "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen")))

                    End While
                    If Not sqlReader.HasRows Then
                        Response.Write("<script>window.alert('NO SE PUEDE MOSTRAR LA INFORMACION');</script>")
                    End If
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message, "ERROR EN MOSTRAR INFO", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class