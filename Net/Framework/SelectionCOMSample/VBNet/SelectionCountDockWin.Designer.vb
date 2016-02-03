Imports Microsoft.VisualBasic
Imports System
Namespace SelectionCOMSample
  Partial Public Class SelectionCountDockWin
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary> 
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
	  If disposing AndAlso (components IsNot Nothing) Then
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
	  Dim listViewItem1 As New System.Windows.Forms.ListViewItem("")
	  Dim listViewItem2 As New System.Windows.Forms.ListViewItem("")
	  Me.listView1 = New System.Windows.Forms.ListView()
	  Me.LayerName = New System.Windows.Forms.ColumnHeader()
	  Me.FeatureCount = New System.Windows.Forms.ColumnHeader()
	  Me.SuspendLayout()
	  ' 
	  ' listView1
	  ' 
	  Me.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
	  Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.LayerName, Me.FeatureCount})
	  Me.listView1.Dock = System.Windows.Forms.DockStyle.Fill
	  Me.listView1.Items.AddRange(New System.Windows.Forms.ListViewItem() { listViewItem1, listViewItem2})
	  Me.listView1.Location = New System.Drawing.Point(0, 0)
	  Me.listView1.Name = "listView1"
	  Me.listView1.Size = New System.Drawing.Size(344, 287)
	  Me.listView1.TabIndex = 1
	  Me.listView1.UseCompatibleStateImageBehavior = False
	  ' 
	  ' LayerName
	  ' 
	  Me.LayerName.Text = "Layer Name"
	  Me.LayerName.Width = 120
	  ' 
	  ' FeatureCount
	  ' 
	  Me.FeatureCount.Text = "Selected Features Count"
	  Me.FeatureCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
	  Me.FeatureCount.Width = 200
	  ' 
	  ' SelectionCountDockWin
	  ' 
	  Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
	  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
	  Me.Controls.Add(Me.listView1)
      'Me.Name = "SelectionCountDockWin"
	  Me.Size = New System.Drawing.Size(344, 287)
	  Me.ResumeLayout(False)

	End Sub

	#End Region

	Private listView1 As System.Windows.Forms.ListView
	Private LayerName As System.Windows.Forms.ColumnHeader
	Private FeatureCount As System.Windows.Forms.ColumnHeader

  End Class
End Namespace
