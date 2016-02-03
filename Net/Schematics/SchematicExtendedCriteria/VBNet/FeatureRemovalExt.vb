Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<System.Runtime.InteropServices.Guid(FeatureRemovalExt.GUID)> _
<System.Runtime.InteropServices.ProgId(FeatureRemovalExt.PROGID)> _
Public Class FeatureRemovalExt
	Implements ESRI.ArcGIS.Schematic.ISchematicFeatureRemovalExtended

	Public Const GUID As String = "EBBE13D2-0996-4bb9-BEB8-8781FD8A1A23"
	Public Const PROGID As String = "CustomExtCriteriaVB.FeatureRemovalExt"

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

#Region "ISchematicFeatureRemovalExtended Implementations"
	Public Function Evaluate(ByVal schematicFeature As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature) As Boolean Implements ESRI.ArcGIS.Schematic.ISchematicFeatureRemovalExtended.Evaluate

		' if not the right class do nothing
		If schematicFeature.SchematicElementClass.Name <> "cables" Then Return False

		' Remove specific schematic elements
		If schematicFeature.Name = "5-7-0" Or schematicFeature.Name = "5-4-0" Then
			Return True
		Else
			Return False
		End If
	End Function

	Private ReadOnly Property Name() As String _
	 Implements ESRI.ArcGIS.Schematic.ISchematicFeatureRemovalExtended.Name
		Get
			Return "Remove cables with particular ID (VBNet)"
		End Get
	End Property
#End Region
End Class


