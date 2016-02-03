Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto
Imports System.IO

Public Partial Class FrmAddJPIP
	Inherits Form
	Public Sub New()
		InitializeComponent()
	End Sub

    Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
        Try
            'create raster dataset from the JPIP service url
            Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
            Dim wsFact As IWorkspaceFactory = TryCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
            Dim tempPath As String = Path.GetTempPath()
            Dim ws As IRasterWorkspace2 = TryCast(wsFact.OpenFromFile(tempPath, 0), IRasterWorkspace2)
            Dim rds As IRasterDataset = ws.OpenRasterDataset(txtJPIPUrl.Text)

            'create a layer from the raster dataset
            Dim rasterLayer As IRasterLayer = New RasterLayerClass()
            rasterLayer.CreateFromDataset(rds)
            Dim layerName As String = txtLayerName.Text
            If layerName = "" Then
                layerName = txtJPIPUrl.Text.Substring(txtJPIPUrl.Text.LastIndexOf("/") + 1, txtJPIPUrl.Text.Length - txtJPIPUrl.Text.LastIndexOf("/") - 1)
            End If
            rasterLayer.Name = layerName

            'add the JPIP layer to the current data frame of ArcMap
            My.ArcMap.Document.FocusMap.AddLayer(rasterLayer)
            Me.Close()
        Catch
            MessageBox.Show("Couldn't connect to the specified URL, sample url: jpip://myserver:8080/JP2Server/imagealias")
        End Try
    End Sub


    Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
        Me.Close()
    End Sub


    Private Sub FrmAddJPIP_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub txtJPIPUrl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJPIPUrl.TextChanged
        If txtJPIPUrl.Text.ToLower().Contains("jp2server/") Then
            txtLayerName.Text = txtJPIPUrl.Text.Substring(txtJPIPUrl.Text.LastIndexOf("/") + 1, txtJPIPUrl.Text.Length - txtJPIPUrl.Text.LastIndexOf("/") - 1)
        Else
            txtLayerName.Text = ""
        End If
    End Sub
End Class
