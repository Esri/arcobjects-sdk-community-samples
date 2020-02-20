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
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports System.Windows.Forms

  ''' <summary>
  ''' Summary description for AnimatedZoomInTool.
  ''' </summary>
  <Guid("3d5a0143-757b-4069-b4e7-973d24793923"), ClassInterface(ClassInterfaceType.None), ProgId("AnimatedZoomInTool")> _
  Public NotInheritable Class AnimatedZoomInTool : Inherits BaseTool
	#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisible(False)> _
	Private Shared Sub RegisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryRegistration(registerType)
	End Sub

	<ComUnregisterFunction(), ComVisible(False)> _
	Private Shared Sub UnregisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryUnregistration(registerType)
	End Sub

	#Region "ArcGIS Component Category Registrar generated code"
	''' <summary>
	''' Required method for ArcGIS Component Category registration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  ControlsCommands.Register(regKey)
	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  ControlsCommands.Unregister(regKey)
	End Sub

	#End Region
	#End Region

	#Region "class members"
	Private m_hookHelper As IHookHelper = Nothing

	Private m_bIsAnimating As Boolean = False
	Private m_bZoomOut As Boolean = False
	Private m_dStepCount As Double = 0
	Private m_nTotalSteps As Integer = 0

	Private m_Center As IPoint = New PointClass()

	Private m_wksStep As WKSEnvelope = New WKSEnvelope()

	Private m_dynamicMapEvents As IDynamicMapEvents_Event = Nothing

	Private Const c_dMinimumDelta As Double = 0.01
	Private Const c_dSmoothFactor As Double = 200000.0
	Private Const c_dMinimumSmoothZoom As Double = 0.1
	#End Region

	Public Sub New()
	  MyBase.m_category = ".NET Samples"
	  MyBase.m_caption = "Animated Zoom In"
	  MyBase.m_message = "Zoom in with animation"
	  MyBase.m_toolTip = "Animated Zoom In"
	  MyBase.m_name = "AnimatedZoomInTool"
	  Try
			Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
			MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
			MyBase.m_cursor = New System.Windows.Forms.Cursor(Me.GetType(), Me.GetType().Name & ".cur")
	  Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
	  End Try
	End Sub

#Region "Overridden Class Methods"

    ''' <summary>
    ''' Occurs when this tool is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Nothing Is hook Then
            Return
        End If

        Try
            m_hookHelper = New HookHelperClass()
            m_hookHelper.Hook = hook
            If Nothing Is m_hookHelper.ActiveView Then
                m_hookHelper = Nothing
            End If
        Catch
            m_hookHelper = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' The enabled state of this command, determines whether the command is usable.
    ''' </summary>
    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If Nothing Is m_hookHelper Then
                Return False
            End If

            Dim dynamicMap As IDynamicMap = TryCast(m_hookHelper.FocusMap, IDynamicMap)
            Dim bIsDynamicMapEnabled As Boolean = dynamicMap.DynamicMapEnabled
            If False = bIsDynamicMapEnabled Then
                m_bIsAnimating = False
                m_dStepCount = 0
                m_nTotalSteps = 0
                m_dynamicMapEvents = Nothing
            End If
            Return bIsDynamicMapEnabled
        End Get
    End Property

    ''' <summary>
    ''' Occurs when this tool is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Dim dynamicMap As IDynamicMap = TryCast(m_hookHelper.FocusMap, IDynamicMap)
        If False = dynamicMap.DynamicMapEnabled Then
            Return
        End If

        m_dynamicMapEvents = Nothing
        m_dynamicMapEvents = TryCast(m_hookHelper.FocusMap, IDynamicMapEvents_Event)
        AddHandler m_dynamicMapEvents.DynamicMapStarted, AddressOf DynamicMapEvents_DynamicMapStarted

        m_bIsAnimating = False
        m_dStepCount = 0
        m_nTotalSteps = 0
    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ' Zoom on the focus map based on user drawn rectangle
        m_bZoomOut = Shift = 1

        Dim activeView As IActiveView = TryCast(m_hookHelper.FocusMap, IActiveView)
        Dim rubberBand As IRubberBand = New RubberEnvelopeClass()
        ' This method intercepts the Mouse events from here
        Dim zoomBounds As IEnvelope = TryCast(rubberBand.TrackNew(activeView.ScreenDisplay, Nothing), IEnvelope)
        If Nothing Is zoomBounds Then
            Return
        End If

        Dim wksZoomBounds As WKSEnvelope
        zoomBounds.QueryWKSCoords(wksZoomBounds)

        Dim fittedBounds As IEnvelope = activeView.ScreenDisplay.DisplayTransformation.FittedBounds
        Dim wksFittedBounds As WKSEnvelope
        fittedBounds.QueryWKSCoords(wksFittedBounds)

        If True = m_bZoomOut Then
            Dim dXScale As Double = fittedBounds.Width * fittedBounds.Width / zoomBounds.Width
            Dim dYScale As Double = fittedBounds.Height * fittedBounds.Height / zoomBounds.Height

            wksZoomBounds.XMin = fittedBounds.XMin - dXScale
            wksZoomBounds.YMin = fittedBounds.YMin - dYScale
            wksZoomBounds.XMax = fittedBounds.XMax + dXScale
            wksZoomBounds.YMax = fittedBounds.YMax + dYScale
        End If

        m_wksStep.XMax = 1
        m_wksStep.YMax = 1
        m_wksStep.XMin = 1
        m_wksStep.YMin = 1
        m_nTotalSteps = 0

        ' Calculate how fast the zoom will go by changing the step size
        Do While (System.Math.Abs(m_wksStep.XMax) > c_dMinimumDelta) OrElse (System.Math.Abs(m_wksStep.YMax) > c_dMinimumDelta) OrElse (System.Math.Abs(m_wksStep.XMin) > c_dMinimumDelta) OrElse (System.Math.Abs(m_wksStep.YMin) > c_dMinimumDelta)
            m_nTotalSteps += 1

            ' calculate the step size
            ' step size is the difference between the zoom bounds and the fitted bounds
            m_wksStep.XMin = (wksZoomBounds.XMin - wksFittedBounds.XMin) / m_nTotalSteps
            m_wksStep.YMin = (wksZoomBounds.YMin - wksFittedBounds.YMin) / m_nTotalSteps
            m_wksStep.XMax = (wksZoomBounds.XMax - wksFittedBounds.XMax) / m_nTotalSteps
            m_wksStep.YMax = (wksZoomBounds.YMax - wksFittedBounds.YMax) / m_nTotalSteps
        Loop

        m_bIsAnimating = True
        m_dStepCount = 0
    End Sub

    Public Overrides Function Deactivate() As Boolean
        m_bIsAnimating = False
        m_dStepCount = 0
        m_nTotalSteps = 0

        If Nothing Is m_hookHelper Then
            Return False
        End If

        Dim dynamicMap As IDynamicMap = TryCast(m_hookHelper.FocusMap, IDynamicMap)
        If False = dynamicMap.DynamicMapEnabled Then
            Return True
        End If

        m_dynamicMapEvents = TryCast(m_hookHelper.FocusMap, IDynamicMapEvents_Event)
        RemoveHandler m_dynamicMapEvents.DynamicMapStarted, AddressOf DynamicMapEvents_DynamicMapStarted

        Return True
    End Function
