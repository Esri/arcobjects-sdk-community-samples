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
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Analyst3D


	Public Partial Class GlobeView : Inherits Window
    Private _map As Map
		Public globeControl As AxGlobeControl
    Private mapPath As String = "..\..\..\..\..\data\Globe\"

    Public Property SelectedMap() As Map
      Get
        Return _map
      End Get
      Set(ByVal value As Map)
        _map = value
      End Set
    End Property

		Private Sub WindowLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      CreateEngineControls()
		End Sub

    ' Create ArcGIS Engine Controls and set them to be child of each WindowsFormsHost elements
    Private Sub CreateEngineControls()
      'set Engine controls to the child of each hosts 
      globeControl = New AxGlobeControl()
      mapHost.Child = globeControl

      'set Engine controls properties
      globeControl.BackColor = System.Drawing.Color.Black
          AddHandler globeControl.OnMouseMove, AddressOf globeControl_OnMouseMove
      'style
      globeControl.BorderStyle = 0
      ' set default tool
      Navigate(Nothing, Nothing)
      ' listen to events
      Dim glbDisplay As GlobeDisplay = TryCast(globeControl.GlobeDisplay, GlobeDisplay)
          AddHandler glbDisplay.AfterDraw, AddressOf glbDisplay_AfterDraw
    End Sub

    Private Sub glbDisplay_AfterDraw(ByVal pViewer As ISceneViewer)
      ' load 3dd files
      If globeControl.DocumentFilename Is Nothing Then
        globeControl.Load3dFile(mapPath & "\" & _map.MapName & ".3dd")
      End If
    End Sub

    Private Sub globeControl_OnMouseMove(ByVal sender As Object, ByVal e As IGlobeControlEvents_OnMouseMoveEvent)
      'get scale range in Kilometers
      Dim globeCamera As IGlobeViewUtil = TryCast(globeControl.GlobeCamera, IGlobeViewUtil)
      Dim distanceInKM As Double = globeCamera.ScalingDistance / 1000
      Coordinates.Text = String.Format("{0} {1} {2}", "Distance: ", distanceInKM.ToString("######.##"), globeControl.Globe.GlobeUnits.ToString().Substring(4))
    End Sub

		Private Sub Navigate(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim command As ICommand = New ControlsGlobeNavigateTool()
			command.OnCreate(globeControl.Object)
			globeControl.CurrentTool = CType(command, ITool)
		End Sub

    Private Sub Fly(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim command As ICommand = New ControlsGlobeFlyTool()
      command.OnCreate(globeControl.Object)
      globeControl.CurrentTool = CType(command, ITool)
    End Sub

    Private Sub FullExtent(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim command As ICommand = New ControlsGlobeFullExtentCommand()
      command.OnCreate(globeControl.Object)
      command.OnClick()
    End Sub

		Private Sub SpinLeft(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim command As ICommand = New ControlsGlobeSpinClockwiseCommand()
			command.OnCreate(globeControl.Object)
			command.OnClick()
		End Sub

		Private Sub SpinRight(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim command As ICommand = New ControlsGlobeSpinCounterClockwiseCommand()
			command.OnCreate(globeControl.Object)
			command.OnClick()
		End Sub

		Private Sub SpinStop(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim command As ICommand = New ControlsGlobeSpinStopCommand()
			command.OnCreate(globeControl.Object)
			command.OnClick()
		End Sub

    Private Sub Window_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
      'Important: must release control and other related com objects.
      'otherwise application may not shut down properly.
      globeControl.Dispose()
      ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
    End Sub
	End Class
