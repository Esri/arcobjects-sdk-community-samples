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
Imports ESRI.ArcGIS.ADF.CATIDs

Imports System.Runtime.InteropServices

<ComClass(RoutingToolbar.ClassId, RoutingToolbar.InterfaceId, RoutingToolbar.EventsId), _
 ProgId("RoutingSample.RoutingToolbar")> _
Public Class RoutingToolbar
	Implements ESRI.ArcGIS.SystemUI.IToolBarDef

#Region "COM GUIDs"
	' These  GUIDs provide the COM identity for this class 
	' and its COM interfaces. If you change them, existing 
	' clients will no longer be able to access the class.
	Public Const ClassId As String = "6052656e-3835-4713-81d0-a5ca5ff4c3ef"
	Public Const InterfaceId As String = "12a04071-5783-4fd4-9782-c6898dbd47b6"
	Public Const EventsId As String = "6ba5b87e-0930-4840-85f1-3214f3f808e9"
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
	''' <summary>
	''' Required method for ArcGIS Component Category registration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		MxCommandBars.Register(regKey)

	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		MxCommandBars.Unregister(regKey)
	End Sub

#End Region
#End Region

#Region "Public Methods and Properties"

	' A creatable COM class must have a Public Sub New() 
	' with no parameters, otherwise, the class will not be 
	' registered in the COM registry and cannot be created 
	' via CreateObject.
	Public Sub New()
		MyBase.New()
	End Sub

	Public ReadOnly Property Caption() As String Implements ESRI.ArcGIS.SystemUI.IToolBarDef.Caption
		Get
			Return "Routing Sample"
		End Get
	End Property

	Public Sub GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements ESRI.ArcGIS.SystemUI.IToolBarDef.GetItemInfo
		If pos = 0 Then
			itemDef.ID = "RoutingSample.RoutingCommand"
			itemDef.Group = False
		End If
	End Sub

	Public ReadOnly Property ItemCount() As Integer Implements ESRI.ArcGIS.SystemUI.IToolBarDef.ItemCount
		Get
			Return 1
		End Get
	End Property

	Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.SystemUI.IToolBarDef.Name
		Get
			Return "RoutingSampleVBNetCommand"
		End Get
	End Property

#End Region

End Class


