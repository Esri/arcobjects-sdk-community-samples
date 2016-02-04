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
Imports Microsoft.VisualBasic
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geometry
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace ViperPin
  Public NotInheritable Class ViperPinTool
	  Inherits BaseTool
	  Implements IShapeConstructorTool, ISketchTool
	#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisible(False)> _
	Private Shared Sub RegisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryRegistration(registerType)

	  '
	  ' TODO: Add any COM registration code here
	  '
	End Sub

	<ComUnregisterFunction(), ComVisible(False)> _
	Private Shared Sub UnregisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryUnregistration(registerType)

	  '
	  ' TODO: Add any COM unregistration code here
	  '
	End Sub

	#Region "ArcGIS Component Category Registrar generated code"
	''' <summary>
	''' Required method for ArcGIS Component Category registration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  MxCommands.Register(regKey)
	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  MxCommands.Unregister(regKey)
	End Sub

	#End Region
	#End Region

  Private m_application As IApplication
  Private m_editor As IEditor3
  Private m_editEvents As IEditEvents_Event
  Private m_editEvents5 As IEditEvents5_Event
  Private m_edSketch As IEditSketch3
  Private m_csc As IShapeConstructor

  Private m_form As ViperPinForm

  Public Sub New()
    MyBase.m_category = "Developer Samples" 'localizable text
    MyBase.m_caption = "ViperPin" 'Text in construct tools window
    MyBase.m_message = "populate parcel pin" 'localizable text
    MyBase.m_toolTip = "Viper PIN tool" 'localizable text      MyBase.m_name = "DeveloperSamples_ViperPin" 'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
    Try
      Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
      'MyBase.m_cursor = New System.Windows.Forms.Cursor(Me.GetType(), Me.GetType().Name + ".cur")
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try
  End Sub

#Region "ISketchTool Members"
  'pass to constructor
  Public Sub AddPoint(ByVal point As IPoint, ByVal Clone As Boolean, ByVal allowUndo As Boolean) Implements ISketchTool.AddPoint
    m_csc.AddPoint(point, Clone, allowUndo)
  End Sub

  Public ReadOnly Property Anchor() As IPoint Implements ISketchTool.Anchor
    Get
      Return m_csc.Anchor
    End Get
  End Property

  Public Property AngleConstraint() As Double Implements ISketchTool.AngleConstraint
    Get
      Return m_csc.AngleConstraint
    End Get
    Set(ByVal value As Double)
      m_csc.AngleConstraint = value
    End Set
  End Property

  Public Property Constraint() As esriSketchConstraint Implements ISketchTool.Constraint
    Get
      Return m_csc.Constraint
    End Get
    Set(ByVal value As esriSketchConstraint)
      m_csc.Constraint = value
    End Set
  End Property

  Public Property DistanceConstraint() As Double Implements ISketchTool.DistanceConstraint
    Get
      Return m_csc.DistanceConstraint
    End Get
    Set(ByVal value As Double)
      m_csc.DistanceConstraint = value
    End Set
  End Property

  Public Property IsStreaming() As Boolean Implements ISketchTool.IsStreaming
    Get
      Return m_csc.IsStreaming
    End Get
    Set(ByVal value As Boolean)
      m_csc.IsStreaming = value
    End Set
  End Property

  Public ReadOnly Property Location() As IPoint Implements ISketchTool.Location
    Get
      Return m_csc.Location
    End Get
  End Property

#End Region

