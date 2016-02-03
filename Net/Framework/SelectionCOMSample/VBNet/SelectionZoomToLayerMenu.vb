Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses

Namespace SelectionCOMSample
  ''' <summary>
  ''' Summary description for SelectionZoomToLayerMenu.
  ''' </summary>
  <Guid("b722a7bf-53ef-4a12-bf38-afe31e7bc521"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.SelectionZoomToLayerMenu")> _
  Public NotInheritable Class SelectionZoomToLayerMenu
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
	  AddItem("SelectionCOMSample.ZoomToLayerMultiItem")
	End Sub

	Public Overrides ReadOnly Property Caption() As String
	  Get
		Return "Zoom To Layer"
	  End Get
	End Property
	Public Overrides ReadOnly Property Name() As String
	  Get
		Return "ESRI_SelectionCOMSample_ZoomToLayerMenu"
	  End Get
	End Property
  End Class
End Namespace