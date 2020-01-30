Imports System.IO
Imports MySql.Data.MySqlClient

Public Class WebForm3
	Inherits System.Web.UI.Page
	Dim panel
    Dim conexion As New MySqlConnection("datasource=188.213.5.150;port=3306;username=ldmj;password=ladamijo;CharSet=UTF8")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblUsuario.Text = Request.Params("usuario")
        panel = Panel1
        lblNO.Visible = False
        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If
        If Not IsPostBack Then
            cargarDatos()
        End If
        mostrarAlojamientos()
    End Sub

    Sub irAReservar(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim s As String = "?usuario=" + lblUsuario.Text + "&id=" + btn.ID
        Response.Redirect("Realizar.aspx" + s)
        conexion.Close()
    End Sub

    Protected Sub DropProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropProvincia.SelectedIndexChanged, DropTipo.SelectedIndexChanged
        cargarMunicipio()
    End Sub

    Protected Sub SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropMunicipio.SelectedIndexChanged, RBsi.CheckedChanged, RBno.CheckedChanged, DropProvincia.SelectedIndexChanged, DropTipo.SelectedIndexChanged, RBcsi.CheckedChanged, RBcno.CheckedChanged, RBtsi.CheckedChanged, RBtno.CheckedChanged
        mostrarAlojamientos()
    End Sub

    Protected Sub Ordenacion_CheckedChanged(sender As Object, e As EventArgs) Handles RBAsc.CheckedChanged, RBDesc.CheckedChanged
        mostrarAlojamientos()
    End Sub

    Sub cargarDatos()
        cargarTipo()
        cargarProvincias()
        cargarMunicipio()
    End Sub

    Sub cargarTipo()
        Dim sqlQuery As String = "SELECT DISTINCT `lodgingtype` FROM alojamientos_fac.alojamientos"
        Try
            Dim adapter As New MySqlDataAdapter(sqlQuery, conexion)
            Dim tabla As New DataTable
            adapter.Fill(tabla)
            DropTipo.DataSource = tabla
            DropTipo.DataTextField = "lodgingtype"
            DropTipo.DataBind()
        Catch ex As MySqlException
            'MessageBox.Show(ex.Message, "ERROR TIPO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub cargarProvincias()
        Dim sqlQuery As String = "SELECT `nombre`, `id` FROM alojamientos_fac.provincias ORDER BY `nombre` ASC"
        Try
            Dim adapter As New MySqlDataAdapter(sqlQuery, conexion)
            Dim tabla As New DataTable
            adapter.Fill(tabla)
            DropProvincia.DataSource = tabla
            DropProvincia.DataTextField = "nombre"
            DropProvincia.DataValueField = "id"
            DropProvincia.DataBind()
        Catch ex As MySqlException
            'MessageBox.Show(ex.Message, "ERROR PROVINCIAS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'DropProvincia_SelectedIndexChanged(DropProvincia,)
    End Sub

    Sub cargarMunicipio()
        Try
            Dim adapter As New MySqlDataAdapter("SELECT DISTINCT `municipality` FROM alojamientos_fac.alojamientos WHERE territory = " + DropProvincia.SelectedValue + " AND lodgingtype = '" + DropTipo.SelectedItem.Text + "' ORDER BY `municipality` ASC", conexion)
            Dim tabla As New DataTable()
            adapter.Fill(tabla)
            DropMunicipio.DataSource = tabla
            DropMunicipio.DataTextField = "municipality"
            DropMunicipio.DataBind()
        Catch ex As MySqlException
            'MessageBox.Show(ex.Message, "ERROR MUNICIPIOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub mostrarOrden()
        RBAsc.Visible = True
        RBDesc.Visible = True
    End Sub

    Sub ocultarOrden()
        RBAsc.Visible = False
        RBDesc.Visible = False
    End Sub

    Sub mostrarAlojamientos()
        If labelFiltros.Text = 1 Then
            Panel1.Controls.Clear()
            sacarNombresConFiltros()
        ElseIf labelFiltros.Text = 0 Then
            sacarNombresSinfiltros()
        End If
    End Sub

    Sub sacarNombresSinfiltros()
        Dim comando As String = Nothing
        Dim sqlReader As MySqlDataReader = Nothing
        mostrarOrden()
        Try
            If RBAsc.Checked = True Then
                comando = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, postalcode, capacity, imagen, restaurant, store, autocaravana FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND activo = @idActivo ORDER BY documentname ASC"
            ElseIf RBDesc.Checked = True Then
                comando = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, postalcode, capacity, imagen, restaurant, store, autocaravana FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND activo = @idActivo ORDER BY documentname DESC"
            End If
            Using sqlComm As New MySqlCommand()
                With sqlComm
                    .Connection = conexion
                    .CommandText = comando
                    .CommandType = CommandType.Text
                    .Parameters.Add("@idTipo", MySqlDbType.VarChar).Value = DropTipo.SelectedItem
                    .Parameters.Add("@idMuni", MySqlDbType.VarChar).Value = DropMunicipio.SelectedItem
                    .Parameters.Add("@idPro", MySqlDbType.Int16).Value = DropProvincia.SelectedValue
                    .Parameters.Add("@idActivo", MySqlDbType.Int16).Value = 1
                End With
                sqlReader = sqlComm.ExecuteReader()
                renderItems(sqlReader)
            End Using
        Catch ex As MySqlException
            'MessageBox.Show("El alojamiento no esta disponible", "ERROR DE ALOJAMIENTO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub sacarNombresConFiltros()
        mostrarOrden()
        Try
            Dim sqlQuery As String
            If RBAsc.Checked = True Then
                sqlQuery = "SELECT signatura, documentname,turismdescription, address, postalcode, phone, tourismemail, web, capacity,restaurant,autocaravana,store, imagen FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND restaurant = @idRest AND autocaravana = @idCaravan AND store = @idTienda AND activo = @idActivo ORDER BY documentname ASC"
            Else
                sqlQuery = "SELECT signatura, documentname,turismdescription, address, postalcode, phone, tourismemail, web, capacity,restaurant,autocaravana,store, imagen FROM alojamientos_fac.alojamientos WHERE lodgingtype = @idTipo AND municipality = @idMuni AND territory = @idPro AND restaurant = @idRest AND autocaravana = @idCaravan AND store = @idTienda AND activo = @idActivo ORDER BY documentname DESC"
            End If
            Using sqlComm As New MySqlCommand()
                With sqlComm
                    .Connection = conexion
                    .CommandText = sqlQuery
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@idTipo", DropTipo.SelectedItem)
                    .Parameters.AddWithValue("@idMuni", DropMunicipio.SelectedItem)
                    .Parameters.AddWithValue("@idPro", CInt(DropProvincia.SelectedValue))
                    .Parameters.AddWithValue("@idActivo", 1)
                    If RBsi.Checked Then
                        .Parameters.AddWithValue("@idRest", 1)
                    Else
                        .Parameters.AddWithValue("@idRest", 0)
                    End If
                    If RBcsi.Checked Then
                        .Parameters.AddWithValue("@idCaravan", 1)
                    Else
                        .Parameters.AddWithValue("@idCaravan", 0)
                    End If
                    If RBtsi.Checked Then
                        .Parameters.AddWithValue("@idTienda", 1)
                    Else
                        .Parameters.AddWithValue("@idTienda", 0)
                    End If
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
        div.Attributes.Add("class", "item")
        div.Attributes.Add("class", "row")
        boton.Text = "Reservar"
        boton.Attributes.Add("class", "btn")
        AddHandler boton.Click, AddressOf irAReservar
        Panel1.Controls.Clear()
        If Not sqlReader.HasRows Then
            lblNO.Visible = True
            ocultarOrden()
        Else
            lblNO.Visible = False
            While sqlReader.Read()
                idAlojamiento = sqlReader("signatura")
                html = html + "<div class='col-sm-5'>"
                html = html + "<img src='" + "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen")) + "'>"
                html = html + "</div>"
                html = html + "<div class='col-sm-7'>"
                html = html + "<h2 class='lblnombre'>" + sqlReader("documentname").ToString + "</h2>"
                html = html + "<p class='lbldescripcion'>" + sqlReader("turismdescription") + "</p>"
                html = html + "<p class='lbldireccion'>Dirección: " + sqlReader("address").ToString + "</p>"
                html = html + "<p class='lblcodpostal'>" + sqlReader("postalcode").ToString + "</p>"
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


    'Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
    '	mostrarOrden()
    '	Try
    '		Dim comando As String
    '		If RBAsc.Checked = True Then
    '			comando = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, postalcode, capacity, imagen, restaurant, store, autocaravana FROM alojamientos_fac.alojamientos WHERE UPPER(documentname) = UPPER(@idNombre) ORDER BY documentname ASC"
    '		ElseIf RBDesc.Checked = True Then
    '			comando = "SELECT signatura, documentname, turismdescription, address, phone, tourismemail, web, postalcode, capacity, imagen, restaurant, store, autocaravana FROM alojamientos_fac.alojamientos WHERE UPPER(documentname) = UPPER(@idNombre) ORDER BY documentname DESC"
    '		End If
    '		Using sqlComm As New MySqlCommand()
    '			With sqlComm
    '				.Connection = conexion
    '				.CommandText = comando
    '				.CommandType = CommandType.Text
    '				.Parameters.Add("@idNombre", MySqlDbType.VarChar).Value = TBBuscar.Text

    '			End With
    '			Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()

    '			Panel1.Controls.Clear()

    '			While sqlReader.Read()

    '				id = sqlReader("signatura")

    '				Dim div As New HtmlGenericControl("div")
    '				div.Attributes.Add("class", "item")
    '				Dim html As String = ""
    '				html = html + "<img src='" + "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen")) + "'>"
    '				html = html + "<label class='lblnombre'>" + sqlReader("documentname").ToString + "</label>"
    '				html = html + "<label class='lbldescripcion'>" + sqlReader("turismdescription") + "</label>"
    '				html = html + "<label class='lbldireccion'>" + sqlReader("address").ToString + "</label>"
    '				html = html + "<label class='lblcodpostal'>" + sqlReader("postalcode").ToString + "</label>"
    '				html = html + "<label class='lbltelefono'>" + sqlReader("phone").ToString + "</label>"
    '				html = html + "<label class='lblemail'>" + sqlReader("tourismemail").ToString + "</label>"
    '				html = html + "<label class='lblweb'>" + sqlReader("web").ToString + "</label>"

    '				If sqlReader("restaurant") = 1 Then
    '					html = html + "Si"
    '				Else
    '					html = html + "No"
    '				End If
    '				html = html + "</label>"

    '				html = html + "<label class='lblautocaravana'>"
    '				If sqlReader("autocaravana") = 1 Then
    '					html = html + "Si"
    '				Else
    '					html = html + "No"
    '				End If

    '				html = html + "<label class='lblstore'>"
    '				If sqlReader("store") = 1 Then
    '					html = html + "Si"
    '				Else
    '					html = html + "No"
    '				End If
    '				html = html + "</label>"
    '				html = html + "<label class='lblcapacidad'>" + sqlReader("capacity").ToString + "</label>"


    '				div.InnerHtml = html

    '				Dim boton As New Button
    '				boton.ID = id
    '				boton.Text = "Reservar"
    '				'boton.Attributes.Add("onclick", "return false")
    '				AddHandler boton.Click, AddressOf irAReservar
    '				Panel1.Controls.Add(div)
    '				Panel1.Controls.Add(boton)
    '			End While
    '			If Not sqlReader.HasRows Then
    '				lblNO.Visible = True
    '				ocultarOrden()
    '			Else
    '				lblNO.Visible = False
    '			End If
    '		End Using
    '	Catch ex As MySqlException
    '		'MessageBox.Show("El alojamiento no esta disponible", "ERROR DE ALOJAMIENTO", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '	End Try
    'End Sub

    'Sub sacarImagen()
    '	Try
    '		Dim sqlQuery As String = "SELECT imagen FROM alojamientos_fac.alojamientos WHERE documentname = @idNombre"


    '		Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select
    '			With sqlComm
    '				.Connection = conexion
    '				.CommandText = sqlQuery
    '				.CommandType = CommandType.Text
    '				.Parameters.AddWithValue("@idNombre", listNombres.SelectedItem.ToString)
    '			End With
    '			Try
    '				conexion.Open()
    '				Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
    '				While sqlReader.Read()
    '					Dim imageUrl As String = "data:image/jpg;base64," & Convert.ToBase64String(sqlReader("imagen"))
    '					Me.Imagen.ImageUrl = imageUrl
    '				End While

    '			Catch ex As MySqlException
    '				MessageBox.Show(ex.Message, "ERROR DE IMAGEN", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '			End Try
    '		End Using
    '	Catch ex As MySql.Data.MySqlClient.MySqlException
    '		MessageBox.Show(ex.Message, "ERROR CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '	End Try
    'End Sub
End Class