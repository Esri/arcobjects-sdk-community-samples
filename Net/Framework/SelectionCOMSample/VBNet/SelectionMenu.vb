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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses

Namespace SelectionCOMSample
  ''' <summary>
  ''' Summary description for SelectionMenu.
  ''' </summary>
  <Guid("6bafe9c8-72cd-436a-bfdd-d9b8efbaedd9"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.SelectionMenu")> _
  Public NotInheritable Class SelectionMenu
	  Inherits BaseMenu
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

	Public Sub New()
	  AddItem("esriArcMapUI.ClearSelectionCommand")
	  AddItem("esriArcMapUI.SelectAllCommand")
	End Sub

	Public Overrides ReadOnly Property Caption() As String
	  Get
		Return "Selection Tools Menu"
	  End Get
	End Property
	Public Overrides ReadOnly Property Name() As String
	  Get
		Return "ESRI_SelectionCOMSample_SelectionMenu"
	  End Get
	End Property
  End Class
End Namespace