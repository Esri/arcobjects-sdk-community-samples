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
Partial Class DigitDockableWindow
    Inherits System.Windows.Forms.UserControl

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
		Me.components = New System.ComponentModel.Container()
		Me.btnOKPanel1 = New System.Windows.Forms.Button()
		Me.lblNodeLabel = New System.Windows.Forms.Label()
		Me.Splitter = New System.Windows.Forms.SplitContainer()
		Me.cboNodeType = New System.Windows.Forms.ComboBox()
		Me.btnOKPanel2 = New System.Windows.Forms.Button()
		Me.cboLinkType = New System.Windows.Forms.ComboBox()
		Me.lblLinkLabel = New System.Windows.Forms.Label()
		Me.lblMode = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.Splitter.Panel1.SuspendLayout()
		Me.Splitter.Panel2.SuspendLayout()
		Me.Splitter.SuspendLayout()
		CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btnOKPanel1
		'
		Me.btnOKPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ErrorProvider1.SetError(Me.btnOKPanel1, "Invalid data entry")
		Me.btnOKPanel1.Location = New System.Drawing.Point(214, 200)
		Me.btnOKPanel1.Name = "btnOKPanel1"
		Me.btnOKPanel1.Size = New System.Drawing.Size(44, 24)
		Me.btnOKPanel1.TabIndex = 7
		Me.btnOKPanel1.Text = "OK"
		Me.btnOKPanel1.UseVisualStyleBackColor = True
		Me.btnOKPanel1.Visible = False
		'
		'lblNodeLabel
		'
		Me.lblNodeLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
								Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblNodeLabel.AutoSize = True
		Me.lblNodeLabel.Location = New System.Drawing.Point(3, 8)
		Me.lblNodeLabel.Name = "lblNodeLabel"
		Me.lblNodeLabel.Size = New System.Drawing.Size(60, 13)
		Me.lblNodeLabel.TabIndex = 5
		Me.lblNodeLabel.Text = "Node Type"
		'
		'Splitter
		'
		Me.Splitter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
								Or System.Windows.Forms.AnchorStyles.Left) _
								Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Splitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.Splitter.Location = New System.Drawing.Point(27, 28)
		Me.Splitter.Name = "Splitter"
		Me.Splitter.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'Splitter.Panel1
		'
		Me.Splitter.Panel1.AutoScroll = True
		Me.Splitter.Panel1.Controls.Add(Me.btnOKPanel1)
		Me.Splitter.Panel1.Controls.Add(Me.cboNodeType)
		Me.Splitter.Panel1.Controls.Add(Me.lblNodeLabel)
		'
		'Splitter.Panel2
		'
		Me.Splitter.Panel2.AutoScroll = True
		Me.Splitter.Panel2.Controls.Add(Me.btnOKPanel2)
		Me.Splitter.Panel2.Controls.Add(Me.cboLinkType)
		Me.Splitter.Panel2.Controls.Add(Me.lblLinkLabel)
		Me.Splitter.Size = New System.Drawing.Size(278, 518)
		Me.Splitter.SplitterDistance = 239
		Me.Splitter.TabIndex = 17
		'
		'cboNodeType
		'
		Me.cboNodeType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
								Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboNodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboNodeType.FormattingEnabled = True
		Me.cboNodeType.Location = New System.Drawing.Point(63, 5)
		Me.cboNodeType.Name = "cboNodeType"
		Me.cboNodeType.Size = New System.Drawing.Size(209, 21)
		Me.cboNodeType.TabIndex = 6
		'
		'btnOKPanel2
		'
		Me.btnOKPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOKPanel2.Location = New System.Drawing.Point(220, 238)
		Me.btnOKPanel2.Name = "btnOKPanel2"
		Me.btnOKPanel2.Size = New System.Drawing.Size(44, 24)
		Me.btnOKPanel2.TabIndex = 9
		Me.btnOKPanel2.Text = "OK"
		Me.btnOKPanel2.UseVisualStyleBackColor = True
		Me.btnOKPanel2.Visible = False
		'
		'cboLinkType
		'
		Me.cboLinkType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
								Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboLinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboLinkType.Enabled = False
		Me.cboLinkType.FormattingEnabled = True
		Me.cboLinkType.Location = New System.Drawing.Point(63, 3)
		Me.cboLinkType.Name = "cboLinkType"
		Me.cboLinkType.Size = New System.Drawing.Size(209, 21)
		Me.cboLinkType.TabIndex = 8
		'
		'lblLinkLabel
		'
		Me.lblLinkLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
								Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblLinkLabel.AutoSize = True
		Me.lblLinkLabel.Enabled = False
		Me.lblLinkLabel.Location = New System.Drawing.Point(3, 6)
		Me.lblLinkLabel.Name = "lblLinkLabel"
		Me.lblLinkLabel.Size = New System.Drawing.Size(54, 13)
		Me.lblLinkLabel.TabIndex = 7
		Me.lblLinkLabel.Text = "Link Type"
		'
		'lblMode
		'
		Me.lblMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblMode.AutoSize = True
		Me.lblMode.Location = New System.Drawing.Point(104, 556)
		Me.lblMode.Name = "lblMode"
		Me.lblMode.Size = New System.Drawing.Size(33, 13)
		Me.lblMode.TabIndex = 19
		Me.lblMode.Text = "None"
		'
		'Label1
		'
		Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(24, 556)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(74, 13)
		Me.Label1.TabIndex = 18
		Me.Label1.Text = "Current Mode:"
		'
		'ErrorProvider1
		'
		Me.ErrorProvider1.ContainerControl = Me
		Me.ErrorProvider1.RightToLeft = True
		'
		'DigitDockableWindow
		'
		Me.Controls.Add(Me.Splitter)
		Me.Controls.Add(Me.lblMode)
		Me.Controls.Add(Me.Label1)
		Me.MaximumSize = New System.Drawing.Size(1000, 1000)
		Me.MinimumSize = New System.Drawing.Size(300, 597)
		Me.Name = "DigitDockableWindow"
		Me.Size = New System.Drawing.Size(331, 595)
		Me.Tag = "ESRI_AddInDockableWindow_Digit"
		Me.Splitter.Panel1.ResumeLayout(False)
		Me.Splitter.Panel1.PerformLayout()
		Me.Splitter.Panel2.ResumeLayout(False)
		Me.Splitter.Panel2.PerformLayout()
		Me.Splitter.ResumeLayout(False)
		CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
    Friend WithEvents btnOKPanel1 As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Splitter As System.Windows.Forms.SplitContainer
    Friend WithEvents cboNodeType As System.Windows.Forms.ComboBox
    Friend WithEvents lblNodeLabel As System.Windows.Forms.Label
    Friend WithEvents btnOKPanel2 As System.Windows.Forms.Button
    Friend WithEvents cboLinkType As System.Windows.Forms.ComboBox
    Friend WithEvents lblLinkLabel As System.Windows.Forms.Label
    Friend WithEvents lblMode As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
