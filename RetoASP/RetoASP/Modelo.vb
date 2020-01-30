Public Class Modelo

    Private _usuarioLogeado As Usuario

    Public Sub New()
        _usuarioLogeado = Nothing
    End Sub

    Public Property usuarioLogeado() As Usuario
        Get
            Return _usuarioLogeado
        End Get
        Set(ByVal value As Usuario)
            _usuarioLogeado = value
        End Set
    End Property

End Class
