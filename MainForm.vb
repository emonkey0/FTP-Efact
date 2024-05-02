Imports System.IO
Imports System.Net

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Establecer las credenciales FTP
        Dim ftpCredential As New NetworkCredential("android", "android")

        Try
            ' Crear una instancia del cliente FTP con las credenciales
            Dim ftpClient As New FtpWebRequest("ftp://192.168.1.12:2221/", ftpCredential)

            ' Conectarse al servidor FTP
            ftpClient.Method = WebRequestMethods.Ftp.ListDirectory

            Using ftpResponse As FtpWebResponse = DirectCast(ftpClient.GetResponse(), FtpWebResponse)
                ' Conexión exitosa
            End Using
        Catch ex As Exception
            ' Error al conectarse al servidor FTP
            MessageBox.Show("No se pudo conectar al Servidor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView1.Rows.Clear()

        ' Obtener los valores ingresados por el usuario
        Dim rfc As String = Label1.Text.Trim()
        Dim uuid As String = Label2.Text.Trim()
        Dim fechaInicio As DateTime = DateTimePicker1.Value
        Dim fechaFinal As DateTime = DateTimePicker2.Value

        ' Crear una instancia del cliente FTP con las credenciales
        Dim ftpCredential As New NetworkCredential("android", "android")
        Dim ftpClient As New FtpWebRequest("ftp://192.168.1.12:2221/", ftpCredential)

        ' Primer caso: Buscar todos los archivos dentro del rango de fechas
        If String.IsNullOrEmpty(rfc) AndAlso String.IsNullOrEmpty(uuid) Then
            BuscarArchivosEntreFechas(ftpClient, fechaInicio, fechaFinal)

            ' Segundo caso: Buscar archivos con el RFC dentro del rango de fechas
        ElseIf Not String.IsNullOrEmpty(rfc) AndAlso String.IsNullOrEmpty(uuid) Then
            BuscarArchivosConRFCEntreFechas(ftpClient, rfc, fechaInicio, fechaFinal)

            ' Tercer caso: Buscar el archivo específico con el UUID
        ElseIf String.IsNullOrEmpty(rfc) AndAlso Not String.IsNullOrEmpty(uuid) Then
            BuscarArchivoConUUID(ftpClient, uuid)
        End If
    End Sub

    Private Sub BuscarArchivosEntreFechas(ftpClient As FtpWebRequest, fechaInicio As DateTime, fechaFinal As DateTime)
        For Each anio As Integer = fechaInicio.Year To fechaFinal.Year
            For mes As Integer = 1 To 12
                If anio = fechaInicio.Year AndAlso mes < fechaInicio.Month Then
                    Continue For
                End If

                If anio = fechaFinal.Year AndAlso mes > fechaFinal.Month Then
                    Exit For
                End If

                Dim directorio As String = $"ftp://192.168.1.12:2221/{anio}/{mes}"
                BuscarArchivosEnDirectorio(ftpClient, directorio)
            Next
        Next
    End Sub

    Private Sub BuscarArchivosConRFCEntreFechas(ftpClient As FtpWebRequest, rfc As String, fechaInicio As DateTime, fechaFinal As DateTime)
        If anio = fechaFinal.Year AndAlso mes > fechaFinal.Month Then Exit For
        For mes As Integer = 1 To 12
                If anio = fechaInicio.Year AndAlso mes < fechaInicio.Month Then
                    Continue For
                End If

                If anio = fechaFinal.Year AndAlso mes > fechaFinal.Month Then
                    Exit For
                End If

                Dim directorio As String = Path.Combine("ftp://192.168.1.12:2221/", anio.ToString(), mes.ToString(), rfc)
                BuscarArchivosEnDirectorio(ftpClient, directorio)
            Next
        Next
    End Sub

    Private Sub BuscarArchivoConUUID(ftpClient As FtpWebRequest, uuid As String)
        Dim encontrado As Boolean = False
        Dim archivosXml As New List(Of String)()

        For anio As Integer = DateTime.Now.Year - 3 To DateTime.Now.Year
            For mes As Integer = 1 To 12
                Dim directorio As String = Path.Combine("ftp://192.168.1.12:2221/", anio.ToString(), mes.ToString())
                ftpClient.Method = WebRequestMethods.Ftp.ListDirectory

                Try
                    Dim ftpResponse As FtpWebResponse = DirectCast(ftpClient.GetResponse(), FtpWebResponse)

                    Using dataStream As Stream = ftpResponse.GetResponseStream()
                        Using reader As New StreamReader(dataStream)
                            While True
                                Dim line As String = reader.ReadLine()
                                If String.IsNullOrEmpty(line) Then
                                    Exit While
                                End If

                                Dim subdirectorio As String = Path.Combine(directorio, line)

                                If Not line.Contains(".xml") Then
                                    ftpClient = DirectCast(WebRequest.Create(subdirectorio), FtpWebRequest)
                                    ftpClient.Credentials = New NetworkCredential("android", "android")
                                    ftpClient.Method = WebRequestMethods.Ftp.ListDirectory

                                    Try
                                        ftpResponse = DirectCast(ftpClient.GetResponse(), FtpWebResponse)
                                    Catch ex As WebException
                                        ' Registrar el error o mostrar un mensaje más descriptivo
                                        Continue While
                                    End Try

                                    Using dataStream2 As Stream = ftpResponse.GetResponseStream()
                                        Using reader2 As New StreamReader(dataStream2)
                                            While True
                                                Dim line2 As String = reader2.ReadLine()
                                                If String.IsNullOrEmpty(line2) Then
                                                    Exit While
                                                End If

                                                Dim archivoXml As String = Path.Combine(subdirectorio, line2)

                                                If line2.Contains(uuid) Then
                                                    archivosXml.Add(archivoXml)
                                                    encontrado = True
                                                End If
                                            End While
                                        End Using
                                    End Using
                                End If
                            End While
                        End Using
                    End Using

                    ftpResponse.Close()
                Catch ex As Exception
                    ' Registrar el error o mostrar un mensaje más descriptivo
                End Try
            Next
        Next

        If encontrado Then
            For Each archivo As String In archivosXml
                Dim partes As String() = archivo.Split(New String() {"/", ":"}, StringSplitOptions.RemoveEmptyEntries)
                Dim rfc As String = partes(partes.Length - 3)
                Dim mes As Integer = Integer.Parse(partes(partes.Length - 2))
                Dim anio As Integer = Integer.Parse(partes(partes.Length - 3))
                Dim nombreArchivo As String = Path.GetFileName(archivo)
                AgregarArchivoADataGridView(rfc, mes, anio, nombreArchivo)
            Next
        Else
            MessageBox.Show("Archivo UUID no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BuscarArchivosEnDirectorio(ftpClient As FtpWebRequest, directorio As String)
        ftpClient.Method = WebRequestMethods.Ftp.ListDirectory
        Dim ftpResponse As FtpWebResponse

        Try
            ftpResponse = DirectCast(ftpClient.GetResponse(), FtpWebResponse)
        Catch ex As WebException
            ' Registrar el error o mostrar un mensaje más descriptivo
            Return
        End Try

        Using dataStream As Stream = ftpResponse.GetResponseStream()
            Using reader As New StreamReader(dataStream)
                While True
                    Dim line As String = reader.ReadLine()
                    If String.IsNullOrEmpty(line) Then
                        Exit While
                    End If

                    Dim archivoXml As String = Path.Combine(directorio, line)

                    If line.Contains(".xml") Then
                        Dim partes As String() = directorio.Split(New String() {"/"}, StringSplitOptions.RemoveEmptyEntries)
                        Dim rfc As String = partes(partes.Length - 1)
                        Dim mes As Integer = Integer.Parse(partes(partes.Length - 2))
                        Dim anio As Integer = Integer.Parse(partes(partes.Length - 3))
                        AgregarArchivoADataGridView(rfc, mes, anio, line)
                    End If
                End While
            End Using
        End Using

        ftpResponse.Close()
    End Sub

    Private Sub AgregarArchivoADataGridView(rfc As String, mes As Integer, anio As Integer, archivoXml As String)
        Dim fila As New String() {False.ToString(), rfc, mes.ToString(), anio.ToString(), archivoXml, "Descargar"};
        DataGridView1.Rows.Add(fila)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 5 AndAlso e.RowIndex >= 0 Then
            DescargarArchivo(e.RowIndex)
        End If
    End Sub

    Private Sub DescargarArchivo(rowIndex As Integer)
        Dim ruta As String = DataGridView1.Rows(rowIndex).Cells(4).Value.ToString()
        Dim ftpCredential As New NetworkCredential("android", "android")
        Dim ftpClient As New FtpWebRequest(Path.Combine("ftp://192.168.1.12:2221/", ruta), ftpCredential)

        ftpClient.Method = WebRequestMethods.Ftp.DownloadFile

        Try
            Dim ftpResponse As FtpWebResponse = DirectCast(ftpClient.GetResponse(), FtpWebResponse)

            Using dataStream As Stream = ftpResponse.GetResponseStream()
                Dim saveFileDialog As New SaveFileDialog()
                saveFileDialog.Filter = "XML Files|*.xml"
                saveFileDialog.FileName = Path.GetFileName(ruta)

                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    Using fileStream As New FileStream(saveFileDialog.FileName, FileMode.Create)
                        dataStream.CopyTo(fileStream)
                    End Using
                End If
            End Using

            ftpResponse.Close()
        Catch ex As Exception
            MessageBox.Show($"Error al descargar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class