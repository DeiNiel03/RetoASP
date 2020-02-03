Imports MySql.Data.MySqlClient

Public Class WebForm7
	Inherits System.Web.UI.Page
	Dim usuario As String = Request.Params("usuario").ToString
	Dim conexion As New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		lblId.Text = usuario

		obtenerReservas()

	End Sub

	Sub obtenerReservas()

		Dim html As String = ""
		Dim div As New HtmlGenericControl("div")
		Dim boton As New Button
		div.Attributes.Add("class", "item")

		boton.Text = "Eliminar Reserva"
		boton.Attributes.Add("class", "btn")
		Panel1.Controls.Clear()

		Try
			Dim sqlQuery As String = "SELECT id, documentname, phone, personas, territory, municipality, fecha_entrada, fecha_salida FROM alojamientos_fac.alojamientos a, alojamientos_fac.reservas r WHERE a.signatura = r.alojamiento AND DNI = @id"
			Using sqlComm As New MySqlCommand()
				With sqlComm
					.Connection = conexion
					.CommandText = sqlQuery
					.CommandType = CommandType.Text
					.Parameters.AddWithValue("@id", usuario)
				End With
				Try
					Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
					While sqlReader.Read()
						Dim idR As Integer = sqlReader("id")

						AddHandler boton.Click, Sub(sender, e) eliminarReserva(idR)

						html = html + "<div class='col-sm-7'>"
						html = html + "<h2 class='lblnombre'>" + sqlReader("documentname").ToString + "</h2>"
						html = html + "<p class='lbllocalicacion'>" + sqlReader("municipality") + ", " + sqlReader("territory") + "</p>"
						html = html + "<p class='lblnumero'>" + sqlReader("phone") + "</p>"
						html = html + "<p class='lblfechas'>" + "Del " + sqlReader("fecha_entrada") + " al " + sqlReader("fecha_salida") + "</p>"
						html = html + "<p class='lblperonas'>" + sqlReader("personas") + "</p>"
						html = html + "</div>"
						div.InnerHtml = html
						boton.ID = id
						div.Controls.Add(boton)
						Panel1.Controls.Add(div)
					End While
					If Not sqlReader.HasRows Then
						Response.Write("<script>window.alert('NO SE LE PUEDE MOSTRAR SUS RESERVAS');</script>")
					End If
				Catch ex As MySqlException
					'MessageBox.Show(ex.Message + "CONTACTE CON NUESTRO SERVICIO TECNICO", "ERROR AL OBTENER SUS RESERVAS", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End Using
		Catch ex As MySql.Data.MySqlClient.MySqlException
			'MessageBox.Show(ex.Message + "CONTACTE CON NUESTRO SERVICIO TECNICO", "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub

	Sub eliminarReserva(idR As String)

	End Sub

End Class