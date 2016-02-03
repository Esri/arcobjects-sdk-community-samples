Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses

Namespace SelectionCOMSample
  ''' <summary>
  ''' Summary description for SelectionToolbar.
  ''' </summary>
  <Guid("680d2553-a6a7-4fbe-88e6-f16650276126"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.SelectionToolbar")> _
  Public NotInheritable Class SelectionToolbar
	  Inherits BaseToolbar
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
	  AddItem("SelectionCOMSample.SelectionTargetComboBox")
	  BeginGroup()
	  AddItem("SelectionCOMSample.SelectionZoomToLayerMenu")
	  BeginGroup()
	  AddItem("{26cd6ddc-c2d0-4102-a853-5f7043c6e797}") 'Toggle dockwin button
	  AddItem("{15de72ff-f31f-4655-98b6-191b7348375a}") 'Select by line tool
	  AddItem("SelectionCOMSample.SelectionToolPalette")
	  BeginGroup()
	  AddItem("SelectionCOMSample.SelectionMenu")
	End Sub

	Public Overrides ReadOnly Property Caption() As String
	  Get
		Return "Selection COM Toolbar"
	  End Get
	End Property
	Public Overrides ReadOnly Property Name() As String
	  Get
		Return "ESRI_SelectionCOMSample_SelectionToolbar"
	  End Get
	End Property
  End Class
End Namespace