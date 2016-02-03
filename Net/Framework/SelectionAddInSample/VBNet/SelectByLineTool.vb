Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem

Imports My

Namespace SelectionSample
  Public Class SelectByLineTool
	  Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
	Private m_isMouseDown As Boolean = False
	Private m_lineFeedback As ESRI.ArcGIS.Display.INewLineFeedback
	Private m_focusMap As IActiveView

	Public Sub New()

	End Sub

	Protected Overrides Sub OnMouseDown(ByVal arg As MouseEventArgs)
      Dim mxDoc As IMxDocument = ArcMap.Document
	  m_focusMap = TryCast(mxDoc.FocusMap, IActiveView)
	  Dim point As IPoint = TryCast(m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y), IPoint)

	  If m_lineFeedback Is Nothing Then
		m_lineFeedback = New ESRI.ArcGIS.Display.NewLineFeedback()
		m_lineFeedback.Display = m_focusMap.ScreenDisplay
		m_lineFeedback.Start(point)
	  Else
		m_lineFeedback.AddPoint(point)
	  End If

	  m_isMouseDown = True
	End Sub

	Protected Overrides Sub OnDoubleClick()
	  m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

	  Dim polyline As IPolyline

	  If m_lineFeedback IsNot Nothing Then
		polyline = m_lineFeedback.Stop()
		If polyline IsNot Nothing Then
		  ArcMap.Document.FocusMap.SelectByShape(polyline, Nothing, False)
		End If
	  End If


	  m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

	  m_lineFeedback = Nothing
	  m_isMouseDown = False
	End Sub

	Protected Overrides Sub OnMouseMove(ByVal arg As MouseEventArgs)
	  If (Not m_isMouseDown) Then
		  Return
	  End If

	  Dim point As IPoint = TryCast(m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y), IPoint)
	  m_lineFeedback.MoveTo(point)
	End Sub

	Protected Overrides Sub OnUpdate()
	  If (Not SelectionExtension.IsExtensionEnabled()) Then
		Me.Enabled = False
		Return
	  End If

	  Me.Enabled = SelectionExtension.HasSelectableLayer()
	End Sub
  End Class
End Namespace
