Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.TrackingAnalyst

Namespace TAUpdateControlSample
	Public Partial Class TAUpdateControlForm : Inherits Form
		Private m_taUpdateCtrl As ITAUpdateControl = Nothing

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub TAUpdateControlForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			'Get the ITAUpdateControl interface from the TA Display Manager
			'Store it as a member variable for future use
			'The DisplayManager is a singleton so the reference should never change
            Dim taEnv As ITrackingEnvironment3 = New TrackingEnvironmentClass()
			m_taUpdateCtrl = CType(taEnv.DisplayManager, ITAUpdateControl)
		End Sub

		'Populate the controls with the current settings
		Public Sub PopulateDialog()
			If Not m_taUpdateCtrl Is Nothing Then
				checkManualUpdate.Checked = m_taUpdateCtrl.ManualUpdate
				txtUpdateRate.Text = Convert.ToString(m_taUpdateCtrl.MaxUpdateRate)
				cbUpdateMethod.SelectedIndex = CInt(m_taUpdateCtrl.UpdateMethod)
				txtUpdateValue.Text = Convert.ToString(m_taUpdateCtrl.UpdateValue)

				checkAutoRefresh.Checked = m_taUpdateCtrl.AutoRefresh
				txtRefreshRate.Text = Convert.ToString(m_taUpdateCtrl.RefreshRate)

				cbRefreshType.SelectedIndex = 0
			End If
		End Sub

		'Cause a map refresh
		'The type of refresh used is determined by the refresh method combo box
		Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
			Dim eScreenUpdateType As enumScreenUpdateType = CType(cbRefreshType.SelectedIndex, enumScreenUpdateType)
			m_taUpdateCtrl.RefreshDisplay(eScreenUpdateType)
		End Sub

		'Output the display statistics to the Statistics text box
		'The Statistics are returned as an XML string
        'The XML string should be relatively self-explanatory
		Private Sub btnStats_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStats.Click
			txtStatistics.Clear()
			txtStatistics.Text = m_taUpdateCtrl.Statistics
		End Sub

		'Apply any setting changes to Display Manager 
		Private Sub btnApply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
			If Not m_taUpdateCtrl Is Nothing Then
				m_taUpdateCtrl.ManualUpdate = checkManualUpdate.Checked
				m_taUpdateCtrl.MaxUpdateRate = Convert.ToDouble(txtUpdateRate.Text)
				m_taUpdateCtrl.UpdateMethod = CType(cbUpdateMethod.SelectedIndex, enumScreenUpdateThresholdType)
				m_taUpdateCtrl.UpdateValue = Convert.ToInt32(txtUpdateValue.Text)

				m_taUpdateCtrl.AutoRefresh = checkAutoRefresh.Checked
				m_taUpdateCtrl.RefreshRate = Convert.ToDouble(txtRefreshRate.Text)
			End If
		End Sub

		'Just hide the dialog instead of disposing it
		Private Sub TAUpdateControlForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			Me.Hide()
			e.Cancel = True
		End Sub

		'Disable the options that aren't used when causing the display to update manually
		Private Sub checkManualUpdate_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkManualUpdate.CheckedChanged
			txtUpdateRate.Enabled = Not checkManualUpdate.Checked
			cbUpdateMethod.Enabled = Not checkManualUpdate.Checked
			txtUpdateValue.Enabled = Not checkManualUpdate.Checked
		End Sub

		'Disable the options that aren't used when the display is in AutoRefresh mode
		Private Sub checkAutoRefresh_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkAutoRefresh.CheckedChanged
			txtRefreshRate.Enabled = checkAutoRefresh.Checked
		End Sub

		Private Sub btnHelp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHelp.Click
            Dim sHelpDescription As String = "Description of ITAUpdateControl settings:" & Constants.vbLf + Constants.vbLf & "Update Settings:" & Constants.vbLf + Constants.vbTab & "Description: Determines when to refresh the map if data is actively being received from a server." & Constants.vbLf + Constants.vbTab & "Manual Update: When enabled Tracking Analyst will not automatically update the display." & Constants.vbLf + Constants.vbTab + Constants.vbTab & "The user or another application is responsible for managing display updates" & Constants.vbLf & "." & Constants.vbTab & "Update Rate: The minimum duration between screen refreshes." & Constants.vbLf + Constants.vbTab & "Update Method: Criteria that triggers an update to occur." & Constants.vbLf + Constants.vbTab + Constants.vbTab & "Event-based: An update is triggered when the number of messages specified by the Update Value has been received" & Constants.vbLf + Constants.vbTab + Constants.vbTab & "CPU Usage-based: An update is triggered when the time it takes to draw is less than the specified percentage of the time between updates." & Constants.vbLf + Constants.vbTab & "Update Value: The threshold value of points or CPU percentage that must be met before an update will occur." & Constants.vbLf + Constants.vbTab + Constants.vbTab & "The update rate will prevent a low threshold from causing the map to update too often." & Constants.vbLf & "Refresh Settings:" & Constants.vbLf + Constants.vbTab & "Description: Determines when to refresh the map if no data is being received." & Constants.vbLf + Constants.vbTab + Constants.vbTab & "If no data is being received then the map will not be updated and time windows and aging would become stale." & Constants.vbLf + Constants.vbTab & "Automatic: When enabled Tracking Analyst will perform maintenance refreshes even if no new data has been received." & Constants.vbLf + Constants.vbTab & "Refresh Rate: The maximum duration between screen refreshes/updates." & Constants.vbLf & "Display Refresh:" & Constants.vbLf + Constants.vbTab & "Description: Causes a manual screen refresh." & Constants.vbLf + Constants.vbTab & "Refresh Method: There are five different methods for refreshing the display." & Constants.vbLf + Constants.vbTab + Constants.vbTab & "See RefreshDisplay in the Developer Help for more information on the methods." & Constants.vbLf & "Statistics:" & Constants.vbLf + Constants.vbTab & "Description: XML string containing the current TAUpdateControl settings and metrics on previous screen update metrics." & Constants.vbLf

			MessageBox.Show(Me, sHelpDescription, "TAUpdateControl Description")
		End Sub
	End Class
End Namespace