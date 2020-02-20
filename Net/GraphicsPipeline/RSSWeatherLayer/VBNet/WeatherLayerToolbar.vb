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
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.SystemUI
Imports System
Imports System.Runtime.InteropServices

  ''' <summary>
  ''' The layer's command items toolbar
  ''' </summary>
  <Guid("653D29A8-10A4-44b8-9140-86170B715931"), ClassInterface(ClassInterfaceType.None), ProgId("WeatherLayerToolbar"), ComVisible(True)> _
  Public Class WeatherLayerToolbar : Implements IToolBarDef
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
	  MxCommandBars.Register(regKey)
	  ControlsToolbars.Register(regKey)
	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  MxCommandBars.Unregister(regKey)
	  ControlsToolbars.Unregister(regKey)
	End Sub

	#End Region
	#End Region

	Private Structure ToolDef
	  Public itemDef As String
	  Public group As Boolean
	  Public Sub New(ByVal itd As String, ByVal grp As Boolean)
		itemDef = itd
		group = grp
	  End Sub
	End Structure

	Private m_toolDefs As ToolDef() = { New ToolDef("AddRSSWeatherLayer", False), New ToolDef("SelectByCityName", False), New ToolDef("AddWeatherItemCmd", False), New ToolDef("AddWeatherItemTool", False), New ToolDef("RefreshLayerCmd", False) }

	Public Sub New()
	End Sub


	#Region "IToolBarDef Implementations"
	Public Sub GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements IToolBarDef.GetItemInfo
	  itemDef.ID = m_toolDefs(pos).itemDef
	  itemDef.Group = m_toolDefs(pos).group
	End Sub

	Public ReadOnly Property Caption() As String Implements IToolBarDef.Caption
	  Get
		Return "RSS Weather layer"
	  End Get
	End Property

	Public ReadOnly Property Name() As String Implements IToolBarDef.Name
	  Get
		Return "WeatherLayerToolbar"
	  End Get
	End Property

	Public ReadOnly Property ItemCount() As Integer Implements IToolBarDef.ItemCount
	  Get
		Return m_toolDefs.Length
	  End Get
	End Property
	#End Region

  End Class