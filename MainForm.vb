Imports System.Net
Imports System.IO
Imports System.Windows.Forms

Public Class MainForm
    Private ftpServer As String = "ftp://192.168.1.12:2221"
    Private ftpUsername As String = "android"
    Private ftpPassword As String = "android"

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuración inicial del formulario
        fechaI_txt.MaxDate = New DateTime(Now.Year, 12, 31)
        fechaF.MaxDate = New DateTime(Now.Year, 12, 31)
        fechaF.MinDate = New DateTime(Now.Year, 1, 1)
        fechaI_txt.Value = New DateTime(Now.Year, Now.Month, 1)
        fechaF.Value = New DateTime(Now.Year, Now.Month, DateTime.DaysInMonth(Now.Year, Now.Month))
    End Sub

    Private Sub Buscar_Click(sender As Object, e As EventArgs) Handles Buscar.Click
        Dim rfc As String = rfc_txt.Text
        Dim startDate As DateTime = fechaI_txt.Value
        Dim endDate As DateTime = fechaF.Value

        ' Limpiar DataGridView antes de realizar una nueva búsqueda
        DataGridView1.Rows.Clear()

        ' Llamar a los métodos de búsqueda según los parámetros proporcionados
        If String.IsNullOrWhiteSpace(rfc) Then
            ' Búsqueda por rango de fechas
            SearchByDate(startDate, endDate)
        Else
            ' Búsqueda por RFC y rango de fechas
            SearchByRFC(rfc, startDate, endDate)
        End If
    End Sub

    Private Sub SearchByDate(startDate As DateTime, endDate As DateTime)
        Try
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpServer), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            request.Credentials = New NetworkCredential(ftpUsername, ftpPassword)

            Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
                Using stream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(stream)
                        Dim line As String = reader.ReadLine()
                        While line IsNot Nothing
                            If IsDateFolderInRange(line, startDate, endDate) Then
                                Dim directory As String = line
                                Dim files As List(Of String) = GetXmlFilesInDirectory(directory)
                                AddFilesToDataGridView(files, directory)
                            End If
                            line = reader.ReadLine()
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al realizar la búsqueda por fecha: " & ex.Message)
        End Try
    End Sub

    Private Function IsDateFolderInRange(folderName As String, startDate As DateTime, endDate As DateTime) As Boolean
        Dim folderDate As DateTime
        If DateTime.TryParse(folderName, folderDate) Then
            Return folderDate >= startDate AndAlso folderDate <= endDate
        End If
        Return False
    End Function

    Private Function GetXmlFilesInDirectory(directory As String) As List(Of String)
        Dim files As New List(Of String)()

        Try
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create($"{ftpServer}/{directory}"), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            request.Credentials = New NetworkCredential(ftpUsername, ftpPassword)

            Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
                Using stream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(stream)
                        Dim line As String = reader.ReadLine()
                        While line IsNot Nothing
                            If line.EndsWith(".xml") Then
                                files.Add($"{ftpServer}/{directory}/{line}")
                            End If
                            line = reader.ReadLine()
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error al obtener archivos .xml en el directorio {directory}: {ex.Message}")
        End Try

        Return files
    End Function

    Private Sub AddFilesToDataGridView(files As List(Of String), directory As String)
        For Each file In files
            Dim fileName As String = Path.GetFileName(file)
            Dim row As String() = New String() {directory, fileName}
            DataGridView1.Rows.Add(row)
        Next
    End Sub

    Private Sub SearchByRFC(rfc As String, startDate As DateTime, endDate As DateTime)
        ' Búsqueda por RFC y rango de fechas
        Try
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpServer), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            request.Credentials = New NetworkCredential(ftpUsername, ftpPassword)

            Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
                Using stream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(stream)
                        Dim line As String = reader.ReadLine()
                        While line IsNot Nothing
                            Dim directory As String = line
                            If directory.StartsWith(rfc) AndAlso IsDateFolderInRange(directory.Substring(rfc.Length + 1), startDate, endDate) Then
                                Dim files As List(Of String) = GetXmlFilesInDirectory(directory)
                                AddFilesToDataGridView(files, directory)
                            End If
                            line = reader.ReadLine()
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al realizar la búsqueda por RFC: " & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim senderGrid = DirectCast(sender, DataGridView)
            If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
               e.RowIndex >= 0 Then
                Dim x = New FolderBrowserDialog
                x.ShowDialog()
                'Dim coneccionFTP = "ftp://ftp.site4now.net/webService/XML_GESEM/" & año & "/" & mes & "/" & rfc & "/" & DataGridView1.Rows(e.RowIndex).Cells("UUID").Value
                Dim coneccionFTP = DataGridView1.Rows(e.RowIndex).Cells("ruta").Value
                My.Computer.Network.DownloadFile(coneccionFTP, x.SelectedPath & "/" & DataGridView1.Rows(e.RowIndex).Cells("UUID").Value, "efactweb-002", "8e344aFea4a21825")
                MessageBox.Show("DESCARGA FINALIZADA")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub descargarTodo_btn_Click(sender As Object, e As EventArgs) Handles descargarTodo_btn.Click
        Try
            Dim dialog = New FolderBrowserDialog
            dialog.ShowDialog()
            Dim folder = dialog.SelectedPath
            For Each row As DataGridViewRow In DataGridView1.Rows
                My.Computer.Network.DownloadFile(row.Cells("ruta").Value, folder & "/" & row.Cells("UUID").Value, "efactweb-002", "8e344aFea4a21825")
            Next
            MessageBox.Show("DESCARGA FINALIZADA")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function MES(mesNum As Integer) As String
        Select Case mesNum
            Case 1
                Return "ENERO"
            Case 2
                Return "FEBRERO"
            Case 3
                Return "MARZO"
            Case 4
                Return "ABRIL"
            Case 5
                Return "MAYO"
            Case 6
                Return "JUNIO"
            Case 7
                Return "JULIO"
            Case 8
                Return "AGOSTO"
            Case 9
                Return "SEPTIEMBRE"
            Case 10
                Return "OCTUBRE"
            Case 11
                Return "NOVIEMBRE"
            Case 12
                Return "DICIEMBRE"
        End Select
        Return ""
    End Function
End Class
