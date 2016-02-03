Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Geometry


Namespace MultipleGlobeViewers
	Public Class MultipleGlobeViewers
		Inherits ESRI.ArcGIS.Desktop.AddIns.Button
		#Region "Member Variables"

		Private globe As ESRI.ArcGIS.GlobeCore.IGlobe
		Private globeDisplay As ESRI.ArcGIS.GlobeCore.IGlobeDisplay
		Private globeCamera As ESRI.ArcGIS.GlobeCore.IGlobeCamera
		Private globeDispEvent As ESRI.ArcGIS.GlobeCore.IGlobeDisplayEvents_Event
		Private theForm As SecondaryViewerForm
		Private viewer As ESRI.ArcGIS.Analyst3D.ISceneViewer
		Private viewerGlobeCamera As ESRI.ArcGIS.GlobeCore.IGlobeCamera
		Private topDownView As Boolean = False
		Private viewerCaption As String = ""

		#End Region

		#Region "DLLImportFunction"

		<DllImport("gdi32.dll")> _
		Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
		End Function

		<DllImport("user32.dll")> _
		Shared Function ShowWindow(ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
		End Function


		#End Region

		Public Sub New()
			globe = ArcGlobe.Globe
			globeDisplay = globe.GlobeDisplay
			globeCamera = CType(globeDisplay.ActiveViewer.Camera, ESRI.ArcGIS.GlobeCore.IGlobeCamera)
			globeDispEvent = CType(globeDisplay, ESRI.ArcGIS.GlobeCore.IGlobeDisplayEvents_Event)
		End Sub

		Protected Overrides Sub OnClick()
			theForm = New SecondaryViewerForm()
			'register form's events
			AddHandler theForm.topDownButton.Click, AddressOf topDownButton_Click
			AddHandler theForm.normalButton.Click, AddressOf normalButton_Click
			AddHandler theForm.FormClosing, AddressOf theForm_FormClosing
			'get viewer list
			getListOfSecondaryViewers()
			'register the ArcGlobe globe's display afterdraw event
			AddHandler globeDispEvent.AfterDraw, AddressOf globeDispEvent_AfterDraw

			theForm.Show()
		End Sub

		Protected Overrides Sub OnUpdate()
			Enabled = ArcGlobe.Application IsNot Nothing
		End Sub

		#Region "Custom Methods"

		Private Sub globeDispEvent_AfterDraw(ByVal pViewer As ISceneViewer)
			Dim obsLat As Double
			Dim obsLon As Double
			Dim obsAlt As Double
			Dim tarLat As Double
			Dim tarLon As Double
			Dim tarAlt As Double
			globeCamera.GetObserverLatLonAlt(obsLat, obsLon, obsAlt)
			globeCamera.GetTargetLatLonAlt(tarLat, tarLon, tarAlt)
			'set the observer and target of the secondary viewer to be the same as main viewer if top-down view = false
			If topDownView = False OrElse globeCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationGlobal Then
				viewerGlobeCamera.OrientationMode = globeCamera.OrientationMode
				viewerGlobeCamera.SetObserverLatLonAlt(obsLat, obsLon, obsAlt)
				viewerGlobeCamera.SetTargetLatLonAlt(tarLat, tarLon, tarAlt)
			'set the observer top down view for the secondary viewer
			ElseIf topDownView = True AndAlso globeCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal Then
				viewerGlobeCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal
				tarLat = obsLat + 0.0000001
				tarLon = obsLon + 0.0000001
				viewerGlobeCamera.SetTargetLatLonAlt(tarLat, tarLon, tarAlt)
			End If
		End Sub

		Private Sub getListOfSecondaryViewers()
			Dim viewers As ESRI.ArcGIS.esriSystem.IArray = globeDisplay.GetAllViewers()
			If viewers.Count < 2 Then
				Return
			End If
			For i As Integer = 0 To viewers.Count - 1
				Dim viewerElement As ESRI.ArcGIS.Analyst3D.ISceneViewer = CType(viewers.Element(i), ESRI.ArcGIS.Analyst3D.ISceneViewer)
				If viewerElement.Caption <> "Globe view" Then
					theForm.viewerListBox.Items.Add(viewerElement.Caption)
				End If
			Next i

		End Sub

		Private Sub normalButton_Click(ByVal sender As Object, ByVal e As EventArgs)
			topDownView = False
			viewerCaption = theForm.viewerListBox.SelectedItem.ToString()
			viewer = CType(globeDisplay.FindViewer(viewerCaption), ESRI.ArcGIS.Analyst3D.ISceneViewer)
			viewerGlobeCamera = CType(viewer.Camera, ESRI.ArcGIS.GlobeCore.IGlobeCamera)
		End Sub

		Private Sub topDownButton_Click(ByVal sender As Object, ByVal e As EventArgs)
			topDownView = True
			viewerCaption = theForm.viewerListBox.SelectedItem.ToString()
			viewer = CType(globeDisplay.FindViewer(viewerCaption), ESRI.ArcGIS.Analyst3D.ISceneViewer)
			viewerGlobeCamera = CType(viewer.Camera, ESRI.ArcGIS.GlobeCore.IGlobeCamera)
		End Sub

		Private Sub theForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
			'unregister the ArcGlobe globe's display afterdraw event
			RemoveHandler globeDispEvent.AfterDraw, AddressOf globeDispEvent_AfterDraw
		End Sub
		#End Region
	End Class
End Namespace
