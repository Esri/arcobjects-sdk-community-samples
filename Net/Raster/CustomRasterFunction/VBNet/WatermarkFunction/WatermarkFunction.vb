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
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ADF.CATIDs

Namespace CustomFunction
    ''' <summary>
    ''' Enumerator for the location of the watermark.
    ''' </summary>
    <Guid("148F193A-0D46-4d4c-BC9C-A05AC4BE0BAB")> _
    <ComVisible(True)> _
    Public Enum esriWatermarkLocation
        esriWatermarkTopLeft = 0
        esriWatermarkTopRight
        esriWatermarkCenter
        esriWatermarkBottomLeft
        esriWatermarkBottomRight
    End Enum

    <Guid("168721E7-7010-4a36-B886-F644437B164D")> _
    <ClassInterface(ClassInterfaceType.None)> _
    <ProgId("CustomFunction.WatermarkFunction")> _
    <ComVisible(True)> _
    Public Class WatermarkFunction
        Implements IRasterFunction
        Implements IPersistVariant
        Implements IDocumentVersionSupportGEN
        Implements IXMLSerialize
        Implements IXMLVersionSupport
#Region "Private Members"
        Private myUID As UID
        ' UID for the Watermark Function.
        Private myRasterInfo As IRasterInfo
        ' Raster Info for the Watermark Function
        Private myPixeltype As rstPixelType
        ' Pixel Type of the Watermark Function.
        Private myName As String
        ' Name of the Watermark Function.
        Private myDescription As String
        ' Description of the Watermark Function.
        Private myValidFlag As Boolean
        ' Flag to specify validity of the Watermark Function.
        Private myWatermarkImagePath As String
        ' Path to the Watermark Image.
        Private myBlendPercentage As Double
        ' Percentage of the blending.
        Private blendValue As Double
        ' Actual value of the blend percentage.
        Private myWatermarkLocation As esriWatermarkLocation
        ' Location of the Watermark.
        Private myFunctionHelper As IRasterFunctionHelper
        ' Raster Function Helper object.
        Private myWatermarkImage As Bitmap
        ' Watermark Image object.
#End Region
        Public Sub New()
            myWatermarkImagePath = ""
            myBlendPercentage = 50.0
            ' Default value for the blending percentage.
            blendValue = 0.5
            ' Default value for the blend value.
            myWatermarkLocation = esriWatermarkLocation.esriWatermarkBottomRight

            myName = "WatermarkFunction"
            myPixeltype = rstPixelType.PT_UNKNOWN
            ' Default value for the pixel type.
            myDescription = "Add a watermark to the request."
            myValidFlag = True

            myFunctionHelper = New RasterFunctionHelperClass()

            myWatermarkImage = Nothing

            myUID = New UIDClass()
            myUID.Value = "{" & "168721E7-7010-4a36-B886-F644437B164D" & "}"
        End Sub

#Region "IRasterFunction Members"
        ''' <summary>
        ''' Name of the Raster Function.
        ''' </summary>
        Public Property Name() As String Implements IRasterFunction.Name
            Get
                Return myName
            End Get
            Set(ByVal value As String)
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
            Set(ByVal value As rstPixelType)
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
            Set(ByVal value As String)
                myDescription = value
            End Set
        End Property

        ''' <summary>
        ''' Initialize the Raster function using the argument object. This is one of the two
        ''' main functions to implement for a custom Raster function. The raster object is 
        ''' dereferenced if required and given to the RasterFuntionHelper object to bind.
        ''' </summary>
        ''' <param name="pArguments">Arguments object used for initialization</param>
        Public Sub Bind(ByVal pArguments As Object) Implements IRasterFunction.Bind
            Try
                ' Check if the Arguments object is of the correct type.
                Dim watermarkFuncArgs As IWatermarkFunctionArguments = Nothing
                If TypeOf pArguments Is IWatermarkFunctionArguments Then
                    watermarkFuncArgs = DirectCast(pArguments, IWatermarkFunctionArguments)
                    myBlendPercentage = watermarkFuncArgs.BlendPercentage
                    myWatermarkImagePath = watermarkFuncArgs.WatermarkImagePath
                    myWatermarkLocation = watermarkFuncArgs.WatermarkLocation

                    Dim inputRaster As Object = watermarkFuncArgs.Raster
                    If TypeOf watermarkFuncArgs.Raster Is IRasterFunctionVariable Then
                        Dim rasterFunctionVariable As IRasterFunctionVariable = DirectCast(watermarkFuncArgs.Raster, IRasterFunctionVariable)
                        inputRaster = rasterFunctionVariable.Value
                    End If

                    ' Call the Bind method of the Raster Function Helper object.
                    myFunctionHelper.Bind(inputRaster)
                Else
                    ' Throw an error if incorrect arguments object is passed.
                    Throw New System.Exception("Incorrect arguments object. Expected: IWatermarkFunctionArguments")
                End If

                ' Get the raster info and Pixel Type from the RasterFunctionHelper object.
                myRasterInfo = myFunctionHelper.RasterInfo
                myPixeltype = myRasterInfo.PixelType

                ' Convert blending percentage to blending value.
                If myBlendPercentage >= 0.0 AndAlso myBlendPercentage <= 100.0 Then
                    blendValue = myBlendPercentage / 100.0
                Else
                    ''' A value of 50% is used as default.
                    blendValue = 0.5
                End If

                If myWatermarkImagePath <> "" Then
                    ' Load the watermark image from the path provided
                    myWatermarkImage = New Bitmap(myWatermarkImagePath)
                    ' and check the pixel type of the loaded image to see if its compatible.
                    If myWatermarkImage.PixelFormat <> System.Drawing.Imaging.PixelFormat.Format32bppArgb AndAlso myWatermarkImage.PixelFormat <> System.Drawing.Imaging.PixelFormat.Format24bppRgb Then
                        ' Throw error if the image is not compatible.
                        Throw New System.Exception("Invalid watermark image. Please provide one with 8 bits per band in ARGB or RGB format.")
                    End If

                    ' Cleanup
                    myWatermarkImage.Dispose()
                    myWatermarkImage = Nothing
                End If
            Catch exc As Exception
                '#Region "Cleanup"
                If myWatermarkImage IsNot Nothing Then
                    myWatermarkImage.Dispose()
                End If
                myWatermarkImage = Nothing
                '#End Region

                Dim myExc As New System.Exception("Exception caught in Bind method of Watermark Function. " & exc.Message, exc)
                Throw myExc
            End Try
        End Sub

        ''' <summary>
        ''' Read pixels from the input Raster and fill the PixelBlock provided with processed pixels.
        ''' The RasterFunctionHelper object is used to handle pixel type conversion and resampling.
        ''' The watermark image is then blended to the bottom right corner of the pixel block. 
        ''' </summary>
        ''' <param name="pTlc">Point to start the reading from in the Raster</param>
        ''' <param name="pRaster">Reference Raster for the PixelBlock</param>
        ''' <param name="pPixelBlock">PixelBlock to be filled in</param>
        Public Sub Read(ByVal pTlc As IPnt, ByVal pRaster As IRaster, ByVal pPixelBlock As IPixelBlock) Implements IRasterFunction.Read
            Dim wMBitmapData As BitmapData = Nothing
            Dim pixelValue As Double = 0.0
            Dim wmRow As Integer = 0
            Dim wmCol As Integer = 0
            Try
                ' Call Read method of the Raster Function Helper object.
                myFunctionHelper.Read(pTlc, Nothing, pRaster, pPixelBlock)

                Dim wMBandOffset As Integer = 0

                '#Region "Reference Raster Properties"
                ' Get the pixel type of the reference raster to see if 
                ' it is compatible (8 bit).
                Dim referenceProps As IRasterProps = DirectCast(pRaster, IRasterProps)
                If referenceProps.PixelType <> rstPixelType.PT_UCHAR AndAlso referenceProps.PixelType <> rstPixelType.PT_CHAR Then
                    Throw New System.Exception("Function can only be applied to 8bit data.")
                End If

                '#End Region

                '#Region "Load watermark image object"
                ' Create new image object for the watermark image.
                myWatermarkImage = New Bitmap(myWatermarkImagePath)
                ' Read number of bands of the watermark image.
                If myWatermarkImage.PixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb Then
                    wMBandOffset = 4
                Else
                    If myWatermarkImage.PixelFormat = System.Drawing.Imaging.PixelFormat.Format24bppRgb Then
                        wMBandOffset = 3
                    Else
                        Throw New System.Exception("Invalid bitmap. Please provide one with 8bits per band in ARGB or RGB format.")
                    End If
                End If
                '#End Region

                Dim pBHeight As Integer = pPixelBlock.Height
                Dim pBWidth As Integer = pPixelBlock.Width
                Dim wMHeight As Integer = myWatermarkImage.Height
                Dim wMWidth As Integer = myWatermarkImage.Width
                Dim wMRowIndex As Integer = 0
                Dim wMColIndex As Integer = 0
                Dim pBRowIndex As Integer = 0
                Dim pBColIndex As Integer = 0
                Dim endRow As Integer = 0
                Dim endCol As Integer = 0
                Dim waterStartCol As Boolean = False
                Dim waterStartRow As Boolean = False

                ' Calculate the row/column values that specify where to start the blending.
                '#Region "Calculate Indices"
                ''' If the number of rows of the pixelblock are more than the watermark image
                endRow = pBHeight
                If pBHeight >= wMHeight Then
                    ''' Set the row index to start blending in the pixelblock.
                    Select Case myWatermarkLocation
                        Case esriWatermarkLocation.esriWatermarkTopLeft
                            If True Then
                                pBRowIndex = 0
                                endRow = pBRowIndex + wMHeight
                                Exit Select
                            End If
                        Case esriWatermarkLocation.esriWatermarkTopRight
                            If True Then
                                pBRowIndex = 0
                                endRow = pBRowIndex + wMHeight
                                Exit Select
                            End If
                        Case esriWatermarkLocation.esriWatermarkCenter
                            If True Then
                                pBRowIndex = (pBHeight \ 2) - (wMHeight \ 2)
                                endRow = pBRowIndex + wMHeight
                                Exit Select
                            End If
                        Case esriWatermarkLocation.esriWatermarkBottomLeft
                            If True Then
                                pBRowIndex = pBHeight - wMHeight
                                Exit Select
                            End If
                        Case esriWatermarkLocation.esriWatermarkBottomRight
                            If True Then
                                pBRowIndex = pBHeight - wMHeight
                                Exit Select
                            End If
                        Case Else
                            Exit Select
                    End Select

                    If myWatermarkLocation = esriWatermarkLocation.esriWatermarkCenter Then
                        pBRowIndex = (pBHeight \ 2) - (wMHeight \ 2)
                        endRow = pBRowIndex + wMHeight
                    End If
                Else
                    ''' If the number of rows of the watermark image is more than that of the pixelblock.
                    ''' Set the row index to start blending in the watermark image.
                    wMRowIndex = (wMHeight - pBHeight)
                    waterStartRow = True
                End If

                ''' If the number of cols of the pixelblock are more than the watermark image
                endCol = pBWidth
                If pBWidth >= wMWidth Then
                    ''' Set the col index to start blending in the pixelblock.
                    ''' Set the row index to start blending in the pixelblock.
                    Select Case myWatermarkLocation
                        Case esriWatermarkLocation.esriWatermarkTopLeft
                            If True Then
                                pBColIndex = 0
                                endCol = pBColIndex + wMWidth
                                Exit Select
                            End If
                        Case esriWatermarkLocation.esriWatermarkTopRight
                            If True Then
                                pBColIndex = pBWidth - wMWidth
                                Exit Select
                            End If
                        Case esriWatermarkLocation.esriWatermarkCenter
                            If True Then
                                pBColIndex = (pBWidth \ 2) - (wMWidth \ 2)
                                endCol = pBColIndex + wMWidth
                                Exit Select
                            End If
                        Case esriWatermarkLocation.esriWatermarkBottomLeft
                            If True Then
                                pBColIndex = 0
                                endCol = pBColIndex + wMWidth
                                Exit Select
                            End If
                        Case esriWatermarkLocation.esriWatermarkBottomRight
                            If True Then
                                pBColIndex = pBWidth - wMWidth
                                Exit Select
                            End If
                        Case Else
                            Exit Select
                    End Select
                Else
                    ''' If the number of cols of the watermark image is more than that of the pixelblock.
                    ''' Set the col index to start blending in the watermark image.
                    wMColIndex = (wMWidth - pBWidth)
                    waterStartCol = True
                End If
                '#End Region

                '#Region "Prepare Watermark Image for reading"
                ' Get the pixels from the watermark image using the lockbits function.
                wMBitmapData = myWatermarkImage.LockBits(New Rectangle(0, 0, wMWidth, wMHeight), _
                                                         ImageLockMode.[ReadOnly], myWatermarkImage.PixelFormat)

                Dim wMScan0 As System.IntPtr = wMBitmapData.Scan0
                Dim wMStride As Integer = wMBitmapData.Stride
                '#End Region

                ' The unsafe keyword is used so that pointers can be used to access pixels
                ' from the watermark image.
                Dim wMPaddingOffset As Integer = wMStride - (myWatermarkImage.Width * wMBandOffset)

                Dim wMBufferSize As Integer = wMStride * wMHeight
                Dim wMLocalBuffer(wMBufferSize) As Byte
                Call Marshal.Copy(wMScan0, wMLocalBuffer, 0, wMBufferSize)
                Dim wMLocalBufferIndex As Integer = 0

                ' Start filling from the correct row, col in the pixelblock 
                ' using the indices calculated above
                Dim pixelValues As System.Array
                If pPixelBlock.Planes = 3 Then
                    If wMBandOffset = 4 Then
                        ' To check for transparency in WM Image
                        '#Region "3 Band PixelBlock"
                        For nBand As Integer = 0 To pPixelBlock.Planes - 1
                            'Dim wMStartByte As Pointer(Of Byte) = CType(CType(wMScan0, Pointer(Of System.Void)), Pointer(Of Byte))
                            wMLocalBufferIndex = 0

                            ''' If the number of rows of the watermark image are more than the request.
                            If waterStartRow Then
                                ''' Skip to the correct row in the watermark image.
                                wMLocalBufferIndex += (wMStride * wMRowIndex)
                            End If

                            Dim ipPixelBlock As IPixelBlock3 = DirectCast(pPixelBlock, IPixelBlock3)
                            pixelValues = DirectCast(ipPixelBlock.PixelData(nBand), System.Array)
                            Dim i As Integer = pBRowIndex
                            While i < endRow
                                ''' If the number of cols of the watermark image are more than the request.
                                If waterStartCol Then
                                    ''' Skip to the correct column in the watermark image.                                
                                    wMLocalBufferIndex += (wMColIndex * wMBandOffset)
                                End If

                                Dim k As Integer = pBColIndex
                                While k < endCol
                                    pixelValue = Convert.ToDouble(pixelValues.GetValue(k, i))
                                    If Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + 3)) <> 0.0 AndAlso _
                                        Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(nBand, k, i)) = 1 Then
                                        ' Blend the pixelValue from the PixelBlock with the corresponding
                                        ' pixel from the watermark image.
                                        pixelValue = ((1 - blendValue) * pixelValue) +
                                            (blendValue * Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + (2 - nBand))))
                                    End If
                                    pixelValues.SetValue(Convert.ToByte(pixelValue), k, i)

                                    wMLocalBufferIndex += wMBandOffset
                                    k += 1
                                    wmCol += 1
                                End While
                                wMLocalBufferIndex += wMPaddingOffset
                                i += 1
                                wmRow += 1
                            End While
                            DirectCast(pPixelBlock, IPixelBlock3).PixelData(nBand) = pixelValues
                            '#End Region
                        Next
                    Else
                        '#Region "3 Band PixelBlock"
                        For nBand As Integer = 0 To pPixelBlock.Planes - 1
                            'Dim wMStartByte As Pointer(Of Byte) = CType(CType(wMScan0, Pointer(Of System.Void)), Pointer(Of Byte))
                            wMLocalBufferIndex = 0

                            ''' If the number of rows of the watermark image are more than the request.
                            If waterStartRow Then
                                ''' Skip to the correct row in the watermark image.
                                wMLocalBufferIndex += (wMStride * wMRowIndex)
                            End If

                            Dim ipPixelBlock As IPixelBlock3 = DirectCast(pPixelBlock, IPixelBlock3)
                            pixelValues = DirectCast(ipPixelBlock.PixelData(nBand), System.Array)
                            Dim i As Integer = pBRowIndex
                            While i < endRow
                                ''' If the number of cols of the watermark image are more than the request.
                                If waterStartCol Then
                                    ''' Skip to the correct column in the watermark image.                                
                                    wMLocalBufferIndex += (wMColIndex * wMBandOffset)
                                End If

                                Dim k As Integer = pBColIndex
                                While k < endCol
                                    pixelValue = Convert.ToDouble(pixelValues.GetValue(k, i))
                                    If Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(nBand, k, i)) = 1 Then
                                        ' Blend the pixelValue from the PixelBlock with the corresponding
                                        ' pixel from the watermark image.
                                        pixelValue = ((1 - blendValue) * pixelValue) +
                                            (blendValue * Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + (2 - nBand))))
                                    End If
                                    pixelValues.SetValue(Convert.ToByte(pixelValue), k, i)

                                    wMLocalBufferIndex += wMBandOffset
                                    k += 1
                                    wmCol += 1
                                End While
                                wMLocalBufferIndex += wMPaddingOffset
                                i += 1
                                wmRow += 1
                            End While
                            DirectCast(pPixelBlock, IPixelBlock3).PixelData(nBand) = pixelValues
                            '#End Region
                        Next
                    End If
                Else
                    If wMBandOffset = 4 Then
                        ' To check for transparency in WM Image
                        '#Region "Not 3 Band PixelBlock"
                        For nBand As Integer = 0 To pPixelBlock.Planes - 1
                            ' Dim wMStartByte As Pointer(Of Byte) = CType(CType(wMScan0, Pointer(Of System.Void)), Pointer(Of Byte))
                            wMLocalBufferIndex = 0

                            ''' If the number of rows of the watermark image are more than the request.
                            If waterStartRow Then
                                ''' Skip to the correct row in the watermark image.
                                wMLocalBufferIndex += (wMStride * wMRowIndex)
                            End If

                            Dim ipPixelBlock As IPixelBlock3 = DirectCast(pPixelBlock, IPixelBlock3)
                            pixelValues = DirectCast(ipPixelBlock.PixelData(nBand), System.Array)
                            Dim nooftimes As Integer = 0
                            Dim noofskips As Integer = 0
                            Dim i As Integer = pBRowIndex
                            While i < endRow
                                ''' If the number of cols of the watermark image are more than the request.
                                If waterStartCol Then
                                    ''' Skip to the correct column in the watermark image.                                
                                    wMLocalBufferIndex += (wMColIndex * wMBandOffset)
                                End If

                                Dim k As Integer = pBColIndex
                                While k < endCol
                                    pixelValue = Convert.ToDouble(pixelValues.GetValue(k, i))
                                    If Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(nBand, k, i)) = 1 Then
                                        'Convert.ToDouble(wMStartByte[3]) != 0.0 &&
                                        ' Calculate the average value of the pixels of the watermark image
                                        Dim avgValue As Double = (Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + 0)) + _
                                                                  Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + 1)) + _
                                                                  Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + 2))) / 3

                                        ' and blend it with the pixelValue from the PixelBlock.
                                        pixelValue = ((1 - blendValue) * pixelValue) + (blendValue * avgValue)
                                    End If
                                    pixelValues.SetValue(Convert.ToByte(pixelValue), k, i)

                                    nooftimes += 1
                                    noofskips += wMBandOffset
                                    wMLocalBufferIndex += wMBandOffset
                                    k += 1
                                    wmCol += 1
                                End While
                                wMLocalBufferIndex += wMPaddingOffset
                                i += 1
                                wmRow += 1
                            End While
                            DirectCast(pPixelBlock, IPixelBlock3).PixelData(nBand) = pixelValues
                            '#End Region
                        Next
                    Else
                        '#Region "Not 3 Band PixelBlock"
                        For nBand As Integer = 0 To pPixelBlock.Planes - 1
                            'Dim wMStartByte As Pointer(Of Byte) = CType(CType(wMScan0, Pointer(Of System.Void)), Pointer(Of Byte))
                            wMLocalBufferIndex = 0

                            ''' If the number of rows of the watermark image are more than the request.
                            If waterStartRow Then
                                ''' Skip to the correct row in the watermark image.
                                wMLocalBufferIndex += (wMStride * wMRowIndex)
                            End If

                            Dim ipPixelBlock As IPixelBlock3 = DirectCast(pPixelBlock, IPixelBlock3)
                            pixelValues = DirectCast(ipPixelBlock.PixelData(nBand), System.Array)
                            Dim nooftimes As Integer = 0
                            Dim noofskips As Integer = 0
                            Dim i As Integer = pBRowIndex
                            While i < endRow
                                ''' If the number of cols of the watermark image are more than the request.
                                If waterStartCol Then
                                    ''' Skip to the correct column in the watermark image.                                
                                    wMLocalBufferIndex += (wMColIndex * wMBandOffset)
                                End If

                                Dim k As Integer = pBColIndex
                                While k < endCol
                                    pixelValue = Convert.ToDouble(pixelValues.GetValue(k, i))
                                    If Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(nBand, k, i)) = 1 Then
                                        ' Calculate the average value of the pixels of the watermark image
                                        Dim avgValue As Double = (Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + 0)) + _
                                                                  Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + 1)) + _
                                                                  Convert.ToDouble(wMLocalBuffer(wMLocalBufferIndex + 2))) / 3

                                        ' and blend it with the pixelValue from the PixelBlock.
                                        pixelValue = ((1 - blendValue) * pixelValue) + (blendValue * avgValue)
                                    End If
                                    pixelValues.SetValue(Convert.ToByte(pixelValue), k, i)

                                    nooftimes += 1
                                    noofskips += wMBandOffset
                                    wMLocalBufferIndex += wMBandOffset
                                    k += 1
                                    wmCol += 1
                                End While
                                wMLocalBufferIndex += wMPaddingOffset
                                i += 1
                                wmRow += 1
                            End While
                            DirectCast(pPixelBlock, IPixelBlock3).PixelData(nBand) = pixelValues
                            '#End Region
                        Next
                    End If
                End If

                '#Region "Cleanup"
                myWatermarkImage.UnlockBits(wMBitmapData)
                myWatermarkImage.Dispose()
                myWatermarkImage = Nothing
                wMBitmapData = Nothing
                wMScan0 = CType(Nothing, System.IntPtr)
                '#End Region
                wMStride = 0
            Catch exc As Exception
                '#Region "Cleanup"
                If wMBitmapData IsNot Nothing Then
                    myWatermarkImage.UnlockBits(wMBitmapData)
                End If
                wMBitmapData = Nothing
                If myWatermarkImage IsNot Nothing Then
                    myWatermarkImage.Dispose()
                End If
                myWatermarkImage = Nothing
                '#End Region

                Dim myExc As New System.Exception("Exception caught in Read method of Watermark Function. " & exc.Message, exc)
                Throw myExc
            End Try
        End Sub

        ''' <summary>
        ''' Update the Raster Function
        ''' </summary>
        Public Sub Update() Implements IRasterFunction.Update
            Try
            Catch exc As Exception
                Dim myExc As New System.Exception("Exception caught in Update method of Watermark Function", exc)
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
        Public Sub Load(ByVal Stream As IVariantStream) Implements IPersistVariant.Load
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
        Public Sub Save(ByVal Stream As IVariantStream) Implements IPersistVariant.Save
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
        Public Function ConvertToSupportedObject(ByVal docVersion As esriArcGISVersion) As Object Implements IDocumentVersionSupportGEN.ConvertToSupportedObject
            Return Nothing
        End Function

        ''' <summary>
        ''' Check if the object is supported at the given version
        ''' </summary>
        ''' <param name="docVersion">Version to check against</param>
        ''' <returns>True if the object is supported</returns>
        Public Function IsSupportedAtVersion(ByVal docVersion As esriArcGISVersion) As Boolean Implements IDocumentVersionSupportGEN.IsSupportedAtVersion
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
        Public Sub Deserialize(ByVal data As IXMLSerializeData) Implements IXMLSerialize.Deserialize
            myName = data.GetString(data.Find("Name"))
            myDescription = data.GetString(data.Find("Description"))
            myPixeltype = CType(data.GetInteger(data.Find("PixelType")), rstPixelType)
        End Sub

        ''' <summary>
        ''' Serialize the Raster Function into the stream provided.
        ''' </summary>
        ''' <param name="data">Xml stream to serialize the function into</param>
        Public Sub Serialize(ByVal data As IXMLSerializeData) Implements IXMLSerialize.Serialize
            data.TypeName = "WatermarkFunction"
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
        <ComRegisterFunction()> _
        Private Shared Sub Reg(ByVal regKey As String)
            ESRI.ArcGIS.ADF.CATIDs.RasterFunctions.Register(regKey)
        End Sub

        <ComUnregisterFunction()> _
        Private Shared Sub Unreg(ByVal regKey As String)
            ESRI.ArcGIS.ADF.CATIDs.RasterFunctions.Unregister(regKey)
        End Sub
