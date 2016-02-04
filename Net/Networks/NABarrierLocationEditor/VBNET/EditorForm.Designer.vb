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
Namespace NABarrierLocationEditor
	Public Partial Class EditorForm
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
			Me.dataGridViewJunctions = New System.Windows.Forms.DataGridView()
			Me.JunctionEID = New System.Windows.Forms.DataGridViewTextBoxColumn()
			Me.btnSave = New System.Windows.Forms.Button()
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.btnZoomToBarrier = New System.Windows.Forms.Button()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.rbFlashElementPortion = New System.Windows.Forms.RadioButton()
			Me.rbFlashBarrierPortion = New System.Windows.Forms.RadioButton()
			Me.rbFlashSourceFeature = New System.Windows.Forms.RadioButton()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.dataGridViewEdges = New System.Windows.Forms.DataGridView()
			Me.EdgeEID = New System.Windows.Forms.DataGridViewTextBoxColumn()
			Me.Direction = New System.Windows.Forms.DataGridViewComboBoxColumn()
			Me.fromPos = New System.Windows.Forms.DataGridViewTextBoxColumn()
			Me.toPos = New System.Windows.Forms.DataGridViewTextBoxColumn()
			Me.label10 = New System.Windows.Forms.Label()
			Me.label9 = New System.Windows.Forms.Label()
			Me.label8 = New System.Windows.Forms.Label()
			Me.label7 = New System.Windows.Forms.Label()
			Me.label6 = New System.Windows.Forms.Label()
			Me.label5 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			CType(Me.dataGridViewJunctions, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.groupBox2.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			CType(Me.dataGridViewEdges, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' dataGridViewJunctions
			' 
			Me.dataGridViewJunctions.AllowUserToResizeRows = False
			Me.dataGridViewJunctions.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.dataGridViewJunctions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
			Me.dataGridViewJunctions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() { Me.JunctionEID})
			Me.dataGridViewJunctions.Location = New System.Drawing.Point(605, 108)
			Me.dataGridViewJunctions.Name = "dataGridViewJunctions"
			Me.dataGridViewJunctions.Size = New System.Drawing.Size(143, 182)
			Me.dataGridViewJunctions.TabIndex = 1
'			Me.dataGridViewJunctions.RowHeaderMouseClick += New System.Windows.Forms.DataGridViewCellMouseEventHandler(Me.dataGridView_RowHeaderMouseClick);
			' 
			' JunctionEID
			' 
			Me.JunctionEID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
			Me.JunctionEID.HeaderText = "Junction EID"
			Me.JunctionEID.Name = "JunctionEID"
			' 
			' btnSave
			' 
			Me.btnSave.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnSave.Location = New System.Drawing.Point(570, 341)
			Me.btnSave.Name = "btnSave"
			Me.btnSave.Size = New System.Drawing.Size(100, 25)
			Me.btnSave.TabIndex = 5
			Me.btnSave.Text = "OK"
			Me.btnSave.UseVisualStyleBackColor = True
'			Me.btnSave.Click += New System.EventHandler(Me.btnSave_Click);
			' 
			' btnCancel
			' 
			Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnCancel.Location = New System.Drawing.Point(680, 341)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(100, 25)
			Me.btnCancel.TabIndex = 6
			Me.btnCancel.Text = "Cancel"
			Me.btnCancel.UseVisualStyleBackColor = True
			' 
			' btnZoomToBarrier
			' 
			Me.btnZoomToBarrier.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.btnZoomToBarrier.Location = New System.Drawing.Point(12, 341)
			Me.btnZoomToBarrier.Name = "btnZoomToBarrier"
			Me.btnZoomToBarrier.Size = New System.Drawing.Size(160, 25)
			Me.btnZoomToBarrier.TabIndex = 10
			Me.btnZoomToBarrier.Text = "&Zoom To Barrier"
			Me.btnZoomToBarrier.UseVisualStyleBackColor = True
'			Me.btnZoomToBarrier.Click += New System.EventHandler(Me.btnZoomToBarrier_Click);
			' 
			' groupBox2
			' 
			Me.groupBox2.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.groupBox2.Controls.Add(Me.rbFlashElementPortion)
			Me.groupBox2.Controls.Add(Me.rbFlashBarrierPortion)
			Me.groupBox2.Controls.Add(Me.rbFlashSourceFeature)
			Me.groupBox2.Location = New System.Drawing.Point(212, 326)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(320, 48)
			Me.groupBox2.TabIndex = 12
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "Geometry Flash Options"
			' 
			' rbFlashElementPortion
			' 
			Me.rbFlashElementPortion.AutoSize = True
			Me.rbFlashElementPortion.Location = New System.Drawing.Point(131, 19)
			Me.rbFlashElementPortion.Name = "rbFlashElementPortion"
			Me.rbFlashElementPortion.Size = New System.Drawing.Size(63, 17)
			Me.rbFlashElementPortion.TabIndex = 2
			Me.rbFlashElementPortion.Text = "&Element"
			Me.rbFlashElementPortion.UseVisualStyleBackColor = True
			' 
			' rbFlashBarrierPortion
			' 
			Me.rbFlashBarrierPortion.AutoSize = True
			Me.rbFlashBarrierPortion.Checked = True
			Me.rbFlashBarrierPortion.Location = New System.Drawing.Point(15, 19)
			Me.rbFlashBarrierPortion.Name = "rbFlashBarrierPortion"
			Me.rbFlashBarrierPortion.Size = New System.Drawing.Size(101, 17)
			Me.rbFlashBarrierPortion.TabIndex = 1
			Me.rbFlashBarrierPortion.TabStop = True
			Me.rbFlashBarrierPortion.Text = "Location &Range"
			Me.rbFlashBarrierPortion.UseVisualStyleBackColor = True
			' 
			' rbFlashSourceFeature
			' 
			Me.rbFlashSourceFeature.AutoSize = True
			Me.rbFlashSourceFeature.Location = New System.Drawing.Point(209, 19)
			Me.rbFlashSourceFeature.Name = "rbFlashSourceFeature"
			Me.rbFlashSourceFeature.Size = New System.Drawing.Size(98, 17)
			Me.rbFlashSourceFeature.TabIndex = 0
			Me.rbFlashSourceFeature.Text = "Source &Feature"
			Me.rbFlashSourceFeature.UseVisualStyleBackColor = True
			' 
			' groupBox3
			' 
			Me.groupBox3.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.groupBox3.Controls.Add(Me.label10)
			Me.groupBox3.Controls.Add(Me.label9)
			Me.groupBox3.Controls.Add(Me.label8)
			Me.groupBox3.Controls.Add(Me.label7)
			Me.groupBox3.Controls.Add(Me.label6)
			Me.groupBox3.Controls.Add(Me.label5)
			Me.groupBox3.Controls.Add(Me.label3)
			Me.groupBox3.Controls.Add(Me.label4)
			Me.groupBox3.Controls.Add(Me.dataGridViewEdges)
			Me.groupBox3.Controls.Add(Me.dataGridViewJunctions)
			Me.groupBox3.Location = New System.Drawing.Point(12, 12)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(768, 308)
			Me.groupBox3.TabIndex = 13
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "Location Ranges"
			' 
			' dataGridViewEdges
			' 
			Me.dataGridViewEdges.AllowUserToResizeRows = False
			Me.dataGridViewEdges.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.dataGridViewEdges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
			Me.dataGridViewEdges.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() { Me.EdgeEID, Me.Direction, Me.fromPos, Me.toPos})
			Me.dataGridViewEdges.Location = New System.Drawing.Point(19, 108)
			Me.dataGridViewEdges.Name = "dataGridViewEdges"
			Me.dataGridViewEdges.Size = New System.Drawing.Size(580, 182)
			Me.dataGridViewEdges.TabIndex = 5
