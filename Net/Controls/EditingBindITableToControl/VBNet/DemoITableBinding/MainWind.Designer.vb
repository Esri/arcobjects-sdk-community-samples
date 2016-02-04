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
Namespace DemoITableBinding
  <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
  Partial Class MainWnd
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
      MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWnd))
            Me.bindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
            Me.bindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
            Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
            Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
            Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
            Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
            Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
            Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
            Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
            Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
            Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
            Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
            Me.dataGridView1 = New System.Windows.Forms.DataGridView()
            Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.chkUseCVD = New System.Windows.Forms.CheckBox()
            CType(Me.bindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.bindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.bindingNavigator1.SuspendLayout()
            CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tableLayoutPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'bindingSource1
            '
            Me.bindingSource1.AllowNew = True
            '
            'bindingNavigator1
            '
            Me.bindingNavigator1.AddNewItem = Me.BindingNavigatorAddNewItem
            Me.bindingNavigator1.BindingSource = Me.bindingSource1
            Me.tableLayoutPanel1.SetColumnSpan(Me.bindingNavigator1, 2)
            Me.bindingNavigator1.CountItem = Me.BindingNavigatorCountItem
            Me.bindingNavigator1.DeleteItem = Me.BindingNavigatorDeleteItem
            Me.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.bindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem})
            Me.bindingNavigator1.Location = New System.Drawing.Point(0, 307)
            Me.bindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
            Me.bindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
            Me.bindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
            Me.bindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
            Me.bindingNavigator1.Name = "bindingNavigator1"
            Me.bindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
            Me.bindingNavigator1.Size = New System.Drawing.Size(457, 25)
            Me.bindingNavigator1.TabIndex = 0
            Me.bindingNavigator1.Text = "BindingNavigator1"
            '
            'BindingNavigatorAddNewItem
            '
            Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
            Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
            Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
            Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
            Me.BindingNavigatorAddNewItem.Text = "Add new"
            '
            'BindingNavigatorCountItem
            '
            Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
            Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
            Me.BindingNavigatorCountItem.Text = "of {0}"
            Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
            '
            'BindingNavigatorDeleteItem
            '
            Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
            Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
            Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
            Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
            Me.BindingNavigatorDeleteItem.Text = "Delete"
            '
            'BindingNavigatorMoveFirstItem
            '
            Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
            Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
            Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
            Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
            Me.BindingNavigatorMoveFirstItem.Text = "Move first"
            '
            'BindingNavigatorMovePreviousItem
            '
            Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
            Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
            Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
            Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
            Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
            '
            'BindingNavigatorSeparator
            '
            Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
            Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
            '
            'BindingNavigatorPositionItem
            '
            Me.BindingNavigatorPositionItem.AccessibleName = "Position"
            Me.BindingNavigatorPositionItem.AutoSize = False
            Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
            Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 21)
            Me.BindingNavigatorPositionItem.Text = "0"
            Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
            '
            'BindingNavigatorSeparator1
            '
            Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
            Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
            '
            'BindingNavigatorMoveNextItem
            '
            Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
            Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
            Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
            Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
            Me.BindingNavigatorMoveNextItem.Text = "Move next"
            '
            'BindingNavigatorMoveLastItem
            '
            Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
            Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
            Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
            Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
            Me.BindingNavigatorMoveLastItem.Text = "Move last"
            '
            'BindingNavigatorSeparator2
            '
            Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
            Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
            '
            'dataGridView1
            '
            Me.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.tableLayoutPanel1.SetColumnSpan(Me.dataGridView1, 2)
            Me.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dataGridView1.Location = New System.Drawing.Point(3, 3)
            Me.dataGridView1.Name = "dataGridView1"
            Me.dataGridView1.Size = New System.Drawing.Size(451, 276)
            Me.dataGridView1.TabIndex = 0
            '
            'tableLayoutPanel1
            '
            Me.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tableLayoutPanel1.ColumnCount = 2
            Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tableLayoutPanel1.Controls.Add(Me.dataGridView1, 0, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.bindingNavigator1, 0, 2)
            Me.tableLayoutPanel1.Controls.Add(Me.textBox1, 0, 1)
            Me.tableLayoutPanel1.Controls.Add(Me.chkUseCVD, 1, 1)
            Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
            Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
            Me.tableLayoutPanel1.RowCount = 3
            Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
            Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
            Me.tableLayoutPanel1.Size = New System.Drawing.Size(457, 332)
            Me.tableLayoutPanel1.TabIndex = 2
            '
            'textBox1
            '
            Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.textBox1.Location = New System.Drawing.Point(3, 285)
            Me.textBox1.Name = "textBox1"
            Me.textBox1.Size = New System.Drawing.Size(131, 20)
            Me.textBox1.TabIndex = 1
            '
            'chkUseCVD
            '
            Me.chkUseCVD.AutoSize = True
            Me.chkUseCVD.Location = New System.Drawing.Point(231, 285)
            Me.chkUseCVD.Name = "chkUseCVD"
            Me.chkUseCVD.Size = New System.Drawing.Size(223, 17)
            Me.chkUseCVD.TabIndex = 2
            Me.chkUseCVD.Text = "Use Coded Value Domain on 'Enabled' field"
            Me.chkUseCVD.UseVisualStyleBackColor = True
            '
            'MainWnd
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(457, 332)
            Me.Controls.Add(Me.tableLayoutPanel1)
            Me.Name = "MainWnd"
            Me.Text = "ITable Data Binding"
            CType(Me.bindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.bindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.bindingNavigator1.ResumeLayout(False)
            Me.bindingNavigator1.PerformLayout()
            CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tableLayoutPanel1.ResumeLayout(False)
            Me.tableLayoutPanel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents dataGridView1 As System.Windows.Forms.DataGridView
    Private WithEvents bindingSource1 As System.Windows.Forms.BindingSource
    Private WithEvents bindingNavigator1 As System.Windows.Forms.BindingNavigator
        Friend WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents textBox1 As System.Windows.Forms.TextBox
    Friend WithEvents chkUseCVD As System.Windows.Forms.CheckBox

  End Class
End Namespace