Public Class Prueba
	Inherits System.Web.UI.Page
	Dim panel
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		panel = Panel1

		Dim h1 = New HtmlGenericControl("h1")
		h1.Attributes.Add("id", "nombre")
		h1.InnerText = "Alojamiento Pepa"
		Panel1.Controls.Add(h1)

		Dim boton = New HtmlGenericControl("Button")
		boton.InnerText = "Botón"
		boton.Attributes.Add("onClick", "alert('Hola')")
		Panel1.Controls.Add(boton)


		Dim texto As String = "Mi texto"
		Dim div = New HtmlGenericControl("div")
		div.InnerHtml = "<Label>" + texto + "</Label>"

		Panel1.Controls.Add(div)

	End Sub

End Class