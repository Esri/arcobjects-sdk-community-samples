Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
 
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF.CATIDs
 
Namespace Core
  Public partial Class SnapEditor
	 Inherits Form
    Dim editor As IEngineEditor
    Dim editLayers As IEngineEditLayers
    Dim snapEnvironment As IEngineSnapEnvironment
 
    Dim isRefreshing As Boolean
 
    Public Sub New()
      InitializeComponent()

      'get the snapEnvironment
      editor = New EngineEditorClass()
      editLayers = DirectCast(editor, IEngineEditLayers)
      snapEnvironment = DirectCast(editor, IEngineSnapEnvironment)

      'Update this form with the snap environment and snap tips
      'Note: from here on we expect all snap changes to be made via this form, so it is not updated after direct changes to the snapping 
      'environment or the Arc Engine Snap Window. 
      RefreshDisplay()
    End Sub

    '/ <summary>
    '/ Update the window with the current snap environment and snap tip settings
    '/ </summary>
    Private Sub RefreshDisplay()
      isRefreshing = True

      snapTolerance.Text = snapEnvironment.SnapTolerance.ToString()
      snapTolUnits.SelectedIndex = CType(snapEnvironment.SnapToleranceUnits, Integer)
      snapTips.Checked = (DirectCast(editor, IEngineEditProperties2)).SnapTips

      'remove all feature snap agents from the data grid view
      snapAgents.Rows.Clear()
      
      'display feature snap and snap agents that are active
      Dim i As Integer
      For i = 0 To snapEnvironment.SnapAgentCount - 1
        Dim snapAgent As IEngineSnapAgent = DirectCast(snapEnvironment.SnapAgent(i), IEngineSnapAgent)
        Dim ftrSnapAgent As IEngineFeatureSnapAgent = TryCast(snapAgent, IEngineFeatureSnapAgent)

        If Not ftrSnapAgent Is Nothing Then
          'for feature snap agents add a row to the data view grid 
          Dim hitType As esriGeometryHitPartType = ftrSnapAgent.HitType
          Dim vertex As Boolean, edge As Boolean, endSnap As Boolean
          vertex = (hitType And esriGeometryHitPartType.esriGeometryPartVertex) = esriGeometryHitPartType.esriGeometryPartVertex
          edge = (hitType And esriGeometryHitPartType.esriGeometryPartBoundary) = esriGeometryHitPartType.esriGeometryPartBoundary
          endSnap = (hitType And esriGeometryHitPartType.esriGeometryPartEndpoint) = esriGeometryHitPartType.esriGeometryPartEndpoint
          Dim vertexString As String = "      "
          Dim edgeString As String = "     "
          Dim endString As String = "     "
          If vertex Then vertexString = "vertex"
          If edge Then edgeString = " edge"
          If endSnap Then endString = " end"

          Dim hitTypes As String = vertexString + edgeString + endString
          Dim rowData() As Object = {snapAgent.Name.ToString(), ftrSnapAgent.FeatureClass.AliasName, hitTypes}

          snapAgents.Rows.Add(rowData)
        Else
          'add the active edit sketch snap agents    
          Dim rowData() As Object = {snapAgent.Name.ToString(), "<not applicable>", "<not applicable>"}

          snapAgents.Rows.Add(rowData)
        End If
      Next

      isRefreshing = False
    End Sub

#Region "Button Handlers"
    Private Sub clearAgents_Click(ByVal sender As Object, ByVal e As EventArgs) Handles clearAgents.Click
      snapEnvironment.ClearSnapAgents()

      'refresh this window
      RefreshDisplay()
    End Sub

    '/ <summary>
    '/ Turns off feature snap agents and turns off edit sketch snap agents by removing them
    '/ </summary>
    '/ <remarks>
    '/ Using Clear or Remove feature snap agents can be deactivated so that the user can't enable them.
    '/ Here we simply turn off all the agents, which will allow the user to turn them on.
    '/ </remarks>
    Private Sub turnOffAgents_Click(ByVal sender As Object, ByVal e As EventArgs) Handles turnOffAgents.Click
      TurnOffAgentsHelper()
    End Sub

    Private Sub reverseAgentsPriority_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles reverseAgentsPriority.Click
      'get all the snap agents in reverse order and then deactivate them
      Dim snapAgentList As ArrayList = New ArrayList()

      Dim i As Integer
      For i = snapEnvironment.SnapAgentCount - 1 To 0 Step i - 1
        Dim tmpAgent As IEngineSnapAgent = snapEnvironment.SnapAgent(i)
        snapAgentList.Add(tmpAgent)
        snapEnvironment.RemoveSnapAgent(i)
      Next

      'add the agents back to the environment
      Dim agent As IEngineSnapAgent
      For Each agent In snapAgentList
        snapEnvironment.AddSnapAgent(agent)
      Next

      'refresh this window
      RefreshDisplay()
    End Sub

    '/ <summary>
    '/ Adds a feature snap agent for the target layer and turn on  all options.
    '/ </summary>
    '/ <remarks>
    '/ This method does not check if there already exists a feature snap agent for the 
    '/ target layer. It is recommended that you do so since duplicate agents can appear
    '/ on the Snap Settings Form
    '/ </remarks>
    Private Sub addFeatureSnapAgent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFeatureSnapAgent.Click
      Dim featureSnapAgent As IEngineFeatureSnapAgent = New EngineFeatureSnap()
      If (editLayers.TargetLayer Is Nothing) Then
        System.Windows.Forms.MessageBox.Show("Please start an edit session")
        Return
      End If

      featureSnapAgent.FeatureClass = editLayers.TargetLayer.FeatureClass
      featureSnapAgent.HitType = esriGeometryHitPartType.esriGeometryPartVertex Or esriGeometryHitPartType.esriGeometryPartBoundary Or esriGeometryHitPartType.esriGeometryPartEndpoint
      snapEnvironment.AddSnapAgent(featureSnapAgent)

      'refresh this window
      RefreshDisplay()
    End Sub

    '/ <summary>
    '/ Adds, and hence turns on, Edit Sketch snap agents
    '/ </summary>
    Private Sub addSketchSnapAgent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addSketchSnapAgent.Click
      AddSketchSnapAgents()
      RefreshDisplay()
    End Sub
