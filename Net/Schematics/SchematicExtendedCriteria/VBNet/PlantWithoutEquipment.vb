Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<System.Runtime.InteropServices.Guid(PlantWithoutEquipment.GUID)> _
<System.Runtime.InteropServices.ProgId(PlantWithoutEquipment.PROGID)> _
Public Class PlantWithoutEquipment
	Implements ESRI.ArcGIS.Schematic.ISchematicNodeReductionExtended

	Public Const GUID As String = "6C12F9A4-3265-4a9e-9CB6-7FBA618154F1"
	Public Const PROGID As String = "CustomExtCriteriaVB.PlantWithoutEquipment"

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


#Region "ISchematicNodeReductionExtended Implementations"
	' Implementation of a new ISchematicNodeReductionExtended interface 
	' to be used as an additional criteria during the execution of a 
	' Node Reduction By Priority rule
	' Description of the new schematic node reduction criteria
	Private ReadOnly Property Name() As String _
	 Implements ESRI.ArcGIS.Schematic.ISchematicNodeReductionExtended.Name

		Get
			Return "Reduce plant without equipments (VBNet)"
		End Get
	End Property

	'The SelectReduction procedure works with the input node schematic node candidate to the 
	'reduction and with the input linkElements list of schematic link elements incident to 
	'this schematic node.	It must return True for the output reduce boolean parameter if 
	'the node is reduced, false if the node is kept.	 When the output ppLink schematic link 
	'is not nothing, it determines the target node that will be used to reconnect the links 
	'incidents to the reduced node.	In this sample procedure, the node candidate to the 
	'reduction is analyzed. If records related to this node exist in the plants_equipments table, 
	'the node is kept (output reduce parameter is False); else, it is reduced (output reduce 
	'parameter is True).

	Public Function SelectReduction( _
    ByVal node As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureNode, _
    ByVal enumLink As ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature, _
    ByRef link As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureLink) As Boolean _
    Implements ESRI.ArcGIS.Schematic.ISchematicNodeReductionExtended.SelectReduction

        ' if associated feature doesn't exist do nothing
        Dim schemAssociatedNode As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature
        schemAssociatedNode = CType(node, ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature)
        If (schemAssociatedNode Is Nothing) Then Return False

        Dim schemElementClass As ESRI.ArcGIS.Geodatabase.IDataset

        schemElementClass = CType(schemAssociatedNode.SchematicElementClass, ESRI.ArcGIS.Geodatabase.IDataset)
        If (schemElementClass Is Nothing) Then Return False
        If (schemElementClass.Name.IndexOf("plants") < 0) Then Return False

        Dim plantsWorkspace As ESRI.ArcGIS.Geodatabase.IFeatureWorkspace
        plantsWorkspace = CType(schemElementClass.Workspace, ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)

        Dim plantsEquipment As ESRI.ArcGIS.Geodatabase.ITable
        plantsEquipment = plantsWorkspace.OpenTable("plants_equipments")
        If (plantsEquipment Is Nothing) Then Return False

        ' filter for the selected feature
        Dim schemAssociation As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeaturePrimaryAssociation
        schemAssociation = CType(schemAssociatedNode, ESRI.ArcGIS.Schematic.ISchematicInMemoryFeaturePrimaryAssociation)
        If (schemAssociation Is Nothing) Then Return False

        Dim plantsFilter As New ESRI.ArcGIS.Geodatabase.QueryFilterClass()
        plantsFilter.WhereClause = "PlantID = " & schemAssociation.ObjectID

        Dim plantsCursor As ESRI.ArcGIS.Geodatabase.ICursor
        plantsCursor = plantsEquipment.Search(plantsFilter, True)

        ' if found equipment return false
        If (plantsCursor IsNot Nothing) AndAlso (plantsCursor.NextRow IsNot Nothing) Then Return False

        Return True     ' if this far
    End Function
#End Region

End Class