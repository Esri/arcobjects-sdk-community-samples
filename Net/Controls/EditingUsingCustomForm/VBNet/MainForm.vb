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
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Display



Public Class MainForm
    <STAThread()> _
Shared Sub Main()
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)
        Application.Run(New MainForm())
    End Sub

#Region "private members"
    Private m_mapControl As IMapControl3 = Nothing
#End Region

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_mapControl = CType(AxMapControl1.Object, IMapControl3)

        'relative file path to the sample data from EXE location
        Dim filePath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        filePath = System.IO.Path.Combine (filePath, "ArcGIS\data\USAMajorHighways")

        'Add Lakes layer
        Dim workspaceFactory As IWorkspaceFactory = New ShapefileWorkspaceFactoryClass()
        Dim workspace As IFeatureWorkspace = CType(workspaceFactory.OpenFromFile(filePath, AxMapControl1.hWnd), IFeatureWorkspace)
        Dim featureLayer As IFeatureLayer = New FeatureLayerClass()
        featureLayer.Name = "Lakes"
        featureLayer.Visible = True
        featureLayer.FeatureClass = workspace.OpenFeatureClass("us_lakes")

        'create a SimplerRenderer
        Dim color As IRgbColor = New RgbColorClass()
        color.Red = 190
        color.Green = 232
        color.Blue = 255

        Dim sym As ISimpleFillSymbol = New SimpleFillSymbolClass()
        sym.Color = color
        Dim renderer As ISimpleRenderer = New SimpleRendererClass()
        renderer.Symbol = sym
        CType(featureLayer, IGeoFeatureLayer).Renderer = renderer

        AxMapControl1.Map.AddLayer(CType(featureLayer, ILayer))

        'Add Highways layer
        featureLayer = New FeatureLayerClass()
        featureLayer.Name = "Highways"
        featureLayer.Visible = True
        featureLayer.FeatureClass = workspace.OpenFeatureClass("usa_major_highways")
        AxMapControl1.Map.AddLayer(CType(featureLayer, ILayer))

        '******** Important *************
        'store a reference to this form (Mainform) using the EditHelper class
        EditHelper.TheMainForm = Me
        EditHelper.IsEditorFormOpen = False

        'add the EditCmd command to the toolbar
        axEditorToolbar.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        axEditorToolbar.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        axEditorToolbar.AddItem("esriControls.ControlsAddDataCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        axEditorToolbar.AddItem(New EditCmd(), 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

    End Sub

    Private Sub MainForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        'Warn users if the ArcGIS Engine samples used by this application have not been compiled
        Dim checkList As ArrayList = New ArrayList()
        checkList.Add("ReshapePolylineEditTask_CS.ReshapePolylineEditTask")
        checkList.Add("VertexCommands_CS.CustomVertexCommands")

        Dim t As Type = Nothing
        Dim success As Boolean = True

        Dim item As String
        For Each item In checkList
            t = Type.GetTypeFromProgID(item)

            If t Is Nothing Then
                success = False
                Exit For
            End If
        Next

        If Not success Then
            MessageBox.Show("Editing will not function correctly until the C# ReshapePolylineEditTask and VertexCommands samples have been compiled. More information can be found in the 'How to use' section for this sample.", _
                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

#Region "public properties"
    Public ReadOnly Property MapControl() As IMapControl3
        Get
            Return m_mapControl
        End Get
    End Property

#End Region

End Class
