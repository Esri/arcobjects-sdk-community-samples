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
' Copyright 2010 ESRI
' 
' All rights reserved under the copyright laws of the United States
' and applicable international laws, treaties, and conventions.
'
' You may freely redistribute and use this sample code, with or
' without modification, provided you include the original copyright
' notice and use restrictions.
' 
' See the use restrictions at &ltyour ArcGIS install location&gt/DeveloperKit10.0/userestrictions.txt.
'Namespace SchematicCreateBasicSettingsAddIn

Imports System.Collections.Generic
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.ArcCatalog
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Carto
Imports System.Windows.Forms

Public Class GenerateSchematicTemplate
	Inherits ESRI.ArcGIS.Desktop.AddIns.Button
#Region "Variables"


	Public formNames As frmDatasetTemplateName
	Private m_pWS As ESRI.ArcGIS.Geodatabase.IWorkspace
	Private m_sfn As String
	Private m_pB As ESRI.ArcGIS.Schematic.ISchematicBuilder
	Private m_pSDS As ESRI.ArcGIS.Schematic.ISchematicDataset
	Private m_pSB As ESRI.ArcGIS.Schematic.ISchematicStandardBuilder
	Private m_pSDT As ESRI.ArcGIS.Schematic.ISchematicDiagramClass

	Private m_pSDI As ESRI.ArcGIS.Schematic.ISchematicDatasetImport

	Private templateInfo As NameEvents
	Public formReduce As frmSelectItemsToReduce
	Private blnCancel As Boolean
	Public formAdvanced As frmAdvanced
	Private strLayers As String
	Private strNodeLayers As String
	Private m_myCol As New NameValueCollection()
	Private m_SelectedObject As IGxObject = Nothing

