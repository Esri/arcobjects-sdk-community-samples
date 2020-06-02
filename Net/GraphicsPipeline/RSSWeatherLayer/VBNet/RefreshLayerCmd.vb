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
	''' Refreshes the layer's data (runs the layer update thread)
	''' </summary>     
  <Guid("30FD7DC6-6696-4101-99AD-4C8CDE968E4B"), ProgId("RefreshLayerCmd"), ComVisible(True)> _
  Public Class RefreshLayerCmd : Inherits BaseCommand
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

	'Class members
	Private m_hookHelper As IHookHelper = Nothing

	''' <summary>
	''' CTor
	''' </summary>
		Public Sub New()
			MyBase.m_category = "Weather"
			MyBase.m_caption = "Refresh layer"
			MyBase.m_message = "Refresh the layer"
			MyBase.m_toolTip = "Refresh layer"
			MyBase.m_name = MyBase.m_category & "_" & MyBase.m_caption

			Try
				MyBase.m_bitmap = New Bitmap(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType(), "RefreshLayerCmd.bmp"))
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
        'Instantiate the hook helper
        If m_hookHelper Is Nothing Then
            m_hookHelper = New HookHelperClass()
        End If

        'set the hook
        m_hookHelper.Hook = hook
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Try
            If 0 = m_hookHelper.FocusMap.LayerCount Then
                Return
            End If

            Dim weatherLayer As RSSWeatherLayerClass = Nothing

            'get the weather layer
            Dim layers As IEnumLayer = m_hookHelper.FocusMap.Layers(Nothing, False)
            layers.Reset()
            Dim layer As ILayer = layers.Next()
            Do While Not layer Is Nothing
                If TypeOf layer Is RSSWeatherLayerClass Then
                    weatherLayer = CType(layer, RSSWeatherLayerClass)
                    weatherLayer.Refresh()
                    Return
                End If
                layer = layers.Next()
            Loop
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message)
        End Try
    End Sub

#End Region
  End Class