#Region "ITool Members"
  'pass to constructor
  Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
    m_csc.OnMouseDown(Button, Shift, X, Y)
  End Sub

  Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
    m_csc.OnMouseMove(Button, Shift, X, Y)
  End Sub

  Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
    m_csc.OnMouseUp(Button, Shift, X, Y)
  End Sub

  Public Overrides Function OnContextMenu(ByVal X As Integer, ByVal Y As Integer) As Boolean
    Return m_csc.OnContextMenu(X, Y)
  End Function

  Public Overrides Sub OnKeyDown(ByVal keyCode As Integer, ByVal Shift As Integer)
    m_csc.OnKeyDown(keyCode, Shift)
  End Sub

  Public Overrides Sub OnKeyUp(ByVal keyCode As Integer, ByVal Shift As Integer)
    m_csc.OnKeyUp(keyCode, Shift)
  End Sub

  Public Overrides Sub Refresh(ByVal hDC As Integer)
    m_csc.Refresh(hDC)
  End Sub

  Public Overrides ReadOnly Property Cursor() As Integer
    Get
      Return m_csc.Cursor
    End Get
  End Property

  Public Overrides Sub OnDblClick()
    If Control.ModifierKeys = Keys.Shift Then
        Dim so As ISketchOperation = New SketchOperation()
        so.MenuString_2 = "Finish Sketch Part"
        so.Start(m_editor)
        m_edSketch.FinishSketchPart()
        so.Finish(Nothing)
    Else
        m_edSketch.FinishSketch()
    End If
  End Sub

  Public Overrides Function Deactivate() As Boolean
    'unsubscribe events
    RemoveHandler m_editEvents.OnSketchModified, AddressOf m_editEvents_OnSketchModified
    RemoveHandler m_editEvents5.OnShapeConstructorChanged, AddressOf m_editEvents5_OnShapeConstructorChanged
    RemoveHandler m_editEvents.OnSketchFinished, AddressOf m_editEvents_OnSketchFinished
    Return MyBase.Deactivate()
  End Function

#End Region

    Public Overrides Sub OnCreate(ByVal hook As Object)
      If hook Is Nothing Then
        Return
      End If
      m_application = TryCast(hook, IApplication)

      'get the editor
      Dim editorUid As New UID()
      editorUid.Value = "esriEditor.Editor"
      m_editor = TryCast(m_application.FindExtensionByCLSID(editorUid), IEditor3)
      m_editEvents = TryCast(m_editor, IEditEvents_Event)
      m_editEvents5 = TryCast(m_editor, IEditEvents5_Event)
    End Sub
    Public Overrides ReadOnly Property Enabled() As Boolean
      Get
        'Enable the tool if we are editing
        Return m_editor.EditState = esriEditState.esriStateEditing
      End Get
    End Property

    Public Overrides Sub OnClick()
      m_edSketch = TryCast(m_editor, IEditSketch3)

      'Restrict to line constructors (for this tool)
      m_edSketch.GeometryType = esriGeometryType.esriGeometryPolyline

      'Activate a constructor based on the current sketch geometry
      If m_edSketch.GeometryType = esriGeometryType.esriGeometryPoint Then
        m_csc = New PointConstructorClass()
      Else
        m_csc = New StraightConstructorClass()
      End If
      m_csc.Initialize(m_editor)
      m_edSketch.ShapeConstructor = m_csc
      m_csc.Activate()

      'set the current task to null
      m_editor.CurrentTask = Nothing

      'setup events
      AddHandler m_editEvents.OnSketchModified, AddressOf m_editEvents_OnSketchModified
      AddHandler m_editEvents5.OnShapeConstructorChanged, AddressOf m_editEvents5_OnShapeConstructorChanged
      AddHandler m_editEvents.OnSketchFinished, AddressOf m_editEvents_OnSketchFinished

      'Create form and pass initialization parameters
      m_form = New ViperPinForm(m_editor)

      MyBase.OnClick()
    End Sub

    Private Sub m_editEvents_OnSketchFinished()
      'send a space to hide the construction toolbar
      SendKeys.SendWait(" ")

      'Show the dialog modal
      m_form.ShowDialog()
    End Sub

    Private Sub m_editEvents_OnSketchModified()
      m_csc.SketchModified()
    End Sub

    Private Sub m_editEvents5_OnShapeConstructorChanged()
      'activate new constructor
      m_csc.Deactivate()
      m_csc = Nothing
      m_csc = m_edSketch.ShapeConstructor
      m_csc.Activate()
    End Sub
  End Class
End Namespace