#End Region


	Public Sub New()

	End Sub

	Private Function GetLayers() As IEnumLayer
		'now get the map document to parse out the feature classes
		Dim pGxDialog As GxDialog = New GxDialogClass()
		Dim pEnumGxObject As IEnumGxObject = Nothing
		Dim pResult As Boolean

		pGxDialog.ObjectFilter = New GxFilterMapsClass()
		pGxDialog.Title = "Select a map document"
		Try
			pResult = pGxDialog.DoModalOpen(0, pEnumGxObject)
			'check to see if the user canceled the dialog
			If pResult = False Then
				Return Nothing
			End If
			Dim pGxObject As IGxObject = pEnumGxObject.Next()
			Dim pMapReader As IMapReader = New MapReaderClass()
			pMapReader.Open(pGxObject.FullName.ToString())
			Dim pMap As IMap = pMapReader.Map(0)
			Dim pUID As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass()
			pUID.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}"
			'feature layer
			Dim pLayers As IEnumLayer = pMap.Layers(pUID, True)
			Return pLayers
		Catch
			'error getting layers
			Return Nothing
		End Try

	End Function


	Private Function ProcessFCs(ByVal fcComplexEdge As IEnumFeatureClass, ByVal fcComplexNode As IEnumFeatureClass, ByVal fcSimpleEdge As IEnumFeatureClass, ByVal fcSimpleNode As IEnumFeatureClass) As Dictionary(Of String, IFeatureClass)
		Dim pDictionary As New Dictionary(Of String, IFeatureClass)()

		'handle complex edge
		Dim fc As IFeatureClass = fcComplexEdge.Next()
		If fc IsNot Nothing Then
			Do
				Try
					pDictionary.Add(fc.AliasName, fc)
					'do nothing
				Catch
				End Try
				fc = fcComplexEdge.Next()
			Loop While fc IsNot Nothing
		End If

		'handle complex node
		fc = fcComplexNode.Next()
		If fc IsNot Nothing Then
			Do
				Try
					pDictionary.Add(fc.AliasName, fc)
					'do nothing
				Catch
				End Try
				fc = fcComplexNode.Next()
			Loop While fc IsNot Nothing
		End If

		'handle simple edge
		fc = fcSimpleEdge.Next()
		If fc IsNot Nothing Then
			Do
				Try
					pDictionary.Add(fc.AliasName, fc)
					'do nothing
				Catch
				End Try
				fc = fcSimpleEdge.Next()
			Loop While fc IsNot Nothing
		End If

		'handle simple node
		fc = fcSimpleNode.Next()
		If fc IsNot Nothing Then
			Do
				Try
					pDictionary.Add(fc.AliasName, fc)
					'do nothing
				Catch
				End Try
				fc = fcSimpleNode.Next()
			Loop While fc IsNot Nothing
		End If
		Return pDictionary
	End Function

	Private Function CreateSchLayers(ByVal pLayers As IEnumLayer) As String
		If pLayers Is Nothing Then
			Return ""
		End If
		pLayers.Reset()
		Dim pLayer As ILayer = pLayers.Next()
		Dim featureLayer As IFeatureLayer
		Dim featureClass As IFeatureClass
		Dim pStrLayerNames As String = ""
		Dim pDataset As IDataset
		System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
		System.Windows.Forms.Cursor.Show()

		m_pSDS.DesignMode = True
		m_pSDI = DirectCast(m_pSDS, ESRI.ArcGIS.Schematic.ISchematicDatasetImport)

		Dim myDictionary As New Dictionary(Of String, IFeatureClass)()
		Dim gn As IGeometricNetwork = Nothing
		Do
			featureLayer = DirectCast(pLayer, IFeatureLayer)
			featureClass = featureLayer.FeatureClass
			pDataset = DirectCast(featureClass, IDataset)

			If featureClass.FeatureType = esriFeatureType.esriFTSimpleJunction OrElse featureClass.FeatureType = esriFeatureType.esriFTSimpleEdge OrElse featureClass.FeatureType = esriFeatureType.esriFTComplexEdge OrElse featureClass.FeatureType = esriFeatureType.esriFTComplexJunction Then

				'The FeatureType property of feature classes that implement this interface will be esriFTSimpleJunction, esriDTSimpleEdge, esriFTComplexJunction, or esriFTComplexEdge.
				Dim networkClass As INetworkClass = DirectCast(featureLayer.FeatureClass, INetworkClass)

				If networkClass.GeometricNetwork IsNot Nothing Then
					'we have a network class
					If (gn Is Nothing) OrElse (gn IsNot networkClass.GeometricNetwork) Then
						'need to process all the classes
						Dim localDictionary As New Dictionary(Of String, IFeatureClass)()
						gn = networkClass.GeometricNetwork
						Dim fcComplexEdge As IEnumFeatureClass = networkClass.GeometricNetwork.ClassesByType(esriFeatureType.esriFTComplexEdge)
						Dim fcComplexNode As IEnumFeatureClass = networkClass.GeometricNetwork.ClassesByType(esriFeatureType.esriFTComplexJunction)
						Dim fcSimpleEdge As IEnumFeatureClass = networkClass.GeometricNetwork.ClassesByType(esriFeatureType.esriFTSimpleEdge)
						Dim fcSimpleNode As IEnumFeatureClass = networkClass.GeometricNetwork.ClassesByType(esriFeatureType.esriFTSimpleJunction)
						localDictionary = ProcessFCs(fcComplexEdge, fcComplexNode, fcSimpleEdge, fcSimpleNode)
						If myDictionary.Count = 0 Then
							'just copy it
							myDictionary = localDictionary
						Else
							'merge
							Dim keyColl As Dictionary(Of String, IFeatureClass).KeyCollection = localDictionary.Keys

							For Each s As String In keyColl
								Dim fc As IFeatureClass = Nothing
								Dim bln As Boolean = localDictionary.TryGetValue(s, fc)
								myDictionary.Add(s, fc)
							Next
						End If
					End If
					'Build up the string that will go to the select items to reduce form
					pStrLayerNames += pDataset.Name.ToString()
					pStrLayerNames += ";"

					'Build up the string for just the node feature classes
					If featureClass.FeatureType = esriFeatureType.esriFTSimpleJunction Or featureClass.FeatureType = esriFeatureType.esriFTComplexJunction Then
						strNodeLayers += pDataset.Name.ToString()
						strNodeLayers += ";"
					End If

					'create the fields collections to be used by the frmAdvanced form
					Dim pFields As IFields = featureClass.Fields
					If pFields.FieldCount > 0 Then
						For i As Integer = 0 To pFields.FieldCount - 1
							'don't mess with objectid or shape or GlobalID
							Dim name As String = pFields.Field(i).Name.ToString()
							If (name <> "OBJECTID") AndAlso (name <> "SHAPE") AndAlso (name <> "GlobalID") AndAlso (name <> featureClass.OIDFieldName.ToString()) AndAlso (name <> featureClass.ShapeFieldName.ToString()) Then
								m_myCol.Add(pDataset.Name.ToString(), pFields.Field(i).Name.ToString())
							End If
						Next
					End If

					'remove the layer from the list of dictionary classes
					If myDictionary.ContainsKey(featureClass.AliasName) Then
						myDictionary.Remove(featureClass.AliasName)
					End If

					m_pSDI.ImportFeatureLayer(featureLayer, m_pSDT, True, True, True)
				End If
			End If
			pLayer = pLayers.Next()
		Loop While pLayer IsNot Nothing

		'handle any feature classes that were not in the map
		If myDictionary.Count > 0 Then
			Dim keyColl As Dictionary(Of String, IFeatureClass).KeyCollection = myDictionary.Keys
			For Each s As String In keyColl
				Dim fc As IFeatureClass = Nothing
				Dim bln As Boolean = myDictionary.TryGetValue(s, fc)
				Dim o As IObjectClass = DirectCast(fc, IObjectClass)
				pDataset = DirectCast(fc, IDataset)

				pStrLayerNames += pDataset.Name.ToString()
				pStrLayerNames += ";"

				'Build up the string for just the node feature classes
				If featureClass.FeatureType = esriFeatureType.esriFTSimpleJunction Or featureClass.FeatureType = esriFeatureType.esriFTComplexJunction Then
					strNodeLayers += pDataset.Name.ToString()
					strNodeLayers += ";"
				End If

				'create the fields collections to be used by the frmAdvanced form
				Dim pFields As IFields = fc.Fields
				If pFields.FieldCount > 0 Then
					For i As Integer = 0 To pFields.FieldCount - 1
						'don't mess with objectid or shape or GlobalID
						Dim name As String = pFields.Field(i).Name.ToString()
						If (name <> "OBJECTID") AndAlso (name <> "SHAPE") AndAlso (name <> "GlobalID") AndAlso (name <> fc.OIDFieldName.ToString()) AndAlso (name <> fc.ShapeFieldName.ToString()) Then
							m_myCol.Add(pDataset.Name.ToString(), pFields.Field(i).Name.ToString())
						End If
					Next
				End If
				If (fc.FeatureType = esriFeatureType.esriFTComplexJunction) OrElse (fc.FeatureType = esriFeatureType.esriFTSimpleJunction) Then
					'node
					m_pSDI.ImportObjectClass(o, m_pSDT, True, esriSchematicElementType.esriSchematicNodeType)
				Else
					'link
					m_pSDI.ImportObjectClass(o, m_pSDT, True, esriSchematicElementType.esriSchematicLinkType)
				End If
			Next
		End If

		m_pSDS.Save(ESRI.ArcGIS.esriSystem.esriArcGISVersion.esriArcGISVersionCurrent, True)
		m_pSDS.DesignMode = False
		Return pStrLayerNames
	End Function

	Protected Overrides Sub OnClick()
		blnCancel = False
		formNames = New frmDatasetTemplateName()
		AddHandler formNames.cancelFormEvent, AddressOf formNames_cancelFormEvent
		AddHandler formNames.nextFormEvent, AddressOf formNames_nextFormEvent
		m_SelectedObject = ArcCatalog.ThisApplication.SelectedObject
		If (m_SelectedObject.Category = "Schematic Dataset") OrElse (m_SelectedObject.Category.ToLower().Contains("database")) Then
			If m_SelectedObject.Category.ToLower().Contains("database") Then
				'get dataset and template names, then create the objects
				formNames.blnNewDataset = True
			Else
				'dataset, just get template names, then create objects
				formNames.blnNewDataset = False
			End If
			'show the first form of the wizard 
			If formNames.ShowDialog() = DialogResult.Cancel Then
				formNames = Nothing
				Return
			End If
		Else
			'we are not on a database or a schematic dataset
			blnCancel = True
		End If

		If blnCancel = True Then
			System.Windows.Forms.MessageBox.Show("The name of the dataset or template already exists.  Please try again with valid names.")
		End If

		If blnCancel <> True Then
			'only true if the user cancels the first form formNames_cancelFormEvent
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
			strLayers = CreateSchLayers(GetLayers())
			If strLayers.Length > 0 Then
				'make sure we get something back
				'find out if we need to create node reduction rules
				formReduce = New frmSelectItemsToReduce()
				AddHandler formReduce.doneFormEvent, AddressOf formReduce_doneFormEvent
				AddHandler formReduce.cancelFormEvent, AddressOf formReduce_cancelFormEvent

				formReduce.itemList = strNodeLayers
				System.Windows.Forms.Cursor.Current = Cursors.Default
				formReduce.ShowDialog()
            Else
                'this can happen if the map document didn't have any
                'layers corresponding to a geometric network
                blnCancel = True
            End If
			System.Windows.Forms.Cursor.Current = Cursors.Default
		End If

		If blnCancel <> True Then
			'could have cancelled on either frmDatasetTemplateName or frmSelectItemsToReduce
			'Advanced Form
			formAdvanced = New frmAdvanced()
			AddHandler formAdvanced.doneFormEvent, AddressOf formAdvanced_doneFormEvent

			formAdvanced.strLayers = Me.strLayers
			formAdvanced.strNodeLayers = Me.strNodeLayers
			formAdvanced.m_myCol = Me.m_myCol
			formAdvanced.ShowDialog()
		End If

		Try
			ArcCatalog.ThisApplication.Refresh(m_sfn)
			ArcCatalog.ThisApplication.Location = m_SelectedObject.FullName.ToString()
		Catch
		End Try
		cleanUp()
	End Sub


	Private Sub formReduce_cancelFormEvent(ByVal sender As Object, ByVal e As EventArgs)
		blnCancel = True
		formReduce.Close()
	End Sub

	Private Sub formAdvanced_doneFormEvent(ByVal sender As Object, ByVal e As AdvancedEvents)
		m_pSDS.DesignMode = True
		formAdvanced.Cursor = System.Windows.Forms.Cursors.WaitCursor
		'process the algorithm if there is one
		If e.AlgorithmName <> "" Then
			Dim a As ISchematicAlgoSmartTree = New SchematicAlgoSmartTreeClass()
			If e.AlgorithmParams.Count > 0 Then
				Dim keys As Dictionary(Of String, String).KeyCollection = e.AlgorithmParams.Keys
				Dim strValue As String = ""
				For Each s As String In keys
					If s = "Direction" Then
						e.AlgorithmParams.TryGetValue(s, strValue)

						If strValue = "Top to Bottom" Then
							a.Direction = esriSchematicAlgoDirection.esriSchematicAlgoTopDown
						ElseIf strValue = "Bottom to Top" Then
							a.Direction = esriSchematicAlgoDirection.esriSchematicAlgoBottomUp
						ElseIf strValue = "Left to Right" Then
							a.Direction = esriSchematicAlgoDirection.esriSchematicAlgoLeftRight
						Else
							a.Direction = esriSchematicAlgoDirection.esriSchematicAlgoRightLeft
						End If
					End If
				Next
				If e.RootClass <> "" Then
					Dim pECC As ISchematicElementClassContainer = DirectCast(m_pSDS, ISchematicElementClassContainer)
					Dim pEC As ISchematicElementClass = pECC.GetSchematicElementClass(e.RootClass)
					Dim u As New ESRI.ArcGIS.esriSystem.UID()
					u.Value = "{3AD9D8B8-0A1D-4F32-ABB5-54B848A46F85}"

					Dim pAttrConst As ISchematicAttributeConstant = DirectCast(pEC.CreateSchematicAttribute("RootFlag", u), ISchematicAttributeConstant)
					Dim pAttrMgmt As ISchematicAttributeManagement = DirectCast(pAttrConst, ISchematicAttributeManagement)
					pAttrMgmt.StorageMode = esriSchematicAttributeStorageMode.esriSchematicAttributeFieldStorage
					pAttrConst.ConstantValue = "-1"
				End If
			End If


			m_pSDT.SchematicAlgorithm = DirectCast(a, ISchematicAlgorithm)
		End If

		'check to see if we need to add associated fields
		If e.FieldsToCreate IsNot Nothing Then
			If e.FieldsToCreate.Count > 0 Then
				Dim pECC As ISchematicElementClassContainer = DirectCast(m_pSDS, ISchematicElementClassContainer)

				'create the associated field attributes
				Dim keys As String() = e.FieldsToCreate.AllKeys
				For Each s As String In keys
					'get the feature class
					Dim pEC As ISchematicElementClass = pECC.GetSchematicElementClass(s)
					If pEC IsNot Nothing Then
						Dim strName As String = ""
						Dim values As String() = e.FieldsToCreate.GetValues(s)
						For Each v As String In values
							'create the field
							Dim u As New ESRI.ArcGIS.esriSystem.UID()
							u.Value = "{7DE3A19D-32D0-41CD-B896-37CA3AFBD88A}"

							Dim pClass As IClass = DirectCast(pEC, IClass)
							If pClass.FindField(v) <> -1 Then
								'name exists
								strName = "RF" & v.ToString()
							Else
								strName = v.ToString()
							End If
							Dim pFieldAttr As ISchematicAttributeAssociatedField = DirectCast(pEC.CreateSchematicAttribute(strName, u), ISchematicAttributeAssociatedField)
							pFieldAttr.AssociatedFieldName = v
							Dim pAttrMgmt As ISchematicAttributeManagement = DirectCast(pFieldAttr, ISchematicAttributeManagement)

							pAttrMgmt.StorageMode = esriSchematicAttributeStorageMode.esriSchematicAttributeFieldStorage
						Next
					End If
				Next
			End If
		End If

		m_pSDS.Save(ESRI.ArcGIS.esriSystem.esriArcGISVersion.esriArcGISVersionCurrent, True)
		m_pSDS.DesignMode = False
		formAdvanced.Cursor = System.Windows.Forms.Cursors.Default
		formAdvanced.Close()
	End Sub

	Private Function CreateTemplate(ByVal templateInfo As NameEvents) As Boolean
		'need to get everything first
		Dim pDatabase As IGxDatabase = Nothing

		If m_SelectedObject.Category = "Schematic Dataset" Then
			pDatabase = DirectCast(m_SelectedObject.Parent, IGxDatabase)
		Else
			'on the database already
			pDatabase = DirectCast(m_SelectedObject, IGxDatabase)
		End If
		m_pWS = pDatabase.Workspace

		Dim pSWF As ESRI.ArcGIS.Schematic.ISchematicWorkspaceFactory = New SchematicWorkspaceFactory()
		Dim pSW As ESRI.ArcGIS.Schematic.ISchematicWorkspace = pSWF.Open(m_pWS)

		m_pSDS = pSW.SchematicDatasetByName(templateInfo.DatasetName)

		Dim pDCContainer As ISchematicDiagramClassContainer = m_pSDS
		m_pSDT = pDCContainer.GetSchematicDiagramClass(templateInfo.TemplateName.ToString())
		If m_pSDT IsNot Nothing Then
			'name already exists
			Return False
		End If

		'create the schematic template
		m_pSDT = m_pSDS.CreateSchematicDiagramClass(templateInfo.TemplateName)

		If (templateInfo.UseVertices = True) Then
			m_pB = DirectCast(m_pSDT, ESRI.ArcGIS.Schematic.ISchematicBuilder)
			m_pSB = DirectCast(m_pSDT.SchematicBuilder, ESRI.ArcGIS.Schematic.ISchematicStandardBuilder)
			m_pSB.InitializeLinksVertices = templateInfo.UseVertices
		End If
		m_pSDS.Save(ESRI.ArcGIS.esriSystem.esriArcGISVersion.esriArcGISVersion10, False)
		Return True
	End Function

	Private Function CreateDataset(ByVal templateInfo As NameEvents) As Boolean
		Try
			Dim pDatabase As IGxDatabase = DirectCast(m_SelectedObject, IGxDatabase)

			m_pWS = pDatabase.Workspace

			Dim pSWF As ESRI.ArcGIS.Schematic.ISchematicWorkspaceFactory = New SchematicWorkspaceFactory()
			Dim pSW As ESRI.ArcGIS.Schematic.ISchematicWorkspace = pSWF.Open(m_pWS)

			m_pSDS = pSW.SchematicDatasetByName(templateInfo.DatasetName.ToString())
			If m_pSDS IsNot Nothing Then
				'name exists
				Return False
			End If

			m_pSDS = pSW.CreateSchematicDataset(templateInfo.DatasetName, "")
			Return True
		Catch		'ex As Exception
			Return False
		End Try
	End Function

	Private Sub formNames_cancelFormEvent(ByVal sender As Object, ByVal e As EventArgs)
		'user is canceling the wizard
		formNames.Close()
		formNames = Nothing
		blnCancel = True
	End Sub

	Private Sub formReduce_doneFormEvent(ByVal sender As Object, ByVal e As ReduceEvents)
		'user click the done button on the reduce form
		Dim pIsbr As ISchematicBuilderRule
		Dim pIsbrc As ISchematicBuilderRuleContainer = DirectCast(m_pSDT, ISchematicBuilderRuleContainer)
		Dim pIsbrce As ISchematicBuilderRuleContainerEdit = DirectCast(pIsbrc, ISchematicBuilderRuleContainerEdit)

		formReduce.Cursor = System.Windows.Forms.Cursors.WaitCursor
		Dim selectedItems As String() = e.SelectedObjects
		m_pSDS.DesignMode = True
		For Each s As String In selectedItems
			'setup rule properties
			Dim pRule As ISchematicNodeReductionRuleByPriority = New SchematicNodeReductionRuleByPriorityClass()
			pRule.NodeDegreeConstraint = True
			pRule.ReduceNodeDegree0 = True
			pRule.ReduceNodeDegree2 = True
			pRule.ReduceNodeDegree1 = False
			pRule.ReduceNodeDegreeSup3 = False

			'set the name and class to reduce
			Dim pNR As ISchematicNodeReductionRule = DirectCast(pRule, ISchematicNodeReductionRule)
			pNR.Description = "Remove " & s.ToString()
			pNR.NodeClassName = s.ToString()

			'add it to the template
			pIsbr = pIsbrce.AddSchematicBuilderRule()
			pIsbr.SchematicRule = DirectCast(pRule, ISchematicRule)
		Next

		'save and close
		m_pSDS.Save(ESRI.ArcGIS.esriSystem.esriArcGISVersion.esriArcGISVersion10, False)
		m_pSDS.DesignMode = False
		formReduce.Cursor = System.Windows.Forms.Cursors.Default
		formReduce.Close()
	End Sub

	Private Sub formNames_nextFormEvent(ByVal sender As Object, ByVal e As NameEvents)
		Dim blnCheck As Boolean = False
		'check if we need to create a new dataset
		templateInfo = New NameEvents(e.NewDataset, e.DatasetName, e.TemplateName, e.UseVertices)
		formNames.Cursor = System.Windows.Forms.Cursors.WaitCursor

		If templateInfo.NewDataset = True Then
			blnCheck = CreateDataset(templateInfo)
			If blnCheck = False Then
				'name exists
				blnCancel = True
			End If

			blnCheck = CreateTemplate(templateInfo)
			If blnCheck = False Then
				'name exists
				blnCancel = True
			End If
		Else
			'just create a new template
			blnCheck = CreateTemplate(templateInfo)
			If blnCheck = False Then
				'name exists
				blnCancel = True
			End If
		End If
		formNames.Cursor = System.Windows.Forms.Cursors.Default
		formNames.Close()
	End Sub

	Protected Overrides Sub OnUpdate()
		'Enabled = ArcCatalog.Application IsNot Nothing
		Enabled = ArcCatalog.Application IsNot Nothing
		'If (ArcCatalog.ThisApplication.SelectedObject.Category = "File Geodatabase") OrElse (ArcCatalog.ThisApplication.SelectedObject.Category = "Personal Geodatabase") OrElse (ArcCatalog.ThisApplication.SelectedObject.Category = "Schematic Dataset") OrElse (ArcCatalog.ThisApplication.SelectedObject.Category = "Spatial Database Connection") Then
		If (ArcCatalog.ThisApplication.SelectedObject.Category = "File Geodatabase") OrElse (ArcCatalog.ThisApplication.SelectedObject.Category = "Personal Geodatabase") OrElse (ArcCatalog.ThisApplication.SelectedObject.Category = "Schematic Dataset") OrElse (ArcCatalog.ThisApplication.SelectedObject.Category = "Spatial Database Connection") Then
			Enabled = True
		Else
			Enabled = False
		End If

	End Sub

	Private Sub cleanUp()

		m_pWS = Nothing
		m_pSDT = Nothing
		m_pSDS = Nothing
		m_pSB = Nothing
		m_pB = Nothing
		m_SelectedObject = Nothing
		templateInfo = Nothing
		m_pSDI = Nothing
		formNames = Nothing
		formReduce = Nothing
		m_sfn = ""
		blnCancel = False
		strNodeLayers = ""
		strLayers = ""
	End Sub

End Class
