Public Class WebForm6
	Inherits System.Web.UI.Page

	Dim latitud As Double
	Dim longitud As Double

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		latitud = CType(Request.Params("lat"), Double)
		longitud = CType(Request.Params("lon"), Double)


	End Sub

End Class