#End Region
    End Class

    <Guid("933A9DEF-D56F-4e37-911D-AC16982CA697")> _
    <InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Public Interface IWatermarkFunctionArguments
        Inherits IRasterFunctionArguments
        Inherits IPersistVariant
        Inherits IXMLSerialize
#Region "WatermarkFunctionArguments Members"
        <DispId(&H50505001)> _
        Property Raster() As Object

        <DispId(&H50505002)> _
        Property WatermarkImagePath() As String

        <DispId(&H50505003)> _
        Property BlendPercentage() As Double

        <DispId(&H50505004)> _
        Property WatermarkLocation() As esriWatermarkLocation
#End Region
    End Interface

    <Guid("996DC8E5-086B-41b5-919A-A3B9B86F2B30")> _
    <ClassInterface(ClassInterfaceType.None)> _
    <ProgId("CustomFunction.WatermarkFunctionArguments")> _
    <ComVisible(True)> _
    Public Class WatermarkFunctionArguments
        Implements IWatermarkFunctionArguments
        Implements IPersistVariant
        Implements IDocumentVersionSupportGEN
        Implements IXMLSerialize
        Implements IXMLVersionSupport
#Region "Private Members"
        Private myName As String
        Private myDescription As String
        Private myUID As UID
        ' UID for the Watermark Function.
        Private myProperties As IPropertySet
        ' Property set to store the properties of the arguments object.
