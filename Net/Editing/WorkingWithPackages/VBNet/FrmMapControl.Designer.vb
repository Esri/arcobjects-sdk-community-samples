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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMapControl
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMapControl))
        Me.txtWebMap = New System.Windows.Forms.TextBox()
        Me.btnWebMap = New System.Windows.Forms.Button()
        Me.txtMapPackage = New System.Windows.Forms.TextBox()
        Me.txtLayerPackage = New System.Windows.Forms.TextBox()
        Me.btnLoadmpk = New System.Windows.Forms.Button()
        Me.btnLoadlpk = New System.Windows.Forms.Button()
        Me.axLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl()
        Me.axToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl()
        Me.axTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl()
        Me.axMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl()
        CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtWebMap
        '
        Me.txtWebMap.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWebMap.Location = New System.Drawing.Point(149, 470)
        Me.txtWebMap.Name = "txtWebMap"
        Me.txtWebMap.Size = New System.Drawing.Size(504, 20)
        Me.txtWebMap.TabIndex = 15
        Me.txtWebMap.Text = "http://www.arcgis.com/sharing/content/items/931d892ac7a843d7ba29d085e0433465/item" & _
    ".pkinfo"
        '
        'btnWebMap
        '
        Me.btnWebMap.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnWebMap.Location = New System.Drawing.Point(11, 456)
        Me.btnWebMap.Name = "btnWebMap"
        Me.btnWebMap.Size = New System.Drawing.Size(130, 34)
        Me.btnWebMap.TabIndex = 14
        Me.btnWebMap.Text = "Load Web Map"
        Me.btnWebMap.UseVisualStyleBackColor = True
        '
        'txtMapPackage
        '
        Me.txtMapPackage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMapPackage.Location = New System.Drawing.Point(149, 430)
        Me.txtMapPackage.Name = "txtMapPackage"
        Me.txtMapPackage.Size = New System.Drawing.Size(504, 20)
        Me.txtMapPackage.TabIndex = 13
        Me.txtMapPackage.Text = "http://www.arcgis.com/sharing/content/items/326babea97ba4ab79e4292904e0478cc/item" & _
    ".pkinfo"
        '
        'txtLayerPackage
        '
        Me.txtLayerPackage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLayerPackage.Location = New System.Drawing.Point(149, 389)
        Me.txtLayerPackage.Name = "txtLayerPackage"
        Me.txtLayerPackage.Size = New System.Drawing.Size(504, 20)
        Me.txtLayerPackage.TabIndex = 12
        Me.txtLayerPackage.Text = "http://www.arcgis.com/sharing/content/items/483b230c56a44c33beb13f9b9ab9f88d/item" & _
    ".pkinfo"
        '
        'btnLoadmpk
        '
        Me.btnLoadmpk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoadmpk.Location = New System.Drawing.Point(11, 416)
        Me.btnLoadmpk.Name = "btnLoadmpk"
        Me.btnLoadmpk.Size = New System.Drawing.Size(130, 34)
        Me.btnLoadmpk.TabIndex = 11
        Me.btnLoadmpk.Text = "Load Map Package"
        Me.btnLoadmpk.UseVisualStyleBackColor = True
        '
        'btnLoadlpk
        '
        Me.btnLoadlpk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoadlpk.Location = New System.Drawing.Point(12, 376)
        Me.btnLoadlpk.Name = "btnLoadlpk"
        Me.btnLoadlpk.Size = New System.Drawing.Size(130, 34)
        Me.btnLoadlpk.TabIndex = 10
        Me.btnLoadlpk.Text = "Load Layer Package"
        Me.btnLoadlpk.UseVisualStyleBackColor = True
        '
        'axLicenseControl1
        '
        Me.axLicenseControl1.Enabled = True
        Me.axLicenseControl1.Location = New System.Drawing.Point(236, 138)
        Me.axLicenseControl1.Name = "axLicenseControl1"
        Me.axLicenseControl1.OcxState = CType(resources.GetObject("axLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.axLicenseControl1.TabIndex = 19
        '
        'axToolbarControl1
        '
        Me.axToolbarControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axToolbarControl1.Location = New System.Drawing.Point(12, 11)
        Me.axToolbarControl1.Name = "axToolbarControl1"
        Me.axToolbarControl1.OcxState = CType(resources.GetObject("axToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axToolbarControl1.Size = New System.Drawing.Size(641, 28)
        Me.axToolbarControl1.TabIndex = 18
        '
        'axTOCControl1
        '
        Me.axTOCControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.axTOCControl1.Location = New System.Drawing.Point(11, 40)
        Me.axTOCControl1.Name = "axTOCControl1"
        Me.axTOCControl1.OcxState = CType(resources.GetObject("axTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axTOCControl1.Size = New System.Drawing.Size(196, 328)
        Me.axTOCControl1.TabIndex = 17
        '
        'axMapControl1
        '
        Me.axMapControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axMapControl1.Location = New System.Drawing.Point(213, 40)
        Me.axMapControl1.Name = "axMapControl1"
        Me.axMapControl1.OcxState = CType(resources.GetObject("axMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axMapControl1.Size = New System.Drawing.Size(440, 328)
        Me.axMapControl1.TabIndex = 16
        '
        'FrmMapControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 501)
        Me.Controls.Add(Me.axLicenseControl1)
        Me.Controls.Add(Me.axToolbarControl1)
        Me.Controls.Add(Me.axTOCControl1)
        Me.Controls.Add(Me.axMapControl1)
        Me.Controls.Add(Me.txtWebMap)
        Me.Controls.Add(Me.btnWebMap)
        Me.Controls.Add(Me.txtMapPackage)
        Me.Controls.Add(Me.txtLayerPackage)
        Me.Controls.Add(Me.btnLoadmpk)
        Me.Controls.Add(Me.btnLoadlpk)
        Me.Name = "FrmMapControl"
        Me.Text = "Working With Packages"
        CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
  Private WithEvents txtWebMap As System.Windows.Forms.TextBox
  Private WithEvents btnWebMap As System.Windows.Forms.Button
  Private WithEvents txtMapPackage As System.Windows.Forms.TextBox
  Private WithEvents txtLayerPackage As System.Windows.Forms.TextBox
  Private WithEvents btnLoadmpk As System.Windows.Forms.Button
  Private WithEvents btnLoadlpk As System.Windows.Forms.Button
  Private WithEvents axLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
  Private WithEvents axToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
  Private WithEvents axTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
  Private WithEvents axMapControl1 As ESRI.ArcGIS.Controls.AxMapControl

End Class
