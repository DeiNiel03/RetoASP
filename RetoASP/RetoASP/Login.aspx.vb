Public Class WebForm2
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

	End Sub

	Protected Sub TextBox1_TextChanged1(sender As Object, e As EventArgs)

	End Sub

	Protected Sub btRegistro_Click(sender As Object, e As EventArgs) Handles btRegistro.Click
		Response.Redirect("Registro.aspx")
	End Sub
End Class