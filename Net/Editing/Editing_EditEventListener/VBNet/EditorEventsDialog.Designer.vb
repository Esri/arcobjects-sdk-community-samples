<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditorEventsDialog
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
        Me.tabControl1 = New System.Windows.Forms.TabControl
        Me.tabPage1 = New System.Windows.Forms.TabPage
        Me.lblEditorEvents = New System.Windows.Forms.Label
        Me.lstEditorEvents = New System.Windows.Forms.ListBox
        Me.tabPage2 = New System.Windows.Forms.TabPage
        Me.chkSelectEvent = New System.Windows.Forms.CheckedListBox
        Me.tabControl1.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.tabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl1.Location = New System.Drawing.Point(0, 0)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(340, 158)
        Me.tabControl1.TabIndex = 3
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.lblEditorEvents)
        Me.tabPage1.Controls.Add(Me.lstEditorEvents)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage1.Size = New System.Drawing.Size(332, 132)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "Listen to Events"
        Me.tabPage1.UseVisualStyleBackColor = True
        '
        'lblEditorEvents
        '
        Me.lblEditorEvents.AutoSize = True
        Me.lblEditorEvents.Location = New System.Drawing.Point(3, -21)
        Me.lblEditorEvents.Name = "lblEditorEvents"
        Me.lblEditorEvents.Size = New System.Drawing.Size(162, 13)
        Me.lblEditorEvents.TabIndex = 7
        Me.lblEditorEvents.Text = "Editor Events (Most Recent First)"
        '
        'lstEditorEvents
        '
        Me.lstEditorEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstEditorEvents.FormattingEnabled = True
        Me.lstEditorEvents.Location = New System.Drawing.Point(3, 3)
        Me.lstEditorEvents.Name = "lstEditorEvents"
        Me.lstEditorEvents.ScrollAlwaysVisible = True
        Me.lstEditorEvents.Size = New System.Drawing.Size(326, 121)
        Me.lstEditorEvents.TabIndex = 4
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.chkSelectEvent)
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage2.Size = New System.Drawing.Size(332, 132)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "Select Events"
        Me.tabPage2.UseVisualStyleBackColor = True
        '
        'chkSelectEvent
        '
        Me.chkSelectEvent.CheckOnClick = True
        Me.chkSelectEvent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkSelectEvent.FormattingEnabled = True
        Me.chkSelectEvent.Location = New System.Drawing.Point(3, 3)
        Me.chkSelectEvent.Name = "chkSelectEvent"
        Me.chkSelectEvent.Size = New System.Drawing.Size(326, 124)
        Me.chkSelectEvent.TabIndex = 0
        '
        'EditorEventsDialog
        '
        Me.Controls.Add(Me.tabControl1)
        Me.Name = "EditorEventsDialog"
        Me.Size = New System.Drawing.Size(340, 158)
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        Me.tabPage1.PerformLayout()
        Me.tabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents tabControl1 As System.Windows.Forms.TabControl
    Public WithEvents tabPage1 As System.Windows.Forms.TabPage
    Private WithEvents lblEditorEvents As System.Windows.Forms.Label
    Public WithEvents lstEditorEvents As System.Windows.Forms.ListBox
    Private WithEvents tabPage2 As System.Windows.Forms.TabPage
    Private WithEvents chkSelectEvent As System.Windows.Forms.CheckedListBox

End Class
