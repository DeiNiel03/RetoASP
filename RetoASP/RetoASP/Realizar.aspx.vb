Imports System.IO
Imports MySql.Data.MySqlClient
Public Class WebForm4
	Inherits System.Web.UI.Page

	Dim conexion As New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")
	Dim id As String
	Dim usuario As String
	Dim MinDate As Date = Date.MinValue
	Dim MaxDate As Date = Date.MaxValue
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		id = Request.Params("id").ToString
		usuario = Request.Params("usuario").ToString
		lblUsuario.Text = usuario
		lblid.Text = id
		mostrarInfo()
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

	Sub mostrarInfo()
		Dim sqlQuery As String = "SELECT documentname, address, capacity FROM alojamientos_fac.alojamientos WHERE signatura = '" & id & "'"
		Try
			Dim adapter As New MySqlDataAdapter(sqlQuery, conexion)
			Dim tabla As New DataSet
			adapter.Fill(tabla)

			Dim row As DataRow = tabla.Tables(0).Rows(0)
			lblNombre.Text = row("documentname")
			lblDirecion.Text = row("address")
			lblCapacidad.Text = row("capacity")
		Catch ex As MySqlException
			'MessageBox.Show(ex.Message, "ERROR TIPO", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub
End Class