#End Region

	#Region "Dynamic Map Events"
	Private Sub DynamicMapEvents_DynamicMapStarted(ByVal Display As IDisplay, ByVal dynamicDisplay As IDynamicDisplay)
	  If False = m_bIsAnimating Then
			m_dStepCount = 0
			m_nTotalSteps = 0
			Return
	  End If

	  If m_dStepCount >= m_nTotalSteps Then
			m_bIsAnimating = False
			m_dStepCount = 0
			m_nTotalSteps = 0
			Return
	  End If

	  ' Increase the bounds by the step amount
	  Dim activeView As IActiveView = TryCast(m_hookHelper.FocusMap, IActiveView)
	  Dim newVisibleBounds As IEnvelope = activeView.ScreenDisplay.DisplayTransformation.FittedBounds

	  ' Smooth the zooming.  Faster at higher scales, slower at lower
	  Dim dSmoothZooom As Double = activeView.FocusMap.MapScale / c_dSmoothFactor
	  If dSmoothZooom < c_dMinimumSmoothZoom Then
			dSmoothZooom = c_dMinimumSmoothZoom
	  End If

	  newVisibleBounds.XMin = newVisibleBounds.XMin + (m_wksStep.XMin * dSmoothZooom)
	  newVisibleBounds.YMin = newVisibleBounds.YMin + (m_wksStep.YMin * dSmoothZooom)
	  newVisibleBounds.XMax = newVisibleBounds.XMax + (m_wksStep.XMax * dSmoothZooom)
	  newVisibleBounds.YMax = newVisibleBounds.YMax + (m_wksStep.YMax * dSmoothZooom)

	  activeView.ScreenDisplay.DisplayTransformation.VisibleBounds = newVisibleBounds

	  m_dStepCount = m_dStepCount + dSmoothZooom
	End Sub
	#End Region
  End Class
