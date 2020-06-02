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
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.NetworkAnalystUI
Imports SubsetNetworkEvaluators

Namespace SubsetNetworkEvaluatorsUI
	''' <summary>
	''' The RemoveSubsetAttributesCommand is a context menu item automatically added to the ArcCatalog
	''' Network Dataset context menu.  If the network analyst extension license is checked out
	''' you can use this command to quickly reverse the affects of the AddSubsetAttributesCommand.  It only
	''' removes those subset attributes that were or could have been added by the add command.  You can always
	''' just remove the subset attributes manually using the attributes page of the network dataset property sheet.
	''' Removing the attribute will also remove the parameters and evaluator assignments.
	''' </summary>
	<Guid("EDD273A2-2AC2-4f2f-B096-7088B58F0667"), ClassInterface(ClassInterfaceType.None), ProgId("SubsetNetworkEvaluatorsUI.RemoveSubsetAttributesCommand")> _
	Public NotInheritable Class RemoveSubsetAttributesCommand : Inherits BaseCommand
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
			GxCommands.Register(regKey)
			GxNetworkDatasetContextMenuCommands.Register(regKey)
		End Sub
		''' <summary>
		''' Required method for ArcGIS Component Category unregistration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			GxCommands.Unregister(regKey)
			GxNetworkDatasetContextMenuCommands.Unregister(regKey)
		End Sub

#End Region
#End Region

		Private m_application As IApplication = Nothing
		Private m_nax As INetworkAnalystExtension = Nothing

		Public Sub New()
			MyBase.m_category = "Network Analyst Samples" 'localizable text
			MyBase.m_caption = "Remove Subset Attributes" 'localizable text
			MyBase.m_message = "Remove Subset Attributes" 'localizable text
			MyBase.m_toolTip = "Remove Subset Attributes" 'localizable text
			MyBase.m_name = "NASamples_RemoveSubsetAttributes" 'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
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
			m_nax = TryCast(SubsetHelperUI.GetNAXConfiguration(m_application), INetworkAnalystExtension)
		End Sub

		''' <summary>
		''' Occurs when this command is clicked
		''' </summary>
		Public Overloads Overrides Sub OnClick()
			Dim gxApp As IGxApplication = TryCast(m_application, IGxApplication)
			Dim gxDataset As IGxDataset = Nothing
			Dim dsType As esriDatasetType = esriDatasetType.esriDTAny

			If Not gxApp Is Nothing Then
				gxDataset = TryCast(gxApp.SelectedObject, IGxDataset)
				dsType = gxDataset.Type
			End If

			If dsType <> esriDatasetType.esriDTNetworkDataset Then
				Return
			End If

			Dim ds As IDataset = gxDataset.Dataset
			If ds Is Nothing Then
				Return
			End If

			Dim nds As INetworkDataset = TryCast(ds, INetworkDataset)
			If nds Is Nothing Then
				Return
			End If

			If (Not nds.Buildable) Then
				Return
			End If

			Dim netBuild As INetworkBuild = TryCast(nds, INetworkBuild)
			If netBuild Is Nothing Then
				Return
			End If

			Dim dsComponent As IDatasetComponent = TryCast(nds, IDatasetComponent)
			Dim deNet As IDENetworkDataset = Nothing
			If Not dsComponent Is Nothing Then
				deNet = TryCast(dsComponent.DataElement, IDENetworkDataset)
			End If

			If deNet Is Nothing Then
				Return
			End If

			FilterSubsetEvaluator.RemoveFilterSubsetAttribute(deNet)
			ScaleSubsetEvaluator.RemoveScaleSubsetAttributes(deNet)

			netBuild.UpdateSchema(deNet)
		End Sub

		Public Overloads Overrides ReadOnly Property Enabled() As Boolean
			Get
				Dim gxApp As IGxApplication = TryCast(m_application, IGxApplication)
				Dim gxDataset As IGxDataset = Nothing
				Dim dsType As esriDatasetType = esriDatasetType.esriDTAny

				Dim naxEnabled As Boolean = False
				Dim naxConfig As IExtensionConfig = TryCast(m_nax, IExtensionConfig)
				If Not naxConfig Is Nothing Then
					naxEnabled = naxConfig.State = esriExtensionState.esriESEnabled
				End If

				If naxEnabled Then
					If Not gxApp Is Nothing Then
						gxDataset = TryCast(gxApp.SelectedObject, IGxDataset)
						dsType = gxDataset.Type
					End If
				End If

				Return (dsType = esriDatasetType.esriDTNetworkDataset)
			End Get
		End Property

#End Region
	End Class
End Namespace
