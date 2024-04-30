<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ConnectButton = New System.Windows.Forms.Button()
        Me.FtpAddressTextBox = New System.Windows.Forms.TextBox()
        Me.FileListBox = New System.Windows.Forms.ListBox()
        Me.SearchTextBox = New System.Windows.Forms.TextBox()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.FilteredFileListBox = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'ConnectButton
        '
        Me.ConnectButton.Location = New System.Drawing.Point(353, 220)
        Me.ConnectButton.Name = "ConnectButton"
        Me.ConnectButton.Size = New System.Drawing.Size(75, 23)
        Me.ConnectButton.TabIndex = 0
        Me.ConnectButton.Text = "Connect"
        Me.ConnectButton.UseVisualStyleBackColor = True
        '
        'FtpAddressTextBox
        '
        Me.FtpAddressTextBox.Location = New System.Drawing.Point(340, 194)
        Me.FtpAddressTextBox.Name = "FtpAddressTextBox"
        Me.FtpAddressTextBox.Size = New System.Drawing.Size(100, 20)
        Me.FtpAddressTextBox.TabIndex = 1
        '
        'FileListBox
        '
        Me.FileListBox.FormattingEnabled = True
        Me.FileListBox.Location = New System.Drawing.Point(467, 129)
        Me.FileListBox.Name = "FileListBox"
        Me.FileListBox.Size = New System.Drawing.Size(120, 95)
        Me.FileListBox.TabIndex = 2
        '
        'SearchTextBox
        '
        Me.SearchTextBox.Location = New System.Drawing.Point(594, 12)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.Size = New System.Drawing.Size(100, 20)
        Me.SearchTextBox.TabIndex = 3
        Me.SearchTextBox.Text = "SearchTextBox"
        '
        'SearchButton
        '
        Me.SearchButton.Location = New System.Drawing.Point(604, 38)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(75, 23)
        Me.SearchButton.TabIndex = 4
        Me.SearchButton.Text = "Search"
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'FilteredFileListBox
        '
        Me.FilteredFileListBox.FormattingEnabled = True
        Me.FilteredFileListBox.Location = New System.Drawing.Point(638, 129)
        Me.FilteredFileListBox.Name = "FilteredFileListBox"
        Me.FilteredFileListBox.Size = New System.Drawing.Size(120, 95)
        Me.FilteredFileListBox.TabIndex = 5
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.FilteredFileListBox)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.SearchTextBox)
        Me.Controls.Add(Me.FileListBox)
        Me.Controls.Add(Me.FtpAddressTextBox)
        Me.Controls.Add(Me.ConnectButton)
        Me.Name = "MainForm"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ConnectButton As Windows.Forms.Button
    Friend WithEvents FtpAddressTextBox As Windows.Forms.TextBox
    Friend WithEvents FileListBox As Windows.Forms.ListBox
    Friend WithEvents SearchTextBox As Windows.Forms.TextBox
    Friend WithEvents SearchButton As Windows.Forms.Button
    Friend WithEvents FilteredFileListBox As Windows.Forms.ListBox
End Class