#End Region

#Region "Snap Tips and Tolerance Handlers"
    Private Sub snapTips_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles snapTips.CheckedChanged
      Try
        'turn snap tips on/off      
        DirectCast(editor, IEngineEditProperties2).SnapTips = snapTips.Checked
      Catch
        snapTips.Checked = DirectCast(editor, IEngineEditProperties2).SnapTips
      End Try
    End Sub

    Private Sub snapTolerance_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles snapTolerance.TextChanged
      Try
        snapEnvironment.SnapTolerance = Convert.ToDouble(snapTolerance.Text)
      Catch
        snapTolerance.Text = snapEnvironment.SnapTolerance.ToString()
      End Try
    End Sub

    Private Sub snapTolUnits_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles snapTolUnits.SelectedIndexChanged
      Try
        snapEnvironment.SnapToleranceUnits = CType(snapTolUnits.SelectedIndex, esriEngineSnapToleranceUnits)
      Catch
        snapTolUnits.SelectedIndex = CType(snapEnvironment.SnapToleranceUnits, Integer)
      End Try
    End Sub

#End Region

#Region "Helper Methods"
    '/ <summary>
    '/ Turns on or turns off the Edit Sketch snap agents by adding or removing them
    '/ </summary>
    Private Sub AddSketchSnapAgents()
      'give anchor snap (i.e. vertex snap) priority to get more accurate snap tips while snapped to a vertex
      Dim t As Type = Type.GetTypeFromProgID("esriControls.EngineAnchorSnap")
      Dim obj As System.Object = Activator.CreateInstance(t)
      Dim snapAgent As IEngineSnapAgent = DirectCast(obj, IEngineSnapAgent)
      snapEnvironment.AddSnapAgent(snapAgent)

      'edge sketch edges
      t = Type.GetTypeFromProgID("esriControls.EngineSketchSnap")
      obj = Activator.CreateInstance(t)
      snapAgent = DirectCast(obj, IEngineSnapAgent)
      snapEnvironment.AddSnapAgent(snapAgent)

      'perpendicular to edit sketch
      t = Type.GetTypeFromProgID("esriControls.EnginePerpendicularSnap")
      obj = Activator.CreateInstance(t)
      snapAgent = DirectCast(obj, IEngineSnapAgent)
      snapEnvironment.AddSnapAgent(snapAgent)
    End Sub

    '/ <summary>
    '/ Turns off feature snap agents and turns off edit sketch snap agents by removing them
    '/ </summary>
    '/ <remarks>
    '/ Using Clear or Remove feature snap agents can be deactivated so that the user can't enable them.
    '/ Here we simply turn off all the agents, which will allow the user to turn them on.
    '/ </remarks>
    Private Sub TurnOffAgentsHelper()
      Dim i As Integer
      For i = snapEnvironment.SnapAgentCount - 1 To 0 Step i - 1
        Dim snapAgent As IEngineSnapAgent = DirectCast(snapEnvironment.SnapAgent(i), IEngineSnapAgent)
        Dim ftrSnapAgent As IEngineFeatureSnapAgent = TryCast(snapAgent, IEngineFeatureSnapAgent)

        If (ftrSnapAgent IsNot Nothing) Then
          'turn off the feature snap agent
          ftrSnapAgent.HitType = esriGeometryHitPartType.esriGeometryPartNone
        Else
          'there is no way to turn snap agents off, they must be removed 
          snapEnvironment.RemoveSnapAgent(i)
        End If
      Next

      'refresh this window
      RefreshDisplay()
    End Sub
#End Region
  End Class
End Namespace

