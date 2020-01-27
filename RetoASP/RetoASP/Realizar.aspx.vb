Public Class WebForm4
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		lblUsuario.Text = Request.Params("usuario")
	End Sub
End Class