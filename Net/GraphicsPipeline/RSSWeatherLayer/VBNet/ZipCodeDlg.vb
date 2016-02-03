Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

	''' <summary>
	''' Gets the input zipCode from the user
	''' </summary>
	Public Class ZipCodeDlg : Inherits System.Windows.Forms.Form
		Private txtZipCode As System.Windows.Forms.TextBox
		Private lblZipCode As System.Windows.Forms.Label
		Private chkZoomTo As System.Windows.Forms.CheckBox
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private WithEvents btnOK As System.Windows.Forms.Button
		Private WithEvents btnCancel As System.Windows.Forms.Button
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If Not components Is Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.txtZipCode = New System.Windows.Forms.TextBox()
			Me.lblZipCode = New System.Windows.Forms.Label()
			Me.chkZoomTo = New System.Windows.Forms.CheckBox()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.btnOK = New System.Windows.Forms.Button()
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.groupBox1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' txtZipCode
			' 
			Me.txtZipCode.Location = New System.Drawing.Point(64, 24)
			Me.txtZipCode.Name = "txtZipCode"
			Me.txtZipCode.TabIndex = 0
			Me.txtZipCode.Text = ""
			' 
			' lblZipCode
			' 
			Me.lblZipCode.Location = New System.Drawing.Point(8, 24)
			Me.lblZipCode.Name = "lblZipCode"
			Me.lblZipCode.Size = New System.Drawing.Size(48, 16)
			Me.lblZipCode.TabIndex = 1
			Me.lblZipCode.Text = "ZipCode:"
			' 
			' chkZoomTo
			' 
			Me.chkZoomTo.Location = New System.Drawing.Point(8, 56)
			Me.chkZoomTo.Name = "chkZoomTo"
			Me.chkZoomTo.TabIndex = 2
			Me.chkZoomTo.Text = "Zoom to item"
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.chkZoomTo)
			Me.groupBox1.Controls.Add(Me.txtZipCode)
			Me.groupBox1.Controls.Add(Me.lblZipCode)
			Me.groupBox1.Location = New System.Drawing.Point(8, 8)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(176, 88)
			Me.groupBox1.TabIndex = 3
			Me.groupBox1.TabStop = False
			' 
			' btnOK
			' 
			Me.btnOK.Location = New System.Drawing.Point(8, 120)
			Me.btnOK.Name = "btnOK"
			Me.btnOK.TabIndex = 4
			Me.btnOK.Text = "OK"
'			Me.btnOK.Click += New System.EventHandler(Me.btnOK_Click);
			' 
			' btnCancel
			' 
			Me.btnCancel.Location = New System.Drawing.Point(112, 120)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.TabIndex = 5
			Me.btnCancel.Text = "Cancel"
'			Me.btnCancel.Click += New System.EventHandler(Me.btnCancel_Click);
			' 
			' ZipCodeDlg
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(194, 152)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnOK)
			Me.Controls.Add(Me.groupBox1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
			Me.Name = "ZipCodeDlg"
			Me.ShowInTaskbar = False
			Me.Text = "Add by zip code dialog"
			Me.TopMost = True
			Me.groupBox1.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub
		#End Region


		''' <summary>
		''' The Ok button click
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
	Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
	  'set the dialog result
			Me.DialogResult = System.Windows.Forms.DialogResult.OK

	  'close the dialog
			Me.Close()
	End Sub

	''' <summary>
	''' Cancel button click
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
		Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
	  'set the dialog result
			Me.DialogResult = DialogResult.Cancel

	  'close the dialog
			Me.Close()
		End Sub

	''' <summary>
	''' Returns the zipCode entered by the user
	''' </summary>
		Public ReadOnly Property ZipCode() As Long
			Get
		'make sure that the zipcode is a number
				If IsNumber(txtZipCode.Text) Then
					Return Long.Parse(txtZipCode.Text)
				Else
					Return 0
				End If
			End Get
		End Property

	''' <summary>
    ''' Returns whether the user checked the option to zoom to the given zipCode weather item
	''' </summary>
		Public ReadOnly Property ZoomToItem() As Boolean
			Get
				Return chkZoomTo.Checked
			End Get
		End Property

		''' <summary>
    ''' test whether a string is a number
		''' </summary>
		''' <param name="input"></param>
		''' <returns></returns>
	Private Function IsNumber(ByVal input As String) As Boolean
			For Each c As Char In input
				If (Not Char.IsNumber(c)) Then
				Return False
				End If
			Next c
			Return True
	End Function
	End Class

