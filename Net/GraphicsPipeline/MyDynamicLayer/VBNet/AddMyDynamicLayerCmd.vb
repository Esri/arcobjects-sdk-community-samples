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
Imports System.Data
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry

  ''' <summary>
  ''' Summary description for AddMyDynamicLayerCmd.
  ''' </summary>
  <Guid("0ddfd2b0-45ba-416a-93c8-c7db41de30e4"), ClassInterface(ClassInterfaceType.None), ComVisible(True), ProgId("AddMyDynamicLayerCmd")> _
  Public NotInheritable Class AddMyDynamicLayerCmd : Inherits BaseCommand
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

	Private m_hookHelper As IHookHelper = Nothing
	Private m_dynamicLayer As MyDynamicLayerClass = Nothing
	Private m_bIsConnected As Boolean = False
	Private m_bOnce As Boolean = True

	Public Sub New()
	  MyBase.m_category = ".NET Samples"
	  MyBase.m_caption = "Add Dynamic Layer"
	  MyBase.m_message = "Add Dynamic Layer"
	  MyBase.m_toolTip = "Add Dynamic Layer"
	  MyBase.m_name = "AddMyDynamicLayerCmd"

	  Try
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
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        'make sure to switch into dynamic mode
        Dim dynamicMap As IDynamicMap = TryCast(m_hookHelper.FocusMap, IDynamicMap)
        'make sure that the current Map supports the DynamicDisplay
        If Nothing Is dynamicMap Then
            Return
        End If

        If (Not m_bIsConnected) Then
            'make sure to switch into dynamic mode
            If (Not dynamicMap.DynamicMapEnabled) Then
                dynamicMap.DynamicMapEnabled = True
            End If

            'do some initializations...
            If m_bOnce Then
                'initialize the dynamic layer
                m_dynamicLayer = New MyDynamicLayerClass()
                m_dynamicLayer.Name = "Dynamic Layer"

                m_bOnce = False
            End If

            'make sure that the layer did not get added to the map        
            Dim bLayerHasBeenAdded As Boolean = False
            If m_hookHelper.FocusMap.LayerCount > 0 Then
                Dim layers As IEnumLayer = m_hookHelper.FocusMap.Layers(Nothing, False)
                layers.Reset()
                Dim layer As ILayer = layers.Next()
                Do While Not layer Is Nothing
                    If TypeOf layer Is MyDynamicLayerClass Then
                        bLayerHasBeenAdded = True
                        Exit Do
                    End If
                    layer = layers.Next()
                Loop
            End If

            If (Not bLayerHasBeenAdded) Then
                'add the dynamic layer to the map
                m_hookHelper.FocusMap.AddLayer(m_dynamicLayer)
            End If
            'connect to the synthetic data source
            m_dynamicLayer.Connect()
        Else
            'disconnect to the synthetic data source
            m_dynamicLayer.Disconnect()

            'delete the layer from the map
            m_hookHelper.FocusMap.DeleteLayer(m_dynamicLayer)

            'disable the dynamic display
            If dynamicMap.DynamicMapEnabled Then
                dynamicMap.DynamicMapEnabled = False
            End If
        End If

        'set the connected flag
        m_bIsConnected = Not m_bIsConnected
    End Sub

    ''' <summary>
    ''' set the state of the button of the command
    ''' </summary>
    Public Overrides ReadOnly Property Checked() As Boolean
        Get
            Return m_bIsConnected
        End Get
    End Property

#End Region

  End Class

