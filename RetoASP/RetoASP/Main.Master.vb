Public Class Main
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Home_Click(sender As Object, e As EventArgs)
        Response.Redirect("Reservar.aspx")
    End Sub

    Protected Sub Registro_Click(sender As Object, e As EventArgs)
        Response.Redirect("Registro.aspx")
    End Sub

    Protected Sub Login_Click(sender As Object, e As EventArgs)
        Response.Redirect("Login.aspx")
    End Sub
End Class