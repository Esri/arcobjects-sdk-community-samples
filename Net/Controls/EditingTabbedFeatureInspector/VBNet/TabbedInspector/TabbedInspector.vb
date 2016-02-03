Imports System
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

Namespace TabbedFeatureInspector


  <Guid(TabbedInspectorCLSID.TabbedInspectorCLSID)> _
  <ClassInterface(ClassInterfaceType.None)> _
  <ProgId("TabbedFeatureInspector.TabbedInspectorCS")> _
  Partial Public Class TabbedInspector
    Inherits UserControl
    Implements IEngineObjectInspector
    Implements IClassExtension
    Implements IFeatureClassExtension

#Region "COM Registration Function(s)"

    <ComRegisterFunction()> _
    <ComVisible(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType)
    End Sub

    <ComUnregisterFunction()> _
    <ComVisible(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType)
    End Sub

#Region "ArcGIS Component Category Registrar generated code"

    '/ <summary>
    '/ Required method for ArcGIS Component Category registration -
    '/ Do not modify the contents of this method with the code editor.
    '/ </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      GeoObjectClassExtensions.Register(regKey)
    End Sub

    '/ <summary>
    '/ Required method for ArcGIS Component Category unregistration -
    '/ Do not modify the contents of this method with the code editor.
    '/ </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      GeoObjectClassExtensions.Unregister(regKey)
    End Sub

#End Region

#End Region

    Private Const SW_SHOW As Short = 5
    Public Shared title As String = "Custom Feature Inspector Properties:"
    Private m_inspector As IEngineObjectInspector

    Public Sub New()
      InitializeComponent()
      If m_inspector Is Nothing Then
        m_inspector = New EngineFeatureInspector()
      End If
    End Sub

#Region "Handles"

    '/ <summary>
    '/ Returns the handle for the picture box.
    '/ </summary>
    Public ReadOnly Property picHwnd() As Integer
      Get
        Return defaultPictureBox.Handle.ToInt32()
      End Get
    End Property

    '/ <summary>
    '/ Returns the handle property of the tab page that holds the default inspector.
    '/ </summary>
    Public ReadOnly Property stabHwnd() As Integer
      Get
        Return standardTabPage.Handle.ToInt32()
      End Get
    End Property
#End Region

#Region "IEngineObjectInspector Implementations"
    '/ <summary>
    '/ Clear the inspector before inspecting another object.
    '/ </summary>
    Public Sub Clear() Implements IEngineObjectInspector.Clear
      m_inspector.Clear()
      customListBox.Items.Clear()
    End Sub

    '/ <summary>
    '/ Copies the values from srcRow to the row being edited.
    '/ </summary>
    '/ <param name="srcRow"></param>
    Public Sub Copy(ByVal srcRow As IRow) Implements IEngineObjectInspector.Copy
      m_inspector.Copy(srcRow)
    End Sub

    '/ <summary>
    '/ The window handle for the inspector.
    '/ </summary>
    Public ReadOnly Property hWnd() As Integer Implements IEngineObjectInspector.hWnd
      Get
        Return Handle.ToInt32()
      End Get
    End Property

    '/ <summary>
    '/ Inspects the properties of the features.
    '/ </summary>
    '/ <param name="objects"></param>
    '/ <param name="Editor"></param>
    Public Sub Inspect(ByVal objects As IEngineEnumRow, ByVal Editor As IEngineEditor) Implements IEngineObjectInspector.Inspect
      Try
        SetParent(m_inspector.hWnd, stabHwnd)
        SetParent(stabHwnd, picHwnd)

        ShowWindow(m_inspector.hWnd, SW_SHOW)
        m_inspector.Inspect(objects, Editor)

        Dim EnumRow As IEngineEnumRow = objects
        Dim row As IRow = EnumRow.Next()
        Dim inspFeature As IFeature = DirectCast(row, IFeature)

        'user selected the layer name instead of a feature.
        If objects.Count > 1 Then
          Return
        End If

        Select Case inspFeature.Shape.GeometryType
          Case esriGeometryType.esriGeometryPolygon
            'do this for polygons.
            customListBox.Items.Clear()
            ReportPolygons(inspFeature)
            Exit Sub
          Case esriGeometryType.esriGeometryPolyline
            'do this for lines.
            customListBox.Items.Clear()
            ReportPolylines(inspFeature)
            Exit Sub
          Case esriGeometryType.esriGeometryPoint
            'do this for points.
            customListBox.Items.Clear()
            ReportPoints(inspFeature)
            Exit Sub
          Case Else
            Exit Sub
        End Select
        'End switch.
      Catch ex As Exception
        MessageBox.Show("IObjectInspector_Inspect: " + ex.Message)
      End Try
    End Sub

#End Region

