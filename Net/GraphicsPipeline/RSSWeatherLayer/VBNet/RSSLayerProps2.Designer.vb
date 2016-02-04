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

  Public Partial Class RSSLayerProps2
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
	  Me.label1 = New System.Windows.Forms.Label()
	  Me.txtSymbolSize = New System.Windows.Forms.TextBox()
	  Me.SuspendLayout()
	  ' 
	  ' label1
	  ' 
	  Me.label1.AutoSize = True
	  Me.label1.Location = New System.Drawing.Point(13, 13)
	  Me.label1.Name = "label1"
	  Me.label1.Size = New System.Drawing.Size(65, 13)
	  Me.label1.TabIndex = 0
	  Me.label1.Text = "Symbol size:"
	  ' 
	  ' txtSymbolSize
	  ' 
	  Me.txtSymbolSize.Location = New System.Drawing.Point(84, 13)
	  Me.txtSymbolSize.Name = "txtSymbolSize"
	  Me.txtSymbolSize.Size = New System.Drawing.Size(66, 20)
	  Me.txtSymbolSize.TabIndex = 1
'	  Me.txtSymbolSize.TextChanged += New System.EventHandler(Me.txtSymbolSize_TextChanged);
	  ' 
	  ' RSSLayerProps2
	  ' 
	  Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
	  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
	  Me.ClientSize = New System.Drawing.Size(213, 273)
	  Me.Controls.Add(Me.txtSymbolSize)
	  Me.Controls.Add(Me.label1)
	  Me.Name = "RSSLayerProps2"
	  Me.Text = "RSS Layer input"
	  Me.ResumeLayout(False)
	  Me.PerformLayout()

	End Sub

	#End Region

	Private label1 As System.Windows.Forms.Label
	Public WithEvents txtSymbolSize As System.Windows.Forms.TextBox
  End Class
