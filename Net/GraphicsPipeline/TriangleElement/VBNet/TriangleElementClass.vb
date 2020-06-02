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
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem

<ComVisible(True), Guid("DC8482C9-5DD6-44dc-BF3C-54B18AB813C9")> _
  Public Interface ITriangleElement
  Property FillSymbol() As ISimpleFillSymbol
  Property Size() As Double
  Property Angle() As Double
End Interface

<Guid(TriangleElementClass.CLASSGUID), ClassInterface(ClassInterfaceType.None), ProgId("TriangleElement.TriangleElementClass")> _
Public NotInheritable Class TriangleElementClass
  Implements ITriangleElement, _
             IElement, _
             IElementProperties, _
             IElementProperties2, _
             IElementProperties3, _
             IBoundsProperties, _
             ITransform2D, _
             IGraphicElement, _
             IPersistVariant, _
             IClone, _
             IDocumentVersionSupportGEN

#Region "class members"

  'some win32 imports and constants
  <System.Runtime.InteropServices.DllImport("gdi32", EntryPoint:="GetDeviceCaps", ExactSpelling:=True, CharSet:=System.Runtime.InteropServices.CharSet.Ansi, SetLastError:=True)> _
  Public Shared Function GetDeviceCaps(ByVal hDC As Integer, ByVal nIndex As Integer) As Integer
  End Function
  Private Const c_Cosine30 As Double = 0.866025403784439
  Private Const c_Deg2Rad As Double = (Math.PI / 180.0)
  Private Const c_Rad2Deg As Double = (180.0 / Math.PI)
  Private Const c_Version As Integer = 2
  Public Const CLASSGUID As String = "cbf943e2-ce6d-49f4-a4a7-ce16f02379ad"
  Public Const LOGPIXELSX As Integer = 88
  Public Const LOGPIXELSY As Integer = 90

  Private m_triangle As IPolygon = Nothing
  Private m_pointGeometry As IPoint = Nothing
  Private m_fillSymbol As ISimpleFillSymbol = Nothing
  Private m_rotation As Double = 0.0
  Private m_size As Double = 20.0
  Private m_selectionTracker As ISelectionTracker = Nothing
  Private m_cachedDisplay As IDisplay = Nothing
  Private m_nativeSR As ISpatialReference = Nothing
  Private m_elementName As String = String.Empty
  Private m_elementType As String = "TriangleElement"
  Private m_customProperty As Object = Nothing
  Private m_autoTrans As Boolean = True
  Private m_scaleRef As Double = 0.0
  Private m_anchorPointType As esriAnchorPointEnum = esriAnchorPointEnum.esriCenterPoint
  Private m_dDeviceRatio As Double = 0
#End Region

#Region "class constructor"
  Public Sub New()
    'initialize the element's geometry
    m_triangle = New PolygonClass()
    m_triangle.SetEmpty()

    InitMembers()
  End Sub
#End Region

#Region "ITriangleElement Members"

  Public Property FillSymbol() As ISimpleFillSymbol Implements ITriangleElement.FillSymbol
    Get
      Return m_fillSymbol
    End Get
    Set(ByVal value As ISimpleFillSymbol)
      m_fillSymbol = value
    End Set
  End Property

  Public Property Size() As Double Implements ITriangleElement.Size
    Get
      Return m_size
    End Get
    Set(ByVal value As Double)
      m_size = value
    End Set
  End Property

  Public Property Angle() As Double Implements ITriangleElement.Angle
    Get
      Return m_rotation
    End Get
    Set(ByVal value As Double)
      m_rotation = value
    End Set
  End Property

#End Region

