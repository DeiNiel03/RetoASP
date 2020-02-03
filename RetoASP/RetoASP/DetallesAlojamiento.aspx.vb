Imports MySql.Data.MySqlClient

Public Class DetallesAlojamiento
    Inherits System.Web.UI.Page

    Dim conexion As MySqlConnection
    Dim signatura As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conexion = New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")
        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If
        signatura = Request.Params("signatura").ToString
        mostrarAlojamiento()
    End Sub

    Sub mostrarAlojamiento()
        Try
            Dim sqlQuery As String = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, territory, municipality, postalcode, capacity, imagen, restaurant, store, autocaravana FROM alojamientos_fac.alojamientos WHERE signatura = '" + signatura + "'"
            Using sqlComm As New MySqlCommand()
                With sqlComm
                    .Connection = conexion
                    .CommandText = sqlQuery
                    .CommandType = CommandType.Text
                End With
                Try
                    Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
                    renderItems(sqlReader)
                Catch ex As MySqlException
                    'MessageBox.Show("El alojamiento no esta disponible", "ERROR DE ALOJAMIENTO", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        Catch ex As MySql.Data.MySqlClient.MySqlException
            'MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub renderItems(sqlReader As MySqlDataReader)
        Dim idAlojamiento As String
        Dim html As String = ""
        Dim div As New HtmlGenericControl("div")
        Dim boton As New Button
        Dim provincia As String = Nothing
        Dim description As String = Nothing
        div.Attributes.Add("class", "item")
        div.Attributes.Add("class", "row")
        boton.Text = "Reservar"
        boton.Attributes.Add("class", "btn")
        AddHandler boton.Click, AddressOf irAReservar
        Panel1.Controls.Clear()
        If Not sqlReader.HasRows Then
            lblNO.Visible = True
        Else
            lblNO.Visible = False
            While sqlReader.Read()
                idAlojamiento = sqlReader("signatura")
                'Territory
                If sqlReader("territory") = 1 Then
                    provincia = "Bizkaia/Vizcaya"
                ElseIf sqlReader("territory") = 2 Then
                    provincia = "Araba/Alava"
                Else
                    provincia = "Gipuzkoa/Guipuzcoa"
                End If
                'Description
                description = HttpUtility.HtmlDecode(sqlReader("turismdescription"))
                description = Regex.Replace(description, "<[^>]*(>|$)", "")
                If description.Length > 400 Then
                    description = description.Trim().Remove(400)
                End If
                html = html + "<div class='col-sm-5'>"
                html = html + "<img class='lodging-img' src='" + "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen")) + "'>"
                html = html + "</div>"
                html = html + "<div class='col-sm-7'>"
                html = html + "<h2 class='lblnombre'>" + sqlReader("documentname").ToString + "</h2>"
                html = html + "<p class='lbllocalicacion'>" + sqlReader("municipality") + ", " + provincia + "</p>"
                html = html + "<p class='lbldescripcion'>" + description + "</p>"
                html = html + "<p class='lbldireccion'>Dirección: " + sqlReader("address").ToString + " " + sqlReader("postalcode").ToString + "</p>"
                html = html + "<p class='lbltelefono'>Telefono: " + sqlReader("phone").ToString + "</p>"
                html = html + "<p class='lblemail'> Email: " + sqlReader("tourismemail").ToString + "</p>"
                html = html + "<a class='lblweb' href='" + sqlReader("web").ToString + "'>Web: " + sqlReader("web").ToString + "</a>"
                html = html + "<p class='lblrestaurante'>Restaurante: "
                If sqlReader("restaurant") = 1 Then
                    html = html + "Si"
                Else
                    html = html + "No"
                End If
                html = html + "</p>"
                html = html + "<p class='lblautocaravana'>Caravana: "
                If sqlReader("autocaravana") = 1 Then
                    html = html + "Si"
                Else
                    html = html + "No"
                End If
                html = html + "</p>"
                html = html + "<p class='lblstore'>Tienda: "
                If sqlReader("store") = 1 Then
                    html = html + "Si"
                Else
                    html = html + "No"
                End If
                html = html + "</p>"
                html = html + "<p class='lblcapacidad'>Capacidad: " + sqlReader("capacity").ToString + "</p>"
                html = html + "</div>"
                div.InnerHtml = html
                boton.ID = idAlojamiento
                div.Controls.Add(boton)
                Panel1.Controls.Add(div)
            End While
        End If
    End Sub

    Sub irAReservar(sender As Object, e As EventArgs)
        Dim params As String = "?signatura=" + signatura
        Response.Redirect("Realizar.aspx" + params)
    End Sub

End Class