#Region "IClassExtension Implementations"

    '/ <summary>
    '/ Initializes the extension, passing in a reference to its class helper.
    '/ </summary>
    '/ <param name="pClassHelper"></param>
    '/ <param name="pExtensionProperties"></param>
    Public Sub Init(ByVal pClassHelper As IClassHelper, ByVal pExtensionProperties As IPropertySet) Implements IClassExtension.Init
    End Sub

    '/ <summary>
    '/ Informs the extension that its class helper is going away.
    '/ </summary>
    Public Sub Shutdown() Implements IClassExtension.Shutdown
      m_inspector = Nothing
    End Sub

#End Region
    <DllImport("user32.dll", CharSet:=CharSet.Ansi)> _
    Private Shared Function SetParent(ByVal hWndChild As Integer, ByVal hWndNewParent As Integer) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Ansi)> _
    Private Shared Function ShowWindow(ByVal hWnd As Integer, ByVal nCmdShow As Integer) As Integer
    End Function

    '<DllImport("user32.dll", CharSet = CharSet.Ansi)> _ 
    'private static extern Integer ShowWindow(Integer hWnd, Integer nCmdShow)

    '/ <summary>
    '/ Reports the area, perimeter, and number of vertices for the selected polygon
    '/ </summary>
    '/ <param name="inspFeature"></param>
    Private Sub ReportPolygons(ByVal inspFeature As IFeature)
      Try
        Dim shpPolygon As IPolygon = DirectCast(inspFeature.Shape, IPolygon)
        Dim polyArea As IArea = DirectCast(shpPolygon, IArea)
        Dim polyCurve As ICurve = shpPolygon
        Dim polyPoints As IPointCollection = DirectCast(shpPolygon, IPointCollection)

        customListBox.BeginUpdate()
        customListBox.Items.Add(title)
        customListBox.Items.Add("")
        customListBox.Items.Add("FID:         " + inspFeature.OID.ToString)

        'Report Area First
        customListBox.Items.Add("AREA :       " + Convert.ToString(polyArea.Area))

        'Then Perimeter
        customListBox.Items.Add("PERIMETER:   " + Convert.ToString(polyCurve.Length))

        'Number of vertices
        'polyPoints = shpPolygon
        customListBox.Items.Add("VERTICES:   " + Convert.ToString(polyPoints.PointCount))

        ' Determine the width of the items in the list to get the best column width setting.
        Dim width As Integer
        width = customListBox.CreateGraphics() _
        .MeasureString(customListBox.Items(customListBox.Items.Count - 1).ToString(), customListBox.Font).Width()

        ' Set the column width based on the width of each item in the list.
        customListBox.ColumnWidth = width

        customListBox.EndUpdate()
      Catch ex As FormatException
        MessageBox.Show("ReportPolygons: " + ex.Message)
      End Try
    End Sub

    '/ <summary>
    '/ Reports length, FromPoint-x, FromPoint-y, ToPoint-x, ToPoint-x of selected polylines.
    '/ </summary>
    '/ <param name="inspFeature"></param>
    Private Sub ReportPolylines(ByVal inspFeature As IFeature)
      Dim lCurve As ICurve = DirectCast(inspFeature.Shape, ICurve)

      customListBox.BeginUpdate()

      customListBox.Items.Add(title)
      customListBox.Items.Add("")
      customListBox.Items.Add("FID:                    " + Convert.ToString(inspFeature.OID))

      'Report Length First
      customListBox.Items.Add("LENGTH:                " + Convert.ToString(lCurve.Length))

      'Report Start Point next
      customListBox.Items.Add("FROMPOINT-X:    " + Convert.ToString(lCurve.FromPoint.X))

      'Report End Point 
      customListBox.Items.Add("FROMPOINT-Y:    " + Convert.ToString(lCurve.FromPoint.Y))

      'Report End Point last
      customListBox.Items.Add("TOPOINT-X:        " + Convert.ToString(lCurve.ToPoint.X))

      customListBox.Items.Add("TOPOINT-Y:        " + Convert.ToString(lCurve.ToPoint.Y))

      ' Determine the width of the items in the list to get the best column width setting.
      Dim width As Integer
      width = customListBox.CreateGraphics() _
              .MeasureString(customListBox.Items(customListBox.Items.Count - 1).ToString(), customListBox.Font).Width
      ' Set the column width based on the width of each item in the list.
      customListBox.ColumnWidth = width
      customListBox.EndUpdate()
    End Sub

    '/ <summary>
    '/ Reports the coordinates of the selected point features.
    '/ </summary>
    '/ <param name="inspFeature"></param>
    Private Sub ReportPoints(ByVal inspFeature As IFeature)
      Dim shpPt As IPoint = DirectCast(inspFeature.Shape, IPoint)

      customListBox.BeginUpdate()
      customListBox.Items.Add(title)
      customListBox.Items.Add("")
      customListBox.Items.Add("FID:       " + inspFeature.OID.ToString)

      'Report X and Y coordinate locations
      customListBox.Items.Add("X-COORD :   " + Convert.ToString(shpPt.X))
      customListBox.Items.Add("Y-COORD :   " + Convert.ToString(shpPt.Y))
      ' Determine the width of the items in the list to get the best column width setting.
      Dim width As Integer
      width = customListBox.CreateGraphics() _
              .MeasureString(customListBox.Items(customListBox.Items.Count - 1).ToString(), customListBox.Font).Width
      ' Set the column width based on the width of each item in the list.
      customListBox.ColumnWidth = width

      customListBox.EndUpdate()
    End Sub
    Private WithEvents customTabPage As System.Windows.Forms.TabPage
    Private WithEvents customListBox As System.Windows.Forms.ListBox
    Private WithEvents standardTabPage As System.Windows.Forms.TabPage
    Private WithEvents inspectorTabControl As System.Windows.Forms.TabControl
    Private WithEvents defaultPictureBox As System.Windows.Forms.PictureBox

    Private Sub InitializeComponent()
      Me.customTabPage = New System.Windows.Forms.TabPage
      Me.customListBox = New System.Windows.Forms.ListBox
      Me.defaultPictureBox = New System.Windows.Forms.PictureBox
      Me.inspectorTabControl = New System.Windows.Forms.TabControl
      Me.standardTabPage = New System.Windows.Forms.TabPage
      Me.customTabPage.SuspendLayout()
      DirectCast(Me.defaultPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.inspectorTabControl.SuspendLayout()
      Me.standardTabPage.SuspendLayout()
      Me.SuspendLayout()
      '
      'customTabPage
      '
      Me.customTabPage.AutoScroll = True
      Me.customTabPage.Controls.Add(Me.customListBox)
      Me.customTabPage.Location = New System.Drawing.Point(4, 22)
      Me.customTabPage.Name = "customTabPage"
      Me.customTabPage.Padding = New System.Windows.Forms.Padding(3)
      Me.customTabPage.Size = New System.Drawing.Size(292, 294)
      Me.customTabPage.TabIndex = 1
      Me.customTabPage.Text = "Custom"
      Me.customTabPage.UseVisualStyleBackColor = True
      '
      'customListBox
      '
      Me.customListBox.Dock = System.Windows.Forms.DockStyle.Fill
      Me.customListBox.FormattingEnabled = True
      Me.customListBox.Location = New System.Drawing.Point(3, 3)
      Me.customListBox.MaximumSize = New System.Drawing.Size(1000, 1000)
      Me.customListBox.MultiColumn = True
      Me.customListBox.Name = "customListBox"
      Me.customListBox.Size = New System.Drawing.Size(286, 277)
      Me.customListBox.TabIndex = 0
      '
      'defaultPictureBox
      '
      Me.defaultPictureBox.BackColor = System.Drawing.SystemColors.Window
      Me.defaultPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
      Me.defaultPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
      Me.defaultPictureBox.Location = New System.Drawing.Point(3, 3)
      Me.defaultPictureBox.MaximumSize = New System.Drawing.Size(1000, 1000)
      Me.defaultPictureBox.Name = "defaultPictureBox"
      Me.defaultPictureBox.Size = New System.Drawing.Size(136, 118)
      Me.defaultPictureBox.TabIndex = 0
      Me.defaultPictureBox.TabStop = False
      '
      'inspectorTabControl
      '
      Me.inspectorTabControl.Controls.Add(Me.standardTabPage)
      Me.inspectorTabControl.Controls.Add(Me.customTabPage)
      Me.inspectorTabControl.Dock = System.Windows.Forms.DockStyle.Fill
      Me.inspectorTabControl.Location = New System.Drawing.Point(0, 0)
      Me.inspectorTabControl.MaximumSize = New System.Drawing.Size(1000, 1000)
      Me.inspectorTabControl.Name = "inspectorTabControl"
      Me.inspectorTabControl.SelectedIndex = 0
      Me.inspectorTabControl.Size = New System.Drawing.Size(150, 150)
      Me.inspectorTabControl.TabIndex = 1
      '
      'standardTabPage
      '
      Me.standardTabPage.AutoScroll = True
      Me.standardTabPage.Controls.Add(Me.defaultPictureBox)
      Me.standardTabPage.Location = New System.Drawing.Point(4, 22)
      Me.standardTabPage.Name = "standardTabPage"
      Me.standardTabPage.Padding = New System.Windows.Forms.Padding(3)
      Me.standardTabPage.Size = New System.Drawing.Size(142, 124)
      Me.standardTabPage.TabIndex = 0
      Me.standardTabPage.Text = "Standard"
      Me.standardTabPage.UseVisualStyleBackColor = True
      '
      'TabbedInspector
      '
      Me.Controls.Add(Me.inspectorTabControl)
      Me.Name = "TabbedInspector"
      Me.customTabPage.ResumeLayout(False)
      DirectCast(Me.defaultPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
      Me.inspectorTabControl.ResumeLayout(False)
      Me.standardTabPage.ResumeLayout(False)
      Me.ResumeLayout(False)

    End Sub
  End Class
End Namespace


