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
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

  ''' <summary>
  ''' Summary description for RemoveLayerCmd.
  ''' </summary>
  <Guid("d848a775-dc85-4ee3-88a6-a780572139a2"), ClassInterface(ClassInterfaceType.None), ProgId("MapControlSaveLayerFile.RemoveLayerCmd")> _
  Public NotInheritable Class RemoveLayerCmd : Inherits BaseCommand
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
	  MyBase.m_caption = "Remove Layer"
	  MyBase.m_message = "Remove Layer"
	  MyBase.m_toolTip = "Remove Layer"
	  MyBase.m_name = "MapControlSaveLayerFile_RemoveLayerCmd"

	  Try
		'
		' TODO: change bitmap name if necessary
		'
		Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
		MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
	  Catch ex As Exception
		System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
	  End Try
	End Sub

#Region "Overridden Class Methods"

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

        ' TODO:  Add other initialization code
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        'need to get the layer from the custom-property of the map
        If Nothing Is m_hookHelper Then
            Return
        End If

        'get the mapControl hook
        Dim hook As Object = Nothing
        If TypeOf m_hookHelper.Hook Is IToolbarControl2 Then
            hook = (CType(m_hookHelper.Hook, IToolbarControl2)).Buddy
        Else
            hook = m_hookHelper.Hook
        End If

        'get the custom property from which is supposed to be the layer to be saved
        Dim customProperty As Object = Nothing
        Dim mapControl As IMapControl3 = Nothing
        If TypeOf hook Is IMapControl3 Then
            mapControl = CType(hook, IMapControl3)
            customProperty = mapControl.CustomProperty
        Else
            Return
        End If

        If Nothing Is customProperty OrElse Not (TypeOf customProperty Is ILayer) Then
            Return
        End If

        m_hookHelper.FocusMap.DeleteLayer(CType(customProperty, ILayer))
    End Sub

#End Region
  End Class
