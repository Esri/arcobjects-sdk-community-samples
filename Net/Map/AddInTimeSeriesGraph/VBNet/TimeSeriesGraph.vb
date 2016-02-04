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
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI

Public Class TimeSeriesGraph
    Inherits ESRI.ArcGIS.Desktop.AddIns.Tool

    Public Sub New()

    End Sub

    Protected Overrides Sub OnMouseDown(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
        On Error Resume Next
        Dim X As Integer
        Dim Y As Integer
        X = arg.X
        Y = arg.Y

        Dim pMxApp As IMxApplication
        Dim pMxDoc As IMxDocument
        pMxApp = TimeSeriesGraphAddInVB.My.ArcMap.Application
        pMxDoc = pMxApp.Document

        ' calculate tolerance rectangle to identify features inside it
        Dim Tolerance As Integer
        Tolerance = pMxDoc.SearchTolerancePixels

        Dim pDispTrans As IDisplayTransformation
        pDispTrans = pMxApp.Display.DisplayTransformation
        Dim pToleranceRect As ESRI.ArcGIS.esriSystem.tagRECT
        pToleranceRect.left = X - Tolerance
        pToleranceRect.right = X + Tolerance
        pToleranceRect.top = Y - Tolerance
        pToleranceRect.bottom = Y + Tolerance

        Dim pSearchEnvelope As IEnvelope
        pSearchEnvelope = New Envelope
        pDispTrans.TransformRect(pSearchEnvelope, pToleranceRect, (ESRI.ArcGIS.Display.esriDisplayTransformationEnum.esriTransformPosition Or ESRI.ArcGIS.Display.esriDisplayTransformationEnum.esriTransformToMap))

        ' identify feature points of measurement
        Dim pBasicDoc As IBasicDocument
        pBasicDoc = pMxApp.Document
        pSearchEnvelope.SpatialReference = pMxDoc.ActiveView.FocusMap.SpatialReference

        Dim pIdentify As IIdentify
        pIdentify = pMxDoc.FocusMap.Layer(0)
        If pIdentify Is Nothing Then
            MsgBox("No layer")
            Exit Sub
        End If

        Dim pIDArray As IArray
        pIDArray = pIdentify.Identify(pSearchEnvelope)

        ' get object from feature point
        Dim pIDObj As IIdentifyObj
        pIDObj = Nothing
        If Not pIDArray Is Nothing Then
            pIDObj = pIDArray.Element(0)
        End If

        If pIDObj Is Nothing Then
            MsgBox("No feature was identified")
            Exit Sub
        End If

        ' get the name of the layer containing feature points
        Dim pLayer As ILayer
        pLayer = pMxDoc.FocusMap.Layer(0)

        Dim layerName As String
        layerName = pLayer.Name

        ' get primary display field for measurement values and set names of a date/time field and gage ID field
        Dim pFeatLayer As IFeatureLayer
        pFeatLayer = pLayer
        Dim dataFldName As String
        Dim timefldName As String
        Dim gageIDFldName As String
        dataFldName = "TSValue"
        timefldName = "TSDateTime"   ' substitute data/time field name for different dataset
        gageIDFldName = "Name"         ' substitute gage ID field name for different dataset

        ' get display table from layer
        Dim pTable As ITable
        pTable = Nothing
        Dim pDisplayTable As IDisplayTable
        pDisplayTable = pLayer
        If Not pDisplayTable Is Nothing Then
            pTable = pDisplayTable.DisplayTable
            If pTable Is Nothing Then GoTo THEEND
        End If

        ' get fields from display table
        Dim pFields As IFields
        pFields = pTable.Fields
        Dim fldCount As Long
        fldCount = pFields.FieldCount

        ' create WHERE clause from identified objects of measurement points
        Dim gageIDFldIdx As Integer
        gageIDFldIdx = pFields.FindField(gageIDFldName)

        Dim pRowIDObj As IRowIdentifyObject
        pRowIDObj = pIDObj

        Dim gageID As String
        gageID = pRowIDObj.Row.Value(gageIDFldIdx)

        Dim pFeatureLayerDef As IFeatureLayerDefinition
        pFeatureLayerDef = pLayer
        Dim definitionExpression As String
        definitionExpression = pFeatureLayerDef.DefinitionExpression

        Dim whereClause As String
        If definitionExpression = "" Then
            whereClause = "[" + gageIDFldName + "] = '" + gageID + "'"
        Else
            whereClause = "[" + gageIDFldName + "] = '" + gageID + "' AND " + definitionExpression
        End If

        'find color for the identified object from feature layer's renderer
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pGeoFeatureLayer = pLayer

        Dim pLookupSymbol As ILookupSymbol
        pLookupSymbol = pGeoFeatureLayer.Renderer

        Dim pFeature As IFeature
        pFeature = pRowIDObj.Row

        Dim pSymbol As IMarkerSymbol
        pSymbol = pLookupSymbol.LookupSymbol(False, pFeature)

        ' Find an opened GraphWindow
        Dim pDataGraphBase As IDataGraphBase
        pDataGraphBase = Nothing
        Dim pDataGraphT As IDataGraphT
        Dim pDGWin As IDataGraphWindow2
        pDGWin = Nothing

        Dim pDataGraphs As IDataGraphCollection
        pDataGraphs = pMxDoc
        Dim grfCount As Integer
        grfCount = pDataGraphs.DataGraphCount
        Dim i As Integer
        For i = 0 To (grfCount - 1)
            pDataGraphBase = pDataGraphs.DataGraph(i)
            pDGWin = FindGraphWindow(pDataGraphBase)
            If Not pDGWin Is Nothing Then Exit For
        Next i

        ' if there is not an opened graph window - create a new graph for
        If pDGWin Is Nothing Then
            ' create graph
            pDataGraphBase = New DataGraphT
            pDataGraphT = pDataGraphBase

            ' load template from <ARCGISHOME>\GraphTemplates\
            Dim strPath As String
            strPath = Environment.GetEnvironmentVariable("ARCGISHOME")
            pDataGraphT.LoadTemplate(strPath + "GraphTemplates\timeseries.tee")

            ' graph, axis and legend titles. Substitute them for different input layer
            pDataGraphT.GeneralProperties.Title = "Daily Streamflow for Guadalupe Basin in 1999"
            pDataGraphT.LegendProperties.Title = "Monitoring Point"
            pDataGraphT.AxisProperties(0).Title = "Streamflow (cfs)"
            pDataGraphT.AxisProperties(0).Logarithmic = True
            pDataGraphT.AxisProperties(2).Title = "Date"
            pDataGraphBase.Name = layerName
        Else ' get graph from the opened window
            pDataGraphT = pDataGraphBase
        End If

        ' create vertical line series for all measurements for the identified gage
        Dim pSP As ISeriesProperties
        pSP = pDataGraphT.AddSeries("line:vertical")
        pSP.ColorType = esriGraphColorType.esriGraphColorCustomAll
        pSP.CustomColor = pSymbol.Color.RGB
        pSP.WhereClause = whereClause
        pSP.InLegend = True
        pSP.Name = gageID

        pSP.SourceData = pLayer
        pSP.SetField(0, timefldName)
        pSP.SetField(1, dataFldName)
        Dim pSortFlds As IDataSortSeriesProperties
        pSortFlds = pSP
        Dim idx As Long
        pSortFlds.AddSortingField(timefldName, True, idx)


        pDataGraphBase.UseSelectedSet = True

        Dim pCancelTracker As ITrackCancel
        pCancelTracker = New CancelTracker
        pDataGraphT.Update(pCancelTracker)

        ' create data graph window if there is not any opened one
        If pDGWin Is Nothing Then
            pDGWin = New DataGraphWindow
            pDGWin.DataGraphBase = pDataGraphBase
            pDGWin.Application = pMxApp
            pDGWin.Show(True)

            pDataGraphs.AddDataGraph(pDataGraphBase)
        End If

THEEND:
        Exit Sub
        'MyBase.OnMouseDown(arg)
    End Sub

    ' finds an opened graph window
    Private Function FindGraphWindow(ByRef pDataGraphBase As IDataGraphBase) As IDataGraphWindow2

        Dim pApplicationWindows As IApplicationWindows
        pApplicationWindows = TimeSeriesGraphAddInVB.My.ArcMap.Application

        Dim pDataWindows As ISet
        pDataWindows = pApplicationWindows.DataWindows
        Dim winCount As Integer
        winCount = pDataWindows.Count
        If winCount <= 0 Then
            FindGraphWindow = Nothing
            Exit Function
        End If

        pDataWindows.Reset()

        Dim pDataGraphWindow2 As IDataGraphWindow2
        pDataGraphWindow2 = Nothing
        Dim i As Integer
        For i = 0 To (winCount - 1)

            pDataGraphWindow2 = pDataWindows.Next
            If Not pDataGraphWindow2 Is Nothing Then
                Dim pDataGraphTmp As IDataGraphBase
                pDataGraphTmp = pDataGraphWindow2.DataGraphBase
                If pDataGraphBase Is pDataGraphTmp Then
                    Exit For
                End If
            End If

        Next i

        FindGraphWindow = pDataGraphWindow2
    End Function

    Protected Overrides Sub OnUpdate()
        Enabled = TimeSeriesGraphAddInVB.My.ArcMap.Application IsNot Nothing
    End Sub
End Class
