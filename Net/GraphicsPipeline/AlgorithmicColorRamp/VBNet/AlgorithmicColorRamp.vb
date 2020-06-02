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
Option Strict Off
Option Explicit On
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System
Imports System.Collections
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class AlgorithmicColorRamp
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Private frmAlgoColorRamp As New frmAlgorithmicColorRamp()

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        '
        ' When the utility is selected, check that we have a currently selected
        ' feature layer with a ClassBreaksRenderer already set. First we get the contents view.
        '
        Dim ContentsView As IContentsView
        ContentsView = My.ArcMap.Document.CurrentContentsView
        '
        ' If we have a DisplayView active
        '
        Dim VarSelectedItem As Object
        Dim GeoFeatureLayer As IGeoFeatureLayer
        Dim ClassBreaksRenderer As IClassBreaksRenderer
        Dim pColors As IEnumColors
        Dim lngCount As Integer
        Dim HsvColor As IHsvColor
        Dim ClonedSymbol As IClone
        Dim NewSymbol As ISymbol
        Dim ActiveView As IActiveView 'AlgorithimcColorRamp contains HSV colors.
        If TypeOf ContentsView Is TOCDisplayView Then
            If IsDBNull(ContentsView.SelectedItem) Then
                '
                ' If we don't have anything selected.
                '
                'Err.Raise(94, , "SelectedItem is Null" & vbNewLine & "Select a layer in the Table of Contents to rename")
                MsgBox("SelectedItem is Null." & "Select a layer in the Table of Contents to rename.", MsgBoxStyle.Information, "No Layer Selected")
                Exit Sub
            End If
            '
            ' Get the selected Item.
            '
            VarSelectedItem = ContentsView.SelectedItem
            '
            ' Selected Item should implement the IGeoFeatureLayer interface - therefore we
            ' have selected a feature layer with a Renderer property (Note: Other interfaces
            ' also have a Renderer property, which may behave differently.
            '
            If TypeOf VarSelectedItem Is IGeoFeatureLayer Then
                GeoFeatureLayer = VarSelectedItem
                '
                ' Set the cached property to true, so we can refresh this layer
                ' without refreshing all the layers, when we have changed the symbols.
                '
                GeoFeatureLayer.Cached = True
                '
                ' Check we have an existing ClassBreaksRenderer.
                '
                If TypeOf GeoFeatureLayer.Renderer Is IClassBreaksRenderer Then
                    ClassBreaksRenderer = GeoFeatureLayer.Renderer
                    '
                    ' If successful so far we can go ahead and open the Form. This allows the
                    ' user to change the properties of the new RandomColorRamp.
                    '
                    frmAlgoColorRamp.m_lngClasses = ClassBreaksRenderer.BreakCount
                    frmAlgoColorRamp.ShowDialog()
                    '
                    ' Return the selected colors enumeration.
                    pColors = frmAlgoColorRamp.m_enumNewColors
                    If pColors Is Nothing Then
                        '
                        ' User has cancelled the form, or not set a ramp.
                        '
                        'MsgBox("Colors object is empty. Exit Sub")

                        Exit Sub
                    End If
                    '
                    ' Set the new random colors onto the Symbol array of the ClassBreaksRenderer.
                    '
                    pColors.Reset() ' Because you never know if the enumeration has been
                    ' iterated before being passed back.

                    For lngCount = 0 To ClassBreaksRenderer.BreakCount - 1
                        '
                        ' For each Value in the ClassBreaksRenderer, we clone the existing
                        ' Fill symbol (so that all the properties are faithful preserved,
                        ' and set its color from our new AlgorithmicColorRamp.
                        '
                        ClonedSymbol = CloneMe(ClassBreaksRenderer.Symbol(lngCount))
                        '
                        ' Now the ClonedSymbol variable holds a copy of the existing
                        ' Symbol, we can change the assigned Color. We set the new
                        ' symbol onto the Symbol array of the Renderer.          '
                        '
                        HsvColor = pColors.Next
                        NewSymbol = SetColorOfUnknownSymbol(ClonedSymbol, HsvColor)
                        If Not NewSymbol Is Nothing Then
                            ClassBreaksRenderer.Symbol(lngCount) = NewSymbol
                        End If
                    Next lngCount
                    '
                    ' Refresh the table of contents and the changed layer.
                    '
                    ActiveView = My.ArcMap.Document.FocusMap
                    ActiveView.ContentsChanged()
                    My.ArcMap.Document.UpdateContents()
                    ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, GeoFeatureLayer, Nothing)
                End If
            End If
        End If
    End Sub

    Public Function CloneMe(ByRef OriginalClone As IClone) As IClone
        '
        ' This function clones the input object.
        '
        CloneMe = Nothing
        If Not OriginalClone Is Nothing Then
            CloneMe = OriginalClone.Clone
        End If
    End Function

    Private Function SetColorOfUnknownSymbol(ByVal ClonedSymbol As IClone, ByVal Color As IColor) As ISymbol
        '
        ' This function takes an IClone interface, works out the underlying coclass
        ' (which should be some kind of symbol) and then sets the Color property
        ' according to the passed in color.
        '
        SetColorOfUnknownSymbol = Nothing
        If ClonedSymbol Is Nothing Then
            Exit Function
        End If
        '
        ' Here we figure out which kind of symbol we have. For the simple symbol
        ' types, simply setting the color property is OK. However, more complex
        ' symbol types require further investigation.
        '
        Dim FillSymbol As IFillSymbol
        Dim MarkerFillSymbol As IMarkerFillSymbol
        Dim MarkerSymbol_A As IMarkerSymbol
        Dim LineFillSymbol As ILineFillSymbol
        Dim LineSymbol As ILineSymbol
        Dim PictureFillSymbol As IPictureFillSymbol
        Dim MarkerSymbol_B As IMarkerSymbol
        Dim PictureMarkerSymbol As IPictureMarkerSymbol
        Dim MarkerLineSymbol As IMarkerLineSymbol
        Dim MarkerSymbol_C As IMarkerSymbol
        Dim LineSymbol_B As ILineSymbol
        If TypeOf ClonedSymbol Is ISymbol Then
            '
            ' Check for Fill symbols.
            '
            If TypeOf ClonedSymbol Is IFillSymbol Then
                '
                ' Check for SimpleFillSymbols or MultiLevelFillSymbols.
                '
                If (TypeOf ClonedSymbol Is ISimpleFillSymbol) Or (TypeOf ClonedSymbol Is IMultiLayerFillSymbol) Then
                    FillSymbol = ClonedSymbol
                    '
                    ' Here we simply change the color of the Fill.
                    '
                    FillSymbol.Color = Color
                    SetColorOfUnknownSymbol = FillSymbol
                    '
                    ' Check for MarkerFillSymbols.
                    '
                ElseIf TypeOf ClonedSymbol Is IMarkerFillSymbol Then
                    MarkerFillSymbol = ClonedSymbol
                    '
                    ' Here we change the color of the MarkerSymbol.
                    '
                    MarkerSymbol_A = SetColorOfUnknownSymbol(MarkerFillSymbol.MarkerSymbol_A, Color)
                    MarkerFillSymbol.MarkerSymbol = MarkerSymbol_A
                    SetColorOfUnknownSymbol = MarkerFillSymbol
                    '
                    ' Check for LineFillSymbols.
                    '
                ElseIf TypeOf ClonedSymbol Is ILineFillSymbol Then
                    LineFillSymbol = ClonedSymbol
                    '
                    ' Here we change the color of the LineSymbol.
                    '
                    LineSymbol = SetColorOfUnknownSymbol(LineFillSymbol.LineSymbol, Color)
                    LineFillSymbol.LineSymbol = LineSymbol
                    SetColorOfUnknownSymbol = LineFillSymbol
                    '
                    ' Check for PictureFillSymbols.
                    '
                ElseIf TypeOf ClonedSymbol Is IPictureFillSymbol Then
                    PictureFillSymbol = ClonedSymbol
                    '
                    ' Here we simply change the color of the BackgroundColor.
                    '
                    PictureFillSymbol.BackgroundColor = Color
                    SetColorOfUnknownSymbol = PictureFillSymbol
                End If
                '
                ' Check for Marker symbols.
                '
            ElseIf TypeOf ClonedSymbol Is IMarkerSymbol Then
                '
                ' Check for SimpleMarkerSymbols, ArrowMarkerSymbols or
                ' CharacterMarkerSymbols.
                '
                If (TypeOf ClonedSymbol Is IMultiLayerMarkerSymbol) Or (TypeOf ClonedSymbol Is ISimpleMarkerSymbol) Or (TypeOf ClonedSymbol Is IArrowMarkerSymbol) Or (TypeOf ClonedSymbol Is ICharacterMarkerSymbol) Then
                    MarkerSymbol_B = ClonedSymbol
                    '
                    ' For these types, we simply change the color property.
                    '
                    MarkerSymbol_B.Color = Color
                    SetColorOfUnknownSymbol = MarkerSymbol_B
                    '
                    ' Check for PictureMarkerSymbols.
                    '
                ElseIf TypeOf ClonedSymbol Is IPictureMarkerSymbol Then
                    PictureMarkerSymbol = ClonedSymbol
                    '
                    ' Here we change the BackgroundColor property.
                    '
                    PictureMarkerSymbol.Color = Color
                    SetColorOfUnknownSymbol = PictureMarkerSymbol
                End If
                '
                ' Check for Line symbols.
                '
            ElseIf TypeOf ClonedSymbol Is ILineSymbol Then
                '
                ' Check for MarkerLine symbols.
                '
                If TypeOf ClonedSymbol Is IMarkerLineSymbol Then
                    MarkerLineSymbol = ClonedSymbol
                    '
                    ' Here we change the color of the MarkerSymbol.
                    '
                    MarkerSymbol_C = SetColorOfUnknownSymbol(MarkerLineSymbol.MarkerSymbol, Color)
                    MarkerLineSymbol.MarkerSymbol = MarkerSymbol_C
                    SetColorOfUnknownSymbol = MarkerLineSymbol
                    '
                    ' Check for other Line symbols.
                    '
                ElseIf (TypeOf ClonedSymbol Is ISimpleLineSymbol) Or (TypeOf ClonedSymbol Is IHashLineSymbol) Or (TypeOf ClonedSymbol Is ICartographicLineSymbol) Then
                    LineSymbol_B = ClonedSymbol
                    LineSymbol_B.Color = Color
                    SetColorOfUnknownSymbol = LineSymbol_B
                End If
            End If
        End If

    End Function

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub
End Class
