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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

Public Enum EColorCombinationType
    enuComponents
    enuCIELabColorRamp
    enuLabLChColorRamp
    enuRGBAverage
    enuCIELabMatrix
End Enum

<ComClass(MultivariateRenderer.ClassId, MultivariateRenderer.InterfaceId, MultivariateRenderer.EventsId)> _
Public Class MultivariateRenderer

  Inherits ExportSupport

  ' class definition for MultivariateRenderer, a custom multivariate feature renderer
  '   consisting of 

  Implements IFeatureRenderer     ' all feature renderers must support this interface
  Implements IMultivariateRenderer ' custom interface
  Implements ILegendInfo          ' for TOC and legend support
  Implements IPersistVariant      ' to support saving and loading .mxd and .lyr files that contain this renderer
  Implements IRotationRenderer    ' to support symbol rotation by field value
  Implements ITransparencyRenderer ' we don't do anything real with this




#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "6A921DB3-5D31-4D85-9857-687CEDBC0D29"
    Public Const InterfaceId As String = "DDC4CD50-DF02-4B2F-9B85-DA87FDED9EA7"
    Public Const EventsId As String = "36E15D76-2463-41DD-A673-0C83021AC30A"
#End Region

    ' data members
    Private m_eColorCombinationMethod As EColorCombinationType = EColorCombinationType.enuComponents
    Private m_pShapePatternRend As IFeatureRenderer
    Private m_pColorRend1 As IFeatureRenderer
    Private m_pColorRend2 As IFeatureRenderer
    Private m_pSizeRend As IFeatureRenderer
    ' for the renderer's TOC and legend entry 
    ' current implementation is simple, but could be extended
    Private m_pLegendGroups() As ILegendGroup
    Private m_sRotationField As String
    Private m_eRotationType As esriSymbolRotationType = esriSymbolRotationType.esriRotateSymbolGeographic
    Private m_sTransparencyField As String
    Private m_pMainRend As IFeatureRenderer ' as renderers are assigned, use this to keep track of which one has the base symbols
    Private m_ShapeType As esriGeometryType
    Private m_pFeatureClass As IFeatureClass
    Private m_pQueryFilter As IQueryFilter
    Private m_OLEColorMatrix(3, 3) As Long

    Private Const E_FAIL As Long = &H80004005

    Public Sub New()
        
    End Sub

    Protected Overrides Sub Finalize()
        m_pShapePatternRend = Nothing
        m_pColorRend1 = Nothing
        m_pColorRend2 = Nothing
        MyBase.Finalize()
    End Sub

    Public Sub CreateLegend() Implements IMultivariateRenderer.CreateLegend
        ' NOT IMPL

        ' this is a place holder sub for logic that can be called that creates a more
        '   involved entry for the layer's TOC and legend entry

    End Sub

    Public Function CanRender(ByVal featClass As IFeatureClass, ByVal Display As IDisplay) As Boolean Implements IFeatureRenderer.CanRender
        ' only use this renderer if we have points, lines, or polygons
        Return (featClass.ShapeType = esriGeometryType.esriGeometryPoint) Or _
               (featClass.ShapeType = esriGeometryType.esriGeometryPolyline) Or _
               (featClass.ShapeType = esriGeometryType.esriGeometryPolygon)
    End Function

    Public Sub Draw(ByVal cursor As IFeatureCursor, ByVal DrawPhase As esriDrawPhase, ByVal Display As IDisplay, ByVal trackCancel As ITrackCancel) Implements IFeatureRenderer.Draw
        ' loop through and draw each feature

        Dim pFeat As IFeature
        Dim pRend As IFeatureRenderer
        Dim pFeatDraw As IFeatureDraw

        Dim bContinue As Boolean

        ' do not draw features if no display
        If (Display Is Nothing) Then
            Exit Sub
        End If

        ' we can't draw without somewhere to get our base symbols from
        If (m_pMainRend Is Nothing) Then
            Exit Sub
        End If

        If Not m_pSizeRend Is Nothing Then
            ' size varies
            If m_ShapeType = esriGeometryType.esriGeometryPoint Or m_ShapeType = esriGeometryType.esriGeometryPolyline Then
                If DrawPhase = esriDrawPhase.esriDPGeography Then
                    ' draw symbols in order from large to small
                    DrawSymbolsInOrder(cursor, DrawPhase, Display, trackCancel)
                End If
            ElseIf m_ShapeType = esriGeometryType.esriGeometryPolygon Then
                If (DrawPhase = esriDrawPhase.esriDPAnnotation) Then
                    ' draw primary symbology from large to small
                    DrawSymbolsInOrder(cursor, DrawPhase, Display, trackCancel)
                ElseIf (DrawPhase = esriDrawPhase.esriDPGeography) Then
                    ' draw background symbology
                    pFeat = cursor.NextFeature
                    bContinue = True

                    ' while there are still more features and drawing has not been cancelled
                    Dim pBackFillSym As IFillSymbol
                    Do While (Not pFeat Is Nothing) And (bContinue = True)
                        ' draw the feature
                        pFeatDraw = pFeat
                        If TypeOf m_pSizeRend Is IClassBreaksRenderer Then
                            Dim pCBRend As IClassBreaksRenderer
                            pCBRend = m_pSizeRend
                            pBackFillSym = pCBRend.BackgroundSymbol
                        Else
                            Dim pPropRend As IProportionalSymbolRenderer
                            pPropRend = m_pSizeRend
                            pBackFillSym = pPropRend.BackgroundSymbol
                        End If
                        Display.SetSymbol(pBackFillSym)

                        'implementation of IExportSupport
                        BeginFeature(pFeat, Display)

                        pFeatDraw.Draw(DrawPhase, Display, pBackFillSym, True, Nothing, esriDrawStyle.esriDSNormal)

                        'implementation of IExportSupport
                        GenerateExportInfo(pFeat, Display)
                        EndFeature(Display)

                        pFeat = cursor.NextFeature
                        If Not trackCancel Is Nothing Then bContinue = trackCancel.[Continue]
                    Loop
                Else
                    ' raising this error makes the selection symbol draw for selected features
                    On Error GoTo 0
                    Err.Raise(E_FAIL)
                End If
            End If

        Else
            ' size does not vary
            If (DrawPhase <> esriDrawPhase.esriDPGeography) Then
                ' raising this error makes the selection symbol draw for selected features
                On Error GoTo 0
                Err.Raise(E_FAIL)
            Else
                DrawSymbols(cursor, DrawPhase, Display, trackCancel)
            End If
        End If

    End Sub

    Public WriteOnly Property ExclusionSet() As IFeatureIDSet Implements IFeatureRenderer.ExclusionSet
        Set(ByVal Value As IFeatureIDSet)
            ' NOT IMPL    
        End Set
    End Property

    Public Sub PrepareFilter(ByVal fc As IFeatureClass, ByVal queryFilter As IQueryFilter) Implements IFeatureRenderer.PrepareFilter
        ' prepare filter for drawing

        ' must add OID
        queryFilter.AddField(fc.OIDFieldName)

        m_ShapeType = fc.ShapeType
        If m_ShapeType = esriGeometryType.esriGeometryPoint Then
            If Not m_sRotationField = "" Then
                queryFilter.AddField(m_sRotationField)
            End If
        End If

        ' save the feature class and the query filter so that multiple cursors can be built in DrawSymbols
        m_pFeatureClass = fc
        m_pQueryFilter = queryFilter

        ' prepare filters on constituent renderers so I can use SymbolByFeature in Draw
        If Not m_pShapePatternRend Is Nothing Then m_pShapePatternRend.PrepareFilter(fc, queryFilter)
        If Not m_pColorRend1 Is Nothing Then m_pColorRend1.PrepareFilter(fc, queryFilter)
        If Not m_pColorRend2 Is Nothing Then m_pColorRend2.PrepareFilter(fc, queryFilter)
        If Not m_pSizeRend Is Nothing Then m_pSizeRend.PrepareFilter(fc, queryFilter)

        ' if we're combining colors from two (sequential) quantitative schemes, build color matrix now
        '   this gives flexibility to extend in future
        ' in current implementation we determine combined color based on two colors, one from each constituent 
        '   ClassBreaksRenderer.  so, we could determine color on demand when drawing. but, by creating 
        '   the color matrix here and storing for later use, we leave open the possibility of swapping in 
        '   different logic for determining combined colors based on all known colors in each constituent
        '   renderer, not just the colors for the given feature
        If (Not m_pColorRend1 Is Nothing) And (Not m_pColorRend2 Is Nothing) Then
            If Not m_eColorCombinationMethod = EColorCombinationType.enuComponents Then
                BuildColorMatrix()
            End If
        End If
        'implementation of IExportSupport
        AddExportFields(fc, queryFilter)

    End Sub

    Public ReadOnly Property RenderPhase(ByVal DrawPhase As esriDrawPhase) As Boolean Implements IFeatureRenderer.RenderPhase
        Get
            Return (DrawPhase = esriDrawPhase.esriDPGeography) Or (DrawPhase = esriDrawPhase.esriDPAnnotation)
        End Get
    End Property

    Public ReadOnly Property SymbolByFeature(ByVal Feature As IFeature) As ISymbol Implements IFeatureRenderer.SymbolByFeature
        Get
            Return GetFeatureSymbol(Feature)
        End Get
    End Property

    Public ReadOnly Property LegendGroup(ByVal Index As Integer) As ILegendGroup Implements ILegendInfo.LegendGroup
        Get
            Dim pLegendInfo As ILegendInfo = Nothing
            Dim strHeading As String

            Select Case Index
                Case 0
                    pLegendInfo = m_pMainRend
                    If m_pMainRend Is m_pShapePatternRend Then
                        strHeading = "Shape/Pattern: "
                    ElseIf m_pMainRend Is m_pSizeRend Then
                        strHeading = "Size: "
                    Else
                        strHeading = "Color 1: "
                    End If
                Case 1
                    If Not m_pShapePatternRend Is Nothing Then
                        If Not m_pSizeRend Is Nothing Then
                            pLegendInfo = m_pSizeRend
                            strHeading = "Size: "
                        Else
                            pLegendInfo = m_pColorRend1
                            strHeading = "Color 1: "
                        End If
                    Else
                        If Not m_pSizeRend Is Nothing Then
                            pLegendInfo = m_pColorRend1
                            strHeading = "Color 1: "
                        Else
                            pLegendInfo = m_pColorRend2
                            strHeading = "Color 2: "
                        End If
                    End If
                Case 2
                    pLegendInfo = m_pColorRend1
                    strHeading = "Color 1: "
                Case 3
                    pLegendInfo = m_pColorRend2
                    strHeading = "Color 2: "

            End Select

            Dim pLegendGroup As ILegendGroup
            pLegendGroup = pLegendInfo.LegendGroup(0)
            'pLegendGroup.Heading = strHeading & pLegendGroup.Heading

            Return pLegendGroup

        End Get
    End Property

    Public ReadOnly Property LegendGroupCount() As Integer Implements ILegendInfo.LegendGroupCount
        Get
            Dim pLegInfo As ILegendInfo
            Dim n As Integer

            n = 0
            If Not m_pSizeRend Is Nothing Then
                pLegInfo = m_pSizeRend
                If Not pLegInfo.LegendGroup(0) Is Nothing Then n = n + 1
            End If
            If Not m_pShapePatternRend Is Nothing Then
                pLegInfo = m_pShapePatternRend
                If Not pLegInfo.LegendGroup(0) Is Nothing Then n = n + 1
            End If
            If Not m_pColorRend1 Is Nothing Then
                pLegInfo = m_pColorRend1
                If Not pLegInfo.LegendGroup(0) Is Nothing Then n = n + 1
            End If
            If Not m_pColorRend2 Is Nothing And Not m_pColorRend2 Is m_pColorRend1 Then
                'If Not m_pColorRend2 Is Nothing Then
                pLegInfo = m_pColorRend2
                If Not pLegInfo.LegendGroup(0) Is Nothing Then n = n + 1
            End If

            Return n
        End Get
    End Property

    Public ReadOnly Property LegendItem() As ILegendItem Implements ILegendInfo.LegendItem
        Get
            Return Nothing
        End Get
    End Property

    Public Property SymbolsAreGraduated() As Boolean Implements ILegendInfo.SymbolsAreGraduated
        Get
            Return False
        End Get
        Set(ByVal Value As Boolean)
            ' NOT IMPL
        End Set
    End Property

    Public ReadOnly Property ID() As UID Implements IPersistVariant.ID
        Get
            Dim pUID As New UID
            pUID.Value = "MultivariateRenderer"
            'pUID.Value = ClassId
            Return pUID
        End Get
    End Property

    Public Sub Load(ByVal Stream As IVariantStream) Implements IPersistVariant.Load
        'load the persisted parameters of the renderer

        m_eColorCombinationMethod = Stream.Read
        m_pShapePatternRend = Stream.Read
        m_pColorRend1 = Stream.Read
        m_pColorRend2 = Stream.Read
        m_pSizeRend = Stream.Read
        'm_pLegendGroups = = Stream.Read
        m_sRotationField = Stream.Read
        m_eRotationType = Stream.Read
        m_sTransparencyField = Stream.Read
        m_pMainRend = Stream.Read

        'CreateLegend() ' not needed now
    End Sub

    Public Sub Save(ByVal Stream As IVariantStream) Implements IPersistVariant.Save
        'persist the settings for the renderer

        Stream.Write(m_eColorCombinationMethod)
        Stream.Write(m_pShapePatternRend)
        Stream.Write(m_pColorRend1)
        Stream.Write(m_pColorRend2)
        Stream.Write(m_pSizeRend)
        'Stream.Write(m_pLegendGroups)
        Stream.Write(m_sRotationField)
        Stream.Write(m_eRotationType)
        Stream.Write(m_sTransparencyField)
        Stream.Write(m_pMainRend)
    End Sub

    Private Function GetFeatureSymbol(ByVal pFeat As IFeature) As ISymbol

        Dim pSym As ISymbol

        ' get base symbol
        pSym = m_pMainRend.SymbolByFeature(pFeat)

        ' modify base symbol as necessary

        If (Not m_pSizeRend Is Nothing) And (Not m_pMainRend Is m_pSizeRend) And (Not pSym Is Nothing) Then
            pSym = ApplySize(pSym, pFeat)
        End If

        If ((Not m_pColorRend1 Is Nothing) Or (Not m_pColorRend2 Is Nothing)) And (Not pSym Is Nothing) Then
            pSym = ApplyColor(pSym, pFeat)
        End If

        If ((m_ShapeType = esriGeometryType.esriGeometryPoint) Or ((m_ShapeType = esriGeometryType.esriGeometryPolygon) And TypeOf pSym Is IMarkerSymbol)) And (Not pSym Is Nothing) Then
            If m_sRotationField <> "" Then
                pSym = ApplyRotation(pSym, pFeat)
            End If
        End If

        If m_sTransparencyField <> "" Then
            pSym = ApplyTransparency(pSym)
        End If
        'End If

        Return pSym

    End Function


    Private Function SortData(ByVal pCursor As IFeatureCursor, ByVal pTrackCancel As ITrackCancel) As IFeatureCursor
        ' sort in descending by value
        Dim pTable As ITable
        pTable = m_pFeatureClass

        Dim pTableSort As ITableSort
        pTableSort = New TableSort
        pTableSort.Table = pTable
        pTableSort.Cursor = pCursor

        ' why do I have to do this?
        Dim pQF As IQueryFilter
        pQF = New QueryFilter
        pQF.SubFields = "*"
        pQF.WhereClause = m_pQueryFilter.WhereClause
        pTableSort.QueryFilter = pQF

        Dim pPSRend As IProportionalSymbolRenderer
        pPSRend = m_pSizeRend
        Dim strValueField As String
        strValueField = pPSRend.Field
        pTableSort.Fields = strValueField
        pTableSort.Ascending(strValueField) = False

        Dim pDataNorm As IDataNormalization
        pDataNorm = pPSRend
        If pDataNorm.NormalizationType = esriDataNormalization.esriNormalizeByField Then
            ' comparison is not simple comparison of field values, use callback to do custom compare

            ' get normalization field and add to table sort
            Dim strFields As String = ""
            strFields = strFields & strValueField
            Dim strNormField As String
            strNormField = pDataNorm.NormalizationField
            strFields = strFields & ","
            strFields = strFields & strNormField
            pTableSort.Fields = strFields
            pTableSort.Ascending(strNormField) = False

            ' create new custom table call sort object and connect to the TableSort object
            Dim pTableSortCallBack As ITableSortCallBack
            pTableSortCallBack = New SortCallBack(pTable.Fields.FindField(strValueField), pTable.Fields.FindField(strNormField))
            pTableSort.Compare = pTableSortCallBack
        End If

        ' call the sort
        pTableSort.Sort(pTrackCancel)

        ' retrieve the sorted rows
        Dim pSortedCursor As IFeatureCursor
        pSortedCursor = pTableSort.Rows()

        Return pSortedCursor
    End Function

    Private Sub DrawSymbolsInOrder(ByVal Cursor As IFeatureCursor, ByVal drawPhase As esriDrawPhase, ByVal Display As IDisplay, ByVal trackCancel As ITrackCancel)
        ' this sub draws either markers or line symbols from large small so that the smallest symbols will be drawn on top

        ' in graduated symbol case, a cursor is built and parsed n times for n size classes
        ' in proportional symbol case, symbols are sorted and drawn from largest to smallest

        Dim iSizeIndex As Integer
        Dim iCurrentDrawableSymbolIndex As Integer
        Dim pMyCursor As IFeatureCursor
        Dim pFeat As IFeature
        Dim pFeatDraw As IFeatureDraw
        Dim bContinue As Boolean = True
        Dim pSizeSym As ISymbol
        Dim pDrawSym As ISymbol
        Dim pSortedCursor As IFeatureCursor

        If TypeOf m_pSizeRend Is IProportionalSymbolRenderer Then
            ' sort 
            pSortedCursor = SortData(Cursor, trackCancel)

            ' draw
            pFeat = pSortedCursor.NextFeature
            Do While Not pFeat Is Nothing
                pDrawSym = GetFeatureSymbol(pFeat)
                ' draw the feature
                pFeatDraw = pFeat
                Display.SetSymbol(pDrawSym)

                'implementation of IExportSupport
                BeginFeature(pFeat, Display)

                pFeatDraw.Draw(drawPhase, Display, pDrawSym, True, Nothing, esriDrawStyle.esriDSNormal)

                'implementation of IExportSupport
                GenerateExportInfo(pFeat, Display)
                EndFeature(Display)

                ' get next feature
                pFeat = pSortedCursor.NextFeature
                If Not trackCancel Is Nothing Then bContinue = trackCancel.[Continue]
            Loop

        Else
            Dim pSizeCBRend As IClassBreaksRenderer
            pSizeCBRend = m_pSizeRend
            pMyCursor = Cursor
            For iCurrentDrawableSymbolIndex = (pSizeCBRend.BreakCount - 1) To 0 Step -1
                ' do not build a cursor the 1st time because we already have one
                If iCurrentDrawableSymbolIndex < (pSizeCBRend.BreakCount - 1) Then
                    ' build pMyCursor
                    pMyCursor = m_pFeatureClass.Search(m_pQueryFilter, True)
                End If
                pFeat = pMyCursor.NextFeature
                Do While Not pFeat Is Nothing
                    ' check to see if we will draw in this pass
                    pSizeSym = m_pSizeRend.SymbolByFeature(pFeat)
                    iSizeIndex = GetSymbolIndex(pSizeSym, pSizeCBRend)
                    If (iSizeIndex = iCurrentDrawableSymbolIndex) Then
                        ' go ahead and draw the symbol
                        ' get symbol to draw
                        pDrawSym = GetFeatureSymbol(pFeat)

                        ' draw the feature
                        pFeatDraw = pFeat
                        Display.SetSymbol(pDrawSym)

                        'implementation of IExportSupport
                        BeginFeature(pFeat, Display)

                        pFeatDraw.Draw(drawPhase, Display, pDrawSym, True, Nothing, esriDrawStyle.esriDSNormal)

                        'implementation of IExportSupport
                        GenerateExportInfo(pFeat, Display)
                        EndFeature(Display)

                        If Not trackCancel Is Nothing Then bContinue = trackCancel.[Continue]
                    End If

                    pFeat = pMyCursor.NextFeature
                Loop

            Next iCurrentDrawableSymbolIndex ' increment DOWN to next symbol size

        End If

    End Sub

    Private Sub DrawSymbols(ByVal Cursor As IFeatureCursor, ByVal drawPhase As esriDrawPhase, ByVal Display As IDisplay, ByVal trackCancel As ITrackCancel)

        Dim pFeat As IFeature
        Dim pFeatDraw As IFeatureDraw
        Dim bContinue As Boolean = True
        Dim pDrawSym As ISymbol

        pFeat = Cursor.NextFeature
        bContinue = True
        ' while there are still more features and drawing has not been cancelled
        Do While (Not pFeat Is Nothing) And (bContinue = True)
            ' get symbol to draw
            pDrawSym = GetFeatureSymbol(pFeat)
            ' draw the feature
            pFeatDraw = pFeat
            Display.SetSymbol(pDrawSym)

            'implementation of IExportSupport
            BeginFeature(pFeat, Display)

            pFeatDraw.Draw(drawPhase, Display, pDrawSym, True, Nothing, esriDrawStyle.esriDSNormal)

            'implementation of IExportSupport
            GenerateExportInfo(pFeat, Display)
            EndFeature(Display)

            ' get next feature
            pFeat = Cursor.NextFeature
            If Not trackCancel Is Nothing Then bContinue = trackCancel.[Continue]
        Loop

    End Sub

    Private Function GetCombinedColor(ByVal pColor1 As IColor, ByVal pColor2 As IColor, ByVal eCombinationMethod As EColorCombinationType, Optional ByVal pOriginColor As IColor = Nothing) As ESRI.ArcGIS.Display.IColor
        ' combines the input colors based on m_eColorCombinationMethod

        Dim pOutColor As IColor

        Dim MyOLE_COLOR As Long ' As OLE_COLOR in VB6
        Dim pMainRGBColor As IRgbColor
        Dim pVariationRGBColor As IRgbColor
        Dim pMergedRGBColor As IRgbColor
        Dim bOK As Boolean
        Dim pAlgorithmicCR As IAlgorithmicColorRamp

        ' if either of the colors are null, then don't run the color through any algorithm,
        '   instead, just return the other color.  if both are null, then return a null color
        If pColor1.NullColor Then
            pOutColor = pColor2

        ElseIf pColor2.NullColor Then
            pOutColor = pColor1

        ElseIf eCombinationMethod = EColorCombinationType.enuComponents Then
            ' HSV components
            ' create a new HSV color
            Dim pHSVDrawColor As IHsvColor
            pHSVDrawColor = New HsvColor
            ' get HSV values from Color1 and Color2 and assign to pHSVDrawColor
            Dim pHSVColor1 As IHsvColor
            Dim pHSVColor2 As IHsvColor

            pHSVColor1 = New HsvColor
            pHSVColor1.RGB = pColor1.RGB
            pHSVColor2 = New HsvColor
            pHSVColor2.RGB = pColor2.RGB

            pHSVDrawColor.Hue = pHSVColor1.Hue
            pHSVDrawColor.Saturation = pHSVColor2.Saturation
            pHSVDrawColor.Value = pHSVColor2.Value

            pOutColor = pHSVDrawColor

        ElseIf eCombinationMethod = EColorCombinationType.enuRGBAverage Then
            ' use additive color model to merge the two colors
            MyOLE_COLOR = pColor1.RGB
            pMainRGBColor = New RgbColor
            pMainRGBColor.RGB = MyOLE_COLOR
            MyOLE_COLOR = pColor2.RGB
            pVariationRGBColor = New RgbColor
            pVariationRGBColor.RGB = MyOLE_COLOR
            ' merged color = RGB average of the two colors
            pMergedRGBColor = New RgbColor
            pMergedRGBColor.Red = (pMainRGBColor.Red + pVariationRGBColor.Red) / 2
            pMergedRGBColor.Green = (pMainRGBColor.Green + pVariationRGBColor.Green) / 2
            pMergedRGBColor.Blue = (pMainRGBColor.Blue + pVariationRGBColor.Blue) / 2

            pOutColor = pMergedRGBColor
        ElseIf (eCombinationMethod = EColorCombinationType.enuCIELabColorRamp) Or (eCombinationMethod = EColorCombinationType.enuLabLChColorRamp) Then
            ' use color ramp and take central color between the two colors
            pAlgorithmicCR = New AlgorithmicColorRamp
            If m_eColorCombinationMethod = EColorCombinationType.enuCIELabColorRamp Then
                pAlgorithmicCR.Algorithm = esriColorRampAlgorithm.esriCIELabAlgorithm
            Else
                pAlgorithmicCR.Algorithm = esriColorRampAlgorithm.esriLabLChAlgorithm
            End If
            pAlgorithmicCR.Size = 3
            pAlgorithmicCR.FromColor = pColor1
            pAlgorithmicCR.ToColor = pColor2
            pAlgorithmicCR.CreateRamp(bOK)

            pOutColor = pAlgorithmicCR.Color(1)     ' middle color in ramp
        Else ' EColorCombinationType.enuCIELabMatrix

            Dim iLab1(3) As Double ' L, a, b values for Color1
            Dim iLab2(3) As Double ' L, a, b values for Color2
            Dim iLabOrig(3) As Double ' L, a, b values for pOriginColor
            pColor1.GetCIELAB(iLab1(0), iLab1(1), iLab1(2))
            pColor2.GetCIELAB(iLab2(0), iLab2(1), iLab2(2))
            pOriginColor.GetCIELAB(iLabOrig(0), iLabOrig(1), iLabOrig(2))

            Dim iLabOut(3) As Double
            ' add color1 vector and color2 vector, then subtract the origin color vector
            iLabOut(0) = iLab1(0) + iLab2(0) - iLabOrig(0)
            iLabOut(1) = iLab1(1) + iLab2(1) - iLabOrig(1)
            iLabOut(2) = iLab1(2) + iLab2(2) - iLabOrig(2)

            CorrectLabOutofRange(iLabOut(0), iLabOut(1), iLabOut(2))

            Dim pHSVColor As IHsvColor
            pHSVColor = New HsvColor
            pHSVColor.SetCIELAB(iLabOut(0), iLabOut(1), iLabOut(2))
            pOutColor = pHSVColor
        End If

        Return pOutColor

    End Function

    Private Sub CorrectLabOutofRange(ByRef L As Double, ByRef a As Double, ByRef b As Double)

        If L > 100 Then
            L = 100
        ElseIf L < 0 Then
            L = 0
        End If

        If a > 120 Then
            a = 120
        ElseIf a < -120 Then
            a = -120
        End If

        If b > 120 Then
            b = 120
        ElseIf b < -120 Then
            b = -120
        End If

    End Sub

    Private Sub RemoveLegend()

        Dim i As Integer

        If Not m_pLegendGroups Is Nothing Then
            For i = 0 To UBound(m_pLegendGroups)
                m_pLegendGroups(i) = Nothing
            Next i
        End If
    End Sub

    Private Function CalcMainRend() As IFeatureRenderer
        ' consider using an internal array to keep track of active arrays in correct order, this will make it easier to implement ILegendInfo

        If (Not m_pShapePatternRend Is Nothing) Then
            If (m_ShapeType = esriGeometryType.esriGeometryPolygon) And Not m_pSizeRend Is Nothing Then
                Return m_pSizeRend
            Else
                Return m_pShapePatternRend
            End If
        ElseIf (Not m_pSizeRend Is Nothing) Then
            Return m_pSizeRend
        ElseIf (Not m_pColorRend1 Is Nothing) Then
            Return m_pColorRend1
        ElseIf (Not m_pColorRend2 Is Nothing) Then
            Return m_pColorRend2
        Else
            Return Nothing ' must have shape or color or size, if not you can't render...
        End If

    End Function

    Public Property ColorCombinationMethod() As EColorCombinationType Implements IMultivariateRenderer.ColorCombinationMethod
        Get
            Return m_eColorCombinationMethod
        End Get
        Set(ByVal Value As EColorCombinationType)
            m_eColorCombinationMethod = Value
        End Set
    End Property

    Public Property ColorRend1() As ESRI.ArcGIS.Carto.IFeatureRenderer Implements IMultivariateRenderer.ColorRend1
        Get
            Return m_pColorRend1
        End Get
        Set(ByVal Value As ESRI.ArcGIS.Carto.IFeatureRenderer)
            m_pColorRend1 = Value
            m_pMainRend = CalcMainRend()
        End Set
    End Property

    Public Property ColorRend2() As ESRI.ArcGIS.Carto.IFeatureRenderer Implements IMultivariateRenderer.ColorRend2
        Get
            Return m_pColorRend2
        End Get
        Set(ByVal Value As ESRI.ArcGIS.Carto.IFeatureRenderer)
            m_pColorRend2 = Value
        End Set
    End Property

    Public Property ShapePatternRend() As ESRI.ArcGIS.Carto.IFeatureRenderer Implements IMultivariateRenderer.ShapePatternRend
        Get
            Return m_pShapePatternRend
        End Get
        Set(ByVal Value As ESRI.ArcGIS.Carto.IFeatureRenderer)
            m_pShapePatternRend = Value
            m_pMainRend = CalcMainRend()
        End Set
    End Property

    Public Property SizeRend() As ESRI.ArcGIS.Carto.IFeatureRenderer Implements IMultivariateRenderer.SizeRend
        Get
            Return m_pSizeRend
        End Get
        Set(ByVal Value As ESRI.ArcGIS.Carto.IFeatureRenderer)
            m_pSizeRend = Value
            m_pMainRend = CalcMainRend()
        End Set
    End Property

    Private Function ApplyRotation(ByVal pMarkerSym As IMarkerSymbol, ByVal pFeat As IFeature) As IMarkerSymbol

        Dim lAngle As Double
        lAngle = Convert.ToDouble(pFeat.Value(pFeat.Fields.FindField(m_sRotationField)))

        If m_eRotationType = esriSymbolRotationType.esriRotateSymbolGeographic Then
            pMarkerSym.Angle = pMarkerSym.Angle - lAngle
        Else
            pMarkerSym.Angle = pMarkerSym.Angle + lAngle - 90
        End If

        Return pMarkerSym
    End Function

    Private Function ApplyTransparency(ByVal pSym As ISymbol) As ISymbol

        ' TODO

        Return pSym
    End Function

    Private Function ApplyColor(ByVal pSym As ISymbol, ByVal pFeat As IFeature) As ISymbol
        On Error GoTo ErrHand


        Dim pSym1 As ISymbol
        Dim pSym2 As ISymbol
        Dim pColor As IColor
        Dim pHSVColor As IHsvColor

        If (Not m_pColorRend1 Is Nothing) And (Not m_pColorRend2 Is Nothing) Then ' for now both color renderers need to be set to apply color
            pSym1 = m_pColorRend1.SymbolByFeature(pFeat)
            pSym2 = m_pColorRend2.SymbolByFeature(pFeat)
            ' only use GetCombinedColor for HSV component-type combination method
            If m_eColorCombinationMethod = EColorCombinationType.enuComponents Then
                pColor = GetCombinedColor(GetSymbolColor(pSym1), GetSymbolColor(pSym2), m_eColorCombinationMethod)

                pHSVColor = pColor

            Else
                'pColor = m_pColorMatrix(GetSymbolIndex(pSym1, m_pColorRend1), GetSymbolIndex(pSym2, m_pColorRend2))
                pColor = New RgbColor
                pColor.RGB = m_OLEColorMatrix(GetSymbolIndex(pSym1, m_pColorRend1), GetSymbolIndex(pSym2, m_pColorRend2))
            End If


            If TypeOf pSym Is IMarkerSymbol Then
                Dim pMarkerSym As IMarkerSymbol
                pMarkerSym = pSym
                pMarkerSym.Color = pColor
            ElseIf TypeOf pSym Is ILineSymbol Then
                Dim pLineSym As ILineSymbol
                pLineSym = pSym
                pLineSym.Color = pColor
            Else
                Dim pFillSym As IFillSymbol
                pFillSym = pSym
                pFillSym.Color = pColor

            End If


        End If

        Return pSym
        Exit Function
