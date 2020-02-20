'Copyright 2019 Esri

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
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ADF.CATIDs

Namespace CustomFunction
	<Guid("652642F3-9106-4EB3-9262-A4C39E03BC56")> _
	<ClassInterface(ClassInterfaceType.None)> _
	<ProgId("CustomFunction.NDVICustomFunction")> _
	<ComVisible(True)> _
	Public Class NDVICustomFunction
		Implements IRasterFunction
		Implements IPersistVariant
		Implements IDocumentVersionSupportGEN
		Implements IXMLSerialize
		Implements IXMLVersionSupport
		#Region "Private Members"
		Private myUID As UID
		' UID for the Function.
		Private myRasterInfo As IRasterInfo
		' Raster Info for the Function
		Private myPixeltype As rstPixelType
		' Pixel Type of the Function.
		Private myName As String
		' Name of the Function.
		Private myDescription As String
		' Description of the Function.
		Private myValidFlag As Boolean
		' Flag to specify validity of the Function.
		Private myFunctionHelper As IRasterFunctionHelper
		' Raster Function Helper object.
		Private myInpPixeltype As rstPixelType
		Private myInpNumBands As Integer

		Private myBandIndices As String()
		#End Region

		Public Sub New()
			myName = "NDVI Custom Function"
			myPixeltype = rstPixelType.PT_FLOAT
			myDescription = "Custom NDVI Function which calculates the NDVI without any scaling."
			myValidFlag = True
            myFunctionHelper = New RasterFunctionHelper()

			myInpPixeltype = myPixeltype
			myInpNumBands = 0

			myBandIndices = Nothing

            myUID = New UID()
			myUID.Value = "{652642F3-9106-4EB3-9262-A4C39E03BC56}"
		End Sub

		#Region "IRasterFunction Members"

		''' <summary>
		''' Name of the Raster Function.
		''' </summary>
		Public Property Name() As String Implements IRasterFunction.Name
			Get
				Return myName
			End Get
			Set
				myName = value
			End Set
		End Property

		''' <summary>
		''' Pixel Type of the Raster Function
		''' </summary>
		Public Property PixelType() As rstPixelType Implements IRasterFunction.PixelType
			Get
				Return myPixeltype
			End Get
			Set
				myPixeltype = value
			End Set
		End Property

		''' <summary>
		''' Output Raster Info for the Raster Function
		''' </summary>
		Public ReadOnly Property RasterInfo() As IRasterInfo Implements IRasterFunction.RasterInfo
			Get
				Return myRasterInfo
			End Get
		End Property

		''' <summary>
		''' Description of the Raster Function
		''' </summary>
		Public Property Description() As String Implements IRasterFunction.Description
			Get
				Return myDescription
			End Get
			Set
				myDescription = value
			End Set
		End Property

		''' <summary>
		''' Initialize the Raster function using the argument object. This is one of the two
		''' main functions to implement for a custom Raster function. The raster object is 
		''' dereferenced if required and given to the RasterFuntionHelper object to bind.
		''' </summary>
		''' <param name="pArguments">Arguments object used for initialization</param>
		Public Sub Bind(pArguments As Object) Implements IRasterFunction.Bind
			Try
				' Check if the Arguments object is of the correct type.
				Dim customFunctionArgs As INDVICustomFunctionArguments = Nothing
				If TypeOf pArguments Is INDVICustomFunctionArguments Then
					customFunctionArgs = DirectCast(pArguments, INDVICustomFunctionArguments)
					Dim inputRaster As Object = customFunctionArgs.Raster
					If TypeOf customFunctionArgs.Raster Is IRasterFunctionVariable Then
						Dim rasterFunctionVariable As IRasterFunctionVariable = DirectCast(customFunctionArgs.Raster, IRasterFunctionVariable)
						inputRaster = rasterFunctionVariable.Value
					End If

					' Call the Bind method of the Raster Function Helper object.
					myFunctionHelper.Bind(inputRaster)
				Else
					' Throw an error if incorrect arguments object is passed.
					Throw New System.Exception("Incorrect arguments object. Expected: INDVICustomFunctionArguments")
				End If

				' Check to see if Band Indices exist.
				If customFunctionArgs.BandIndices IsNot Nothing AndAlso customFunctionArgs.BandIndices <> "" Then
					myBandIndices = customFunctionArgs.BandIndices.Split(" "C)
				Else
					' If not, throw an error.
					Throw New System.Exception("Incorrect parameters specified. Expected: Valid band indices.")
				End If

				' Create a new RasterInfo object and initialize from the FunctionHelper object.
				' A new RasterInfo Object is created because assigning myFunctionHelper.RasterInfo
				' directly creates a reference.
                myRasterInfo = New RasterInfo()
				myRasterInfo.BandCount = myFunctionHelper.RasterInfo.BandCount
				myRasterInfo.BlockHeight = myFunctionHelper.RasterInfo.BlockHeight
				myRasterInfo.BlockWidth = myFunctionHelper.RasterInfo.BlockWidth
				myRasterInfo.CellSize = myFunctionHelper.RasterInfo.CellSize
				myRasterInfo.Extent = myFunctionHelper.RasterInfo.Extent
				myRasterInfo.FirstPyramidLevel = myFunctionHelper.RasterInfo.FirstPyramidLevel
				myRasterInfo.Format = myFunctionHelper.RasterInfo.Format
				myRasterInfo.GeodataXform = myFunctionHelper.RasterInfo.GeodataXform
				myRasterInfo.MaximumPyramidLevel = myFunctionHelper.RasterInfo.MaximumPyramidLevel
				myRasterInfo.NativeExtent = myFunctionHelper.RasterInfo.NativeExtent
				myRasterInfo.NativeSpatialReference = myFunctionHelper.RasterInfo.NativeSpatialReference
				myRasterInfo.NoData = myFunctionHelper.RasterInfo.NoData
				myRasterInfo.Origin = myFunctionHelper.RasterInfo.Origin
				myRasterInfo.PixelType = rstPixelType.PT_FLOAT
				' Output pixel type should be output of the NDVI.
				myRasterInfo.Resampling = myFunctionHelper.RasterInfo.Resampling
				myRasterInfo.SupportBandSelection = myFunctionHelper.RasterInfo.SupportBandSelection

				' Store required input properties.
				myInpPixeltype = myRasterInfo.PixelType
				myInpNumBands = myRasterInfo.BandCount

				' Set output pixel properties.
				myRasterInfo.BandCount = 1
				myPixeltype = rstPixelType.PT_FLOAT

				' Perform validation to see if the indices passed are valid.
				If myInpNumBands < 2 OrElse myBandIndices.Length < 2 Then
					' If not, throw an error.
					Throw New System.Exception("Incorrect parameters specified. Expected: Valid band indices.")
				End If
				For i As Integer = 0 To myBandIndices.Length - 1
					Dim currBand As Integer = Convert.ToInt16(myBandIndices(i)) - 1
					If (currBand < 0) OrElse (currBand > myInpNumBands) Then
						' If not, throw an error.
						Throw New System.Exception("Incorrect parameters specified. Expected: Valid band indices.")
					End If
				Next
			Catch exc As Exception
				Dim myExc As New System.Exception("Exception caught in Bind method: " & exc.Message, exc)
				Throw myExc
			End Try
		End Sub

		''' <summary>
		''' Read pixels from the input Raster and fill the PixelBlock provided with processed pixels.
		''' </summary>
		''' <param name="pTlc">Point to start the reading from in the Raster</param>
		''' <param name="pRaster">Reference Raster for the PixelBlock</param>
		''' <param name="pPixelBlock">PixelBlock to be filled in</param>
		Public Sub Read(pTlc As IPnt, pRaster As IRaster, pPixelBlock As IPixelBlock) Implements IRasterFunction.Read
			Try
				' Create a new pixel block to read the input data into.
				' This is done because the pPixelBlock represents the output
				' pixel block which is different from the input. 
				Dim pBHeight As Integer = pPixelBlock.Height
				Dim pBWidth As Integer = pPixelBlock.Width
                Dim pBlockSize As IPnt = New Pnt()
				pBlockSize.X = pBWidth
				pBlockSize.Y = pBHeight
                Dim inputPixelBlock As IPixelBlock = New PixelBlock()
				DirectCast(inputPixelBlock, IPixelBlock4).Create(myInpNumBands, pBWidth, pBHeight, myInpPixeltype)

				' Call Read method of the Raster Function Helper object to read the input pixels into
				' the inputPixelBlock.
				myFunctionHelper.Read(pTlc, Nothing, pRaster, inputPixelBlock)

				Dim inpPixelValues1 As System.Array
				Dim inpPixelValues2 As System.Array
				Dim outPixelValues As System.Array
				Dim index1 As Integer = Convert.ToInt16(myBandIndices(0)) - 1
				

				' Get NIR band index specified by user.
				Dim index2 As Integer = Convert.ToInt16(myBandIndices(1)) - 1
				

				' Get Red Band index specified by user.
				' Get the correct bands from the input.
				Dim ipPixelBlock As IPixelBlock3 = DirectCast(inputPixelBlock, IPixelBlock3)
                inpPixelValues1 = DirectCast(ipPixelBlock.PixelData(index1), System.Array)
                inpPixelValues2 = DirectCast(ipPixelBlock.PixelData(index2), System.Array)
                outPixelValues = DirectCast(DirectCast(pPixelBlock, IPixelBlock3).PixelData(0), System.Array)
				Dim i As Integer = 0
				Dim k As Integer = 0
				Dim pixelValue As Double = 0.0
				Dim nirValue As Double = 0.0
				Dim irValue As Double = 0.0
				' Perform the NDVI computation and store the result in the output pixel block.
				For i = 0 To pBHeight - 1
					For k = 0 To pBWidth - 1
						nirValue = Convert.ToDouble(inpPixelValues1.GetValue(k, i))
						irValue = Convert.ToDouble(inpPixelValues2.GetValue(k, i))
						' Check if input is not NoData.
						If (Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(index1, k, i)) = 1) AndAlso Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(index2, k, i)) = 1 Then
							' NDVI[k] = (NIR[k]-Red[k])/(NIR[k]+Red[k]);
							If (nirValue + irValue) <> 0 Then
								' Check for division by 0.
								pixelValue = (nirValue - irValue) / (nirValue + irValue)
								If pixelValue < -1.0 OrElse pixelValue > 1.0 Then
									pixelValue = 0.0
								End If
							Else
								pixelValue = 0.0
							End If
						End If
						outPixelValues.SetValue(CSng(pixelValue), k, i)
					Next
				Next
				' Set the output pixel values on the output pixel block.
                DirectCast(pPixelBlock, IPixelBlock3).PixelData(0) = outPixelValues
				' Copy over the NoData mask from the input and set it on the output.
                DirectCast(pPixelBlock, IPixelBlock3).NoDataMask(0) = DirectCast(inputPixelBlock, IPixelBlock3).NoDataMask(0)
			Catch exc As Exception
				Dim myExc As New System.Exception("Exception caught in Read method: " & exc.Message, exc)
				Throw myExc
			End Try
		End Sub

		''' <summary>
		''' Update the Raster Function
		''' </summary>
		Public Sub Update() Implements IRasterFunction.Update
			Try
			Catch exc As Exception
				Dim myExc As New System.Exception("Exception caught in Update method: ", exc)
				Throw myExc
			End Try
		End Sub

		''' <summary>
		''' Property that specifies whether the Raster Function is still valid.
		''' </summary>
		Public ReadOnly Property Valid() As Boolean Implements IRasterFunction.Valid
			Get
				Return myValidFlag
			End Get
		End Property

		#End Region

		#Region "IPersistVariant Members"

		''' <summary>
		''' UID to identify the function.
		''' </summary>
		Public ReadOnly Property ID() As UID Implements IPersistVariant.ID
			Get
				Return myUID
			End Get
		End Property

		''' <summary>
		''' Load the properties of the function from the stream provided
		''' </summary>
		''' <param name="Stream">Stream that contains the serialized form of the function</param>
		Public Sub Load(Stream As IVariantStream) Implements IPersistVariant.Load
			If TypeOf Stream Is IDocumentVersion Then
				Dim docVersion As IDocumentVersion = DirectCast(Stream, IDocumentVersion)
				If docVersion.DocumentVersion >= esriArcGISVersion.esriArcGISVersion10 Then
					Dim streamVersion As esriArcGISVersion = CType(CInt(Stream.Read()), esriArcGISVersion)
					If streamVersion >= esriArcGISVersion.esriArcGISVersion10 Then
						myName = DirectCast(Stream.Read(), String)
						myDescription = DirectCast(Stream.Read(), String)
						myPixeltype = CType(CInt(Stream.Read()), rstPixelType)
					End If
				End If
			End If
		End Sub

		''' <summary>
		''' Save the properties of the function to the stream provided
		''' </summary>
		''' <param name="Stream">Stream to which to serialize the function into</param>
		Public Sub Save(Stream As IVariantStream) Implements IPersistVariant.Save
			If TypeOf Stream Is IDocumentVersion Then
				Dim docVersion As IDocumentVersion = DirectCast(Stream, IDocumentVersion)
				If docVersion.DocumentVersion >= esriArcGISVersion.esriArcGISVersion10 Then
					Stream.Write(CInt(esriArcGISVersion.esriArcGISVersion10))
					Stream.Write(myName)
					Stream.Write(myDescription)
					Stream.Write(CInt(myPixeltype))
				End If
			End If
		End Sub

		#End Region

		#Region "IDocumentVersionSupportGEN Members"

		''' <summary>
		''' Convert the instance into an object supported by the given version
		''' </summary>
		''' <param name="docVersion">Version to convert to</param>
		''' <returns>Object that supports given version</returns>
		Public Function ConvertToSupportedObject(docVersion As esriArcGISVersion) As Object Implements IDocumentVersionSupportGEN.ConvertToSupportedObject
			Return Nothing
		End Function

		''' <summary>
		''' Check if the object is supported at the given version
		''' </summary>
		''' <param name="docVersion">Version to check against</param>
		''' <returns>True if the object is supported</returns>
		Public Function IsSupportedAtVersion(docVersion As esriArcGISVersion) As Boolean Implements IDocumentVersionSupportGEN.IsSupportedAtVersion
			If docVersion >= esriArcGISVersion.esriArcGISVersion10 Then
				Return True
			Else
				Return False
			End If
		End Function

		#End Region

		#Region "IXMLSerialize Members"

		''' <summary>
		''' Deserialize the Raster Function from the datastream provided
		''' </summary>
		''' <param name="data">Xml stream to deserialize the function from</param>
		Public Sub Deserialize(data As IXMLSerializeData) Implements IXMLSerialize.Deserialize
			myName = data.GetString(data.Find("Name"))
			myDescription = data.GetString(data.Find("Description"))
			myPixeltype = CType(data.GetInteger(data.Find("PixelType")), rstPixelType)
		End Sub

		''' <summary>
		''' Serialize the Raster Function into the stream provided.
		''' </summary>
		''' <param name="data">Xml stream to serialize the function into</param>
		Public Sub Serialize(data As IXMLSerializeData) Implements IXMLSerialize.Serialize
			data.TypeName = "NDVICustomFunction"
			data.TypeNamespaceURI = "http://www.esri.com/schemas/ArcGIS/10.2"
			data.AddString("Name", myName)
			data.AddString("Description", myDescription)
			data.AddInteger("PixelType", CInt(myPixeltype))
		End Sub

		#End Region

		#Region "IXMLVersionSupport Members"

		''' <summary>
		''' Returns the namespaces supported by the object
		''' </summary>
		Public ReadOnly Property MinNamespaceSupported() As String Implements IXMLVersionSupport.MinNamespaceSupported
			Get
                Return "http://www.esri.com/schemas/ArcGIS/10.2"
			End Get
		End Property

		#End Region

		#Region "COM Registration Function(s)"
		<ComRegisterFunction> _
		Private Shared Sub Reg(regKey As String)
			ESRI.ArcGIS.ADF.CATIDs.RasterFunctions.Register(regKey)
		End Sub

		<ComUnregisterFunction> _
		Private Shared Sub Unreg(regKey As String)
			ESRI.ArcGIS.ADF.CATIDs.RasterFunctions.Unregister(regKey)
		End Sub
		#End Region
	End Class

	<Guid("157ACDCC-9F2B-4CC4-BFFD-FEB933F3E788")> _
	<InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
	Public Interface INDVICustomFunctionArguments
		Inherits IRasterFunctionArguments
		Inherits IPersistVariant
		Inherits IDocumentVersionSupportGEN
		Inherits IXMLSerialize
		Inherits IXMLVersionSupport
		#Region "INDVICustomFunctionArguments Members"

		<DispId(&H50505001)> _
		Property Raster() As Object

		<DispId(&H50505002)> _
		Property BandIndices() As String
		#End Region
	End Interface

	<Guid("CB684500-0A15-46C5-B1C1-8062A1629F66")> _
	<ClassInterface(ClassInterfaceType.None)> _
	<ProgId("CustomFunction.NDVICustomFunctionArguments")> _
	<ComVisible(True)> _
	Public Class NDVICustomFunctionArguments
		Implements INDVICustomFunctionArguments
		#Region "Private Members"
		Private myName As String
		Private myDescription As String

		Private myUID As UID
		' UID for the NDVI Custom Function Arguments.
		Private myProperties As IPropertySet
		' Property set to store the properties of the arguments object.
		#End Region
		Public Sub New()
			myName = "NDVI Custom Function Arguments"
			myDescription = "Arguments object for the NDVI Custom Function"

			' Set default values
            myProperties = New PropertySet()
			myProperties.SetProperty("Raster", Nothing)
			myProperties.SetProperty("BandIndices", "1 2")
			' Default value for band indexes: first two bands of image.
            myUID = New UID()
			myUID.Value = "{CB684500-0A15-46C5-B1C1-8062A1629F66}"
		End Sub

		#Region "NDVICustomFunctionArguments Members"

		''' <summary>
		''' Raster to apply the raster function to.
		''' </summary>
		Public Property Raster() As Object Implements INDVICustomFunctionArguments.Raster
			Get
				Return GetDereferencedValue("Raster")
			End Get
			Set
				myProperties.SetProperty("Raster", value)
			End Set
		End Property

		''' <summary>
		''' Indices of the bands to use in the NDVI computation.
		''' </summary>
		Public Property BandIndices() As String Implements INDVICustomFunctionArguments.BandIndices
			Get
                Return Convert.ToString(GetDereferencedValue("BandIndices"))
			End Get
			Set
				myProperties.SetProperty("BandIndices", value)
			End Set
		End Property

		''' <summary>
		''' Dereference and return the value of the property whose name is given if necessary.
		''' </summary>
		''' <param name="name">Name of the property to check.</param>
		''' <returns>Dereferenced value of the property corresponding to the name given.</returns>
		Private Function GetDereferencedValue(name As String) As Object
			Dim value As Object = myProperties.GetProperty(name)
			If value IsNot Nothing AndAlso TypeOf value Is IRasterFunctionVariable AndAlso Not (TypeOf value Is IRasterFunctionTemplate) Then
                Dim rFVar As IRasterFunctionVariable = DirectCast(value, IRasterFunctionVariable)
                Return rFVar.Value
			End If
			Return value
		End Function

		#End Region

		#Region "IRasterFunctionArguments Members"

		''' <summary>
		''' A list of files associated with the raster
		''' </summary>
		Public ReadOnly Property FileList() As IStringArray Implements IRasterFunctionArguments.FileList
			Get
				Dim rasterObject As Object = myProperties.GetProperty("Raster")
				Dim rasterDataset As IRasterDataset = Nothing
				If TypeOf rasterObject Is IRasterDataset Then
					rasterDataset = DirectCast(rasterObject, IRasterDataset)
				ElseIf TypeOf rasterObject Is IName Then
					rasterDataset = DirectCast(DirectCast(rasterObject, IName).Open(), IRasterDataset)
				End If
				If rasterDataset IsNot Nothing AndAlso TypeOf rasterDataset Is IRasterDatasetInfo Then
					Dim rasterDatasetInfo As IRasterDatasetInfo = DirectCast(rasterDataset, IRasterDatasetInfo)
					Return rasterDatasetInfo.FileList
				Else
					Return Nothing
				End If
			End Get
		End Property

		''' <summary>
		''' Get the value associated with the name provided.
		''' </summary>
		''' <param name="Name">Name of the property</param>
		''' <returns>Value of the property name provided</returns>
		Public Function GetValue(Name As String) As Object Implements IRasterFunctionArguments.GetValue
			Return myProperties.GetProperty(Name)
		End Function

		''' <summary>
		''' A list of all the names in the property set.
		''' </summary>
		Public ReadOnly Property Names() As IStringArray Implements IRasterFunctionArguments.Names
			Get
				' Generate a list of names in the propertyset.
				Dim names__1 As Object = Nothing, values As Object = Nothing
				myProperties.GetAllProperties(names__1, values)
				Dim myNames As IStringArray = New StrArray()
				Dim nameArray As String() = DirectCast(names__1, String())
				For i As Integer = 0 To nameArray.GetLength(0) - 1
					myNames.Add(nameArray(i))
				Next
				Return myNames
			End Get
		End Property

		''' <summary>
		''' Set the given property name to the given value
		''' </summary>
		''' <param name="Name">Name of the property</param>
		''' <param name="Value">Value of the property</param>
		Public Sub PutValue(Name As String, Value As Object) Implements IRasterFunctionArguments.PutValue
			myProperties.SetProperty(Name, Value)
		End Sub

		''' <summary>
		''' Remove the value of the property name provided
		''' </summary>
		''' <param name="Name">Name of the property to be removed</param>
		Public Sub Remove(Name As String) Implements IRasterFunctionArguments.Remove
			myProperties.RemoveProperty(Name)
		End Sub

		''' <summary>
		''' Clear the property set of all names and values.
		''' </summary>
		Public Sub RemoveAll() Implements IRasterFunctionArguments.RemoveAll
			myProperties = Nothing
            myProperties = New PropertySet()
		End Sub

		''' <summary>
		''' A list of all the values in the property set
		''' </summary>
		Public ReadOnly Property Values() As IVariantArray Implements IRasterFunctionArguments.Values
			Get
				' Generate a list of values in the propertyset.
				Dim names As Object = Nothing, values__1 As Object = Nothing
				myProperties.GetAllProperties(names, values__1)
				Dim myValues As IVariantArray = New VarArray()
				Dim valArray As Object() = DirectCast(values__1, Object())
				For i As Integer = 0 To valArray.GetLength(0) - 1
					myValues.Add(valArray(i))
				Next
				Return myValues
			End Get
		End Property

		''' <summary>
		''' Resolve variables containing field names with the corresponding values.
		''' </summary>
		''' <param name="pRow">The row corresponding to the function raster dataset.</param>
		''' <param name="pPropertySet">Property Set to add the list of the names and the resolved values to.</param>
		Public Sub Resolve(pRow As IRow, pPropertySet As IPropertySet) Implements IRasterFunctionArguments.Resolve
			ResolveRasterVal(pRow)
		End Sub

		''' <summary>
		''' Update the variables containing field names to their updated values.
		''' </summary>
		''' <param name="pRow">The row corresponding to the function raster dataset.</param>
		''' <param name="pPropertySet">Property Set to add the list of the names and the updated values to.</param>
		''' <param name="pTemplateArguments">The arguements object containing the properties to update if</param>
		Public Sub Update(pRow As IRow, pPropertySet As IPropertySet, pTemplateArguments As IRasterFunctionArguments) Implements IRasterFunctionArguments.Update
			Resolve(pRow, pPropertySet)
		End Sub

		''' <summary>
		''' Resolve the 'Raster' variable if it contains field names with the corresponding values.
		''' </summary>
		''' <param name="pRow">The row corresponding to the function raster dataset.</param>
		Private Sub ResolveRasterVal(pRow As IRow)
			Try
				' Get the Raster property.
				Dim myRasterObject As Object = myProperties.GetProperty("Raster")
				' Check to see if it is a variable
				If TypeOf myRasterObject Is IRasterFunctionVariable Then
					Dim rasterVar As IRasterFunctionVariable = DirectCast(myRasterObject, IRasterFunctionVariable)
					Dim rasterVal As Object = FindPropertyInRow(rasterVar, pRow)
					If rasterVal IsNot Nothing AndAlso TypeOf rasterVal Is String Then
						Dim datasetPath As String = DirectCast(rasterVal, String)
						rasterVar.Value = OpenRasterDataset(datasetPath)
					End If
				End If
			Catch exc As Exception
				Dim myExc As New System.Exception("Exception caught in ResolveRasterVal: " & exc.Message, exc)
				Throw myExc
			End Try
		End Sub

		''' <summary>
		''' Check the Name and Alias properties of the given Raster Function Variable to see
		''' if they contain a field name and get the value of the corresponding field if needed.
		''' </summary>
		''' <param name="rasterFunctionVar">The Raster Function Variable to check.</param>
		''' <param name="pRow">The row corresponding to the function raster dataset.</param>
		''' <returns></returns>
		Private Function FindPropertyInRow(rasterFunctionVar As IRasterFunctionVariable, pRow As IRow) As Object
			Dim varName As String = ""
            Dim varNames As IStringArray = New StrArray()
			varName = rasterFunctionVar.Name
			' If the name of  the variable contains '@Field'
			If varName.Contains("@Field.") Then
				varNames.Add(varName)
			End If
			' Add it to the list of names.
			' Check the aliases of the variable
			For i As Integer = 0 To rasterFunctionVar.Aliases.Count - 1
				' Check the list of aliases for the '@Field' string
                varName = rasterFunctionVar.Aliases.Element(i)
				If varName.Contains("@Field.") Then
					varNames.Add(varName)
					' and add any that are found to the list of names.
				End If
			Next

			' Use the list of names and find the value by looking up the appropriate field.
			For i As Integer = 0 To varNames.Count - 1
				' Get the variable name containing the field string
                varName = varNames.Element(i)
				' Replace the '@Field' with nothing to get just the name of the field.
				Dim fieldName As String = varName.Replace("@Field.", "")
				Dim rowFields As IFields = pRow.Fields
				' Look up the index of the field name in the row.
				Dim fieldIndex As Integer = rowFields.FindField(fieldName)
				' If it is a valid index and the field type is string, return the value.
                If fieldIndex <> -1 AndAlso ((rowFields.Field(fieldIndex)).Type = esriFieldType.esriFieldTypeString) Then
                    Return pRow.Value(fieldIndex)
                End If
			Next
			' If no value has been returned yet, return null.
			Return Nothing
		End Function

		''' <summary>
		''' Open the Raster Dataset given the path to the file.
		''' </summary>
		''' <param name="path">Path to the Raster Dataset file.</param>
		''' <returns>The opened Raster Dataset.</returns>
		Private Function OpenRasterDataset(path As String) As IRasterDataset
			Try
				Dim inputWorkspace As String = System.IO.Path.GetDirectoryName(path)
				Dim inputDatasetName As String = System.IO.Path.GetFileName(path)
				Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
				Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
				Dim workspace As IWorkspace = workspaceFactory.OpenFromFile(inputWorkspace, 0)
				Dim rasterWorkspace As IRasterWorkspace = DirectCast(workspace, IRasterWorkspace)
				Dim myRasterDataset As IRasterDataset = rasterWorkspace.OpenRasterDataset(inputDatasetName)
				Return myRasterDataset
			Catch exc As Exception
				Throw exc
			End Try
		End Function
		#End Region

		#Region "IPersistVariant Members"

		''' <summary>
		''' UID to identify the object.
		''' </summary>
		Public ReadOnly Property ID() As UID Implements IPersistVariant.ID
			Get
				Return myUID
			End Get
		End Property

		''' <summary>
		''' Load the properties of the argument object from the stream provided
		''' </summary>
		''' <param name="Stream">Stream that contains the serialized form of the argument object</param>
		Public Sub Load(Stream As IVariantStream) Implements IPersistVariant.Load
			If TypeOf Stream Is IDocumentVersion Then
				Dim docVersion As IDocumentVersion = DirectCast(Stream, IDocumentVersion)
				If docVersion.DocumentVersion >= esriArcGISVersion.esriArcGISVersion10 Then
					Dim streamVersion As esriArcGISVersion = CType(CInt(Stream.Read()), esriArcGISVersion)
					If streamVersion >= esriArcGISVersion.esriArcGISVersion10 Then
						myName = DirectCast(Stream.Read(), String)
						myDescription = DirectCast(Stream.Read(), String)
						myProperties = DirectCast(Stream.Read(), IPropertySet)
					End If
				End If
			End If
		End Sub

		''' <summary>
		''' Save the properties of the argument object to the stream provided
		''' </summary>
		''' <param name="Stream">Stream to which to serialize the argument object into</param>
		Public Sub Save(Stream As IVariantStream) Implements IPersistVariant.Save
			If TypeOf Stream Is IDocumentVersion Then
				Dim docVersion As IDocumentVersion = DirectCast(Stream, IDocumentVersion)
				If docVersion.DocumentVersion >= esriArcGISVersion.esriArcGISVersion10 Then
					Dim names As Object = Nothing, values As Object = Nothing
					myProperties.GetAllProperties(names, values)
					Dim nameArray As String() = DirectCast(names, String())
					Dim valArray As Object() = DirectCast(values, Object())
					For i As Integer = 0 To nameArray.GetLength(0) - 1
						If TypeOf valArray(i) Is IDataset Then
							Dim myDatasetName As IName = DirectCast(valArray(i), IDataset).FullName
							myProperties.SetProperty(nameArray(i), myDatasetName)
						End If
					Next
					Stream.Write(CInt(esriArcGISVersion.esriArcGISVersion10))
					Stream.Write(myName)
					Stream.Write(myDescription)
					Stream.Write(myProperties)
				End If
			End If
		End Sub

		#End Region

		#Region "IDocumentVersionSupportGEN Members"

		''' <summary>
		''' Convert the instance into an object supported by the given version
		''' </summary>
		''' <param name="docVersion">Version to convert to</param>
		''' <returns>Object that supports given version</returns>
		Public Function ConvertToSupportedObject(docVersion As esriArcGISVersion) As Object Implements IDocumentVersionSupportGEN.ConvertToSupportedObject
			Return Nothing
		End Function

		''' <summary>
		''' Check if the object is supported at the given version
		''' </summary>
		''' <param name="docVersion">Version to check against</param>
		''' <returns>True if the object is supported</returns>
		Public Function IsSupportedAtVersion(docVersion As esriArcGISVersion) As Boolean Implements IDocumentVersionSupportGEN.IsSupportedAtVersion
			If docVersion >= esriArcGISVersion.esriArcGISVersion10 Then
				Return True
			Else
				Return False
			End If
		End Function

		#End Region

		#Region "IXMLSerialize Members"

		''' <summary>
		''' Deserialize the argument object from the datastream provided
		''' </summary>
		''' <param name="data">Xml stream to deserialize the argument object from</param>
		Public Sub Deserialize(data As IXMLSerializeData) Implements IXMLSerialize.Deserialize
			Dim nameIndex As Integer = data.Find("Names")
			Dim valIndex As Integer = data.Find("Values")
			If nameIndex <> -1 AndAlso valIndex <> -1 Then
				Dim myNames As IStringArray = DirectCast(data.GetVariant(nameIndex), IStringArray)
				Dim myValues As IVariantArray = DirectCast(data.GetVariant(valIndex), IVariantArray)
				For i As Integer = 0 To myNames.Count - 1
                    myProperties.SetProperty(myNames.Element(i), myValues.Element(i))
				Next
			End If
		End Sub

		''' <summary>
		''' Serialize the argument object into the stream provided.
		''' </summary>
		''' <param name="data">Xml stream to serialize the argument object into</param>
		Public Sub Serialize(data As IXMLSerializeData) Implements IXMLSerialize.Serialize
			'#Region "Prepare PropertySet"
			Dim names As Object = Nothing, values As Object = Nothing
			myProperties.GetAllProperties(names, values)
			Dim myNames As IStringArray = New StrArray()
			Dim nameArray As String() = DirectCast(names, String())
			Dim myValues As IVariantArray = New VarArray()
			Dim valArray As Object() = DirectCast(values, Object())
			For i As Integer = 0 To nameArray.GetLength(0) - 1
				myNames.Add(nameArray(i))
				If TypeOf valArray(i) Is IDataset Then
					Dim myDatasetName As IName = DirectCast(valArray(i), IDataset).FullName
					myValues.Add(myDatasetName)
				Else
					myValues.Add(valArray(i))
				End If
			Next
			'#End Region
			data.TypeName = "NDVICustomFunctionArguments"
            data.TypeNamespaceURI = "http://www.esri.com/schemas/ArcGIS/10.2"
			data.AddObject("Names", myNames)
			data.AddObject("Values", myValues)
		End Sub

		#End Region

		#Region "IXMLVersionSupport Members"

		''' <summary>
		''' Returns the namespaces supported by the object
		''' </summary>
		Public ReadOnly Property MinNamespaceSupported() As String Implements IXMLVersionSupport.MinNamespaceSupported
			Get
                Return "http://www.esri.com/schemas/ArcGIS/10.2"
			End Get
		End Property

		#End Region
	End Class
End Namespace
