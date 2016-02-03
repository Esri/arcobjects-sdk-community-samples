Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

' This command brings up the property pages for the ArcGIS Network Analyst extension environment.
Namespace NAEngine
	<Guid("7E98FE97-DA7A-4069-BC85-091D75B1AF65"), ClassInterface(ClassInterfaceType.None), ProgId("NAEngine.NAProperties")> _
	Public NotInheritable Class cmdNAProperties : Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
		Public Sub New()
			MyBase.m_caption = "Properties..."
		End Sub

		Public Overrides Sub OnClick()
            ' Show the Property Page form for ArcGIS Network Analyst extension
            Dim props As frmNAProperties = New frmNAProperties()
			props.ShowModal()
		End Sub

		Public Overrides Sub OnCreate(ByVal hook As Object)
			' Since this ToolbarMenu item is on the ToolbarControl the Hook is initialized by the ToolbarControl.
            Dim toolbarControl As ESRI.ArcGIS.Controls.IToolbarControl = TryCast(hook, ESRI.ArcGIS.Controls.IToolbarControl)
		End Sub
	End Class
End Namespace
