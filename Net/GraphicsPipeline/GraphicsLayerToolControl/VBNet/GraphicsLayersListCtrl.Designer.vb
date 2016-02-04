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
Partial Public Class GraphicsLayersListCtrl
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

#Region "Component Designer generated code"

  ''' <summary> 
  ''' Required method for Designer support - do not modify 
  ''' the contents of this method with the code editor.
  ''' </summary>
  Private Sub InitializeComponent()
    Me.cmbGraphicsLayerList = New System.Windows.Forms.ComboBox()
    Me.lblGraphicsLayer = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    ' 
    ' cmbGraphicsLayerList
    ' 
    Me.cmbGraphicsLayerList.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
    Me.cmbGraphicsLayerList.Location = New System.Drawing.Point(84, 3)
    Me.cmbGraphicsLayerList.Name = "cmbGraphicsLayerList"
    Me.cmbGraphicsLayerList.Size = New System.Drawing.Size(162, 21)
    Me.cmbGraphicsLayerList.TabIndex = 3
    '	  Me.cmbGraphicsLayerList.SelectedIndexChanged += New System.EventHandler(Me.cmbGraphicsLayerList_SelectedIndexChanged);
    ' 
    ' lblGraphicsLayer
    ' 
    Me.lblGraphicsLayer.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
    Me.lblGraphicsLayer.Location = New System.Drawing.Point(1, 4)
    Me.lblGraphicsLayer.Name = "lblGraphicsLayer"
    Me.lblGraphicsLayer.Size = New System.Drawing.Size(88, 17)
    Me.lblGraphicsLayer.TabIndex = 2
    Me.lblGraphicsLayer.Text = "Graphics Layer:"
    Me.lblGraphicsLayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    ' 
    ' GraphicsLayersListCtrl
    ' 
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.cmbGraphicsLayerList)
    Me.Controls.Add(Me.lblGraphicsLayer)
    Me.Name = "GraphicsLayersListCtrl"
    Me.Size = New System.Drawing.Size(249, 28)
    Me.ResumeLayout(False)

  End Sub

#End Region

  Public WithEvents cmbGraphicsLayerList As System.Windows.Forms.ComboBox
  Public lblGraphicsLayer As System.Windows.Forms.Label
End Class