#Region "IElement Members"

  Public Sub Activate(ByVal Display As IDisplay) Implements IElement.Activate
    'cache the display
    m_cachedDisplay = Display

    SetupDeviceRatio(Display.hDC, Display)

    'need to calculate the points of the triangle polygon
    If m_triangle.IsEmpty Then
      BuildTriangleGeometry(m_pointGeometry)
    End If

    'need to refresh the element's tracker
    RefreshTracker()
  End Sub

  Public Sub Deactivate() Implements IElement.Deactivate
    m_cachedDisplay = Nothing
  End Sub

  Public Sub Draw(ByVal Display As IDisplay, ByVal TrackCancel As ITrackCancel) Implements IElement.Draw
    If Not Nothing Is m_triangle AndAlso Not Nothing Is m_fillSymbol Then
      Display.SetSymbol(CType(m_fillSymbol, ISymbol))
      Display.DrawPolygon(m_triangle)
    End If
  End Sub

  Public Property Geometry() As IGeometry Implements IElement.Geometry
    Get
      Return TryCast(Clone(m_pointGeometry), IGeometry)
    End Get
    Set(ByVal value As IGeometry)
      Try
        m_pointGeometry = TryCast(Clone(value), IPoint)

        UpdateElementSpatialRef()
      Catch ex As Exception
        System.Diagnostics.Trace.WriteLine(ex.Message)
      End Try
    End Set
  End Property

  Public Function HitTest(ByVal x As Double, ByVal y As Double, ByVal Tolerance As Double) As Boolean Implements IElement.HitTest
    If Nothing Is m_cachedDisplay Then
      Return False
    End If

    Dim point As IPoint = New PointClass()
    point.PutCoords(x, y)

    Return (CType(m_triangle, IRelationalOperator)).Contains(CType(point, IGeometry))
  End Function

  Public Property Locked() As Boolean Implements IElement.Locked
    Get
      Return False
    End Get
    Set(ByVal value As Boolean)

    End Set
  End Property

  Public Sub QueryBounds(ByVal Display As IDisplay, ByVal Bounds As IEnvelope) Implements IElement.QueryBounds
    'return a bounding envelope
    Dim polygon As IPolygon = New PolygonClass()
    polygon.SetEmpty()

    CType(m_fillSymbol, ISymbol).QueryBoundary(Display.hDC, Display.DisplayTransformation, m_triangle, polygon)

    Bounds.XMin = polygon.Envelope.XMin
    Bounds.XMax = polygon.Envelope.XMax
    Bounds.YMin = polygon.Envelope.YMin
    Bounds.YMax = polygon.Envelope.YMax
    Bounds.SpatialReference = polygon.Envelope.SpatialReference
  End Sub

  Public Sub QueryOutline(ByVal Display As IDisplay, ByVal Outline As IPolygon) Implements IElement.QueryOutline
    'return a polygon which is the outline of the element
    Dim polygon As IPolygon = New PolygonClass()
    polygon.SetEmpty()
    CType(m_fillSymbol, ISymbol).QueryBoundary(Display.hDC, Display.DisplayTransformation, m_triangle, polygon)
    CType(Outline, IPointCollection).AddPointCollection(CType(polygon, IPointCollection))
  End Sub

  Public ReadOnly Property SelectionTracker() As ISelectionTracker Implements IElement.SelectionTracker
    Get
      Return m_selectionTracker
    End Get
  End Property

#End Region

