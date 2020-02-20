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
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.controls
Imports ESRI.ArcGIS.ADF.COMSupport.OLE
Imports ESRI.ArcGIS


Public Class Form1
    Inherits System.Windows.Forms.Form

    <STAThread()> _
Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Application shutting down. Unable to bind to ArcGIS Engine runtime.")
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
    Public WithEvents cmdFullExtent As System.Windows.Forms.Button
    Public WithEvents chkTracking As System.Windows.Forms.CheckBox
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.cmdFullExtent = New System.Windows.Forms.Button
        Me.chkTracking = New System.Windows.Forms.CheckBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdFullExtent
        '
        Me.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFullExtent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFullExtent.Location = New System.Drawing.Point(552, 480)
        Me.cmdFullExtent.Name = "cmdFullExtent"
        Me.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFullExtent.Size = New System.Drawing.Size(121, 25)
        Me.cmdFullExtent.TabIndex = 1
        Me.cmdFullExtent.Text = "Zoom to Full Extent"
        '
        'chkTracking
        '
        Me.chkTracking.BackColor = System.Drawing.SystemColors.Control
        Me.chkTracking.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTracking.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTracking.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTracking.Location = New System.Drawing.Point(8, 488)
        Me.chkTracking.Name = "chkTracking"
        Me.chkTracking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTracking.Size = New System.Drawing.Size(137, 17)
        Me.chkTracking.TabIndex = 0
        Me.chkTracking.Text = "Enable GPS Tracking"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(144, 480)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(353, 33)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Use the left hand mouse button to zoom in. Use the other mouse buttons to click o" & _
        "n an agent and change the symbology.  "
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(664, 464)
        Me.AxMapControl1.TabIndex = 3
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(24, 24)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl1.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(680, 514)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.cmdFullExtent)
        Me.Controls.Add(Me.chkTracking)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "On Her Majesty's Secret Service"
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private m_pGeographicCoordinateSystem As IGeographicCoordinateSystem
    Private m_pProjectedCoordinateSystem As IProjectedCoordinateSystem
    Private m_pGraphicsContainer As IGraphicsContainer
    Private m_pMapControl As IMapControl2

    Private Structure AGENT_IN_FIELD
        Dim Latitude As Double
        Dim Longitude As Double
        Dim CodeNumber As String
        Dim Located As Boolean
    End Structure
    Private agentArray(20) As AGENT_IN_FIELD

    Private Sub cmdFullExtent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFullExtent.Click

        m_pMapControl.Extent = m_pMapControl.FullExtent

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        m_pMapControl = AxMapControl1.Object

        'Find sample data
        Dim sFilePath As String
        sFilePath = System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\World")

        'Add sample shapefile data
        m_pMapControl.AddShapeFile(sFilePath, "world30")
        m_pMapControl.AddLayerFromFile(sFilePath & "\continents.lyr")

        'Symbolize the data
        SymbolizeData(m_pMapControl.Layer(0), 0.1, GetRGBColor(0, 0, 0), GetRGBColor(0, 128, 0))
        SymbolizeData(m_pMapControl.Layer(1), 0.1, GetRGBColor(0, 0, 0), GetRGBColor(140, 196, 254))
        'Set up a global Geographic Coordinate System
        MakeCoordinateSystems()

        'Get the MapControl's graphics container and get the IGraphicsContainer interface
        m_pGraphicsContainer = m_pMapControl.ActiveView.GraphicsContainer

        'Populate an array with agent id's and locations
        LoadAgentArray()

        'Loop through the array and display each agent location
        Dim i As Short
        For i = 0 To 19
            DisplayAgentLocation(agentArray(i))
        Next i

        Timer1.Interval = 800
    End Sub

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick

        'Distance used in calculating the new point location
        Dim dMaxDistance As Double
        dMaxDistance = m_pMapControl.Extent.Width / 20

        'Loop through the elements in the GraphicContainer and get the IElement interface
        m_pGraphicsContainer.Reset()
        Dim pElement As IElement
        pElement = m_pGraphicsContainer.Next

        Dim pElementProperties As IElementProperties
        Dim pPoint As IPoint
        Do While Not pElement Is Nothing
            'QI for IElementProperties interface from IElement interface
            pElementProperties = pElement
            'If agent has not been located
            If CBool(pElementProperties.Name) = False Then
                'Get hold of the IPoint interface from the elements geometry
                pPoint = pElement.Geometry
                'Create new random point coordinates based upon current location
                pPoint.X = pPoint.X - (dMaxDistance * (Rnd() - 0.5))
                pPoint.Y = pPoint.Y - (dMaxDistance * (Rnd() - 0.5))
                'Set the point onto the GeographicCoordinateSystem - WHERE the point came FROM
                pPoint.Project(m_pGeographicCoordinateSystem)
                If pPoint.IsEmpty = False Then
                    'Ensure that the point is within the horizon of the coordinate system. Mollweide
                    'has a generic rectangular horizon with the following limits:
                    '-179.999988540844, -90.000000000000, 179.999988540844, 90.000000000000
                    If pPoint.X > 179.999988540844 Then
                        pPoint.X = pPoint.X - 359.999977081688
                    ElseIf pPoint.X < -179.999988540844 Then
                        pPoint.X = pPoint.X + 359.999977081688
                    End If
                    If pPoint.Y > 89.891975 Then 'fudge value to clip near poles
                        pPoint.Y = pPoint.Y - 179.78395
                    ElseIf pPoint.Y < -89.891975 Then  'fudge value to clip near poles
                        pPoint.Y = pPoint.Y + 179.78395
                    End If
                    'Project the point onto the displays current spatial reference
                    'WHERE the point is going TO
                    pPoint.Project(m_pProjectedCoordinateSystem)
                    pElement.Geometry = pPoint
                End If
            End If
            pElement = m_pGraphicsContainer.Next
        Loop

        'Refresh the graphics
        m_pMapControl.Refresh(esriViewDrawPhase.esriViewGraphics)

    End Sub

    Private Sub DisplayAgentLocation(ByRef agent As AGENT_IN_FIELD)

        'Create a point and get the IPoint interface
        Dim pPoint As IPoint
        pPoint = New PointClass
        'Set the points x and y coordinates
        pPoint.PutCoords(agent.Longitude, agent.Latitude)
        'Set the points spatial reference - WHERE the point is coming FROM
        pPoint.SpatialReference = m_pGeographicCoordinateSystem
        'Project the point onto the displays current spatial reference - WHERE the point is going TO
        pPoint.Project(m_pProjectedCoordinateSystem)

        'Create a marker element and get the IElement interface
        Dim pElement As IElement
        pElement = New MarkerElementClass
        'Set the elements geometry
        pElement.Geometry = pPoint

        'QI for the IMarkerElement interface from the IElement interface
        Dim pMarkerElement As IMarkerElement
        pMarkerElement = pElement
        'Set the marker symbol
        pMarkerElement.Symbol = GetMarkerSymbol(agent.Located)

        'QI for the IElementProperties interface from the IMarkerElement interface
        Dim pElementProperties As IElementProperties
        pElementProperties = pMarkerElement
        pElementProperties.Name = CStr(agent.Located)

        'Add the element to the graphics container
        m_pGraphicsContainer.AddElement(pElement, 0)

    End Sub

    Private Function GetMarkerSymbol(ByRef bLocated As Boolean) As Object

        'Create a new system draw font
        Dim drawFont As New System.Drawing.Font("ESRI Crime Analysis", 21, FontStyle.Regular) ' FontStyle.Regular)

        'Create a new CharacterMarkerSymbol and get the ICharacterMarkerSymbol interface
        Dim pCharMarker As ICharacterMarkerSymbol
        pCharMarker = New CharacterMarkerSymbolClass
        'Set the marker symbol properties
        pCharMarker.Font = GetIFontDispFromFont(drawFont)
        If bLocated = True Then
            pCharMarker.CharacterIndex = 56
            pCharMarker.Color = GetRGBColor(255, 0, 0)
            pCharMarker.Size = 30
        Else
            pCharMarker.CharacterIndex = 46
            pCharMarker.Color = GetRGBColor(0, 0, 0)
            pCharMarker.Size = 30
        End If
        GetMarkerSymbol = pCharMarker

    End Function

    Private Sub MakeCoordinateSystems()

        'Create a spatial reference environment and get theISpatialReferenceFactory2 interface
        Dim pSpatRefFact As ISpatialReferenceFactory2
        pSpatRefFact = New SpatialReferenceEnvironmentClass
        'Create a geographic coordinate system and get the IGeographicCoordinateSystem interface
        m_pGeographicCoordinateSystem = pSpatRefFact.CreateGeographicCoordinateSystem(ESRI.ArcGIS.Geometry.esriSRGeoCSType.esriSRGeoCS_WGS1984)
        'Create a projected coordinate system and get the IProjectedCoordinateSystem interface
        m_pProjectedCoordinateSystem = pSpatRefFact.CreateProjectedCoordinateSystem(ESRI.ArcGIS.Geometry.esriSRProjCSType.esriSRProjCS_World_Mollweide)
        'Set the map controls spatial reference property
        m_pMapControl.SpatialReference = m_pProjectedCoordinateSystem

    End Sub

    Private Sub SymbolizeData(ByRef pLayer As ESRI.ArcGIS.Carto.ILayer2, ByRef dWidth As Double, ByRef pColorLine As ESRI.ArcGIS.Display.IRgbColor, ByRef pColorFill As ESRI.ArcGIS.Display.IRgbColor)

        'Create a line symbol and get the ILineSymbol interface
        Dim pLineSymbol As ILineSymbol
        pLineSymbol = New SimpleLineSymbolClass
        'Set the line symbol properties
        pLineSymbol.Width = dWidth
        pLineSymbol.Color = pColorLine

        'Create a fill symbol and get the IFillSymbol interface
        Dim pFillSymbol As ISimpleFillSymbol
        pFillSymbol = New SimpleFillSymbolClass
        'Set the fill symbol properties
        pFillSymbol.Outline = pLineSymbol
        pFillSymbol.Color = pColorFill

        'Create a simple renderer and get the ISimpleRenderer interface
        Dim pSimpleRenderer As ISimpleRenderer
        pSimpleRenderer = New SimpleRendererClass
        'Set the simple renderer properties
        pSimpleRenderer.Symbol = pFillSymbol

        'QI for the IGeoFeatureLayer interface from the ILayer2 interface
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pGeoFeatureLayer = pLayer
        'Set the GeoFeatureLayer properties
        pGeoFeatureLayer.Renderer = pSimpleRenderer

    End Sub

    Private Function GetRGBColor(ByRef pRed As Integer, ByRef pGreen As Integer, ByRef pBlue As Integer) As ESRI.ArcGIS.Display.IRgbColor

        'Create an RGB color and get the IRGBColor interface
        Dim pRGB As IRgbColor
        pRGB = New RgbColorClass
        'Set rgb color properties
        With pRGB
            .Red = pRed
            .Green = pGreen
            .Blue = pBlue
        End With
        GetRGBColor = pRGB

    End Function

    Private Sub LoadAgentArray()

        'Populate an array of agent locations and id's. The locations are in decimal degrees,
        'based on the WGS1984 geographic coordinate system. (ie unprojected).
        'Obviously, these values could be read directly from a GPS unit.
        agentArray(0).CodeNumber = "001"
        agentArray(0).Latitude = 56.185128983308
        agentArray(0).Longitude = 37.556904400607
        agentArray(0).Located = False
        agentArray(1).CodeNumber = "002"
        agentArray(1).Latitude = 48.3732928679818
        agentArray(1).Longitude = 6.91047040971168
        agentArray(1).Located = False
        agentArray(2).CodeNumber = "003"
        agentArray(2).Latitude = 32.1487101669196
        agentArray(2).Longitude = 39.3596358118361
        agentArray(2).Located = False
        agentArray(3).CodeNumber = "004"
        agentArray(3).Latitude = 29.7450682852807
        agentArray(3).Longitude = 71.2078907435508
        agentArray(3).Located = False
        agentArray(4).CodeNumber = "005"
        agentArray(4).Latitude = 38.7587253414264
        agentArray(4).Longitude = 138.509863429439
        agentArray(4).Located = False
        agentArray(5).CodeNumber = "006"
        agentArray(5).Latitude = 35.1532625189681
        agentArray(5).Longitude = -82.0242792109256
        agentArray(5).Located = False
        agentArray(6).CodeNumber = "007"
        agentArray(6).Latitude = -26.1396054628225
        agentArray(6).Longitude = 28.5432473444613
        agentArray(6).Located = True
        agentArray(7).CodeNumber = "008"
        agentArray(7).Latitude = 33.9514415781487
        agentArray(7).Longitude = 3.90591805766313
        agentArray(7).Located = False
        agentArray(8).CodeNumber = "009"
        agentArray(8).Latitude = 29.7450682852807
        agentArray(8).Longitude = 16.5250379362671
        agentArray(8).Located = False
        agentArray(9).CodeNumber = "010"
        agentArray(9).Latitude = 45.9696509863429
        agentArray(9).Longitude = 23.1350531107739
        agentArray(9).Located = False
        agentArray(10).CodeNumber = "011"
        agentArray(10).Latitude = 48.9742033383915
        agentArray(10).Longitude = 14.1213960546282
        agentArray(10).Located = False
        agentArray(11).CodeNumber = "012"
        agentArray(11).Latitude = 29.7450682852807
        agentArray(11).Longitude = 79.0197268588771
        agentArray(11).Located = False
        agentArray(12).CodeNumber = "013"
        agentArray(12).Latitude = 43.5660091047041
        agentArray(12).Longitude = 125.289833080425
        agentArray(12).Located = False
        agentArray(13).CodeNumber = "014"
        agentArray(13).Latitude = 7.5113808801214
        agentArray(13).Longitude = -68.2033383915023
        agentArray(13).Located = False
        agentArray(14).CodeNumber = "015"
        agentArray(14).Latitude = 9.31411229135053
        agentArray(14).Longitude = -79.6206373292868
        agentArray(14).Located = False
        agentArray(15).CodeNumber = "016"
        agentArray(15).Latitude = 8.71320182094082
        agentArray(15).Longitude = -9.31411229135053
        agentArray(15).Located = True
        agentArray(16).CodeNumber = "017"
        agentArray(16).Latitude = 22.5341426403642
        agentArray(16).Longitude = 53.7814871016692
        agentArray(16).Located = False
        agentArray(17).CodeNumber = "018"
        agentArray(17).Latitude = 42.3641881638847
        agentArray(17).Longitude = 45.9696509863429
        agentArray(17).Located = False
        agentArray(18).CodeNumber = "019"
        agentArray(18).Latitude = 39.3596358118361
        agentArray(18).Longitude = 27.9423368740516
        agentArray(18).Located = False
        agentArray(19).CodeNumber = "020"
        agentArray(19).Latitude = 22.5341426403642
        agentArray(19).Longitude = 104.257966616085
        agentArray(19).Located = False

    End Sub

    Private Sub chkTracking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTracking.Click
        'Turn the timer on or off
        If chkTracking.CheckState = CheckState.Checked Then
            Timer1.Start()
        ElseIf chkTracking.CheckState = CheckState.Unchecked Then
            Timer1.Stop()
        End If

    End Sub

    Private Sub AxMapControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        'If left mouse button then zoom in
        Dim pPoint As IPoint
        Dim pTopologicalOperator As ITopologicalOperator
        Dim pPolygon As IPolygon
        Dim pRelationalOperator As IRelationalOperator
        Dim pElement As IElement
        Dim pMarkerElement As IMarkerElement
        Dim pElementProperties As IElementProperties
        If e.button = 1 Then
            m_pMapControl.Extent = m_pMapControl.TrackRectangle
        Else
            'Create a point and get the IPoint interface
            pPoint = New PointClass
            'Set points coordinates
            pPoint.PutCoords(e.mapX, e.mapY)

            'QI for ITopologicalOperator interface through IPoint interface
            pTopologicalOperator = pPoint
            'Create a polygon by buffering the point and get the IPolygon interface
            pPolygon = pTopologicalOperator.Buffer(m_pMapControl.Extent.Width * 0.02)
            'QI for IRelationalOperator interface through IPolygon interface
            pRelationalOperator = pPolygon

            'Draw the polygon
            m_pMapControl.DrawShape(pPolygon)

            'Loop through the elements in the GraphicContainer and get the IElement interface
            m_pGraphicsContainer.Reset()
            pElement = m_pGraphicsContainer.Next
            Do While Not pElement Is Nothing
                'If the polygon contains the point
                If (pRelationalOperator.Contains(pElement.Geometry) = True) Then
                    'QI for IMarkerElement interface through IElement interface
                    pMarkerElement = pElement
                    pMarkerElement.Symbol = GetMarkerSymbol(True)
                    'QI for the IElementProperties interface through IElement interface
                    pElementProperties = pElement
                    pElementProperties.Name = CStr(True)
                End If
                pElement = m_pGraphicsContainer.Next
            Loop
            If chkTracking.CheckState = CheckState.Unchecked Then
                'Refresh the graphics
                m_pMapControl.Refresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
            End If
        End If
    End Sub
End Class