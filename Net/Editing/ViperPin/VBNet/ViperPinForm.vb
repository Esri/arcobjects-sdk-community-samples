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
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports System
Imports System.Windows.Forms
Imports Microsoft.Win32

Namespace ViperPin
  Partial Public Class ViperPinForm
	  Inherits Form
    Private m_editor As IEditor3
  Private m_curve As IPolyline
  Private m_editLayers As IEditLayers
  Private m_edSketch As IEditSketch3
  Private m_lotNum As Integer

    Public Sub New(ByVal editor As IEditor3)
      InitializeComponent()

      m_editor = editor
      m_edSketch = TryCast(m_editor, IEditSketch3)
      m_editLayers = TryCast(m_editor, IEditLayers)

      lblEditLayer.Text = m_editLayers.CurrentLayer.Name

      'Load field combo box with field names
      Dim fields As IFields = m_editLayers.CurrentLayer.FeatureClass.Fields
      For i As Integer = 0 To fields.FieldCount - 1
        cmbPINField.Items.Add(fields.Field(i).Name)
      Next i

      'get pinfield from registry
      Dim pinField As String = Nothing
      Dim pRegKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\ESRI\ViperPin")
      If pRegKey IsNot Nothing Then
        pinField = pRegKey.GetValue("Pinfield").ToString()
      End If

      'set the combo box to the pinfield
      For i As Integer = 0 To fields.FieldCount - 1
        If pinField = fields.Field(i).Name Then
          cmbPINField.Text = pinField
          Exit For
        Else
          cmbPINField.Text = "None"
        End If
      Next i

      'cmbPINField.SelectedIndex = 0;
      cmbPINField.Refresh()
      m_lotNum = 1
      txtlot.Text = "1"

      'Set center right of form to center right of screen
      Me.StartPosition = FormStartPosition.Manual
      Me.Left = 0
      Me.Top = CInt((Screen.PrimaryScreen.Bounds.Height / 2) - (Me.Height \ 2))
    End Sub

  Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdOK.Click
    m_lotNum = Integer.Parse(txtlot.Text)

    'Set pin value
    SetPINValue()

    'save pinfield
    Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software", True)
    Dim newKey As RegistryKey = regKey.CreateSubKey("ESRI\ViperPin")
    newKey.SetValue("Pinfield", cmbPINField.Text)

    Me.Hide()

      'redraw labels
      'm_Editor.Display.Invalidate(Nothing, True, CShort(esriViewDrawPhase.esriViewGraphics))

      Dim mxDoc As IMxDocument
      mxDoc = TryCast(m_editor.Parent.Document, IMxDocument)
      mxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
  End Sub

  Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.Close()
  End Sub

  Private Sub SetPINValue()
    'The Theory.
    'Select polygons that intersect the sketch.
    'Construct one polyline from the boundaries and intersect with sketch.
    'Sort resulting intersection locations (multipoint) by distance of the intersect
    ' from the start of the sketch and create new ordered multipoint.
    'Loop through new ordered multipoint, select underlying parcel and calc pin.

    Dim featLayer As IFeatureLayer = m_editLayers.CurrentLayer
    m_curve = TryCast(m_edSketch.Geometry, IPolyline)

    'Search parcel polys by graphic to get feature cursor
    Dim spatialFilter As ISpatialFilter = New SpatialFilterClass()
    spatialFilter.Geometry = m_curve
    spatialFilter.GeometryField = m_editLayers.CurrentLayer.FeatureClass.ShapeFieldName
    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses

            Dim featCursor As IFeatureCursor = featLayer.Search(spatialFilter, False)
    Dim feature As IFeature = featCursor.NextFeature()

    'If we have no intersects then exit
    If feature Is Nothing Then
        Return
    End If

    'Make a GeomBag of the polygons boundaries (polylines)
    Dim geomBag As IGeometryCollection = New GeometryBagClass()
    Dim missing As Object = Type.Missing
    Do While feature IsNot Nothing
        Dim pPoly As ITopologicalOperator = TryCast(feature.Shape, ITopologicalOperator)
        geomBag.AddGeometry(pPoly.Boundary, missing, missing)
        feature = featCursor.NextFeature()
    Loop

    'Make one polyline from the boundaries
    Dim polyLineU As IPolyline = New PolylineClass()
    Dim topoOp As ITopologicalOperator = TryCast(polyLineU, ITopologicalOperator)
    topoOp.ConstructUnion(TryCast(geomBag, IEnumGeometry))

    'Get the intersections of the boundaries and the curve
    Dim pointCol As IPointCollection = TryCast(topoOp.Intersect(m_curve, esriGeometryDimension.esriGeometry0Dimension), IPointCollection)

    'The point collection is not ordered by distance along the curve so
    'need to create a new collection with this info

    Dim pointOrder(pointCol.PointCount - 1) As Integer
    Dim dac As Double = 0, dfc As Double = 0
    Dim bRS As Boolean = False

    For i As Integer = 0 To pointCol.PointCount - 1
        Dim queryPoint As IPoint = New PointClass()
        pointCol.QueryPoint(i, queryPoint)
        m_curve.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, queryPoint, False, Nothing, dac, dfc, bRS)
        pointOrder(i) = CInt(Fix(dac))
    Next i

    'use built in bubble sort
    System.Array.Sort(pointOrder)

    'Loop through the sorted array and calc midpoint between parcel boundaries
    Dim midPoints As IPointCollection = New MultipointClass()

    For i As Integer = 0 To pointOrder.Length - 2
        'Get the midpoint distance
        Dim midPointDist As Double = (pointOrder(i) + pointOrder(i + 1)) / 2

        'create a point at the distance and store in point collection
        Dim queryPoint As IPoint = New PointClass()
        m_curve.QueryPoint(esriSegmentExtension.esriNoExtension, midPointDist, False, queryPoint)
        midPoints.AddPoint(queryPoint, missing, missing)
    Next i

    'If ends of sketch are included then add them as points
    If chkEnds.Checked Then
        Dim before As Object = TryCast(0, Object)
        midPoints.AddPoint(m_curve.FromPoint, before, missing)
        midPoints.AddPoint(m_curve.ToPoint, missing, missing)
    End If

    m_editor.StartOperation()

    'Loop through calculated midpoints, select polygon and calc pin
    For i As Integer = 0 To midPoints.PointCount - 1
        Dim midPoint As IPoint = midPoints.Point(i)
        spatialFilter = New SpatialFilterClass()
        spatialFilter.Geometry = midPoint
        spatialFilter.GeometryField = m_editLayers.CurrentLayer.FeatureClass.ShapeFieldName
        spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin

        featCursor = featLayer.Search(spatialFilter, False)
        feature = featCursor.NextFeature()
        Do While feature IsNot Nothing
          feature.Value(feature.Fields.FindField(cmbPINField.Text)) = m_lotNum
          feature.Store()
          m_lotNum += Integer.Parse(txtlotinc.Text)
          feature = featCursor.NextFeature()
        Loop
    Next i
    m_editor.StopOperation("ViperPIN")
    txtlot.Text = m_lotNum.ToString()
  End Sub
  End Class
End Namespace