#End Region
        Public Sub New()
            myName = "WatermarkFunctionArguments"
            myDescription = "Arguments object for the Watermark Function"

            ' Set default values
            myProperties = New PropertySetClass()
            myProperties.SetProperty("Raster", Nothing)
            myProperties.SetProperty("WatermarkImagePath", "")
            myProperties.SetProperty("BlendPercentage", 50.0)
            myProperties.SetProperty("WatermarkLocation", CustomFunction.esriWatermarkLocation.esriWatermarkBottomRight)

            myUID = New UIDClass()
            myUID.Value = "{" & "996DC8E5-086B-41b5-919A-A3B9B86F2B30" & "}"
        End Sub

#Region "WatermarkFunctionArguments Members"
        ''' <summary>
        ''' Raster to apply the raster function to.
        ''' </summary>
        Public Property Raster() As Object Implements IWatermarkFunctionArguments.Raster
            Get
                Return GetDereferencedValue("Raster")
            End Get
            Set(ByVal value As Object)
                myProperties.SetProperty("Raster", value)
            End Set
        End Property

        ''' <summary>
        ''' Path to the image to blend into the raster.
        ''' </summary>
        Public Property WatermarkImagePath() As String Implements IWatermarkFunctionArguments.WatermarkImagePath
            Get
                Return Convert.ToString(GetDereferencedValue("WatermarkImagePath"))
            End Get
            Set(ByVal value As String)
                myProperties.SetProperty("WatermarkImagePath", value)
            End Set
        End Property

        ''' <summary>
        ''' Percentage value by which to blend the watermark image with the raster
        ''' </summary>
        Public Property BlendPercentage() As Double Implements IWatermarkFunctionArguments.BlendPercentage
            Get
                Return Convert.ToDouble(GetDereferencedValue("BlendPercentage"))
            End Get
            Set(ByVal value As Double)
                myProperties.SetProperty("BlendPercentage", value)
            End Set
        End Property

        Public Property WatermarkLocation() As esriWatermarkLocation Implements IWatermarkFunctionArguments.WatermarkLocation
            Get
                Return CType(Convert.ToInt16(GetDereferencedValue("WatermarkLocation")), esriWatermarkLocation)
            End Get
            Set(ByVal value As esriWatermarkLocation)
                myProperties.SetProperty("WatermarkLocation", value)
            End Set
        End Property

        ''' <summary>
        ''' Dereference and return the value of the property whose name is given if necessary.
        ''' </summary>
        ''' <param name="name">Name of the property to check.</param>
        ''' <returns>Dereferenced value of the property corresponding to the name given.</returns>
        Public Function GetDereferencedValue(ByVal name As String) As Object
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
        Public Function GetValue(ByVal Name As String) As Object Implements IRasterFunctionArguments.GetValue
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
        Public Sub PutValue(ByVal Name As String, ByVal Value As Object) Implements IRasterFunctionArguments.PutValue
            myProperties.SetProperty(Name, Value)
        End Sub

        ''' <summary>
        ''' Remove the value of the property name provided
        ''' </summary>
        ''' <param name="Name">Name of the property to be removed</param>
        Public Sub Remove(ByVal Name As String) Implements IRasterFunctionArguments.Remove
            myProperties.RemoveProperty(Name)
        End Sub

        ''' <summary>
        ''' Clear the property set of all names and values.
        ''' </summary>
        Public Sub RemoveAll() Implements IRasterFunctionArguments.RemoveAll
            myProperties = Nothing
            myProperties = New PropertySetClass()
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
        Public Sub Resolve(ByVal pRow As IRow, ByVal pPropertySet As IPropertySet) Implements IRasterFunctionArguments.Resolve
            Try
                ResolveRasterVal(pRow)
                ResolveBlendPVal(pRow)
                ResolveWatermarkPathVal(pRow)
            Catch exc As Exception
                Dim myExc As New System.Exception("Exception caught in Resolve: " & exc.Message, exc)
                Throw myExc
            End Try
        End Sub

        ''' <summary>
        ''' Update the variables containing field names to their updated values.
        ''' </summary>
        ''' <param name="pRow">The row corresponding to the function raster dataset.</param>
        ''' <param name="pPropertySet">Property Set to add the list of the names and the updated values to.</param>
        ''' <param name="pTemplateArguments">The arguments object containing the properties to update if</param>
        Public Sub Update(ByVal pRow As IRow, ByVal pPropertySet As IPropertySet, ByVal pTemplateArguments As IRasterFunctionArguments) Implements IRasterFunctionArguments.Update
            Resolve(pRow, pPropertySet)
        End Sub

        ''' <summary>
        ''' Resolve the 'Raster' variable if it contains field names with the corresponding values.
        ''' </summary>
        ''' <param name="pRow">The row corresponding to the function raster dataset.</param>
        Private Sub ResolveRasterVal(ByVal pRow As IRow)
            Try
                ' Get the Raster property.
                Dim myRasterObject As Object = myProperties.GetProperty("Raster")
                ' Check to see if it is a variable
                If TypeOf myRasterObject Is IRasterFunctionVariable Then
                    Dim rasterVar As IRasterFunctionVariable = DirectCast(myRasterObject, IRasterFunctionVariable)
                    Dim rasterVal As Object = FindPropertyInRow(rasterVar, pRow)
                    If rasterVal IsNot Nothing AndAlso TypeOf rasterVal Is String Then
                        ' Open the Raster Dataset from the path provided.
                        Dim datasetPath As String = DirectCast(rasterVal, String)
                        Dim rasterDataset As IRasterDataset = Nothing
                        rasterVar.Value = rasterDataset
                    End If
                End If
            Catch exc As Exception
                Dim myExc As New System.Exception("Exception caught in ResolveRasterVal: " & exc.Message, exc)
                Throw myExc
            End Try
        End Sub

        ''' <summary>
        ''' Open the Raster Dataset given the path to the file.
        ''' </summary>
        ''' <param name="path">Path to the Raster Dataset file.</param>
        ''' <returns>The opened Raster Dataset.</returns>
        Private Function OpenRasterDataset(ByVal path As String) As IRasterDataset
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

        ''' <summary>
        ''' Resolve the 'BlendPercentage' variable if it contains field names with the corresponding values.
        ''' </summary>
        ''' <param name="pRow">The row corresponding to the function raster dataset.</param>
        Private Sub ResolveBlendPVal(ByVal pRow As IRow)
            ' Get the BlendPercentage property.
            Dim myRasterObject As Object = myProperties.GetProperty("BlendPercentage")
            ' Check to see if it is a variable
            If TypeOf myRasterObject Is IRasterFunctionVariable Then
                Dim bpVar As IRasterFunctionVariable = DirectCast(myRasterObject, IRasterFunctionVariable)
                Dim rasterVal As Object = FindPropertyInRow(bpVar, pRow)
                If rasterVal IsNot Nothing AndAlso TypeOf rasterVal Is String Then
                    ' Get the blend percentage value from string
                    Try
                        bpVar.Value = Convert.ToDouble(DirectCast(rasterVal, String))
                    Catch generatedExceptionName As Exception
                    End Try
                End If
            End If
        End Sub

        ''' <summary>
        ''' Resolve the 'WatermarkImagePath' variable if it contains field names with the corresponding values.
        ''' </summary>
        ''' <param name="pRow">The row corresponding to the function raster dataset.</param>
        Private Sub ResolveWatermarkPathVal(ByVal pRow As IRow)
            ' Get the WatermarkImagePath property.
            Dim myRasterObject As Object = myProperties.GetProperty("WatermarkImagePath")
            ' Check to see if it is a variable
            If TypeOf myRasterObject Is IRasterFunctionVariable Then
                Dim wipVar As IRasterFunctionVariable = DirectCast(myRasterObject, IRasterFunctionVariable)
                Dim rasterVal As Object = FindPropertyInRow(wipVar, pRow)
                If rasterVal IsNot Nothing AndAlso TypeOf rasterVal Is String Then
                    ' Get the blend percentage value from string
                    wipVar.Value = DirectCast(rasterVal, String)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Check the Name and Alias properties of the given Raster Function Variable to see
        ''' if they contain a reference to a field and get the value of the corresponding field if needed.
        ''' </summary>
        ''' <param name="rasterFunctionVar">The Raster Function Variable to check.</param>
        ''' <param name="pRow">The row corresponding to the function raster dataset.</param>
        ''' <returns></returns>
        Private Function FindPropertyInRow(ByVal rasterFunctionVar As IRasterFunctionVariable, ByVal pRow As IRow) As Object
            Dim varName As String = ""
            Dim varNames As IStringArray = New StrArrayClass()
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
        Public Sub Load(ByVal Stream As IVariantStream) Implements IPersistVariant.Load
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
        Public Sub Save(ByVal Stream As IVariantStream) Implements IPersistVariant.Save
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
        Public Function ConvertToSupportedObject(ByVal docVersion As esriArcGISVersion) As Object Implements IDocumentVersionSupportGEN.ConvertToSupportedObject
            Return Nothing
        End Function

        ''' <summary>
        ''' Check if the object is supported at the given version
        ''' </summary>
        ''' <param name="docVersion">Version to check against</param>
        ''' <returns>True if the object is supported</returns>
        Public Function IsSupportedAtVersion(ByVal docVersion As esriArcGISVersion) As Boolean Implements IDocumentVersionSupportGEN.IsSupportedAtVersion
            If docVersion >= esriArcGISVersion.esriArcGISVersion10 Then
                Return True
            Else
                Return False
            End If
        End Function
#End Region

#Region "IXMLSerialize Members"
        ''' <summary>
        ''' Deserialize the argument object from the stream provided
        ''' </summary>
        ''' <param name="data">Xml stream to deserialize the argument object from</param>
        Public Sub Deserialize(ByVal data As IXMLSerializeData) Implements IXMLSerialize.Deserialize
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
        Public Sub Serialize(ByVal data As IXMLSerializeData) Implements IXMLSerialize.Serialize
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
            data.TypeName = "WatermarkFunctionArguments"
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