ErrHand:
        MsgBox("Apply Color " & Err.Description & "Line: " & Err.Erl)
    End Function


    

    Private Function ApplySize(ByVal pSym As ISymbol, ByVal pFeat As IFeature) As ISymbol

        If TypeOf pSym Is IMarkerSymbol Then
            ' Marker Symbol
            Dim pTargetMarkerSym As IMarkerSymbol
            pTargetMarkerSym = pSym
            Dim pSourceMarkerSym As IMarkerSymbol
            pSourceMarkerSym = m_pSizeRend.SymbolByFeature(pFeat)
            If Not (pSourceMarkerSym Is Nothing) Then
                pTargetMarkerSym.Size = pSourceMarkerSym.Size
            End If
        Else
            ' Line Symbol
            Dim pTargetLineSym As ILineSymbol
            pTargetLineSym = pSym
            Dim pSourceLineSym As ILineSymbol
            pSourceLineSym = m_pSizeRend.SymbolByFeature(pFeat)

            If Not (pSourceLineSym Is Nothing) Then
                pTargetLineSym.Width = pSourceLineSym.Width
            End If
        End If

        Return pSym
    End Function

    Public Property RotationField() As String Implements IRotationRenderer.RotationField
        Get
            Return m_sRotationField
        End Get
        Set(ByVal Value As String)
            m_sRotationField = Value
        End Set
    End Property

    Public Property TransparencyField() As String Implements ITransparencyRenderer.TransparencyField
        Get
            Return m_sTransparencyField
        End Get
        Set(ByVal Value As String)
            m_sTransparencyField = Value
        End Set
    End Property

    Public Property RotationType() As ESRI.ArcGIS.Carto.esriSymbolRotationType Implements IRotationRenderer.RotationType
        Get
            Return m_eRotationType
        End Get
        Set(ByVal Value As ESRI.ArcGIS.Carto.esriSymbolRotationType)
            m_eRotationType = Value
        End Set
    End Property

    Private Function GetSymbolIndex(ByVal pSym As ISymbol, ByVal pRend As IClassBreaksRenderer) As Integer
        ' given an input symbol and a renderer, this function returns the index of
        '   the class that the symbol represents in the renderer

        Dim i As Integer
        Dim iNumBreaks As Integer

        iNumBreaks = pRend.BreakCount
        i = 0
        Dim pLegendInfo As ILegendInfo
        pLegendInfo = pRend
        Do While (i < iNumBreaks - 1)
            If pLegendInfo.SymbolsAreGraduated Then
                ' compare based on size
                If SymbolsAreSameSize(pSym, pRend.Symbol(i)) Then Exit Do
            Else
                ' compare based on color
                If SymbolsAreSameColor(pSym, pRend.Symbol(i)) Then Exit Do
            End If
            i = i + 1
        Loop

        Return i

    End Function

    Private Function SymbolsAreSameSize(ByVal pSym1 As ISymbol, ByVal psym2 As ISymbol) As Boolean

        If TypeOf pSym1 Is IMarkerSymbol Then
            Dim pMS1 As IMarkerSymbol
            Dim pMS2 As IMarkerSymbol
            pMS1 = pSym1
            pMS2 = psym2
            Return pMS1.Size = pMS2.Size
        Else
            Dim pLS1 As ILineSymbol
            Dim pLS2 As ILineSymbol
            pLS1 = pSym1
            pLS2 = psym2
            Return pLS1.Width = pLS2.Width
        End If

    End Function

    Private Function SymbolsAreSameColor(ByVal pSym1 As ISymbol, ByVal psym2 As ISymbol) As Boolean

        Dim pColor1 As IColor
        Dim pColor2 As IColor
        pColor1 = GetSymbolColor(pSym1)
        pColor2 = GetSymbolColor(psym2)
        Return pColor1.RGB = pColor2.RGB

    End Function

    Private Sub BuildColorMatrix()
        On Error GoTo ErrHand

        Dim pCBRend1 As IClassBreaksRenderer
        Dim pCBRend2 As IClassBreaksRenderer

        pCBRend1 = New ClassBreaksRenderer()
        pCBRend2 = New ClassBreaksRenderer()

        If ((TypeOf m_pColorRend1 Is IFeatureRenderer) And (TypeOf m_pColorRend2 Is IFeatureRenderer)) Then
            pCBRend1 = CType(m_pColorRend1, IClassBreaksRenderer)
            pCBRend2 = CType(m_pColorRend2, IClassBreaksRenderer)

            Dim i As Integer
            Dim j As Integer
            Dim pColor1 As IColor
            Dim pColor2 As IColor
            Dim pColor As IColor

            If m_eColorCombinationMethod = EColorCombinationType.enuCIELabMatrix Then
                ' new (11/5/04) 

                ' origin (CIELab average now, but would be better to extend both lines to intersection point,
                '   or average of points where they are closest)
                pColor1 = GetSymbolColor(pCBRend1.Symbol(0))
                pColor2 = GetSymbolColor(pCBRend2.Symbol(0))
                pColor = GetCombinedColor(pColor1, pColor2, EColorCombinationType.enuCIELabColorRamp)
                Dim pOriginColor As IColor
                pOriginColor = pColor
                m_OLEColorMatrix(i, j) = pColor.RGB

                ' bottom edge (known)
                For i = 1 To pCBRend1.BreakCount - 1
                    pColor = GetSymbolColor(pCBRend1.Symbol(i))
                    m_OLEColorMatrix(i, 0) = pColor.RGB
                Next

                ' left edge (known)
                For j = 1 To pCBRend2.BreakCount - 1
                    pColor = GetSymbolColor(pCBRend2.Symbol(j))
                    m_OLEColorMatrix(0, j) = pColor.RGB
                Next

                ' remaining values (interpolated)
                For i = 1 To pCBRend1.BreakCount - 1
                    For j = 1 To pCBRend2.BreakCount - 1
                        pColor1 = GetSymbolColor(pCBRend1.Symbol(i))
                        pColor2 = GetSymbolColor(pCBRend2.Symbol(j))
                        pColor = GetCombinedColor(pColor1, pColor2, EColorCombinationType.enuCIELabMatrix, pOriginColor)
                        m_OLEColorMatrix(i, j) = pColor.RGB
                    Next j
                Next i

            Else

                For i = 0 To pCBRend1.BreakCount - 1
                    For j = 0 To pCBRend2.BreakCount - 1
                        pColor1 = GetSymbolColor(pCBRend1.Symbol(i))
                        pColor2 = GetSymbolColor(pCBRend2.Symbol(j))
                        pColor = GetCombinedColor(pColor1, pColor2, m_eColorCombinationMethod)
                        m_OLEColorMatrix(i, j) = pColor.RGB

                    Next j
                Next i

            End If
        
        End If

        Exit Sub
