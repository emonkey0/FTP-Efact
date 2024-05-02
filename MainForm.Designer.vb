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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.SELECCIONAR = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RFC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MES = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AÑO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DESCARGAR = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.StartDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.EndDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.Buscar = New System.Windows.Forms.Button()
        Me.RFCTextBox = New System.Windows.Forms.TextBox()
        Me.UUIDTextBox = New System.Windows.Forms.TextBox()
        Me.DownloadButton = New System.Windows.Forms.Button()
        Me.RFCLabel = New System.Windows.Forms.Label()
        Me.UUIDLabel = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SELECCIONAR, Me.RFC, Me.MES, Me.AÑO, Me.UUID, Me.DESCARGAR})
        Me.DataGridView1.Location = New System.Drawing.Point(32, 120)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(678, 198)
        Me.DataGridView1.TabIndex = 5
        '
        'SELECCIONAR
        '
        Me.SELECCIONAR.HeaderText = "SELECCIONAR"
        Me.SELECCIONAR.Name = "SELECCIONAR"
        Me.SELECCIONAR.ReadOnly = True
        '
        'RFC
        '
        Me.RFC.HeaderText = "RFC"
        Me.RFC.Name = "RFC"
        Me.RFC.ReadOnly = True
        '
        'MES
        '
        Me.MES.HeaderText = "MES"
        Me.MES.Name = "MES"
        Me.MES.ReadOnly = True
        '
        'AÑO
        '
        Me.AÑO.HeaderText = "AÑO"
        Me.AÑO.Name = "AÑO"
        Me.AÑO.ReadOnly = True
        '
        'UUID
        '
        Me.UUID.HeaderText = "UUID"
        Me.UUID.Name = "UUID"
        Me.UUID.ReadOnly = True
        '
        'DESCARGAR
        '
        Me.DESCARGAR.HeaderText = "DESCARGAR"
        Me.DESCARGAR.Name = "DESCARGAR"
        Me.DESCARGAR.ReadOnly = True
        Me.DESCARGAR.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DESCARGAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'StartDateTimePicker
        '
        Me.StartDateTimePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.StartDateTimePicker.Location = New System.Drawing.Point(32, 33)
        Me.StartDateTimePicker.Name = "StartDateTimePicker"
        Me.StartDateTimePicker.Size = New System.Drawing.Size(200, 20)
        Me.StartDateTimePicker.TabIndex = 6
        '
        'EndDateTimePicker
        '
        Me.EndDateTimePicker.Location = New System.Drawing.Point(270, 33)
        Me.EndDateTimePicker.Name = "EndDateTimePicker"
        Me.EndDateTimePicker.Size = New System.Drawing.Size(200, 20)
        Me.EndDateTimePicker.TabIndex = 7
        '
        'Buscar
        '
        Me.Buscar.Location = New System.Drawing.Point(614, 30)
        Me.Buscar.Name = "Buscar"
        Me.Buscar.Size = New System.Drawing.Size(75, 23)
        Me.Buscar.TabIndex = 8
        Me.Buscar.Text = "Buscar"
        Me.Buscar.UseVisualStyleBackColor = True
        '
        'RFCTextBox
        '
        Me.RFCTextBox.Location = New System.Drawing.Point(497, 32)
        Me.RFCTextBox.Name = "RFCTextBox"
        Me.RFCTextBox.Size = New System.Drawing.Size(100, 20)
        Me.RFCTextBox.TabIndex = 9
        '
        'UUIDTextBox
        '
        Me.UUIDTextBox.Location = New System.Drawing.Point(497, 78)
        Me.UUIDTextBox.Name = "UUIDTextBox"
        Me.UUIDTextBox.Size = New System.Drawing.Size(100, 20)
        Me.UUIDTextBox.TabIndex = 10
        '
        'DownloadButton
        '
        Me.DownloadButton.Location = New System.Drawing.Point(435, 345)
        Me.DownloadButton.Name = "DownloadButton"
        Me.DownloadButton.Size = New System.Drawing.Size(75, 23)
        Me.DownloadButton.TabIndex = 11
        Me.DownloadButton.Text = "Button2"
        Me.DownloadButton.UseVisualStyleBackColor = True
        '
        'RFCLabel
        '
        Me.RFCLabel.AutoSize = True
        Me.RFCLabel.Location = New System.Drawing.Point(524, 9)
        Me.RFCLabel.Name = "RFCLabel"
        Me.RFCLabel.Size = New System.Drawing.Size(28, 13)
        Me.RFCLabel.TabIndex = 12
        Me.RFCLabel.Text = "RFC"
        '
        'UUIDLabel
        '
        Me.UUIDLabel.AutoSize = True
        Me.UUIDLabel.Location = New System.Drawing.Point(524, 62)
        Me.UUIDLabel.Name = "UUIDLabel"
        Me.UUIDLabel.Size = New System.Drawing.Size(34, 13)
        Me.UUIDLabel.TabIndex = 13
        Me.UUIDLabel.Text = "UUID"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.UUIDLabel)
        Me.Controls.Add(Me.RFCLabel)
        Me.Controls.Add(Me.DownloadButton)
        Me.Controls.Add(Me.UUIDTextBox)
        Me.Controls.Add(Me.RFCTextBox)
        Me.Controls.Add(Me.Buscar)
        Me.Controls.Add(Me.EndDateTimePicker)
        Me.Controls.Add(Me.StartDateTimePicker)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "MainForm"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As Windows.Forms.DataGridView
    Friend WithEvents StartDateTimePicker As Windows.Forms.DateTimePicker

    Private Sub DataGridView1_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Friend WithEvents SELECCIONAR As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents RFC As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MES As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AÑO As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UUID As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DESCARGAR As Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents EndDateTimePicker As Windows.Forms.DateTimePicker
    Friend WithEvents Buscar As Windows.Forms.Button
    Friend WithEvents RFCTextBox As Windows.Forms.TextBox
    Friend WithEvents UUIDTextBox As Windows.Forms.TextBox
    Friend WithEvents DownloadButton As Windows.Forms.Button
    Friend WithEvents RFCLabel As Windows.Forms.Label
    Friend WithEvents UUIDLabel As Windows.Forms.Label
End Class
