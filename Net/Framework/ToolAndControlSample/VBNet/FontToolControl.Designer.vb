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
Partial Class FontToolControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        'Clean up
        If m_hBitmap.ToInt32() <> 0 Then
            DeleteObject(m_hBitmap)
            m_hBitmap = IntPtr.Zero
        End If

        If m_ifc IsNot Nothing Then
            m_ifc.Dispose()
            m_ifc = Nothing
        End If

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
        Me.cboFont = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'cboFont
        '
        Me.cboFont.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFont.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFont.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboFont.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFont.FormattingEnabled = True
        Me.cboFont.Location = New System.Drawing.Point(0, 0)
        Me.cboFont.Name = "cboFont"
        Me.cboFont.Size = New System.Drawing.Size(200, 21)
        Me.cboFont.TabIndex = 2
        '
        'FontToolControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cboFont)
        Me.Name = "FontToolControl"
        Me.Size = New System.Drawing.Size(200, 20)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents cboFont As System.Windows.Forms.ComboBox

End Class