#Region "IElementProperties Members"

  ''' <summary>
  ''' Indicates if transform is applied to symbols and other parts of element.
  ''' False = only apply transform to geometry.
  ''' Update font size in ITransform2D routines
  ''' </summary>
  Public Property AutoTransform() As Boolean Implements IElementProperties.AutoTransform, IElementProperties2.AutoTransform, IElementProperties3.AutoTransform
    Get
      Return m_autoTrans
    End Get
    Set(ByVal value As Boolean)
      m_autoTrans = value
    End Set
  End Property

  Public Property CustomProperty() As Object Implements IElementProperties.CustomProperty, IElementProperties2.CustomProperty, IElementProperties3.CustomProperty
    Get
      Return m_customProperty
    End Get
    Set(ByVal value As Object)
      m_customProperty = value
    End Set
  End Property

  Public Property Name() As String Implements IElementProperties.Name, IElementProperties2.Name, IElementProperties3.Name
    Get
      Return m_elementName
    End Get
    Set(ByVal value As String)
      m_elementName = value
    End Set
  End Property

  Public Property Type() As String Implements IElementProperties.Type, IElementProperties2.Type, IElementProperties3.Type
    Get
      Return m_elementType
    End Get
    Set(ByVal value As String)
      m_elementType = value
    End Set
  End Property

#End Region

#Region "IElementProperties2 Members"


  Public Function CanRotate() As Boolean Implements IElementProperties2.CanRotate, IElementProperties3.CanRotate
    Return True
  End Function

  Public Property ReferenceScale() As Double Implements IElementProperties2.ReferenceScale, IElementProperties3.ReferenceScale
    Get
      Return m_scaleRef
    End Get
    Set(ByVal value As Double)
      m_scaleRef = value
    End Set
  End Property

#End Region

#Region "IElementProperties3 Members"

  Public Property AnchorPoint() As esriAnchorPointEnum Implements IElementProperties3.AnchorPoint
    Get
      Return m_anchorPointType
    End Get
    Set(ByVal value As esriAnchorPointEnum)
      m_anchorPointType = value
    End Set
  End Property

#End Region

#Region "IBoundsProperties Members"

  Public Property FixedAspectRatio() As Boolean Implements IBoundsProperties.FixedAspectRatio
    Get
      Return True
    End Get
    Set(ByVal value As Boolean)
      Throw New Exception("The method or operation is not implemented.")
    End Set
  End Property

  Public ReadOnly Property FixedSize() As Boolean Implements IBoundsProperties.FixedSize
    Get
      Return True
    End Get
  End Property

#End Region

#Region "ITransform2D Members"

  Public Sub Move(ByVal dx As Double, ByVal dy As Double) Implements ITransform2D.Move
    If Nothing Is m_triangle Then
      Return
    End If

    CType(m_triangle, ITransform2D).Move(dx, dy)
    CType(m_pointGeometry, ITransform2D).Move(dx, dy)

    RefreshTracker()
  End Sub

  Public Sub MoveVector(ByVal v As ILine) Implements ITransform2D.MoveVector
    If Nothing Is m_triangle Then
      Return
    End If

    CType(m_triangle, ITransform2D).MoveVector(v)
    CType(m_pointGeometry, ITransform2D).MoveVector(v)

    RefreshTracker()
  End Sub

  Public Sub Rotate(ByVal Origin As IPoint, ByVal rotationAngle As Double) Implements ITransform2D.Rotate
    If Nothing Is m_triangle Then
      Return
    End If

    CType(m_triangle, ITransform2D).Rotate(Origin, rotationAngle)
    CType(m_pointGeometry, ITransform2D).Rotate(Origin, rotationAngle)

    m_rotation = rotationAngle * c_Rad2Deg

    RefreshTracker()
  End Sub

  Public Sub Scale(ByVal Origin As IPoint, ByVal sx As Double, ByVal sy As Double) Implements ITransform2D.Scale
    If Nothing Is m_triangle Then
      Return
    End If

    CType(m_triangle, ITransform2D).Scale(Origin, sx, sy)
    CType(m_pointGeometry, ITransform2D).Scale(Origin, sx, sy)

    If m_autoTrans Then
      m_size *= Math.Max(sx, sy)
    End If

    RefreshTracker()
  End Sub

  Public Sub Transform(ByVal direction As esriTransformDirection, ByVal transformation As ITransformation) Implements ITransform2D.Transform
    If Nothing Is m_triangle Then
      Return
    End If

    'Geometry
    CType(m_triangle, ITransform2D).Transform(direction, transformation)

    Dim affineTrans As IAffineTransformation2D = CType(transformation, IAffineTransformation2D)
    If affineTrans.YScale <> 1.0 Then
      m_size *= Math.Max(affineTrans.YScale, affineTrans.XScale)
    End If

    RefreshTracker()
  End Sub

#End Region

#Region "IGraphicElement Members"

  Public Property SpatialReference() As ISpatialReference Implements IElementProperties3.SpatialReference, IGraphicElement.SpatialReference
    Get
      Return m_nativeSR
    End Get
    Set(ByVal value As ISpatialReference)
      m_nativeSR = value
      UpdateElementSpatialRef()
    End Set
  End Property

#End Region

#Region "IPersistVariant Members"

  Public ReadOnly Property ID() As UID Implements IPersistVariant.ID
    Get
      Dim uid As UID = New UIDClass()
      uid.Value = "{" & TriangleElementClass.CLASSGUID & "}"
      Return uid
    End Get
  End Property

  Public Sub Load(ByVal Stream As IVariantStream) Implements IPersistVariant.Load
    Dim ver As Integer = CInt(Fix(Stream.Read()))
    If ver > c_Version OrElse ver <= 0 Then
      Throw New Exception("Wrong version!")
    End If

    InitMembers()

    m_size = CDbl(Stream.Read())
    m_scaleRef = CDbl(Stream.Read())
    m_anchorPointType = CType(Stream.Read(), esriAnchorPointEnum)
    m_autoTrans = CBool(Stream.Read())
    m_elementType = CStr(Stream.Read())
    m_elementName = CStr(Stream.Read())
    m_nativeSR = TryCast(Stream.Read(), ISpatialReference)
    m_fillSymbol = TryCast(Stream.Read(), ISimpleFillSymbol)
    m_pointGeometry = TryCast(Stream.Read(), IPoint)
    m_triangle = TryCast(Stream.Read(), IPolygon)

    If ver = 2 Then
      m_rotation = CDbl(Stream.Read())
    End If
  End Sub

  Public Sub Save(ByVal Stream As IVariantStream) Implements IPersistVariant.Save
    Stream.Write(c_Version)

    Stream.Write(m_size)
    Stream.Write(m_scaleRef)
    Stream.Write(m_anchorPointType)
    Stream.Write(m_autoTrans)
    Stream.Write(m_elementType)
    Stream.Write(m_elementName)
    Stream.Write(m_nativeSR)
    Stream.Write(m_fillSymbol)
    Stream.Write(m_pointGeometry)
    Stream.Write(m_triangle)

    Stream.Write(m_rotation)
  End Sub

#End Region

#Region "IClone Members"

  Public Sub Assign(ByVal src As IClone) Implements IClone.Assign

    '1. make sure that src is pointing to a valid object
    If Nothing Is src Then
            Throw New COMException("Invalid object.")
    End If

    '2. make sure that the type of src is of type 'TriangleElementClass'
    If Not (TypeOf src Is TriangleElementClass) Then
      Throw New COMException("Bad object type.")
    End If

    '3. assign the properties of src to the current instance
    Dim srcTriangle As TriangleElementClass = CType(src, TriangleElementClass)
    m_elementName = srcTriangle.Name
    m_elementType = srcTriangle.Type
    m_autoTrans = srcTriangle.AutoTransform
    m_scaleRef = srcTriangle.ReferenceScale
    m_rotation = srcTriangle.Angle
    m_size = srcTriangle.Size
    m_anchorPointType = srcTriangle.AnchorPoint

    Dim objCopy As IObjectCopy = New ObjectCopyClass()

    'take care of the custom property
    If Not Nothing Is srcTriangle.CustomProperty Then
      If TypeOf srcTriangle.CustomProperty Is IClone Then
        m_customProperty = CObj((CType(srcTriangle.CustomProperty, IClone)).Clone())
      ElseIf TypeOf srcTriangle.CustomProperty Is IPersistStream Then
        m_customProperty = objCopy.Copy(CObj(srcTriangle.CustomProperty))
      ElseIf srcTriangle.CustomProperty.GetType().IsSerializable Then
        'serialize to a memory stream
        Dim memoryStream As MemoryStream = New MemoryStream()
        Dim binaryFormatter As BinaryFormatter = New BinaryFormatter()
        binaryFormatter.Serialize(memoryStream, srcTriangle.CustomProperty)
        Dim bytes As Byte() = memoryStream.ToArray()

        memoryStream = New MemoryStream(bytes)
        m_customProperty = binaryFormatter.Deserialize(memoryStream)
      End If
    End If

    If Not Nothing Is srcTriangle.SpatialReference Then
      m_nativeSR = TryCast(objCopy.Copy(srcTriangle.SpatialReference), ISpatialReference)
    Else
      m_nativeSR = Nothing
    End If

    If Not Nothing Is srcTriangle.FillSymbol Then
      m_fillSymbol = TryCast(objCopy.Copy(srcTriangle.FillSymbol), ISimpleFillSymbol)
    Else
      m_fillSymbol = Nothing
    End If

    If Not Nothing Is srcTriangle.Geometry Then
      m_triangle = TryCast(objCopy.Copy(srcTriangle.Geometry), IPolygon)
      m_pointGeometry = TryCast(objCopy.Copy((CType(m_triangle, IArea)).Centroid), IPoint)
    Else
      m_triangle = Nothing
      m_pointGeometry = Nothing
    End If
  End Sub

  Public Function Clone() As IClone Implements IClone.Clone
    Dim triangle As TriangleElementClass = New TriangleElementClass()
    triangle.Assign(CType(Me, IClone))

    Return CType(triangle, IClone)
  End Function

  Public Function IsEqual(ByVal other As IClone) As Boolean Implements IClone.IsEqual
    '1. make sure that the 'other' object is pointing to a valid object
    If Nothing Is other Then
            Throw New COMException("Invalid object.")
    End If

    '2. verify the type of 'other'
    If Not (TypeOf other Is TriangleElementClass) Then
      Throw New COMException("Bad object type.")
    End If

    Dim otherTriangle As TriangleElementClass = CType(other, TriangleElementClass)
    'test that all ot the object's properties are the same.
        'please note the usage of IsEqual when using ArcObjects components that
    'supports cloning
    If otherTriangle.Name = m_elementName AndAlso otherTriangle.Type = m_elementType AndAlso otherTriangle.AutoTransform = m_autoTrans AndAlso otherTriangle.ReferenceScale = m_scaleRef AndAlso otherTriangle.Angle = m_rotation AndAlso otherTriangle.Size = m_size AndAlso otherTriangle.AnchorPoint = m_anchorPointType AndAlso (CType(otherTriangle.Geometry, IClone)).IsEqual(CType(m_triangle, IClone)) AndAlso (CType(otherTriangle.FillSymbol, IClone)).IsEqual(CType(m_fillSymbol, IClone)) AndAlso (CType(otherTriangle.SpatialReference, IClone)).IsEqual(CType(m_nativeSR, IClone)) Then
      Return True
    End If

    Return False
  End Function

  Public Function IsIdentical(ByVal other As IClone) As Boolean Implements IClone.IsIdentical
    '1. make sure that the 'other' object is pointing to a valid object
    If Nothing Is other Then
            Throw New COMException("Invalid object.")
    End If

    '2. verify the type of 'other'
    If Not (TypeOf other Is TriangleElementClass) Then
      Throw New COMException("Bad object type.")
    End If

    '3. test if the other is the 'this'
    If CType(other, TriangleElementClass) Is Me Then
      Return True
    End If

    Return False
  End Function

#End Region

#Region "IDocumentVersionSupportGEN Members"

  Public Function ConvertToSupportedObject(ByVal docVersion As esriArcGISVersion) As Object Implements IDocumentVersionSupportGEN.ConvertToSupportedObject
    'in case of 8.3, create a character marker element and use a triangle marker...
    Dim charMarkerSymbol As ICharacterMarkerSymbol = New CharacterMarkerSymbolClass()
    charMarkerSymbol.Color = m_fillSymbol.Color
    charMarkerSymbol.Angle = m_rotation
    charMarkerSymbol.Size = m_size
        charMarkerSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(New Font("ESRI Default Marker", CSng(m_size), FontStyle.Regular))
    charMarkerSymbol.CharacterIndex = 184

    Dim markerElement As IMarkerElement = New MarkerElementClass()
    markerElement.Symbol = CType(charMarkerSymbol, IMarkerSymbol)

    Dim point As IPoint = TryCast((CType(m_pointGeometry, IClone)).Clone(), IPoint)
    Dim element As IElement = CType(markerElement, IElement)
    element.Geometry = CType(point, IGeometry)

    Return element
  End Function

  Public Function IsSupportedAtVersion(ByVal docVersion As esriArcGISVersion) As Boolean Implements IDocumentVersionSupportGEN.IsSupportedAtVersion
    'support all versions except 8.3
    If esriArcGISVersion.esriArcGISVersion83 = docVersion Then
      Return False
    Else
      Return True
    End If
  End Function

#End Region

#Region "private methods"
  Private Function Clone(ByVal obj As Object) As IClone
    If Nothing Is obj OrElse Not (TypeOf obj Is IClone) Then
      Return Nothing
    End If

    Return (CType(obj, IClone)).Clone()
  End Function

  Private Function TwipsPerPixelX() As Integer
    Return 16
  End Function

  Private Function TwipsPerPixelY() As Integer
    Return 16
  End Function

  Private Sub SetupDeviceRatio(ByVal hDC As Integer, ByVal display As ESRI.ArcGIS.Display.IDisplay)
    If Not display.DisplayTransformation Is Nothing Then
      If display.DisplayTransformation.Resolution <> 0 Then
        m_dDeviceRatio = display.DisplayTransformation.Resolution / 72
        '  Check the ReferenceScale of the display transformation. If not zero, we need to
        '  adjust the Size, XOffset and YOffset of the Symbol we hold internally before drawing.
        If display.DisplayTransformation.ReferenceScale <> 0 Then
          m_dDeviceRatio = m_dDeviceRatio * display.DisplayTransformation.ReferenceScale / display.DisplayTransformation.ScaleRatio
        End If
      End If
    Else
            ' If we don't have a display transformation, calculate the resolution
      ' from the actual device.
      If display.hDC <> 0 Then
        ' Get the resolution from the device context hDC.
        m_dDeviceRatio = System.Convert.ToDouble(GetDeviceCaps(hDC, LOGPIXELSX)) / 72
      Else
        ' If invalid hDC assume we're drawing to the screen.
        m_dDeviceRatio = 1 / (TwipsPerPixelX() / 20) ' 1 Point = 20 Twips.
      End If
    End If
  End Sub

  Private Function PointsToMap(ByVal displayTransform As IDisplayTransformation, ByVal dPointSize As Double) As Double
    Dim tempPointsToMap As Double = 0
    If displayTransform Is Nothing Then
      tempPointsToMap = dPointSize * m_dDeviceRatio
    Else
      tempPointsToMap = displayTransform.FromPoints(dPointSize)
    End If
    Return tempPointsToMap
  End Function

  Private Sub BuildTriangleGeometry(ByVal pointGeometry As IPoint)
    Try
      If Nothing Is m_triangle OrElse Nothing Is pointGeometry OrElse Nothing Is m_cachedDisplay Then
        Return
      End If

      m_triangle.SpatialReference = pointGeometry.SpatialReference
      m_triangle.SetEmpty()

      Dim missing As Object = System.Reflection.Missing.Value
      Dim pointCollection As IPointCollection = CType(m_triangle, IPointCollection)

      Dim radius As Double = PointsToMap(m_cachedDisplay.DisplayTransformation, m_size)

      Dim X As Double = pointGeometry.X
      Dim Y As Double = pointGeometry.Y

      Dim point As IPoint = New PointClass()
      point.X = X + radius * c_Cosine30
      point.Y = Y - 0.5 * radius
      pointCollection.AddPoint(point, missing, missing)

      point = New PointClass()
      point.X = X
      point.Y = Y + radius
      pointCollection.AddPoint(point, missing, missing)

      point = New PointClass()
      point.X = X - radius * c_Cosine30
      point.Y = Y - 0.5 * radius
      pointCollection.AddPoint(point, missing, missing)

      m_triangle.Close()

      If m_rotation <> 0.0 Then
        CType(pointCollection, ITransform2D).Rotate(pointGeometry, m_rotation * c_Deg2Rad)
      End If

      Return
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
    End Try
  End Sub

  Private Sub SetDefaultDymbol()
        Dim c As IColor = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Black), IColor)
    Dim lineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
    lineSymbol.Style = esriSimpleLineStyle.esriSLSSolid
    lineSymbol.Width = 1.0
    lineSymbol.Color = c

        c = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Navy), IColor)
    If Nothing Is m_fillSymbol Then
      m_fillSymbol = New SimpleFillSymbolClass()
    End If
    m_fillSymbol.Color = c
    m_fillSymbol.Style = esriSimpleFillStyle.esriSFSSolid
    m_fillSymbol.Outline = CType(lineSymbol, ILineSymbol)
  End Sub


  ''' <summary>
  ''' assign the triangle's geometry to the selection tracker
  ''' </summary>
  Private Sub RefreshTracker()
    If Nothing Is m_cachedDisplay Then
      Return
    End If

    m_selectionTracker.Display = CType(m_cachedDisplay, IScreenDisplay)


    Dim outline As IPolygon = New PolygonClass()
    Me.QueryOutline(m_cachedDisplay, outline)

    m_selectionTracker.Geometry = CType(outline, IGeometry)
  End Sub

  Private Sub UpdateElementSpatialRef()
    If Nothing Is m_cachedDisplay OrElse Nothing Is m_nativeSR OrElse Nothing Is m_triangle OrElse Nothing Is m_cachedDisplay.DisplayTransformation.SpatialReference Then
      Return
    End If

    If Nothing Is m_triangle.SpatialReference Then
      m_triangle.SpatialReference = m_cachedDisplay.DisplayTransformation.SpatialReference
    End If

    m_triangle.Project(m_nativeSR)

    RefreshTracker()
  End Sub

  Private Sub InitMembers()
    'initialize the selection tracker
    m_selectionTracker = New PolygonTrackerClass()
    m_selectionTracker.Locked = False
    m_selectionTracker.ShowHandles = True

    'set a default symbol
    SetDefaultDymbol()
  End Sub
#End Region

End Class
