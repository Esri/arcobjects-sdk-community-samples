Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.Controls

' This command allows users to load locations from another point feature layer into the selected NALayer and active category.
Namespace NAEngine
    <Guid("72BDDCB7-03E8-4777-BECA-11DC47EFEDBA"), ClassInterface(ClassInterfaceType.None), ProgId("NAEngine.LoadLocations")> _
    Public NotInheritable Class cmdLoadLocations : Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
        Private m_mapControl As ESRI.ArcGIS.Controls.IMapControl3

        Public Sub New()
            MyBase.m_caption = "Load Locations..."
        End Sub

        Public Overrides Sub OnClick()
            If m_mapControl Is Nothing Then
                MessageBox.Show("Error: Map control is null for this command")
                Return
            End If

            ' Get the NALayer and corresponding NAContext of the layer that
            ' was right-clicked on in the table of contents
            ' m_MapControl.CustomProperty was set in frmMain.axTOCControl1_OnMouseDown
            Dim naLayer As INALayer = TryCast(m_mapControl.CustomProperty, INALayer)
            If naLayer Is Nothing Then
                MessageBox.Show("Error: NALayer was not set as the CustomProperty of the map control")
                Return
            End If

            Dim naEnv As IEngineNetworkAnalystEnvironment = CommonFunctions.GetTheEngineNetworkAnalystEnvironment()
            If naEnv Is Nothing OrElse naEnv.NAWindow Is Nothing Then
                MessageBox.Show("Error: EngineNetworkAnalystEnvironment is not properly configured")
                Return
            End If

            Dim naWindowCategory As ESRI.ArcGIS.Controls.IEngineNAWindowCategory = naEnv.NAWindow.ActiveCategory
            If naWindowCategory Is Nothing Then
                MessageBox.Show("Error: There is no active category for the NAWindow")
                Return
            End If

            Dim naClass As INAClass = naWindowCategory.NAClass
            If naClass Is Nothing Then
                MessageBox.Show("Error: There is no NAClass for the active category")
                Return
            End If

            Dim naClassDefinition As INAClassDefinition = naClass.ClassDefinition
            If naClassDefinition Is Nothing Then
                MessageBox.Show("Error: NAClassDefinition is null for the active NAClass")
                Return
            End If

            If (Not naClassDefinition.IsInput) Then
                MessageBox.Show("Error: Locations can only be loaded into an input NAClass")
                Return
            End If

            ' Set the Active Analysis layer to be the layer right-clicked on
            naEnv.NAWindow.ActiveAnalysis = naLayer

            ' Show the Property Page form for ArcGIS Network Analyst extension
            Dim loadLocations As frmLoadLocations = New frmLoadLocations()
            If loadLocations.ShowModal(m_mapControl, naEnv) Then
                ' Notify that the context has changed, because we have added locations to a NAClass within it
                Dim contextEdit As INAContextEdit = TryCast(naEnv.NAWindow.ActiveAnalysis.Context, INAContextEdit)
                contextEdit.ContextChanged()

                ' Refresh the NAWindow and the map
                m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, m_mapControl.Extent)
                naEnv.NAWindow.UpdateContent(naEnv.NAWindow.ActiveCategory)
            End If
        End Sub

        Public Overrides Sub OnCreate(ByVal hook As Object)
            ' The "hook" was set as a MapControl in formMain_Load
            m_mapControl = TryCast(hook, ESRI.ArcGIS.Controls.IMapControl3)
        End Sub
    End Class
End Namespace
