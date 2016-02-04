'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TabbedInspector
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.customTabPage = New System.Windows.Forms.TabPage
    Me.customListBox = New System.Windows.Forms.ListBox
    Me.defaultPictureBox = New System.Windows.Forms.PictureBox
    Me.inspectorTabControl = New System.Windows.Forms.TabControl
    Me.standardTabPage = New System.Windows.Forms.TabPage
    Me.customTabPage.SuspendLayout()
    DirectCast(Me.defaultPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.inspectorTabControl.SuspendLayout()
    Me.standardTabPage.SuspendLayout()
    Me.SuspendLayout()
    '
    'customTabPage
    '
    Me.customTabPage.AutoScroll = True
    Me.customTabPage.Controls.Add(Me.customListBox)
    Me.customTabPage.Location = New System.Drawing.Point(4, 22)
    Me.customTabPage.Name = "customTabPage"
    Me.customTabPage.Padding = New System.Windows.Forms.Padding(3)
    Me.customTabPage.Size = New System.Drawing.Size(292, 294)
    Me.customTabPage.TabIndex = 1
    Me.customTabPage.Text = "Custom"
    Me.customTabPage.UseVisualStyleBackColor = True
    '
    'customListBox
    '
    Me.customListBox.Dock = System.Windows.Forms.DockStyle.Fill
    Me.customListBox.FormattingEnabled = True
    Me.customListBox.Location = New System.Drawing.Point(3, 3)
    Me.customListBox.MaximumSize = New System.Drawing.Size(1000, 1000)
    Me.customListBox.MultiColumn = True
    Me.customListBox.Name = "customListBox"
    Me.customListBox.Size = New System.Drawing.Size(286, 277)
    Me.customListBox.TabIndex = 0
    '
    'defaultPictureBox
    '
    Me.defaultPictureBox.BackColor = System.Drawing.SystemColors.Window
    Me.defaultPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.defaultPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
    Me.defaultPictureBox.Location = New System.Drawing.Point(3, 3)
    Me.defaultPictureBox.MaximumSize = New System.Drawing.Size(1000, 1000)
    Me.defaultPictureBox.Name = "defaultPictureBox"
    Me.defaultPictureBox.Size = New System.Drawing.Size(136, 118)
    Me.defaultPictureBox.TabIndex = 0
    Me.defaultPictureBox.TabStop = False
    '
    'inspectorTabControl
    '
    Me.inspectorTabControl.Controls.Add(Me.standardTabPage)
    Me.inspectorTabControl.Controls.Add(Me.customTabPage)
    Me.inspectorTabControl.Dock = System.Windows.Forms.DockStyle.Fill
    Me.inspectorTabControl.Location = New System.Drawing.Point(0, 0)
    Me.inspectorTabControl.MaximumSize = New System.Drawing.Size(1000, 1000)
    Me.inspectorTabControl.Name = "inspectorTabControl"
    Me.inspectorTabControl.SelectedIndex = 0
    Me.inspectorTabControl.Size = New System.Drawing.Size(150, 150)
    Me.inspectorTabControl.TabIndex = 1
    '
    'standardTabPage
    '
    Me.standardTabPage.AutoScroll = True
    Me.standardTabPage.Controls.Add(Me.defaultPictureBox)
    Me.standardTabPage.Location = New System.Drawing.Point(4, 22)
    Me.standardTabPage.Name = "standardTabPage"
    Me.standardTabPage.Padding = New System.Windows.Forms.Padding(3)
    Me.standardTabPage.Size = New System.Drawing.Size(142, 124)
    Me.standardTabPage.TabIndex = 0
    Me.standardTabPage.Text = "Standard"
    Me.standardTabPage.UseVisualStyleBackColor = True
    '
    'TabbedInspector
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.inspectorTabControl)
    Me.Name = "TabbedInspector"
    Me.customTabPage.ResumeLayout(False)
    DirectCast(Me.defaultPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.inspectorTabControl.ResumeLayout(False)
    Me.standardTabPage.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Private WithEvents customTabPage As System.Windows.Forms.TabPage
  Private WithEvents customListBox As System.Windows.Forms.ListBox
  Private WithEvents defaultPictureBox As System.Windows.Forms.PictureBox
  Private WithEvents inspectorTabControl As System.Windows.Forms.TabControl
  Private WithEvents standardTabPage As System.Windows.Forms.TabPage

End Class
