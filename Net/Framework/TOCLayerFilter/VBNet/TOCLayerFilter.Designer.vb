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
Partial Class TOCLayerFilter
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
        Me.components = New System.ComponentModel.Container
        Me.tvwLayer = New System.Windows.Forms.TreeView
        Me.cboLayerType = New System.Windows.Forms.ComboBox
        Me.contextMenuDummy = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SuspendLayout()
        '
        'tvwLayer
        '
        Me.tvwLayer.ContextMenuStrip = Me.contextMenuDummy
        Me.tvwLayer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwLayer.Location = New System.Drawing.Point(0, 21)
        Me.tvwLayer.Name = "tvwLayer"
        Me.tvwLayer.Size = New System.Drawing.Size(212, 185)
        Me.tvwLayer.TabIndex = 2
        '
        'cboLayerType
        '
        Me.cboLayerType.ContextMenuStrip = Me.contextMenuDummy
        Me.cboLayerType.Dock = System.Windows.Forms.DockStyle.Top
        Me.cboLayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLayerType.FormattingEnabled = True
        Me.cboLayerType.Items.AddRange(New Object() {"All Layer Type", "Feature Layers", "Raster Layers", "Data Layers"})
        Me.cboLayerType.Location = New System.Drawing.Point(0, 0)
        Me.cboLayerType.Name = "cboLayerType"
        Me.cboLayerType.Size = New System.Drawing.Size(212, 21)
        Me.cboLayerType.TabIndex = 3
        '
        'contextMenuDummy
        '
        Me.contextMenuDummy.Name = "contextMenuDummy"
        Me.contextMenuDummy.Size = New System.Drawing.Size(61, 4)
        '
        'TOCLayerFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tvwLayer)
        Me.Controls.Add(Me.cboLayerType)
        Me.Name = "TOCLayerFilter"
        Me.Size = New System.Drawing.Size(212, 206)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tvwLayer As System.Windows.Forms.TreeView
    Private WithEvents cboLayerType As System.Windows.Forms.ComboBox
    Friend WithEvents contextMenuDummy As System.Windows.Forms.ContextMenuStrip

End Class
