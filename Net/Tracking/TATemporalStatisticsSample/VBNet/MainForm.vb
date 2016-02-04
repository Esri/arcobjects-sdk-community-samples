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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.TrackingAnalyst
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.GeoDatabaseExtensions

Namespace TemporalStatistics
	Public NotInheritable Partial Class MainForm : Inherits Form
		#Region "class private members"
		Private m_mapControl As IMapControl3 = Nothing
		Private m_mapDocumentName As String = String.Empty
		#End Region
		Private m_amsWorkspaceFactory As IWorkspaceFactory = Nothing
		Private m_bTAInitialized As Boolean = False
		Private Const TEMPORALLAYERCLSID As String = "{78C7430C-17CF-11D5-B7CF-00010265ADC5}" 'CLSID for ITemporalLayer

		#Region "class constructor"
		Public Sub New()
			InitializeComponent()
		End Sub
		#End Region

		Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			'get the MapControl
			m_mapControl = CType(axMapControl1.Object, IMapControl3)

			'disable the Save menu (since there is no document yet)
			menuSaveDoc.Enabled = False

			timerStats.Start()
		End Sub

		#Region "Main Menu event handlers"
		Private Sub menuNewDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuNewDoc.Click
			'execute New Document command
			Dim command As ICommand = New CreateNewDocument()
			command.OnCreate(m_mapControl.Object)
			command.OnClick()
		End Sub

		Private Sub menuOpenDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuOpenDoc.Click
			'execute Open Document command
			Dim command As ICommand = New ControlsOpenDocCommandClass()
			command.OnCreate(m_mapControl.Object)
			command.OnClick()
		End Sub

		Private Sub menuSaveDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuSaveDoc.Click
			'execute Save Document command
			If m_mapControl.CheckMxFile(m_mapDocumentName) Then
				'create a new instance of a MapDocument
				Dim mapDoc As IMapDocument = New MapDocumentClass()
				mapDoc.Open(m_mapDocumentName, String.Empty)

                'Make sure that the MapDocument is not readonly
                If mapDoc.IsReadOnly(m_mapDocumentName) Then
                    MessageBox.Show("Map document is read only!")
                    mapDoc.Close()
                    Return
                End If

				'Replace its contents with the current map
				mapDoc.ReplaceContents(CType(m_mapControl.Map, IMxdContents))

				'save the MapDocument in order to persist it
				mapDoc.Save(mapDoc.UsesRelativePaths, False)

				'close the MapDocument
				mapDoc.Close()
			End If
		End Sub

		Private Sub menuSaveAs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuSaveAs.Click
			'execute SaveAs Document command
			Dim command As ICommand = New ControlsSaveAsDocCommandClass()
			command.OnCreate(m_mapControl.Object)
			command.OnClick()
		End Sub

		Private Sub menuExitApp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuExitApp.Click
			'exit the application
			Application.Exit()
		End Sub
		#End Region

        'listen to MapReplaced event in order to update the status bar and the Save menu
		Private Sub axMapControl1_OnMapReplaced(ByVal sender As Object, ByVal e As IMapControlEvents2_OnMapReplacedEvent) Handles axMapControl1.OnMapReplaced
			'get the current document name from the MapControl
			m_mapDocumentName = m_mapControl.DocumentFilename

            'if there is no MapDocument, disable the Save menu and clear the status bar
			If m_mapDocumentName = String.Empty Then
				menuSaveDoc.Enabled = False
				statusBarXY.Text = String.Empty
			Else
                'enable the Save menu and write the doc name to the status bar
				menuSaveDoc.Enabled = True
				statusBarXY.Text = Path.GetFileName(m_mapDocumentName)
			End If

			'Update combo list of tracking services
			PopulateTrackingServices()
		End Sub

		Private Sub axMapControl1_OnMouseMove(ByVal sender As Object, ByVal e As IMapControlEvents2_OnMouseMoveEvent) Handles axMapControl1.OnMouseMove
			statusBarXY.Text = String.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4))
		End Sub

		'Initialize the Tracking Analyst Environment
        Private Function setupTrackingEnv(ByRef mapObj As Object) As ITrackingEnvironment3
            Dim extentionManager As IExtensionManager = New ExtensionManagerClass()

            Dim uid As UID = New UIDClass()
            uid.Value = "esriTrackingAnalyst.TrackingEngineUtil"

            CType(extentionManager, IExtensionManagerAdmin).AddExtension(uid, mapObj)

            Dim trackingEnv As ITrackingEnvironment3 = New TrackingEnvironmentClass()

            'mapObj = m_mapControl
            Dim oMapControl As Object = m_mapControl.Object
            trackingEnv.Initialize(oMapControl)
            trackingEnv.EnableTemporalDisplayManagement = True
            Return trackingEnv
        End Function

		'Periodically update the statistics information
		Private Sub timerStats_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerStats.Tick
			'Initialize TA if there is hasn't been already and there are Tracking layers in the map
			If (Not m_bTAInitialized) AndAlso Not GetAllTrackingLayers() Is Nothing Then
				Dim oMapControl As Object = m_mapControl
                Dim taEnv As ITrackingEnvironment3 = setupTrackingEnv(oMapControl)

				If Not taEnv Is Nothing Then
					m_bTAInitialized = True
				End If

				'Need to refresh the map once to get the tracks moving
				m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)
			End If

			RefreshStatistics()
		End Sub

		Private Sub RefreshStatistics()
			Try
				Dim temporalLayer As ITemporalLayer = GetSelectedTemporalLayer()

                'If a temporal layer is selected in the combo box only update that layer's stats
				If temporalLayer Is Nothing Then
					RefreshAllStatistics()
				Else
					RefreshLayerStatistics(temporalLayer.Name, CType((CType(temporalLayer, IFeatureLayer)).FeatureClass, ITemporalFeatureClassStatistics))
				End If
			Catch ex As Exception
				statusBarXY.Text = ex.Message
			End Try
		End Sub

		'Refresh the statistics for all tracking layers in the map
		'The AMSWorkspaceFactory provides easy access to query the statistics for every layer at once
		Private Sub RefreshAllStatistics()
			Try
                Dim oValues As Object = Nothing
                Dim oNames As Object = Nothing
                Dim sNames As String()

				If m_amsWorkspaceFactory Is Nothing Then
					m_amsWorkspaceFactory = New AMSWorkspaceFactoryClass()
				End If

				'Get the AMS Workspace Factory Statistics interface
				Dim temporalWsfStatistics As ITemporalWorkspaceStatistics = CType(m_amsWorkspaceFactory, ITemporalWorkspaceStatistics)
				'Get the message rates for all the tracking connections in the map
				Dim psMessageRates As IPropertySet = temporalWsfStatistics.AllMessageRates
				psMessageRates.GetAllProperties(oNames, oValues)
				sNames = CType(oNames, String())
				Dim oaMessageRates As Object() = CType(oValues, Object())

				'Get the feature counts for all the tracking connections in the map
				Dim psTotalFeatureCounts As IPropertySet = temporalWsfStatistics.AllTotalFeatureCounts
				psTotalFeatureCounts.GetAllProperties(oNames, oValues)
				Dim oaFeatureCounts As Object() = CType(oValues, Object())

				'Get the connection status for all the tracking connections in the map
				Dim psConnectionStatus As IPropertySet = temporalWsfStatistics.ConnectionStatus
				psConnectionStatus.GetAllProperties(oNames, oValues)
				Dim sConnectionNames As String() = CType(oNames, String())
				Dim oaConnectionStatus As Object() = CType(oValues, Object())
				Dim htConnectionStatus As Hashtable = New Hashtable(sConnectionNames.Length)
				Dim i As Integer = 0
				Do While i < sConnectionNames.Length
					htConnectionStatus.Add(sConnectionNames(i), oaConnectionStatus(i))
					i += 1
				Loop

				'Get the track counts for all the tracking connections in the map
				Dim psTrackCounts As IPropertySet = temporalWsfStatistics.AllTrackCounts
				psTrackCounts.GetAllProperties(oNames, oValues)
				Dim oaTrackCounts As Object() = CType(oValues, Object())

				'Get the sample sizes for all the tracking connections in the map
				Dim psSampleSizes As IPropertySet = temporalWsfStatistics.AllSampleSizes
				psSampleSizes.GetAllProperties(oNames, oValues)
				Dim oaSampleSizes As Object() = CType(oValues, Object())

				'Clear the existing list view items and repopulate from the collection
				lvStatistics.BeginUpdate()
				lvStatistics.Items.Clear()

				'Create list view items from statistics' IPropertySets
				i = 0
				Do While i < sNames.Length
					Dim lvItem As ListViewItem = New ListViewItem(sNames(i))
					lvItem.SubItems.Add(Convert.ToString(oaMessageRates(i)))
					lvItem.SubItems.Add(Convert.ToString(oaFeatureCounts(i)))

					Dim sConnName As String = sNames(i).Split(New Char() { "/"c })(0)
					Dim eWCS As esriWorkspaceConnectionStatus = CType(Convert.ToInt32(htConnectionStatus(sConnName)), esriWorkspaceConnectionStatus)
					lvItem.SubItems.Add(eWCS.ToString())

					lvItem.SubItems.Add(Convert.ToString(oaTrackCounts(i)))
					lvItem.SubItems.Add(Convert.ToString(oaSampleSizes(i)))
					lvStatistics.Items.Add(lvItem)
					i += 1
				Loop

				lvStatistics.EndUpdate()
			Catch ex As System.Exception
				statusBarXY.Text = ex.Message
			End Try
		End Sub

		'Refresh the statistics for a single layer using the ITemporalFeatureClassStatistics
		Private Sub RefreshLayerStatistics(ByVal sLayerName As String, ByVal temporalFCStats As ITemporalFeatureClassStatistics)
			Dim lvItem As ListViewItem = New ListViewItem(sLayerName)
			lvItem.SubItems.Add(Convert.ToString(temporalFCStats.MessageRate))
			lvItem.SubItems.Add(Convert.ToString(temporalFCStats.TotalFeatureCount))
			lvItem.SubItems.Add("Not Available")
			lvItem.SubItems.Add(Convert.ToString(temporalFCStats.TrackCount))
			lvItem.SubItems.Add(Convert.ToString(temporalFCStats.SampleSize))

			lvStatistics.Items.Clear()
			lvStatistics.Items.Add(lvItem)
		End Sub

		'Cause a manual refresh of the statistics, also update the timer interval
		Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
			Try
				timerStats.Stop()
				SetSampleSize()
				RefreshStatistics()
				Dim dTimerRate As Double = Convert.ToDouble(txtRate.Text)
				timerStats.Interval = Convert.ToInt32(dTimerRate * 1000)
				timerStats.Start()
			Catch ex As Exception
				statusBarXY.Text = ex.Message
			End Try
		End Sub

        'Populate the combo box with the tracking services in the map
		Private Sub PopulateTrackingServices()
			Dim lyr As ILayer = Nothing
			Dim temporalLayers As IEnumLayer = GetAllTrackingLayers()

			cbTrackingServices.Items.Clear()
			cbTrackingServices.Items.Add("All")

			If Not temporalLayers Is Nothing Then
                lyr = temporalLayers.Next()
                Do While Not (lyr) Is Nothing
                    cbTrackingServices.Items.Add(lyr.Name)
                    lyr = temporalLayers.Next()
                Loop
            End If

			cbTrackingServices.SelectedIndex = 0
		End Sub

		'Query the map for all the tracking layers in it
		Private Function GetAllTrackingLayers() As IEnumLayer
			Try
				Dim uidTemoralLayer As IUID = New UIDClass()
				uidTemoralLayer.Value = TEMPORALLAYERCLSID

				'This call throws an E_FAIL exception if the map has no layers, caught below
                Return m_mapControl.ActiveView.FocusMap.Layers(CType(uidTemoralLayer, UID), True)
			Catch
				Return Nothing
			End Try
		End Function

        'Get the tracking layer that is selected in the combo box according to its name
		Private Function GetSelectedTemporalLayer() As ITemporalLayer
			Dim temporalLayer As ITemporalLayer = Nothing

			If cbTrackingServices.SelectedIndex > 0 Then
				Dim lyr As ILayer = Nothing
				Dim temporalLayers As IEnumLayer = GetAllTrackingLayers()
				Dim selectedLayerName As String = cbTrackingServices.Text

                lyr = temporalLayers.Next()
                Do While Not lyr Is Nothing
                    If lyr.Name = selectedLayerName Then
                        temporalLayer = CType(lyr, ITemporalLayer)
                    End If
                    lyr = temporalLayers.Next()
                Loop
			End If

			Return temporalLayer
		End Function

        'Reset the statistic's feature count for one or all of the layers in the map
		Private Sub btnResetFC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetFC.Click
			Try
				Dim temporalLayer As ITemporalLayer = GetSelectedTemporalLayer()

				If temporalLayer Is Nothing Then
					If m_amsWorkspaceFactory Is Nothing Then
						m_amsWorkspaceFactory = New AMSWorkspaceFactoryClass()
					End If

					'Get the AMS Workspace Factory Statistics interface
					Dim temporalWsfStatistics As ITemporalWorkspaceStatistics = CType(m_amsWorkspaceFactory, ITemporalWorkspaceStatistics)
					temporalWsfStatistics.ResetAllFeatureCounts()
				Else
					Dim temporalFCStats As ITemporalFeatureClassStatistics = CType((CType(temporalLayer, IFeatureLayer)).FeatureClass, ITemporalFeatureClassStatistics)
					temporalFCStats.ResetFeatureCount()
				End If
			Catch ex As Exception
				statusBarXY.Text = ex.Message
			End Try
		End Sub

        'Reset the statistic's message rate for one or all of the layers in the map
		Private Sub btnResetMsgRate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetMsgRate.Click
			Try
				Dim temporalLayer As ITemporalLayer = GetSelectedTemporalLayer()

				If temporalLayer Is Nothing Then
					If m_amsWorkspaceFactory Is Nothing Then
						m_amsWorkspaceFactory = New AMSWorkspaceFactoryClass()
					End If

					'Get the AMS Workspace Factory Statistics interface
					Dim temporalWsfStatistics As ITemporalWorkspaceStatistics = CType(m_amsWorkspaceFactory, ITemporalWorkspaceStatistics)
					temporalWsfStatistics.ResetAllMessageRates()
				Else
					Dim temporalFCStats As ITemporalFeatureClassStatistics = CType((CType(temporalLayer, IFeatureLayer)).FeatureClass, ITemporalFeatureClassStatistics)
					temporalFCStats.ResetMessageRate()
				End If
			Catch ex As Exception
				statusBarXY.Text = ex.Message
			End Try
		End Sub

		'Set the sampling size for one or all of the layers in the map
		'The sampling size determines how many messages are factored into the message rate calculation.  
		'For instance a sampling size of 500 will store the times the last 500 messages were received.
        'The message rate is calculated as the (oldest timestamp - current time) / number of messages
		Private Sub SetSampleSize()
			Try
				Dim samplingSize As Integer = Convert.ToInt32(txtSampleSize.Text)
				Dim temporalLayer As ITemporalLayer = GetSelectedTemporalLayer()

				If temporalLayer Is Nothing Then
					If m_amsWorkspaceFactory Is Nothing Then
						m_amsWorkspaceFactory = New AMSWorkspaceFactoryClass()
					End If

					'Get the AMS Workspace Factory Statistics interface
					Dim temporalWsfStatistics As ITemporalWorkspaceStatistics = CType(m_amsWorkspaceFactory, ITemporalWorkspaceStatistics)
					temporalWsfStatistics.SetAllSampleSizes(samplingSize)
				Else
					Dim temporalFCStats As ITemporalFeatureClassStatistics = CType((CType(temporalLayer, IFeatureLayer)).FeatureClass, ITemporalFeatureClassStatistics)
					temporalFCStats.SampleSize = samplingSize
				End If
			Catch ex As Exception
				statusBarXY.Text = ex.Message
			End Try
		End Sub

		Private Sub cbTrackingServices_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs) Handles cbTrackingServices.SelectionChangeCommitted
			RefreshStatistics()
		End Sub
	End Class
End Namespace