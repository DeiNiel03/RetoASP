Public Class WebForm5
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		lblUsuario.Text = Request.Params("usuario")
	End Sub

	Protected Sub btnReservar_Click(sender As Object, e As EventArgs) Handles btnReservar.Click
		Response.Redirect("Reservar.aspx?usuario=" + lblUsuario.Text)
	End Sub

	Protected Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click
		Response.Redirect("")
	End Sub
End Class