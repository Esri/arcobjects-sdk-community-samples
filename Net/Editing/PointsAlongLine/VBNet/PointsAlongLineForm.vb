Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

Namespace PointsAlongLine
  Partial Public Class PointsAlongLineForm
	  Inherits Form
  Private m_editor As IEditor3
  Private m_edSketch As IEditSketch3
  Private m_polyline As IPolyline

  Private m_editTemplate As IEditTemplate
  Private m_featureLayer As IFeatureLayer
  Private m_featureClass As IFeatureClass

  Public Sub New(ByVal editor As IEditor3)
    InitializeComponent()

    m_editor = editor
    m_edSketch = TryCast(m_editor, IEditSketch3)

    m_polyline = TryCast(m_edSketch.Geometry, IPolyline)
    tbLineLength.Text = (m_polyline.Length.ToString("F"))

    'get the template
    m_editTemplate = m_editor.CurrentTemplate
    m_featureLayer = TryCast(m_editTemplate.Layer, IFeatureLayer)
    m_featureClass = m_featureLayer.FeatureClass

  End Sub

  Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.Close()
  End Sub

  Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdOK.Click
    'calc distance between points
    Dim dbp As Double = 0
    If rbNOP.Checked Then
    dbp = m_polyline.Length / (Integer.Parse(txtNOP.Text) + 1)
    Else
    dbp = Integer.Parse(txtDist.Text)
    End If

    m_editor.StartOperation()
    Me.Cursor = Cursors.WaitCursor

    'create points at distance between points up to total length
    For d As Double = dbp To m_polyline.Length - 1 Step dbp
    Dim contructionPoint As IConstructPoint = New PointClass()
    contructionPoint.ConstructAlong(m_polyline, esriSegmentExtension.esriNoExtension, d, False)
    CreatePoint(TryCast(contructionPoint, IPoint))
    Next d

    'create points at start and end of sketch
    If chkEnds.Checked Then
    CreatePoint(m_polyline.FromPoint)
    CreatePoint(m_polyline.ToPoint)
    End If

    Me.Cursor = Cursors.Default
    m_editor.StopOperation("Create points along a line")
    Me.Close()
  End Sub

  Private Sub CreatePoint(ByVal point As IPoint)
  'create point for the current template
  Dim feature As IFeature = m_featureClass.CreateFeature()
  feature.Shape = point
  m_editTemplate.SetDefaultValues(feature)
  feature.Store()

  'Invalidate the area around the new feature
  m_editor.Display.Invalidate(feature.Extent, True, CShort(Fix(esriScreenCache.esriAllScreenCaches)))
  End Sub

  End Class
End Namespace
