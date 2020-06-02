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
namespace NABarrierLocationEditor
{
    partial class EditorForm
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
            this.dataGridViewJunctions = new System.Windows.Forms.DataGridView();
            this.JunctionEID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnZoomToBarrier = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbFlashElementPortion = new System.Windows.Forms.RadioButton();
            this.rbFlashBarrierPortion = new System.Windows.Forms.RadioButton();
            this.rbFlashSourceFeature = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridViewEdges = new System.Windows.Forms.DataGridView();
            this.EdgeEID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direction = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.fromPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJunctions)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEdges)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewJunctions
            // 
            this.dataGridViewJunctions.AllowUserToResizeRows = false;
            this.dataGridViewJunctions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewJunctions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewJunctions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JunctionEID});
            this.dataGridViewJunctions.Location = new System.Drawing.Point(605, 108);
            this.dataGridViewJunctions.Name = "dataGridViewJunctions";
            this.dataGridViewJunctions.Size = new System.Drawing.Size(143, 182);
            this.dataGridViewJunctions.TabIndex = 1;
            this.dataGridViewJunctions.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_RowHeaderMouseClick);
            // 
            // JunctionEID
            // 
            this.JunctionEID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JunctionEID.HeaderText = "Junction EID";
            this.JunctionEID.Name = "JunctionEID";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(570, 341);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(680, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnZoomToBarrier
            // 
            this.btnZoomToBarrier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoomToBarrier.Location = new System.Drawing.Point(12, 341);
            this.btnZoomToBarrier.Name = "btnZoomToBarrier";
            this.btnZoomToBarrier.Size = new System.Drawing.Size(160, 25);
            this.btnZoomToBarrier.TabIndex = 10;
            this.btnZoomToBarrier.Text = "&Zoom To Barrier";
            this.btnZoomToBarrier.UseVisualStyleBackColor = true;
            this.btnZoomToBarrier.Click += new System.EventHandler(this.btnZoomToBarrier_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.rbFlashElementPortion);
            this.groupBox2.Controls.Add(this.rbFlashBarrierPortion);
            this.groupBox2.Controls.Add(this.rbFlashSourceFeature);
            this.groupBox2.Location = new System.Drawing.Point(212, 326);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 48);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Geometry Flash Options";
            // 
            // rbFlashElementPortion
            // 
            this.rbFlashElementPortion.AutoSize = true;
            this.rbFlashElementPortion.Location = new System.Drawing.Point(131, 19);
            this.rbFlashElementPortion.Name = "rbFlashElementPortion";
            this.rbFlashElementPortion.Size = new System.Drawing.Size(63, 17);
            this.rbFlashElementPortion.TabIndex = 2;
            this.rbFlashElementPortion.Text = "&Element";
            this.rbFlashElementPortion.UseVisualStyleBackColor = true;
            // 
            // rbFlashBarrierPortion
            // 
            this.rbFlashBarrierPortion.AutoSize = true;
            this.rbFlashBarrierPortion.Checked = true;
            this.rbFlashBarrierPortion.Location = new System.Drawing.Point(15, 19);
            this.rbFlashBarrierPortion.Name = "rbFlashBarrierPortion";
            this.rbFlashBarrierPortion.Size = new System.Drawing.Size(101, 17);
            this.rbFlashBarrierPortion.TabIndex = 1;
            this.rbFlashBarrierPortion.TabStop = true;
            this.rbFlashBarrierPortion.Text = "Location &Range";
            this.rbFlashBarrierPortion.UseVisualStyleBackColor = true;
            // 
            // rbFlashSourceFeature
            // 
            this.rbFlashSourceFeature.AutoSize = true;
            this.rbFlashSourceFeature.Location = new System.Drawing.Point(209, 19);
            this.rbFlashSourceFeature.Name = "rbFlashSourceFeature";
            this.rbFlashSourceFeature.Size = new System.Drawing.Size(98, 17);
            this.rbFlashSourceFeature.TabIndex = 0;
            this.rbFlashSourceFeature.Text = "Source &Feature";
            this.rbFlashSourceFeature.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dataGridViewEdges);
            this.groupBox3.Controls.Add(this.dataGridViewJunctions);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(768, 308);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Location Ranges";
            // 
            // dataGridViewEdges
            // 
            this.dataGridViewEdges.AllowUserToResizeRows = false;
            this.dataGridViewEdges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewEdges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEdges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EdgeEID,
            this.Direction,
            this.fromPos,
            this.toPos});
            this.dataGridViewEdges.Location = new System.Drawing.Point(19, 108);
            this.dataGridViewEdges.Name = "dataGridViewEdges";
            this.dataGridViewEdges.Size = new System.Drawing.Size(580, 182);
            this.dataGridViewEdges.TabIndex = 5;
            this.dataGridViewEdges.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_RowHeaderMouseClick);
            // 
            // EdgeEID
            // 
            this.EdgeEID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EdgeEID.HeaderText = "Edge EID";
            this.EdgeEID.Name = "EdgeEID";
            this.EdgeEID.Width = 78;
            // 
            // Direction
            // 
            this.Direction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Direction.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Direction.HeaderText = "Direction";
            this.Direction.MinimumWidth = 40;
            this.Direction.Name = "Direction";
            this.Direction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Direction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Direction.Width = 74;
            // 
            // fromPos
            // 
            this.fromPos.FillWeight = 22.22223F;
            this.fromPos.HeaderText = "From Element Position";
            this.fromPos.Name = "fromPos";
            this.fromPos.Width = 140;
            // 
            // toPos
            // 
            this.toPos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.toPos.FillWeight = 177.7778F;
            this.toPos.HeaderText = "To Element Position";
            this.toPos.Name = "toPos";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Flash:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Edit:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Delete:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Add:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Click in a cell to edit its contents";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Click a row header";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Select rows and press the Delete key";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(286, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Enter new barrier information in row with * in the row header";
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 383);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnZoomToBarrier);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.MaximumSize = new System.Drawing.Size(800, 2000);
            this.MinimumSize = new System.Drawing.Size(800, 410);
            this.Name = "EditorForm";
            this.Text = "Barrier Location Editor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJunctions)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEdges)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewJunctions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn JunctionEID;
        private System.Windows.Forms.Button btnZoomToBarrier;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbFlashBarrierPortion;
        private System.Windows.Forms.RadioButton rbFlashSourceFeature;
        private System.Windows.Forms.RadioButton rbFlashElementPortion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridViewEdges;
        private System.Windows.Forms.DataGridViewTextBoxColumn EdgeEID;
        private System.Windows.Forms.DataGridViewComboBoxColumn Direction;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromPos;
        private System.Windows.Forms.DataGridViewTextBoxColumn toPos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

    }
}