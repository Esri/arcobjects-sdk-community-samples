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
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display

  ''' <summary>
  ''' Summary description for CacheLayerManagerCmd.
  ''' </summary>
  <Guid("a59082a3-7f81-459d-8dd7-af0eb57bf3a8"), ClassInterface(ClassInterfaceType.None), ProgId("DynamicCacheLayerManagerController.CacheLayerManagerCmd")> _
  Public NotInheritable Class CacheLayerManagerCmd : Inherits BaseCommand
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

	Private m_hookHelper As IHookHelper

	Public Sub New()
	  MyBase.m_category = ".NET Samples"
	  MyBase.m_caption = "Dynamic cache layer manager"
	  MyBase.m_message = "Dynamic cache layer manager"
	  MyBase.m_toolTip = "Dynamic cache layer manager"
		MyBase.m_name = "CacheLayerManagerCmd"

	  Try
			Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
			MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
	  Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
	  End Try
	End Sub

	#Region "Overriden Class Methods"

	''' <summary>
	''' Occurs when this command is created
	''' </summary>
	''' <param name="hook">Instance of the application</param>
	Public Overrides Sub OnCreate(ByVal hook As Object)
	  If hook Is Nothing Then
			Return
	  End If

	  If m_hookHelper Is Nothing Then
			m_hookHelper = New HookHelperClass()
	  End If

	  m_hookHelper.Hook = hook
	End Sub

	''' <summary>
	''' Occurs when this command is clicked
	''' </summary>
	Public Overrides Sub OnClick()
	  ' work only in dynamic mode
	  Dim dynamicMap As IDynamicMap = TryCast(m_hookHelper.FocusMap, IDynamicMap)
	  If dynamicMap Is Nothing OrElse (Not dynamicMap.DynamicMapEnabled) Then
			MessageBox.Show("Please enable dynamic mode and try again.")
			Return
	  End If

	  Dim cacheMgrDlg As CacheManagerDlg = New CacheManagerDlg(m_hookHelper)

	  cacheMgrDlg.ShowDialog()
	End Sub

	#End Region
  End Class

