Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI

Namespace SelectionCOMSample
  <Guid("c1537917-5ca0-4637-9728-a76b70517545"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.SelectionExtension")> _
  Public Class SelectionExtension
	  Implements IExtension, IExtensionConfig
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
	  MxExtension.Register(regKey)

	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  MxExtension.Unregister(regKey)

	End Sub

	#End Region
	#End Region
	Private m_application As IApplication
	Private m_enableState As esriExtensionState
	Private Shared s_dockWindow As IDockableWindow
	Private Shared s_extension As SelectionExtension
	Private m_hasSelectableLayer As Boolean
	Private m_map As IMap
	Private m_doc As IMxDocument

	Public Sub New()
	  s_extension = Me
	End Sub
	#Region "IExtension Members"

	''' <summary>
	''' Name of extension. Do not exceed 31 characters
	''' </summary>
	Public ReadOnly Property Name() As String Implements IExtension.Name
	  Get
		Return "ESRI_SelectionCOMSample_SelectionExtension"
	  End Get
	End Property

	Public Sub Shutdown() Implements IExtension.Shutdown
	  m_application = Nothing
	End Sub

	Public Sub Startup(ByRef initializationData As Object) Implements IExtension.Startup
	  m_application = TryCast(initializationData, IApplication)
	  If m_application Is Nothing Then
		Return
	  End If

	  m_doc = TryCast(m_application.Document, IMxDocument)

	  'Get dockable window.
	  Dim dockableWindowManager As IDockableWindowManager = TryCast(m_application, IDockableWindowManager)
	  Dim dockWinID As UID = New UIDClass()
	  dockWinID.Value = "SelectionCOMSample.SelectionCountDockWin"
	  s_dockWindow = dockableWindowManager.GetDockableWindow(dockWinID)

	  'Wire up events.
	  Dim docEvents As IDocumentEvents_Event = TryCast(m_doc, IDocumentEvents_Event)
	  AddHandler docEvents.NewDocument, AddressOf ArcMap_NewOpenDocument
	  AddHandler docEvents.OpenDocument, AddressOf ArcMap_NewOpenDocument

	End Sub

	#End Region

	Friend ReadOnly Property GetSelectionCountWindow() As IDockableWindow
	  Get
		Return s_dockWindow
	  End Get
	End Property

	Friend Shared Function GetExtension() As SelectionExtension
	  Return s_extension
	End Function

	Private Sub ArcMap_NewOpenDocument()
	  Dim pageLayoutEvent As IActiveViewEvents_Event = TryCast(m_doc.PageLayout, IActiveViewEvents_Event)
	  AddHandler pageLayoutEvent.FocusMapChanged, AddressOf AVEvents_FocusMapChanged

	  Initialize()
	End Sub

	Private Sub Initialize()
	  'Reset event handlers.
	  Dim avEvent As IActiveViewEvents_Event = TryCast(m_doc.FocusMap, IActiveViewEvents_Event)
	  AddHandler avEvent.ItemAdded, AddressOf AvEvent_ItemAdded
	  AddHandler avEvent.ItemDeleted, AddressOf AvEvent_ItemAdded
	  AddHandler avEvent.SelectionChanged, AddressOf UpdateSelCountDockWin
	  AddHandler avEvent.ContentsChanged, AddressOf avEvent_ContentsChanged
	  'Update the UI.
	  m_map = m_doc.FocusMap
	  FillComboBox()
	  UpdateSelCountDockWin()
	  m_hasSelectableLayer = CheckForSelectableLayer()
	End Sub

	Private Sub avEvent_ContentsChanged()
	  m_hasSelectableLayer = CheckForSelectableLayer()
	End Sub

	Private Sub AvEvent_ItemAdded(ByVal Item As Object)
	  m_map = m_doc.FocusMap
	  FillComboBox()
	  UpdateSelCountDockWin()
	  m_hasSelectableLayer = CheckForSelectableLayer()
	End Sub

	Private Sub AVEvents_FocusMapChanged()
	  Initialize()
	End Sub

	Private Sub UpdateSelCountDockWin()
	  ' Update the contents of the lsitView, when the selection changes in the map. 
	  Dim featureLayer As IFeatureLayer
	  Dim featSel As IFeatureSelection

	  SelectionCountDockWin.Clear()

	  ' Loop through the layers in the map and add the layer's name and
	  ' selection count to the list box
	  For i As Integer = 0 To m_map.LayerCount - 1
		If TypeOf m_map.Layer(i) Is IFeatureSelection Then
		  featureLayer = TryCast(m_map.Layer(i), IFeatureLayer)
		  If featureLayer Is Nothing Then
			Exit For
		  End If

		  featSel = TryCast(featureLayer, IFeatureSelection)

		  Dim count As Integer = 0
		  If featSel.SelectionSet IsNot Nothing Then
			count = featSel.SelectionSet.Count
		  End If
		  SelectionCountDockWin.AddItem(featureLayer.Name, count)
		End If
	  Next i
	End Sub

	Private Sub FillComboBox()
	  Dim selCombo As SelectionTargetComboBox = SelectionTargetComboBox.GetSelectionComboBox()
	  If selCombo Is Nothing Then
		Return
	  End If

	  selCombo.ClearAll()

	  Dim featureLayer As IFeatureLayer
	  ' Loop through the layers in the map and add the layer's name to the combo box.
	  For i As Integer = 0 To m_map.LayerCount - 1
		If TypeOf m_map.Layer(i) Is IFeatureSelection Then
		  featureLayer = TryCast(m_map.Layer(i), IFeatureLayer)
		  If featureLayer Is Nothing Then
			Exit For
		  End If

		  selCombo.AddItem(featureLayer.Name, featureLayer)
		End If
	  Next i

	End Sub

	Private Function CheckForSelectableLayer() As Boolean
	  Dim map As IMap = m_doc.FocusMap
	  ' Bail if map has no layers
	  If map.LayerCount = 0 Then
		Return False
	  End If

	  ' Fetch all the feature layers in the focus map
	  ' and see if at least one is selectable
	  Dim uid As New UIDClass()
	  uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"
	  Dim enumLayers As IEnumLayer = map.Layers(uid, True)
	  Dim featureLayer As IFeatureLayer = TryCast(enumLayers.Next(), IFeatureLayer)
	  Do While featureLayer IsNot Nothing
		If featureLayer.Selectable = True Then
		  Return True
		End If
		featureLayer = TryCast(enumLayers.Next(), IFeatureLayer)
	  Loop
	  Return False
	End Function
	Friend ReadOnly Property IsExtensionEnabled() As Boolean
	  Get
		Return Me.State = esriExtensionState.esriESEnabled
	  End Get
	End Property

	Friend Function HasSelectableLayer() As Boolean
	  Return m_hasSelectableLayer
	End Function


	#Region "IExtensionConfig Members"

	Public ReadOnly Property Description() As String Implements IExtensionConfig.Description
	  Get
        Return "SelectionExtension" & Constants.vbCrLf & "Copyright © ESRI 2009" & Constants.vbCrLf & Constants.vbCrLf & "This extension is a selection sample extension in VB.NET."
	  End Get
	End Property

	''' <summary>
	''' Friendly name shown in the Extension dialog
	''' </summary>
	Public ReadOnly Property ProductName() As String Implements IExtensionConfig.ProductName
	  Get
		Return "Selection Sample Extension"
	  End Get
	End Property

	Public Property State() As esriExtensionState Implements IExtensionConfig.State
	  Get
		Return m_enableState
	  End Get
	  Set(ByVal value As esriExtensionState)
		If m_enableState <> 0 AndAlso value = m_enableState Then
		  Return
		End If

		'Check if ok to enable or disable extension
		Dim requestState As esriExtensionState = value
		If requestState = esriExtensionState.esriESEnabled Then
		  'Cannot enable if it's already in unavailable state
		  If m_enableState = esriExtensionState.esriESUnavailable Then
			Throw New COMException("Cannot enable extension")
		  End If

		  'Determine if state can be changed
		  Dim checkState As esriExtensionState = StateCheck(True)
		  m_enableState = checkState
		ElseIf requestState = 0 OrElse requestState = esriExtensionState.esriESDisabled Then
		  'Determine if state can be changed
		  Dim checkState As esriExtensionState = StateCheck(False)
		  If checkState <> m_enableState Then
			m_enableState = checkState
		  End If
		End If

	  End Set
	End Property

	#End Region

	''' <summary>
	''' Determine extension state 
	''' </summary>
	''' <param name="requestEnable">true if to enable; false to disable</param>
	Private Function StateCheck(ByVal requestEnable As Boolean) As esriExtensionState
	  'TODO: Replace with advanced extension state checking if needed
	  'Turn on or off extension directly
	  If requestEnable Then
		Return esriExtensionState.esriESEnabled
	  Else
		Return esriExtensionState.esriESDisabled
	  End If
	End Function
  End Class
End Namespace