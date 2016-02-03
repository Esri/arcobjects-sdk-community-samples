Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.NetworkAnalystUI
Imports SubsetNetworkEvaluators

Namespace SubsetNetworkEvaluatorsUI
	''' <summary>
	''' This command toggles on and off the event based behavior to update the relevant subset parameter arrays
	''' automatically by listening to selection change events and graphic element change events.  When the command
	''' is toggled off, the subset parameter arrays are cleared out and the results may not match the current selection
    ''' or graphic element geometries in this case.
	''' </summary>
	<Guid("f213e01f-3a45-44c7-a350-397a794e9084"), ClassInterface(ClassInterfaceType.None), ProgId("SubsetNetworkEvaluatorsUI.AutoUpdateNetworkElementArrayParametersCommand")> _
	Public NotInheritable Class AutoUpdateNetworkElementArrayParametersCommand : Inherits BaseCommand
#Region "COM Registration Function(s)"
		<ComRegisterFunction(), ComVisible(False)> _
		Private Shared Sub RegisterFunction(ByVal registerType As Type)
			' Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType)

			'
			' TODO: Add any COM registration code here
			''
		End Sub

		<ComUnregisterFunction(), ComVisible(False)> _
		Private Shared Sub UnregisterFunction(ByVal registerType As Type)
			' Required for ArcGIS Component Category Registrar support
			ArcGISCategoryUnregistration(registerType)

			'
			' TODO: Add any COM unregistration code here
			''
		End Sub

#Region "ArcGIS Component Category Registrar generated code"
		''' <summary>
		''' Required method for ArcGIS Component Category registration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			MxCommands.Register(regKey)

		End Sub
		''' <summary>
		''' Required method for ArcGIS Component Category unregistration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			MxCommands.Unregister(regKey)

		End Sub

#End Region
#End Region

		Private m_application As IApplication = Nothing
		Private m_nax As INetworkAnalystExtension = Nothing

		Private m_naWindowEventSource As INAWindow = Nothing
		Private m_mapEventSource As IMap = Nothing
		Private m_graphicsEventSource As IGraphicsContainer = Nothing

		Private m_ActiveAnalysisChanged As ESRI.ArcGIS.NetworkAnalystUI.INAWindowEvents_OnActiveAnalysisChangedEventHandler
		Private m_ActiveViewEventsSelectionChanged As ESRI.ArcGIS.Carto.IActiveViewEvents_SelectionChangedEventHandler
		Private m_AllGraphicsDeleted As ESRI.ArcGIS.Carto.IGraphicsContainerEvents_AllElementsDeletedEventHandler
		Private m_GraphicAdded As ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementAddedEventHandler
		Private m_GraphicDeleted As ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementDeletedEventHandler
		Private m_GraphicsAdded As ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementsAddedEventHandler
		Private m_GraphicUpdated As ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementUpdatedEventHandler

		Public Sub New()
			MyBase.m_category = "Network Analyst Samples" 'localizable text
			MyBase.m_caption = "Auto Update Network Element Array Parameters" 'localizable text
			MyBase.m_message = "Auto Update Network Element Array Parameters" 'localizable text
			MyBase.m_toolTip = "Auto Update Network Element Array Parameters" 'localizable text
			MyBase.m_name = "NASamples_AutoUpdateNetworkElementArrayParameters"	'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

			Try
				'
				' TODO: change bitmap name if necessary
				'
				Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
				'MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)

				Dim pAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetAssembly(Me.GetType())
				Dim pStream As System.IO.Stream = pAssembly.GetManifestResourceStream(bitmapResourceName)
				MyBase.m_bitmap = CType(Image.FromStream(pStream), Bitmap)
			Catch ex As Exception
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
			End Try
		End Sub

