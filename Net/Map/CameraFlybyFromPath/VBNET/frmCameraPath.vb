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
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports CameraFlybyFromPath
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase

Namespace CameraFlybyFromPath
    ''' <summary>
    ''' Summary description for frmCameraPath.
    ''' </summary>
    Public Class frmCameraPath : Inherits System.Windows.Forms.Form
        Private groupBox_PathSource As System.Windows.Forms.GroupBox
        Private radiobutton_Sel_line_feat As System.Windows.Forms.RadioButton
        Private checkBox_ReverseOrder As System.Windows.Forms.CheckBox
        Private label_VertOffset As System.Windows.Forms.Label
        Private textBox_VertOffset As System.Windows.Forms.TextBox
        Private label_Simp_Factor As System.Windows.Forms.Label
        Private label1 As System.Windows.Forms.Label
        Private label2 As System.Windows.Forms.Label
        Private groupBox_PathDest As System.Windows.Forms.GroupBox
        Private radioButton_flyby As System.Windows.Forms.RadioButton
        Private radioButton_currentTarget As System.Windows.Forms.RadioButton
        Private radioButton_currentObserver As System.Windows.Forms.RadioButton
        Private label_TrackName As System.Windows.Forms.Label
        Private textBox_TrackName As System.Windows.Forms.TextBox
        Private WithEvents button_Import As System.Windows.Forms.Button
        Private WithEvents button_Cancel As System.Windows.Forms.Button
        Private groupBox_TrackProps As System.Windows.Forms.GroupBox
        Private checkBox_Overwrite As System.Windows.Forms.CheckBox
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private trackBarSimplificationFactor As System.Windows.Forms.TrackBar
        Private comboBoxLayers As System.Windows.Forms.ComboBox

        Private globe As IGlobe
        Private layerArray As IArray = New ArrayClass()

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            ''
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.groupBox_PathSource = New System.Windows.Forms.GroupBox()
            Me.comboBoxLayers = New System.Windows.Forms.ComboBox()
            Me.label1 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.label_Simp_Factor = New System.Windows.Forms.Label()
            Me.textBox_VertOffset = New System.Windows.Forms.TextBox()
            Me.label_VertOffset = New System.Windows.Forms.Label()
            Me.checkBox_ReverseOrder = New System.Windows.Forms.CheckBox()
            Me.radiobutton_Sel_line_feat = New System.Windows.Forms.RadioButton()
            Me.trackBarSimplificationFactor = New System.Windows.Forms.TrackBar()
            Me.groupBox_PathDest = New System.Windows.Forms.GroupBox()
            Me.radioButton_currentObserver = New System.Windows.Forms.RadioButton()
            Me.radioButton_currentTarget = New System.Windows.Forms.RadioButton()
            Me.radioButton_flyby = New System.Windows.Forms.RadioButton()
            Me.label_TrackName = New System.Windows.Forms.Label()
            Me.textBox_TrackName = New System.Windows.Forms.TextBox()
            Me.button_Import = New System.Windows.Forms.Button()
            Me.button_Cancel = New System.Windows.Forms.Button()
            Me.checkBox_Overwrite = New System.Windows.Forms.CheckBox()
            Me.groupBox_TrackProps = New System.Windows.Forms.GroupBox()
            Me.groupBox_PathSource.SuspendLayout()
            CType(Me.trackBarSimplificationFactor, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.groupBox_PathDest.SuspendLayout()
            Me.groupBox_TrackProps.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' groupBox_PathSource
            ' 
            Me.groupBox_PathSource.Controls.Add(Me.comboBoxLayers)
            Me.groupBox_PathSource.Controls.Add(Me.label1)
            Me.groupBox_PathSource.Controls.Add(Me.label2)
            Me.groupBox_PathSource.Controls.Add(Me.label_Simp_Factor)
            Me.groupBox_PathSource.Controls.Add(Me.textBox_VertOffset)
            Me.groupBox_PathSource.Controls.Add(Me.label_VertOffset)
            Me.groupBox_PathSource.Controls.Add(Me.checkBox_ReverseOrder)
            Me.groupBox_PathSource.Controls.Add(Me.radiobutton_Sel_line_feat)
            Me.groupBox_PathSource.Controls.Add(Me.trackBarSimplificationFactor)
            Me.groupBox_PathSource.Location = New System.Drawing.Point(8, 8)
            Me.groupBox_PathSource.Name = "groupBox_PathSource"
            Me.groupBox_PathSource.Size = New System.Drawing.Size(296, 160)
            Me.groupBox_PathSource.TabIndex = 0
            Me.groupBox_PathSource.TabStop = False
            Me.groupBox_PathSource.Text = "Path source"
            ' 
            ' comboBoxLayers
            ' 
            Me.comboBoxLayers.Location = New System.Drawing.Point(152, 24)
            Me.comboBoxLayers.Name = "comboBoxLayers"
            Me.comboBoxLayers.Size = New System.Drawing.Size(136, 21)
            Me.comboBoxLayers.TabIndex = 8
            ' 
            ' label1
            ' 
            Me.label1.Location = New System.Drawing.Point(258, 137)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(30, 16)
            Me.label1.TabIndex = 6
            Me.label1.Text = "High"
            ' 
            ' label2
            ' 
            Me.label2.Location = New System.Drawing.Point(117, 137)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(32, 16)
            Me.label2.TabIndex = 7
            Me.label2.Text = "Low"
            ' 
            ' label_Simp_Factor
            ' 
            Me.label_Simp_Factor.Location = New System.Drawing.Point(16, 112)
            Me.label_Simp_Factor.Name = "label_Simp_Factor"
            Me.label_Simp_Factor.Size = New System.Drawing.Size(104, 24)
            Me.label_Simp_Factor.TabIndex = 5
            Me.label_Simp_Factor.Text = "Simplification factor"
            ' 
            ' textBox_VertOffset
            ' 
            Me.textBox_VertOffset.Location = New System.Drawing.Point(88, 72)
            Me.textBox_VertOffset.Name = "textBox_VertOffset"
            Me.textBox_VertOffset.Size = New System.Drawing.Size(56, 20)
            Me.textBox_VertOffset.TabIndex = 3
            Me.textBox_VertOffset.Text = "0.0"
            Me.textBox_VertOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            ' 
            ' label_VertOffset
            ' 
            Me.label_VertOffset.Location = New System.Drawing.Point(16, 80)
            Me.label_VertOffset.Name = "label_VertOffset"
            Me.label_VertOffset.Size = New System.Drawing.Size(88, 16)
            Me.label_VertOffset.TabIndex = 2
            Me.label_VertOffset.Text = "Vertical offset"
            ' 
            ' checkBox_ReverseOrder
            ' 
            Me.checkBox_ReverseOrder.Location = New System.Drawing.Point(16, 48)
            Me.checkBox_ReverseOrder.Name = "checkBox_ReverseOrder"
            Me.checkBox_ReverseOrder.Size = New System.Drawing.Size(144, 24)
            Me.checkBox_ReverseOrder.TabIndex = 1
            Me.checkBox_ReverseOrder.Text = "Apply in reverse order"
            ' 
            ' radiobutton_Sel_line_feat
            ' 
            Me.radiobutton_Sel_line_feat.Checked = True
            Me.radiobutton_Sel_line_feat.Location = New System.Drawing.Point(16, 24)
            Me.radiobutton_Sel_line_feat.Name = "radiobutton_Sel_line_feat"
            Me.radiobutton_Sel_line_feat.Size = New System.Drawing.Size(144, 24)
            Me.radiobutton_Sel_line_feat.TabIndex = 0
            Me.radiobutton_Sel_line_feat.TabStop = True
            Me.radiobutton_Sel_line_feat.Text = "Selected line feature in"
            ' 
            ' trackBarSimplificationFactor
            ' 
            Me.trackBarSimplificationFactor.Cursor = System.Windows.Forms.Cursors.Hand
            Me.trackBarSimplificationFactor.Location = New System.Drawing.Point(120, 108)
            Me.trackBarSimplificationFactor.Maximum = 100
            Me.trackBarSimplificationFactor.Name = "trackBarSimplificationFactor"
            Me.trackBarSimplificationFactor.Size = New System.Drawing.Size(168, 45)
            Me.trackBarSimplificationFactor.TabIndex = 4
            Me.trackBarSimplificationFactor.Value = 5
            ' 
            ' groupBox_PathDest
            ' 
            Me.groupBox_PathDest.Controls.Add(Me.radioButton_currentObserver)
            Me.groupBox_PathDest.Controls.Add(Me.radioButton_currentTarget)
            Me.groupBox_PathDest.Controls.Add(Me.radioButton_flyby)
            Me.groupBox_PathDest.Location = New System.Drawing.Point(8, 176)
            Me.groupBox_PathDest.Name = "groupBox_PathDest"
            Me.groupBox_PathDest.Size = New System.Drawing.Size(296, 104)
            Me.groupBox_PathDest.TabIndex = 1
            Me.groupBox_PathDest.TabStop = False
            Me.groupBox_PathDest.Text = "Path destination"
            ' 
            ' radioButton_currentObserver
            ' 
            Me.radioButton_currentObserver.Location = New System.Drawing.Point(16, 72)
            Me.radioButton_currentObserver.Name = "radioButton_currentObserver"
            Me.radioButton_currentObserver.Size = New System.Drawing.Size(272, 16)
            Me.radioButton_currentObserver.TabIndex = 2
            Me.radioButton_currentObserver.Text = "Move target along path with current observer"
            ' 
            ' radioButton_currentTarget
            ' 
            Me.radioButton_currentTarget.Location = New System.Drawing.Point(16, 48)
            Me.radioButton_currentTarget.Name = "radioButton_currentTarget"
            Me.radioButton_currentTarget.Size = New System.Drawing.Size(272, 16)
            Me.radioButton_currentTarget.TabIndex = 1
            Me.radioButton_currentTarget.Text = "Move observer along path with current target"
            ' 
            ' radioButton_flyby
            ' 
            Me.radioButton_flyby.Checked = True
            Me.radioButton_flyby.Location = New System.Drawing.Point(16, 24)
            Me.radioButton_flyby.Name = "radioButton_flyby"
            Me.radioButton_flyby.Size = New System.Drawing.Size(272, 16)
            Me.radioButton_flyby.TabIndex = 0
            Me.radioButton_flyby.TabStop = True
            Me.radioButton_flyby.Text = "Move both observer and target along path (fly by)"
            ' 
            ' label_TrackName
            ' 
            Me.label_TrackName.Location = New System.Drawing.Point(16, 24)
            Me.label_TrackName.Name = "label_TrackName"
            Me.label_TrackName.Size = New System.Drawing.Size(88, 16)
            Me.label_TrackName.TabIndex = 2
            Me.label_TrackName.Text = "Track name:"
            ' 
            ' textBox_TrackName
            ' 
            Me.textBox_TrackName.Location = New System.Drawing.Point(96, 23)
            Me.textBox_TrackName.Name = "textBox_TrackName"
            Me.textBox_TrackName.Size = New System.Drawing.Size(192, 20)
            Me.textBox_TrackName.TabIndex = 3
            Me.textBox_TrackName.Text = "Track from path"
            ' 
            ' button_Import
            ' 
            Me.button_Import.Location = New System.Drawing.Point(144, 368)
            Me.button_Import.Name = "button_Import"
            Me.button_Import.Size = New System.Drawing.Size(80, 24)
            Me.button_Import.TabIndex = 4
            Me.button_Import.Text = "Import"
            '			Me.button_Import.Click += New System.EventHandler(Me.button_Import_Click);
            ' 
            ' button_Cancel
            ' 
            Me.button_Cancel.Location = New System.Drawing.Point(232, 368)
            Me.button_Cancel.Name = "button_Cancel"
            Me.button_Cancel.Size = New System.Drawing.Size(72, 24)
            Me.button_Cancel.TabIndex = 5
            Me.button_Cancel.Text = "Cancel"
            '			Me.button_Cancel.Click += New System.EventHandler(Me.button_Cancel_Click);
            ' 
            ' checkBox_Overwrite
            ' 
            Me.checkBox_Overwrite.Checked = True
            Me.checkBox_Overwrite.CheckState = System.Windows.Forms.CheckState.Checked
            Me.checkBox_Overwrite.Enabled = False
            Me.checkBox_Overwrite.Location = New System.Drawing.Point(16, 48)
            Me.checkBox_Overwrite.Name = "checkBox_Overwrite"
            Me.checkBox_Overwrite.Size = New System.Drawing.Size(184, 16)
            Me.checkBox_Overwrite.TabIndex = 7
            Me.checkBox_Overwrite.Text = "Overwrite last imported track"
            ' 
            ' groupBox_TrackProps
            ' 
            Me.groupBox_TrackProps.Controls.Add(Me.textBox_TrackName)
            Me.groupBox_TrackProps.Controls.Add(Me.label_TrackName)
            Me.groupBox_TrackProps.Controls.Add(Me.checkBox_Overwrite)
            Me.groupBox_TrackProps.Location = New System.Drawing.Point(8, 288)
            Me.groupBox_TrackProps.Name = "groupBox_TrackProps"
            Me.groupBox_TrackProps.Size = New System.Drawing.Size(296, 72)
            Me.groupBox_TrackProps.TabIndex = 8
            Me.groupBox_TrackProps.TabStop = False
            Me.groupBox_TrackProps.Text = "Animation track properties"
            ' 
            ' frmCameraPath
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(312, 398)
            Me.Controls.Add(Me.groupBox_TrackProps)
            Me.Controls.Add(Me.button_Cancel)
            Me.Controls.Add(Me.button_Import)
            Me.Controls.Add(Me.groupBox_PathDest)
            Me.Controls.Add(Me.groupBox_PathSource)
            Me.Name = "frmCameraPath"
            Me.Text = "Camera flyby from path"
            '			Me.Load += New System.EventHandler(Me.frmCameraPath_Load);
            Me.groupBox_PathSource.ResumeLayout(False)
            Me.groupBox_PathSource.PerformLayout()
            CType(Me.trackBarSimplificationFactor, System.ComponentModel.ISupportInitialize).EndInit()
            Me.groupBox_PathDest.ResumeLayout(False)
            Me.groupBox_TrackProps.ResumeLayout(False)
            Me.groupBox_TrackProps.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
#End Region


        Private Sub frmCameraPath_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim scene As IScene = globe.GlobeDisplay.Scene

            'check
            Dim enumFtrLayers As IEnumLayer = scene.Layers(Nothing, True)
            enumFtrLayers.Reset()
            Dim lyr As ILayer = Nothing
            Dim fLayer As IFeatureLayer = Nothing
            lyr = enumFtrLayers.Next()
            Do
                If TypeOf lyr Is IFeatureLayer Then
                    fLayer = TryCast(lyr, IFeatureLayer)
                    Exit Do
                End If

                lyr = enumFtrLayers.Next()
            Loop While Not lyr Is Nothing

            If Not fLayer Is Nothing Then
                Dim uid As UID = New UIDClass()
                uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"

                Dim enumLayers As IEnumLayer = scene.Layers(uid, True)
                enumLayers.Reset()
                fLayer = CType(enumLayers.Next(), IFeatureLayer)

                Do While Not fLayer Is Nothing
                    If fLayer.FeatureClass.ShapeType.ToString() = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline.ToString() OrElse fLayer.FeatureClass.ShapeType.ToString() = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryLine.ToString() Then
                        comboBoxLayers.Items.Add(fLayer.Name)
                    End If
                    layerArray.Add(fLayer)

                    fLayer = CType(enumLayers.Next(), IFeatureLayer)
                Loop

            Else
                MessageBox.Show("You don't have any line feature layers in your document")
            End If
        End Sub

        Private Sub button_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button_Cancel.Click
            Me.Close()
        End Sub

        Private Sub button_Import_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button_Import.Click
            ' point to the first selected feature:
            Dim AGImportPathOptionsCls As ESRI.ArcGIS.Animation.IAGImportPathOptions = New ESRI.ArcGIS.Animation.AGImportPathOptionsClass()

            ' Set properties for AGImportPathOptions
            AGImportPathOptionsCls.BasicMap = CType(globe, ESRI.ArcGIS.Carto.IBasicMap) ' Explicit Cast
            AGImportPathOptionsCls.AnimationTracks = CType(globe, ESRI.ArcGIS.Animation.IAGAnimationTracks) ' Explicit Cast
            AGImportPathOptionsCls.AnimationType = New ESRI.ArcGIS.GlobeCore.AnimationTypeGlobeCameraClass()
            AGImportPathOptionsCls.AnimatedObject = globe.GlobeDisplay.ActiveViewer.Camera

            If Me.radioButton_flyby.Checked = True Then
                AGImportPathOptionsCls.ConversionType = esriFlyFromPathType.esriFlyFromPathObsAndTarget
                AGImportPathOptionsCls.PutAngleCalculationMethods(esriPathAngleCalculation.esriAngleAddRelative, esriPathAngleCalculation.esriAngleAddRelative, esriPathAngleCalculation.esriAngleAddRelative)
                AGImportPathOptionsCls.PutAngleCalculationValues(0.0, 0.0, 0.0)
            ElseIf Me.radioButton_currentTarget.Checked = True Then
                AGImportPathOptionsCls.ConversionType = esriFlyFromPathType.esriFlyFromPathObserver
            ElseIf Me.radioButton_currentObserver.Checked = True Then
                AGImportPathOptionsCls.ConversionType = esriFlyFromPathType.esriFlyFromPathTarget
            End If

            Dim pAzimuth, pInclination, pRollVal As Double
            AGImportPathOptionsCls.GetAngleCalculationValues(pAzimuth, pInclination, pRollVal)

            AGImportPathOptionsCls.LookaheadFactor = Me.trackBarSimplificationFactor.Value / 100
            AGImportPathOptionsCls.TrackName = Me.textBox_TrackName.Text
            AGImportPathOptionsCls.OverwriteExisting = Convert.ToBoolean(Me.checkBox_Overwrite.CheckState)
            AGImportPathOptionsCls.VerticalOffset = Convert.ToDouble(Me.textBox_VertOffset.Text)
            AGImportPathOptionsCls.ReversePath = Convert.ToBoolean(Me.checkBox_ReverseOrder.CheckState)

            ' get the layer selected in the combo box
            If Me.comboBoxLayers.SelectedIndex = -1 Then
                MessageBox.Show("Please select a layer before you proceed")
            Else
                'set the layer based on the item selected in the combo box
                Dim layer As ESRI.ArcGIS.Carto.ILayer = CType(layerArray.Element(Me.comboBoxLayers.SelectedIndex), ESRI.ArcGIS.Carto.ILayer) ' Explicit Cast

                ' Get the line feature selected in the layer
                Dim featureLayer As ESRI.ArcGIS.Carto.IFeatureLayer = CType(layer, ESRI.ArcGIS.Carto.IFeatureLayer) ' Explicit Cast
                Dim featureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = CType(layer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Explicit Cast
                Dim selectionSet As ESRI.ArcGIS.Geodatabase.ISelectionSet = featureSelection.SelectionSet
                Dim featureClass As ESRI.ArcGIS.Geodatabase.IFeatureClass = featureLayer.FeatureClass
                Dim shapeField As String = featureClass.ShapeFieldName
                Dim spatialFilterCls As ESRI.ArcGIS.Geodatabase.ISpatialFilter = New ESRI.ArcGIS.Geodatabase.SpatialFilterClass()

                Dim scene As IScene = globe.GlobeDisplay.Scene

                Dim spatialReference As ESRI.ArcGIS.Geometry.ISpatialReference = scene.SpatialReference
                spatialFilterCls.GeometryField = shapeField
                spatialReference = spatialFilterCls.OutputSpatialReference(shapeField)
                Dim cursor As ESRI.ArcGIS.Geodatabase.ICursor
                cursor = Nothing
                selectionSet.Search(spatialFilterCls, True, cursor)
                Dim featureCursor As ESRI.ArcGIS.Geodatabase.IFeatureCursor = CType(cursor, ESRI.ArcGIS.Geodatabase.IFeatureCursor) ' Explicit Cast

                Dim lineFeature As ESRI.ArcGIS.Geodatabase.IFeature
                lineFeature = featureCursor.NextFeature()
                If lineFeature Is Nothing Then
                    MessageBox.Show("Please select a feature in the selected layer")
                Else
                    CreateFlybyFromPathAnimation(globe, lineFeature, AGImportPathOptionsCls)
                End If
            End If
            Me.Close()
        End Sub

        Public Sub SetVariables(ByVal pGlobe As IGlobe)
            globe = pGlobe
        End Sub

        Private Sub CreateFlybyFromPathAnimation(ByVal globe As ESRI.ArcGIS.GlobeCore.IGlobe, ByVal lineFeature As ESRI.ArcGIS.Geodatabase.IFeature, ByVal AGImportPathOptionsCls As ESRI.ArcGIS.Animation.IAGImportPathOptions)
            Dim globeDisplay As ESRI.ArcGIS.GlobeCore.IGlobeDisplay = globe.GlobeDisplay
            Dim scene As ESRI.ArcGIS.Analyst3D.IScene = globeDisplay.Scene

            ' Get a handle to the animation extension
            Dim basicScene2 As ESRI.ArcGIS.Analyst3D.IBasicScene2 = CType(scene, ESRI.ArcGIS.Analyst3D.IBasicScene2) ' Explicit Cast
            Dim animationExtension As ESRI.ArcGIS.Animation.IAnimationExtension = basicScene2.AnimationExtension

            ' Get the geometry of the line feature
            Dim geometry As ESRI.ArcGIS.Geometry.IGeometry = lineFeature.Shape

            ' Create AGAnimationUtils and AGImportPathOptions objects
            Dim AGAnimationUtilsCls As ESRI.ArcGIS.Animation.IAGAnimationUtils = New ESRI.ArcGIS.Animation.AGAnimationUtilsClass()
            AGImportPathOptionsCls.PathGeometry = geometry

            AGImportPathOptionsCls.AnimationEnvironment = animationExtension.AnimationEnvironment
            Dim AGAnimationContainer As ESRI.ArcGIS.Animation.IAGAnimationContainer = animationExtension.AnimationTracks.AnimationObjectContainer

            ' Call "CreateFlybyFromPath" method
            AGAnimationUtilsCls.CreateFlybyFromPath(AGAnimationContainer, AGImportPathOptionsCls)
        End Sub

    End Class
End Namespace
