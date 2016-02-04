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

Public Partial Class NDVICustomFunctionUIForm
	Inherits Form
	#Region "Private Members"
	Private myInputRaster As Object
	Private myBandIndices As String
	Private myDirtyFlag As Boolean
	#End Region

	#Region "NDVICustomFunctionUIForm Properties"
	''' <summary>
	''' Constructor
	''' </summary>
	Public Sub New()
		InitializeComponent()
		myInputRaster = Nothing
		myBandIndices = ""
		HintLbl.Text = "NIR Red"
	End Sub

	''' <summary>
	''' Get or set the band indices.
	''' </summary>
	Public Property BandIndices() As String
		Get
			myBandIndices = BandIndicesTxtBox.Text
			Return myBandIndices
		End Get
		Set
			myBandIndices = value
			BandIndicesTxtBox.Text = value
		End Set
	End Property

	''' <summary>
	''' Flag to specify if the form has changed
	''' </summary>
	Public Property IsFormDirty() As Boolean
		Get
			Return myDirtyFlag
		End Get
		Set
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
		Set
			myInputRaster = value
			inputRasterTxtbox.Text = GetInputRasterName(myInputRaster)
		End Set
	End Property

	#End Region

	#Region "NDVICustomFunctionUIForm Members"

	''' <summary>
	''' This function takes a raster object and returns the formatted name of  
	''' the object for display in the UI.
	''' </summary>
	''' <param name="inputRaster">Object whose name is to be found</param>
	''' <returns>Name of the object</returns>
	Private Function GetInputRasterName(inputRaster As Object) As String
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
		BandIndicesTxtBox.Text = myBandIndices
	End Sub

	Private Sub inputRasterBtn_Click(sender As Object, e As EventArgs)
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

	Public Sub ShowRasterDatasetBrowser(handle__1 As Integer, ByRef ipSelectedObjects As IEnumGxObject)
        Dim ipFilterCollection As IGxObjectFilterCollection = New GxDialog()

        Dim ipFilter1 As IGxObjectFilter = New GxFilterRasterDatasets()
		ipFilterCollection.AddFilter(ipFilter1, True)
		Dim ipGxDialog As IGxDialog = DirectCast(ipFilterCollection, IGxDialog)

		ipGxDialog.RememberLocation = True
		ipGxDialog.Title = "Open"

		ipGxDialog.AllowMultiSelect = False
		ipGxDialog.RememberLocation = True

		ipGxDialog.DoModalOpen(CInt(Handle.ToInt32()), ipSelectedObjects)
		Return
	End Sub

	Private Sub BandIndicesTxtBox_TextChanged(sender As Object, e As EventArgs)
		myBandIndices = BandIndicesTxtBox.Text
		myDirtyFlag = True
	End Sub

	#End Region
End Class
