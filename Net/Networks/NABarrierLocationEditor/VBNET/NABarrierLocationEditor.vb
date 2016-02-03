Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.NetworkAnalystUI

Namespace NABarrierLocationEditor
	''' <summary>
	''' Summary description for BarrierLocationEditor.
	''' 
	''' This sample teaches how to load polygon and polyline barrier values programmatically,
	''' while also providing a way to visualize and edit the underlying network element values that make up a polygon or polyline barrier.
	''' As a side benefit, the programmer also learns how to flash the geometry of a network element 
	''' (with a corresponding digitized direction arrow), as well as how to set up a context menu command for the NAWindow.
	'''   
	''' </summary>
	<Guid("7a93ba10-9dee-11da-a746-0800200c9a66"), ClassInterface(ClassInterfaceType.None), ProgId("NABarrierLocationEditor.NABarrierLocationEditor")> _
	Public NotInheritable Class NABarrierLocationEditor
		Inherits BaseCommand
		Implements INAWindowCommand2
		Public m_naExt As INetworkAnalystExtension ' Hook into the Desktop NA Extension
		Public m_application As IApplication ' Hook into ArcMap

#Region "COM Registration Function(s)"
		<ComRegisterFunction(), ComVisible(False)> _
		Private Shared Sub RegisterFunction(ByVal registerType As Type)
			' Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType)
		End Sub

		<ComUnregisterFunction(), ComVisible(False)> _
		Private Shared Sub UnregisterFunction(ByVal registerType As Type)
			' Required for ArcGIS Component Category Registrar support
			ArcGISCategoryUnregistration(registerType)
		End Sub

#Region "ArcGIS Component Category Registrar generated code"
		''' <summary>
		''' Required method for ArcGIS Component Category registration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			ESRI.ArcGIS.ADF.CATIDs.MxCommands.Register(regKey)
			ESRI.ArcGIS.ADF.CATIDs.ControlsCommands.Register(regKey)
			' Register with NetworkAnalystWindowItemsCommand to get the 
			' command to show up when you right click on the class in the NAWindow
			ESRI.ArcGIS.ADF.CATIDs.NetworkAnalystWindowItemsCommand.Register(regKey)
		End Sub
		''' <summary>
		''' Required method for ArcGIS Component Category unregistration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			ESRI.ArcGIS.ADF.CATIDs.MxCommands.Unregister(regKey)
			ESRI.ArcGIS.ADF.CATIDs.ControlsCommands.Unregister(regKey)
			ESRI.ArcGIS.ADF.CATIDs.NetworkAnalystWindowItemsCommand.Unregister(regKey)
		End Sub

#End Region

#End Region

		Public Sub New()
			MyBase.m_category = "Developer Samples"
			MyBase.m_caption = "Edit Network Analyst Barrier Location Ranges"
			MyBase.m_message = "Edit Network Analyst Barrier Location Ranges"
			MyBase.m_toolTip = "Edit Network Analyst Barrier Location Ranges"
			MyBase.m_name = "EditNABarrierLocationRanges"

			Try
				Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
				MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
			Catch ex As Exception
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
			End Try
		End Sub

		Protected Overrides Sub Finalize()
			m_application = Nothing
			m_naExt = Nothing
			GC.Collect()
		End Sub

#Region "Overridden Class Methods"

		''' <summary>
		''' Occurs when this command is created
		''' </summary>
		''' <param name="hook">Instance of the application</param>
		''' 
		Public Overrides Sub OnCreate(ByVal hook As Object)
			If hook Is Nothing Then
				Return
			End If

			m_application = TryCast(hook, IApplication)
			MyBase.m_enabled = True

			If Not m_application Is Nothing Then
				m_naExt = TryCast(m_application.FindExtensionByName("Network Analyst"), INetworkAnalystExtension)
			End If
		End Sub

		''' <summary>
		''' Occurs when this command is clicked
		''' </summary>
		Public Overrides Sub OnClick()
			Try
				OpenBarrierEditorForm()
			Catch exception As Exception
				MessageBox.Show(exception.Message, "Error")
			End Try
		End Sub

#End Region

#Region "Overridden INAWindowCommand Methods"

		Public Function Applies(ByVal naLayer As INALayer, ByVal category As INAWindowCategory) As Boolean Implements INAWindowCommand.Applies, INAWindowCommand2.Applies
			' The category is associated with an NAClass.
			' In our case, we want the PolygonBarriers and PolylineBarriers classes
			If Not category Is Nothing Then
				Dim categoryName As String = category.NAClass.ClassDefinition.Name
				If categoryName = "PolygonBarriers" OrElse categoryName = "PolylineBarriers" Then
					Return True
				End If
			End If

			Return False
		End Function

#Region "INAWindowCommand2 Members"

		''' <summary>
		''' Occurs in determining whether or not to include the command as a context menu item
		''' <param name="naLayer">The active analysis layer</param>
		''' <param name="categoryGroup">The selected group in the NAWindow</param>
		''' </summary>
		Private Function AppliesToGroup(ByVal naLayer As INALayer, ByVal categoryGroup As INAWindowCategoryGroup) As Boolean Implements INAWindowCommand2.AppliesToGroup
			If Not categoryGroup Is Nothing Then
				Return Applies(naLayer, categoryGroup.Category)
			End If

			Return False
		End Function

		Private ReadOnly Property Priority() As Integer Implements INAWindowCommand2.Priority
			Get
				Return 1
			End Get
		End Property

