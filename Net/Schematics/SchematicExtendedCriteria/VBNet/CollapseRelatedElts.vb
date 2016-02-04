'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<System.Runtime.InteropServices.Guid(CollapseRelatedElts.GUID)> _
<System.Runtime.InteropServices.ProgId(CollapseRelatedElts.PROGID)> _
Public Class CollapseRelatedElts
	Implements ESRI.ArcGIS.Schematic.ISchematicCollapseRelatedElementsExtended

	Public Const GUID As String = "BB27AD60-32D9-4f54-B7BB-D170CC15D48E"
	Public Const PROGID As String = "CustomExtCriteriaVB.CollapseRelatedElts"

	' Register/unregister categories for this class
#Region "Component Category Registration"
	<System.Runtime.InteropServices.ComRegisterFunction()> _
	Public Shared Sub Register(ByVal CLSID As String)
		ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Register(CLSID)
	End Sub

	<System.Runtime.InteropServices.ComUnregisterFunction()> _
	Public Shared Sub Unregister(ByVal CLSID As String)
		ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Unregister(CLSID)
	End Sub
#End Region

#Region "ISchematicCollapseRelatedElementsExtended Implementations"
	Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.Schematic.ISchematicCollapseRelatedElementsExtended.Name
		Get
			Return "Test extended collapse (VBNet)"
		End Get
	End Property

	Public Function SelectElementsToCollapse( _
	 ByVal node As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureNode, _
	 ByVal relatedFeatures As ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature) _
	As ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature _
	 Implements ESRI.ArcGIS.Schematic.ISchematicCollapseRelatedElementsExtended.SelectElementsToCollapse

		On Error Resume Next
		' get feature
		Dim esriFeature As ESRI.ArcGIS.Geodatabase.IFeature
		esriFeature = CType(node, ESRI.ArcGIS.Geodatabase.IFeature)
		If (esriFeature Is Nothing) Then Return Nothing

		' get feature class
		Dim esriFeatureClass As ESRI.ArcGIS.Geodatabase.IFeatureClass
		esriFeatureClass = CType(esriFeature.Class, ESRI.ArcGIS.Geodatabase.IFeatureClass)

		' if not the right feature class do nothing
		If (esriFeatureClass.AliasName <> "plants") Then Return Nothing

		Dim okToCollapse As Boolean = True

		relatedFeatures.Reset()

		' Test if you want to collapse related feature
		'Dim schemElement As ESRI.ArcGIS.Schematic.ISchematicElement = relatedElements.Next
		'Do While (schemElement IsNot Nothing AndAlso okToCollapse)
		'	okToCollapse = CanCollapseElement(schemElement)
		'	schemElement = relatedElements.Next
		'Loop

		If Not okToCollapse Then
			' if nothing to collapse return nothing
			Return Nothing
		ElseIf RelatedFeatures.Count = 0 Then
			' create a list of feature to collapse
			Dim enumCollapse As EnumCollapsedElts
			enumCollapse = New EnumCollapsedElts()

			' get incident links
			Dim enumLinks As ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeatureLink
			enumLinks = node.GetIncidentLinks(Schematic.esriSchematicEndPointType.esriSchematicOriginOrExtremityNode)
			If enumLinks Is Nothing Then Return enumCollapse
			If enumLinks.Count > 1 Then
				enumLinks.Reset()
				' for each link 
				Dim schemLink As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureLink
				schemLink = enumLinks.Next
				If schemLink IsNot Nothing Then
					' Add node to collapse
					enumCollapse.Add(node)

					While schemLink IsNot Nothing
						' Add link to collapse
						enumCollapse.Add(schemLink)
						schemLink = enumLinks.Next
					End While
				End If
			End If
			Return enumCollapse
		Else
			Return relatedFeatures
		End If
	End Function
#End Region
End Class