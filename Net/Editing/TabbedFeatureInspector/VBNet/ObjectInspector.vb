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
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem

<ComClass(ObjectInspector.ClassId, ObjectInspector.InterfaceId, ObjectInspector.EventsId), _
 ProgId("TabbedFeatureInspectorVBNet.ObjectInspector")> _
Public NotInheritable Class ObjectInspector
  Inherits UserControl
  Implements ESRI.ArcGIS.Editor.IObjectInspector
  Implements ESRI.ArcGIS.Geodatabase.IClassExtension
  Implements ESRI.ArcGIS.Geodatabase.IFeatureClassExtension

#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisibleAttribute(False)> _
  Public Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)

    'Add any COM registration code after the ArcGISCategoryRegistration() call

  End Sub

  <ComUnregisterFunction(), ComVisibleAttribute(False)> _
  Public Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)

    'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

  End Sub

#Region "ArcGIS Component Category Registrar generated code"
  ''' <summary>
  ''' Required method for ArcGIS Component Category registration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    GeoObjectClassExtensions.Register(regKey)

  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    GeoObjectClassExtensions.Unregister(regKey)

  End Sub

#End Region
#End Region

#Region "COM GUIDs"
  ' These  GUIDs provide the COM identity for this class 
  ' and its COM interfaces. If you change them, existing 
  ' clients will no longer be able to access the class.
  Public Const ClassId As String = "2cfe1569-8732-4e73-ac8b-31b87be9631b"
  Public Const InterfaceId As String = "8e71ff21-3906-415c-a26d-af2d9c00233e"
  Public Const EventsId As String = "5636873f-0653-42fd-aca8-f87953c685f8"
  Friend WithEvents inspectorTabControl As System.Windows.Forms.TabControl
  Friend WithEvents standardTabPage As System.Windows.Forms.TabPage
  Friend WithEvents pictureBoxDefault As System.Windows.Forms.PictureBox
  Friend WithEvents customTabPage As System.Windows.Forms.TabPage
  Friend WithEvents listBoxInspector As System.Windows.Forms.ListBox
