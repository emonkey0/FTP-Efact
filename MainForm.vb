Imports System.IO
Imports System.Net
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel

Public Class MainForm
    Private Sub ConnectButton_Click(sender As Object, e As EventArgs) Handles ConnectButton.Click
        ' Establish FTP connection
        Dim ftpAddress As String = FtpAddressTextBox.Text
        Dim username As String = "android"
        Dim password As String = "android"

        Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpAddress), FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.ListDirectoryDetails
        request.Credentials = New NetworkCredential(username, password)

        Try
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Dim responseStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(responseStream)
            Dim directoryContent As String = reader.ReadToEnd()
            response.Close()

            ' Display directory content in ListBox
            FileListBox.Items.Clear()
            FileListBox.Items.AddRange(directoryContent.Split(vbCrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
        Catch ex As Exception
            MessageBox.Show("Error connecting to FTP server: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Dim searchText As String = SearchTextBox.Text
        Dim fileList As New List(Of String)

        ' Filter files based on search text
        For Each item As String In FileListBox.Items
            If item.Contains(searchText) Then
                fileList.Add(item)
            End If
        Next

        ' Display filtered files in ListBox
        FilteredFileListBox.Items.Clear()
        FilteredFileListBox.Items.AddRange(fileList.ToArray())
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
