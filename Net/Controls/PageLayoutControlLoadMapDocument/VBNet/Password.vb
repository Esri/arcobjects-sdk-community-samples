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
Public Class frmPassword
  Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'Form overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtPassword As System.Windows.Forms.TextBox
  Friend WithEvents cmdOK As System.Windows.Forms.Button
  Friend WithEvents cmdCancel As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.txtPassword = New System.Windows.Forms.TextBox
    Me.cmdOK = New System.Windows.Forms.Button
    Me.cmdCancel = New System.Windows.Forms.Button
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(16, 24)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(232, 40)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Please enter the password for the published map in the text box below and press O" & _
    "K."
    '
    'Label2
    '
    Me.Label2.Location = New System.Drawing.Point(8, 80)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(72, 16)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Password:"
    '
    'txtPassword
    '
    Me.txtPassword.Location = New System.Drawing.Point(80, 80)
    Me.txtPassword.Name = "txtPassword"
    Me.txtPassword.Size = New System.Drawing.Size(160, 20)
    Me.txtPassword.TabIndex = 2
    Me.txtPassword.Text = ""
    '
    'cmdOK
    '
    Me.cmdOK.Location = New System.Drawing.Point(24, 120)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(88, 32)
    Me.cmdOK.TabIndex = 3
    Me.cmdOK.Text = "OK"
    '
    'cmdCancel
    '
    Me.cmdCancel.Location = New System.Drawing.Point(136, 120)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(88, 32)
    Me.cmdCancel.TabIndex = 4
    Me.cmdCancel.Text = "Cancel"
    '
    'frmPassword
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(264, 174)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.txtPassword)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Name = "frmPassword"
    Me.Text = "frmPassword"
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private m_password As String
  Private m_check As Integer


  Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    txtPassword.Text = ""
    m_check = 0
    Me.Close()
  End Sub


  Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    m_password = txtPassword.Text
    txtPassword.Text = ""
    m_check = 1
    Me.Close()
  End Sub


  Public ReadOnly Property Password() As String
    Get
      Return m_password
    End Get
  End Property

  Public ReadOnly Property Check() As Integer
    Get
      Return m_check
    End Get
  End Property

  Private Sub frmPassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'Set placeholder character for password
    txtPassword.PasswordChar = "*"
    cmdOK.Enabled = False
  End Sub

  Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
    'Set Enabled property
    If txtPassword.Text = "" Then
      cmdOK.Enabled = False
    Else
      cmdOK.Enabled = True
    End If
  End Sub
End Class
