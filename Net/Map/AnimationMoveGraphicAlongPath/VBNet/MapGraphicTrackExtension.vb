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
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework

<Guid("829D5FE5-D6FB-44FE-8729-FC9454D8B171"), ClassInterface(ClassInterfaceType.None), ProgId("AnimationDeveloperSamples.MapGraphicTrackExtension")> _
Public Class MapGraphicTrackExtension : Implements IPersistStream, IMapGraphicTrackExtension

    Private propSet As IPropertySet
    Private persist As IPersistStream

#Region "constructor"
    Public Sub New()
        Dim traceElem As ILineElement = New LineElementClass()
        SetDefaultSymbol(traceElem)
        'add a tag to the trace line
        Dim elemProps As IElementProperties = CType(traceElem, IElementProperties)
        elemProps.Name = "{E63706E1-B13C-4184-8AB8-97F67FA052D4}"
        Dim bShowTrace As Boolean = True
        propSet = New PropertySetClass()
        propSet.SetProperty("Line Element", traceElem)
        propSet.SetProperty("Show Trace", bShowTrace)
        persist = CType(propSet, IPersistStream)
    End Sub
#End Region

#Region "IPersistStream Members"
    Public Sub GetSizeMax(<System.Runtime.InteropServices.Out()> ByRef pcbSize As _ULARGE_INTEGER) Implements IPersistStream.GetSizeMax
        persist.GetSizeMax(pcbSize)
    End Sub

    Public Sub GetClassID(<System.Runtime.InteropServices.Out()> ByRef pClassID As Guid) Implements IPersist.GetClassID
        pClassID = Me.GetType().GUID
    End Sub

    Public Sub GetClassID1(<System.Runtime.InteropServices.Out()> ByRef pClassID As Guid) Implements IPersistStream.GetClassID
        pClassID = Me.GetType().GUID
    End Sub

    Public Sub Load(ByVal pstm As IStream) Implements IPersistStream.Load
        persist.Load(pstm)
    End Sub

    Public Sub IsDirty() Implements IPersistStream.IsDirty
        persist.IsDirty()
    End Sub

    Public Sub Save(ByVal pstm As IStream, ByVal fClearDirty As Integer) Implements IPersistStream.Save
        persist.Save(pstm, fClearDirty)
    End Sub
#End Region

#Region "IMapGraphicTrackExtension members"
    Public Property TraceElement() As ILineElement Implements IMapGraphicTrackExtension.TraceElement
        Get
            Return CType(propSet.GetProperty("Line Element"), ILineElement)
        End Get
        Set(ByVal value As ILineElement)
            Dim temp As ILineElement = CType(value, ILineElement)
            propSet.SetProperty("Line Element", temp)
        End Set
    End Property

    Public Property ShowTrace() As Boolean Implements IMapGraphicTrackExtension.ShowTrace
        Get
            Return CBool(propSet.GetProperty("Show Trace"))
        End Get
        Set(ByVal value As Boolean)
            Dim temp As Boolean = value
            propSet.SetProperty("Show Trace", temp)
        End Set
    End Property

    Public Sub ClearTrace() Implements IMapGraphicTrackExtension.ClearTrace
        Dim elem As IElement = CType(propSet.GetProperty("Line Element"), IElement)
        elem.Geometry.SetEmpty()
    End Sub
#End Region

#Region "private methods"
    Private Sub SetDefaultSymbol(ByVal elem As ILineElement)
        Dim defaultLineSym As ILineSymbol = Nothing
        Dim esriStylePath As String
        Dim styleGallery As IStyleGallery = New StyleGalleryClass()
        Dim styleStor As IStyleGalleryStorage = CType(styleGallery, IStyleGalleryStorage)
        esriStylePath = styleStor.DefaultStylePath & "ESRI.style"

        Dim styleItems As IEnumStyleGalleryItem = styleGallery.Items("Line Symbols", esriStylePath, "Dashed")
        styleItems.Reset()
        Dim styleGalleryItem As IStyleGalleryItem = styleItems.Next()
        Do While Not (styleGalleryItem Is Nothing)
            If styleGalleryItem.Name = "Dashed 4:4" Then
                defaultLineSym = CType(styleGalleryItem.Item, ILineSymbol)
                defaultLineSym.Width = 1.5
                Dim rgbColor As IRgbColor = New RgbColorClass()
                rgbColor.Red = 255
                rgbColor.Blue = 0
                rgbColor.Green = 0
                rgbColor.Transparency = 50
                defaultLineSym.Color = rgbColor
                Exit Do
            Else
                styleGalleryItem = styleItems.Next()
            End If
        Loop
        elem.Symbol = defaultLineSym
    End Sub
#End Region
End Class

Public Interface IMapGraphicTrackExtension
    Property TraceElement() As ILineElement

    Property ShowTrace() As Boolean

    Sub ClearTrace()

End Interface

