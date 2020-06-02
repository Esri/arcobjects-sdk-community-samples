/*

   Copyright 2019 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
namespace DemoITableBinding
{
  partial class MainWnd
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWnd));
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
        this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
        this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
        this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
        this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
        this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
        this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
        this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
        this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
        this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
        this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
        this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        this.chkUseCVD = new System.Windows.Forms.CheckBox();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
        this.bindingNavigator1.SuspendLayout();
        this.tableLayoutPanel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 2);
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
        this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dataGridView1.Location = new System.Drawing.Point(3, 3);
        this.dataGridView1.Name = "dataGridView1";
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
        this.dataGridView1.Size = new System.Drawing.Size(700, 454);
        this.dataGridView1.TabIndex = 0;
        // 
        // bindingNavigator1
        // 
        this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
        this.bindingNavigator1.BindingSource = this.bindingSource1;
        this.tableLayoutPanel1.SetColumnSpan(this.bindingNavigator1, 2);
        this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
        this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
        this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
        this.bindingNavigator1.Location = new System.Drawing.Point(0, 485);
        this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
        this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
        this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
        this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
        this.bindingNavigator1.Name = "bindingNavigator1";
        this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
        this.bindingNavigator1.Size = new System.Drawing.Size(706, 25);
        this.bindingNavigator1.TabIndex = 2;
        this.bindingNavigator1.Text = "bindingNavigator1";
        // 
        // bindingNavigatorAddNewItem
        // 
        this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
        this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
        this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
        this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
        this.bindingNavigatorAddNewItem.Text = "Add new";
        // 
        // bindingNavigatorCountItem
        // 
        this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
        this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
        this.bindingNavigatorCountItem.Text = "of {0}";
        this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
        // 
        // bindingNavigatorDeleteItem
        // 
        this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
        this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
        this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
        this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
        this.bindingNavigatorDeleteItem.Text = "Delete";
        // 
        // bindingNavigatorMoveFirstItem
        // 
        this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
        this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
        this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
        this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
        this.bindingNavigatorMoveFirstItem.Text = "Move first";
        // 
        // bindingNavigatorMovePreviousItem
        // 
        this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
        this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
        this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
        this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
        this.bindingNavigatorMovePreviousItem.Text = "Move previous";
        // 
        // bindingNavigatorSeparator
        // 
        this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
        this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
        // 
        // bindingNavigatorPositionItem
        // 
        this.bindingNavigatorPositionItem.AccessibleName = "Position";
        this.bindingNavigatorPositionItem.AutoSize = false;
        this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
        this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
        this.bindingNavigatorPositionItem.Text = "0";
        this.bindingNavigatorPositionItem.ToolTipText = "Current position";
        // 
        // bindingNavigatorSeparator1
        // 
        this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
        this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
        // 
        // bindingNavigatorMoveNextItem
        // 
        this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
        this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
        this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
        this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
        this.bindingNavigatorMoveNextItem.Text = "Move next";
        // 
        // bindingNavigatorMoveLastItem
        // 
        this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
        this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
        this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
        this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
        this.bindingNavigatorMoveLastItem.Text = "Move last";
        // 
        // bindingNavigatorSeparator2
        // 
        this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
        this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
        // 
        // textBox1
        // 
        this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)));
        this.textBox1.Location = new System.Drawing.Point(3, 463);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(130, 20);
        this.textBox1.TabIndex = 0;
        // 
        // tableLayoutPanel1
        // 
        this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.tableLayoutPanel1.ColumnCount = 2;
        this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
        this.tableLayoutPanel1.Controls.Add(this.bindingNavigator1, 0, 2);
        this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
        this.tableLayoutPanel1.Controls.Add(this.chkUseCVD, 1, 1);
        this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        this.tableLayoutPanel1.Name = "tableLayoutPanel1";
        this.tableLayoutPanel1.RowCount = 3;
        this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.tableLayoutPanel1.Size = new System.Drawing.Size(706, 510);
        this.tableLayoutPanel1.TabIndex = 4;
        // 
        // chkUseCVD
        // 
        this.chkUseCVD.AutoSize = true;
        this.chkUseCVD.Location = new System.Drawing.Point(356, 463);
        this.chkUseCVD.Name = "chkUseCVD";
        this.chkUseCVD.Size = new System.Drawing.Size(231, 17);
        this.chkUseCVD.TabIndex = 3;
        this.chkUseCVD.Text = "Use Coded Value Domain on \'Enabled\' field";
        this.chkUseCVD.UseVisualStyleBackColor = true;
        this.chkUseCVD.CheckedChanged += new System.EventHandler(this.chkUseCVD_CheckedChanged);
        // 
        // MainWnd
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(706, 510);
        this.Controls.Add(this.tableLayoutPanel1);
        this.Name = "MainWnd";
        this.Text = "ITable Data Binding";
        this.Load += new System.EventHandler(this.MainWnd_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
        this.bindingNavigator1.ResumeLayout(false);
        this.bindingNavigator1.PerformLayout();
        this.tableLayoutPanel1.ResumeLayout(false);
        this.tableLayoutPanel1.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
      private System.Windows.Forms.BindingSource bindingSource1;
    private System.Windows.Forms.BindingNavigator bindingNavigator1;
    private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
    private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
    private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.CheckBox chkUseCVD;
  }
}

