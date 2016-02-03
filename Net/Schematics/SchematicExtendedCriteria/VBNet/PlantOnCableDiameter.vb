Option Strict Off
Option Explicit On


<System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<System.Runtime.InteropServices.Guid(PlantOnCableDiameter.GUID)> _
<System.Runtime.InteropServices.ProgId(PlantOnCableDiameter.PROGID)> _
Public Class PlantOnCableDiameter
	Implements ESRI.ArcGIS.Schematic.ISchematicNodeReductionExtended

	Public Const GUID As String = "A42296B1-BDED-4d92-AEE6-59E47CBEA6EF"
	Public Const PROGID As String = "CustomExtCriteriaVB.PlantOnCableDiameter"

	' Register/unregister categories for this class
#Region "Component Category Registration"
	<System.Runtime.InteropServices.ComRegisterFunction()> _
	Public Shared Sub Register(ByVal regKey As String)
		ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Register(regKey)
	End Sub

	<System.Runtime.InteropServices.ComUnregisterFunction()> _
	Public Shared Sub Unregister(ByVal regKey As String)
		ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Unregister(regKey)
	End Sub
#End Region

	' Implementation of a new ISchematicNodeReductionExtended interface to be used as an additional criteria during the execution of a Node Reduction By Priority rule

#Region "ISchematicNodeReductionExtended Implementations"
	' Description of the new schematic node reduction criteria
	'Private ReadOnly Property ISchematicNodeReductionExtended_Name() As String _
	'Implements Schematic.ISchematicNodeReductionExtended.Name
	Private ReadOnly Property Name() As String _
	 Implements ESRI.ArcGIS.Schematic.ISchematicNodeReductionExtended.Name
		Get
			Return "Reduce if connected cable diameters are 8 (VBNet)"
		End Get
	End Property

	'The SelectReduction procedure works with the input pNode schematic node candidate to the 
	'reduction and with the input pEnumLink list of schematic link elements incident to this 
	'schematic node.  It must return True for the output Reduce boolean parameter if the pNode 
	'is reduced, false if the pNode is kept.	When the output ppLink schematic link is not 
	'nothing, it determines the target node that will be used to reconnect the links incidents 
	'to the reduced node.	 In this sample procedure, the set of links incident to the node 
	'candidate to the reduction is analyzed. For each of them, the procedure checks the value 
	'of the first field (diameter of the incident cable). If all values are 8, the returned 
	'output reduce parameter is True.
	Public Function SelectReduction( _
	ByVal node As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureNode, _
	ByVal enumLink As ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature, _
	ByRef link As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureLink) As Boolean _
	Implements ESRI.ArcGIS.Schematic.ISchematicNodeReductionExtended.SelectReduction

		On Error Resume Next
		' if enumLink is empty do nothing
		If (enumLink Is Nothing) Then Return False
		If (enumLink.Count = 0) Then Return False

		enumLink.Reset()

		Dim schemAssociatedLink As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature
		schemAssociatedLink = enumLink.Next
		' for each link in enumLink
		While (schemAssociatedLink IsNot Nothing)
			' get cables
			Dim cablesFeature As ESRI.ArcGIS.Geodatabase.IFeature
			cablesFeature = CType(schemAssociatedLink.SchematicElement, ESRI.ArcGIS.Schematic.ISchematicElementAssociatedObject)
			If (cablesFeature Is Nothing) Then Return False

			' get cables class
			Dim cablesDataset As ESRI.ArcGIS.Geodatabase.IDataset
			cablesDataset = cablesFeature.Class

			' if not the right class do nothing
			If (cablesDataset.Name.IndexOf("cables") = 0) Then Return False

			' get workspace
			Dim cablesWorkspace As ESRI.ArcGIS.Geodatabase.IFeatureWorkspace
			cablesWorkspace = cablesDataset.Workspace

			' open table cables_attributes
			Dim cablesTable As ESRI.ArcGIS.Geodatabase.ITable
			cablesTable = cablesWorkspace.OpenTable("cables_attributes")
			If (cablesTable Is Nothing) Then Return False

			' get diameter value, if not 8 do nothing
			If (cablesTable.GetRow(cablesFeature.OID).Value(1) <> 8) Then Return False

			schemAssociatedLink = enumLink.Next
		End While

		Return True			' True if this far
	End Function
#End Region

End Class