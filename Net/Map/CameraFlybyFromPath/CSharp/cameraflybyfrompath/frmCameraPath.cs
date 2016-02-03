using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CameraFlybyFromPath;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace cameraflybyfrompath
{
	/// <summary>
	/// Summary description for frmCameraPath.
	/// </summary>
	public class frmCameraPath : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox_PathSource;
		private System.Windows.Forms.RadioButton radiobutton_Sel_line_feat;
		private System.Windows.Forms.CheckBox checkBox_ReverseOrder;
		private System.Windows.Forms.Label label_VertOffset;
		private System.Windows.Forms.TextBox textBox_VertOffset;
		private System.Windows.Forms.Label label_Simp_Factor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox_PathDest;
		private System.Windows.Forms.RadioButton radioButton_flyby;
		private System.Windows.Forms.RadioButton radioButton_currentTarget;
		private System.Windows.Forms.RadioButton radioButton_currentObserver;
		private System.Windows.Forms.Label label_TrackName;
		private System.Windows.Forms.TextBox textBox_TrackName;
		private System.Windows.Forms.Button button_Import;
		private System.Windows.Forms.Button button_Cancel;
		private System.Windows.Forms.GroupBox groupBox_TrackProps;
		private System.Windows.Forms.CheckBox checkBox_Overwrite;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TrackBar trackBarSimplificationFactor;
		private System.Windows.Forms.ComboBox comboBoxLayers;

		private IGlobe globe;
		private IArray layerArray = new ArrayClass();
		
		public frmCameraPath()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox_PathSource = new System.Windows.Forms.GroupBox();
            this.comboBoxLayers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_Simp_Factor = new System.Windows.Forms.Label();
            this.textBox_VertOffset = new System.Windows.Forms.TextBox();
            this.label_VertOffset = new System.Windows.Forms.Label();
            this.checkBox_ReverseOrder = new System.Windows.Forms.CheckBox();
            this.radiobutton_Sel_line_feat = new System.Windows.Forms.RadioButton();
            this.trackBarSimplificationFactor = new System.Windows.Forms.TrackBar();
            this.groupBox_PathDest = new System.Windows.Forms.GroupBox();
            this.radioButton_currentObserver = new System.Windows.Forms.RadioButton();
            this.radioButton_currentTarget = new System.Windows.Forms.RadioButton();
            this.radioButton_flyby = new System.Windows.Forms.RadioButton();
            this.label_TrackName = new System.Windows.Forms.Label();
            this.textBox_TrackName = new System.Windows.Forms.TextBox();
            this.button_Import = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.checkBox_Overwrite = new System.Windows.Forms.CheckBox();
            this.groupBox_TrackProps = new System.Windows.Forms.GroupBox();
            this.groupBox_PathSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSimplificationFactor)).BeginInit();
            this.groupBox_PathDest.SuspendLayout();
            this.groupBox_TrackProps.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_PathSource
            // 
            this.groupBox_PathSource.Controls.Add(this.comboBoxLayers);
            this.groupBox_PathSource.Controls.Add(this.label1);
            this.groupBox_PathSource.Controls.Add(this.label2);
            this.groupBox_PathSource.Controls.Add(this.label_Simp_Factor);
            this.groupBox_PathSource.Controls.Add(this.textBox_VertOffset);
            this.groupBox_PathSource.Controls.Add(this.label_VertOffset);
            this.groupBox_PathSource.Controls.Add(this.checkBox_ReverseOrder);
            this.groupBox_PathSource.Controls.Add(this.radiobutton_Sel_line_feat);
            this.groupBox_PathSource.Controls.Add(this.trackBarSimplificationFactor);
            this.groupBox_PathSource.Location = new System.Drawing.Point(8, 8);
            this.groupBox_PathSource.Name = "groupBox_PathSource";
            this.groupBox_PathSource.Size = new System.Drawing.Size(296, 160);
            this.groupBox_PathSource.TabIndex = 0;
            this.groupBox_PathSource.TabStop = false;
            this.groupBox_PathSource.Text = "Path source";
            // 
            // comboBoxLayers
            // 
            this.comboBoxLayers.Location = new System.Drawing.Point(152, 24);
            this.comboBoxLayers.Name = "comboBoxLayers";
            this.comboBoxLayers.Size = new System.Drawing.Size(136, 21);
            this.comboBoxLayers.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(258, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "High";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(117, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Low";
            // 
            // label_Simp_Factor
            // 
            this.label_Simp_Factor.Location = new System.Drawing.Point(16, 112);
            this.label_Simp_Factor.Name = "label_Simp_Factor";
            this.label_Simp_Factor.Size = new System.Drawing.Size(104, 24);
            this.label_Simp_Factor.TabIndex = 5;
            this.label_Simp_Factor.Text = "Simplification factor";
            // 
            // textBox_VertOffset
            // 
            this.textBox_VertOffset.Location = new System.Drawing.Point(88, 72);
            this.textBox_VertOffset.Name = "textBox_VertOffset";
            this.textBox_VertOffset.Size = new System.Drawing.Size(56, 20);
            this.textBox_VertOffset.TabIndex = 3;
            this.textBox_VertOffset.Text = "0.0";
            this.textBox_VertOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_VertOffset
            // 
            this.label_VertOffset.Location = new System.Drawing.Point(16, 80);
            this.label_VertOffset.Name = "label_VertOffset";
            this.label_VertOffset.Size = new System.Drawing.Size(88, 16);
            this.label_VertOffset.TabIndex = 2;
            this.label_VertOffset.Text = "Vertical offset";
            // 
            // checkBox_ReverseOrder
            // 
            this.checkBox_ReverseOrder.Location = new System.Drawing.Point(16, 48);
            this.checkBox_ReverseOrder.Name = "checkBox_ReverseOrder";
            this.checkBox_ReverseOrder.Size = new System.Drawing.Size(144, 24);
            this.checkBox_ReverseOrder.TabIndex = 1;
            this.checkBox_ReverseOrder.Text = "Apply in reverse order";
            // 
            // radiobutton_Sel_line_feat
            // 
            this.radiobutton_Sel_line_feat.Checked = true;
            this.radiobutton_Sel_line_feat.Location = new System.Drawing.Point(16, 24);
            this.radiobutton_Sel_line_feat.Name = "radiobutton_Sel_line_feat";
            this.radiobutton_Sel_line_feat.Size = new System.Drawing.Size(144, 24);
            this.radiobutton_Sel_line_feat.TabIndex = 0;
            this.radiobutton_Sel_line_feat.TabStop = true;
            this.radiobutton_Sel_line_feat.Text = "Selected line feature in";
            // 
            // trackBarSimplificationFactor
            // 
            this.trackBarSimplificationFactor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarSimplificationFactor.Location = new System.Drawing.Point(120, 108);
            this.trackBarSimplificationFactor.Maximum = 100;
            this.trackBarSimplificationFactor.Name = "trackBarSimplificationFactor";
            this.trackBarSimplificationFactor.Size = new System.Drawing.Size(168, 45);
            this.trackBarSimplificationFactor.TabIndex = 4;
            this.trackBarSimplificationFactor.Value = 5;
            // 
            // groupBox_PathDest
            // 
            this.groupBox_PathDest.Controls.Add(this.radioButton_currentObserver);
            this.groupBox_PathDest.Controls.Add(this.radioButton_currentTarget);
            this.groupBox_PathDest.Controls.Add(this.radioButton_flyby);
            this.groupBox_PathDest.Location = new System.Drawing.Point(8, 176);
            this.groupBox_PathDest.Name = "groupBox_PathDest";
            this.groupBox_PathDest.Size = new System.Drawing.Size(296, 104);
            this.groupBox_PathDest.TabIndex = 1;
            this.groupBox_PathDest.TabStop = false;
            this.groupBox_PathDest.Text = "Path destination";
            // 
            // radioButton_currentObserver
            // 
            this.radioButton_currentObserver.Location = new System.Drawing.Point(16, 72);
            this.radioButton_currentObserver.Name = "radioButton_currentObserver";
            this.radioButton_currentObserver.Size = new System.Drawing.Size(272, 16);
            this.radioButton_currentObserver.TabIndex = 2;
            this.radioButton_currentObserver.Text = "Move target along path with current observer";
            // 
            // radioButton_currentTarget
            // 
            this.radioButton_currentTarget.Location = new System.Drawing.Point(16, 48);
            this.radioButton_currentTarget.Name = "radioButton_currentTarget";
            this.radioButton_currentTarget.Size = new System.Drawing.Size(272, 16);
            this.radioButton_currentTarget.TabIndex = 1;
            this.radioButton_currentTarget.Text = "Move observer along path with current target";
            // 
            // radioButton_flyby
            // 
            this.radioButton_flyby.Checked = true;
            this.radioButton_flyby.Location = new System.Drawing.Point(16, 24);
            this.radioButton_flyby.Name = "radioButton_flyby";
            this.radioButton_flyby.Size = new System.Drawing.Size(272, 16);
            this.radioButton_flyby.TabIndex = 0;
            this.radioButton_flyby.TabStop = true;
            this.radioButton_flyby.Text = "Move both observer and target along path (fly by)";
            // 
            // label_TrackName
            // 
            this.label_TrackName.Location = new System.Drawing.Point(16, 24);
            this.label_TrackName.Name = "label_TrackName";
            this.label_TrackName.Size = new System.Drawing.Size(88, 16);
            this.label_TrackName.TabIndex = 2;
            this.label_TrackName.Text = "Track name:";
            // 
            // textBox_TrackName
            // 
            this.textBox_TrackName.Location = new System.Drawing.Point(96, 23);
            this.textBox_TrackName.Name = "textBox_TrackName";
            this.textBox_TrackName.Size = new System.Drawing.Size(192, 20);
            this.textBox_TrackName.TabIndex = 3;
            this.textBox_TrackName.Text = "Track from path";
            // 
            // button_Import
            // 
            this.button_Import.Location = new System.Drawing.Point(144, 368);
            this.button_Import.Name = "button_Import";
            this.button_Import.Size = new System.Drawing.Size(80, 24);
            this.button_Import.TabIndex = 4;
            this.button_Import.Text = "Import";
            this.button_Import.Click += new System.EventHandler(this.button_Import_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(232, 368);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(72, 24);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // checkBox_Overwrite
            // 
            this.checkBox_Overwrite.Checked = true;
            this.checkBox_Overwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Overwrite.Enabled = false;
            this.checkBox_Overwrite.Location = new System.Drawing.Point(16, 48);
            this.checkBox_Overwrite.Name = "checkBox_Overwrite";
            this.checkBox_Overwrite.Size = new System.Drawing.Size(184, 16);
            this.checkBox_Overwrite.TabIndex = 7;
            this.checkBox_Overwrite.Text = "Overwrite last imported track";
            // 
            // groupBox_TrackProps
            // 
            this.groupBox_TrackProps.Controls.Add(this.textBox_TrackName);
            this.groupBox_TrackProps.Controls.Add(this.label_TrackName);
            this.groupBox_TrackProps.Controls.Add(this.checkBox_Overwrite);
            this.groupBox_TrackProps.Location = new System.Drawing.Point(8, 288);
            this.groupBox_TrackProps.Name = "groupBox_TrackProps";
            this.groupBox_TrackProps.Size = new System.Drawing.Size(296, 72);
            this.groupBox_TrackProps.TabIndex = 8;
            this.groupBox_TrackProps.TabStop = false;
            this.groupBox_TrackProps.Text = "Animation track properties";
            // 
            // frmCameraPath
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(312, 398);
            this.Controls.Add(this.groupBox_TrackProps);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Import);
            this.Controls.Add(this.groupBox_PathDest);
            this.Controls.Add(this.groupBox_PathSource);
            this.Name = "frmCameraPath";
            this.Text = "Camera flyby from path";
            this.Load += new System.EventHandler(this.frmCameraPath_Load);
            this.groupBox_PathSource.ResumeLayout(false);
            this.groupBox_PathSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSimplificationFactor)).EndInit();
            this.groupBox_PathDest.ResumeLayout(false);
            this.groupBox_TrackProps.ResumeLayout(false);
            this.groupBox_TrackProps.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
				
		
		private void frmCameraPath_Load(object sender, System.EventArgs e)
		{
            IScene scene = globe.GlobeDisplay.Scene;

            //check
            IEnumLayer enumFtrLayers = scene.get_Layers(null, true);
            enumFtrLayers.Reset();
            ILayer lyr = null;
            IFeatureLayer fLayer = null;
            lyr = enumFtrLayers.Next();
            do
            {
                if (lyr is IFeatureLayer)
                {
                    fLayer = lyr as IFeatureLayer;
                    break;
                }

                lyr = enumFtrLayers.Next();
            } while (lyr != null);
            
            if (fLayer != null)
            {
                UID uid = new UIDClass();
                uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}";

                IEnumLayer enumLayers = scene.get_Layers(uid, true);
                enumLayers.Reset();
                fLayer = (IFeatureLayer)enumLayers.Next();

                while (fLayer != null)
                {
                    if (fLayer.FeatureClass.ShapeType.ToString() == esriGeometryType.esriGeometryPolyline.ToString() || fLayer.FeatureClass.ShapeType.ToString() == esriGeometryType.esriGeometryLine.ToString())
                    {
                        comboBoxLayers.Items.Add(fLayer.Name);
                    }
                    layerArray.Add(fLayer);

                    fLayer = (IFeatureLayer)enumLayers.Next();
                }
            }

            else
            {
                MessageBox.Show("You don't have any line feature layers in your document");
            }
		}

		private void button_Cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button_Import_Click(object sender, System.EventArgs e)
		{
			// point to the first selected feature:
			ESRI.ArcGIS.Animation.IAGImportPathOptions AGImportPathOptionsCls = new ESRI.ArcGIS.Animation.AGImportPathOptionsClass();

			// Set properties for AGImportPathOptions
			AGImportPathOptionsCls.BasicMap = (ESRI.ArcGIS.Carto.IBasicMap)globe; // Explicit Cast
			AGImportPathOptionsCls.AnimationTracks = (ESRI.ArcGIS.Animation.IAGAnimationTracks)globe; // Explicit Cast
			AGImportPathOptionsCls.AnimationType = new ESRI.ArcGIS.GlobeCore.AnimationTypeGlobeCameraClass();
			AGImportPathOptionsCls.AnimatedObject = globe.GlobeDisplay.ActiveViewer.Camera; 
			
			if (this.radioButton_flyby.Checked == true)
			{
				AGImportPathOptionsCls.ConversionType = esriFlyFromPathType.esriFlyFromPathObsAndTarget;
				AGImportPathOptionsCls.PutAngleCalculationMethods(esriPathAngleCalculation.esriAngleAddRelative, esriPathAngleCalculation.esriAngleAddRelative, esriPathAngleCalculation.esriAngleAddRelative);
				AGImportPathOptionsCls.PutAngleCalculationValues(0.0, 0.0, 0.0);
			}
			else if (this.radioButton_currentTarget.Checked == true)
			{
				AGImportPathOptionsCls.ConversionType = esriFlyFromPathType.esriFlyFromPathObserver;
			}
			else if (this.radioButton_currentObserver.Checked == true)
			{
				AGImportPathOptionsCls.ConversionType = esriFlyFromPathType.esriFlyFromPathTarget;
			}
			
			double pAzimuth, pInclination, pRollVal;
			AGImportPathOptionsCls.GetAngleCalculationValues(out pAzimuth, out pInclination, out pRollVal);
			
			AGImportPathOptionsCls.LookaheadFactor = this.trackBarSimplificationFactor.Value/100;
			AGImportPathOptionsCls.TrackName = this.textBox_TrackName.Text;
			AGImportPathOptionsCls.OverwriteExisting = Convert.ToBoolean(this.checkBox_Overwrite.CheckState);
			AGImportPathOptionsCls.VerticalOffset = Convert.ToDouble(this.textBox_VertOffset.Text);
			AGImportPathOptionsCls.ReversePath = Convert.ToBoolean(this.checkBox_ReverseOrder.CheckState);
			
			// get the layer selected in the combo box
			if (this.comboBoxLayers.SelectedIndex == -1) 
			{
				MessageBox.Show("Please select a layer before you proceed");
			}
			else
			{
				//set the layer based on the item selected in the combo box
				ESRI.ArcGIS.Carto.ILayer layer = (ESRI.ArcGIS.Carto.ILayer) layerArray.get_Element(this.comboBoxLayers.SelectedIndex); // Explicit Cast
				
				// Get the line feature selected in the layer
				ESRI.ArcGIS.Carto.IFeatureLayer featureLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)layer; // Explicit Cast
				ESRI.ArcGIS.Carto.IFeatureSelection featureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)layer; // Explicit Cast
				ESRI.ArcGIS.Geodatabase.ISelectionSet selectionSet = featureSelection.SelectionSet;
				ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = featureLayer.FeatureClass;
				string shapeField = featureClass.ShapeFieldName;
				ESRI.ArcGIS.Geodatabase.ISpatialFilter spatialFilterCls = new ESRI.ArcGIS.Geodatabase.SpatialFilterClass();
				
				IScene scene = globe.GlobeDisplay.Scene;

				ESRI.ArcGIS.Geometry.ISpatialReference spatialReference = scene.SpatialReference;
				spatialFilterCls.GeometryField = shapeField;
				spatialFilterCls.set_OutputSpatialReference(shapeField, spatialReference);
				ESRI.ArcGIS.Geodatabase.ICursor cursor;
				selectionSet.Search(spatialFilterCls, true, out cursor);
				ESRI.ArcGIS.Geodatabase.IFeatureCursor featureCursor = (ESRI.ArcGIS.Geodatabase.IFeatureCursor)cursor; // Explicit Cast

				ESRI.ArcGIS.Geodatabase.IFeature lineFeature;
				lineFeature = featureCursor.NextFeature();
				if (lineFeature == null)
				{
					MessageBox.Show("Please select a feature in the feature layer selected");
				}
				else
				{
					CreateFlybyFromPathAnimation(globe, lineFeature, AGImportPathOptionsCls);
				}
            }
			this.Close();
		}

		public void SetVariables(IGlobe pGlobe)
		{
			globe = pGlobe;
		}
		
		private void CreateFlybyFromPathAnimation(ESRI.ArcGIS.GlobeCore.IGlobe globe, ESRI.ArcGIS.Geodatabase.IFeature lineFeature, ESRI.ArcGIS.Animation.IAGImportPathOptions AGImportPathOptionsCls)
		{
			ESRI.ArcGIS.GlobeCore.IGlobeDisplay globeDisplay = globe.GlobeDisplay;
			ESRI.ArcGIS.Analyst3D.IScene scene = globeDisplay.Scene;

			// Get a handle to the animation extension
			ESRI.ArcGIS.Analyst3D.IBasicScene2 basicScene2 = (ESRI.ArcGIS.Analyst3D.IBasicScene2)scene; // Explicit Cast
			ESRI.ArcGIS.Animation.IAnimationExtension animationExtension = basicScene2.AnimationExtension;

			// Get the geometry of the line feature
			ESRI.ArcGIS.Geometry.IGeometry geometry = lineFeature.Shape;

			// Create AGAnimationUtils and AGImportPathOptions objects
			ESRI.ArcGIS.Animation.IAGAnimationUtils AGAnimationUtilsCls = new ESRI.ArcGIS.Animation.AGAnimationUtilsClass();
			AGImportPathOptionsCls.PathGeometry = geometry;

			AGImportPathOptionsCls.AnimationEnvironment = animationExtension.AnimationEnvironment;
			ESRI.ArcGIS.Animation.IAGAnimationContainer AGAnimationContainer = animationExtension.AnimationTracks.AnimationObjectContainer;

			// Call "CreateFlybyFromPath" method
			AGAnimationUtilsCls.CreateFlybyFromPath(AGAnimationContainer, AGImportPathOptionsCls);
		}

	}
}
