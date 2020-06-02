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
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI

Namespace GlobeFlyTool
	Public Class Fly
		Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
		#Region "DllImport"

		<DllImport("user32")> _
		Public Shared Function SetCursor(ByVal hCursor As Integer) As Integer
		End Function
		<DllImport("user32")> _
		Public Shared Function GetClientRect(ByVal hwnd As Integer, ByRef lpRect As Rectangle) As Integer
		End Function
		<DllImport("user32")> _
		Shared Function GetCursorPos(ByRef lpPoint As System.Drawing.Point) As Boolean
		End Function
		<DllImport("user32")> _
		Public Shared Function GetWindowRect(ByVal hwnd As Integer, ByRef lpRect As Rectangle) As Integer
		End Function

		#End Region

		#Region "Member Variables"

		Private globe As IGlobe
		Private globeDisplay As IGlobeDisplay
		Private globeCamera As IGlobeCamera
		Private camera As ICamera
		Private scene As IScene
		Private inUse As Boolean
		Private bCancel As Boolean = False
		Private orbitalFly As Boolean = False
		Private mouseX As Long
		Private mouseY As Long
		Private motion As Double = 2 'speed of the scene fly through in scene units
		Private distance As Double 'distance between target and observer
		Private currentElevation As Double 'normal fly angles in radians
		Private currentAzimut As Double 'normal fly angles in radians
		Private speed As Integer
		Private flyCur As System.Windows.Forms.Cursor
		Private moveFlyCur As System.Windows.Forms.Cursor
		Private theClock As Microsoft.VisualBasic.Devices.Clock
		Private lastClock As Long
		Private observer As GlobeFlyTool.PointZ
		Private target As GlobeFlyTool.PointZ
		Private viewVec As GlobeFlyTool.PointZ

		#End Region

		#Region "Constructor/Destructor"

		Public Sub New()
			globe = ArcGlobe.Globe
			scene = TryCast(globe, IScene)
			globeDisplay = globe.GlobeDisplay
			camera = globeDisplay.ActiveViewer.Camera
			globeCamera = TryCast(camera, IGlobeCamera)
			theClock = New Microsoft.VisualBasic.Devices.Clock()
			flyCur = New System.Windows.Forms.Cursor(Me.GetType().Assembly.GetManifestResourceStream("Fly.cur"))
			moveFlyCur = New System.Windows.Forms.Cursor(Me.GetType().Assembly.GetManifestResourceStream("fly1.cur"))
			speed = 0
		End Sub

		Protected Overrides Sub Finalize()
			flyCur = Nothing
			moveFlyCur = Nothing
		End Sub

		#End Region

		Protected Overrides Sub OnUpdate()
			Enabled = ArcGlobe.Application IsNot Nothing

			If inUse Then
				Cursor = moveFlyCur
			Else
				Cursor = flyCur
			End If
		End Sub

		#Region "Tool overrides"

		Protected Overrides Sub OnMouseUp(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
			If arg.Button = MouseButtons.Left OrElse arg.Button = MouseButtons.Right Then
				If (Not inUse) Then
					mouseX = arg.X
					mouseY = arg.Y

					If speed = 0 Then
						StartFlight(arg.X, arg.Y)
					End If
				Else
					'Set the speed
					If arg.Button = MouseButtons.Left Then
						speed = speed + 1
					ElseIf arg.Button = MouseButtons.Right Then
						speed = speed - 1
					End If
				End If
			Else
				'EndFlight();
				inUse = False
				bCancel = True
			End If
		End Sub

		Protected Overrides Sub OnMouseMove(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
			If (Not inUse) Then
				Return
			End If

			mouseX = arg.X
			mouseY = arg.Y
		End Sub

		Protected Overrides Sub OnKeyUp(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.KeyEventArgs)
			If inUse = True Then
				'Slow down the speed of the fly through
				If arg.KeyCode = Keys.Down OrElse arg.KeyCode = Keys.Left Then
					motion = motion / 2
				'Speed up the speed of the fly through
				ElseIf arg.KeyCode = Keys.Up OrElse arg.KeyCode = Keys.Right Then
					motion = motion * 2
				ElseIf arg.KeyCode = Keys.Escape Then
					bCancel = True
				End If

			End If
		End Sub

		Protected Overrides Sub OnKeyDown(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.KeyEventArgs)
			If arg.KeyCode = Keys.Escape Then 'ESC is pressed
				bCancel = True
			End If
		End Sub

		#End Region

		#Region "Fly Functions"

		Public Sub StartFlight(ByVal x As Double, ByVal y As Double)
			inUse = True

			globeDisplay.IsNavigating = True
			Dim camOrientMode As ESRI.ArcGIS.GlobeCore.esriGlobeCameraOrientationMode = globeCamera.OrientationMode

			orbitalFly = If((camOrientMode = ESRI.ArcGIS.GlobeCore.esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal), True, False)

			Dim pObs As IPoint = camera.Observer
			Dim pTar As IPoint = camera.Target

			observer = New GlobeFlyTool.PointZ(pObs.X, pObs.Y, pObs.Z)
			target = New GlobeFlyTool.PointZ(pTar.X, pTar.Y, pTar.Z)

			viewVec = target - observer
			distance = viewVec.Norm()

			'avoid center of globe
			If target.Norm() < 0.25 Then
				target = target + viewVec
				distance = distance * 2
			End If

			currentElevation = Math.Atan(viewVec.z / Math.Sqrt((viewVec.x * viewVec.x) + (viewVec.y + viewVec.y)))
			currentAzimut = Math.Atan2(viewVec.y, viewVec.x) '2.26892;//

			'Windows API call to get windows client coordinates
			Dim pt As New System.Drawing.Point()
			Dim ans As Boolean = GetCursorPos(pt)
			Dim rect As New Rectangle()
			If GetWindowRect(globeDisplay.ActiveViewer.hWnd, rect) = 0 Then
				Return
			End If

			mouseX = pt.X - rect.Left
			mouseY = pt.Y - rect.Top

			If (Not orbitalFly) Then
				globeCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationGlobal
			Else
				globeCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal
			End If
			globeCamera.NavigationType = esriGlobeNavigationType.esriGlobeNavigationFree
			globeCamera.RollFactor = 1.0

			globeDisplay.IsNavigating = True
			globeDisplay.IsNavigating = False
			globeDisplay.IsNavigating = True

			lastClock = theClock.TickCount

			'Windows API call to set cursor
			SetCursor(moveFlyCur.Handle.ToInt32())
			'Continue the flight
			Flight()
		End Sub

		Public Sub Flight()
			'speed in scene units
			Dim motionUnit As Double = (0.000001 + Math.Abs(observer.Norm() - 1.0) / 200.0) * motion
			'Get IMessageDispatcher interface
			Dim pMessageDispatcher As IMessageDispatcher
			pMessageDispatcher = New MessageDispatcherClass()

			'Set the ESC key to be seen as a cancel action
			pMessageDispatcher.CancelOnClick = False
			pMessageDispatcher.CancelOnEscPress = True
			bCancel = False
			Do
				'Get the elapsed time
				Dim currentClock As Long = theClock.TickCount
				Dim lastFrameDuration As Double = CDbl(currentClock - lastClock) / 1000
				lastClock = currentClock

				If lastFrameDuration < 0.01 Then
					lastFrameDuration = 0.01
				End If

				If lastFrameDuration > 1 Then
					lastFrameDuration = 0.1
				End If

				System.Diagnostics.Debug.Print(lastFrameDuration.ToString())

				'Windows API call to get windows client coordinates
				Dim rect As New Rectangle()
				If GetClientRect(globeDisplay.ActiveViewer.hWnd, rect) = 0 Then
					Return
				End If

				'Get normal vectors
				Dim dXMouseNormal, dYMouseNormal As Double

				dXMouseNormal = 2 * (CDbl(mouseX) / CDbl(rect.Right - rect.Left)) - 1
				dYMouseNormal = 2 * (CDbl(mouseY) / CDbl(rect.Bottom - rect.Top)) - 1

				Dim dir As PointZ = Me.RotateNormal(lastFrameDuration, dXMouseNormal, dYMouseNormal)

				Dim visTarget As New PointZ(observer.x + distance * dir.x, observer.y + distance * dir.y, observer.z + distance * dir.z)
				target.x = visTarget.x
				target.y = visTarget.y
				target.z = visTarget.z

				If speed <> 0 Then
					Dim speedFactor As Integer = If((speed > 0), (1 << speed), -(1 << (-speed)))

					'Move the camera in the viewing directions
					observer.x = observer.x + (lastFrameDuration * (2 Xor speedFactor) * motionUnit * dir.x)
					observer.y = observer.y + (lastFrameDuration * (2 Xor speedFactor) * motionUnit * dir.y)
					observer.z = observer.z + (lastFrameDuration * (2 Xor speedFactor) * motionUnit * dir.z)
					target.x = target.x + (lastFrameDuration * (2 Xor speedFactor) * motionUnit * dir.x)
					target.y = target.y + (lastFrameDuration * (2 Xor speedFactor) * motionUnit * dir.y)
					target.z = target.z + (lastFrameDuration * (2 Xor speedFactor) * motionUnit * dir.z)
				End If

				Dim globeViewUtil As ESRI.ArcGIS.GlobeCore.IGlobeViewUtil = TryCast(globeCamera, ESRI.ArcGIS.GlobeCore.IGlobeViewUtil)
				Dim obsLat As Double
				Dim obsLon As Double
				Dim obsAlt As Double
				Dim tarLat As Double
				Dim tarLon As Double
				Dim tarAlt As Double

				globeViewUtil.GeocentricToGeographic(observer.x, observer.y, observer.z, obsLon, obsLat, obsAlt)
				globeViewUtil.GeocentricToGeographic(target.x, target.y, target.z, tarLon, tarLat, tarAlt)
				globeCamera.SetObserverLatLonAlt(obsLat, obsLon, obsAlt / 1000)
				globeCamera.SetTargetLatLonAlt(tarLat, tarLon, tarAlt / 1000)

				globeCamera.SetAccurateViewDirection(target.x - observer.x, target.y - observer.y, target.z - observer.z)

				Dim rollAngle As Double = 0
				If speed > 0 Then
					rollAngle = 10 * dXMouseNormal * Math.Abs(dXMouseNormal)
				End If
				camera.RollAngle = rollAngle

				'Redraw the scene viewer 
				globeDisplay.RefreshViewers()

				'Dispatch any waiting messages: OnMouseMove / OnMouseUp / OnKeyUp events
				Dim objCancel As Object = TryCast(bCancel, Object)
				pMessageDispatcher.Dispatch(globeDisplay.ActiveViewer.hWnd, False, objCancel)

				'End flight if ESC key pressed
				If bCancel = True Then
					EndFlight()
				End If

			Loop While inUse = True AndAlso bCancel = False

			bCancel = False
		End Sub

		Public Sub EndFlight()
			inUse = False
			bCancel = True
			speed = 0
			globeDisplay.IsNavigating = False

			' reposition target
			Dim currentObs As New PointZ()
			Dim newTarget As IPoint = New PointClass()
			currentObs.x = camera.Observer.X
			currentObs.y = camera.Observer.Y
			currentObs.z = camera.Observer.Z

			Dim orX As Integer = 0
			Dim orY As Integer = 0
			Dim width As Integer = 0
			Dim height As Integer = 0
			camera.GetViewport(orX, orY, width, height)

			Dim obj1 As Object
			Dim obj2 As Object
			Try
				globeDisplay.Locate(globeDisplay.ActiveViewer, width \ 2, height \ 2, True, True, newTarget, obj1, obj2)
			Catch e As System.Exception
				MessageBox.Show(e.Message)
				MessageBox.Show(e.StackTrace.ToString())
			End Try

			If newTarget Is Nothing Then ' no intersection with globe, but don't let the target to be too far
				newTarget = camera.Target
				Dim tar As New PointZ(currentObs.x, currentObs.y, currentObs.z)

				Dim elevObs As Double = tar.Norm() - 1.0
				If elevObs <= 0.0001 Then
					elevObs = 0.0001
				End If

				Dim oldTarget As New PointZ(newTarget.X, newTarget.Y, newTarget.Z)
				Dim dir As PointZ = (oldTarget - tar)
				Dim val As Double = dir.Norm()
				If val > 0.0 Then
					dir.x = dir.x * elevObs * 10 / val
					dir.y = dir.y * elevObs * 10 / val
					dir.z = dir.z * elevObs * 10 / val
				End If

				tar = tar + dir
				newTarget.X = tar.x
				newTarget.Y = tar.y
				newTarget.Z = tar.z
			End If

			Dim globeViewUtil As ESRI.ArcGIS.GlobeCore.IGlobeViewUtil = TryCast(globeCamera, ESRI.ArcGIS.GlobeCore.IGlobeViewUtil)
			Dim obsLat As Double
			Dim obsLon As Double
			Dim obsAlt As Double
			Dim tarLat As Double
			Dim tarLon As Double
			Dim tarAlt As Double
			globeViewUtil.GeocentricToGeographic(currentObs.x, currentObs.y, currentObs.z, obsLon, obsLat, obsAlt)
			globeViewUtil.GeocentricToGeographic(newTarget.X, newTarget.Y, newTarget.Z, tarLon, tarLat, tarAlt)
			globeCamera.SetObserverLatLonAlt(obsLat, obsLon, obsAlt / 1000)
			globeCamera.SetTargetLatLonAlt(tarLat, tarLon, tarAlt / 1000)
			camera.RollAngle = 0
			camera.PropertiesChanged()
			globeDisplay.RefreshViewers()

			'Windows API call to set cursor
			SetCursor(moveFlyCur.Handle.ToInt32())
		End Sub

		Public Function RotateNormal(ByVal lastFrameDuration As Double, ByVal mouseXNorm As Double, ByVal mouseYNorm As Double) As PointZ
			currentElevation = currentElevation - (lastFrameDuration * mouseYNorm * (Math.Abs(mouseYNorm)))
			currentAzimut = currentAzimut - (lastFrameDuration * mouseXNorm * (Math.Abs(mouseXNorm)))

			If currentElevation > 0.45 * 3.141592 Then
				currentElevation = 0.45 * 3.141592
			End If
			If currentElevation < -0.45 * 3.141592 Then
				currentElevation = -0.45 * 3.141592
			End If
			Do While currentAzimut < 0
				currentAzimut = currentAzimut + (2 * 3.141592)
			Loop
			Do While currentAzimut > 2 * 3.141592
				currentAzimut = currentAzimut - (2 * 3.141592)
			Loop

			Dim x As Double = Math.Cos(currentElevation) * Math.Cos(currentAzimut)
			Dim y As Double = Math.Cos(currentElevation) * Math.Sin(currentAzimut)
			Dim z As Double = Math.Sin(currentElevation)

			Dim p As GlobeFlyTool.PointZ = New PointZ(x, y, z)
			Return p
		End Function

		#End Region
	End Class

End Namespace
