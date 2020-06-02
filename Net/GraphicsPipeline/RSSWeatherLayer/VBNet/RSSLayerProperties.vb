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
Imports System.Messaging
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

  ''' <summary>
  ''' Summary description for RSSLayerProperties.
  ''' </summary>
  <Guid("C8442468-53EE-40d7-A241-896FB8B2E027"), ClassInterface(ClassInterfaceType.None), ProgId("RSSLayerProperties"), ComVisible(True)> _
  Public NotInheritable Class RSSLayerProperties : Inherits BaseCommand
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

	Private m_pHookHelper As IHookHelper

	Public Sub New()
	  MyBase.m_category = "Weather"
	  MyBase.m_caption = "Weather Layer properties"
	  MyBase.m_message = "Show RSS Weather Layer properties"
	  MyBase.m_toolTip = "Show RSS Weather Layer properties"
	  MyBase.m_name = MyBase.m_category & "_" & MyBase.m_caption

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
        If m_pHookHelper Is Nothing Then
            m_pHookHelper = New HookHelperClass()
        End If

        m_pHookHelper.Hook = hook

        ' TODO:  Add RSSLayerProperties.OnCreate implementation
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Try
            'search for the weatherLayer first
            Dim layer As ILayer = Nothing
            Dim RSSLayer As RSSWeatherLayerClass = Nothing

            If m_pHookHelper.FocusMap.LayerCount = 0 Then
                Return
            End If

            Dim layers As IEnumLayer = m_pHookHelper.FocusMap.Layers(Nothing, False)
            layers.Reset()
            layer = layers.Next()
            Do While Not layer Is Nothing
                If TypeOf layer Is RSSWeatherLayerClass Then
                    RSSLayer = CType(layer, RSSWeatherLayerClass)
                    Exit Do
                End If
                layer = layers.Next()
            Loop

            'In case that the weather layer wasn't found,just return
            If Nothing Is RSSLayer Then
                Return
            End If


            'Launch the layer's properties
            Dim typ As Type
            Dim obj As Object
            Dim g As Guid()

            ' METHOD 1: Instantiating a COM object and displaying its property pages
            ' ONLY WORKS ON TRUE COM OBJECTS!  .NET objects that have rolled their own
            ' ISpecifyPropertyPages implementation will error out when you try to cast
            ' the instantiated object to your own ISpecifyPropertyPages implementation.

            ' Get the typeinfo for the ActiveX common dialog control
            typ = Type.GetTypeFromProgID("MSComDlg.CommonDialog")

            ' Create an instance of the control.  We pass it to the property frame function
            ' so the property pages have an object from which to get current settings and apply
            ' new settings.
            obj = Activator.CreateInstance(typ)
            ' This handy function calls IPersistStreamInit->New on COM objects to initialize them
            ActiveXMessageFormatter.InitStreamedObject(obj)

            ' Get the property pages for the control using the direct CAUUID method
            ' This only works for true COM objects and I demonstrate it here only
            ' to show how it is done.  Use the static method
            ' PropertyPage.GetPagesForType() method for real-world use.
            Dim pag As ISpecifyPropertyPages = CType(obj, ISpecifyPropertyPages)
            Dim cau As CAUUID = New CAUUID(0)
            pag.GetPages(cau)
            g = cau.GetPages()

            ' Instantiating a .NET object and displaying its property pages
            ' WORKS ON ALL OBJECTS, .NET or COM    

            ' Create an instance of the .NET control, MyUserControl
            typ = Type.GetTypeFromProgID("PropertySheet")

            ' Retrieve the pages for the control
            g = PropertyPage.GetPagesForType(typ)

            ' Create an instance of the control that we can give to the property pages
            obj = Activator.CreateInstance(typ)

            'add the RSS layer to the property-sheet control
            CType(obj, PropertySheet).RSSWatherLayer = RSSLayer

            ' Display the OLE Property page for the control
            Dim items As Object() = New Object() {obj}

            PropertyPage.CreatePropertyFrame(IntPtr.Zero, 500, 500, "RSS Layer properties", items, g)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message)
        End Try
    End Sub

#End Region
  End Class
