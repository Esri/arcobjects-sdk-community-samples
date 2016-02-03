Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Geodatabase

<ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<Guid(ExpandLinks.GUID)> _
<ProgId(ExpandLinks.PROGID)> _
Public Class ExpandLinks
	Implements ISchematicExpandLinksByAttributeExtended

	Public Const GUID As String = "D98900E4-B15D-4610-B42D-5D70E368FBC2"
	Public Const PROGID As String = "CustomExtCriteriaVB.ExpandLinks"

#Region "Component Category Registration"
	<ComRegisterFunction()> _
	Public Shared Sub Register(ByVal CLSID As String)
		ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Register(CLSID)
	End Sub

	<ComUnregisterFunction()> _
	Public Shared Sub Unregister(ByVal CLSID As String)
		ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Unregister(CLSID)
	End Sub
#End Region

#Region "ISchematicExpandLinksByAttributeExtended Members"

	Public Function Evaluate(ByVal schematicFeature As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature) As String _
	 Implements ESRI.ArcGIS.Schematic.ISchematicExpandLinksByAttributeExtended.Evaluate

		If (schematicFeature.SchematicElementClass.SchematicElementType <> esriSchematicElementType.esriSchematicLinkType) Then Return ""

		Dim schemLink As ISchematicInMemoryFeatureLink
		schemLink = CType(schematicFeature, ISchematicInMemoryFeatureLink)

		' Get From node
		Dim schemFromNode As ISchematicInMemoryFeatureNode
		schemFromNode = schemLink.FromNode

		' Get Associated feature
		Dim scheAsso As ISchematicElementAssociatedObject
		scheAsso = CType(schemFromNode.SchematicElement, ISchematicElementAssociatedObject)
		If scheAsso Is Nothing Then Return ""

		Dim esriFeature As IFeature
		esriFeature = CType(scheAsso.AssociatedObject, IFeature)

		' Get list of fields
		Dim esriFields As IFields
		esriFields = esriFeature.Fields
		Dim numField As Integer
		' Find Field MaxOutLines
		numField = esriFields.FindFieldByAliasName("MaxOutLines")
		If (numField <= 0) Then Return "" ' Field not found

		' Return value of the field
		Return esriFeature.Value(numField).ToString()
	End Function

	Public ReadOnly Property Name() As String Implements ISchematicExpandLinksByAttributeExtended.Name
		Get
            Return "Use origin plant's MaxOutLines value (VBNet)"
		End Get
	End Property

#End Region
End Class
