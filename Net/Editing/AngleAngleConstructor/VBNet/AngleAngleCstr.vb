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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports System.Runtime.InteropServices
Imports System.Windows.Forms


<ComClass(AngleAngleCstr.ClassId, AngleAngleCstr.InterfaceId, AngleAngleCstr.EventsId), _
 ProgId("AngleAngleVB.AngleAngleCstr")> _
Public Class AngleAngleCstr
  Implements IShapeConstructor, IPersist

#Region "COM GUIDs"
  ' These  GUIDs provide the COM identity for this class 
  ' and its COM interfaces. If you change them, existing 
  ' clients will no longer be able to access the class.
  Public Const ClassId As String = "cdcbb1bf-a87d-4927-8e75-9babe1979f90"
  Public Const InterfaceId As String = "2bb643ed-bb80-4203-983c-16eef50d859a"
  Public Const EventsId As String = "48d4380d-2fdb-4163-94ad-a4c11124a308"
#End Region

  Dim m_editor As IEditor3
  Dim m_edSketch As IEditSketch3
  Dim m_snappingEnv As ISnappingEnvironment
  Dim m_snapper As IPointSnapper
  Dim m_snappingFeedback As ISnappingFeedback

  'Declare 3 points
  Dim m_firstPoint As IPoint
  Dim m_secondPoint As IPoint
  Dim m_activePoint As IPoint
  'Declare 2 angles
  Dim m_firstAngle As Double
  Dim m_secondAngle As Double
  Dim m_etoolPhase As ToolPhase
  Enum ToolPhase
    Inactive
    SecondPoint
    Intersection
  End Enum

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()
  End Sub

#Region "IShapeConstructor implementation"

  Public Sub Activate() Implements IShapeConstructor.Activate
  End Sub

  Public ReadOnly Property Active() As Boolean Implements IShapeConstructor.Active
    Get
      Return True
    End Get
  End Property

  Public Sub AddPoint(ByVal point As IPoint, ByVal Clone As Boolean, ByVal allowUndo As Boolean) Implements IShapeConstructor.AddPoint
  End Sub
  Public ReadOnly Property Anchor() As IPoint Implements IShapeConstructor.Anchor
    Get
      Return Nothing
    End Get
  End Property


  Public Property AngleConstraint() As Double Implements IShapeConstructor.AngleConstraint
    Get
      Return Nothing
    End Get
    Set(ByVal value As Double)
    End Set
  End Property

  Public Property Constraint() As esriSketchConstraint Implements IShapeConstructor.Constraint
    Get
      Return Nothing
    End Get
    Set(ByVal value As esriSketchConstraint)
    End Set
  End Property

  Public ReadOnly Property Cursor() As Integer Implements IShapeConstructor.Cursor
    Get
      Return 0
    End Get
  End Property

  Public Sub Deactivate() Implements IShapeConstructor.Deactivate
  End Sub

  Public Property DistanceConstraint() As Double Implements IShapeConstructor.DistanceConstraint
    Get
      Return Nothing
    End Get
    Set(ByVal value As Double)
    End Set
  End Property

  Public ReadOnly Property Enabled() As Boolean Implements IShapeConstructor.Enabled
    Get
      Return True
    End Get
  End Property

  Public ReadOnly Property ID() As String Implements IShapeConstructor.ID
    Get
      Return ""
    End Get
  End Property

  Public Sub Initialize(ByVal pEditor As IEditor) Implements IShapeConstructor.Initialize

    'Initialize the shape constructor
    m_editor = pEditor
    m_edSketch = pEditor

    'Get the snap environment
    m_snappingEnv = m_editor.Parent.FindExtensionByName("ESRI Snapping")
    m_snapper = m_snappingEnv.PointSnapper
    m_snappingFeedback = New SnappingFeedbackClass()
    m_snappingFeedback.Initialize(m_editor.Parent, m_snappingEnv, True)

    m_firstPoint = New PointClass()
    m_secondPoint = New PointClass()
    m_activePoint = New PointClass()

    'Set the phase to inactive so we start at the beginning 
    m_etoolPhase = ToolPhase.Inactive
  End Sub

  Public Property IsStreaming() As Boolean Implements IShapeConstructor.IsStreaming
    Get
      Return Nothing
    End Get
    Set(ByVal value As Boolean)
    End Set
  End Property


  Public ReadOnly Property Location() As IPoint Implements IShapeConstructor.Location
    Get
      Return Nothing
    End Get
  End Property

  Public Function OnContextMenu(ByVal X As Integer, ByVal Y As Integer) As Boolean Implements IShapeConstructor.OnContextMenu
    Return True
  End Function

  Public Sub OnKeyDown(ByVal keyState As Integer, ByVal shift As Integer) Implements IShapeConstructor.OnKeyDown
    'If the escape key is used, throw away the calculated point 
    If (keyState = Keys.Escape) Then
      m_etoolPhase = ToolPhase.Inactive
    End If
  End Sub

  Public Sub OnKeyUp(ByVal keyState As Integer, ByVal shift As Integer) Implements IShapeConstructor.OnKeyUp
  End Sub

  Public Sub OnMouseDown(ByVal Button As Integer, ByVal shift As Integer, ByVal X As Integer, ByVal Y As Integer) Implements IShapeConstructor.OnMouseDown
    If (Button <> Keys.LButton) Then
      Return
    End If

    Select Case m_etoolPhase
      Case (ToolPhase.Inactive)
        GetFirstPoint()
      Case (ToolPhase.SecondPoint)
        GetSecondPoint()
      Case (ToolPhase.Intersection)
        GetIntersection()
    End Select
  End Sub

  Public Sub OnMouseMove(ByVal Button As Integer, ByVal shift As Integer, ByVal X As Integer, ByVal Y As Integer) Implements IShapeConstructor.OnMouseMove

    'Snap the mouse location
    If (m_etoolPhase <> ToolPhase.Intersection) Then
      m_activePoint = m_editor.Display.DisplayTransformation.ToMapPoint(X, Y)
      Dim snapResult As ISnappingResult = m_snapper.Snap(m_activePoint)
      m_snappingFeedback.Update(snapResult, 0)

      If (snapResult Is Nothing) Then
        m_activePoint = snapResult.Location
      End If
    End If
  End Sub

  Public Sub OnMouseUp(ByVal Button As Integer, ByVal shift As Integer, ByVal X As Integer, ByVal Y As Integer) Implements IShapeConstructor.OnMouseUp
  End Sub

  Public Sub Refresh(ByVal hdc As Integer) Implements IShapeConstructor.Refresh
    m_snappingFeedback.Refresh(hdc)
  End Sub

  Public Sub SketchModified() Implements IShapeConstructor.SketchModified
  End Sub
