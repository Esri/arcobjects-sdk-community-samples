Option Strict Off
Option Explicit On

Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.SchematicControls
Imports ESRI.ArcGIS.esriSystem
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Display

Namespace CurrentDigitTool
    Friend Module CurrentTool
        Public currentDigit As DigitTool
        Public currentDockableWindow As ESRI.ArcGIS.Framework.IDockableWindow
        Public digitDockableWindow As DigitDockableWindow
    End Module
End Namespace

Public Class DigitTool
    Inherits ESRI.ArcGIS.Desktop.AddIns.Tool

    Private m_app As ESRI.ArcGIS.Framework.IApplication
    Public m_dockableDigit As DigitDockableWindow
    Private m_schematicExtension As ESRI.ArcGIS.esriSystem.IExtension
    Private m_schematicLayer As ISchematicLayer
    Private m_schematicLayerForLink As ISchematicLayer
    Private m_schematicFeature1 As ISchematicFeature
    Private m_schematicFeature2 As ISchematicFeature
    Private m_linkFbk As INewLineFeedback
	Private m_messageFromOK As String = vbCrLf & "Complete missing data and click on OK button"

	Private m_x As Integer
	Private m_y As Integer

	Private m_dockableWindow As ESRI.ArcGIS.Framework.IDockableWindow
	Private m_fromDeactivate As Boolean = False
	Private m_DeactivatedFromDock As Boolean = False


	Public Sub New()
		m_app = My.ArcMap.Application
	End Sub

	Protected Overrides Sub OnUpdate()
		'the tool is enable only if the diagram is in memory
		SetTargetLayer()

		If (m_schematicLayer Is Nothing) Then
			Enabled = False
			Return
		End If

		If (m_schematicLayer.IsEditingSchematicDiagram() = False) Then
			Enabled = False
			If Not m_dockableWindow Is Nothing Then
				m_dockableWindow.Show(False)
			End If

			Return
		End If

		Dim inMemoryDiagram As ISchematicInMemoryDiagram = m_schematicLayer.SchematicInMemoryDiagram

		If (inMemoryDiagram Is Nothing) Then
			Enabled = False
		Else
			Enabled = True
		End If
	End Sub

	Protected Overrides Function OnDeactivate() As Boolean
		CurrentDigitTool.CurrentTool.currentDigit = Nothing

		If Not m_dockableWindow Is Nothing And Not m_DeactivatedFromDock Then
			m_fromDeactivate = True
			m_dockableWindow.Dock(ESRI.ArcGIS.Framework.esriDockFlags.esriDockUnPinned)
			m_dockableWindow.Show(False)
		Else
			m_DeactivatedFromDock = False
		End If

		Return True

	End Function

	Protected Overrides Sub OnActivate()
		Me.Cursor = Cursors.Cross

		CurrentDigitTool.CurrentTool.currentDigit = Me

		SetTargetLayer()

		Dim windowID As UID = New UIDClass
		windowID.Value = "DigitTool_DockableWindowVB"
		m_dockableWindow = My.ArcMap.DockableWindowManager.GetDockableWindow(windowID)

		If (m_dockableDigit Is Nothing) Then
			m_dockableDigit = CurrentDigitTool.digitDockableWindow
		End If

		If m_dockableDigit IsNot Nothing Then
			m_dockableDigit.Init(m_schematicLayer)
		End If

		m_dockableWindow.Show(True)

		CurrentDigitTool.CurrentTool.currentDockableWindow = m_dockableWindow

	End Sub

	Protected Overrides Sub OnMouseMove(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
		Try
			If Not m_schematicFeature1 Is Nothing Then

				Dim mxApp As ESRI.ArcGIS.ArcMapUI.IMxApplication = m_app

				If mxApp Is Nothing Then
					Return
				End If

				Dim appDisplay As ESRI.ArcGIS.Display.IAppDisplay = mxApp.Display

				If appDisplay Is Nothing Then
					Return
				End If

				Dim screenDisplay As IScreenDisplay = appDisplay.FocusScreen

				If screenDisplay Is Nothing Then
					Return
				End If


				'Move the Feedback to the current mouse location
				Dim pnt As IPoint = screenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y)

				If (m_linkFbk IsNot Nothing And pnt IsNot Nothing) Then
					m_linkFbk.MoveTo(pnt)
				End If
			End If

		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Protected Overrides Sub OnMouseUp(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
		Dim abortOperation As Boolean = False
		Dim schematicOperation As ESRI.ArcGIS.Schematic.ISchematicOperation = Nothing

		Try

			If m_dockableDigit Is Nothing Then
				Return
			End If

			If Not arg Is Nothing Then
				m_x = arg.X
				m_y = arg.Y
			End If

			If m_dockableWindow Is Nothing Then
				Return
			End If

			If Not m_dockableWindow.IsVisible() Then
				m_dockableWindow.Show(True)
			End If

			Dim target As ESRI.ArcGIS.SchematicControls.ISchematicTarget

			target = m_schematicExtension

			If Not target Is Nothing Then
				m_schematicLayer = target.SchematicTarget
			End If

			If m_schematicLayer Is Nothing Then
				MsgBox("No target Layer")
				Return
			End If

			Dim inMemoryDiagram As ISchematicInMemoryDiagram
			Dim schematicInMemoryFeatureClass As ISchematicInMemoryFeatureClass
			Dim schematicInMemoryFeatureClassContainer As ISchematicInMemoryFeatureClassContainer

			'Get the point
			Dim point As New ESRI.ArcGIS.Geometry.Point

			Dim mxApp As ESRI.ArcGIS.ArcMapUI.IMxApplication
			Dim appDisplay As ESRI.ArcGIS.Display.IAppDisplay
			Dim screenDisplay As IScreenDisplay
			Dim display As IDisplay
			Dim transform As IDisplayTransformation
			Dim spatialReference As ISpatialReference

			inMemoryDiagram = m_schematicLayer.SchematicInMemoryDiagram
			schematicInMemoryFeatureClassContainer = inMemoryDiagram

			If (schematicInMemoryFeatureClassContainer Is Nothing) Then
				Return
			End If

			mxApp = m_app

			If mxApp Is Nothing Then
				Return
			End If

			appDisplay = mxApp.Display

			If appDisplay Is Nothing Then
				Return
			End If

			screenDisplay = appDisplay.FocusScreen
			display = screenDisplay

			If display Is Nothing Then
				Return
			End If

			transform = display.DisplayTransformation

			If transform Is Nothing Then
				Return
			End If

			spatialReference = transform.SpatialReference

			Dim mapPt As ESRI.ArcGIS.esriSystem.WKSPoint
			Dim devPoint As ESRI.ArcGIS.Display.tagPOINT
			devPoint.x = m_x
			devPoint.y = m_y
			transform.TransformCoords(mapPt, devPoint, 1, 1) 'esriTransformToMap

			point.SpatialReference = spatialReference
			point.Project(spatialReference)
			point.X = mapPt.X
			point.Y = mapPt.Y

			schematicInMemoryFeatureClass = schematicInMemoryFeatureClassContainer.GetSchematicInMemoryFeatureClass(m_dockableDigit.FeatureClass)

			If schematicInMemoryFeatureClass Is Nothing Then
				System.Windows.Forms.MessageBox.Show("Invalid Type.")
				Return
			End If

			If (m_dockableDigit.CreateNode = True) Then
				'TestMandatoryField
				m_dockableDigit.btnOKPanel1.Visible = False

				If m_dockableDigit.ValidateFields() = False Then
					m_dockableDigit.x() = m_x
					m_dockableDigit.y() = m_y
					MsgBox(m_dockableDigit.ErrorProvider1.GetError(m_dockableDigit.btnOKPanel1) & m_messageFromOK)
					Exit Sub
				End If

				Dim geometry As ESRI.ArcGIS.Geometry.IGeometry

				Dim schematicInMemoryFeatureNode As ISchematicInMemoryFeature

				geometry = point

				schematicOperation = New ESRI.ArcGIS.SchematicControls.SchematicOperation

				'digit operation is undo(redo)able we add it in the stack
				Dim doc As IMxDocument = m_app.Document
				Dim operationStack As ESRI.ArcGIS.SystemUI.IOperationStack
				operationStack = doc.OperationStack()
				operationStack.Do(schematicOperation)
				schematicOperation.StartOperation("Digit", m_app, m_schematicLayer, True)

				'do abort operation
				abortOperation = True

				schematicInMemoryFeatureNode = schematicInMemoryFeatureClass.CreateSchematicInMemoryFeatureNode(geometry, "")
				'schematicInMemoryFeatureNode.UpdateStatus = esriSchematicUpdateStatus.esriSchematicUpdateStatusNew if we want the node deleted after update
				schematicInMemoryFeatureNode.UpdateStatus = esriSchematicUpdateStatus.esriSchematicUpdateStatusLocked

				abortOperation = False
				schematicOperation.StopOperation()

				m_dockableDigit.FillValue(schematicInMemoryFeatureNode)

				If (m_dockableDigit.AutoClear()) Then
					m_dockableDigit.SelectionChanged()
				End If

			Else
				m_dockableDigit.btnOKPanel2.Visible = False

				'Get the Tolerance of ArcMap
				Dim tolerance As Double
				Dim mxDocument As IMxDocument = m_app.Document
				Dim point2 As ESRI.ArcGIS.esriSystem.WKSPoint
				Dim devPt As ESRI.ArcGIS.Display.tagPOINT

				tolerance = mxDocument.SearchTolerancePixels
				devPt.x = tolerance
				devPt.y = tolerance

				transform.TransformCoords(point2, devPt, 1, 2) '2 <-> esriTransformSize 4 <-> esriTransformToMap

				tolerance = point2.X * 5 'increase the tolerance value

				Dim schematicFeatures As IEnumSchematicFeature
				schematicFeatures = m_schematicLayer.GetSchematicFeaturesAtPoint(point, tolerance, False, True)

				If Not schematicFeatures.Count > 0 Then
					Return
				End If

				Dim schematicFeatureSelected As ISchematicFeature
				Dim distancetmp As Double
				Dim distance As Double = 0
				schematicFeatures.Reset()


				''schematicFeatureSelected = schematicFeatures.Next

				''pSchematicFeatures may contain several features , we are choosing the closest node.
				Dim schematicFeature2 As ISchematicFeature = schematicFeatures.Next()

				Dim dX As Double
				Dim dY As Double
				Dim schematicInMemoryFeatureNode As ISchematicInMemoryFeatureNode = Nothing

				If schematicFeature2 IsNot Nothing Then
					If schematicFeature2.SchematicElementClass.SchematicElementType = esriSchematicElementType.esriSchematicNodeType Then
						schematicInMemoryFeatureNode = schematicFeature2
					End If

				End If

				Dim schematicInMemoryFeatureNodeGeometry As ISchematicInMemoryFeatureNodeGeometry = schematicInMemoryFeatureNode
				dX = schematicInMemoryFeatureNodeGeometry.Position.X
				dY = schematicInMemoryFeatureNodeGeometry.Position.Y
				schematicFeatureSelected = schematicFeature2
				distance = SquareDistance(dX - point.X, dY - point.Y)

				While (schematicFeature2 IsNot Nothing)

					''find the closest featureNode...
					If schematicInMemoryFeatureNode IsNot Nothing Then

						schematicInMemoryFeatureNodeGeometry = schematicInMemoryFeatureNode

						If schematicInMemoryFeatureNodeGeometry Is Nothing Then
							Continue While
						End If

						dX = schematicInMemoryFeatureNodeGeometry.Position.X
						dY = schematicInMemoryFeatureNodeGeometry.Position.Y
						distancetmp = SquareDistance(dX - point.X, dY - point.Y)

						If (distancetmp < distance) Then
							distance = distancetmp
							schematicFeatureSelected = schematicFeature2
						End If
					End If

					schematicFeature2 = schematicFeatures.Next()

					If schematicFeature2 IsNot Nothing Then
						If schematicFeature2.SchematicElementClass.SchematicElementType = ESRI.ArcGIS.Schematic.esriSchematicElementType.esriSchematicNodeType Then
							schematicInMemoryFeatureNode = schematicFeature2
						End If
					End If

				End While

				If (schematicFeatureSelected Is Nothing) Then
					Exit Sub
				End If

				If (schematicFeatureSelected.SchematicElementClass.SchematicElementType <> esriSchematicElementType.esriSchematicNodeType) Then
					Exit Sub
				End If

				If m_schematicFeature1 Is Nothing Then
					m_schematicFeature1 = schematicFeatureSelected
					m_dockableDigit.SchematicFeature1() = m_schematicFeature1

					If m_dockableDigit.CheckValidFeature(True) <> True Then
						m_schematicFeature1 = Nothing
						m_dockableDigit.SchematicFeature1() = m_schematicFeature1
						Err.Raise(513, "CheckValidFeature", "Invalid starting node for this link type")
					End If

					''Begin Feedback 
					m_linkFbk = New NewLineFeedback
					m_linkFbk.Display() = screenDisplay

					''symbol
					Dim sLnSym As ISimpleLineSymbol
					Dim rGB As IRgbColor

					sLnSym = m_linkFbk.Symbol

					rGB = New RgbColor
					' Make a color
					With rGB
						.Red = 255
						.Green = 0
						.Blue = 0
					End With

					' Setup the symbol with color and style
					sLnSym.Color = rGB

					m_linkFbk.Start(point)
					''End Feedback

					'To know if we are in the same diagram.
					m_schematicLayerForLink = m_schematicLayer

				Else
					If (Not m_schematicLayerForLink Is m_schematicLayer) Then
						MsgBox("wrong Target layer")
						m_schematicLayerForLink = Nothing
						EndFeedBack()
						Exit Sub
					End If
					m_schematicFeature2 = schematicFeatureSelected
					m_dockableDigit.SchematicFeature2() = m_schematicFeature2

					'TestMandatoryField
					If m_dockableDigit.ValidateFields() = False Then
						m_dockableDigit.x() = m_x
						m_dockableDigit.y() = m_y
						MsgBox(m_dockableDigit.ErrorProvider1.GetError(m_dockableDigit.btnOKPanel2) & m_messageFromOK)
						Exit Sub
					End If

					If m_dockableDigit.CheckValidFeature(False) <> True Then
						m_schematicFeature2 = Nothing
						m_dockableDigit.SchematicFeature2() = m_schematicFeature2
						Err.Raise(513, "CheckValidFeature", "Invalid End node for this link type")
					End If

					'CreateLink
					Dim schematicInMemoryFeatureLink As ISchematicInMemoryFeature

					schematicOperation = New ESRI.ArcGIS.SchematicControls.SchematicOperation

					'digit operation is undo(redo)able we add it in the stack
					Dim doc As IMxDocument = m_app.Document
					Dim operationStack As ESRI.ArcGIS.SystemUI.IOperationStack
					operationStack = doc.OperationStack()
					operationStack.Do(schematicOperation)
					schematicOperation.StartOperation("Digit", m_app, m_schematicLayer, True)

					abortOperation = True

					schematicInMemoryFeatureLink = schematicInMemoryFeatureClass.CreateSchematicInMemoryFeatureLink(m_schematicFeature1, m_schematicFeature2, "")
					'schematicInMemoryFeatureLink.UpdateStatus = esriSchematicUpdateStatus.esriSchematicUpdateStatusNew if we want the link deleted after update
					schematicInMemoryFeatureLink.UpdateStatus = esriSchematicUpdateStatus.esriSchematicUpdateStatusLocked

					abortOperation = False
					schematicOperation.StopOperation()

					m_dockableDigit.FillValue(schematicInMemoryFeatureLink)

					'End Feedback
					EndFeedBack()

					m_schematicLayerForLink = Nothing

					If (m_dockableDigit.AutoClear()) Then
						m_dockableDigit.SelectionChanged()
					End If

				End If

			End If

			'Refresh the view and viewer windows
			RefreshView()

			Return

		Catch ex As Exception
			If abortOperation = True And schematicOperation IsNot Nothing Then
				schematicOperation.AbortOperation()
			End If
			EndFeedBack()
			MsgBox(ex.Message)
		End Try

	End Sub

	Private Sub RefreshView()
		Dim map As IMap
		Dim mxDocument2 As IMxDocument = m_app.Document
		map = mxDocument2.FocusMap
		Dim activeView As IActiveView
		activeView = map
		activeView.Refresh()

		'refresh viewer window
		Dim applicationWindows As IApplicationWindows = m_app

		Dim mySet As ISet = applicationWindows.DataWindows

		If mySet IsNot Nothing Then
			mySet.Reset()
			Dim dataWindow As IMapInsetWindow = mySet.Next()
			While dataWindow IsNot Nothing
				dataWindow.Refresh()
				dataWindow = mySet.Next()
			End While
		End If

	End Sub

	Public Sub MyMouseUp(ByVal x As Integer, ByVal y As Integer)
		m_x = x
		m_y = y
		m_messageFromOK = ""
		OnMouseUp(Nothing)
		m_messageFromOK = vbCrLf & "Complete missing data and click on OK button"
	End Sub

    Public Sub EndFeedBack()

        m_schematicFeature1 = Nothing
        m_schematicFeature2 = Nothing

        If m_dockableDigit IsNot Nothing Then
            m_dockableDigit.SchematicFeature1() = m_schematicFeature1
            m_dockableDigit.SchematicFeature2() = m_schematicFeature2
        End If

        If Not m_linkFbk Is Nothing Then
            m_linkFbk.Stop()
            m_linkFbk = Nothing
        End If

    End Sub

    Public WriteOnly Property SchematicFeature1() As ISchematicFeature
        Set(ByVal Value As ISchematicFeature)
            m_schematicFeature1 = Value
        End Set
    End Property

    Public WriteOnly Property SchematicFeature2() As ISchematicFeature
        Set(ByVal Value As ISchematicFeature)
            m_schematicFeature2 = Value
        End Set
    End Property

    Public Property DeactivatedFromDock() As Boolean
        Get
            Return m_DeactivatedFromDock
        End Get
        Set(ByVal value As Boolean)
            m_DeactivatedFromDock = value
        End Set
    End Property

    Public Property FromDeactivate() As Boolean
        Get
            Return m_fromDeactivate
        End Get
        Set(ByVal value As Boolean)
            m_fromDeactivate = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        m_linkFbk = Nothing
        'If (m_dockableWindow IsNot Nothing) Then
        '    m_dockableWindow.Show(False)
        'End If

        MyBase.Finalize()
    End Sub

    Private Function SquareDistance(ByVal dx As Double, ByVal dy As Double) As Double
        Return (dx * dx + dy * dy)
    End Function


    Private Sub SetTargetLayer()
        If m_schematicLayer Is Nothing Then
            Dim extention As ESRI.ArcGIS.esriSystem.IExtension = Nothing
            Dim extensionManager As ESRI.ArcGIS.esriSystem.IExtensionManager

            extensionManager = m_app
            extention = extensionManager.FindExtension("esriSchematicUI.SchematicExtension")

            If extention IsNot Nothing Then

                m_schematicExtension = extention

                Dim target As ISchematicTarget = TryCast(extention, ISchematicTarget)

                If Not target Is Nothing Then
                    m_schematicLayer = target.SchematicTarget
                End If
            End If
        End If
    End Sub

End Class
