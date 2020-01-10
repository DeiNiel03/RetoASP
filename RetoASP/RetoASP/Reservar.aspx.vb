Imports System.IO

Public Class WebForm3
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	'convertir binario a imágen
	'Private Function bytes_imagen(ByVal imagen As Byte()) As Image
	'	Try
	'		'si hay imagen
	'		If Not imagen Is Nothing Then
	'			'caturar array con memorystream hacia bin
	'			Dim bin As New MemoryStream(imagen)
	'			'con el método frostream de image obtenemos imagen
	'			Dim resultado As Image = Image.fromstream(bin)
	'			'y la retornamos
	'			Return resultado
	'		Else
	'			Return Nothing
	'		End If
	'	Catch ex As Exception
	'		Return Nothing
	'	End Try
	'End Function
End Class