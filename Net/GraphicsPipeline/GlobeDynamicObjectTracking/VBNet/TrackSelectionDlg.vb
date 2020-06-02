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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

  ''' <summary>
  ''' This dialog allow user to select the required type of tracking
  ''' </summary>
  Public Partial Class TrackSelectionDlg : Inherits Form
	Public Sub New()
	  InitializeComponent()
	End Sub

	''' <summary>
	''' OK button click event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
	  Me.DialogResult = System.Windows.Forms.DialogResult.OK

	End Sub

	''' <summary>
	''' Cancel button click event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
	  Me.DialogResult = DialogResult.Cancel
	End Sub

	''' <summary>
    ''' Returns the selected mode of tracking (above the element or behind the element)
	''' </summary>
	Public ReadOnly Property UseOrthoTrackingMode() As Boolean
	  Get
		  Return chkOrthogonal.Checked
	  End Get
	End Property
  End Class