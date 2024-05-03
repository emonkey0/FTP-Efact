Imports System.IO
Imports System.Net
Imports System.Windows.Forms
Imports System.Xml

Public Class MainForm

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim ftpUrl As String = "ftp://192.168.100.107:2221/"
        Dim ftpUser As String = "android"
        Dim ftpPassword As String = "android"

        Dim rfc As String = RFCTextBox.Text.Trim()
        Dim uuid As String = UUIDTextBox.Text.Trim()
        Dim fechaInicio As DateTime = StartDateTimePicker.Value
        Dim fechaFin As DateTime = EndDateTimePicker.Value

        Dim xmlFiles As New List(Of String)()

        If String.IsNullOrEmpty(rfc) AndAlso String.IsNullOrEmpty(uuid) Then
            ' Primer Caso: Buscar todos los archivos entre las fechas especificadas
            For year As Integer = fechaInicio.Year To fechaFin.Year
                For month As Integer = 1 To 12
                    If (year = fechaInicio.Year AndAlso month < fechaInicio.Month) OrElse
                       (year = fechaFin.Year AndAlso month > fechaFin.Month) Then
                        Continue For
                    End If

                    Dim monthPath As String = ftpUrl & year & "/" & month & "/"
                    Dim request As FtpWebRequest = CType(WebRequest.Create(monthPath), FtpWebRequest)
                    request.Credentials = New NetworkCredential(ftpUser, ftpPassword)
                    request.Method = WebRequestMethods.Ftp.ListDirectory

                    Try
                        Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
                            Using reader As New StreamReader(response.GetResponseStream())
                                Dim line As String
                                While (InlineAssignHelper(line, reader.ReadLine())) IsNot Nothing
                                    Dim rfcPath As String = monthPath & line & "/"
                                    Dim rfcRequest As FtpWebRequest = CType(WebRequest.Create(rfcPath), FtpWebRequest)
                                    rfcRequest.Credentials = New NetworkCredential(ftpUser, ftpPassword)
                                    rfcRequest.Method = WebRequestMethods.Ftp.ListDirectory

                                    Try
                                        Using rfcResponse As FtpWebResponse = CType(rfcRequest.GetResponse(), FtpWebResponse)
                                            Using rfcReader As New StreamReader(rfcResponse.GetResponseStream())
                                                Dim xmlFile As String
                                                While (InlineAssignHelper(xmlFile, rfcReader.ReadLine())) IsNot Nothing
                                                    xmlFiles.Add(rfcPath & xmlFile)
                                                End While
                                            End Using
                                        End Using
                                    Catch ex As WebException
                                        ' El directorio RFC no existe, continuar con el siguiente RFC
                                    End Try
                                End While
                            End Using
                        End Using
                    Catch ex As WebException
                        ' El directorio del mes no existe, continuar con el siguiente mes
                    End Try
                Next
            Next
        ElseIf Not String.IsNullOrEmpty(rfc) AndAlso String.IsNullOrEmpty(uuid) Then
            ' Segundo Caso: Buscar archivos por RFC y fechas especificadas
            For year As Integer = fechaInicio.Year To fechaFin.Year
                For month As Integer = 1 To 12
                    If (year = fechaInicio.Year AndAlso month < fechaInicio.Month) OrElse
                       (year = fechaFin.Year AndAlso month > fechaFin.Month) Then
                        Continue For
                    End If

                    Dim rfcPath As String = ftpUrl & year & "/" & month & "/" & rfc & "/"
                    Dim request As FtpWebRequest = CType(WebRequest.Create(rfcPath), FtpWebRequest)
                    request.Credentials = New NetworkCredential(ftpUser, ftpPassword)
                    request.Method = WebRequestMethods.Ftp.ListDirectory

                    Try
                        Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
                            Using reader As New StreamReader(response.GetResponseStream())
                                Dim xmlFile As String
                                While (InlineAssignHelper(xmlFile, reader.ReadLine())) IsNot Nothing
                                    xmlFiles.Add(rfcPath & xmlFile)
                                End While
                            End Using
                        End Using
                    Catch ex As WebException
                        ' El directorio RFC no existe, continuar con el siguiente mes
                    End Try
                Next
            Next
        ElseIf String.IsNullOrEmpty(rfc) AndAlso Not String.IsNullOrEmpty(uuid) Then
            ' Tercer Caso: Buscar archivo por UUID
            For year As Integer = 2021 To DateTime.Now.Year
                For month As Integer = 1 To 12
                    Dim xmlPath As String = ftpUrl & year & "/" & month & "/"
                    Dim request As FtpWebRequest = CType(WebRequest.Create(xmlPath), FtpWebRequest)
                    request.Credentials = New NetworkCredential(ftpUser, ftpPassword)
                    request.Method = WebRequestMethods.Ftp.ListDirectory

                    Try
                        Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
                            Using reader As New StreamReader(response.GetResponseStream())
                                Dim line As String
                                While (InlineAssignHelper(line, reader.ReadLine())) IsNot Nothing
                                    If line.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) AndAlso line.StartsWith(uuid, StringComparison.OrdinalIgnoreCase) Then
                                        xmlFiles.Add(xmlPath & line)
                                        Exit For
                                    End If
                                End While
                            End Using
                        End Using
                    Catch ex As WebException
                        ' El directorio no existe, continuar con el siguiente mes
                    End Try

                    If xmlFiles.Count > 0 Then
                        Exit For
                    End If
                Next

                If xmlFiles.Count > 0 Then
                    Exit For
                End If
            Next
        End If

        ' Renderizar los resultados en el DataGridView
        dgvResultados.Rows.Clear()
        For Each xmlFile As String In xmlFiles
            Dim fileName As String = Path.GetFileName(xmlFile)
            Dim rfcName As String = Path.GetFileName(Path.GetDirectoryName(xmlFile))
            Dim month As String = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(xmlFile)))
            Dim year As String = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(xmlFile))))

            dgvResultados.Rows.Add(False, rfcName, month, year, fileName, "Descargar")
        Next
    End Sub

    Private Sub dgvResultados_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResultados.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 5 Then
            ' Descargar el archivo XML seleccionado
            Dim xmlFile As String = dgvResultados.Rows(e.RowIndex).Cells(4).Value.ToString()
            Dim fileName As String = Path.GetFileName(xmlFile)
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.FileName = fileName
            saveFileDialog.Filter = "Archivos XML (*.xml)|*.xml"

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Dim ftpUrl As String = "ftp://192.168.100.107:2221/"
                Dim ftpUser As String = "android"
                Dim ftpPassword As String = "android"

                ' Obtener la ruta completa del archivo en el servidor FTP
                Dim fullPath As String = dgvResultados.Rows(e.RowIndex).Cells(3).Value.ToString() & "/" &
                                 dgvResultados.Rows(e.RowIndex).Cells(2).Value.ToString() & "/" &
                                 dgvResultados.Rows(e.RowIndex).Cells(1).Value.ToString() & "/" &
                                 dgvResultados.Rows(e.RowIndex).Cells(4).Value.ToString()

                Dim request As FtpWebRequest = CType(WebRequest.Create(ftpUrl & fullPath), FtpWebRequest)
                request.Credentials = New NetworkCredential(ftpUser, ftpPassword)
                request.Method = WebRequestMethods.Ftp.DownloadFile

                Try
                    Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
                        Using responseStream As Stream = response.GetResponseStream()
                            Using fileStream As New FileStream(saveFileDialog.FileName, FileMode.Create)
                                responseStream.CopyTo(fileStream)
                            End Using
                        End Using
                    End Using

                    MessageBox.Show("Archivo descargado exitosamente.")
                Catch ex As WebException
                    MessageBox.Show("Error al descargar el archivo. " & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function


End Class