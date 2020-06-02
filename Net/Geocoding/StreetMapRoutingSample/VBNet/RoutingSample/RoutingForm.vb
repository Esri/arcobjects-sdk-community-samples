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
Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Location
Imports ESRI.ArcGIS.LocationUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Display


Public Class RoutingForm
	Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

	Public Sub New()
		MyBase.New()

		'This call is required by the Windows Form Designer.
		InitializeComponent()

		m_dlgDirections = New DirectionsForm
		m_dlgRestrictions = New RestrictionsForm
	End Sub

#End Region

#Region "Form Buttons Events Handlers"

	' Just hides form on closing
	Private Sub RoutingForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
		e.Cancel = True
		Me.Hide()

		' Bug workaround.
		' ArcMap can lose focus after form closing. SetFocus prevents it.
		SetFocus(m_application.hWnd)
	End Sub

	' Bug workaround.
	' Allows Tab and Escape buttons using in form
	Private Sub RoutingForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
		If Not Me.Modal Then
			If e.KeyCode.Equals((Keys.Tab)) Then
				e.Handled = True
				Me.ProcessTabKey(Not e.Modifiers = Keys.Shift)
			ElseIf e.KeyCode.Equals((Keys.Escape)) Then
				Me.Close()
			End If
		End If
	End Sub

	' Bug workaround.
	' Allows Tab button using in form
	Protected Overrides ReadOnly Property ShowFocusCues() As Boolean
		Get
			If Me.Modal Then
				Return MyBase.ShowFocusCues
			Else
				Return True
			End If
		End Get
	End Property

	Private Sub m_btnRoutingService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnRoutingService.Click
		OpenRoutingService()
	End Sub

	Private Sub m_btnAddressLocator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnAddressLocator.Click
		OpenAddressLocator()
	End Sub

	Private Sub m_btnBarriersOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnBarriersOpen.Click
		OpenBarriers()
	End Sub

	Private Sub m_btnBarriersClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnBarriersClear.Click
		ClearBarriers()
	End Sub

	Private Sub m_btnFindRoute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnFindRoute.Click
		FindRoute()
	End Sub

	Private Sub m_btnShowDirections_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnShowDirections.Click
		ShowDirections()
	End Sub

	Private Sub m_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnClose.Click
		Me.Close()
	End Sub

	Private Sub RoutingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		m_cmbDistanceUnit.SelectedIndex = 2
	End Sub

	Private Sub m_btnRestrictions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnRestrictions.Click
		ShowRestrictions()
	End Sub

#End Region

#Region "Public members"

	' Store ArcMap application as internal member
	Public Sub Init(ByVal application As IApplication)
		m_application = application
	End Sub

#End Region

#Region "Private Helpers"

