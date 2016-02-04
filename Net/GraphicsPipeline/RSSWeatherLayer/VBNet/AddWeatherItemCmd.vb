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
Imports ESRI.ArcGIS.Geometry


	''' <summary>
	''' Add a new weather item given a zipCode.
	''' </summary>
  ''' <remarks>Should the weather item exist, it will be updated</remarks>
	<ClassInterface(ClassInterfaceType.None), Guid("D19CA1E0-FC77-4d2a-8FAA-EC74683FA991"), ProgId("AddWeatherItemCmd"), ComVisible(True)> _
	Public NotInheritable Class AddWeatherItemCmd : Inherits BaseCommand
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
	  MxCommands.Register(regKey)
	  ControlsCommands.Register(regKey)
		End Sub
		''' <summary>
		''' Required method for ArcGIS Component Category unregistration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  MxCommands.Unregister(regKey)
	  ControlsCommands.Unregister(regKey)
		End Sub

		#End Region
		#End Region

	'class members
	Private m_hookHelper As IHookHelper = Nothing
		Private m_weatherLayer As RSSWeatherLayerClass = Nothing

	''' <summary>
	''' CTor
	''' </summary>
		Public Sub New()
	  MyBase.m_category = "Weather"
			MyBase.m_caption = "Add Weather item by zipcode"
			MyBase.m_message = "Add weather item by zipcode"
			MyBase.m_toolTip = "Add by zipcode"
			MyBase.m_name = MyBase.m_category & "_" & MyBase.m_caption

			Try
		MyBase.m_bitmap = New Bitmap(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType(), "AddWeatherItemCmd.bmp"))
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

            'get the weather layer
            Dim layers As IEnumLayer = m_hookHelper.FocusMap.Layers(Nothing, False)
            layers.Reset()
            Dim layer As ILayer = layers.Next()
            Do While Not layer Is Nothing
                If TypeOf layer Is RSSWeatherLayerClass Then
                    m_weatherLayer = CType(layer, RSSWeatherLayerClass)
                    Exit Do
                End If
                layer = layers.Next()
            Loop

            'in case that the layer exists
            If Not Nothing Is m_weatherLayer Then
                'launch the zipCode input dialog
                Dim dlg As ZipCodeDlg = New ZipCodeDlg()
                If System.Windows.Forms.DialogResult.OK = dlg.ShowDialog() Then
                    Dim zipCode As Long = dlg.ZipCode
                    If 0 <> zipCode Then
                        'add the weather item to the layer
                        m_weatherLayer.AddItem(zipCode)

                        'if the user checked the 'ZoomTo' checkbox, zoom to the item
                        If dlg.ZoomToItem Then
                            m_weatherLayer.ZoomTo(zipCode)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message)
        End Try
    End Sub

#End Region
	End Class

