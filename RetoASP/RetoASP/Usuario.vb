Public Class Usuario

    Private _dni As String
    Private _nombre As String
    Private _apellidos As String
    Private _email As String
    Private _contrasena As String
    Private _telefono As String
    Private _administrador As Integer
    Private _activo As String

    Public Sub New()

    End Sub

    Public Property dni() As String
        Get
            Return _dni
        End Get
        Set(ByVal value As String)
            _dni = value
        End Set
    End Property

    Public Property apellidos() As String
        Get
            Return _apellidos
        End Get
        Set(ByVal value As String)
            _apellidos = value
        End Set
    End Property

    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Public Property contrasena() As String
        Get
            Return _contrasena
        End Get
        Set(ByVal value As String)
            _contrasena = value
        End Set
    End Property

    Public Property telefono() As String
        Get
            Return _telefono
        End Get
        Set(ByVal value As String)
            _telefono = value
        End Set
    End Property

    Public Property administrador() As Integer
        Get
            Return _administrador
        End Get
        Set(ByVal value As Integer)
            _administrador = value
        End Set
    End Property

    Public Property activo() As String
        Get
            Return _activo
        End Get
        Set(ByVal value As String)
            _activo = value
        End Set
    End Property

End Class
