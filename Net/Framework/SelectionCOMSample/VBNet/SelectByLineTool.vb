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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry

Namespace SelectionCOMSample
  ''' <summary>
  ''' Summary description for SelectByLineTool.
  ''' </summary>
  <Guid("15de72ff-f31f-4655-98b6-191b7348375a"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.SelectByLineTool")> _
  Public NotInheritable Class SelectByLineTool
	  Inherits BaseTool
	#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisible(False)> _
	Private Shared Sub RegisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryRegistration(registerType)

	  '
	  ' TODO: Add any COM registration code here
	  '
	End Sub

	<ComUnregisterFunction(), ComVisible(False)> _
	Private Shared Sub UnregisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryUnregistration(registerType)

	  '
	  ' TODO: Add any COM unregistration code here
	  '
	End Sub

	#Region "ArcGIS Component Category Registrar generated code"
	''' <summary>
	''' Required method for ArcGIS Component Category registration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  MxCommands.Register(regKey)

	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  MxCommands.Unregister(regKey)

	End Sub

	#End Region
	#End Region

	Private m_isMouseDown As Boolean = False
	Private m_lineFeedback As ESRI.ArcGIS.Display.INewLineFeedback
	Private m_mainExtension As SelectionExtension
	Private m_doc As IMxDocument

	Public Sub New()
	  MyBase.m_category = "Developer Samples"
      MyBase.m_caption = "Select ByLine Tool VB.NET."
      MyBase.m_message = "Select by line tool VB.NET."
      MyBase.m_toolTip = "Select by line tool VB.NET." & Constants.vbCrLf & "Selection Sample Extension needs to be turned on in Extensions dialog."
	  MyBase.m_name = "ESRI_SelectionCOMSample_SelectByLineTool"
	  Try
        MyBase.m_bitmap = New Bitmap(Me.GetType().Assembly.GetManifestResourceStream("SelectByLine.png"))
        MyBase.m_cursor = New System.Windows.Forms.Cursor(Me.GetType().Assembly.GetManifestResourceStream("SelectByLine.cur"))
	  Catch ex As Exception
		System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
	  End Try
	  m_mainExtension = SelectionExtension.GetExtension()
	End Sub

#Region "Overridden Class Methods"

        ''' <summary>
        ''' Occurs when this tool is created
        ''' </summary>
        ''' <param name="hook">Instance of the application</param>
        Public Overrides Sub OnCreate(ByVal hook As Object)
            Dim application As IApplication = TryCast(hook, IApplication)
            m_doc = TryCast(application.Document, IMxDocument)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If
        End Sub

        ''' <summary>
        ''' Occurs when this tool is clicked
        ''' </summary>
        Public Overrides Sub OnClick()
        End Sub

        Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
            Dim activeView As IActiveView = TryCast(m_doc.FocusMap, IActiveView)
            Dim point As IPoint = TryCast(activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y), IPoint)

            If m_lineFeedback Is Nothing Then
                m_lineFeedback = New ESRI.ArcGIS.Display.NewLineFeedback()
                m_lineFeedback.Display = activeView.ScreenDisplay
                m_lineFeedback.Start(point)
            Else
                m_lineFeedback.AddPoint(point)
            End If

            m_isMouseDown = True
        End Sub

        Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
            If (Not m_isMouseDown) Then
                Return
            End If

            Dim activeView As IActiveView = TryCast(m_doc.FocusMap, IActiveView)

            Dim point As IPoint = TryCast(activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y), IPoint)
            m_lineFeedback.MoveTo(point)
        End Sub


        Public Overrides Sub OnDblClick()
            Dim activeView As IActiveView = TryCast(m_doc.FocusMap, IActiveView)

            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

            Dim polyline As IPolyline

            If m_lineFeedback IsNot Nothing Then
                polyline = m_lineFeedback.Stop()
                If polyline IsNot Nothing Then
                    m_doc.FocusMap.SelectByShape(polyline, Nothing, False)
                End If
            End If


            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

            m_lineFeedback = Nothing
            m_isMouseDown = False
        End Sub

        Public Overrides ReadOnly Property Enabled() As Boolean
            Get
                If m_mainExtension Is Nothing Then
                    Return False
                Else
                    Return m_mainExtension.HasSelectableLayer() AndAlso m_mainExtension.IsExtensionEnabled
                End If
            End Get
        End Property
#End Region
  End Class
End Namespace
