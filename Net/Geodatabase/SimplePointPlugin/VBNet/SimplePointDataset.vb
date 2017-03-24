Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
''' <summary>
''' Summary description for SimplePointDataset.
''' </summary>
<ComVisible(False)> _
Friend Class SimplePointDataset
  Implements IPlugInDatasetInfo, IPlugInDatasetHelper
#Region "Class members"
  Private m_wkspString, m_datasetString As String
  Private m_bound As IEnvelope
  Private m_fullPath As String
#End Region

#Region "Class constructor"
  Public Sub New(ByVal wkspString As String, ByVal datasetString As String)
    'HIGHLIGHT: constructor checks valid workspace string path and dataset name
    m_wkspString = wkspString
    m_fullPath = System.IO.Path.Combine(wkspString, datasetString)
    If System.IO.Path.HasExtension(datasetString) Then
      m_datasetString = System.IO.Path.GetFileNameWithoutExtension(datasetString)
    Else
      m_datasetString = datasetString
      m_fullPath &= ".csp" 'add the extension
    End If
  End Sub
#End Region

#Region "IPlugInDatasetInfo Members    "
  'HIGHLIGHT: IPlugInDatasetInfo - lightweight!
  Public ReadOnly Property LocalDatasetName() As String Implements IPlugInDatasetInfo.LocalDatasetName
    Get
      Return m_datasetString
    End Get
  End Property

  Public ReadOnly Property ShapeFieldName() As String Implements IPlugInDatasetInfo.ShapeFieldName
    Get
      If Me.DatasetType = esriDatasetType.esriDTTable Then
        Return Nothing
      End If
      Return "Shape"
    End Get
  End Property

  Public ReadOnly Property DatasetType() As esriDatasetType Implements IPlugInDatasetInfo.DatasetType
    Get
      '				return esriDatasetType.esriDTTable;
      '				return esriDatasetType.esriDTFeatureClass;
      Return esriDatasetType.esriDTFeatureDataset
    End Get
  End Property

  Public ReadOnly Property GeometryType() As esriGeometryType Implements IPlugInDatasetInfo.GeometryType
    Get
      Return geometryTypeByID(-1) 'might not be always easy to get
    End Get
  End Property

#End Region

#Region "IPlugInDatasetHelper Members"

  Public ReadOnly Property Bounds() As ESRI.ArcGIS.Geometry.IEnvelope Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.Bounds
    Get
      If Me.DatasetType = esriDatasetType.esriDTTable Then
        Return Nothing
      End If
      If m_bound Is Nothing Then
        '					#Region "use cursor go through records, or we can parse the file directly"
        m_bound = New EnvelopeClass()
        m_bound.SpatialReference = Me.spatialReference


        Dim flds As IFields = Me.Fields(0)
        Dim fieldMapArray As Integer() = New Integer(flds.FieldCount - 1) {}
        Dim i As Integer = 0
        Do While i < flds.FieldCount
          fieldMapArray(i) = -1 'shape field always ignored?
          i += 1
        Loop

        Dim x1 As Double = 999999, y1 As Double = 999999, x2 As Double = 0, y2 As Double = 0 'assumes all positive value in the file

        'Set with appropriate geometry
        Dim workGeom As IGeometry
        Dim cursor As IPlugInCursorHelper
        If Me.DatasetType = esriDatasetType.esriDTFeatureDataset Then
          workGeom = New PolygonClass()
          cursor = Me.FetchAll(2, Nothing, fieldMapArray)
        Else
          workGeom = New PointClass()
          cursor = Me.FetchAll(0, Nothing, fieldMapArray)
        End If

        workGeom.SpatialReference = Me.spatialReference
        Do While True
          Try
            cursor.QueryShape(workGeom)
            If workGeom.Envelope.XMin < x1 Then
              x1 = workGeom.Envelope.XMin
            End If
            If workGeom.Envelope.XMax > x2 Then
              x2 = workGeom.Envelope.XMax
            End If

            If workGeom.Envelope.YMin < y1 Then
              y1 = workGeom.Envelope.YMin
            End If
            If workGeom.Envelope.YMax > y2 Then
              y2 = workGeom.Envelope.YMax
            End If

            cursor.NextRecord()
          Catch comEx As COMException
            System.Diagnostics.Debug.WriteLine(comEx.Message)
            Exit Do 'catch E_FAIL when cursor reaches the end, exit loop
          Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
          End Try
        Loop

        m_bound.PutCoords(x1, y1, x2, y2)

        '					#End Region
      End If

      'HIGHLIGHT: return clone envelope for bound
      Dim cloneEnv As IClone = CType(m_bound, IClone)
      Return CType(cloneEnv.Clone(), IEnvelope)
    End Get
  End Property

  Public ReadOnly Property ClassCount() As Integer Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.ClassCount
    Get
      If Me.DatasetType = esriDatasetType.esriDTFeatureDataset Then
        Return 12
      End If
      Return 1
    End Get
  End Property

  Public ReadOnly Property ClassIndex(ByVal Name As String) As Integer Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.ClassIndex
    Get
      Dim i As Integer = 0
      Do While i < Me.ClassCount
        If Name.Equals(Me.ClassName(i)) Then
          Return i
        End If
        i += 1
      Loop
      Return -1
    End Get
  End Property

  Public ReadOnly Property ClassName(ByVal Index As Integer) As String Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.ClassName
    Get
      If Index Mod 3 = 0 Then
        m_datasetString = "Point"
      End If
      If Index Mod 3 = 1 Then
        m_datasetString = "Polyline"
      End If
      If Index Mod 3 = 2 Then
        m_datasetString = "Polygon"
      End If

      If (Index >= 3 AndAlso Index < 6) OrElse Index >= 9 Then
        m_datasetString &= "M"
      End If

      If Index >= 6 Then
        m_datasetString &= "Z"
      End If

      Return m_datasetString
    End Get
  End Property

#Region "Fetching - returns cursor" 'HIGHLIGHT: Fetching
  Public Function FetchAll(ByVal ClassIndex As Integer, ByVal WhereClause As String, ByVal FieldMap As Object) As ESRI.ArcGIS.Geodatabase.IPlugInCursorHelper Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.FetchAll
    Try
      Dim allCursor As SimplePointCursor = New SimplePointCursor(m_fullPath, Me.Fields(ClassIndex), -1, CType(FieldMap, System.Array), Nothing, Me.geometryTypeByID(ClassIndex))
      setMZ(allCursor, ClassIndex)
      Return CType(allCursor, IPlugInCursorHelper)
    Catch ex As Exception
      System.Diagnostics.Debug.WriteLine(ex.Message)
      Return Nothing
    End Try
  End Function

  Public Function FetchByEnvelope(ByVal ClassIndex As Integer, ByVal env As ESRI.ArcGIS.Geometry.IEnvelope, ByVal strictSearch As Boolean, ByVal WhereClause As String, ByVal FieldMap As Object) As ESRI.ArcGIS.Geodatabase.IPlugInCursorHelper Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.FetchByEnvelope
    If Me.DatasetType = esriDatasetType.esriDTTable Then
      Return Nothing
    End If

    'env passed in always has same spatial reference as the data
        'for identify, it will check if search geometry intersect dataset bound
    'but not ITable.Search(pSpatialQueryFilter, bRecycle) etc
    'so here we should check if input env falls within extent
    Dim boundEnv As IEnvelope = Me.Bounds
    boundEnv.Project(env.SpatialReference)
    If boundEnv.IsEmpty Then
      Return Nothing 'or raise error?
    End If
    Try
      Dim spatialCursor As SimplePointCursor = New SimplePointCursor(m_fullPath, Me.Fields(ClassIndex), -1, CType(FieldMap, System.Array), env, Me.geometryTypeByID(ClassIndex))
      setMZ(spatialCursor, ClassIndex)

      Return CType(spatialCursor, IPlugInCursorHelper)
    Catch ex As Exception
      System.Diagnostics.Debug.WriteLine(ex.Message)
      Return Nothing
    End Try
  End Function

  Public Function FetchByID(ByVal ClassIndex As Integer, ByVal ID As Integer, ByVal FieldMap As Object) As ESRI.ArcGIS.Geodatabase.IPlugInCursorHelper Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.FetchByID
    Try
      Dim idCursor As SimplePointCursor = New SimplePointCursor(m_fullPath, Me.Fields(ClassIndex), ID, CType(FieldMap, System.Array), Nothing, Me.geometryTypeByID(ClassIndex))

      setMZ(idCursor, ClassIndex)
      Return CType(idCursor, IPlugInCursorHelper)
    Catch ex As Exception 'will catch NextRecord error if it reaches EOF without finding a record
      System.Diagnostics.Debug.WriteLine(ex.Message)
      Return Nothing
    End Try
  End Function
#End Region

  Public ReadOnly Property Fields(ByVal ClassIndex As Integer) As ESRI.ArcGIS.Geodatabase.IFields Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.Fields
    Get
      Dim fieldEdit As IFieldEdit
      Dim flds As IFields
      Dim fieldsEdit As IFieldsEdit
      Dim fcDesc As IObjectClassDescription
      If Me.DatasetType = esriDatasetType.esriDTTable Then
        fcDesc = New ObjectClassDescriptionClass()
      Else
        fcDesc = New FeatureClassDescriptionClass()
      End If

      flds = fcDesc.RequiredFields
      fieldsEdit = CType(flds, IFieldsEdit)

      fieldEdit = New FieldClass()
      fieldEdit.Length_2 = 1
      fieldEdit.Name_2 = "ColumnOne"
      fieldEdit.Type_2 = esriFieldType.esriFieldTypeString
      fieldsEdit.AddField(CType(fieldEdit, IField))

      'HIGHLIGHT: Add extra int column
      fieldEdit = New FieldClass()
      fieldEdit.Name_2 = "Extra"
      fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
      fieldsEdit.AddField(CType(fieldEdit, IField))

      'HIGHLIGHT: Set shape field geometry definition
      If Me.DatasetType <> esriDatasetType.esriDTTable Then
        Dim field As IField = flds.Field(flds.FindField("Shape"))
        fieldEdit = CType(field, IFieldEdit)
        Dim geomDefEdit As IGeometryDefEdit = CType(field.GeometryDef, IGeometryDefEdit)
        geomDefEdit.GeometryType_2 = geometryTypeByID(ClassIndex)
        Dim shapeSRef As ISpatialReference = Me.spatialReference

        '				#Region "M & Z"
        'M
        If (ClassIndex >= 3 AndAlso ClassIndex <= 5) OrElse ClassIndex >= 9 Then
          geomDefEdit.HasM_2 = True
          shapeSRef.SetMDomain(0, 1000)
        Else
          geomDefEdit.HasM_2 = False
        End If

        'Z
        If ClassIndex >= 6 Then
          geomDefEdit.HasZ_2 = True
          shapeSRef.SetZDomain(0, 1000)
        Else
          geomDefEdit.HasZ_2 = False
        End If
        '				#End Region

        geomDefEdit.SpatialReference_2 = shapeSRef
      End If

      Return flds
    End Get
  End Property

  Public ReadOnly Property OIDFieldIndex(ByVal ClassIndex As Integer) As Integer Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.OIDFieldIndex
    Get
      Return 0
    End Get
  End Property

  Public ReadOnly Property ShapeFieldIndex(ByVal ClassIndex As Integer) As Integer Implements ESRI.ArcGIS.Geodatabase.IPlugInDatasetHelper.ShapeFieldIndex
    Get
      'add table to ArcMap via add data dialog calls this method, so if it's a table
      'you must return -1 or else ArcMap crashes
      If Me.DatasetType = esriDatasetType.esriDTTable Then
        Return -1
      End If

      Return 1
    End Get
  End Property

#End Region

#Region "internal helper methods"
  Private Function geometryTypeByID(ByVal ClassIndex As Integer) As esriGeometryType
    If Me.DatasetType = esriDatasetType.esriDTTable Then
      Return esriGeometryType.esriGeometryNull
    End If

    If ClassIndex Mod 3 = 0 Then
      Return esriGeometryType.esriGeometryPoint
    ElseIf ClassIndex Mod 3 = 1 Then
      Return esriGeometryType.esriGeometryPolyline
    Else
      Return esriGeometryType.esriGeometryPolygon
    End If
  End Function

  Private ReadOnly Property spatialReference() As ISpatialReference
    Get
      If Me.DatasetType = esriDatasetType.esriDTTable Then
        Return Nothing
      End If

      'singleton
      Dim srefFact As ISpatialReferenceFactory2 = New SpatialReferenceEnvironmentClass()
      Return srefFact.CreateProjectedCoordinateSystem(Convert.ToInt32(esriSRProjCSType.esriSRProjCS_World_Robinson)) ' WGS1984UTM_10N));
    End Get
  End Property

  Private Sub setMZ(ByVal sptCursor As SimplePointCursor, ByVal Index As Integer)
    sptCursor.HasM = ((Index >= 3 AndAlso Index < 6) OrElse Index >= 9)
    sptCursor.HasZ = (Index >= 6)
  End Sub
#End Region
End Class
