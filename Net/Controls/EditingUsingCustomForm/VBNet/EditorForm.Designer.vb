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
Partial Class EditorForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditorForm))
        Me.flowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel
        Me.txtInfo = New System.Windows.Forms.TextBox
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.label1 = New System.Windows.Forms.Label
        Me.cmdReshape = New System.Windows.Forms.Button
        Me.axCreateToolbar = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.axBlankToolBar = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.axUndoRedoToolbar = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.cmdCreate = New System.Windows.Forms.Button
        Me.axReshapeToolbar = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.cmdModify = New System.Windows.Forms.Button
        Me.axModifyToolbar = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.panel1 = New System.Windows.Forms.Panel
        CType(Me.axCreateToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axBlankToolBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axUndoRedoToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axReshapeToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axModifyToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'flowLayoutPanel2
        '
        Me.flowLayoutPanel2.Location = New System.Drawing.Point(124, 38)
        Me.flowLayoutPanel2.Name = "flowLayoutPanel2"
        Me.flowLayoutPanel2.Size = New System.Drawing.Size(95, 30)
        Me.flowLayoutPanel2.TabIndex = 14
        '
        'txtInfo
        '
        Me.txtInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInfo.Location = New System.Drawing.Point(124, 67)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.Size = New System.Drawing.Size(110, 59)
        Me.txtInfo.TabIndex = 13
        '
        'cmdEdit
        '
        Me.cmdEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.Location = New System.Drawing.Point(8, 120)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(110, 32)
        Me.cmdEdit.TabIndex = 8
        Me.cmdEdit.Text = "Edit"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'flowLayoutPanel1
        '
        Me.flowLayoutPanel1.Location = New System.Drawing.Point(124, 6)
        Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
        Me.flowLayoutPanel1.Size = New System.Drawing.Size(95, 33)
        Me.flowLayoutPanel1.TabIndex = 10
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.Red
        Me.label1.Location = New System.Drawing.Point(124, 132)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(51, 16)
        Me.label1.TabIndex = 15
        Me.label1.Text = "label1"
        '
        'cmdReshape
        '
        Me.cmdReshape.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReshape.Location = New System.Drawing.Point(8, 83)
        Me.cmdReshape.Name = "cmdReshape"
        Me.cmdReshape.Size = New System.Drawing.Size(110, 30)
        Me.cmdReshape.TabIndex = 2
        Me.cmdReshape.Text = "Reshape"
        Me.cmdReshape.UseVisualStyleBackColor = True
        '
        'axCreateToolbar
        '
        Me.axCreateToolbar.Location = New System.Drawing.Point(292, 21)
        Me.axCreateToolbar.Name = "axCreateToolbar"
        Me.axCreateToolbar.OcxState = CType(resources.GetObject("axCreateToolbar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axCreateToolbar.Size = New System.Drawing.Size(221, 28)
        Me.axCreateToolbar.TabIndex = 18
        '
        'axBlankToolBar
        '
        Me.axBlankToolBar.Location = New System.Drawing.Point(292, 58)
        Me.axBlankToolBar.Name = "axBlankToolBar"
        Me.axBlankToolBar.OcxState = CType(resources.GetObject("axBlankToolBar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axBlankToolBar.Size = New System.Drawing.Size(221, 28)
        Me.axBlankToolBar.TabIndex = 16
        '
        'axUndoRedoToolbar
        '
        Me.axUndoRedoToolbar.Location = New System.Drawing.Point(292, 130)
        Me.axUndoRedoToolbar.Name = "axUndoRedoToolbar"
        Me.axUndoRedoToolbar.OcxState = CType(resources.GetObject("axUndoRedoToolbar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axUndoRedoToolbar.Size = New System.Drawing.Size(221, 28)
        Me.axUndoRedoToolbar.TabIndex = 17
        '
        'cmdCreate
        '
        Me.cmdCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCreate.Location = New System.Drawing.Point(8, 6)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(110, 31)
        Me.cmdCreate.TabIndex = 4
        Me.cmdCreate.Text = "Create"
        Me.cmdCreate.UseVisualStyleBackColor = True
        '
        'axReshapeToolbar
        '
        Me.axReshapeToolbar.Location = New System.Drawing.Point(291, 186)
        Me.axReshapeToolbar.Name = "axReshapeToolbar"
        Me.axReshapeToolbar.OcxState = CType(resources.GetObject("axReshapeToolbar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axReshapeToolbar.Size = New System.Drawing.Size(222, 28)
        Me.axReshapeToolbar.TabIndex = 15
        '
        'cmdModify
        '
        Me.cmdModify.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.Location = New System.Drawing.Point(8, 44)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.Size = New System.Drawing.Size(110, 32)
        Me.cmdModify.TabIndex = 1
        Me.cmdModify.Text = "Modify"
        Me.cmdModify.UseVisualStyleBackColor = True
        '
        'axModifyToolbar
        '
        Me.axModifyToolbar.Location = New System.Drawing.Point(292, 96)
        Me.axModifyToolbar.Name = "axModifyToolbar"
        Me.axModifyToolbar.OcxState = CType(resources.GetObject("axModifyToolbar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axModifyToolbar.Size = New System.Drawing.Size(221, 28)
        Me.axModifyToolbar.TabIndex = 14
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel1.Controls.Add(Me.label1)
        Me.panel1.Controls.Add(Me.flowLayoutPanel2)
        Me.panel1.Controls.Add(Me.txtInfo)
        Me.panel1.Controls.Add(Me.cmdEdit)
        Me.panel1.Controls.Add(Me.flowLayoutPanel1)
        Me.panel1.Controls.Add(Me.cmdCreate)
        Me.panel1.Controls.Add(Me.cmdReshape)
        Me.panel1.Controls.Add(Me.cmdModify)
        Me.panel1.Location = New System.Drawing.Point(4, 9)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(228, 160)
        Me.panel1.TabIndex = 13
        '
        'EditorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 248)
        Me.Controls.Add(Me.axCreateToolbar)
        Me.Controls.Add(Me.axBlankToolBar)
        Me.Controls.Add(Me.axUndoRedoToolbar)
        Me.Controls.Add(Me.axReshapeToolbar)
        Me.Controls.Add(Me.axModifyToolbar)
        Me.Controls.Add(Me.panel1)
        Me.Name = "EditorForm"
        Me.Text = "EditorForm"
        CType(Me.axCreateToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axBlankToolBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axUndoRedoToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axReshapeToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axModifyToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents flowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents txtInfo As System.Windows.Forms.TextBox
    Private WithEvents cmdEdit As System.Windows.Forms.Button
    Private WithEvents flowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents cmdReshape As System.Windows.Forms.Button
    Private WithEvents axCreateToolbar As ESRI.ArcGIS.Controls.AxToolbarControl
    Private WithEvents axBlankToolBar As ESRI.ArcGIS.Controls.AxToolbarControl
    Private WithEvents axUndoRedoToolbar As ESRI.ArcGIS.Controls.AxToolbarControl
    Private WithEvents cmdCreate As System.Windows.Forms.Button
    Private WithEvents axReshapeToolbar As ESRI.ArcGIS.Controls.AxToolbarControl
    Private WithEvents cmdModify As System.Windows.Forms.Button
    Private WithEvents axModifyToolbar As ESRI.ArcGIS.Controls.AxToolbarControl
    Private WithEvents panel1 As System.Windows.Forms.Panel
End Class