#End Region

#End Region

		''' <summary>
		''' This command will be enabled for Polygon and Polyline Barriers
		''' </summary>
		Public Overrides ReadOnly Property Enabled() As Boolean
			Get
				Return Applies(Nothing, GetActiveCategory())
			End Get
		End Property

		''' <summary>
		''' To open the editor form, we need to first determine which barrier is
		'''  being edited, then pass that value to the form
		''' </summary>
		Private Sub OpenBarrierEditorForm()
			' get the barrier layer by using the category name to as the NAClassName
			Dim activeCategory As INAWindowCategory = GetActiveCategory()
			Dim categoryName As String = activeCategory.NAClass.ClassDefinition.Name
			Dim naLayer As INALayer = GetActiveAnalysisLayer()
			Dim layer As ILayer = naLayer.LayerByNAClassName(categoryName)

			' get a selection count and popup a message if more or less than one item is selected
			Dim fSel As IFeatureSelection = TryCast(layer, IFeatureSelection)
			Dim selSet As ISelectionSet = fSel.SelectionSet
			If selSet.Count <> 1 Then
				System.Windows.Forms.MessageBox.Show("Only one barrier in a category can be selected at a time for this command to execute", "Barrier Location Editor Warning")
			Else
				' get the object IDs of the selected item
				Dim id As Integer = selSet.IDs.Next()

				' Get the barrier feature by using the selected ID
				Dim fClass As IFeatureClass = TryCast(naLayer.Context.NAClasses.ItemByName(categoryName), IFeatureClass)
				Dim barrierFeature As IFeature = fClass.GetFeature(id)

				' display the form for editing the barrier
				Dim form As EditorForm = New EditorForm(m_application, naLayer.Context, barrierFeature)
				form.ShowDialog()
				form = Nothing
			End If
		End Sub

#Region "NAWindow Interaction"

		Private Function GetActiveAnalysisLayer() As INALayer
			If Not m_naExt Is Nothing Then
				Return m_naExt.NAWindow.ActiveAnalysis
			Else
				Return Nothing
			End If
		End Function

		Private Function GetActiveCategory() As INAWindowCategory2
			If Not m_naExt Is Nothing Then
				Return TryCast(m_naExt.NAWindow.ActiveCategory, INAWindowCategory2)
			Else
				Return Nothing
			End If
		End Function

#End Region

	End Class
End Namespace
