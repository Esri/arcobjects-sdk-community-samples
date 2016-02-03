Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.EditorExt
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

Public Class SetFlowByDigitizedDirectionVBNET
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Private m_utilNetExt As IUtilityNetworkAnalysisExt
    Private m_editorExt As IEditor

    Public Sub New()
        Dim uidUtilNet As UID = New UID
        uidUtilNet.Value = "esriEditorExt.UtilityNetworkAnalysisExt"
        m_utilNetExt = CType(My.ArcMap.Application.FindExtensionByCLSID(uidUtilNet), IUtilityNetworkAnalysisExt)

        Dim uidEditor As UID = New UID
        uidEditor.Value = "esriEditor.Editor"
        m_editorExt = CType(My.ArcMap.Application.FindExtensionByCLSID(uidEditor), IEditor)
    End Sub

    Protected Overrides Sub OnClick()
        ' get the current network
        Dim utilNet As IUtilityNetwork = CType(GetCurrentNetwork(), IUtilityNetwork)

        ' create an edit operation enabling an undo for this operation
        m_editorExt.StartOperation()

        ' get a list of the current EIDs for edges in the network
        Dim edgeEIDs As IEnumNetEID = GetCurrentEIDs(esriElementType.esriETEdge)

        ' set the flow direction for each edge in the network
        edgeEIDs.Reset()
        For i = 1 To edgeEIDs.Count
            Dim edgeEID As Integer = edgeEIDs.Next()
            utilNet.SetFlowDirection(edgeEID, esriFlowDirection.esriFDWithFlow)
        Next i

        ' stop the edit operation, specifying a name for this operation
        m_editorExt.StopOperation("Set Flow Direction")

        ' refresh the display to update the flow direction arrows
        Dim mapView As IActiveView = My.ArcMap.Document.ActiveView
        mapView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
    End Sub

    Protected Overrides Sub OnUpdate()
        ' by default, disable the command
        Enabled = False

        ' if there is not a current edit session, then disable the command
        If m_editorExt.EditState <> esriEditState.esriStateEditing Then
            Return
        End If

        ' otherwise, check to see if the flow direction is properly set for each edge EID
        Dim utilNet As IUtilityNetwork = CType(GetCurrentNetwork(), IUtilityNetwork)
        Dim edgeEIDs As IEnumNetEID = GetCurrentEIDs(esriElementType.esriETEdge)
        edgeEIDs.Reset()
        For i = 1 To edgeEIDs.Count
            Dim edgeEID As Integer = edgeEIDs.Next()
            Dim flowDir As esriFlowDirection = utilNet.GetFlowDirection(edgeEID)
            If flowDir <> esriFlowDirection.esriFDWithFlow Then
                ' enable the command if the flow direction is not with the digitized direction
                Enabled = True

                ' we can return right now, since only one edge needs to have
                ' incorrect flow direction in order to enable the command
                Return
            End If
        Next i
    End Sub

    '
    ' returns an enumeration of EIDs of the network elements of the given element type
    '
    Private Function GetCurrentEIDs(ByVal elementType As esriElementType) As IEnumNetEID
        Dim net As INetwork = GetCurrentNetwork()
        Dim eids As IEnumNetEID = net.CreateNetBrowser(esriElementType.esriETEdge)
        Return eids
    End Function

    '
    ' returns a reference to the current logical network
    '
    Private Function GetCurrentNetwork() As INetwork
        ' get the current network from the Utility Network Analysis extension
        Dim nax As INetworkAnalysisExt = CType(m_utilNetExt, INetworkAnalysisExt)
        Dim geomNet As IGeometricNetwork = nax.CurrentNetwork

        ' get the geometric network's logical network
        Dim net As INetwork = geomNet.Network

        Return net
    End Function
End Class