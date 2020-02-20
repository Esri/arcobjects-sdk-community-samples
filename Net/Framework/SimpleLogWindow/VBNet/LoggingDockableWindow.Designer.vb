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
Partial Class LoggingDockableWindow
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
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lblMsg = New System.Windows.Forms.Label
        Me.txtInput = New System.Windows.Forms.TextBox
        Me.lstPendingMessage = New System.Windows.Forms.ListBox
        Me.ctxMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.clearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.grpBoxCtxMenu = New System.Windows.Forms.GroupBox
        Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.radDotNetCtx = New System.Windows.Forms.RadioButton
        Me.radFramework = New System.Windows.Forms.RadioButton
        Me.radDynamic = New System.Windows.Forms.RadioButton
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.ctxMenuStrip.SuspendLayout()
        Me.grpBoxCtxMenu.SuspendLayout()
        Me.flowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'splitContainer1
        '
        Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer1.Name = "splitContainer1"
        Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.lblMsg)
        Me.splitContainer1.Panel1.Controls.Add(Me.txtInput)
        Me.splitContainer1.Panel1.Controls.Add(Me.lstPendingMessage)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.grpBoxCtxMenu)
        Me.splitContainer1.Size = New System.Drawing.Size(340, 204)
        Me.splitContainer1.SplitterDistance = 113
        Me.splitContainer1.TabIndex = 4
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.Location = New System.Drawing.Point(0, 3)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(58, 13)
        Me.lblMsg.TabIndex = 2
        Me.lblMsg.Text = "Input Text:"
        '
        'txtInput
        '
        Me.txtInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInput.Location = New System.Drawing.Point(81, 0)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(256, 20)
        Me.txtInput.TabIndex = 1
        '
        'lstPendingMessage
        '
        Me.lstPendingMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstPendingMessage.ContextMenuStrip = Me.ctxMenuStrip
        Me.lstPendingMessage.HorizontalScrollbar = True
        Me.lstPendingMessage.IntegralHeight = False
        Me.lstPendingMessage.Location = New System.Drawing.Point(0, 25)
        Me.lstPendingMessage.Name = "lstPendingMessage"
        Me.lstPendingMessage.Size = New System.Drawing.Size(340, 88)
        Me.lstPendingMessage.TabIndex = 0
        '
        'ctxMenuStrip
        '
        Me.ctxMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripSeparator1, Me.clearToolStripMenuItem})
        Me.ctxMenuStrip.Name = "ctxMenuStrip"
        Me.ctxMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ctxMenuStrip.ShowItemToolTips = False
        Me.ctxMenuStrip.Size = New System.Drawing.Size(153, 54)
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(71, 6)
        '
        'clearToolStripMenuItem
        '
        Me.clearToolStripMenuItem.Name = "clearToolStripMenuItem"
        Me.clearToolStripMenuItem.Size = New System.Drawing.Size(74, 22)
        Me.clearToolStripMenuItem.Text = "Clear"
        '
        'grpBoxCtxMenu
        '
        Me.grpBoxCtxMenu.Controls.Add(Me.flowLayoutPanel1)
        Me.grpBoxCtxMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpBoxCtxMenu.Location = New System.Drawing.Point(0, 0)
        Me.grpBoxCtxMenu.Name = "grpBoxCtxMenu"
        Me.grpBoxCtxMenu.Size = New System.Drawing.Size(340, 87)
        Me.grpBoxCtxMenu.TabIndex = 2
        Me.grpBoxCtxMenu.TabStop = False
        Me.grpBoxCtxMenu.Text = "Context Menu Option"
        '
        'flowLayoutPanel1
        '
        Me.flowLayoutPanel1.Controls.Add(Me.radDotNetCtx)
        Me.flowLayoutPanel1.Controls.Add(Me.radFramework)
        Me.flowLayoutPanel1.Controls.Add(Me.radDynamic)
        Me.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel1.Location = New System.Drawing.Point(3, 16)
        Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
        Me.flowLayoutPanel1.Size = New System.Drawing.Size(334, 68)
        Me.flowLayoutPanel1.TabIndex = 3
        '
        'radDotNetCtx
        '
        Me.radDotNetCtx.AutoSize = True
        Me.radDotNetCtx.Checked = True
        Me.radDotNetCtx.Location = New System.Drawing.Point(3, 3)
        Me.radDotNetCtx.Name = "radDotNetCtx"
        Me.radDotNetCtx.Size = New System.Drawing.Size(149, 17)
        Me.radDotNetCtx.TabIndex = 0
        Me.radDotNetCtx.TabStop = True
        Me.radDotNetCtx.Text = "Pure .Net (Windows Form)"
        Me.toolTip1.SetToolTip(Me.radDotNetCtx, "Pure .Net Windows Form context menu (ContextMenuStrip Class)")
        Me.radDotNetCtx.UseVisualStyleBackColor = True
        '
        'radFramework
        '
        Me.radFramework.AutoSize = True
        Me.radFramework.Location = New System.Drawing.Point(158, 3)
        Me.radFramework.Name = "radFramework"
        Me.radFramework.Size = New System.Drawing.Size(140, 17)
        Me.radFramework.TabIndex = 1
        Me.radFramework.TabStop = True
        Me.radFramework.Text = "Pre-defined (ArcObjects)"
        Me.toolTip1.SetToolTip(Me.radFramework, "Use a predefined context menu registered in ArcGIS framework")
        Me.radFramework.UseVisualStyleBackColor = True
        '
        'radDynamic
        '
        Me.radDynamic.AutoSize = True
        Me.radDynamic.Location = New System.Drawing.Point(3, 26)
        Me.radDynamic.Name = "radDynamic"
        Me.radDynamic.Size = New System.Drawing.Size(127, 17)
        Me.radDynamic.TabIndex = 2
        Me.radDynamic.TabStop = True
        Me.radDynamic.Text = "Dynamic (ArcObjects)"
        Me.toolTip1.SetToolTip(Me.radDynamic, "Use a temporary context menu created at runtime")
        Me.radDynamic.UseVisualStyleBackColor = True
        '
        'LoggingDockableWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.splitContainer1)
        Me.Name = "LoggingDockableWindow"
        Me.Size = New System.Drawing.Size(340, 204)
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel1.PerformLayout()
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.ResumeLayout(False)
        Me.ctxMenuStrip.ResumeLayout(False)
        Me.grpBoxCtxMenu.ResumeLayout(False)
        Me.flowLayoutPanel1.ResumeLayout(False)
        Me.flowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents splitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents lstPendingMessage As System.Windows.Forms.ListBox
    Private WithEvents grpBoxCtxMenu As System.Windows.Forms.GroupBox
    Private WithEvents flowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents radDotNetCtx As System.Windows.Forms.RadioButton
    Private WithEvents radFramework As System.Windows.Forms.RadioButton
    Private WithEvents radDynamic As System.Windows.Forms.RadioButton
    Private WithEvents ctxMenuStrip As System.Windows.Forms.ContextMenuStrip
    Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents clearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents txtInput As System.Windows.Forms.TextBox

End Class
