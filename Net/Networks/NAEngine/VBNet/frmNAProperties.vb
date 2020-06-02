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
Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Controls

' This window shows the property pages for the ArcGIS Network Analyst extension environment.
Namespace NAEngine
	Public Class frmNAProperties : Inherits System.Windows.Forms.Form
		Private WithEvents btnCancel As System.Windows.Forms.Button
		Private WithEvents btnOK As System.Windows.Forms.Button
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing
		Private chkZoomToResultAfterSolve As System.Windows.Forms.CheckBox
		Private grpMessages As System.Windows.Forms.GroupBox
		Private rdoAllMessages As System.Windows.Forms.RadioButton
		Private rdoErrorsAndWarnings As System.Windows.Forms.RadioButton
		Private rdoNoMessages As System.Windows.Forms.RadioButton
		Private rdoErrors As System.Windows.Forms.RadioButton

		Private m_okClicked As Boolean

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
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.btnOK = New System.Windows.Forms.Button()
			Me.chkZoomToResultAfterSolve = New System.Windows.Forms.CheckBox()
			Me.grpMessages = New System.Windows.Forms.GroupBox()
			Me.rdoNoMessages = New System.Windows.Forms.RadioButton()
			Me.rdoErrors = New System.Windows.Forms.RadioButton()
			Me.rdoErrorsAndWarnings = New System.Windows.Forms.RadioButton()
			Me.rdoAllMessages = New System.Windows.Forms.RadioButton()
			Me.grpMessages.SuspendLayout()
			Me.SuspendLayout()
			' 
			' btnCancel
			' 
			Me.btnCancel.Location = New System.Drawing.Point(240, 216)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(112, 32)
			Me.btnCancel.TabIndex = 4
			Me.btnCancel.Text = "&Cancel"
'			Me.btnCancel.Click += New System.EventHandler(Me.btnCancel_Click);
			' 
			' btnOK
			' 
			Me.btnOK.Location = New System.Drawing.Point(112, 216)
			Me.btnOK.Name = "btnOK"
			Me.btnOK.Size = New System.Drawing.Size(112, 32)
			Me.btnOK.TabIndex = 3
			Me.btnOK.Text = "&OK"
