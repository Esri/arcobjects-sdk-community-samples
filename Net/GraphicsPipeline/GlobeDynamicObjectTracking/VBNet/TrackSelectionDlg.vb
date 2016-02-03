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