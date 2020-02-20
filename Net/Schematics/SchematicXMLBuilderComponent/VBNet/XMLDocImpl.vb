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
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Schematic
Imports System.Windows.Forms

<Guid("A87C1C4A-1827-45e7-980B-4A5C781CF404")> _
 <ClassInterface(ClassInterfaceType.None)> _
 <ProgId("MyExtXmlComponentVB.XMLDocImpl")> _
 Public Class XMLDocImpl

    Implements ISchematicXmlGenerate
    Implements ISchematicXmlUpdate
    '
#Region "Variables"
	'This class must implement the ISchematicXMLGenerate & ISchematicXMLUpdate interfaces

    Private m_application As ESRI.ArcGIS.ArcMap.Application                     ' ArcMap application
    Private m_mxDocument As ESRI.ArcGIS.ArcMapUI.IMxDocument                ' ArcMap document
    Private installationFolder As String

    'The following arrays will be used to create the wished propertyset properties in the XML DOMDocument
    Private m_stationsPropertiesArray() As String = {"Name", "Capacity", "Type", "Feeder"}
    Private m_feedersPropertiesArray() As String = {"Feeder_Description"}
    Private m_LVLinesPropertiesArray() As String = {"Category"}
    Private Const DatasetName As String = "ElectricDataSet"


    Protected Overrides Sub Finalize()
        m_application = Nothing
        m_mxDocument = Nothing
        MyBase.Finalize()
    End Sub
#End Region

