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
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(RoutingCommand.ClassId, RoutingCommand.InterfaceId, RoutingCommand.EventsId), _
 ProgId("RoutingSample.RoutingCommand")> _
Public NotInheritable Class RoutingCommand
	Inherits BaseCommand

#Region "COM GUIDs"
	' These  GUIDs provide the COM identity for this class 
	' and its COM interfaces. If you change them, existing 
	' clients will no longer be able to access the class.
	Public Const ClassId As String = "e05e77c1-4761-4395-8d62-bf5f2b69f96b"
	Public Const InterfaceId As String = "f0fe9da7-d112-4bdc-827a-9cdfbd2aa4a4"
	Public Const EventsId As String = "4876c7c9-ff09-4f51-bb0c-a3731b90ee86"
#End Region

#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub RegisterFunction(ByVal registerType As Type)
		' Required for ArcGIS Component Category Registrar support
		ArcGISCategoryRegistration(registerType)
	End Sub

	<ComUnregisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub UnregisterFunction(ByVal registerType As Type)
		' Required for ArcGIS Component Category Registrar support
		ArcGISCategoryUnregistration(registerType)
	End Sub

#Region "ArcGIS Component Category Registrar generated code"
	Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		MxCommands.Register(regKey)
	End Sub

	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		MxCommands.Unregister(regKey)
	End Sub

#End Region
#End Region

#Region "Private members"

	' ArcGIS application
	Private m_application As IMxApplication
	' Routing form
	Private m_dlgRouting As RoutingForm

#End Region

#Region "Public methods and properties"

	' A creatable COM class must have a Public Sub New() 
	' with no parameters, otherwise, the class will not be 
	' registered in the COM registry and cannot be created 
	' via CreateObject.
	Public Sub New()
		MyBase.New()

		MyBase.m_category = "Developer Samples"
		MyBase.m_caption = "Routing Sample"
		MyBase.m_message = "Routing Sample in VB.NET. Click for Route finding."
		MyBase.m_toolTip = "Routing Sample in VB.NET"
		MyBase.m_name = "RoutingSampleVBNetCommand"

		Try
			Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
			MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
		Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
		End Try
	End Sub

	Public Overrides ReadOnly Property Checked() As Boolean
		Get
			' Checked if Routing Form is Visible
			If m_dlgRouting Is Nothing Then _
				Return False

			Return m_dlgRouting.Visible
		End Get
	End Property

	Public Overrides Sub OnCreate(ByVal hook As Object)
		If Not (hook Is Nothing) Then
			If TypeOf (hook) Is IMxApplication Then
				m_application = CType(hook, IMxApplication)
			End If
		End If
	End Sub

	Public Overrides Sub OnClick()
		If m_dlgRouting Is Nothing Then
			' Create Routing Form
			m_dlgRouting = New RoutingForm
			m_dlgRouting.Init(m_application)

			' show form
			m_dlgRouting.Show()

			' Set ArcMap window as owner for Routing Form
			SetWindowLong(m_dlgRouting.Handle.ToInt32, GWL_HWNDPARENT, m_application.hWnd)
		Else
			' just show/hide form
			If m_dlgRouting.Visible Then
				m_dlgRouting.Hide()
			Else
				m_dlgRouting.Show()
			End If
		End If
	End Sub

#End Region

#Region "Imported functions"

	' Needed to show non-modal Routing form
	Public Declare Ansi Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
	Public Const GWL_HWNDPARENT As Integer = (-8)

#End Region

End Class