#Region "Overridden Class Methods"

		''' <summary>
		''' Occurs when this command is created
		''' </summary>
		''' <param name="hook">Instance of the application</param>
		Public Overloads Overrides Sub OnCreate(ByVal hook As Object)
			If hook Is Nothing Then
				Return
			End If

			m_application = TryCast(hook, IApplication)

			m_nax = Nothing
			m_naWindowEventSource = Nothing
			m_mapEventSource = Nothing
			m_graphicsEventSource = Nothing

			m_nax = TryCast(SubsetHelperUI.GetNAXConfiguration(m_application), INetworkAnalystExtension)
		End Sub

		Public Overloads Overrides ReadOnly Property Enabled() As Boolean
			Get
				Dim naxEnabled As Boolean = False
				Dim naxConfig As IExtensionConfig = TryCast(m_nax, IExtensionConfig)
				If Not naxConfig Is Nothing Then
					naxEnabled = naxConfig.State = esriExtensionState.esriESEnabled
				End If

				Dim naLayer As INALayer = Nothing
				Dim nds As INetworkDataset = Nothing

				If naxEnabled Then
					Dim naWindow As INAWindow = m_nax.NAWindow
					naLayer = naWindow.ActiveAnalysis

					Dim naContext As INAContext = Nothing
					If Not naLayer Is Nothing Then
						naContext = naLayer.Context
					End If

					If Not naContext Is Nothing Then
						nds = naContext.NetworkDataset
					End If
				End If

				Dim enable As Boolean = naxEnabled AndAlso (Not naLayer Is Nothing) AndAlso (Not nds Is Nothing)
				If (Not enable) AndAlso Not m_naWindowEventSource Is Nothing Then
					UnWireEvents()
				End If

				MyBase.m_enabled = enable
				Return MyBase.Enabled
			End Get
		End Property

		''' <summary>
		''' Occurs when this command is clicked
		''' </summary>
		Public Overloads Overrides Sub OnClick()
			If Not m_naWindowEventSource Is Nothing Then
				UnWireEvents()
			Else
				WireEvents()
			End If
		End Sub

		Public Overloads Overrides ReadOnly Property Checked() As Boolean
			Get
				Return (Not m_naWindowEventSource Is Nothing)
			End Get
		End Property

		Private Sub WireEvents()
			Try
				If Not m_naWindowEventSource Is Nothing Then
					UnWireEvents()
				End If

				If (Not m_nax Is Nothing) Then
					m_naWindowEventSource = TryCast((m_nax.NAWindow), INAWindow)
				Else
					m_naWindowEventSource = TryCast((Nothing), INAWindow)
				End If
				If m_naWindowEventSource Is Nothing Then
					Return
				End If

				'Create an instance of the delegate, add it to OnActiveAnalysisChanged event
				m_ActiveAnalysisChanged = New ESRI.ArcGIS.NetworkAnalystUI.INAWindowEvents_OnActiveAnalysisChangedEventHandler(AddressOf OnActiveAnalysisChanged)
				AddHandler CType(m_naWindowEventSource, ESRI.ArcGIS.NetworkAnalystUI.INAWindowEvents_Event).OnActiveAnalysisChanged, m_ActiveAnalysisChanged

				WireSelectionEvent()
				WireGraphicsEvents()
			Catch ex As Exception
				Dim msg As String = SubsetHelperUI.GetFullExceptionMessage(ex)
				MessageBox.Show(msg, "Wire Events")
			End Try
		End Sub

		Private Sub UnWireEvents()
			Try
				If m_naWindowEventSource Is Nothing Then
					Return
				End If

				UnWireSelectionEvent()
				UnWireGraphicsEvents()

				RemoveHandler CType(m_naWindowEventSource, ESRI.ArcGIS.NetworkAnalystUI.INAWindowEvents_Event).OnActiveAnalysisChanged, m_ActiveAnalysisChanged
				m_naWindowEventSource = Nothing
			Catch ex As Exception
				Dim msg As String = SubsetHelperUI.GetFullExceptionMessage(ex)
				MessageBox.Show(msg, "UnWire Events")
			End Try
		End Sub

		Private Sub WireSelectionEvent()
			Try
				If m_naWindowEventSource Is Nothing Then
					Return
				End If

				If Not m_mapEventSource Is Nothing Then
					UnWireSelectionEvent()
				End If

				m_mapEventSource = ActiveMap
				If m_mapEventSource Is Nothing Then
					Return
				End If

				UpdateSelectionEIDArrayParameterValues()

				'Create an instance of the delegate, add it to SelectionChanged event
				m_ActiveViewEventsSelectionChanged = New ESRI.ArcGIS.Carto.IActiveViewEvents_SelectionChangedEventHandler(AddressOf OnActiveViewEventsSelectionChanged)
				AddHandler CType(m_mapEventSource, ESRI.ArcGIS.Carto.IActiveViewEvents_Event).SelectionChanged, m_ActiveViewEventsSelectionChanged
			Catch ex As Exception
				Dim msg As String = SubsetHelperUI.GetFullExceptionMessage(ex)
				MessageBox.Show(msg, "Wire Selection Event")
			End Try
		End Sub

		Private Sub UnWireSelectionEvent()
			Try
				If m_naWindowEventSource Is Nothing Then
					Return
				End If

				If m_mapEventSource Is Nothing Then
					Return
				End If

				RemoveHandler CType(m_mapEventSource, ESRI.ArcGIS.Carto.IActiveViewEvents_Event).SelectionChanged, m_ActiveViewEventsSelectionChanged
				m_mapEventSource = Nothing

				SubsetHelperUI.ClearEIDArrayParameterValues(m_nax, SubsetHelperUI.SelectionEIDArrayBaseName)
			Catch ex As Exception
				Dim msg As String = SubsetHelperUI.GetFullExceptionMessage(ex)
				MessageBox.Show(msg, "UnWire Selection Event")
			End Try
		End Sub

		Private Sub WireGraphicsEvents()
			Try
				If m_naWindowEventSource Is Nothing Then
					Return
				End If

				If Not m_graphicsEventSource Is Nothing Then
					UnWireGraphicsEvents()
				End If

				Dim pActiveMap As IMap = ActiveMap
				Dim graphicsLayer As IGraphicsLayer = Nothing
				If Not pActiveMap Is Nothing Then
					graphicsLayer = pActiveMap.BasicGraphicsLayer
				End If

				If Not graphicsLayer Is Nothing Then
					m_graphicsEventSource = CType(graphicsLayer, IGraphicsContainer)
				End If

				If m_graphicsEventSource Is Nothing Then
					Return
				End If

				UpdateGraphicsEIDArrayParameterValues()

				'Create an instance of the delegate, add it to AllElementsDeleted event
				m_AllGraphicsDeleted = New ESRI.ArcGIS.Carto.IGraphicsContainerEvents_AllElementsDeletedEventHandler(AddressOf OnAllGraphicsDeleted)
				AddHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).AllElementsDeleted, m_AllGraphicsDeleted

				'Create an instance of the delegate, add it to ElementAdded event
				m_GraphicAdded = New ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementAddedEventHandler(AddressOf OnGraphicAdded)
				AddHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).ElementAdded, m_GraphicAdded

				'Create an instance of the delegate, add it to ElementDeleted event
				m_GraphicDeleted = New ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementDeletedEventHandler(AddressOf OnGraphicDeleted)
				AddHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).ElementDeleted, m_GraphicDeleted

				'Create an instance of the delegate, add it to ElementsAdded event
				m_GraphicsAdded = New ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementsAddedEventHandler(AddressOf OnGraphicsAdded)
				AddHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).ElementsAdded, m_GraphicsAdded

				'Create an instance of the delegate, add it to ElementUpdated event
				m_GraphicUpdated = New ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementUpdatedEventHandler(AddressOf OnGraphicUpdated)
				AddHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).ElementUpdated, m_GraphicUpdated
			Catch ex As Exception
				Dim msg As String = SubsetHelperUI.GetFullExceptionMessage(ex)
				MessageBox.Show(msg, "Wire Graphics Events")
			End Try
		End Sub

		Private Sub UnWireGraphicsEvents()
			Try
				If m_naWindowEventSource Is Nothing Then
					Return
				End If

				If m_graphicsEventSource Is Nothing Then
					Return
				End If

				RemoveHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).AllElementsDeleted, m_AllGraphicsDeleted
				RemoveHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).ElementAdded, m_GraphicAdded
				RemoveHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).ElementDeleted, m_GraphicDeleted
				RemoveHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).ElementsAdded, m_GraphicsAdded
				RemoveHandler CType(m_graphicsEventSource, ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event).ElementUpdated, m_GraphicUpdated

				m_graphicsEventSource = Nothing

				SubsetHelperUI.ClearEIDArrayParameterValues(m_nax, SubsetHelperUI.GraphicsEIDArrayBaseName)
			Catch ex As Exception
				Dim msg As String = SubsetHelperUI.GetFullExceptionMessage(ex)
				MessageBox.Show(msg, "UnWire Graphics Events")
			End Try
		End Sub
#End Region

		Private Sub UpdateSelectionEIDArrayParameterValues()
			Dim map As IMap = ActiveMap
			If map Is Nothing Then
				Return
			End If

			Dim naWindow As INAWindow = m_nax.NAWindow
			Dim naLayer As INALayer = Nothing
			Dim naContext As INAContext = Nothing
			Dim nds As INetworkDataset = Nothing

			naLayer = naWindow.ActiveAnalysis
			If Not naLayer Is Nothing Then
				naContext = naLayer.Context
			End If

			If Not naContext Is Nothing Then
				nds = naContext.NetworkDataset
			End If

			If nds Is Nothing Then
				Return
			End If

			Dim baseName As String = SubsetHelperUI.SelectionEIDArrayBaseName
			Dim vt As VarType = SubsetHelperUI.GetEIDArrayParameterType()

			Dim sourceNames As List(Of String) = SubsetHelperUI.FindParameterizedSourceNames(nds, baseName, vt)
			Dim oidArraysBySourceName As Dictionary(Of String, ILongArray) = SubsetHelperUI.GetOIDArraysBySourceNameFromMapSelection(map, sourceNames)
			SubsetHelperUI.UpdateEIDArrayParameterValuesFromOIDArrays(m_nax, oidArraysBySourceName, baseName)
		End Sub

		Private Sub UpdateGraphicsEIDArrayParameterValues()
			Dim graphics As IGraphicsContainer = ActiveGraphics
			If graphics Is Nothing Then
				Return
			End If

			Dim naWindow As INAWindow = m_nax.NAWindow
			Dim naLayer As INALayer = Nothing
			Dim naContext As INAContext = Nothing
			Dim nds As INetworkDataset = Nothing

			naLayer = naWindow.ActiveAnalysis
			If Not naLayer Is Nothing Then
				naContext = naLayer.Context
			End If

			If Not naContext Is Nothing Then
				nds = naContext.NetworkDataset
			End If

			If nds Is Nothing Then
				Return
			End If

			Dim baseName As String = SubsetHelperUI.GraphicsEIDArrayBaseName
			Dim vt As VarType = SubsetHelperUI.GetEIDArrayParameterType()

			Dim sourceNames As List(Of String) = SubsetHelperUI.FindParameterizedSourceNames(nds, baseName, vt)
			Dim searchGeometry As IGeometry = SubsetHelperUI.GetSearchGeometryFromGraphics(graphics)
			SubsetHelperUI.UpdateEIDArrayParameterValuesFromGeometry(m_nax, searchGeometry, baseName)
		End Sub

		Private ReadOnly Property ActiveMap() As IMap
			Get
				Dim doc As IDocument = m_application.Document
				Dim mxdoc As IMxDocument = TryCast(doc, IMxDocument)
				Return CType(mxdoc.FocusMap, IMap)
			End Get
		End Property

		Private ReadOnly Property ActiveGraphics() As IGraphicsContainer
			Get
				Dim pActiveMap As IMap = ActiveMap
				Dim graphics As IGraphicsContainer = Nothing
				If Not pActiveMap Is Nothing Then
					graphics = TryCast(pActiveMap.BasicGraphicsLayer, IGraphicsContainer)
				End If

				Return graphics
			End Get
		End Property

#Region "Event Handlers"

#Region "NAWindow Event Handlers"
		Private Sub OnActiveAnalysisChanged()
			If Not m_mapEventSource Is Nothing Then
				WireSelectionEvent()
			End If

			If Not m_graphicsEventSource Is Nothing Then
				WireGraphicsEvents()
			End If
		End Sub

#End Region

#Region "Selection Event Handler"
		Private Sub OnActiveViewEventsSelectionChanged()
			UpdateSelectionEIDArrayParameterValues()
		End Sub
#End Region

#Region "Graphics Event Handlers"
		Private Sub OnAllGraphicsDeleted()
			UpdateGraphicsEIDArrayParameterValues()
		End Sub

		Private Sub OnGraphicAdded(ByVal element As IElement)
			UpdateGraphicsEIDArrayParameterValues()
		End Sub

		Private Sub OnGraphicDeleted(ByVal element As IElement)
			UpdateGraphicsEIDArrayParameterValues()
		End Sub

		Private Sub OnGraphicsAdded(ByVal elements As IElementCollection)
			UpdateGraphicsEIDArrayParameterValues()
		End Sub

		Private Sub OnGraphicUpdated(ByVal element As IElement)
			UpdateGraphicsEIDArrayParameterValues()
		End Sub
#End Region

#End Region
	End Class
End Namespace
