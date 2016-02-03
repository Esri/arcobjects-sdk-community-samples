Imports Microsoft.VisualBasic
Imports System
Partial Public Class frmPropertyPage
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (Not components Is Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.checkBoxTrace = New System.Windows.Forms.CheckBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.helpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' checkBoxTrace
        ' 
        Me.checkBoxTrace.AutoSize = True
        Me.checkBoxTrace.Location = New System.Drawing.Point(6, 35)
        Me.checkBoxTrace.Name = "checkBoxTrace"
        Me.checkBoxTrace.Size = New System.Drawing.Size(185, 17)
        Me.checkBoxTrace.TabIndex = 0
        Me.checkBoxTrace.Text = "Show trace of the moving graphic"
        Me.checkBoxTrace.UseVisualStyleBackColor = True
        '			Me.checkBoxTrace.Click += New System.EventHandler(Me.checkBoxTrace_Click);
        ' 
        ' groupBox1
        ' 
        Me.groupBox1.Controls.Add(Me.checkBoxTrace)
        Me.groupBox1.Location = New System.Drawing.Point(12, 12)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(301, 101)
        Me.groupBox1.TabIndex = 1
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Trace Properties"
        ' 
        ' frmPropertyPage
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 139)
        Me.ControlBox = False
        Me.Controls.Add(Me.groupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Name = "frmPropertyPage"
        Me.Text = "Graphic Track Properties"
        '			Me.Load += New System.EventHandler(Me.frmPropertyPage_Load);
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents checkBoxTrace As System.Windows.Forms.CheckBox
    Private groupBox1 As System.Windows.Forms.GroupBox
    Private helpProvider1 As System.Windows.Forms.HelpProvider
End Class