#Region "ISchematicXmlGenerate Members"

	Public Sub GenerateXmlData(ByVal diagramName As String, ByVal diagramClassName As String, ByRef xmlSource As Object, ByRef cancel As Boolean) Implements ISchematicXmlGenerate.GenerateXmlData
		Dim xmlDocument As MSXML2.DOMDocument = New MSXML2.DOMDocument()
		Dim maps As ESRI.ArcGIS.Carto.IMaps
		Dim currentMap As ESRI.ArcGIS.Carto.IMap
		Dim enumFeature As ESRI.ArcGIS.Geodatabase.IEnumFeature
		Dim feature As ESRI.ArcGIS.Geodatabase.IFeature
		Dim xmlProcInstr As MSXML2.IXMLDOMProcessingInstruction
		Dim xmlDiagrams As MSXML2.IXMLDOMElement
		Dim xmlDiagram As MSXML2.IXMLDOMElement
		Dim xmlFeatures As MSXML2.IXMLDOMElement
		Dim xmlDataSources As MSXML2.IXMLDOMElement
		Dim xmlDataSource As MSXML2.IXMLDOMElement
		Dim xmlDataSource_NameString As MSXML2.IXMLDOMElement
		Dim xmlDataSource_WorkspaceInfo As MSXML2.IXMLDOMElement
		Dim xmlWorkspaceInfo_PathName As MSXML2.IXMLDOMElement
		Dim xmlWorkspaceInfo_WorkspaceFactoryProgID As MSXML2.IXMLDOMElement
		Dim rootAtt1 As MSXML2.IXMLDOMAttribute
		Dim rootAtt2 As MSXML2.IXMLDOMAttribute
		Dim enumFeatureSetup As ESRI.ArcGIS.Geodatabase.IEnumFeatureSetup
		Dim xmlDatabase As String

		' Retrieving the selected set of features
		enumFeature = Nothing
		feature = Nothing

		m_mxDocument = CType(m_application.Document, ESRI.ArcGIS.ArcMapUI.IMxDocument)
		maps = m_mxDocument.Maps
		Dim i As Integer = 0
		While (i < maps.Count)
			currentMap = maps.Item(i)
			enumFeature = CType(currentMap.FeatureSelection, ESRI.ArcGIS.Geodatabase.IEnumFeature)
			enumFeatureSetup = CType(enumFeature, ESRI.ArcGIS.Geodatabase.IEnumFeatureSetup)
			enumFeatureSetup.AllFields = True
			feature = enumFeature.Next()
			If (feature IsNot Nothing) Then Exit While

			i += 1
		End While

		' if (there is no selected feature in the MxDocument, the procedure is interrupted
		If (feature Is Nothing) Then
			MsgBox("There is no feature selected. Select a set of features.", "Generate/Update XML diagrams", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
			cancel = True
			Return
		End If

		'Checking the feature dataset related to the selected features
		Dim featureClass As ESRI.ArcGIS.Geodatabase.IFeatureClass
		Dim featureDatasetName As String
		featureClass = CType(feature.Class, ESRI.ArcGIS.Geodatabase.IFeatureClass)
		featureDatasetName = featureClass.FeatureDataset.BrowseName

		xmlDatabase = featureClass.FeatureDataset.Workspace.PathName
		' if the selected features come from another feature dataset than the expected one, the procedure is interrupted
		If (featureDatasetName <> DatasetName) Then
			'More restrictive condition: If (xmlDatabase <> "c:\Mybase.gdb") Then
			MsgBox("This component doesn't work from the selected set of features.")
			cancel = True
			Return
		End If

		' Writing the XML heading items in the DOMDocument
		xmlProcInstr = xmlDocument.createProcessingInstruction("xml", "version='1.0'")
		xmlDocument.appendChild(xmlProcInstr)
		xmlProcInstr = Nothing

		'-------- Diagrams Section START --------
		' Creating the root Diagrams element
		xmlDiagrams = xmlDocument.createElement("Diagrams")
		xmlDocument.documentElement = xmlDiagrams
		rootAtt1 = xmlDocument.createAttribute("xmlns:xsi")
		rootAtt1.text = "http://www.w3.org/2001/XMLSchema-instance"
		xmlDiagrams.attributes.setNamedItem(rootAtt1)
		'rootAtt2 = xmlDocument.createAttribute("xsi:noNamespaceSchemaLocation")
		'rootAtt2.text = XmlSchema
		'xmlDiagrams.attributes.setNamedItem(rootAtt2)

		' Creating the Diagram element for the diagram which is going to be generated
		xmlDiagram = xmlDocument.createElement("Diagram")
		xmlDiagrams.appendChild(xmlDiagram)
		rootAtt1 = xmlDocument.createAttribute("EnforceDiagramTemplateName")
		rootAtt1.text = "false"
		xmlDiagram.attributes.setNamedItem(rootAtt1)
		rootAtt2 = xmlDocument.createAttribute("EnforceDiagramName")
		rootAtt2.text = "false"
		xmlDiagram.attributes.setNamedItem(rootAtt2)

		'-------- DataSources Section START --------
		' Creating the DataSources element 
		xmlDataSources = xmlDocument.createElement("Datasources")
		xmlDiagram.appendChild(xmlDataSources)
		xmlDataSource = xmlDocument.createElement("Datasource")
		xmlDataSources.appendChild(xmlDataSource)

		' Specifying the Namestring for the related Datasource element
		xmlDataSource_NameString = xmlDocument.createElement("NameString")
		xmlDataSource.appendChild(xmlDataSource_NameString)
		xmlDataSource_NameString.nodeTypedValue = "XMLDataSource"

		' Specifying the WorkspaceInfo for the related Datasource element
		xmlDataSource_WorkspaceInfo = xmlDocument.createElement("WorkSpaceInfo")
		xmlDataSource.appendChild(xmlDataSource_WorkspaceInfo)
		xmlWorkspaceInfo_PathName = xmlDocument.createElement("PathName")
		xmlDataSource_WorkspaceInfo.appendChild(xmlWorkspaceInfo_PathName)
		xmlWorkspaceInfo_PathName.nodeTypedValue = xmlDatabase
		xmlWorkspaceInfo_WorkspaceFactoryProgID = xmlDocument.createElement("WorkspaceFactoryProgID")
		xmlDataSource_WorkspaceInfo.appendChild(xmlWorkspaceInfo_WorkspaceFactoryProgID)
		xmlWorkspaceInfo_WorkspaceFactoryProgID.nodeTypedValue = "esriDataSourcesGDB.FileGDBWorkspaceFactory"
		'-------- DataSources Section END --------

		'-------- Features Section START --------
		xmlFeatures = xmlDocument.createElement("Features")
		xmlDiagram.appendChild(xmlFeatures)
		While (feature IsNot Nothing)
			Select Case (feature.FeatureType)
				Case ESRI.ArcGIS.Geodatabase.esriFeatureType.esriFTSimpleJunction
					CreateXMLNodeElt(feature, xmlDocument, xmlFeatures, feature.Class.AliasName)
				Case ESRI.ArcGIS.Geodatabase.esriFeatureType.esriFTSimpleEdge
					CreateXMLLinkElt(feature, xmlDocument, xmlFeatures, feature.Class.AliasName)
			End Select
			feature = enumFeature.Next()
		End While

		' output the XML we created
		xmlSource = xmlDocument
		cancel = False
		'-------- Features Section END --------
		'-------- Diagrams Section END --------

	End Sub

	Public Property ApplicationHook() As Object Implements ISchematicXmlGenerate.ApplicationHook
		Get
			Return CType(m_application, Object)
		End Get
		Set(ByVal value As Object)
			m_application = CType(value, ESRI.ArcGIS.ArcMap.Application)
		End Set
	End Property
#End Region

#Region "ISchematicXmlUpdate Members"

	Public Sub UpdateXmlData(ByVal diagramName As String, ByVal diagramClassName As String, ByVal updateInformation As String, ByRef xmlSource As Object, ByRef cancel As Boolean) Implements ISchematicXmlUpdate.UpdateXmlData
		GenerateXmlData(diagramName, diagramClassName, xmlSource, cancel)
	End Sub

	Public Property ApplicationHook1() As Object Implements ISchematicXmlUpdate.ApplicationHook
		Get
			Return CType(m_application, Object)
		End Get
		Set(ByVal value As Object)
			m_application = CType(value, ESRI.ArcGIS.ArcMap.Application)
		End Set
	End Property

#End Region

#Region "private functions"
	' The following CreateXMLLNodeElt private procedure is used to create all the expected 
	' XML items for a XML NodeFeature related to a Station or Feeder simple junction feature
	Private Sub CreateXMLNodeElt(ByVal inFeature As ESRI.ArcGIS.Geodatabase.IFeature, ByRef outDOMDoc As MSXML2.DOMDocument, ByRef outXMLElements As MSXML2.IXMLDOMElement, ByVal inNodeTypeName As String)

		If (Not inFeature.HasOID) Then
			MsgBox("No OID")
			Return
		End If

		Dim xmlNode As MSXML2.IXMLDOMElement
		Dim xmlNode_XCoord As MSXML2.IXMLDOMElement
		Dim xmlNode_YCoord As MSXML2.IXMLDOMElement
		Dim xmlNode_RelatedContainerID As MSXML2.IXMLDOMElement
		Dim relatedContainer As Boolean
		Dim xmlNodeList As MSXML2.IXMLDOMNodeList
		Dim xmlDrawing As MSXML2.IXMLDOMElement
		Dim xmlDrawing_EltTypeName As MSXML2.IXMLDOMElement
		Dim xmlDrawing_ExternalUID As MSXML2.IXMLDOMElement

		'-------- Feature Section START related to the "infeature" --------
		' Creating the NodeFeature element
		xmlNode = outDOMDoc.createElement("NodeFeature")
		outXMLElements.appendChild(xmlNode)
		' Specifying basic XML items for this NodeFeature
		CreateBasicXMLItemsForSchematicElt(inFeature, outDOMDoc, xmlNode, inNodeTypeName)

		' Specifying its X && Y when they exist
		If ((inFeature.Fields.FindField("X") > 0) AndAlso (inFeature.Fields.FindField("Y") > 0)) Then
			' Specifying InitialX
			xmlNode_XCoord = outDOMDoc.createElement("InitialX")
			xmlNode.appendChild(xmlNode_XCoord)
			xmlNode_XCoord.nodeTypedValue = inFeature.Value(inFeature.Fields.FindField("X"))
			' Specifying InitialY
			xmlNode_YCoord = outDOMDoc.createElement("InitialY")
			xmlNode.appendChild(xmlNode_YCoord)
			xmlNode_YCoord.nodeTypedValue = inFeature.Value(inFeature.Fields.FindField("Y"))
		Else
			' Retrieving initial position from Geometry
			Dim oPoint As ESRI.ArcGIS.Geometry.IPoint = TryCast(inFeature.ShapeCopy, ESRI.ArcGIS.Geometry.IPoint)

			If (oPoint IsNot Nothing) Then
				' Specifying InitialX
				xmlNode_XCoord = outDOMDoc.createElement("InitialX")
				xmlNode.appendChild(xmlNode_XCoord)
				xmlNode_XCoord.nodeTypedValue = oPoint.X
				' Specifying InitialY
				xmlNode_YCoord = outDOMDoc.createElement("InitialY")
				xmlNode.appendChild(xmlNode_YCoord)
				xmlNode_YCoord.nodeTypedValue = oPoint.Y
			End If
		End If

		xmlNode_RelatedContainerID = outDOMDoc.createElement("RelatedContainerID")
		xmlNode.appendChild(xmlNode_RelatedContainerID)

		' Specifying its properties 
		Select Case (inFeature.Class.AliasName)
			Case "Station"
				xmlNode_RelatedContainerID.nodeTypedValue = "Container-" & System.Convert.ToString(inFeature.Value(inFeature.Fields.FindField("Feeder")))
				' For Station feature, the field contained in the StationsPropertiesArray will be exported
				CompleteXMLEltByProperties(inFeature, outDOMDoc, xmlNode, m_stationsPropertiesArray)
			Case "Feeder"
				xmlNode_RelatedContainerID.nodeTypedValue = "Container-" & inFeature.OID.ToString()
				' For Feeder feature, the field contained in the StationsPropertiesArray will be exported          
				CompleteXMLEltByProperties(inFeature, outDOMDoc, xmlNode, m_feedersPropertiesArray)
		End Select
		'-------- Feature Section END related to the "infeature" --------

		' Checking the existence of the related container 
		xmlNodeList = outXMLElements.selectNodes("NodeFeature/ExternalUniqueID")
		relatedContainer = False

		For Each node As MSXML2.IXMLDOMNode In xmlNodeList
			If (node.text = xmlNode_RelatedContainerID.nodeTypedValue.ToString()) Then
				relatedContainer = True
				Exit For
			End If
		Next ' pNode

		' Creating the related container when it doesn//t already exist
		If (Not relatedContainer) Then
			xmlDrawing = outDOMDoc.createElement("NodeFeature")
			outXMLElements.appendChild(xmlDrawing)
			' Specifying its FeatureClassName
			xmlDrawing_EltTypeName = outDOMDoc.createElement("FeatureClassName")
			xmlDrawing.appendChild(xmlDrawing_EltTypeName)
			xmlDrawing_EltTypeName.nodeTypedValue = "Containers"
			' Specifying its ExternalUniqueID
			xmlDrawing_ExternalUID = outDOMDoc.createElement("ExternalUniqueID")
			xmlDrawing.appendChild(xmlDrawing_ExternalUID)
			xmlDrawing_ExternalUID.nodeTypedValue = xmlNode_RelatedContainerID.nodeTypedValue
		End If
	End Sub

	' The following CreateXMLLinkElt private procedure is used to create all the expected XML items for a XML LinkFeature related to a HV_Line or LV_Line simple edge feature
	Private Sub CreateXMLLinkElt(ByVal inFeature As ESRI.ArcGIS.Geodatabase.IFeature, ByRef outDOMDoc As MSXML2.DOMDocument, ByRef outXMLElements As MSXML2.IXMLDOMElement, ByVal inLinkTypeName As String)
		If (Not inFeature.HasOID) Then
			MessageBox.Show("No OID")
			Return
		End If

		Dim xmlLink As MSXML2.IXMLDOMElement
		Dim xmlLink_FromNode As MSXML2.IXMLDOMElement
		Dim xmlLink_ToNode As MSXML2.IXMLDOMElement
		Dim indexListPoints As Integer
		Dim listPoints As String
		Dim nbVertices As Integer
		Dim vertices As String
		Dim xmlLink_Vertices As MSXML2.IXMLDOMElement
		Dim xmlLink_Vertex As MSXML2.IXMLDOMElement
		Dim xmlLink_XVertex As MSXML2.IXMLDOMElement
		Dim xmlLink_YVertex As MSXML2.IXMLDOMElement
		Dim xValue As String
		Dim yValue As String

		'-------- Feature Section START related to the "infeature" --------
		' Creating the LinkFeature Feature
		xmlLink = outDOMDoc.createElement("LinkFeature")
		outXMLElements.appendChild(xmlLink)

		' Specifying basic XML items for this LinkFeature
		CreateBasicXMLItemsForSchematicElt(inFeature, outDOMDoc, xmlLink, inLinkTypeName)
		' Specifying its FromNode
		xmlLink_FromNode = outDOMDoc.createElement("FromNode")
		xmlLink.appendChild(xmlLink_FromNode)
		xmlLink_FromNode.nodeTypedValue = inFeature.Value(inFeature.Fields.FindField("FromJunctionType")) & "-" & inFeature.Value(inFeature.Fields.FindField("FromJunctionOID"))
		' Specifying its ToNode
		xmlLink_ToNode = outDOMDoc.createElement("ToNode")
		xmlLink.appendChild(xmlLink_ToNode)
		xmlLink_ToNode.nodeTypedValue = inFeature.Value(inFeature.Fields.FindField("ToJunctionType")) & "-" & inFeature.Value(inFeature.Fields.FindField("ToJunctionOID"))

		'Add Vertices to LinkFeature ---- NEED TO BE COMPLETED
		indexListPoints = inFeature.Fields.FindField("ListPoints")
		If (indexListPoints > 0) Then
			listPoints = ""
			listPoints = inFeature.Value(indexListPoints).ToString()
			If (listPoints <> "") Then
				Dim foundChar As Integer = listPoints.IndexOf(";", 1)
				nbVertices = System.Convert.ToInt32(listPoints.Substring(0, foundChar))
				vertices = listPoints.Substring(foundChar + 1)
				If (nbVertices > 0) Then
					' Specifying its Vertices
					xmlLink_Vertices = outDOMDoc.createElement("Vertices")
					xmlLink.appendChild(xmlLink_Vertices)

					Dim iLoc As Integer
					For i As Integer = 1 To nbVertices
						xValue = ""
						yValue = ""
						iLoc = vertices.IndexOf(";", 1)
						If (vertices <> "" AndAlso (iLoc) > 0) Then
							xValue = vertices.Substring(0, iLoc)
						End If
						vertices = vertices.Substring(iLoc + 1)
						iLoc = vertices.IndexOf(";", 1)
						If (vertices <> ";" AndAlso (iLoc) > 0) Then
							yValue = vertices.Substring(0, iLoc)
						End If

						If (xValue <> "" AndAlso yValue <> "") Then
							xmlLink_Vertex = outDOMDoc.createElement("Vertex")
							xmlLink_Vertices.appendChild(xmlLink_Vertex)
							xmlLink_XVertex = outDOMDoc.createElement("X")
							xmlLink_Vertex.appendChild(xmlLink_XVertex)
							xmlLink_XVertex.nodeTypedValue = xValue
							xmlLink_YVertex = outDOMDoc.createElement("Y")
							xmlLink_Vertex.appendChild(xmlLink_YVertex)
							xmlLink_YVertex.nodeTypedValue = yValue
							If (vertices.Length - iLoc > 0) Then
								vertices = vertices.Substring(iLoc + 1)	'sVertices.Length - iLoc)
							Else
								Exit For
							End If
						Else
							Exit For
						End If
					Next
				End If
			End If
		Else
			' Retrieving ListPoint from geometry
			Dim oPoly As ESRI.ArcGIS.Geometry.IPolyline = TryCast(inFeature.ShapeCopy, ESRI.ArcGIS.Geometry.IPolyline)
			Dim colLink As ESRI.ArcGIS.Geometry.IPointCollection = TryCast(oPoly, ESRI.ArcGIS.Geometry.IPointCollection)
			If (colLink IsNot Nothing AndAlso colLink.PointCount > 2) Then
				Dim oPoint As ESRI.ArcGIS.Geometry.IPoint

				xmlLink_Vertices = outDOMDoc.createElement("Vertices")
				xmlLink.appendChild(xmlLink_Vertices)
				For i As Integer = 1 To colLink.PointCount - 2
					oPoint = colLink.Point(i)

					xmlLink_Vertex = outDOMDoc.createElement("Vertex")
					xmlLink_Vertices.appendChild(xmlLink_Vertex)
					xmlLink_XVertex = outDOMDoc.createElement("X")
					xmlLink_Vertex.appendChild(xmlLink_XVertex)
					xmlLink_XVertex.nodeTypedValue = oPoint.X
					xmlLink_YVertex = outDOMDoc.createElement("Y")
					xmlLink_Vertex.appendChild(xmlLink_YVertex)
					xmlLink_YVertex.nodeTypedValue = oPoint.Y
				Next
			End If
		End If

		'Specifying its properties
		Select Case (inFeature.Class.AliasName)
			Case "LV_Line"
				CompleteXMLEltByProperties(inFeature, outDOMDoc, xmlLink, m_LVLinesPropertiesArray)
		End Select
		'-------- Feature Section END related to the "infeature" --------
	End Sub


	' The following CreateBasicXMLItmesForSchematicElt private procedure is used to create the first expected XML items for a XML NodeFeature or LinkFeature
	Private Sub CreateBasicXMLItemsForSchematicElt(ByVal inFeature As ESRI.ArcGIS.Geodatabase.IFeature, ByRef outDOMDoc As MSXML2.DOMDocument, ByRef outXMLElement As MSXML2.IXMLDOMElement, ByVal inEltTypeName As String)
		Dim xmlElt_EltTypeName As MSXML2.IXMLDOMElement
		Dim xmlElt_ExternalUID As MSXML2.IXMLDOMElement
		Dim xmlElt_DatasourceName As MSXML2.IXMLDOMElement
		Dim xmlElt_UCID As MSXML2.IXMLDOMElement
		Dim xmlElt_UOID As MSXML2.IXMLDOMElement

		' Specifying its FeatureClassName
		xmlElt_EltTypeName = outDOMDoc.createElement("FeatureClassName")
		outXMLElement.appendChild(xmlElt_EltTypeName)
		If (inFeature.Fields.FindField("Feeder") <> -1) Then
			xmlElt_EltTypeName.nodeTypedValue = inEltTypeName & "sFeeder" & inFeature.Value(inFeature.Fields.FindField("Feeder")).ToString()
		Else
			xmlElt_EltTypeName.nodeTypedValue = inEltTypeName & "s"
		End If

		' Specifying its ExternalUniqueID
		xmlElt_ExternalUID = outDOMDoc.createElement("ExternalUniqueID")
		outXMLElement.appendChild(xmlElt_ExternalUID)
		xmlElt_ExternalUID.nodeTypedValue = inEltTypeName & "-" & inFeature.OID.ToString()

		' Specifying its DatasourceName
		xmlElt_DatasourceName = outDOMDoc.createElement("DatasourceName")
		outXMLElement.appendChild(xmlElt_DatasourceName)
		xmlElt_DatasourceName.nodeTypedValue = "XMLDataSource"

		' Specifying its UCID
		xmlElt_UCID = outDOMDoc.createElement("UCID")
		outXMLElement.appendChild(xmlElt_UCID)
		xmlElt_UCID.nodeTypedValue = inFeature.Class.ObjectClassID

		' Add UOID to NodeElement
		xmlElt_UOID = outDOMDoc.createElement("UOID")
		outXMLElement.appendChild(xmlElt_UOID)
		xmlElt_UOID.nodeTypedValue = inFeature.OID
	End Sub

	' The following CompleteXMLEltByProperties private procedure is used to create all the expected propertyset properties listed in the input PropertiesArray array
	Private Sub CompleteXMLEltByProperties(ByVal inFeature As ESRI.ArcGIS.Geodatabase.IFeature, ByRef outDOMDoc As MSXML2.DOMDocument, ByRef outXMLElement As MSXML2.IXMLDOMElement, ByVal propertiesArray() As String)

		Dim i As Integer = 0
		Dim xmlPropertySet As MSXML2.IXMLDOMElement
		Dim xmlPropertyArray As MSXML2.IXMLDOMElement
		Dim xmlPropertySetProperty As MSXML2.IXMLDOMElement
		Dim xmlProperty_Key As MSXML2.IXMLDOMElement
		Dim xmlProperty_Value As MSXML2.IXMLDOMElement

		If (propertiesArray.Length > 0) Then
			'-------- PropertySet Section START --------
			' Creating the PropertySet element for the input outXMLElement
			xmlPropertySet = outDOMDoc.createElement("PropertySet")
			outXMLElement.appendChild(xmlPropertySet)
			' Creating the PropertyArray element
			xmlPropertyArray = outDOMDoc.createElement("PropertyArray")
			xmlPropertySet.appendChild(xmlPropertyArray)

			While (i < propertiesArray.Length)
				' Creating the i PropertySetProperty
				xmlPropertySetProperty = outDOMDoc.createElement("PropertySetProperty")
				xmlPropertyArray.appendChild(xmlPropertySetProperty)
				' Specifying the key && value field related to that i PropertySetProperty
				xmlProperty_Key = outDOMDoc.createElement("Key")
				xmlPropertySetProperty.appendChild(xmlProperty_Key)
				xmlProperty_Key.nodeTypedValue = propertiesArray(i).ToString()
				xmlProperty_Value = outDOMDoc.createElement("Value")
				xmlPropertySetProperty.appendChild(xmlProperty_Value)
				xmlProperty_Value.nodeTypedValue = inFeature.Value(inFeature.Fields.FindField(propertiesArray(i).ToString()))
				i += 1
			End While
		End If
		'-------- PropertySet Section END --------
	End Sub
#End Region
End Class

