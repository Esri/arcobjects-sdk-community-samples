Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.TrackingAnalyst
Imports ESRI.ArcGIS.GeoDatabaseExtensions

Namespace TAPurgeRuleCommand
	Public Partial Class PurgeRuleForm : Inherits Form
		Private m_eTrackingLayers As IEnumLayer = Nothing

		Public Sub New()
			InitializeComponent()
		End Sub

		'The collection of temporal layers in the map/globe
		Public WriteOnly Property TrackingLayers() As IEnumLayer
			Set
				m_eTrackingLayers = Value
			End Set
		End Property

		'Initialize all the dialog controls
		Public Sub PopulateDialog()
			cbTrackingLayers.SelectedIndex = -1
			PopulateTrackingLayerComboBox()
			LoadDialogFromFeatureClassSettings(Nothing)
		End Sub

		'Populate the tracking layer combo box with the names 
		'of all the temporal layers in the map
		Private Sub PopulateTrackingLayerComboBox()
			Dim lyr As ILayer = Nothing
			m_eTrackingLayers.Reset()

            cbTrackingLayers.Items.Clear()
            lyr = m_eTrackingLayers.Next()
            Do While Not lyr Is Nothing
                cbTrackingLayers.Items.Add(lyr.Name)
                lyr = m_eTrackingLayers.Next()
            Loop
		End Sub

		'Get the ILayer using the name selected in the tracking layer combo box
		Private Function GetSelectedLayer() As ILayer
			Dim sLayerName As String = Convert.ToString(cbTrackingLayers.SelectedItem)

			Dim lyr As ILayer = Nothing
			m_eTrackingLayers.Reset()

            lyr = m_eTrackingLayers.Next()
            Do While Not lyr Is Nothing
                If lyr.Name = sLayerName Then
                    Return lyr
                End If
                lyr = m_eTrackingLayers.Next()
            Loop

			Return Nothing
		End Function

		'Repopulate the dialog controls when a different layer is selected
		Private Sub cbTrackingLayers_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs) Handles cbTrackingLayers.SelectionChangeCommitted
			Dim lyr As ILayer = GetSelectedLayer()

			If Not lyr Is Nothing Then
				Dim temporalFC As ITemporalFeatureClass = TryCast((CType(lyr, IFeatureLayer)).FeatureClass, ITemporalFeatureClass)
				LoadDialogFromFeatureClassSettings(temporalFC)
			End If

		End Sub

        'Set the dialog controls according to the settings of the specified temporal feature class
		Private Sub LoadDialogFromFeatureClassSettings(ByVal temporalFC As ITemporalFeatureClass)
			'If no feature class is specified clear all the controls
			If temporalFC Is Nothing Then
				btnApply.Enabled = False
				checkAutoPurge.Checked = False
				cbPurgeRule.SelectedIndex = 0
				txtThreshold.Text = ""
				txtPurgePercent.Text = ""
				Return
			End If

			btnApply.Enabled = True
			checkAutoPurge.Checked = temporalFC.AutoPurge
			cbPurgeRule.SelectedIndex = CInt(temporalFC.PurgeRule)
			txtThreshold.Text = Convert.ToString(temporalFC.Threshold)
			txtPurgePercent.Text = Convert.ToString(temporalFC.PurgePercentage)
		End Sub

		'Apply the new settings to the temporal feature class
		Private Sub btnApply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
			Try
				Dim lyr As ILayer = GetSelectedLayer()

				If Not lyr Is Nothing Then
					Dim temporalFC As ITemporalFeatureClass = CType((CType(lyr, IFeatureLayer)).FeatureClass, ITemporalFeatureClass)

					temporalFC.AutoPurge = checkAutoPurge.Checked

					'only set the other properties if Auto Purge is true
					If checkAutoPurge.Checked Then
						temporalFC.PurgeRule = CType(cbPurgeRule.SelectedIndex, enumPurgeRule)
						temporalFC.Threshold = Convert.ToInt32(txtThreshold.Text)
						temporalFC.PurgePercentage = Convert.ToDouble(txtPurgePercent.Text)
					End If
				End If

			Catch ex As Exception
				MessageBox.Show(ex.Message, "Error")
			End Try
		End Sub

		'Set the appropriate text boxes enabled depending on the purge rule selected
		Private Sub cbPurgeRule_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbPurgeRule.SelectedIndexChanged
			Select Case cbPurgeRule.SelectedIndex
                Case 1
                    txtThreshold.Enabled = True
                    txtPurgePercent.Enabled = False
                Case Else
                    txtThreshold.Enabled = True
                    txtPurgePercent.Enabled = True
            End Select
		End Sub

		Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
			Me.Hide()
		End Sub

		'Don't dispose the dialog when it's closed.  Just hide it so that it can be reopened.
		Private Sub PurgeRuleForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			Me.Hide()
			e.Cancel = True
		End Sub
	End Class
End Namespace