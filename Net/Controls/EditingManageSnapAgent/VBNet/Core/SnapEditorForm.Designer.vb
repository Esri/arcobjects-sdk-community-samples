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
'Error: Converting Properties 
Imports ESRI.ArcGIS.SystemUI
Imports System.Windows.Forms

Namespace Core
  Partial Class SnapEditor
    Inherits System.Windows.Forms.Form
    '/ <summary>
    '/ Required designer variable.
    '/ </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    '/ <summary>
    '/ Clean up any resources being used.
    '/ </summary>
    '/ <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing AndAlso (components IsNot Nothing) Then
        components.Dispose()
      End If
      MyBase.Dispose(disposing)
    End Sub

    '/ <summary>
    '/ Required method for Designer support - do not modify
    '/ the contents of this method with the code editor.
    '/ </summary>
    Private Sub InitializeComponent()
      Me.clearAgents = New System.Windows.Forms.Button
      Me.turnOffAgents = New System.Windows.Forms.Button
      Me.addFeatureSnapAgent = New System.Windows.Forms.Button
      Me.reverseAgentsPriority = New System.Windows.Forms.Button
      Me.snapToleranceLabel = New System.Windows.Forms.Label
      Me.snapTips = New System.Windows.Forms.CheckBox
      Me.snapTolUnits = New System.Windows.Forms.ComboBox
      Me.snapTolerance = New System.Windows.Forms.MaskedTextBox
      Me.snapAgents = New System.Windows.Forms.DataGridView
      Me.addSketchSnapAgent = New System.Windows.Forms.Button
      Me.snapAgentNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
      Me.ftrClass = New System.Windows.Forms.DataGridViewTextBoxColumn
      Me.hitTypes = New System.Windows.Forms.DataGridViewTextBoxColumn
      CType(Me.snapAgents, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'clearAgents
      '
      Me.clearAgents.Location = New System.Drawing.Point(12, 233)
      Me.clearAgents.Name = "clearAgents"
      Me.clearAgents.Size = New System.Drawing.Size(131, 23)
      Me.clearAgents.TabIndex = 0
      Me.clearAgents.Text = "Clear Agents"
      Me.clearAgents.UseVisualStyleBackColor = True
      '
      'turnOffAgents
      '
      Me.turnOffAgents.Location = New System.Drawing.Point(149, 233)
      Me.turnOffAgents.Name = "turnOffAgents"
      Me.turnOffAgents.Size = New System.Drawing.Size(131, 23)
      Me.turnOffAgents.TabIndex = 1
      Me.turnOffAgents.Text = "Turn Off Agents"
      Me.turnOffAgents.UseVisualStyleBackColor = True
      '
      'addFeatureSnapAgent
      '
      Me.addFeatureSnapAgent.Location = New System.Drawing.Point(418, 147)
      Me.addFeatureSnapAgent.Name = "addFeatureSnapAgent"
      Me.addFeatureSnapAgent.Size = New System.Drawing.Size(147, 23)
      Me.addFeatureSnapAgent.TabIndex = 2
      Me.addFeatureSnapAgent.Text = "Add Feature Snap Agent"
      Me.addFeatureSnapAgent.UseVisualStyleBackColor = True
      '
      'reverseAgentsPriority
      '
      Me.reverseAgentsPriority.Location = New System.Drawing.Point(286, 233)
      Me.reverseAgentsPriority.Name = "reverseAgentsPriority"
      Me.reverseAgentsPriority.Size = New System.Drawing.Size(144, 23)
      Me.reverseAgentsPriority.TabIndex = 3
      Me.reverseAgentsPriority.Text = "Reverse Agents' Priority"
      Me.reverseAgentsPriority.UseVisualStyleBackColor = True
      '
      'snapToleranceLabel
      '
      Me.snapToleranceLabel.AutoSize = True
      Me.snapToleranceLabel.Location = New System.Drawing.Point(12, 44)
      Me.snapToleranceLabel.Name = "snapToleranceLabel"
      Me.snapToleranceLabel.Size = New System.Drawing.Size(83, 13)
      Me.snapToleranceLabel.TabIndex = 7
      Me.snapToleranceLabel.Text = "Snap Tolerance"
      '
      'snapTips
      '
      Me.snapTips.AutoSize = True
      Me.snapTips.Location = New System.Drawing.Point(15, 16)
      Me.snapTips.Name = "snapTips"
      Me.snapTips.Size = New System.Drawing.Size(74, 17)
      Me.snapTips.TabIndex = 9
      Me.snapTips.Text = "Snap Tips"
      Me.snapTips.UseVisualStyleBackColor = True
      '
      'snapTolUnits
      '
      Me.snapTolUnits.FormattingEnabled = True
      Me.snapTolUnits.Items.AddRange(New Object() {"Pixels", "Map Units"})
      Me.snapTolUnits.Location = New System.Drawing.Point(159, 41)
      Me.snapTolUnits.Name = "snapTolUnits"
      Me.snapTolUnits.Size = New System.Drawing.Size(121, 21)
      Me.snapTolUnits.TabIndex = 12
      '
      'snapTolerance
      '
      Me.snapTolerance.AllowPromptAsInput = False
      Me.snapTolerance.AsciiOnly = True
      Me.snapTolerance.Location = New System.Drawing.Point(101, 41)
      Me.snapTolerance.Mask = "00000"
      Me.snapTolerance.Name = "snapTolerance"
      Me.snapTolerance.Size = New System.Drawing.Size(52, 20)
      Me.snapTolerance.TabIndex = 14
      '
      'snapAgents
      '
      Me.snapAgents.AllowUserToAddRows = False
      Me.snapAgents.AllowUserToDeleteRows = False
      Me.snapAgents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
      Me.snapAgents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
      Me.snapAgents.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.snapAgentNameColumn, Me.ftrClass, Me.hitTypes})
      Me.snapAgents.Location = New System.Drawing.Point(12, 77)
      Me.snapAgents.MultiSelect = False
      Me.snapAgents.Name = "snapAgents"
      Me.snapAgents.ReadOnly = True
      Me.snapAgents.Size = New System.Drawing.Size(400, 150)
      Me.snapAgents.TabIndex = 15
      '
      'addSketchSnapAgent
      '
      Me.addSketchSnapAgent.Location = New System.Drawing.Point(418, 176)
      Me.addSketchSnapAgent.Name = "addSketchSnapAgent"
      Me.addSketchSnapAgent.Size = New System.Drawing.Size(146, 23)
      Me.addSketchSnapAgent.TabIndex = 17
      Me.addSketchSnapAgent.Text = "Add Sketch Snap Agent"
      Me.addSketchSnapAgent.UseVisualStyleBackColor = True
      '
      'snapAgentNameColumn
      '
      Me.snapAgentNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
      Me.snapAgentNameColumn.HeaderText = "Snap Agent Name"
      Me.snapAgentNameColumn.MinimumWidth = 100
      Me.snapAgentNameColumn.Name = "snapAgentNameColumn"
      Me.snapAgentNameColumn.ReadOnly = True
      Me.snapAgentNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
      '
      'ftrClass
      '
      Me.ftrClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
      Me.ftrClass.HeaderText = "FeatureClass"
      Me.ftrClass.MinimumWidth = 85
      Me.ftrClass.Name = "ftrClass"
      Me.ftrClass.ReadOnly = True
      Me.ftrClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
      Me.ftrClass.Width = 85
      '
      'hitTypes
      '
      Me.hitTypes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
      Me.hitTypes.HeaderText = "Feature Agent Hit Type"
      Me.hitTypes.MinimumWidth = 145
      Me.hitTypes.Name = "hitTypes"
      Me.hitTypes.ReadOnly = True
      Me.hitTypes.Width = 145
      '
      'SnapEditor
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(578, 269)
      Me.Controls.Add(Me.addSketchSnapAgent)
      Me.Controls.Add(Me.snapAgents)
      Me.Controls.Add(Me.snapTolerance)
      Me.Controls.Add(Me.snapTolUnits)
      Me.Controls.Add(Me.snapTips)
      Me.Controls.Add(Me.snapToleranceLabel)
      Me.Controls.Add(Me.reverseAgentsPriority)
      Me.Controls.Add(Me.addFeatureSnapAgent)
      Me.Controls.Add(Me.turnOffAgents)
      Me.Controls.Add(Me.clearAgents)
      Me.Name = "SnapEditor"
      Me.Text = "Snap Editor"
      CType(Me.snapAgents, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)
      Me.PerformLayout()

    End Sub


    Private WithEvents clearAgents As System.Windows.Forms.Button
    Private WithEvents turnOffAgents As System.Windows.Forms.Button
    Private WithEvents addFeatureSnapAgent As System.Windows.Forms.Button
    Private WithEvents reverseAgentsPriority As System.Windows.Forms.Button
    Private snapToleranceLabel As System.Windows.Forms.Label
    Private WithEvents snapTips As System.Windows.Forms.CheckBox
    Private WithEvents snapTolUnits As System.Windows.Forms.ComboBox
    Private WithEvents snapTolerance As System.Windows.Forms.MaskedTextBox
    Private snapAgents As System.Windows.Forms.DataGridView
    Private WithEvents addSketchSnapAgent As System.Windows.Forms.Button
    Friend WithEvents snapAgentNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ftrClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hitTypes As System.Windows.Forms.DataGridViewTextBoxColumn
  End Class
End Namespace

'----------------------------------------------------------------
' Converted from C# to VB .NET using CSharpToVBConverter(1.2).
' Developed by: Kamal Patel (http://www.KamalPatel.net) 
'----------------------------------------------------------------
