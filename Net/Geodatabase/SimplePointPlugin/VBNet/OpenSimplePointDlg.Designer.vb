'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports Microsoft.VisualBasic
Imports System
Partial Public Class OpenSimplePointDlg
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
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OpenSimplePointDlg))
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnOK = New System.Windows.Forms.Button()
    Me.btnOpenDataSource = New System.Windows.Forms.Button()
    Me.groupBox1 = New System.Windows.Forms.GroupBox()
    Me.lblDatasets = New System.Windows.Forms.Label()
    Me.lstDeatureClasses = New System.Windows.Forms.ListBox()
    Me.txtPath = New System.Windows.Forms.TextBox()
    Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.groupBox1.SuspendLayout()
    Me.SuspendLayout()
    ' 
    ' btnCancel
    ' 
    Me.btnCancel.Location = New System.Drawing.Point(195, 276)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 23)
    Me.btnCancel.TabIndex = 0
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '	  Me.btnCancel.Click += New System.EventHandler(Me.btnCancel_Click);
    ' 
    ' btnOK
    ' 
    Me.btnOK.Location = New System.Drawing.Point(37, 276)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(75, 23)
    Me.btnOK.TabIndex = 1
    Me.btnOK.Text = "OK"
    Me.btnOK.UseVisualStyleBackColor = True
    '	  Me.btnOK.Click += New System.EventHandler(Me.btnOK_Click);
    ' 
    ' btnOpenDataSource
    ' 
    Me.btnOpenDataSource.Image = (CType(resources.GetObject("btnOpenDataSource.Image"), System.Drawing.Image))
    Me.btnOpenDataSource.Location = New System.Drawing.Point(249, 17)
    Me.btnOpenDataSource.Name = "btnOpenDataSource"
    Me.btnOpenDataSource.Size = New System.Drawing.Size(23, 23)
    Me.btnOpenDataSource.TabIndex = 2
    Me.toolTip1.SetToolTip(Me.btnOpenDataSource, "Open Simple Point data")
    Me.btnOpenDataSource.UseVisualStyleBackColor = True
    '	  Me.btnOpenDataSource.Click += New System.EventHandler(Me.btnOpenDataSource_Click);
    ' 
    ' groupBox1
    ' 
    Me.groupBox1.Controls.Add(Me.lblDatasets)
    Me.groupBox1.Controls.Add(Me.lstDeatureClasses)
    Me.groupBox1.Controls.Add(Me.txtPath)
    Me.groupBox1.Controls.Add(Me.btnOpenDataSource)
    Me.groupBox1.Location = New System.Drawing.Point(12, 12)
    Me.groupBox1.Name = "groupBox1"
    Me.groupBox1.Size = New System.Drawing.Size(280, 249)
    Me.groupBox1.TabIndex = 3
    Me.groupBox1.TabStop = False
    ' 
    ' lblDatasets
    ' 
    Me.lblDatasets.AutoSize = True
    Me.lblDatasets.Location = New System.Drawing.Point(6, 51)
    Me.lblDatasets.Name = "lblDatasets"
    Me.lblDatasets.Size = New System.Drawing.Size(52, 13)
    Me.lblDatasets.TabIndex = 5
    Me.lblDatasets.Text = "Datasets:"
    ' 
    ' lstDeatureClasses
    ' 
    Me.lstDeatureClasses.FormattingEnabled = True
    Me.lstDeatureClasses.Location = New System.Drawing.Point(6, 67)
    Me.lstDeatureClasses.Name = "lstDeatureClasses"
    Me.lstDeatureClasses.Size = New System.Drawing.Size(266, 173)
    Me.lstDeatureClasses.TabIndex = 4
    '	  Me.lstDeatureClasses.DoubleClick += New System.EventHandler(Me.lstDeatureClasses_DoubleClick);
    ' 
    ' txtPath
    ' 
    Me.txtPath.Location = New System.Drawing.Point(6, 19)
    Me.txtPath.Name = "txtPath"
    Me.txtPath.ReadOnly = True
    Me.txtPath.Size = New System.Drawing.Size(237, 20)
    Me.txtPath.TabIndex = 3
    ' 
    ' OpenSimplePointDlg
    ' 
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(304, 312)
    Me.Controls.Add(Me.groupBox1)
    Me.Controls.Add(Me.btnOK)
    Me.Controls.Add(Me.btnCancel)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "OpenSimplePointDlg"
    Me.ShowInTaskbar = False
    Me.Text = "OpenSimplePointDlg"
    Me.TopMost = True
    Me.groupBox1.ResumeLayout(False)
    Me.groupBox1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private WithEvents btnCancel As System.Windows.Forms.Button
  Private WithEvents btnOK As System.Windows.Forms.Button
  Private WithEvents btnOpenDataSource As System.Windows.Forms.Button
  Private groupBox1 As System.Windows.Forms.GroupBox
  Private toolTip1 As System.Windows.Forms.ToolTip
  Private txtPath As System.Windows.Forms.TextBox
  Private WithEvents lstDeatureClasses As System.Windows.Forms.ListBox
  Private lblDatasets As System.Windows.Forms.Label
End Class