'			Me.dataGridViewEdges.RowHeaderMouseClick += New System.Windows.Forms.DataGridViewCellMouseEventHandler(Me.dataGridView_RowHeaderMouseClick);
			' 
			' EdgeEID
			' 
			Me.EdgeEID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
			Me.EdgeEID.HeaderText = "Edge EID"
			Me.EdgeEID.Name = "EdgeEID"
			Me.EdgeEID.Width = 78
			' 
			' Direction
			' 
			Me.Direction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
			Me.Direction.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing
			Me.Direction.HeaderText = "Direction"
			Me.Direction.MinimumWidth = 40
			Me.Direction.Name = "Direction"
			Me.Direction.Resizable = System.Windows.Forms.DataGridViewTriState.True
			Me.Direction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
			Me.Direction.Width = 74
			' 
			' fromPos
			' 
			Me.fromPos.FillWeight = 22.22223F
			Me.fromPos.HeaderText = "From Element Position"
			Me.fromPos.Name = "fromPos"
			Me.fromPos.Width = 140
			' 
			' toPos
			' 
			Me.toPos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
			Me.toPos.FillWeight = 177.7778F
			Me.toPos.HeaderText = "To Element Position"
			Me.toPos.Name = "toPos"
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label10.Location = New System.Drawing.Point(19, 73)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(41, 13)
			Me.label10.TabIndex = 32
			Me.label10.Text = "Flash:"
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label9.Location = New System.Drawing.Point(19, 57)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(33, 13)
			Me.label9.TabIndex = 31
			Me.label9.Text = "Edit:"
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label8.Location = New System.Drawing.Point(19, 41)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(48, 13)
			Me.label8.TabIndex = 30
			Me.label8.Text = "Delete:"
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label7.Location = New System.Drawing.Point(19, 25)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(33, 13)
			Me.label7.TabIndex = 29
			Me.label7.Text = "Add:"
			' 
			' label6
			' 
			Me.label6.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.label6.AutoSize = True
			Me.label6.Location = New System.Drawing.Point(66, 57)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(158, 13)
			Me.label6.TabIndex = 28
			Me.label6.Text = "Click in a cell to edit its contents"
			' 
			' label5
			' 
			Me.label5.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(66, 73)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(95, 13)
			Me.label5.TabIndex = 27
			Me.label5.Text = "Click a row header"
			' 
			' label3
			' 
			Me.label3.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(66, 41)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(183, 13)
			Me.label3.TabIndex = 26
			Me.label3.Text = "Select rows and press the Delete key"
			' 
			' label4
			' 
			Me.label4.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(66, 25)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(286, 13)
			Me.label4.TabIndex = 25
			Me.label4.Text = "Enter new barrier information in row with * in the row header"
			' 
			' EditorForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(792, 383)
			Me.Controls.Add(Me.groupBox3)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.btnZoomToBarrier)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnSave)
			Me.MaximumSize = New System.Drawing.Size(800, 2000)
			Me.MinimumSize = New System.Drawing.Size(800, 410)
			Me.Name = "EditorForm"
			Me.Text = "Barrier Location Editor"
			CType(Me.dataGridViewJunctions, System.ComponentModel.ISupportInitialize).EndInit()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			CType(Me.dataGridViewEdges, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents dataGridViewJunctions As System.Windows.Forms.DataGridView
		Private WithEvents btnSave As System.Windows.Forms.Button
		Private btnCancel As System.Windows.Forms.Button
		Private JunctionEID As System.Windows.Forms.DataGridViewTextBoxColumn
		Private WithEvents btnZoomToBarrier As System.Windows.Forms.Button
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private rbFlashBarrierPortion As System.Windows.Forms.RadioButton
		Private rbFlashSourceFeature As System.Windows.Forms.RadioButton
		Private rbFlashElementPortion As System.Windows.Forms.RadioButton
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private WithEvents dataGridViewEdges As System.Windows.Forms.DataGridView
		Private EdgeEID As System.Windows.Forms.DataGridViewTextBoxColumn
		Private Direction As System.Windows.Forms.DataGridViewComboBoxColumn
		Private fromPos As System.Windows.Forms.DataGridViewTextBoxColumn
		Private toPos As System.Windows.Forms.DataGridViewTextBoxColumn
		Private label10 As System.Windows.Forms.Label
		Private label9 As System.Windows.Forms.Label
		Private label8 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private label6 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label

	End Class
End Namespace