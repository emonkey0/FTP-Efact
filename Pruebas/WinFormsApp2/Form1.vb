Imports System.Net

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ftpPath As String = "ftp://192.168.1.12:2221/"
        Dim rootNode As TreeNode = GetFtpDirectoryTree(ftpPath)
        PopulateDataGridView(rootNode)
    End Sub

    Private Function GetFtpDirectoryTree(ByVal ftpPath As String) As TreeNode
        Dim rootNode As New TreeNode("Root")
        GetFtpDirectoryTreeRecursively(ftpPath, rootNode)
        Return rootNode
    End Function

    Private Sub GetFtpDirectoryTreeRecursively(ByVal ftpPath As String, ByVal parentNode As TreeNode)
        Dim request As FtpWebRequest = WebRequest.Create(ftpPath)
        request.Method = WebRequestMethods.Ftp.ListDirectoryDetails

        Using response As FtpWebResponse = request.GetResponse()
            Using stream As IO.Stream = response.GetResponseStream()
                Using reader As New IO.StreamReader(stream)
                    While Not reader.EndOfStream
                        Dim line As String = reader.ReadLine()
                        Dim fileName As String = line.Substring(line.LastIndexOf(" ") + 1)
                        Dim isDirectory As Boolean = line.StartsWith("d")
                        Dim newNode As New TreeNode(fileName)

                        If isDirectory Then
                            newNode.Tag = ftpPath & fileName & "/"
                            GetFtpDirectoryTreeRecursively(ftpPath & fileName & "/", newNode)
                        Else
                            ' Add file to parent node
                        End If

                        parentNode.Nodes.Add(newNode)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub PopulateDataGridView(ByVal rootNode As TreeNode)
        For Each node As TreeNode In rootNode.Nodes
            AddNodeToDataGridView(node, Nothing)
        Next
    End Sub

    Private Sub AddNodeToDataGridView(ByVal node As TreeNode, ByVal parentNode As DataGridViewRow)
        Dim newRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Add())

        ' Set cell values for the row based on node information
        newRow.Cells("FileNameColumn").Value = node.Text

        ' Add row to parent node if applicable
        If parentNode IsNot Nothing Then
            ' Add newRow to parentNode
        End If

        ' Recursively add child nodes
        For Each childNode As TreeNode In node.Nodes
            AddNodeToDataGridView(childNode, newRow)
        Next
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