#Region "Initialization helpers"

	' Open Route Service file and create Router and Spatial Reference
	Private Sub OpenRoutingService()
		Try
			Dim res As DialogResult = m_dlgRoutingSrvc.ShowDialog(Me)
			If res = System.Windows.Forms.DialogResult.OK Then
				Dim strPath As String = m_dlgRoutingSrvc.FileName

				' create router factory	
				If m_objRouterFactory Is Nothing Then _
				 m_objRouterFactory = New SMRouterFactoryClass

				' if new path passed
				If (m_objRouter Is Nothing) Or (strPath <> m_strRouteServicePath) Then
					' Barriers should be created again for new Router
					ClearBarriers()

					m_strRouteServicePath = strPath

					' create new router 
					m_objRouter = m_objRouterFactory.CreateRouter(m_strRouteServicePath)
					m_txtRoutingService.Text = m_strRouteServicePath

					'get Router Spatial Reference
					m_objSpatialReference = CreateSpatialReference(m_objRouter.ProjectionString)

					' Init Restrictions
					m_dlgRestrictions.Init(m_objRouter)
				End If
			End If
		Catch ex As Exception
			' Clear on Error
			m_objRouter = Nothing
			m_objSpatialReference = Nothing
			m_txtRoutingService.Text = ""
			m_strRouteServicePath = ""
		Finally
			' Check FindRoute, Barriers and Restrictions buttons state
			CheckRouteButtons()
		End Try
	End Sub

	' Opens Address Locator for addresses geocoding
	Private Sub OpenAddressLocator()
		Try
			' Create Dialog on first call and init filter
			InitAddressLocatorDlg()

			Dim objLocator As ILocator
			objLocator = Nothing

			' Get Locator
			Dim gxObjects As IEnumGxObject
			gxObjects = Nothing

			If m_dlgAddressLocator.DoModalOpen(Me.Handle.ToInt32, gxObjects) And (Not gxObjects Is Nothing) Then
				gxObjects.Reset()

				' Get first Locator
				Dim gxObject As IGxObject
				gxObject = gxObjects.Next()

				If Not gxObject Is Nothing Then
					Dim gxLocator As IGxLocator
					gxLocator = gxObject
					objLocator = gxLocator.Locator
					m_objAddressGeocoding = objLocator
				End If
			End If

			If m_objAddressGeocoding Is Nothing Then
				m_txtAddressLocator.Text = ""
			Else
				m_txtAddressLocator.Text = objLocator.Name
			End If
		Catch ex As Exception
			' Clear on Error
			m_objAddressGeocoding = Nothing
			m_txtAddressLocator.Text = ""
		Finally
			' Check FindRoute, Barriers and Restrictions buttons state
			CheckRouteButtons()
		End Try
	End Sub

	' Adds Barriers from Dataset
	Private Sub OpenBarriers()
		Dim Cursor As System.Windows.Forms.Cursor = Me.Cursor
		Try
			' Create Dialog on first call and init filter
			InitBarriersDlg()

			' Get Barriers 
			Dim gxObject As IGxObject

			Dim gxObjects As IEnumGxObject
			gxObjects = Nothing

			If m_dlgBarriers.DoModalOpen(Me.Handle.ToInt32, gxObjects) And (Not gxObjects Is Nothing) Then

				Me.Cursor = Cursors.WaitCursor

				' Init Barriers
				ClearBarriers()

				' use first object
				gxObjects.Reset()
				gxObject = gxObjects.Next()

				' Use first object
				If Not gxObject Is Nothing Then
					' Add Barriersfrom object dataset
					Dim objGxDS As IGxDataset
					objGxDS = gxObject

					AddBarriersFromDataset(objGxDS.Dataset)
				End If

				' Is Barriers added to Router
				If m_nBarriersCount = 0 Then
					m_txtBarriers.Text = ""
				Else
					m_txtBarriers.Text = gxObject.Name
				End If

				If m_nBarriersIgnoredCount > 0 Then
					MessageBox.Show(Me, m_nBarriersIgnoredCount.ToString + " barriers cannot be added.", _
					 "Routing Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				End If
			End If

		Catch ex As Exception
			' Clear on Error
			ClearBarriers()
		Finally
			Me.Cursor = Cursor
		End Try
	End Sub

#End Region

#Region "Finding Route helpers"

	' Find route and initialize Driving Directions page
	Private Sub FindRoute()
		If m_objRouter Is Nothing Then _
			Exit Sub

		' Init DD
		m_btnShowDirections.Enabled = False
		m_dlgDirections.Init()

		Dim Cursor As System.Windows.Forms.Cursor = Me.Cursor
		Try
			Me.Cursor = Cursors.WaitCursor

			' set highways priority (0.0 - 100.0)	
			Dim objPreferences As SMRoadPreferences
			objPreferences = m_objRouter.Preferences
			objPreferences.Item(esriSMRoadType.esriSMRoadTypeHighways) = CShort(m_trackUseRoad.Value)

			' set route type (Time/Length)	
			If m_rbtnQuickest.Checked Then
				m_objRouter.NetAttributeName = "Time"
			Else
				m_objRouter.NetAttributeName = "Length"
			End If

			' Set Length Units for Directions output
			SetDirectionsUnits()

			' Geocode user data and adds Start and finish
			Dim objStopsCol As SMStopsCollection
			objStopsCol = New SMStopsCollectionClass
			AddStops(objStopsCol)

			' Add restrictions
			m_dlgRestrictions.SetupRouter(m_objRouter)

			' get driving directions	
			Dim objDirections As ISMDirections
			objDirections = m_objRouter.Solve(objStopsCol, Nothing)

			' Output result, zoom to route, fill directions
			ShowResult(objDirections)

		Catch ex As Exception
			MessageBox.Show(Me, "Cannot find route." + vbCrLf + ex.Message, _
			 "Routing Sample", MessageBoxButtons.OK, MessageBoxIcon.Error)

			' Init DD
			m_btnShowDirections.Enabled = False
			m_dlgDirections.Init()
		Finally
			Me.Cursor = Cursor
		End Try

	End Sub

	' Geocodes user data and adds Start and finish
	Private Sub AddStops(ByVal objStopsCol As SMStopsCollection)
		' create and add "From" stop to stops collection	
		CreateStop(m_objRouter, _
		 m_txtStartAddress.Text, m_txtStartCity.Text, _
		 m_txtStartState.Text, m_txtStartCode.Text, _
		 objStopsCol, 0)

		' create and add "To" stop to stops collection	
		CreateStop(m_objRouter, _
		 m_txtFinishAddress.Text, m_txtFinishCity.Text, _
		 m_txtFinishState.Text, m_txtFinishCode.Text, _
		 objStopsCol, 1)
	End Sub

	' Returns Address string
	Private Function GetAddressString(ByVal strAddress As String, ByVal strCity As String, _
	  ByVal strState As String, ByVal strCode As String) As String

		Dim strAddress1 As String = strAddress
		If strAddress1.Length > 0 Then _
		 strAddress1 = strAddress1 + ", "

		Dim strCity1 As String = strCity
		If strCity1.Length > 0 Then _
		 strCity1 = strCity1 + ", "

		Dim strCode1 As String = strCode
		If strCode1.Length > 0 Then _
		 strCode1 = strCode1 + ", "

		Dim strRes As String = strAddress1 + strCode1 + strCity1 + strState

		If strRes.EndsWith(", ") Then _
		 strRes = strRes.Remove(strRes.Length - 2, 2)

		Return strRes
	End Function

	' Geocodes address by Address, City, State, ZIP code
    Private Function GeocodeAddress(ByVal strAddress As String, ByVal strCity As String, _
      ByVal strState As String, ByVal strCode As String) As IPoint

        If m_objAddressGeocoding Is Nothing Then
            Throw New Exception("Cannot geocode address.")
        End If

        ' Get Address fields from textboxes
        Dim objAddressProperties As IPropertySet
        objAddressProperties = New PropertySetClass
        With objAddressProperties
            .SetProperty("Street", strAddress)
            .SetProperty("City", strCity)
            .SetProperty("State", strState)
            .SetProperty("ZIP", strCode)
        End With

        ' Match Address
        Dim objMatchProperties As IPropertySet
        objMatchProperties = m_objAddressGeocoding.MatchAddress(objAddressProperties)

        Dim objMatchFields As IFields
        objMatchFields = m_objAddressGeocoding.MatchFields

        If objMatchFields.FieldCount > 0 Then
            ' Use first candidate
            Dim objGeometryField As IField
            Dim nFieldCount As Integer = objMatchFields.FieldCount
            Dim nIndex As Integer
            'objMatchField = objMatchFields.Field(0)
            For nIndex = 0 To (nFieldCount - 1)
                Dim objCurField As IField = objMatchFields.Field(nIndex)
                If objCurField.Type = esriFieldType.esriFieldTypeGeometry Then
                    objGeometryField = objCurField
                End If
            Next

            If Not objGeometryField Is Nothing Then
                Return objMatchProperties.GetProperty(objGeometryField.Name)
            Else
                Throw New Exception("Cannot obtain geometry field.")
            End If
        End If

        Throw New Exception("Cannot geocode address.")
    End Function

    ' Creates Stop by Stop Point, Index and Description and adds it to Stops collection
    Private Sub CreateStop(ByVal objRouter As SMRouter, _
      ByVal strAddress As String, ByVal strCity As String, _
      ByVal strState As String, ByVal strCode As String, _
      ByVal objStopsCol As SMStopsCollection, _
      ByVal nID As Integer)

        ' geocode point
        Dim objPoint As Point
        objPoint = GeocodeAddress(strAddress, strCity, strState, strCode)

        If objPoint.IsEmpty Then _
         Throw New Exception("Cannot geocode address.")

        ' project point
        objPoint.Project(m_objSpatialReference)

        ' create and initialize router point	
        Dim objRouterPoint As SMRouterPoint
        objRouterPoint = New SMRouterPointClass

        objRouterPoint.X = objPoint.X
        objRouterPoint.Y = objPoint.Y

        ' create flag	
        Dim objFlagCreator2 As ISMFlagCreator2
        objFlagCreator2 = objRouter.FlagCreator
        If Not objFlagCreator2 Is Nothing Then _
         objFlagCreator2.SearchTolerance = 5

        Dim objFlag As SMFlag
        objFlag = objRouter.FlagCreator.CreateFlag(objRouterPoint)

        ' create and initialize stop	
        Dim objStop As SMStop
        objStop = New SMStopClass
        objStop.StopID = nID
        objStop.Duration = 0
        objStop.Flag = objFlag
        objStop.Description = GetAddressString(strAddress, strCity, strState, strCode)

        objStopsCol.Add(objStop)
    End Sub

	Private Function CreateSpatialReference(ByVal strPrjString As String) As ISpatialReference
		' create router spatial reference	
		Dim objSpatRefFact As ISpatialReferenceFactory
		objSpatRefFact = New SpatialReferenceEnvironmentClass

		' create temporary file
		Dim strTempPath As String = System.IO.Path.GetTempFileName()

		Try
			Dim wrtr As System.IO.StreamWriter = System.IO.File.CreateText(strTempPath)
			wrtr.Write(strPrjString)
			wrtr.Close()

			Dim objSR As ISpatialReference
			objSR = objSpatRefFact.CreateESRISpatialReferenceFromPRJFile(strTempPath)

			CreateSpatialReference = objSR
		Finally
			System.IO.File.Delete(strTempPath)
		End Try

	End Function

	' Set length unit for Direction
	Private Sub SetDirectionsUnits()
		Dim nIndex As Integer = m_cmbDistanceUnit.SelectedIndex

		' Get Router Setup object
        Dim objSetup As ISMRouterSetup2
		objSetup = m_objRouter

		' get Directions unit
		Dim eUnits As esriSMDirectionsLengthUnits

		If nIndex = 0 Then ' Feet
			eUnits = esriSMDirectionsLengthUnits.esriSMDLUFeet
		ElseIf nIndex = 1 Then ' Yards
			eUnits = esriSMDirectionsLengthUnits.esriSMDLUYards
		ElseIf nIndex = 2 Then ' Miles
			eUnits = esriSMDirectionsLengthUnits.esriSMDLUMiles
		ElseIf nIndex = 3 Then ' Meters
			eUnits = esriSMDirectionsLengthUnits.esriSMDLUMeters
		ElseIf nIndex = 4 Then ' Kilometers
			eUnits = esriSMDirectionsLengthUnits.esriSMDLUKilometers
		End If

		' Set Directions Unit
		objSetup.DirectionsLengthUnits = eUnits
	End Sub

#End Region

#Region "Resulting helpers"

	' Outputs result, zoom to route, fill directions
	Private Sub ShowResult(ByVal objDirections As ISMDirections)
		' Fill directions
		m_dlgDirections.Init(objDirections)
		m_btnShowDirections.Enabled = True

		Try
            ' Zoom to extent
			Dim objEnv As Envelope
			objEnv = New EnvelopeClass

			objEnv.PutCoords(objDirections.BoundBox.Left, objDirections.BoundBox.Bottom, _
			 objDirections.BoundBox.Right, objDirections.BoundBox.Top)
			objEnv.Expand(1.1, 1.1, True)

			' set spatial reference
			Dim objGeo As IGeometry = objEnv
			objGeo.SpatialReference = m_objSpatialReference

			ZoomToExtent(objEnv)

			' Create polyline
			CreatePolyline(objDirections)
		Catch ex As Exception
			MessageBox.Show(Me, "Cannot show result.", _
			 "Routing Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Finally
			' Refresh map
			RefreshMap()
		End Try
	End Sub

	' refreshes Map
	Private Sub RefreshMap()
		Dim objDoc As IMxDocument
		objDoc = m_application.Document

		Dim objMap As Map
		objMap = objDoc.FocusMap

		Dim objView As IActiveView
		objView = objMap

		objView.Refresh()
	End Sub

	' Zooms Map to extent
	Private Sub ZoomToExtent(ByVal objEnv As Envelope)
		Dim objDoc As IMxDocument
		objDoc = m_application.Document

		Dim objMap As Map
		objMap = objDoc.FocusMap

		Dim objView As IActiveView
		objView = objMap

		' check if projections are not equal
		Dim objGeo As IGeometry = objEnv

		' project to Map projection and expand
		If Not IsSREqual(objGeo.SpatialReference, objMap.SpatialReference) Then _
		 objEnv.Project(objMap.SpatialReference)

		' Zoom
		objView.Extent = objEnv
	End Sub

	' Returns true if Spatial Systems is equal (without precision)
	Private Function IsSREqual(ByVal objSR1 As ISpatialReference, _
	  ByVal objSR2 As ISpatialReference) As Boolean
		Dim objSRClone1 As IClone = objSR1
		IsSREqual = objSRClone1.IsEqual(objSR2)
	End Function

	' Creates Route polyline
	Private Sub CreatePolyline(ByVal objDirections As ISMDirections)
		' create polyline
		Dim objLine As IPolyline
		objLine = New PolylineClass

		' get points collection
		Dim objPoints As IPointCollection
		objPoints = objLine

		' Adds Directions points to polyline
		AddPointsToPolyline(objDirections, objPoints)

		' Project points to Map projection
		Dim objDoc As IMxDocument
		objDoc = m_application.Document
		Dim objMap As Map = objDoc.FocusMap
		objLine.Project(objMap.SpatialReference)

		' create path graphics element
		Dim objElement As IElement
		objElement = New LineElementClass

		objElement.Geometry = objLine

		' Set line color width and style
		SetLineProperties(objElement)

        ' get Graphic container
		Dim objCont As IGraphicsContainer = objMap

		' Add line to map
		objCont.AddElement(objElement, 0)
	End Sub

	' Sets line color, width and style
	Private Sub SetLineProperties(ByVal objElement As IElement)
		Dim objSymbol As SimpleLineSymbol
		objSymbol = New SimpleLineSymbolClass

		' Set Symbol style and width
		objSymbol.Style = esriSimpleLineStyle.esriSLSSolid
		objSymbol.Width = 10

		' Set symbol color
		Dim objColor As RgbColor
		objColor = New RgbColorClass
		objColor.Red = 0
		objColor.Green = 255
		objColor.Blue = 0

		objSymbol.Color = objColor

		' set line symbol
		Dim objLineElement As ILineElement = objElement
		objLineElement.Symbol = objSymbol
	End Sub

	' Adds Directions points to point collection
	Private Sub AddPointsToPolyline(ByVal objDirections As ISMDirections, _
	  ByRef objPoints As IPointCollection)

		Dim objPoint As Point
		objPoint = New PointClass

		' copy points from DD to line
		Dim nItemsCount As Integer = objDirections.Count
		For i As Integer = 0 To nItemsCount - 1

			' get shape from Direction
			Dim objItem As ISMDirItem
			objItem = objDirections.Item(i)

			Dim objShape As ISMPointsCollection
			objShape = objItem.Shape

            ' Add point from Direction to received collection
			Dim nPointsCount As Integer = objShape.Count - 1
			For j As Integer = 0 To nPointsCount

				' get point from route
				Dim objRouterPoint As SMRouterPoint = objShape.Item(j)

                ' Optimization: Not add point if last added point has similar coords
				Dim bAddPoint As Boolean = False

				If objPoint.IsEmpty Then
					bAddPoint = True
				ElseIf (objPoint.X <> objRouterPoint.X) And (objPoint.Y <> objRouterPoint.Y) Then
					bAddPoint = True
				End If

				If bAddPoint Then
					' Add point if need
					objPoint.X = objRouterPoint.X
					objPoint.Y = objRouterPoint.Y
					objPoint.SpatialReference = m_objSpatialReference

					objPoints.AddPoint(objPoint)
				End If
			Next
		Next
	End Sub

#End Region

#Region "Barriers helpers"

	' Adds barriers from object dataset
	Private Sub AddBarriersFromDataset(ByVal objDS As IDataset)
		If objDS.Type = esriDatasetType.esriDTFeatureClass Then
			' It is Feature Class
			AddBarriersFromFeatureClass(objDS)

		ElseIf objDS.Type = esriDatasetType.esriDTFeatureDataset Then
			' It is Feature DatasetClass

			Dim objFeatureDS As IFeatureDataset
			objFeatureDS = objDS

			' Enum Dataset subsets
			Dim objEnumDS As IEnumDataset
			objEnumDS = objFeatureDS.Subsets

			Dim objSubDS As IDataset
			objSubDS = objEnumDS.Next
			Do Until objSubDS Is Nothing
				' Add Barriers from subset Feature Class 
				If objSubDS.Type = esriDatasetType.esriDTFeatureClass Then _
				 AddBarriersFromFeatureClass(objSubDS)

				objSubDS = objEnumDS.Next
			Loop
		End If
	End Sub

	' Adds Barriers from Feature Class 
	Private Sub AddBarriersFromFeatureClass(ByVal objFeatureClass As IFeatureClass)
		' only for Point features
		If objFeatureClass.ShapeType = esriGeometryType.esriGeometryPoint Then
			' create filter
			Dim objFilter As IQueryFilter
			objFilter = New QueryFilterClass

			' enum features
			Dim objCursor As IFeatureCursor
			objCursor = objFeatureClass.Search(objFilter, False)

			Dim objFeature As IFeature
			objFeature = objCursor.NextFeature

			Do Until objFeature Is Nothing
				' Add feature Point to Barriers List
				AddBarrier(objFeature.ShapeCopy)

				objFeature = objCursor.NextFeature
			Loop

		End If
	End Sub

	' Add Barrier
	Private Sub AddBarrier(ByVal objPoint As IPoint)
		' project to Routing projection
		objPoint.Project(m_objSpatialReference)

		' add point from shape to Barriers
		Dim objSMPoint As ISMRouterPoint
		objSMPoint = New SMRouterPointClass

		objSMPoint.X = objPoint.X
		objSMPoint.Y = objPoint.Y

		Dim objBarrier As ISMNetBarrier
		objBarrier = New SMNetBarrierClass

		objBarrier.BarrierID = m_nBarriersCount
		objBarrier.Point = objSMPoint

		Try
			m_objRouter.Barriers.Add(objBarrier)
		Catch ex As Exception
			m_nBarriersIgnoredCount = m_nBarriersIgnoredCount + 1
		End Try

		m_nBarriersCount = m_nBarriersCount + 1
	End Sub

    ' Clears textbox, Barriers collection in Router
	Private Sub ClearBarriers()
		m_nBarriersCount = 0
		m_nBarriersIgnoredCount = 0
		m_txtBarriers.Text = ""
		If Not m_objRouter Is Nothing Then _
		 m_objRouter.Barriers.Clear()
	End Sub

#End Region

#Region "File Dialogs helpers"

	' Open Directions Dialog
	Private Sub ShowDirections()
		m_dlgDirections.ShowDialog(Me)
	End Sub

	' Open Restrictions Dialog
	Private Sub ShowRestrictions()
		m_dlgRestrictions.ShowDialog(Me)
	End Sub

	' Checks FindRoute, Barriers and Restrictions buttons state
	Private Sub CheckRouteButtons()
		m_btnFindRoute.Enabled = False
		m_btnBarriersOpen.Enabled = False
		m_btnRestrictions.Enabled = False

		If Not m_objRouter Is Nothing Then
			If Not m_objAddressGeocoding Is Nothing Then
				m_btnFindRoute.Enabled = True
			End If

			m_btnBarriersOpen.Enabled = True
			m_btnRestrictions.Enabled = True
		End If
	End Sub

	' Creates Dialog on first call and init filter
	Private Sub InitAddressLocatorDlg()
		If m_dlgAddressLocator Is Nothing Then
            ' Create Address Locator Dialog
			m_dlgAddressLocator = New GxDialogClass

			Dim filter As IGxObjectFilter
			filter = New GxFilterAddressLocatorsClass

			m_dlgAddressLocator.ObjectFilter = filter
			m_dlgAddressLocator.Title = "Add Address Locator"
		End If
	End Sub

	' Creates Dialog on first call and init filter
	Private Sub InitBarriersDlg()
		If m_dlgBarriers Is Nothing Then
			' Create Barriers dialog
			m_dlgBarriers = New GxDialogClass

			Dim objFilter As IGxObjectFilter

			objFilter = New GxFilterFeatureDatasetsAndFeatureClassesClass
			m_dlgBarriers.ObjectFilter = objFilter

			m_dlgBarriers.Title = "Choose Barriers Layer"
		End If
	End Sub

#End Region

#Region "Imported methods"
	Public Declare Ansi Function SetFocus Lib "user32" (ByVal Hwnd As Integer) As Integer
#End Region

#End Region

#Region "Private members"

	' ArcMap application
	Private m_application As IApplication

	' Driving Direction dialog
	Private m_dlgDirections As DirectionsForm

	' Router factory and object
	Private m_objRouterFactory As SMRouterFactory
	Private m_objRouter As SMRouter
	' Store Route Services path
	Private m_strRouteServicePath As String
	' Spatial Reference of Router object
	Private m_objSpatialReference As ISpatialReference

	' Address Locator dialog
	Private m_dlgAddressLocator As GxDialog
	' Address Geocoding object
	Private m_objAddressGeocoding As IAddressGeocoding

	' Barriers dialog 
	Private m_dlgBarriers As GxDialogClass
	' Added Barriers count - used for current barrier ID
	Private m_nBarriersCount As Integer
	' Number of ignored Barriers
	Private m_nBarriersIgnoredCount As Integer

	' Restrictions dialog
	Private m_dlgRestrictions As RestrictionsForm

#End Region

End Class

