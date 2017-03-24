Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

	''' <summary>
	''' Summary description for SimplePointCursor.
	''' </summary>
<ComVisible(False)> _
Friend Class SimplePointCursor
  Implements IPlugInCursorHelper
#Region "Class members"
    Private m_bIsFinished As Boolean = False
    Private m_iInterate As Integer = -1

    Private m_sbuffer As String
    Private m_pStreamReader As System.IO.StreamReader
    Private m_iOID As Integer = -1

    Private m_fieldMap As System.Array
    Private m_fields As IFields
    Private m_searchEnv As IEnvelope
    Private m_wkGeom As IGeometry
    Private m_workPts As IPoint()
#End Region

#Region "HRESULTs definitions"
  Private Const E_FAIL As Long = (CInt(&H80004005))
  Private Const S_FALSE As Long = 1
#End Region

#Region "class members"
    Private m_bM, m_bZ As Boolean
#End Region

#Region "Class constructor"
  Public Sub New(ByVal filePath As String, ByVal fields As IFields, ByVal OID As Integer, ByVal fieldMap As System.Array, ByVal queryEnv As IEnvelope, ByVal geomType As esriGeometryType)
    'HIGHLIGHT: 0 - Set up cursor
    m_bIsFinished = False
    m_pStreamReader = New System.IO.StreamReader(filePath)
    m_fields = fields
    m_iOID = OID
    m_fieldMap = fieldMap
    m_searchEnv = queryEnv
    Select Case geomType
      Case esriGeometryType.esriGeometryPolygon
        m_wkGeom = TryCast(New PolygonClass(), IGeometry)
        m_workPts = New PointClass(4) {}
        Dim i As Integer = 0
        Do While i < m_workPts.Length
          m_workPts(i) = New PointClass()
          i += 1
        Loop
      Case esriGeometryType.esriGeometryPolyline
        m_wkGeom = TryCast(New PolylineClass(), IGeometry)
        m_workPts = New PointClass(4) {}
        Dim i As Integer = 0
        Do While i < m_workPts.Length
          m_workPts(i) = New PointClass()
          i += 1
        Loop

      Case esriGeometryType.esriGeometryPoint
        Dim point As IPoint = New Point
        m_wkGeom = TryCast(point, IGeometry)
      Case Else 'doesn't need to set worker geometry if it is table
    End Select

    'advance cursor so data is readily available
    Me.NextRecord()
  End Sub

#End Region

#Region "IPlugInCursorHelper Members"

#Region "Queries..." 'HIGHLIGHT: 2 - Query & read data
  Public Function QueryValues(ByVal Row As IRowBuffer) As Integer Implements IPlugInCursorHelper.QueryValues
    Try
      If m_sbuffer Is Nothing Then
        Return -1
      End If

      Dim i As Integer = 0
      Do While i < m_fieldMap.GetLength(0)
        'HIGHLIGHT: 2.2 QueryValues - field map interpretation
        If m_fieldMap.GetValue(i).Equals(-1) Then
          i += 1
          Continue Do
        End If

        Dim valField As IField = m_fields.Field(i)
        Dim parse As Char = m_sbuffer.Chars(m_sbuffer.Length - 1)
        Select Case valField.Type
          Case esriFieldType.esriFieldTypeInteger, esriFieldType.esriFieldTypeDouble, esriFieldType.esriFieldTypeSmallInteger, esriFieldType.esriFieldTypeSingle
            Row.Value(i) = Convert.ToInt32(parse) 'get ascii code # for the character
          Case esriFieldType.esriFieldTypeString
            Row.Value(i) = parse.ToString()
        End Select
        i += 1
      Loop
      Return m_iInterate 'HIGHLIGHT: 2.3 QueryValues - return OID
    Catch ex As Exception
      System.Diagnostics.Debug.WriteLine(ex.Message)
      Return -1
    End Try

  End Function

  Public Sub QueryShape(ByVal pGeometry As IGeometry) Implements IPlugInCursorHelper.QueryShape
    If pGeometry Is Nothing Then
      Return
    End If

    Try
      Dim x, y As Double
      x = Convert.ToDouble(m_sbuffer.Substring(0, 6))
      y = Convert.ToDouble(m_sbuffer.Substring(6, 6))

      '				#Region "set M and Z aware"
      If m_bZ Then
        CType(pGeometry, IZAware).ZAware = True
      End If
      If m_bM Then
        CType(pGeometry, IMAware).MAware = True
      End If
      '				#End Region

      'HIGHLIGHT: 2.1 QueryShape - (advanced) geometry construction
      If TypeOf pGeometry Is IPoint Then
        CType(pGeometry, IPoint).PutCoords(x, y)
        If m_bM Then
          CType(pGeometry, IPoint).M = m_iInterate
        End If
        If m_bZ Then
          CType(pGeometry, IPoint).Z = m_iInterate * 100
        End If
      ElseIf TypeOf pGeometry Is IPolyline Then
        buildPolyline(CType(pGeometry, IPointCollection), x, y)
      ElseIf TypeOf pGeometry Is IPolygon Then
        buildPolygon(CType(pGeometry, IPointCollection), x, y)
      Else
        pGeometry.SetEmpty()
      End If
    Catch ex As Exception
      System.Diagnostics.Debug.WriteLine(" Error: " & ex.Message)
      pGeometry.SetEmpty()
    End Try
  End Sub

#End Region

#Region "Next..." 'HIGHLIGHT: 1 - Looping mechanism
  Public Function IsFinished() As Boolean Implements IPlugInCursorHelper.IsFinished
    Return m_bIsFinished
  End Function

  Public Sub NextRecord() Implements IPlugInCursorHelper.NextRecord
    If m_bIsFinished Then 'error already thrown once
      Return
    End If

    'OID search has been performed
    If m_iOID > -1 AndAlso Not m_sbuffer Is Nothing Then
      m_pStreamReader.Close()
      m_bIsFinished = True

      Throw New COMException("End of SimplePoint Plugin cursor", E_FAIL)
    Else
      'HIGHLIGHT: 1.1 Next - Read the file for text
      m_sbuffer = ReadFile(m_pStreamReader, m_iOID)
      If m_sbuffer Is Nothing Then
        'finish reading, close the stream reader so resources will be released
        m_pStreamReader.Close()
        m_bIsFinished = True

        'HIGHLIGHT: 1.2 Next - Raise E_FAIL to notify end of cursor
        Throw New COMException("End of SimplePoint Plugin cursor", E_FAIL)
        'HIGHLIGHT: 1.3 Next - Search by envelope; or return all records and let post-filtering do 
        'the work for you (performance overhead)
      ElseIf Not m_searchEnv Is Nothing AndAlso Not (m_searchEnv.IsEmpty) Then
        Me.QueryShape(m_wkGeom)
        Dim pRelOp As IRelationalOperator = CType(m_wkGeom, IRelationalOperator)
        If (Not pRelOp.Disjoint(CType(m_searchEnv, IGeometry))) Then
          Return 'HIGHLIGHT: 1.4 Next - valid record within search geometry - stop advancing
        Else
          Me.NextRecord()
        End If
      End If
    End If

  End Sub
#End Region

#End Region

#Region "Geometry construction"

  Public WriteOnly Property HasM() As Boolean
    Set(ByVal value As Boolean)
      m_bM = value
    End Set
  End Property

  Public WriteOnly Property HasZ() As Boolean
    Set(ByVal value As Boolean)
      m_bZ = value
    End Set
  End Property

  Private Sub buildPolygon(ByVal pGonColl As IPointCollection, ByVal x As Double, ByVal y As Double)
    m_workPts(0).PutCoords(x - 500, y - 500)
    m_workPts(1).PutCoords(x + 500, y - 500)
    m_workPts(2).PutCoords(x + 500, y + 500)
    m_workPts(3).PutCoords(x - 500, y + 500)
    m_workPts(4).PutCoords(x - 500, y - 500)
    Try
      Dim add As Boolean = (pGonColl.PointCount = 0)
      Dim missingVal As Object = System.Reflection.Missing.Value

      Dim i As Integer = 0
      Do While i < m_workPts.Length
        CType(m_workPts(i), IZAware).ZAware = m_bZ
        CType(m_workPts(i), IMAware).MAware = m_bM

        If m_bM Then
          m_workPts(i).M = i Mod 4
        End If
        If m_bZ Then
          m_workPts(i).Z = (i Mod 4) * 100 'match start and end points
        End If

        If add Then
          pGonColl.AddPoint(m_workPts(i), missingVal, missingVal) 'The Add method only accepts either a before index or an after index.
        Else
          pGonColl.UpdatePoint(i, m_workPts(i))
        End If
        i += 1
      Loop

    Catch Ex As Exception
      System.Diagnostics.Debug.WriteLine(Ex.Message)
    End Try
    'Attempted to store an element of the incorrect type into the array.
  End Sub

  Private Sub buildPolyline(ByVal pGonColl As IPointCollection, ByVal x As Double, ByVal y As Double)
    m_workPts(0).PutCoords(x - 500, y - 500)
    m_workPts(1).PutCoords(x + 500, y - 500)
    m_workPts(2).PutCoords(x + 500, y + 500)
    m_workPts(3).PutCoords(x - 500, y + 500)
    m_workPts(4).PutCoords(x, y)

    Try
      Dim add As Boolean = (pGonColl.PointCount = 0)

      Dim missingVal As Object = System.Reflection.Missing.Value
      Dim i As Integer = 0
      Do While i < m_workPts.Length
        CType(m_workPts(i), IZAware).ZAware = m_bZ
        CType(m_workPts(i), IMAware).MAware = m_bM

        If m_bM Then
          m_workPts(i).M = i
        End If
        If m_bZ Then
          m_workPts(i).Z = i * 100
        End If
        'add it point by point - .Net IDL limitation to do batch update?
        If add Then 'pGonColl.AddPoints(5, ref m_workPts[0]);//strange error of type mismatch
          pGonColl.AddPoint(m_workPts(i), missingVal, missingVal) 'The Add method only accepts either a before index or an after index.
        Else
          pGonColl.UpdatePoint(i, m_workPts(i))
        End If
        i += 1
      Loop

      'Can I user replace point collection or addPointcollection?

    Catch Ex As Exception
      System.Diagnostics.Debug.WriteLine(Ex.Message)
    End Try
    'Attempted to store an element of the incorrect type into the array.
  End Sub

#End Region

#Region "private methods"
  Private Function ReadFile(ByVal sr As System.IO.StreamReader, ByVal lineNumber As Integer) As String
    m_iInterate += 1
    Dim buffer As String = sr.ReadLine()

    If buffer Is Nothing Then
      Return Nothing
    End If

    If lineNumber > -1 AndAlso lineNumber <> m_iInterate Then
      buffer = ReadFile(sr, lineNumber)
    End If
    'System.Diagnostics.Debug.WriteLine(buffer);
    Return buffer
  End Function
#End Region
End Class