ErrHand:
        MsgBox(Err.Description)

    End Sub

    Private Function GetSymbolColor(ByVal pSym As ISymbol) As IColor
        Dim pMarkerSym As IMarkerSymbol
        Dim pLineSym As ILineSymbol
        Dim pFillSym As IFillSymbol
        Dim pColor As IColor


        If TypeOf pSym Is IMarkerSymbol Then
            pMarkerSym = pSym
            pColor = pMarkerSym.Color
        ElseIf TypeOf pSym Is ILineSymbol Then
            pLineSym = pSym
            pColor = pLineSym.Color
        ElseIf Not pSym Is Nothing Then
            pFillSym = pSym
            pColor = pFillSym.Color
        Else
            pColor = Nothing
        End If

        Return pColor

    End Function


End Class



'implementation of IExportSupport
' ExportSupport is a private helper class to help the renderer implement of IExportSupport.  This class contains
' the reference to the ExportInfoGenerator object used by the renderer.

Public Class ExportSupport
  Implements IExportSupport

  Dim m_symbologyEnvironment2 As ISymbologyEnvironment2
  Dim m_exportInfoGenerator As IFeatureExportInfoGenerator
  Dim m_exportAttributes As Boolean
  Dim m_exportHyperlinks As Boolean


  Public WriteOnly Property ExportInfo() As ESRI.ArcGIS.Carto.IFeatureExportInfoGenerator Implements ESRI.ArcGIS.Carto.IExportSupport.ExportInfo
    Set(ByVal value As ESRI.ArcGIS.Carto.IFeatureExportInfoGenerator)
      m_exportInfoGenerator = value
    End Set
  End Property


  Public Sub New()
    m_exportAttributes = False
    m_exportHyperlinks = False
  End Sub


  Protected Overrides Sub Finalize()
    m_symbologyEnvironment2 = Nothing
    m_exportInfoGenerator = Nothing
    MyBase.Finalize()
  End Sub



  Public Sub GetExportSettings()

    m_exportAttributes = False
    m_exportHyperlinks = False

    If m_exportInfoGenerator Is Nothing Then Exit Sub

    If m_symbologyEnvironment2 Is Nothing Then
      m_symbologyEnvironment2 = New SymbologyEnvironment
    End If

    m_exportAttributes = m_symbologyEnvironment2.OutputGDICommentForFeatureAttributes
    m_exportHyperlinks = m_symbologyEnvironment2.OutputGDICommentForHyperlinks

  End Sub


  Public Sub GenerateExportInfo(ByRef feature As IFeature, ByRef display As IDisplay)

    If m_exportInfoGenerator Is Nothing Then Exit Sub

    If m_exportAttributes Then m_exportInfoGenerator.GenerateFeatureInfo(feature, display)
    If m_exportHyperlinks Then m_exportInfoGenerator.GenerateHyperlinkInfo(feature, display)

  End Sub


  Public Sub GenerateExportInfo(ByRef featureDraw As IFeatureDraw, ByRef display As IDisplay)

    If m_exportInfoGenerator Is Nothing Then Exit Sub

    Dim feature As IFeature
    feature = featureDraw

    If m_exportAttributes Then m_exportInfoGenerator.GenerateFeatureInfo(feature, display)
    If m_exportHyperlinks Then m_exportInfoGenerator.GenerateHyperlinkInfo(feature, display)

  End Sub


  Public Sub AddExportFields(ByRef fc As IFeatureClass, ByRef queryFilter As IQueryFilter)

    If m_exportInfoGenerator Is Nothing Then Exit Sub

    GetExportSettings()

    If m_exportAttributes Or m_exportHyperlinks Then
      m_exportInfoGenerator.PrepareExportFilter(fc, queryFilter)
    End If

  End Sub


  Public Sub BeginFeature(ByRef feature As IFeature, ByRef display As IDisplay)

    If m_exportInfoGenerator Is Nothing Then Exit Sub

    m_exportInfoGenerator.BeginFeature(feature, display)

  End Sub


  Public Sub EndFeature(ByRef display As IDisplay)

    If m_exportInfoGenerator Is Nothing Then Exit Sub

    m_exportInfoGenerator.EndFeature(display)

  End Sub