'			Me.btnOK.Click += New System.EventHandler(Me.btnOK_Click);
			' 
			' chkZoomToResultAfterSolve
			' 
			Me.chkZoomToResultAfterSolve.Location = New System.Drawing.Point(16, 24)
			Me.chkZoomToResultAfterSolve.Name = "chkZoomToResultAfterSolve"
			Me.chkZoomToResultAfterSolve.Size = New System.Drawing.Size(200, 24)
			Me.chkZoomToResultAfterSolve.TabIndex = 5
			Me.chkZoomToResultAfterSolve.Text = "Zoom To Result After Solve"
			' 
			' grpMessages
			' 
			Me.grpMessages.Controls.Add(Me.rdoNoMessages)
			Me.grpMessages.Controls.Add(Me.rdoErrors)
			Me.grpMessages.Controls.Add(Me.rdoErrorsAndWarnings)
			Me.grpMessages.Controls.Add(Me.rdoAllMessages)
			Me.grpMessages.Location = New System.Drawing.Point(16, 72)
			Me.grpMessages.Name = "grpMessages"
			Me.grpMessages.Size = New System.Drawing.Size(336, 120)
			Me.grpMessages.TabIndex = 6
			Me.grpMessages.TabStop = False
			Me.grpMessages.Text = "Messages"
			' 
			' rdoNoMessages
			' 
			Me.rdoNoMessages.Location = New System.Drawing.Point(16, 88)
			Me.rdoNoMessages.Name = "rdoNoMessages"
			Me.rdoNoMessages.Size = New System.Drawing.Size(304, 24)
			Me.rdoNoMessages.TabIndex = 3
			Me.rdoNoMessages.Text = "No Messages"
			' 
			' rdoErrors
			' 
			Me.rdoErrors.Location = New System.Drawing.Point(16, 64)
			Me.rdoErrors.Name = "rdoErrors"
			Me.rdoErrors.Size = New System.Drawing.Size(304, 24)
			Me.rdoErrors.TabIndex = 2
			Me.rdoErrors.Text = "Errors"
			' 
			' rdoErrorsAndWarnings
			' 
			Me.rdoErrorsAndWarnings.Location = New System.Drawing.Point(16, 40)
			Me.rdoErrorsAndWarnings.Name = "rdoErrorsAndWarnings"
			Me.rdoErrorsAndWarnings.Size = New System.Drawing.Size(304, 24)
			Me.rdoErrorsAndWarnings.TabIndex = 1
			Me.rdoErrorsAndWarnings.Text = "Errors and Warnings"
			' 
			' rdoAllMessages
			' 
			Me.rdoAllMessages.Location = New System.Drawing.Point(16, 16)
			Me.rdoAllMessages.Name = "rdoAllMessages"
			Me.rdoAllMessages.Size = New System.Drawing.Size(304, 24)
			Me.rdoAllMessages.TabIndex = 0
			Me.rdoAllMessages.Text = "All Messages"
			' 
			' frmNAProperties
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(370, 262)
			Me.Controls.Add(Me.grpMessages)
			Me.Controls.Add(Me.chkZoomToResultAfterSolve)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnOK)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "frmNAProperties"
			Me.ShowInTaskbar = False
			Me.Text = "Network Analyst Properties"
			Me.grpMessages.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub
		#End Region

		Public Sub ShowModal()
			m_okClicked = False

            Dim naEnv As IEngineNetworkAnalystEnvironment = CommonFunctions.GetTheEngineNetworkAnalystEnvironment()
			If naEnv Is Nothing Then
				System.Windows.Forms.MessageBox.Show("Error: EngineNetworkAnalystEnvironment is not properly configured")
				Return
			End If

			' Zoom to result after solve or not
			chkZoomToResultAfterSolve.Checked = naEnv.ZoomToResultAfterSolve

			' Set the radio button based on the value in ShowAnalysisMessagesAfterSolve.
			' This is a bit property where multiple values are possible.  
			' Simplify it for the user so assume message types build on each other.  
			'  For example, if you want info, you probably want warnings and errors too
			'   No Messages = 0
			'   Errors = esriEngineNAMessageTypeError
			'   Errors and warnings = esriEngineNAMessageTypeError & esriEngineNAMessageTypeWarning
			'   All = esriEngineNAMessageTypeError & esriEngineNAMessageTypeWarning & esriEngineNAMessageTypeInformative
			If CType(naEnv.ShowAnalysisMessagesAfterSolve And CInt(esriEngineNAMessageType.esriEngineNAMessageTypeInformative), esriEngineNAMessageType) = esriEngineNAMessageType.esriEngineNAMessageTypeInformative Then
				rdoAllMessages.Checked = True
			ElseIf CType(naEnv.ShowAnalysisMessagesAfterSolve And CInt(esriEngineNAMessageType.esriEngineNAMessageTypeWarning), esriEngineNAMessageType) = esriEngineNAMessageType.esriEngineNAMessageTypeWarning Then
				rdoErrorsAndWarnings.Checked = True
			ElseIf CType(naEnv.ShowAnalysisMessagesAfterSolve And CInt(esriEngineNAMessageType.esriEngineNAMessageTypeError), esriEngineNAMessageType) = esriEngineNAMessageType.esriEngineNAMessageTypeError Then
				rdoErrors.Checked = True
			Else
				rdoNoMessages.Checked = True
			End If

			Me.ShowDialog()
			If m_okClicked Then
				' Set ZoomToResultAfterSolve
				naEnv.ZoomToResultAfterSolve = chkZoomToResultAfterSolve.Checked

				' Set ShowAnalysisMessagesAfterSolve
				' Use simplified version so higher severity errors also show lower severity "info" and "warnings"
				If rdoAllMessages.Checked Then
					naEnv.ShowAnalysisMessagesAfterSolve = CInt(esriEngineNAMessageType.esriEngineNAMessageTypeInformative) + CInt(esriEngineNAMessageType.esriEngineNAMessageTypeWarning) + CInt(esriEngineNAMessageType.esriEngineNAMessageTypeError)
				ElseIf rdoErrorsAndWarnings.Checked Then
					naEnv.ShowAnalysisMessagesAfterSolve = CInt(esriEngineNAMessageType.esriEngineNAMessageTypeWarning) + CInt(esriEngineNAMessageType.esriEngineNAMessageTypeError)
				ElseIf rdoErrors.Checked Then
					naEnv.ShowAnalysisMessagesAfterSolve = CInt(esriEngineNAMessageType.esriEngineNAMessageTypeError)
				Else
					naEnv.ShowAnalysisMessagesAfterSolve = 0
				End If
			End If
		End Sub

		Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
			m_okClicked = True
			Me.Close()
		End Sub

		Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
			m_okClicked = False
			Me.Close()
		End Sub
	End Class
End Namespace
