Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports CustomFunction.CustomFunction

Partial Public Class WatermarkFunctionUIForm
    Inherits Form
#Region "Private Members"
    Private myInputRaster As Object
    Private myWaterMarkImagePath As String
    Private myBlendPercentage As Double
    Private myWatermarkLocation As esriWatermarkLocation
    Private myDirtyFlag As Boolean
#End Region

#Region "WatermarkFunctionUIForm Properties"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        myInputRaster = Nothing
        myWaterMarkImagePath = ""
        myBlendPercentage = 0.0
        myWatermarkLocation = esriWatermarkLocation.esriWatermarkBottomRight
    End Sub

    ''' <summary>
    ''' Get or set the watermark image path
    ''' </summary>
    Public Property WatermarkImagePath() As String
        Get
            myWaterMarkImagePath = watermarkImageTxtbox.Text
            Return myWaterMarkImagePath
        End Get
        Set(ByVal value As String)
            myWaterMarkImagePath = value
            watermarkImageTxtbox.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Flag to specify if the form has changed
    ''' </summary>
    Public Property IsFormDirty() As Boolean
        Get
            Return myDirtyFlag
        End Get
        Set(ByVal value As Boolean)
            myDirtyFlag = value
        End Set
    End Property

    ''' <summary>
    ''' Get or set the input raster
    ''' </summary>
    Public Property InputRaster() As Object
        Get
            Return myInputRaster
        End Get
        Set(ByVal value As Object)
            myInputRaster = value
            inputRasterTxtbox.Text = GetInputRasterName(myInputRaster)
        End Set
    End Property

    ''' <summary>
    ''' Get or set the blending percentage
    ''' </summary>
    Public Property BlendPercentage() As Double
        Get
            If blendPercentTxtbox.Text = "" Then
                blendPercentTxtbox.Text = "50.00"
            End If
            myBlendPercentage = Convert.ToDouble(blendPercentTxtbox.Text)
            Return myBlendPercentage
        End Get
        Set(ByVal value As Double)
            myBlendPercentage = value
            blendPercentTxtbox.Text = Convert.ToString(value)
        End Set
    End Property

    ''' <summary>
    ''' Get or set the watermark location.
    ''' </summary>
    Public Property WatermarkLocation() As esriWatermarkLocation
        Get
            Return myWatermarkLocation
        End Get
        Set(ByVal value As esriWatermarkLocation)
            myWatermarkLocation = value
        End Set
    End Property
#End Region

#Region "WatermarkFunctionUIForm Members"
    ''' <summary>
    ''' This function takes a raster object and returns the formatted name of  
    ''' the object for display in the UI.
    ''' </summary>
    ''' <param name="inputRaster">Object whose name is to be found</param>
    ''' <returns>Name of the object</returns>
    Private Function GetInputRasterName(ByVal inputRaster As Object) As String
        If (TypeOf inputRaster Is IRasterDataset) Then
            Dim rasterDataset As IRasterDataset = DirectCast(inputRaster, IRasterDataset)
            Return rasterDataset.CompleteName
        End If

        If (TypeOf inputRaster Is IRaster) Then
            Dim myRaster As IRaster = DirectCast(inputRaster, IRaster)
            Return DirectCast(myRaster, IRaster2).RasterDataset.CompleteName
        End If

        If TypeOf inputRaster Is IDataset Then
            Dim dataset As IDataset = DirectCast(inputRaster, IDataset)
            Return dataset.Name
        End If

        If TypeOf inputRaster Is IName Then
            If TypeOf inputRaster Is IDatasetName Then
                Dim inputDSName As IDatasetName = DirectCast(inputRaster, IDatasetName)
                Return inputDSName.Name
            End If

            If TypeOf inputRaster Is IFunctionRasterDatasetName Then
                Dim inputFRDName As IFunctionRasterDatasetName = DirectCast(inputRaster, IFunctionRasterDatasetName)
                Return inputFRDName.BrowseName
            End If

            If TypeOf inputRaster Is IMosaicDatasetName Then
                Dim inputMDName As IMosaicDatasetName = DirectCast(inputRaster, IMosaicDatasetName)
                Return "MD"
            End If

            Dim inputName As IName = DirectCast(inputRaster, IName)
            Return inputName.NameString
        End If

        If TypeOf inputRaster Is IRasterFunctionTemplate Then
            Dim rasterFunctionTemplate As IRasterFunctionTemplate = DirectCast(inputRaster, IRasterFunctionTemplate)
            Return rasterFunctionTemplate.[Function].Name
        End If

        If TypeOf inputRaster Is IRasterFunctionVariable Then
            Dim rasterFunctionVariable As IRasterFunctionVariable = DirectCast(inputRaster, IRasterFunctionVariable)
            Return rasterFunctionVariable.Name
        End If

        Return ""
    End Function

    ''' <summary>
    ''' Updates the UI textboxes using the properties that have been set.
    ''' </summary>
    Public Sub UpdateUI()
        If myInputRaster IsNot Nothing Then
            inputRasterTxtbox.Text = GetInputRasterName(myInputRaster)
        End If
        blendPercentTxtbox.Text = Convert.ToString(myBlendPercentage)
        watermarkImageTxtbox.Text = myWaterMarkImagePath
        LocationComboBx.SelectedIndex = CInt(myWatermarkLocation)
    End Sub

    Private Sub inputRasterBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim ipSelectedObjects As IEnumGxObject = Nothing
        ShowRasterDatasetBrowser(CInt(Handle.ToInt32()), ipSelectedObjects)

        Dim selectedObject As IGxObject = ipSelectedObjects.[Next]()
        If TypeOf selectedObject Is IGxDataset Then
            Dim ipGxDS As IGxDataset = DirectCast(selectedObject, IGxDataset)
            Dim ipDataset As IDataset
            ipDataset = ipGxDS.Dataset
            myInputRaster = ipDataset.FullName
            inputRasterTxtbox.Text = GetInputRasterName(myInputRaster)
            myDirtyFlag = True
        End If
    End Sub

    Public Sub ShowRasterDatasetBrowser(ByVal handle__1 As Integer, ByRef ipSelectedObjects As IEnumGxObject)
        Dim ipFilterCollection As IGxObjectFilterCollection = New GxDialogClass()

        Dim ipFilter1 As IGxObjectFilter = New GxFilterRasterDatasetsClass()
        ipFilterCollection.AddFilter(ipFilter1, True)
        Dim ipGxDialog As IGxDialog = DirectCast(ipFilterCollection, IGxDialog)

        ipGxDialog.RememberLocation = True
        ipGxDialog.Title = "Open"

        ipGxDialog.AllowMultiSelect = False
        ipGxDialog.RememberLocation = True

        ipGxDialog.DoModalOpen(CInt(Handle.ToInt32()), ipSelectedObjects)
        Return
    End Sub

    Private Sub LocationComboBx_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        myWatermarkLocation = CType(LocationComboBx.SelectedIndex, esriWatermarkLocation)
        myDirtyFlag = True
    End Sub

    Private Sub watermarkImageBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        watermarkImageDlg.ShowDialog()
        If watermarkImageDlg.FileName <> "" Then
            watermarkImageTxtbox.Text = watermarkImageDlg.FileName
            myDirtyFlag = True
        End If
    End Sub

    Private Sub blendPercentTxtbox_ModifiedChanged(ByVal sender As Object, ByVal e As EventArgs)
        If blendPercentTxtbox.Text <> "" Then
            myBlendPercentage = Convert.ToDouble(blendPercentTxtbox.Text)
            myDirtyFlag = True
        End If
    End Sub
#End Region

End Class