End Class



<ComClass(SortCallBack.ClassId, SortCallBack.InterfaceId, SortCallBack.EventsId)> _
Public Class SortCallBack ' would like to declare this as private

    ' class definition for SortCallBack which implements custom table sorting based on field / normalization field

    Implements ITableSortCallBack

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "13137B0F-2255-46a4-9D6E-0A68FA560379"
    Public Const InterfaceId As String = "68A049DC-8E6C-4759-9797-960076474729"
    Public Const EventsId As String = "7BE0E19F-9AB3-4dd4-BB79-6EBD85E7930E"

#End Region

    ' data members
    Private m_value1 As VariantType
    Private m_value2 As VariantType
    Private m_iValueIndex As Integer
    Private m_iNormIndex As Integer

    Public Sub New(ByVal ValueIndex As Integer, ByVal NormIndex As Integer)
        m_iValueIndex = ValueIndex
        m_iNormIndex = NormIndex

    End Sub

    Public Function Compare(ByVal value1 As Object, ByVal value2 As Object, ByVal FieldIndex As Integer, ByVal fieldSortIndex As Integer) As Integer Implements ITableSortCallBack.Compare

        ' sort normalized values 

        If (FieldIndex = m_iValueIndex) Then

            m_value1 = value1
            m_value2 = value2
            Exit Function ' ?
        End If

        If (FieldIndex = m_iNormIndex) Then

            If (value1 = 0) Or (value2 = 0) Then Exit Function ' ?

            Dim dblNormedVal1 As Double
            Dim dblNormedVal2 As Double

            dblNormedVal1 = m_value1 / value1
            dblNormedVal2 = m_value2 / value2

            If dblNormedVal1 > dblNormedVal2 Then
                Compare = 1
            ElseIf dblNormedVal1 < dblNormedVal2 Then
                Compare = -1
            Else
                Compare = 0
            End If
        End If

    End Function

End Class