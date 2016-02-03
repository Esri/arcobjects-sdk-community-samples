Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

  ''' <summary>
  ''' Summary description for ToggleDynamicDisplayCmd.
  ''' </summary>
  <Guid("290c4325-84b9-49fd-9436-b27c4a4f1de5"), ClassInterface(ClassInterfaceType.None), ProgId("ToggleDynamicDisplayCmd")> _
  Public NotInheritable Class ToggleDynamicDisplayCmd : Inherits BaseCommand
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
	Private m_dynamicMap As IDynamicMap

	Public Sub New()
	  MyBase.m_category = "Toggle dynamic display"
	  MyBase.m_caption = "Toggle dynamic mode"
	  MyBase.m_message = "Toggle dynamic mode on and off"
	  MyBase.m_toolTip = "Toggle dynamic mode"
	  MyBase.m_name = "ToggleDynamicDisplayCmd"

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
	  m_dynamicMap = TryCast(m_hookHelper.FocusMap, IDynamicMap)
	  If m_dynamicMap Is Nothing Then
			Return
	  End If

	  m_dynamicMap.DynamicDrawRate = 15
	  m_dynamicMap.DynamicMapEnabled = Not m_dynamicMap.DynamicMapEnabled
	End Sub

	Public Overrides ReadOnly Property Checked() As Boolean
	  Get
		If m_dynamicMap Is Nothing Then
		  Return False
		End If

		Return m_dynamicMap.DynamicMapEnabled
	  End Get
	End Property

	#End Region
  End Class
