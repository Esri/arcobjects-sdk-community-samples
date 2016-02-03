Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

  ''' <summary>
  ''' Summary description for LoadDynamicLayer.
  ''' </summary>
  <Guid("7f55d7ab-54bf-4887-80e4-8e62436e12b8"), ClassInterface(ClassInterfaceType.None), ProgId("LoadDynamicLayerCmd")> _
  Public NotInheritable Class LoadDynamicLayerCmd : Inherits BaseCommand
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
	  MyBase.m_caption = "Add dynamic layer"
	  MyBase.m_message = "Add dynamic layer"
	  MyBase.m_toolTip = "Add dynamic layer"
	  MyBase.m_name = "LoadDynamicLayerCmd"

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
	  Dim dynamicMap As IDynamicMap = TryCast(m_hookHelper.FocusMap, IDynamicMap)
	  If dynamicMap Is Nothing OrElse dynamicMap.DynamicMapEnabled = False Then
			Return
	  End If

	  Dim dynamicLayer As MyDynamicLayer = New MyDynamicLayer()
	  m_hookHelper.FocusMap.AddLayer(dynamicLayer)
	End Sub

	#End Region
  End Class

