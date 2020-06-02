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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ADf.COMSupport.OLE
Imports ESRI.ArcGIS


Public Class Form1
    Inherits System.Windows.Forms.Form
    <STAThread()> _
Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS Engine runtime. Application shutting down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New Form1())
    End Sub
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        'Release COM objects 
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents Text1 As System.Windows.Forms.TextBox
    Public WithEvents cmdReset As System.Windows.Forms.Button
    Public WithEvents cmdFullExtent As System.Windows.Forms.Button
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Text1 = New System.Windows.Forms.TextBox
        Me.cmdReset = New System.Windows.Forms.Button
        Me.cmdFullExtent = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Text1
        '
        Me.Text1.AcceptsReturn = True
        Me.Text1.AutoSize = False
        Me.Text1.BackColor = System.Drawing.SystemColors.Window
        Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text1.Location = New System.Drawing.Point(8, 304)
        Me.Text1.MaxLength = 0
        Me.Text1.Name = "Text1"
        Me.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text1.Size = New System.Drawing.Size(321, 25)
        Me.Text1.TabIndex = 2
        Me.Text1.Text = "Put a map in your app..."
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReset.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(336, 216)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReset.Size = New System.Drawing.Size(73, 25)
        Me.cmdReset.TabIndex = 1
        Me.cmdReset.Text = "Clear Text"
        '
        'cmdFullExtent
        '
        Me.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFullExtent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFullExtent.Location = New System.Drawing.Point(336, 248)
        Me.cmdFullExtent.Name = "cmdFullExtent"
        Me.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFullExtent.Size = New System.Drawing.Size(73, 25)
        Me.cmdFullExtent.TabIndex = 0
        Me.cmdFullExtent.Text = "Full Extent"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(336, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(81, 65)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Right mouse button to drag a rectangle to zoom in."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(336, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(81, 65)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Left mouse button to trace a line to draw text along. "
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 288)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(129, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Enter text:"
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(320, 272)
        Me.AxMapControl1.TabIndex = 6
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(120, 24)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl1.TabIndex = 7
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(420, 350)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.Text1)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.cmdFullExtent)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Draw Text Example"
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
    Private m_pPointCollection As IPointCollection
    Private m_pPolyline As IPolyline

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim sFilePath As String
        'Find sample data by navigating two folders up
        sFilePath = System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\World\Continents.lyr")

        'Add sample 'country' shapefile data
        AxMapControl1.AddLayerFromFile(sFilePath)
        'Set the extent
        AxMapControl1.Extent = AxMapControl1.get_Layer(0).AreaOfInterest

        Dim pGeoFeatureLayer As IGeoFeatureLayer
        'Grab hold of the IgeoFeaturelayer interface on the layer
        'in the map control in order to symbolize the data
        pGeoFeatureLayer = AxMapControl1.get_Layer(0)

        Dim pSimpleRenderer As ISimpleRenderer
        Dim pFillSymbol As ISimpleFillSymbol
        Dim pLineSymbol As ISimpleLineSymbol

        'Create a simple renderer and grab hold of ISimpleRenderer interface
        pSimpleRenderer = New SimpleRendererClass
        'Create a fill symbol and grab hold of the ISimpleFillSymbol interface
        pFillSymbol = New SimpleFillSymbolClass
        'Create a line symbol and grab hold of the ISimpleLineSymbol interface
        pLineSymbol = New SimpleLineSymbolClass

        'Assign line symbol and fill symbol properties
        pLineSymbol.Width = 0.1
        pLineSymbol.Color = GetRGBColor(255, 0, 0) 'Red
        pFillSymbol.Outline = pLineSymbol
        pFillSymbol.Color = GetRGBColor(0, 0, 255) 'Blue

        'Set the symbol property of the renderer
        pSimpleRenderer.Symbol = pFillSymbol

        'Set the renderer property of the geo feature layer
        pGeoFeatureLayer.Renderer = pSimpleRenderer

    End Sub

    Private Sub cmdFullExtent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFullExtent.Click

        'Assign map controls extent property to the full extent of all the layers
        AxMapControl1.Extent = AxMapControl1.FullExtent

    End Sub

    Private Sub cmdReset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReset.Click

        'Get rid of the line and points collection
        m_pPolyline = Nothing
        m_pPointCollection = Nothing

        'Refresh the foreground thereby removing any text annotation
        AxMapControl1.Refresh(esriViewDrawPhase.esriViewForeground, Nothing, Nothing)

    End Sub

    Private Function GetRGBColor(ByRef pRed As Integer, ByRef pGreen As Integer, ByRef pBlue As Integer) As ESRI.ArcGIS.Display.IRgbColor

        Dim pRGB As IRgbColor
        'Create rgb color and grab hold of the IRGBColor interface
        pRGB = New RgbColor
        'Set rgb color properties
        With pRGB
            .Red = pRed
            .Green = pGreen
            .Blue = pBlue
            .UseWindowsDithering = True
        End With
        GetRGBColor = pRGB

    End Function

    Private Sub AxMapControl1_OnAfterDraw(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterDrawEvent) Handles AxMapControl1.OnAfterDraw
        'If foreground refreshed
        Dim pLineSymbol As ILineSymbol
        Dim pTextSymbol As ITextSymbol
        Dim pTextPath As ITextPath
        Dim pSimpleTextSymbol As ISimpleTextSymbol
        If e.viewDrawPhase = esriViewDrawPhase.esriViewForeground Then
            'If a line object for splining text exists
            If Not m_pPolyline Is Nothing Then
                'Ensure there's at least two points in the line
                If m_pPointCollection.PointCount > 1 Then

                    'Create a line symbol and grab hold of the ILineSymbol interface
                    pLineSymbol = New SimpleLineSymbolClass
                    'Set line symbol properties
                    pLineSymbol.Color = GetRGBColor(0, 0, 0)
                    pLineSymbol.Width = 2

                    'Create text symbol
                    'Create a text symbol and grab hold of the ITextSymbol interface
                    pTextSymbol = New TextSymbolClass
                    ''Create a system drawing font symbol with the specified properties
                    Dim drawFont As New System.Drawing.Font("Arial", 16, FontStyle.Bold)

                    'Set the text symbol font by getting the IFontDisp interface
                    pTextSymbol.Font = GetIFontDispFromFont(drawFont)
                    pTextSymbol.Color = GetRGBColor(0, 0, 0)

                    'Create a text path and grab hold of the ITextPath interface
                    pTextPath = New BezierTextPathClass 'to spline the text
                    'Grab hold of the ISimpleTextSymbol interface through the ITextSymbol interface
                    pSimpleTextSymbol = pTextSymbol
                    'Set the text path of the simple text symbol
                    pSimpleTextSymbol.TextPath = pTextPath

                    'Draw the line object and spline the user text around the line
                    AxMapControl1.DrawShape(m_pPolyline, CType(pLineSymbol, Object))
                    AxMapControl1.DrawText(m_pPolyline, Text1.Text, CType(pTextSymbol, Object))
                End If
            End If
        End If
    End Sub

    Private Sub AxMapControl1_OnMouseDown(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        'If left hand mouse button
        Dim pPoint As IPoint
        Dim pEnvelope As IEnvelope
        If e.button = 1 Then

            'Create a point and grab hold of the IPoint interface
            pPoint = New PointClass
            'Set point properties
            pPoint.X = e.mapX
            pPoint.Y = e.mapY

            'If this is the first point of a new line
            If m_pPolyline Is Nothing Then
                'Create the forms private polyline member and grab hold of the IPolyline interface
                m_pPolyline = New PolylineClass
            End If

            'QI for the IPointsCollection interface using the IPolyline interface
            m_pPointCollection = m_pPolyline
            m_pPointCollection.AddPoint(pPoint)

            'Refresh the foreground thereby removing any text annotation
            AxMapControl1.Refresh(esriViewDrawPhase.esriViewForeground, Nothing, Nothing)

        Else

            'If right or middle mouse button zoom to user defined rectangle
            'Create an envelope and grab hold of the IEnvelope interface
            pEnvelope = AxMapControl1.TrackRectangle
            'If user dragged a rectangle
            If Not pEnvelope Is Nothing Then
                'Set map controls extent property
                AxMapControl1.Extent = pEnvelope
            End If
        End If
    End Sub
End Class