#End Region

  Private Sub GetFirstPoint()
    Dim numDialog As INumberDialog = New NumberDialogClass()
    'Set first point to the active point which may have been snapped
    m_firstPoint = m_activePoint
    'Get the angle
    If (numDialog.DoModal("Angle 1", 45, 2, m_editor.Display.hWnd)) Then
      m_firstAngle = numDialog.Value * Math.PI / 180
      m_etoolPhase = ToolPhase.SecondPoint
    End If
  End Sub

  Private Sub GetSecondPoint()
    Dim numDialog As INumberDialog = New NumberDialogClass()
    'Set the second point equal to the active point which may have been snapped 
    m_secondPoint = m_activePoint

    'Get the angle
    If (numDialog.DoModal("Angle 2", -45, 2, m_editor.Display.hWnd)) Then
      m_secondAngle = numDialog.Value * Math.PI / 180
    Else
      m_etoolPhase = ToolPhase.Inactive
    End If

    'Get the intersection point
    Dim constructPoint As IConstructPoint = New PointClass()
    constructPoint.ConstructAngleIntersection(m_firstPoint, m_firstAngle, m_secondPoint, m_secondAngle)

    Dim point As IPoint = constructPoint
    If (Point.IsEmpty) Then
      m_etoolPhase = ToolPhase.Inactive
      MessageBox.Show("No Point Calculated")
    End If

    'Draw the calculated intersection point and erase previous snap feedback
    m_activePoint = point
    m_etoolPhase = ToolPhase.Intersection
    m_snappingFeedback.Update(Nothing, 0)
    DrawPoint(m_activePoint)
  End Sub
  Private Sub GetIntersection()
    Dim editSketch As IEditSketch = m_editor
    editSketch.AddPoint(m_activePoint, True)
    'Set the phase to inactive, back to beginning
    m_etoolPhase = ToolPhase.Inactive
  End Sub

  Private Sub DrawPoint(ByVal pPoint As IPoint)
    'Draw a red graphic dot on the display at the given point location
    Dim color As IRgbColor
    Dim marker As ISimpleMarkerSymbol
    Dim appDisplay As IAppDisplay = m_editor.Display

    'Set the symbol (red, size 8)
    color = New RgbColor()
    color.Red = 255
    color.Green = 0
    color.Blue = 0
    marker = New SimpleMarkerSymbol()
    marker.Color = color
    marker.Size = 8

    'Draw the point
    appDisplay.StartDrawing(0, CShort(esriScreenCache.esriNoScreenCache))
    appDisplay.SetSymbol(marker)
    appDisplay.DrawPoint(pPoint)
    appDisplay.FinishDrawing()
  End Sub

  Public Sub GetClassID(ByRef pClassID As System.Guid) Implements IPersist.GetClassID
    'Explicitly set a guid. Used to set command.checked property
    pClassID = New Guid("cdcbb1bf-a87d-4927-8e75-9babe1979f90")
  End Sub

End Class