#End Region

  Dim m_inspector As ESRI.ArcGIS.Editor.IObjectInspector
  Private m_classHelper As ESRI.ArcGIS.Geodatabase.IClass
  'The win32 methods are required for in VS.net to correctly set parenting of customizable     area in the attribute inspector.
  Private Declare Function SetParent Lib "user32" (ByVal hWndChild As Integer, ByVal hWndNewParent As Integer) As Integer
  Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
  Private Const SW_SHOW As Short = 5
  Private Const SW_HIDE As Short = 0
  Dim m_otitle As String = "Custom Feature Inspector Properties:"
  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()
    InitializeComponent()
    If Me.m_inspector Is Nothing Then
      Me.m_inspector = New FeatureInspector()
    End If
  End Sub
  ''' <summary>
  ''' Clear the inspector before inspecting another object.
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub Clear() Implements ESRI.ArcGIS.Editor.IObjectInspector.Clear
    Me.listBoxInspector.Items.Clear()
    Me.m_inspector.Clear()
  End Sub
  ''' <summary>
  ''' Copies the values from srcRow to the row being edited. 
  ''' </summary>
  ''' <param name="srcRow"></param>
  ''' <remarks></remarks>
  Public Sub Copy(ByVal srcRow As ESRI.ArcGIS.Geodatabase.IRow) Implements ESRI.ArcGIS.Editor.IObjectInspector.Copy
    Me.m_inspector.Copy(srcRow)
  End Sub
  ''' <summary>
  ''' The window handle for the inspector.
  ''' </summary>
  Public ReadOnly Property HWND() As Integer Implements ESRI.ArcGIS.Editor.IObjectInspector.HWND
    Get
      Try

        HWND = Me.Handle.ToInt32
     
        Exit Property

      Catch ex As Exception
        MessageBox.Show("IObjectInspector_hWnd: " & ex.Message)
      End Try
    End Get
  End Property
  ''' <summary>
  ''' Inspects the properties of the features.
  ''' </summary>
  ''' <param name="objects"></param>
  ''' <param name="Editor"></param>
    ''' <remarks>Implementation of the custom inspector for polygon, point and line geometry.</remarks>
  Public Sub Inspect(ByVal objects As ESRI.ArcGIS.Editor.IEnumRow, ByVal Editor As ESRI.ArcGIS.Editor.IEditor) Implements ESRI.ArcGIS.Editor.IObjectInspector.Inspect
    SetParent(Me.m_inspector.HWND, Me.standardTabPage.Handle.ToInt32)
    SetParent(Me.standardTabPage.Handle.ToInt32, Me.pictureBoxDefault.Handle.ToInt32)

    ShowWindow(Me.m_inspector.HWND, SW_SHOW)

    Me.m_inspector.Inspect(objects, Editor)

    'The user has selected the feature class name so exit. 
    If objects.Count > 1 Then Exit Sub

    Dim row As IRow = objects.Next
    Dim feature As IFeature = row

    'Custom inspector values set to populate the list box. 
    'depending on geometry of the feature. 
    Select Case feature.Shape.GeometryType
      Case esriGeometryType.esriGeometryPolygon
        'Do this for Polygons
        listBoxInspector.Items.Clear()
        ReportPolygons(feature)


      Case esriGeometryType.esriGeometryPolyline
        'Do this for polylines
        listBoxInspector.Items.Clear()
        ReportPolylines(feature)

      Case esriGeometryType.esriGeometryPoint
        'Do this for points
        listBoxInspector.Items.Clear()
        ReportPoints(feature)

    End Select
  End Sub
  ''' <summary>
  ''' Initializes the extension, passing in a reference to it's class helper. 
  ''' </summary>
  ''' <param name="pClassHelper"></param>
  ''' <param name="pExtensionProperties"></param>
  Public Sub Init(ByVal pClassHelper As ESRI.ArcGIS.Geodatabase.IClassHelper, ByVal pExtensionProperties As ESRI.ArcGIS.esriSystem.IPropertySet) Implements ESRI.ArcGIS.Geodatabase.IClassExtension.Init
    m_classHelper = pClassHelper.Class

  End Sub
  ''' <summary>
  ''' Informs the extension that it's class helper is going away.
  ''' </summary>
  Public Sub Shutdown() Implements ESRI.ArcGIS.Geodatabase.IClassExtension.Shutdown
    m_inspector = Nothing
  End Sub
  ''' <summary>
  ''' Reports area, perimeter and number of vertices of the polygon. 
  ''' </summary>
  ''' <param name="feature"></param>

  Private Sub ReportPolygons(ByVal feature As IFeature)
    Try
      Dim shpPolygon As IPolygon = feature.Shape
      Dim polyArea As IArea = shpPolygon
      Dim polyCurve As ICurve = shpPolygon
      Dim polyPoints As IPointCollection
      Dim fid As String = "FID:          "
      ' Stop the ListBox from drawing while items are added.
      listBoxInspector.BeginUpdate()
      listBoxInspector.ItemHeight = listBoxInspector.Font.Height * 2
      listBoxInspector.Items.Add(m_otitle)
      listBoxInspector.Items.Add("")
      listBoxInspector.Items.Add(fid.PadRight(22) & feature.OID)

      'Report Area First
      listBoxInspector.Items.Add("Area:                  " & CStr(polyArea.Area))

      'Then Perimeter
      polyCurve = shpPolygon
      listBoxInspector.Items.Add("Perimeter:           " & CStr(polyCurve.Length))

      'Number of vertices
      polyPoints = shpPolygon
      listBoxInspector.Items.Add("Vertices:              " & CStr(polyPoints.PointCount))

      Dim width As Integer = CInt(listBoxInspector.CreateGraphics().MeasureString(listBoxInspector.Items(listBoxInspector.Items.Count - 1).ToString(), listBoxInspector.Font).Width)
      ' Set the column width based on the width of each item in the list.
      listBoxInspector.ColumnWidth = width
      ' End the update process and force a repaint of the ListBox.
      listBoxInspector.EndUpdate()

    Catch ex As Exception
      MessageBox.Show("ReportPolygons:     " & ex.Message)
    End Try
  End Sub
  ''' <summary>
  ''' Reports length, FromPoint-x, FromPoint-y, ToPoint -x, ToPoint -y,  
  ''' </summary>
  ''' <param name="feature"></param>
  Private Sub ReportPolylines(ByVal feature As IFeature)
    Try
      Dim lCurve As ICurve

      lCurve = feature.Shape
      ' Stop the ListBox from drawing while items are added.
      listBoxInspector.BeginUpdate()
      listBoxInspector.Items.Add(m_otitle)
      listBoxInspector.Items.Add("")
      Dim fid As String = "FID:          "
      listBoxInspector.Items.Add(fid.PadRight(22) & feature.OID)

      'Report Length First
      listBoxInspector.Items.Add("LENGTH:             " & CStr(lCurve.Length))

      'Report Start Point next
      listBoxInspector.Items.Add("FROMPOINT-X:      " & CStr(lCurve.FromPoint.X))

      'Report End Point 
      listBoxInspector.Items.Add("FROMPOINT-Y:      " & CStr(lCurve.FromPoint.Y))

      'Report End Point last
      listBoxInspector.Items.Add("TOPOINT-X:          " & CStr(lCurve.ToPoint.X))

      listBoxInspector.Items.Add("TOPOINT-Y:          " & CStr(lCurve.ToPoint.Y))
      Dim width As Integer = CInt(listBoxInspector.CreateGraphics().MeasureString(listBoxInspector.Items(listBoxInspector.Items.Count - 1).ToString(), _
                 listBoxInspector.Font).Width)
      ' Set the column width based on the width of each item in the list.
      listBoxInspector.ColumnWidth = width
      ' End the update process and force a repaint of the ListBox.
      listBoxInspector.EndUpdate()

    Catch ex As Exception
      MessageBox.Show("ReportPolylines: " & ex.Message)
    End Try
  End Sub
  ''' <summary>
  ''' Reports the coordinates of the point feature. 
  ''' </summary>
  ''' <param name="feature"></param>
  Private Sub ReportPoints(ByVal feature As IFeature)
    Try
      Dim shpPt As IPoint = feature.Shape

      ' Stop the ListBox from drawing while items are added.
      listBoxInspector.BeginUpdate()
      listBoxInspector.Items.Add(m_otitle)
      listBoxInspector.Items.Add("")
      Dim fid As String = "FID:          "
      listBoxInspector.Items.Add(fid.PadRight(22) & feature.OID)

      'Report X and Y coordinate locations
      listBoxInspector.Items.Add("X-COORD:     " & (shpPt.X))
      listBoxInspector.Items.Add("Y-COORD:     " & (shpPt.Y))
      Dim width As Integer = CInt(listBoxInspector.CreateGraphics().MeasureString(listBoxInspector.Items(listBoxInspector.Items.Count - 1).ToString(), _
            listBoxInspector.Font).Width)
      ' Set the column width based on the width of each item in the list.
      listBoxInspector.ColumnWidth = width
      ' End the update process and force a repaint of the ListBox.
      listBoxInspector.EndUpdate()

    Catch ex As Exception
      MessageBox.Show("ReportPoints: " & ex.Message)
    End Try
  End Sub

  Private Sub InitializeComponent()
    Me.inspectorTabControl = New System.Windows.Forms.TabControl
    Me.standardTabPage = New System.Windows.Forms.TabPage
    Me.pictureBoxDefault = New System.Windows.Forms.PictureBox
    Me.customTabPage = New System.Windows.Forms.TabPage
    Me.listBoxInspector = New System.Windows.Forms.ListBox
    Me.inspectorTabControl.SuspendLayout()
    Me.standardTabPage.SuspendLayout()
    CType(Me.pictureBoxDefault, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.customTabPage.SuspendLayout()
    Me.SuspendLayout()
    '
    'inspectorTabControl
    '
    Me.inspectorTabControl.Controls.Add(Me.standardTabPage)
    Me.inspectorTabControl.Controls.Add(Me.customTabPage)
    Me.inspectorTabControl.Location = New System.Drawing.Point(3, 3)
    Me.inspectorTabControl.Name = "inspectorTabControl"
    Me.inspectorTabControl.SelectedIndex = 0
    Me.inspectorTabControl.Size = New System.Drawing.Size(326, 321)
    Me.inspectorTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
    Me.inspectorTabControl.TabIndex = 3
    '
    'standardTabPage
    '
    Me.standardTabPage.Controls.Add(Me.pictureBoxDefault)
    Me.standardTabPage.Location = New System.Drawing.Point(4, 22)
    Me.standardTabPage.Name = "standardTabPage"
    Me.standardTabPage.Padding = New System.Windows.Forms.Padding(3)
    Me.standardTabPage.Size = New System.Drawing.Size(318, 295)
    Me.standardTabPage.TabIndex = 0
    Me.standardTabPage.Text = "Standard"
    Me.standardTabPage.UseVisualStyleBackColor = True
    '
    'pictureBoxDefault
    '
    Me.pictureBoxDefault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.pictureBoxDefault.Location = New System.Drawing.Point(3, 3)
    Me.pictureBoxDefault.Name = "pictureBoxDefault"
    Me.pictureBoxDefault.Size = New System.Drawing.Size(309, 292)
    Me.pictureBoxDefault.TabIndex = 0
    Me.pictureBoxDefault.TabStop = False
    '
    'customTabPage
    '
    Me.customTabPage.Controls.Add(Me.listBoxInspector)
    Me.customTabPage.Location = New System.Drawing.Point(4, 22)
    Me.customTabPage.Name = "customTabPage"
    Me.customTabPage.Padding = New System.Windows.Forms.Padding(3)
    Me.customTabPage.Size = New System.Drawing.Size(318, 295)
    Me.customTabPage.TabIndex = 1
    Me.customTabPage.Text = "Custom"
    Me.customTabPage.UseVisualStyleBackColor = True
    '
    'listBoxInspector
    '
    Me.listBoxInspector.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.listBoxInspector.Location = New System.Drawing.Point(4, 6)
    Me.listBoxInspector.MultiColumn = True
    Me.listBoxInspector.Name = "listBoxInspector"
    Me.listBoxInspector.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.listBoxInspector.Size = New System.Drawing.Size(308, 264)
    Me.listBoxInspector.TabIndex = 3
    '
    'ObjectInspector
    '
    Me.Controls.Add(Me.inspectorTabControl)
    Me.Name = "ObjectInspector"
    Me.Size = New System.Drawing.Size(340, 330)
    Me.inspectorTabControl.ResumeLayout(False)
    Me.standardTabPage.ResumeLayout(False)
    CType(Me.pictureBoxDefault, System.ComponentModel.ISupportInitialize).EndInit()
    Me.customTabPage.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Private Sub ObjectInspector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub
End Class


