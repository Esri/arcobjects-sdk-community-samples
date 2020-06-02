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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ESRIWebsitesWindow
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.webBrowser1 = New System.Windows.Forms.WebBrowser
        Me.cboURLs = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'webBrowser1
        '
        Me.webBrowser1.AllowWebBrowserDrop = False
        Me.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webBrowser1.IsWebBrowserContextMenuEnabled = False
        Me.webBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.webBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webBrowser1.Name = "webBrowser1"
        Me.webBrowser1.ScriptErrorsSuppressed = True
        Me.webBrowser1.Size = New System.Drawing.Size(340, 137)
        Me.webBrowser1.TabIndex = 3
        '
        'cboURLs
        '
        Me.cboURLs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cboURLs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboURLs.FormattingEnabled = True
        Me.cboURLs.Location = New System.Drawing.Point(0, 137)
        Me.cboURLs.Name = "cboURLs"
        Me.cboURLs.Size = New System.Drawing.Size(340, 21)
        Me.cboURLs.TabIndex = 4
        '
        'ESRIWebsitesWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.webBrowser1)
        Me.Controls.Add(Me.cboURLs)
        Me.Name = "ESRIWebsitesWindow"
        Me.Size = New System.Drawing.Size(340, 158)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents webBrowser1 As System.Windows.Forms.WebBrowser
    Private WithEvents cboURLs As System.Windows.Forms.ComboBox

End Class
