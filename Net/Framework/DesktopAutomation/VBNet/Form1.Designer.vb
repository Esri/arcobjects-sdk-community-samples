<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.txtShapeFilePath = New System.Windows.Forms.TextBox
        Me.cboApps = New System.Windows.Forms.ComboBox
        Me.btnShutdown = New System.Windows.Forms.Button
        Me.btnDrive = New System.Windows.Forms.Button
        Me.btnStartApp = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(12, 164)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(253, 13)
        Me.label3.TabIndex = 15
        Me.label3.Text = "Shut down application (all changes are abandoned):"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(12, 85)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(221, 13)
        Me.label2.TabIndex = 14
        Me.label2.Text = "Enter the shapefile path to add to application:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 10)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(188, 13)
        Me.label1.TabIndex = 13
        Me.label1.Text = "Choose an ArcGIS Application to start:"
        '
        'txtShapeFilePath
        '
        Me.txtShapeFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtShapeFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.txtShapeFilePath.Enabled = False
        Me.txtShapeFilePath.Location = New System.Drawing.Point(12, 101)
        Me.txtShapeFilePath.Name = "txtShapeFilePath"
        Me.txtShapeFilePath.Size = New System.Drawing.Size(290, 20)
        Me.txtShapeFilePath.TabIndex = 12
        '
        'cboApps
        '
        Me.cboApps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboApps.FormattingEnabled = True
        Me.cboApps.Items.AddRange(New Object() {"ArcMap", "ArcScene", "ArcGlobe"})
        Me.cboApps.Location = New System.Drawing.Point(12, 26)
        Me.cboApps.Name = "cboApps"
        Me.cboApps.Size = New System.Drawing.Size(290, 21)
        Me.cboApps.TabIndex = 11
        '
        'btnShutdown
        '
        Me.btnShutdown.Enabled = False
        Me.btnShutdown.Location = New System.Drawing.Point(12, 180)
        Me.btnShutdown.Name = "btnShutdown"
        Me.btnShutdown.Size = New System.Drawing.Size(290, 23)
        Me.btnShutdown.TabIndex = 10
        Me.btnShutdown.Text = "Exit"
        Me.btnShutdown.UseVisualStyleBackColor = True
        '
        'btnDrive
        '
        Me.btnDrive.Enabled = False
        Me.btnDrive.Location = New System.Drawing.Point(12, 127)
        Me.btnDrive.Name = "btnDrive"
        Me.btnDrive.Size = New System.Drawing.Size(290, 23)
        Me.btnDrive.TabIndex = 9
        Me.btnDrive.Text = "Add a layer"
        Me.btnDrive.UseVisualStyleBackColor = True
        '
        'btnStartApp
        '
        Me.btnStartApp.Location = New System.Drawing.Point(12, 53)
        Me.btnStartApp.Name = "btnStartApp"
        Me.btnStartApp.Size = New System.Drawing.Size(290, 23)
        Me.btnStartApp.TabIndex = 8
        Me.btnStartApp.Text = "Start"
        Me.btnStartApp.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(318, 219)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txtShapeFilePath)
        Me.Controls.Add(Me.cboApps)
        Me.Controls.Add(Me.btnShutdown)
        Me.Controls.Add(Me.btnDrive)
        Me.Controls.Add(Me.btnStartApp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.Text = "Driving Application Form"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents txtShapeFilePath As System.Windows.Forms.TextBox
    Private WithEvents cboApps As System.Windows.Forms.ComboBox
    Private WithEvents btnShutdown As System.Windows.Forms.Button
    Private WithEvents btnDrive As System.Windows.Forms.Button
    Private WithEvents btnStartApp As System.Windows.Forms.Button

End Class
