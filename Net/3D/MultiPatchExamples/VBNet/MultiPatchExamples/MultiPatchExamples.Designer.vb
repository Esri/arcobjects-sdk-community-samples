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

Partial Public Class MultiPatchExamples
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (Not components Is Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MultiPatchExamples))
        Me.axSceneControl = New ESRI.ArcGIS.Controls.AxSceneControl()
        Me.axLicenseControl = New ESRI.ArcGIS.Controls.AxLicenseControl()
        Me.triangleStrip1Button = New System.Windows.Forms.Button()
        Me.triangleStrip2Button = New System.Windows.Forms.Button()
        Me.triangleStrip3Button = New System.Windows.Forms.Button()
        Me.triangleStrip4Button = New System.Windows.Forms.Button()
        Me.triangleStrip5Button = New System.Windows.Forms.Button()
        Me.triangleFan1Button = New System.Windows.Forms.Button()
        Me.triangleFan2Button = New System.Windows.Forms.Button()
        Me.triangleFan3Button = New System.Windows.Forms.Button()
        Me.triangleFan4Button = New System.Windows.Forms.Button()
        Me.triangleFan5Button = New System.Windows.Forms.Button()
        Me.triangles1Button = New System.Windows.Forms.Button()
        Me.triangles2Button = New System.Windows.Forms.Button()
        Me.triangles3Button = New System.Windows.Forms.Button()
        Me.triangles4Button = New System.Windows.Forms.Button()
        Me.triangles5Button = New System.Windows.Forms.Button()
        Me.ring1Button = New System.Windows.Forms.Button()
        Me.ring2Button = New System.Windows.Forms.Button()
        Me.ring3Button = New System.Windows.Forms.Button()
        Me.ring4Button = New System.Windows.Forms.Button()
        Me.ring5Button = New System.Windows.Forms.Button()
        Me.vector3D1Button = New System.Windows.Forms.Button()
        Me.vector3D2Button = New System.Windows.Forms.Button()
        Me.vector3D3Button = New System.Windows.Forms.Button()
        Me.transform3D1Button = New System.Windows.Forms.Button()
        Me.transform3D2Button = New System.Windows.Forms.Button()
        Me.transform3D3Button = New System.Windows.Forms.Button()
        Me.transform3D4Button = New System.Windows.Forms.Button()
        Me.extrusion1Button = New System.Windows.Forms.Button()
        Me.extrusion2Button = New System.Windows.Forms.Button()
        Me.extrusion3Button = New System.Windows.Forms.Button()
        Me.extrusion4Button = New System.Windows.Forms.Button()
        Me.extrusion5Button = New System.Windows.Forms.Button()
        Me.extrusion6Button = New System.Windows.Forms.Button()
        Me.extrusion7Button = New System.Windows.Forms.Button()
        Me.ringGroup1Button = New System.Windows.Forms.Button()
        Me.ringGroup2Button = New System.Windows.Forms.Button()
        Me.ringGroup3Button = New System.Windows.Forms.Button()
        Me.ringGroup4Button = New System.Windows.Forms.Button()
        Me.ringGroup5Button = New System.Windows.Forms.Button()
        Me.extrusion8Button = New System.Windows.Forms.Button()
        Me.extrusionButton9 = New System.Windows.Forms.Button()
        Me.extrusion10Button = New System.Windows.Forms.Button()
        Me.extrusion11Button = New System.Windows.Forms.Button()
        Me.extrusion12Button = New System.Windows.Forms.Button()
        Me.extrusion13Button = New System.Windows.Forms.Button()
        Me.triangleStripGroupBox = New System.Windows.Forms.GroupBox()
        Me.triangleFanGroupBox = New System.Windows.Forms.GroupBox()
        Me.triangleFan6Button = New System.Windows.Forms.Button()
        Me.trianglesGroupBox = New System.Windows.Forms.GroupBox()
        Me.triangles6Button = New System.Windows.Forms.Button()
        Me.ringGroupBox = New System.Windows.Forms.GroupBox()
        Me.ringGroupGroupBox = New System.Windows.Forms.GroupBox()
        Me.vector3DGroupBox = New System.Windows.Forms.GroupBox()
        Me.vector3D5Button = New System.Windows.Forms.Button()
        Me.vector3D4Button = New System.Windows.Forms.Button()
        Me.transform3DGroupBox = New System.Windows.Forms.GroupBox()
        Me.extrusionGroupBox = New System.Windows.Forms.GroupBox()
        Me.extrusion15Button = New System.Windows.Forms.Button()
        Me.extrusion14Button = New System.Windows.Forms.Button()
        Me.compositeGroupBox = New System.Windows.Forms.GroupBox()
        Me.composite4Button = New System.Windows.Forms.Button()
        Me.composite3Button = New System.Windows.Forms.Button()
        Me.composite2Button = New System.Windows.Forms.Button()
        Me.composite1Button = New System.Windows.Forms.Button()
        CType(Me.axSceneControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axLicenseControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.triangleStripGroupBox.SuspendLayout()
        Me.triangleFanGroupBox.SuspendLayout()
        Me.trianglesGroupBox.SuspendLayout()
        Me.ringGroupBox.SuspendLayout()
        Me.ringGroupGroupBox.SuspendLayout()
        Me.vector3DGroupBox.SuspendLayout()
        Me.transform3DGroupBox.SuspendLayout()
        Me.extrusionGroupBox.SuspendLayout()
        Me.compositeGroupBox.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' axSceneControl
        ' 
        Me.axSceneControl.Location = New System.Drawing.Point(12, 13)
        Me.axSceneControl.Name = "axSceneControl"
        Me.axSceneControl.OcxState = (CType(resources.GetObject("axSceneControl.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axSceneControl.Size = New System.Drawing.Size(728, 652)
        Me.axSceneControl.TabIndex = 0
        ' 
        ' axLicenseControl
        ' 
        Me.axLicenseControl.Enabled = True
        Me.axLicenseControl.Location = New System.Drawing.Point(698, 626)
        Me.axLicenseControl.Name = "axLicenseControl"
        Me.axLicenseControl.OcxState = (CType(resources.GetObject("axLicenseControl.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axLicenseControl.Size = New System.Drawing.Size(32, 32)
        Me.axLicenseControl.TabIndex = 1
        ' 
        ' triangleStrip1Button
        ' 
        Me.triangleStrip1Button.Location = New System.Drawing.Point(6, 19)
        Me.triangleStrip1Button.Name = "triangleStrip1Button"
        Me.triangleStrip1Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleStrip1Button.TabIndex = 2
        Me.triangleStrip1Button.Text = "1"
        Me.triangleStrip1Button.UseVisualStyleBackColor = True
        '			Me.triangleStrip1Button.Click += New System.EventHandler(Me.triangleStrip1Button_Click);
        ' 
        ' triangleStrip2Button
        ' 
        Me.triangleStrip2Button.Location = New System.Drawing.Point(42, 19)
        Me.triangleStrip2Button.Name = "triangleStrip2Button"
        Me.triangleStrip2Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleStrip2Button.TabIndex = 3
        Me.triangleStrip2Button.Text = "2"
        Me.triangleStrip2Button.UseVisualStyleBackColor = True
        '			Me.triangleStrip2Button.Click += New System.EventHandler(Me.triangleStrip2Button_Click);
        ' 
        ' triangleStrip3Button
        ' 
        Me.triangleStrip3Button.Location = New System.Drawing.Point(78, 19)
        Me.triangleStrip3Button.Name = "triangleStrip3Button"
        Me.triangleStrip3Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleStrip3Button.TabIndex = 4
        Me.triangleStrip3Button.Text = "3"
        Me.triangleStrip3Button.UseVisualStyleBackColor = True
        '			Me.triangleStrip3Button.Click += New System.EventHandler(Me.triangleStrip3Button_Click);
        ' 
        ' triangleStrip4Button
        ' 
        Me.triangleStrip4Button.Location = New System.Drawing.Point(114, 19)
        Me.triangleStrip4Button.Name = "triangleStrip4Button"
        Me.triangleStrip4Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleStrip4Button.TabIndex = 5
        Me.triangleStrip4Button.Text = "4"
        Me.triangleStrip4Button.UseVisualStyleBackColor = True
        '			Me.triangleStrip4Button.Click += New System.EventHandler(Me.triangleStrip4Button_Click);
        ' 
        ' triangleStrip5Button
        ' 
        Me.triangleStrip5Button.Location = New System.Drawing.Point(150, 19)
        Me.triangleStrip5Button.Name = "triangleStrip5Button"
        Me.triangleStrip5Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleStrip5Button.TabIndex = 6
        Me.triangleStrip5Button.Text = "5"
        Me.triangleStrip5Button.UseVisualStyleBackColor = True
        '			Me.triangleStrip5Button.Click += New System.EventHandler(Me.triangleStrip5Button_Click);
        ' 
        ' triangleFan1Button
        ' 
        Me.triangleFan1Button.Location = New System.Drawing.Point(6, 19)
        Me.triangleFan1Button.Name = "triangleFan1Button"
        Me.triangleFan1Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleFan1Button.TabIndex = 7
        Me.triangleFan1Button.Text = "1"
        Me.triangleFan1Button.UseVisualStyleBackColor = True
        '			Me.triangleFan1Button.Click += New System.EventHandler(Me.triangleFan1Button_Click);
        ' 
        ' triangleFan2Button
        ' 
        Me.triangleFan2Button.Location = New System.Drawing.Point(42, 19)
        Me.triangleFan2Button.Name = "triangleFan2Button"
        Me.triangleFan2Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleFan2Button.TabIndex = 8
        Me.triangleFan2Button.Text = "2"
        Me.triangleFan2Button.UseVisualStyleBackColor = True
        '			Me.triangleFan2Button.Click += New System.EventHandler(Me.triangleFan2Button_Click);
        ' 
        ' triangleFan3Button
        ' 
        Me.triangleFan3Button.Location = New System.Drawing.Point(78, 19)
        Me.triangleFan3Button.Name = "triangleFan3Button"
        Me.triangleFan3Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleFan3Button.TabIndex = 9
        Me.triangleFan3Button.Text = "3"
        Me.triangleFan3Button.UseVisualStyleBackColor = True
        '			Me.triangleFan3Button.Click += New System.EventHandler(Me.triangleFan3Button_Click);
        ' 
        ' triangleFan4Button
        ' 
        Me.triangleFan4Button.Location = New System.Drawing.Point(114, 19)
        Me.triangleFan4Button.Name = "triangleFan4Button"
        Me.triangleFan4Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleFan4Button.TabIndex = 10
        Me.triangleFan4Button.Text = "4"
        Me.triangleFan4Button.UseVisualStyleBackColor = True
        '			Me.triangleFan4Button.Click += New System.EventHandler(Me.triangleFan4Button_Click);
        ' 
        ' triangleFan5Button
        ' 
        Me.triangleFan5Button.Location = New System.Drawing.Point(150, 19)
        Me.triangleFan5Button.Name = "triangleFan5Button"
        Me.triangleFan5Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleFan5Button.TabIndex = 11
        Me.triangleFan5Button.Text = "5"
        Me.triangleFan5Button.UseVisualStyleBackColor = True
        '			Me.triangleFan5Button.Click += New System.EventHandler(Me.triangleFan5Button_Click);
        ' 
        ' triangles1Button
        ' 
        Me.triangles1Button.Location = New System.Drawing.Point(6, 19)
        Me.triangles1Button.Name = "triangles1Button"
        Me.triangles1Button.Size = New System.Drawing.Size(30, 30)
        Me.triangles1Button.TabIndex = 12
        Me.triangles1Button.Text = "1"
        Me.triangles1Button.UseVisualStyleBackColor = True
        '			Me.triangles1Button.Click += New System.EventHandler(Me.triangles1Button_Click);
        ' 
        ' triangles2Button
        ' 
        Me.triangles2Button.Location = New System.Drawing.Point(42, 19)
        Me.triangles2Button.Name = "triangles2Button"
        Me.triangles2Button.Size = New System.Drawing.Size(30, 30)
        Me.triangles2Button.TabIndex = 13
        Me.triangles2Button.Text = "2"
        Me.triangles2Button.UseVisualStyleBackColor = True
        '			Me.triangles2Button.Click += New System.EventHandler(Me.triangles2Button_Click);
        ' 
        ' triangles3Button
        ' 
        Me.triangles3Button.Location = New System.Drawing.Point(78, 19)
        Me.triangles3Button.Name = "triangles3Button"
        Me.triangles3Button.Size = New System.Drawing.Size(30, 30)
        Me.triangles3Button.TabIndex = 14
        Me.triangles3Button.Text = "3"
        Me.triangles3Button.UseVisualStyleBackColor = True
        '			Me.triangles3Button.Click += New System.EventHandler(Me.triangles3Button_Click);
        ' 
        ' triangles4Button
        ' 
        Me.triangles4Button.Location = New System.Drawing.Point(114, 19)
        Me.triangles4Button.Name = "triangles4Button"
        Me.triangles4Button.Size = New System.Drawing.Size(30, 30)
        Me.triangles4Button.TabIndex = 15
        Me.triangles4Button.Text = "4"
        Me.triangles4Button.UseVisualStyleBackColor = True
        '			Me.triangles4Button.Click += New System.EventHandler(Me.triangles4Button_Click);
        ' 
        ' triangles5Button
        ' 
        Me.triangles5Button.Location = New System.Drawing.Point(150, 19)
        Me.triangles5Button.Name = "triangles5Button"
        Me.triangles5Button.Size = New System.Drawing.Size(30, 30)
        Me.triangles5Button.TabIndex = 16
        Me.triangles5Button.Text = "5"
        Me.triangles5Button.UseVisualStyleBackColor = True
        '			Me.triangles5Button.Click += New System.EventHandler(Me.triangles5Button_Click);
        ' 
        ' ring1Button
        ' 
        Me.ring1Button.Location = New System.Drawing.Point(6, 19)
        Me.ring1Button.Name = "ring1Button"
        Me.ring1Button.Size = New System.Drawing.Size(30, 30)
        Me.ring1Button.TabIndex = 17
        Me.ring1Button.Text = "1"
        Me.ring1Button.UseVisualStyleBackColor = True
        '			Me.ring1Button.Click += New System.EventHandler(Me.ring1Button_Click);
        ' 
        ' ring2Button
        ' 
        Me.ring2Button.Location = New System.Drawing.Point(42, 19)
        Me.ring2Button.Name = "ring2Button"
        Me.ring2Button.Size = New System.Drawing.Size(30, 30)
        Me.ring2Button.TabIndex = 18
        Me.ring2Button.Text = "2"
        Me.ring2Button.UseVisualStyleBackColor = True
        '			Me.ring2Button.Click += New System.EventHandler(Me.ring2Button_Click);
        ' 
        ' ring3Button
        ' 
        Me.ring3Button.Location = New System.Drawing.Point(78, 19)
        Me.ring3Button.Name = "ring3Button"
        Me.ring3Button.Size = New System.Drawing.Size(30, 30)
        Me.ring3Button.TabIndex = 19
        Me.ring3Button.Text = "3"
        Me.ring3Button.UseVisualStyleBackColor = True
        '			Me.ring3Button.Click += New System.EventHandler(Me.ring3Button_Click);
        ' 
        ' ring4Button
        ' 
        Me.ring4Button.Location = New System.Drawing.Point(114, 19)
        Me.ring4Button.Name = "ring4Button"
        Me.ring4Button.Size = New System.Drawing.Size(30, 30)
        Me.ring4Button.TabIndex = 20
        Me.ring4Button.Text = "4"
        Me.ring4Button.UseVisualStyleBackColor = True
        '			Me.ring4Button.Click += New System.EventHandler(Me.ring4Button_Click);
        ' 
        ' ring5Button
        ' 
        Me.ring5Button.Location = New System.Drawing.Point(150, 19)
        Me.ring5Button.Name = "ring5Button"
        Me.ring5Button.Size = New System.Drawing.Size(30, 30)
        Me.ring5Button.TabIndex = 21
        Me.ring5Button.Text = "5"
        Me.ring5Button.UseVisualStyleBackColor = True
        '			Me.ring5Button.Click += New System.EventHandler(Me.ring5Button_Click);
        ' 
        ' vector3D1Button
        ' 
        Me.vector3D1Button.Location = New System.Drawing.Point(6, 19)
        Me.vector3D1Button.Name = "vector3D1Button"
        Me.vector3D1Button.Size = New System.Drawing.Size(30, 30)
        Me.vector3D1Button.TabIndex = 22
        Me.vector3D1Button.Text = "1"
        Me.vector3D1Button.UseVisualStyleBackColor = True
        '			Me.vector3D1Button.Click += New System.EventHandler(Me.vector3D1Button_Click);
        ' 
        ' vector3D2Button
        ' 
        Me.vector3D2Button.Location = New System.Drawing.Point(42, 19)
        Me.vector3D2Button.Name = "vector3D2Button"
        Me.vector3D2Button.Size = New System.Drawing.Size(30, 30)
        Me.vector3D2Button.TabIndex = 23
        Me.vector3D2Button.Text = "2"
        Me.vector3D2Button.UseVisualStyleBackColor = True
        '			Me.vector3D2Button.Click += New System.EventHandler(Me.vector3D2Button_Click);
        ' 
        ' vector3D3Button
        ' 
        Me.vector3D3Button.Location = New System.Drawing.Point(78, 19)
        Me.vector3D3Button.Name = "vector3D3Button"
        Me.vector3D3Button.Size = New System.Drawing.Size(30, 30)
        Me.vector3D3Button.TabIndex = 24
        Me.vector3D3Button.Text = "3"
        Me.vector3D3Button.UseVisualStyleBackColor = True
        '			Me.vector3D3Button.Click += New System.EventHandler(Me.vector3D3Button_Click);
        ' 
        ' transform3D1Button
        ' 
        Me.transform3D1Button.Location = New System.Drawing.Point(6, 19)
        Me.transform3D1Button.Name = "transform3D1Button"
        Me.transform3D1Button.Size = New System.Drawing.Size(30, 30)
        Me.transform3D1Button.TabIndex = 25
        Me.transform3D1Button.Text = "1"
        Me.transform3D1Button.UseVisualStyleBackColor = True
        '			Me.transform3D1Button.Click += New System.EventHandler(Me.transform3D1Button_Click);
        ' 
        ' transform3D2Button
        ' 
        Me.transform3D2Button.Location = New System.Drawing.Point(42, 19)
        Me.transform3D2Button.Name = "transform3D2Button"
        Me.transform3D2Button.Size = New System.Drawing.Size(30, 30)
        Me.transform3D2Button.TabIndex = 26
        Me.transform3D2Button.Text = "2"
        Me.transform3D2Button.UseVisualStyleBackColor = True
        '			Me.transform3D2Button.Click += New System.EventHandler(Me.transform3D2Button_Click);
        ' 
        ' transform3D3Button
        ' 
        Me.transform3D3Button.Location = New System.Drawing.Point(78, 19)
        Me.transform3D3Button.Name = "transform3D3Button"
        Me.transform3D3Button.Size = New System.Drawing.Size(30, 30)
        Me.transform3D3Button.TabIndex = 27
        Me.transform3D3Button.Text = "3"
        Me.transform3D3Button.UseVisualStyleBackColor = True
        '			Me.transform3D3Button.Click += New System.EventHandler(Me.transform3D3Button_Click);
        ' 
        ' transform3D4Button
        ' 
        Me.transform3D4Button.Location = New System.Drawing.Point(114, 19)
        Me.transform3D4Button.Name = "transform3D4Button"
        Me.transform3D4Button.Size = New System.Drawing.Size(30, 30)
        Me.transform3D4Button.TabIndex = 28
        Me.transform3D4Button.Text = "4"
        Me.transform3D4Button.UseVisualStyleBackColor = True
        '			Me.transform3D4Button.Click += New System.EventHandler(Me.transform3D4Button_Click);
        ' 
        ' extrusion1Button
        ' 
        Me.extrusion1Button.Location = New System.Drawing.Point(6, 19)
        Me.extrusion1Button.Name = "extrusion1Button"
        Me.extrusion1Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion1Button.TabIndex = 29
        Me.extrusion1Button.Text = "1"
        Me.extrusion1Button.UseVisualStyleBackColor = True
        '			Me.extrusion1Button.Click += New System.EventHandler(Me.extrusion1Button_Click);
        ' 
        ' extrusion2Button
        ' 
        Me.extrusion2Button.Location = New System.Drawing.Point(42, 19)
        Me.extrusion2Button.Name = "extrusion2Button"
        Me.extrusion2Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion2Button.TabIndex = 30
        Me.extrusion2Button.Text = "2"
        Me.extrusion2Button.UseVisualStyleBackColor = True
        '			Me.extrusion2Button.Click += New System.EventHandler(Me.extrusion2Button_Click);
        ' 
        ' extrusion3Button
        ' 
        Me.extrusion3Button.Location = New System.Drawing.Point(78, 19)
        Me.extrusion3Button.Name = "extrusion3Button"
        Me.extrusion3Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion3Button.TabIndex = 31
        Me.extrusion3Button.Text = "3"
        Me.extrusion3Button.UseVisualStyleBackColor = True
        '			Me.extrusion3Button.Click += New System.EventHandler(Me.extrusion3Button_Click);
        ' 
        ' extrusion4Button
        ' 
        Me.extrusion4Button.Location = New System.Drawing.Point(114, 19)
        Me.extrusion4Button.Name = "extrusion4Button"
        Me.extrusion4Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion4Button.TabIndex = 32
        Me.extrusion4Button.Text = "4"
        Me.extrusion4Button.UseVisualStyleBackColor = True
        '			Me.extrusion4Button.Click += New System.EventHandler(Me.extrusion4Button_Click);
        ' 
        ' extrusion5Button
        ' 
        Me.extrusion5Button.Location = New System.Drawing.Point(150, 19)
        Me.extrusion5Button.Name = "extrusion5Button"
        Me.extrusion5Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion5Button.TabIndex = 33
        Me.extrusion5Button.Text = "5"
        Me.extrusion5Button.UseVisualStyleBackColor = True
        '			Me.extrusion5Button.Click += New System.EventHandler(Me.extrusion5Button_Click);
        ' 
        ' extrusion6Button
        ' 
        Me.extrusion6Button.Location = New System.Drawing.Point(186, 19)
        Me.extrusion6Button.Name = "extrusion6Button"
        Me.extrusion6Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion6Button.TabIndex = 34
        Me.extrusion6Button.Text = "6"
        Me.extrusion6Button.UseVisualStyleBackColor = True
        '			Me.extrusion6Button.Click += New System.EventHandler(Me.extrusion6Button_Click);
        ' 
        ' extrusion7Button
        ' 
        Me.extrusion7Button.Location = New System.Drawing.Point(6, 55)
        Me.extrusion7Button.Name = "extrusion7Button"
        Me.extrusion7Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion7Button.TabIndex = 35
        Me.extrusion7Button.Text = "7"
        Me.extrusion7Button.UseVisualStyleBackColor = True
        '			Me.extrusion7Button.Click += New System.EventHandler(Me.extrusion7Button_Click);
        ' 
        ' ringGroup1Button
        ' 
        Me.ringGroup1Button.Location = New System.Drawing.Point(6, 19)
        Me.ringGroup1Button.Name = "ringGroup1Button"
        Me.ringGroup1Button.Size = New System.Drawing.Size(30, 30)
        Me.ringGroup1Button.TabIndex = 36
        Me.ringGroup1Button.Text = "1"
        Me.ringGroup1Button.UseVisualStyleBackColor = True
        '			Me.ringGroup1Button.Click += New System.EventHandler(Me.ringGroup1Button_Click);
        ' 
        ' ringGroup2Button
        ' 
        Me.ringGroup2Button.Location = New System.Drawing.Point(42, 19)
        Me.ringGroup2Button.Name = "ringGroup2Button"
        Me.ringGroup2Button.Size = New System.Drawing.Size(30, 30)
        Me.ringGroup2Button.TabIndex = 37
        Me.ringGroup2Button.Text = "2"
        Me.ringGroup2Button.UseVisualStyleBackColor = True
        '			Me.ringGroup2Button.Click += New System.EventHandler(Me.ringGroup2Button_Click);
        ' 
        ' ringGroup3Button
        ' 
        Me.ringGroup3Button.Location = New System.Drawing.Point(78, 19)
        Me.ringGroup3Button.Name = "ringGroup3Button"
        Me.ringGroup3Button.Size = New System.Drawing.Size(30, 30)
        Me.ringGroup3Button.TabIndex = 38
        Me.ringGroup3Button.Text = "3"
        Me.ringGroup3Button.UseVisualStyleBackColor = True
        '			Me.ringGroup3Button.Click += New System.EventHandler(Me.ringGroup3Button_Click);
        ' 
        ' ringGroup4Button
        ' 
        Me.ringGroup4Button.Location = New System.Drawing.Point(114, 19)
        Me.ringGroup4Button.Name = "ringGroup4Button"
        Me.ringGroup4Button.Size = New System.Drawing.Size(30, 30)
        Me.ringGroup4Button.TabIndex = 39
        Me.ringGroup4Button.Text = "4"
        Me.ringGroup4Button.UseVisualStyleBackColor = True
        '			Me.ringGroup4Button.Click += New System.EventHandler(Me.ringGroup4Button_Click);
        ' 
        ' ringGroup5Button
        ' 
        Me.ringGroup5Button.Location = New System.Drawing.Point(150, 19)
        Me.ringGroup5Button.Name = "ringGroup5Button"
        Me.ringGroup5Button.Size = New System.Drawing.Size(30, 30)
        Me.ringGroup5Button.TabIndex = 40
        Me.ringGroup5Button.Text = "5"
        Me.ringGroup5Button.UseVisualStyleBackColor = True
        '			Me.ringGroup5Button.Click += New System.EventHandler(Me.ringGroup5Button_Click);
        ' 
        ' extrusion8Button
        ' 
        Me.extrusion8Button.Location = New System.Drawing.Point(42, 55)
        Me.extrusion8Button.Name = "extrusion8Button"
        Me.extrusion8Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion8Button.TabIndex = 41
        Me.extrusion8Button.Text = "8"
        Me.extrusion8Button.UseVisualStyleBackColor = True
        '			Me.extrusion8Button.Click += New System.EventHandler(Me.extrusion8Button_Click);
        ' 
        ' extrusionButton9
        ' 
        Me.extrusionButton9.Location = New System.Drawing.Point(78, 55)
        Me.extrusionButton9.Name = "extrusionButton9"
        Me.extrusionButton9.Size = New System.Drawing.Size(30, 30)
        Me.extrusionButton9.TabIndex = 42
        Me.extrusionButton9.Text = "9"
        Me.extrusionButton9.UseVisualStyleBackColor = True
        '			Me.extrusionButton9.Click += New System.EventHandler(Me.extrusionButton9_Click);
        ' 
        ' extrusion10Button
        ' 
        Me.extrusion10Button.Location = New System.Drawing.Point(114, 55)
        Me.extrusion10Button.Name = "extrusion10Button"
        Me.extrusion10Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion10Button.TabIndex = 43
        Me.extrusion10Button.Text = "10"
        Me.extrusion10Button.UseVisualStyleBackColor = True
        '			Me.extrusion10Button.Click += New System.EventHandler(Me.extrusion10Button_Click);
        ' 
        ' extrusion11Button
        ' 
        Me.extrusion11Button.Location = New System.Drawing.Point(150, 55)
        Me.extrusion11Button.Name = "extrusion11Button"
        Me.extrusion11Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion11Button.TabIndex = 44
        Me.extrusion11Button.Text = "11"
        Me.extrusion11Button.UseVisualStyleBackColor = True
        '			Me.extrusion11Button.Click += New System.EventHandler(Me.extrusion11Button_Click);
        ' 
        ' extrusion12Button
        ' 
        Me.extrusion12Button.Location = New System.Drawing.Point(186, 55)
        Me.extrusion12Button.Name = "extrusion12Button"
        Me.extrusion12Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion12Button.TabIndex = 45
        Me.extrusion12Button.Text = "12"
        Me.extrusion12Button.UseVisualStyleBackColor = True
        '			Me.extrusion12Button.Click += New System.EventHandler(Me.extrusion12Button_Click);
        ' 
        ' extrusion13Button
        ' 
        Me.extrusion13Button.Location = New System.Drawing.Point(6, 91)
        Me.extrusion13Button.Name = "extrusion13Button"
        Me.extrusion13Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion13Button.TabIndex = 46
        Me.extrusion13Button.Text = "13"
        Me.extrusion13Button.UseVisualStyleBackColor = True
        '			Me.extrusion13Button.Click += New System.EventHandler(Me.extrusion13Button_Click);
        ' 
        ' triangleStripGroupBox
        ' 
        Me.triangleStripGroupBox.Controls.Add(Me.triangleStrip1Button)
        Me.triangleStripGroupBox.Controls.Add(Me.triangleStrip2Button)
        Me.triangleStripGroupBox.Controls.Add(Me.triangleStrip3Button)
        Me.triangleStripGroupBox.Controls.Add(Me.triangleStrip4Button)
        Me.triangleStripGroupBox.Controls.Add(Me.triangleStrip5Button)
        Me.triangleStripGroupBox.Location = New System.Drawing.Point(746, 13)
        Me.triangleStripGroupBox.Name = "triangleStripGroupBox"
        Me.triangleStripGroupBox.Size = New System.Drawing.Size(223, 59)
        Me.triangleStripGroupBox.TabIndex = 47
        Me.triangleStripGroupBox.TabStop = False
        Me.triangleStripGroupBox.Text = "TriangleStrip"
        ' 
        ' triangleFanGroupBox
        ' 
        Me.triangleFanGroupBox.Controls.Add(Me.triangleFan6Button)
        Me.triangleFanGroupBox.Controls.Add(Me.triangleFan1Button)
        Me.triangleFanGroupBox.Controls.Add(Me.triangleFan2Button)
        Me.triangleFanGroupBox.Controls.Add(Me.triangleFan3Button)
        Me.triangleFanGroupBox.Controls.Add(Me.triangleFan4Button)
        Me.triangleFanGroupBox.Controls.Add(Me.triangleFan5Button)
        Me.triangleFanGroupBox.Location = New System.Drawing.Point(746, 78)
        Me.triangleFanGroupBox.Name = "triangleFanGroupBox"
        Me.triangleFanGroupBox.Size = New System.Drawing.Size(223, 59)
        Me.triangleFanGroupBox.TabIndex = 48
        Me.triangleFanGroupBox.TabStop = False
        Me.triangleFanGroupBox.Text = "TriangleFan"
        ' 
        ' triangleFan6Button
        ' 
        Me.triangleFan6Button.Location = New System.Drawing.Point(186, 19)
        Me.triangleFan6Button.Name = "triangleFan6Button"
        Me.triangleFan6Button.Size = New System.Drawing.Size(30, 30)
        Me.triangleFan6Button.TabIndex = 12
        Me.triangleFan6Button.Text = "6"
        Me.triangleFan6Button.UseVisualStyleBackColor = True
        '			Me.triangleFan6Button.Click += New System.EventHandler(Me.triangleFan6Button_Click);
        ' 
        ' trianglesGroupBox
        ' 
        Me.trianglesGroupBox.Controls.Add(Me.triangles6Button)
        Me.trianglesGroupBox.Controls.Add(Me.triangles1Button)
        Me.trianglesGroupBox.Controls.Add(Me.triangles2Button)
        Me.trianglesGroupBox.Controls.Add(Me.triangles3Button)
        Me.trianglesGroupBox.Controls.Add(Me.triangles4Button)
        Me.trianglesGroupBox.Controls.Add(Me.triangles5Button)
        Me.trianglesGroupBox.Location = New System.Drawing.Point(746, 143)
        Me.trianglesGroupBox.Name = "trianglesGroupBox"
        Me.trianglesGroupBox.Size = New System.Drawing.Size(223, 59)
        Me.trianglesGroupBox.TabIndex = 49
        Me.trianglesGroupBox.TabStop = False
        Me.trianglesGroupBox.Text = "Triangles"
        ' 
        ' triangles6Button
        ' 
        Me.triangles6Button.Location = New System.Drawing.Point(186, 19)
        Me.triangles6Button.Name = "triangles6Button"
        Me.triangles6Button.Size = New System.Drawing.Size(30, 30)
        Me.triangles6Button.TabIndex = 17
        Me.triangles6Button.Text = "6"
        Me.triangles6Button.UseVisualStyleBackColor = True
        '			Me.triangles6Button.Click += New System.EventHandler(Me.triangles6Button_Click);
        ' 
        ' ringGroupBox
        ' 
        Me.ringGroupBox.Controls.Add(Me.ring1Button)
        Me.ringGroupBox.Controls.Add(Me.ring2Button)
        Me.ringGroupBox.Controls.Add(Me.ring3Button)
        Me.ringGroupBox.Controls.Add(Me.ring4Button)
        Me.ringGroupBox.Controls.Add(Me.ring5Button)
        Me.ringGroupBox.Location = New System.Drawing.Point(746, 208)
        Me.ringGroupBox.Name = "ringGroupBox"
        Me.ringGroupBox.Size = New System.Drawing.Size(223, 59)
        Me.ringGroupBox.TabIndex = 50
        Me.ringGroupBox.TabStop = False
        Me.ringGroupBox.Text = "Ring"
        ' 
        ' ringGroupGroupBox
        ' 
        Me.ringGroupGroupBox.Controls.Add(Me.ringGroup1Button)
        Me.ringGroupGroupBox.Controls.Add(Me.ringGroup2Button)
        Me.ringGroupGroupBox.Controls.Add(Me.ringGroup3Button)
        Me.ringGroupGroupBox.Controls.Add(Me.ringGroup4Button)
        Me.ringGroupGroupBox.Controls.Add(Me.ringGroup5Button)
        Me.ringGroupGroupBox.Location = New System.Drawing.Point(746, 273)
        Me.ringGroupGroupBox.Name = "ringGroupGroupBox"
        Me.ringGroupGroupBox.Size = New System.Drawing.Size(223, 59)
        Me.ringGroupGroupBox.TabIndex = 51
        Me.ringGroupGroupBox.TabStop = False
        Me.ringGroupGroupBox.Text = "RingGroup"
        ' 
        ' vector3DGroupBox
        ' 
        Me.vector3DGroupBox.Controls.Add(Me.vector3D5Button)
        Me.vector3DGroupBox.Controls.Add(Me.vector3D4Button)
        Me.vector3DGroupBox.Controls.Add(Me.vector3D1Button)
        Me.vector3DGroupBox.Controls.Add(Me.vector3D2Button)
        Me.vector3DGroupBox.Controls.Add(Me.vector3D3Button)
        Me.vector3DGroupBox.Location = New System.Drawing.Point(746, 338)
        Me.vector3DGroupBox.Name = "vector3DGroupBox"
        Me.vector3DGroupBox.Size = New System.Drawing.Size(223, 59)
        Me.vector3DGroupBox.TabIndex = 52
        Me.vector3DGroupBox.TabStop = False
        Me.vector3DGroupBox.Text = "Vector3D"
        ' 
        ' vector3D5Button
        ' 
        Me.vector3D5Button.Location = New System.Drawing.Point(150, 19)
        Me.vector3D5Button.Name = "vector3D5Button"
        Me.vector3D5Button.Size = New System.Drawing.Size(30, 30)
        Me.vector3D5Button.TabIndex = 57
        Me.vector3D5Button.Text = "5"
        Me.vector3D5Button.UseVisualStyleBackColor = True
        '			Me.vector3D5Button.Click += New System.EventHandler(Me.vector3D5Button_Click);
        ' 
        ' vector3D4Button
        ' 
        Me.vector3D4Button.Location = New System.Drawing.Point(114, 19)
        Me.vector3D4Button.Name = "vector3D4Button"
        Me.vector3D4Button.Size = New System.Drawing.Size(30, 30)
        Me.vector3D4Button.TabIndex = 25
        Me.vector3D4Button.Text = "4"
        Me.vector3D4Button.UseVisualStyleBackColor = True
        '			Me.vector3D4Button.Click += New System.EventHandler(Me.vector3D4Button_Click);
        ' 
        ' transform3DGroupBox
        ' 
        Me.transform3DGroupBox.Controls.Add(Me.transform3D1Button)
        Me.transform3DGroupBox.Controls.Add(Me.transform3D2Button)
        Me.transform3DGroupBox.Controls.Add(Me.transform3D3Button)
        Me.transform3DGroupBox.Controls.Add(Me.transform3D4Button)
        Me.transform3DGroupBox.Location = New System.Drawing.Point(746, 405)
        Me.transform3DGroupBox.Name = "transform3DGroupBox"
        Me.transform3DGroupBox.Size = New System.Drawing.Size(223, 59)
        Me.transform3DGroupBox.TabIndex = 53
        Me.transform3DGroupBox.TabStop = False
        Me.transform3DGroupBox.Text = "Transform3D"
        ' 
        ' extrusionGroupBox
        ' 
        Me.extrusionGroupBox.Controls.Add(Me.extrusion15Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion14Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion1Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion2Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion3Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion4Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion5Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion6Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion7Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion8Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion13Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusionButton9)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion12Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion10Button)
        Me.extrusionGroupBox.Controls.Add(Me.extrusion11Button)
        Me.extrusionGroupBox.Location = New System.Drawing.Point(746, 470)
        Me.extrusionGroupBox.Name = "extrusionGroupBox"
        Me.extrusionGroupBox.Size = New System.Drawing.Size(223, 131)
        Me.extrusionGroupBox.TabIndex = 54
        Me.extrusionGroupBox.TabStop = False
        Me.extrusionGroupBox.Text = "Extrusion"
        ' 
        ' extrusion15Button
        ' 
        Me.extrusion15Button.Location = New System.Drawing.Point(78, 91)
        Me.extrusion15Button.Name = "extrusion15Button"
        Me.extrusion15Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion15Button.TabIndex = 48
        Me.extrusion15Button.Text = "15"
        Me.extrusion15Button.UseVisualStyleBackColor = True
        '			Me.extrusion15Button.Click += New System.EventHandler(Me.extrusion15Button_Click);
        ' 
        ' extrusion14Button
        ' 
        Me.extrusion14Button.Location = New System.Drawing.Point(42, 91)
        Me.extrusion14Button.Name = "extrusion14Button"
        Me.extrusion14Button.Size = New System.Drawing.Size(30, 30)
        Me.extrusion14Button.TabIndex = 47
        Me.extrusion14Button.Text = "14"
        Me.extrusion14Button.UseVisualStyleBackColor = True
        '			Me.extrusion14Button.Click += New System.EventHandler(Me.extrusion14Button_Click);
        ' 
        ' compositeGroupBox
        ' 
        Me.compositeGroupBox.Controls.Add(Me.composite4Button)
        Me.compositeGroupBox.Controls.Add(Me.composite3Button)
        Me.compositeGroupBox.Controls.Add(Me.composite2Button)
        Me.compositeGroupBox.Controls.Add(Me.composite1Button)
        Me.compositeGroupBox.Location = New System.Drawing.Point(746, 607)
        Me.compositeGroupBox.Name = "compositeGroupBox"
        Me.compositeGroupBox.Size = New System.Drawing.Size(223, 59)
        Me.compositeGroupBox.TabIndex = 55
        Me.compositeGroupBox.TabStop = False
        Me.compositeGroupBox.Text = "Composite"
        ' 
        ' composite4Button
        ' 
        Me.composite4Button.Location = New System.Drawing.Point(114, 19)
        Me.composite4Button.Name = "composite4Button"
        Me.composite4Button.Size = New System.Drawing.Size(30, 30)
        Me.composite4Button.TabIndex = 58
        Me.composite4Button.Text = "4"
        Me.composite4Button.UseVisualStyleBackColor = True
        '			Me.composite4Button.Click += New System.EventHandler(Me.composite4Button_Click);
        ' 
        ' composite3Button
        ' 
        Me.composite3Button.Location = New System.Drawing.Point(78, 19)
        Me.composite3Button.Name = "composite3Button"
        Me.composite3Button.Size = New System.Drawing.Size(30, 30)
        Me.composite3Button.TabIndex = 57
        Me.composite3Button.Text = "3"
        Me.composite3Button.UseVisualStyleBackColor = True
        '			Me.composite3Button.Click += New System.EventHandler(Me.composite3Button_Click);
        ' 
        ' composite2Button
        ' 
        Me.composite2Button.Location = New System.Drawing.Point(42, 19)
        Me.composite2Button.Name = "composite2Button"
        Me.composite2Button.Size = New System.Drawing.Size(30, 30)
        Me.composite2Button.TabIndex = 57
        Me.composite2Button.Text = "2"
        Me.composite2Button.UseVisualStyleBackColor = True
        '			Me.composite2Button.Click += New System.EventHandler(Me.composite2Button_Click);
        ' 
        ' composite1Button
        ' 
        Me.composite1Button.Location = New System.Drawing.Point(6, 19)
        Me.composite1Button.Name = "composite1Button"
        Me.composite1Button.Size = New System.Drawing.Size(30, 30)
        Me.composite1Button.TabIndex = 56
        Me.composite1Button.Text = "1"
        Me.composite1Button.UseVisualStyleBackColor = True
        '			Me.composite1Button.Click += New System.EventHandler(Me.composite1Button_Click);
        ' 
        ' MultiPatchExamples
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(981, 677)
        Me.Controls.Add(Me.compositeGroupBox)
        Me.Controls.Add(Me.extrusionGroupBox)
        Me.Controls.Add(Me.transform3DGroupBox)
        Me.Controls.Add(Me.vector3DGroupBox)
        Me.Controls.Add(Me.ringGroupGroupBox)
        Me.Controls.Add(Me.ringGroupBox)
        Me.Controls.Add(Me.trianglesGroupBox)
        Me.Controls.Add(Me.triangleFanGroupBox)
        Me.Controls.Add(Me.triangleStripGroupBox)
        Me.Controls.Add(Me.axLicenseControl)
        Me.Controls.Add(Me.axSceneControl)
        Me.MaximizeBox = False
        Me.Name = "MultiPatchExamples"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MultiPatch Examples"
        Me.TopMost = True
        CType(Me.axSceneControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axLicenseControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.triangleStripGroupBox.ResumeLayout(False)
        Me.triangleFanGroupBox.ResumeLayout(False)
        Me.trianglesGroupBox.ResumeLayout(False)
        Me.ringGroupBox.ResumeLayout(False)
        Me.ringGroupGroupBox.ResumeLayout(False)
        Me.vector3DGroupBox.ResumeLayout(False)
        Me.transform3DGroupBox.ResumeLayout(False)
        Me.extrusionGroupBox.ResumeLayout(False)
        Me.compositeGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private axSceneControl As ESRI.ArcGIS.Controls.AxSceneControl
    Private axLicenseControl As ESRI.ArcGIS.Controls.AxLicenseControl
    Private WithEvents triangleStrip1Button As System.Windows.Forms.Button
    Private WithEvents triangleStrip2Button As System.Windows.Forms.Button
    Private WithEvents triangleStrip3Button As System.Windows.Forms.Button
    Private WithEvents triangleStrip4Button As System.Windows.Forms.Button
    Private WithEvents triangleStrip5Button As System.Windows.Forms.Button
    Private WithEvents triangleFan1Button As System.Windows.Forms.Button
    Private WithEvents triangleFan2Button As System.Windows.Forms.Button
    Private WithEvents triangleFan3Button As System.Windows.Forms.Button
    Private WithEvents triangleFan4Button As System.Windows.Forms.Button
    Private WithEvents triangleFan5Button As System.Windows.Forms.Button
    Private WithEvents triangles1Button As System.Windows.Forms.Button
    Private WithEvents triangles2Button As System.Windows.Forms.Button
    Private WithEvents triangles3Button As System.Windows.Forms.Button
    Private WithEvents triangles4Button As System.Windows.Forms.Button
    Private WithEvents triangles5Button As System.Windows.Forms.Button
    Private WithEvents ring1Button As System.Windows.Forms.Button
    Private WithEvents ring2Button As System.Windows.Forms.Button
    Private WithEvents ring3Button As System.Windows.Forms.Button
    Private WithEvents ring4Button As System.Windows.Forms.Button
    Private WithEvents ring5Button As System.Windows.Forms.Button
    Private WithEvents vector3D1Button As System.Windows.Forms.Button
    Private WithEvents vector3D2Button As System.Windows.Forms.Button
    Private WithEvents vector3D3Button As System.Windows.Forms.Button
    Private WithEvents transform3D1Button As System.Windows.Forms.Button
    Private WithEvents transform3D2Button As System.Windows.Forms.Button
    Private WithEvents transform3D3Button As System.Windows.Forms.Button
    Private WithEvents transform3D4Button As System.Windows.Forms.Button
    Private WithEvents extrusion1Button As System.Windows.Forms.Button
    Private WithEvents extrusion2Button As System.Windows.Forms.Button
    Private WithEvents extrusion3Button As System.Windows.Forms.Button
    Private WithEvents extrusion4Button As System.Windows.Forms.Button
    Private WithEvents extrusion5Button As System.Windows.Forms.Button
    Private WithEvents extrusion6Button As System.Windows.Forms.Button
    Private WithEvents extrusion7Button As System.Windows.Forms.Button
    Private WithEvents ringGroup1Button As System.Windows.Forms.Button
    Private WithEvents ringGroup2Button As System.Windows.Forms.Button
    Private WithEvents ringGroup3Button As System.Windows.Forms.Button
    Private WithEvents ringGroup4Button As System.Windows.Forms.Button
    Private WithEvents ringGroup5Button As System.Windows.Forms.Button
    Private WithEvents extrusion8Button As System.Windows.Forms.Button
    Private WithEvents extrusionButton9 As System.Windows.Forms.Button
    Private WithEvents extrusion10Button As System.Windows.Forms.Button
    Private WithEvents extrusion11Button As System.Windows.Forms.Button
    Private WithEvents extrusion12Button As System.Windows.Forms.Button
    Private WithEvents extrusion13Button As System.Windows.Forms.Button
    Private triangleStripGroupBox As System.Windows.Forms.GroupBox
    Private triangleFanGroupBox As System.Windows.Forms.GroupBox
    Private trianglesGroupBox As System.Windows.Forms.GroupBox
    Private ringGroupBox As System.Windows.Forms.GroupBox
    Private ringGroupGroupBox As System.Windows.Forms.GroupBox
    Private vector3DGroupBox As System.Windows.Forms.GroupBox
    Private transform3DGroupBox As System.Windows.Forms.GroupBox
    Private extrusionGroupBox As System.Windows.Forms.GroupBox
    Private compositeGroupBox As System.Windows.Forms.GroupBox
    Private WithEvents composite3Button As System.Windows.Forms.Button
    Private WithEvents composite2Button As System.Windows.Forms.Button
    Private WithEvents composite1Button As System.Windows.Forms.Button
    Private WithEvents composite4Button As System.Windows.Forms.Button
    Private WithEvents vector3D5Button As System.Windows.Forms.Button
    Private WithEvents vector3D4Button As System.Windows.Forms.Button
    Private WithEvents triangles6Button As System.Windows.Forms.Button
    Private WithEvents triangleFan6Button As System.Windows.Forms.Button
    Private WithEvents extrusion14Button As System.Windows.Forms.Button
    Private WithEvents extrusion15Button As System.Windows.Forms.Button

End Class
