/*

   Copyright 2016 Esri

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

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace MultivariateRenderers
{
	public class PropPageForm : System.Windows.Forms.Form
	{

		private enum eRendererType: int
		{
			eShapePattern,
			eColor,
			eSize
		}

		private bool m_PageIsDirty = false;
		private IComPropertyPageSite m_pSite;
		private IMap m_pMap;
		private IGeoFeatureLayer m_pCurrentLayer;
		private IFeatureRenderer m_pRend;

		private IFeatureRenderer[] m_pShapePatternRendList;
		private IFeatureRenderer[] m_pColorRendList; // we could have separate lists for hue, sat/val, etc, but keep it simple for now
		private IFeatureRenderer[] m_pSizeRendList;

		private EColorCombinationType m_eColorCombinationMethod;
		private IFeatureRenderer m_pShapePatternRend;
		private IFeatureRenderer m_pColorRend1;
		private IFeatureRenderer m_pColorRend2;
		private IFeatureRenderer m_pSizeRend;


	#region  Windows Form Designer generated code 

		public PropPageForm() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.ComboBox cboShapePattern;
		internal System.Windows.Forms.ComboBox cboHue;
		internal System.Windows.Forms.CheckBox chkShapePattern;
		internal System.Windows.Forms.CheckBox chkColor;
		internal System.Windows.Forms.RadioButton radComponents;
		internal System.Windows.Forms.Label lblHue;
		internal System.Windows.Forms.Label lblPrimaryColor;
		internal System.Windows.Forms.RadioButton radCombination;
		internal System.Windows.Forms.ComboBox cboPrimaryColor;
		internal System.Windows.Forms.ComboBox cboSatValue;
		internal System.Windows.Forms.Label lblSatValue;
		internal System.Windows.Forms.Label lblSecondaryColor;
		internal System.Windows.Forms.ComboBox cboSecondaryColor;
		internal System.Windows.Forms.CheckBox chkSize;
		internal System.Windows.Forms.ComboBox cboSize;
        internal System.Windows.Forms.CheckBox chkRotation;
        internal System.Windows.Forms.Button butRotation;
		internal System.Windows.Forms.ComboBox cboSize1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.cboShapePattern = new System.Windows.Forms.ComboBox();
            this.cboHue = new System.Windows.Forms.ComboBox();
            this.chkShapePattern = new System.Windows.Forms.CheckBox();
            this.chkColor = new System.Windows.Forms.CheckBox();
            this.radComponents = new System.Windows.Forms.RadioButton();
            this.lblHue = new System.Windows.Forms.Label();
            this.lblPrimaryColor = new System.Windows.Forms.Label();
            this.radCombination = new System.Windows.Forms.RadioButton();
            this.cboPrimaryColor = new System.Windows.Forms.ComboBox();
            this.cboSatValue = new System.Windows.Forms.ComboBox();
            this.lblSatValue = new System.Windows.Forms.Label();
            this.lblSecondaryColor = new System.Windows.Forms.Label();
            this.cboSecondaryColor = new System.Windows.Forms.ComboBox();
            this.chkSize = new System.Windows.Forms.CheckBox();
            this.chkRotation = new System.Windows.Forms.CheckBox();
            this.butRotation = new System.Windows.Forms.Button();
            this.cboSize1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboShapePattern
            // 
            this.cboShapePattern.Location = new System.Drawing.Point(224, 11);
            this.cboShapePattern.Name = "cboShapePattern";
            this.cboShapePattern.Size = new System.Drawing.Size(121, 21);
            this.cboShapePattern.TabIndex = 24;
            this.cboShapePattern.SelectedIndexChanged += new System.EventHandler(this.cboShapePattern_SelectedIndexChanged);
            // 
            // cboHue
            // 
            this.cboHue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHue.Enabled = false;
            this.cboHue.Location = new System.Drawing.Point(224, 48);
            this.cboHue.Name = "cboHue";
            this.cboHue.Size = new System.Drawing.Size(192, 21);
            this.cboHue.TabIndex = 5;
            this.cboHue.EnabledChanged += new System.EventHandler(this.cboHue_EnabledChanged);
            this.cboHue.SelectedIndexChanged += new System.EventHandler(this.cboHue_SelectedIndexChanged);
            // 
            // chkShapePattern
            // 
            this.chkShapePattern.Location = new System.Drawing.Point(8, 8);
            this.chkShapePattern.Name = "chkShapePattern";
            this.chkShapePattern.Size = new System.Drawing.Size(152, 24);
            this.chkShapePattern.TabIndex = 6;
            this.chkShapePattern.Text = "Shape/Pattern";
            this.chkShapePattern.CheckedChanged += new System.EventHandler(this.chkShapePattern_CheckedChanged);
            // 
            // chkColor
            // 
            this.chkColor.Location = new System.Drawing.Point(8, 32);
            this.chkColor.Name = "chkColor";
            this.chkColor.Size = new System.Drawing.Size(152, 24);
            this.chkColor.TabIndex = 7;
            this.chkColor.Text = "Color";
            this.chkColor.CheckedChanged += new System.EventHandler(this.chkColor_CheckedChanged);
            // 
            // radComponents
            // 
            this.radComponents.Enabled = false;
            this.radComponents.Location = new System.Drawing.Point(24, 56);
            this.radComponents.Name = "radComponents";
            this.radComponents.Size = new System.Drawing.Size(128, 24);
            this.radComponents.TabIndex = 8;
            this.radComponents.Text = "Color Components";
            this.radComponents.CheckedChanged += new System.EventHandler(this.radComponents_CheckedChanged);
            this.radComponents.EnabledChanged += new System.EventHandler(this.radComponents_EnabledChanged);
            // 
            // lblHue
            // 
            this.lblHue.Enabled = false;
            this.lblHue.Location = new System.Drawing.Point(136, 48);
            this.lblHue.Name = "lblHue";
            this.lblHue.Size = new System.Drawing.Size(88, 24);
            this.lblHue.TabIndex = 9;
            this.lblHue.Text = "Hue";
            // 
            // lblPrimaryColor
            // 
            this.lblPrimaryColor.Enabled = false;
            this.lblPrimaryColor.Location = new System.Drawing.Point(136, 104);
            this.lblPrimaryColor.Name = "lblPrimaryColor";
            this.lblPrimaryColor.Size = new System.Drawing.Size(88, 24);
            this.lblPrimaryColor.TabIndex = 12;
            this.lblPrimaryColor.Text = "Color 1";
            // 
            // radCombination
            // 
            this.radCombination.Enabled = false;
            this.radCombination.Location = new System.Drawing.Point(24, 112);
            this.radCombination.Name = "radCombination";
            this.radCombination.Size = new System.Drawing.Size(128, 24);
            this.radCombination.TabIndex = 11;
            this.radCombination.Text = "Color Combination";
            this.radCombination.CheckedChanged += new System.EventHandler(this.radCombination_CheckedChanged);
            this.radCombination.EnabledChanged += new System.EventHandler(this.radCombination_EnabledChanged);
            // 
            // cboPrimaryColor
            // 
            this.cboPrimaryColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrimaryColor.Enabled = false;
            this.cboPrimaryColor.Location = new System.Drawing.Point(224, 104);
            this.cboPrimaryColor.Name = "cboPrimaryColor";
            this.cboPrimaryColor.Size = new System.Drawing.Size(192, 21);
            this.cboPrimaryColor.TabIndex = 10;
            this.cboPrimaryColor.EnabledChanged += new System.EventHandler(this.cboPrimaryColor_EnabledChanged);
            this.cboPrimaryColor.SelectedIndexChanged += new System.EventHandler(this.cboPrimaryColor_SelectedIndexChanged);
            // 
            // cboSatValue
            // 
            this.cboSatValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSatValue.Enabled = false;
            this.cboSatValue.Location = new System.Drawing.Point(224, 72);
            this.cboSatValue.Name = "cboSatValue";
            this.cboSatValue.Size = new System.Drawing.Size(192, 21);
            this.cboSatValue.TabIndex = 13;
            this.cboSatValue.EnabledChanged += new System.EventHandler(this.cboSatValue_EnabledChanged);
            this.cboSatValue.SelectedIndexChanged += new System.EventHandler(this.cboSatValue_selectedIndexChanged);
            // 
            // lblSatValue
            // 
            this.lblSatValue.Enabled = false;
            this.lblSatValue.Location = new System.Drawing.Point(136, 72);
            this.lblSatValue.Name = "lblSatValue";
            this.lblSatValue.Size = new System.Drawing.Size(88, 24);
            this.lblSatValue.TabIndex = 14;
            this.lblSatValue.Text = "Saturation/Value";
            // 
            // lblSecondaryColor
            // 
            this.lblSecondaryColor.Enabled = false;
            this.lblSecondaryColor.Location = new System.Drawing.Point(136, 128);
            this.lblSecondaryColor.Name = "lblSecondaryColor";
            this.lblSecondaryColor.Size = new System.Drawing.Size(88, 24);
            this.lblSecondaryColor.TabIndex = 16;
            this.lblSecondaryColor.Text = "Color 2";
            // 
            // cboSecondaryColor
            // 
            this.cboSecondaryColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSecondaryColor.Enabled = false;
            this.cboSecondaryColor.Location = new System.Drawing.Point(224, 128);
            this.cboSecondaryColor.Name = "cboSecondaryColor";
            this.cboSecondaryColor.Size = new System.Drawing.Size(192, 21);
            this.cboSecondaryColor.TabIndex = 15;
            this.cboSecondaryColor.EnabledChanged += new System.EventHandler(this.cboSecondaryColor_EnabledChanged);
            this.cboSecondaryColor.SelectedIndexChanged += new System.EventHandler(this.cboSecondaryColor_SelectedIndexChanged);
            // 
            // chkSize
            // 
            this.chkSize.Location = new System.Drawing.Point(8, 160);
            this.chkSize.Name = "chkSize";
            this.chkSize.Size = new System.Drawing.Size(152, 24);
            this.chkSize.TabIndex = 18;
            this.chkSize.Text = "Size";
            this.chkSize.CheckedChanged += new System.EventHandler(this.chkSize_CheckedChanged);
            // 
            // chkRotation
            // 
            this.chkRotation.Location = new System.Drawing.Point(8, 192);
            this.chkRotation.Name = "chkRotation";
            this.chkRotation.Size = new System.Drawing.Size(152, 24);
            this.chkRotation.TabIndex = 19;
            this.chkRotation.Text = "Rotation";
            this.chkRotation.CheckedChanged += new System.EventHandler(this.chkRotation_CheckedChanged);
            // 
            // butRotation
            // 
            this.butRotation.Enabled = false;
            this.butRotation.Location = new System.Drawing.Point(224, 192);
            this.butRotation.Name = "butRotation";
            this.butRotation.Size = new System.Drawing.Size(192, 24);
            this.butRotation.TabIndex = 21;
            this.butRotation.Text = "Properties";
            this.butRotation.Click += new System.EventHandler(this.butRotation_Click);
            // 
            // cboSize1
            // 
            this.cboSize1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSize1.Enabled = false;
            this.cboSize1.Location = new System.Drawing.Point(224, 160);
            this.cboSize1.Name = "cboSize1";
            this.cboSize1.Size = new System.Drawing.Size(192, 21);
            this.cboSize1.TabIndex = 23;
            this.cboSize1.SelectedIndexChanged += new System.EventHandler(this.cboSize1_selectedindexchanged);
            // 
            // PropPageForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(424, 285);
            this.Controls.Add(this.cboSize1);
            this.Controls.Add(this.butRotation);
            this.Controls.Add(this.chkRotation);
            this.Controls.Add(this.chkSize);
            this.Controls.Add(this.lblSecondaryColor);
            this.Controls.Add(this.cboSecondaryColor);
            this.Controls.Add(this.lblSatValue);
            this.Controls.Add(this.cboSatValue);
            this.Controls.Add(this.lblPrimaryColor);
            this.Controls.Add(this.radCombination);
            this.Controls.Add(this.cboPrimaryColor);
            this.Controls.Add(this.lblHue);
            this.Controls.Add(this.radComponents);
            this.Controls.Add(this.chkColor);
            this.Controls.Add(this.chkShapePattern);
            this.Controls.Add(this.cboHue);
            this.Controls.Add(this.cboShapePattern);
            this.Name = "PropPageForm";
            this.Text = "ColorPropPageForm";
            this.Load += new System.EventHandler(this.PropPageForm_Load);
            this.ResumeLayout(false);

		}

	#endregion

		public bool IsDirty
		{
			get
			{
				return m_PageIsDirty;
			}
		}

		public IComPropertyPageSite PageSite
		{
			set
			{
				m_pSite = value;
			}
		}

		public void InitControls(IMultivariateRenderer pMultiRend, IMap pMap, IGeoFeatureLayer pGeoLayer)
		{
			// copy properties from the renderer and map to the form

			m_eColorCombinationMethod = pMultiRend.ColorCombinationMethod;
			m_pShapePatternRend = pMultiRend.ShapePatternRend;
			m_pColorRend1 = pMultiRend.ColorRend1;
			m_pColorRend2 = pMultiRend.ColorRend2;
			m_pSizeRend = pMultiRend.SizeRend;

			if (m_pShapePatternRend != null)
			{
				chkShapePattern.CheckState = System.Windows.Forms.CheckState.Checked;
				cboShapePattern.Enabled = true;
			}

			if (m_eColorCombinationMethod == EColorCombinationType.enuComponents)
			{
				radComponents.Checked = true;
				radCombination.Checked = false;
				UpdateColorComb();

			}
			else
			{
                //disabled
                //radComponents.Checked = false;
                //radCombination.Checked = true;

                radComponents.Checked = true;
                radCombination.Checked = false;
				UpdateColorComb();

			}

			if (m_pColorRend1 != null)
			{
				chkColor.CheckState = System.Windows.Forms.CheckState.Checked;
				radComponents.Enabled = true;
                //disabled
                //radCombination.Enabled = true;
                radCombination.Enabled = false;

			}
			if (m_pSizeRend != null)
			{
				chkSize.CheckState = System.Windows.Forms.CheckState.Checked;
				cboSize1.Enabled = true;
			}

			IRotationRenderer pRotRend = null;
			pRotRend = pMultiRend as IRotationRenderer;
			if (pRotRend.RotationField != "")
			{
				chkRotation.CheckState = System.Windows.Forms.CheckState.Checked;
				butRotation.Enabled = true;
			}

            //ITransparencyRenderer pTransRend = null;
            //pTransRend = pMultiRend as ITransparencyRenderer;
            //if (pTransRend.TransparencyField != "")
            //{
            //    chkTransparency.CheckState = System.Windows.Forms.CheckState.Checked;
            //    butTransparency.Enabled = true;
            //}

			m_pMap = pMap;
			m_pCurrentLayer = pGeoLayer;
			m_pRend = pMultiRend as IFeatureRenderer; // we need this object to support the root transparency dialogs

			m_PageIsDirty = false;
		}

		public void InitRenderer(IMultivariateRenderer pMultiRend)
		{
			// copy properties from the form to the renderer

			if (chkShapePattern.CheckState == System.Windows.Forms.CheckState.Checked)
				pMultiRend.ShapePatternRend = m_pShapePatternRend;
			else
				pMultiRend.ShapePatternRend = null;

			if (chkColor.CheckState == System.Windows.Forms.CheckState.Checked)
			{
				pMultiRend.ColorRend1 = m_pColorRend1;
				pMultiRend.ColorRend2 = m_pColorRend2;
				pMultiRend.ColorCombinationMethod = m_eColorCombinationMethod;
			}
			else
			{
				pMultiRend.ColorRend1 = null;
				pMultiRend.ColorRend2 = null;
				pMultiRend.ColorCombinationMethod = EColorCombinationType.enuCIELabMatrix; // default (?)
			}

			if (chkSize.CheckState == System.Windows.Forms.CheckState.Checked)
				pMultiRend.SizeRend = m_pSizeRend;
			else
				pMultiRend.SizeRend = null;

			IRotationRenderer pRotRend = null;
			IRotationRenderer pFormRotRend = null;
			pRotRend = pMultiRend as IRotationRenderer;
			if (chkRotation.CheckState == System.Windows.Forms.CheckState.Checked)
			{
				pFormRotRend = m_pRend as IRotationRenderer;
				pRotRend.RotationField = pFormRotRend.RotationField;
				pRotRend.RotationType = pFormRotRend.RotationType;
			}
			else
			{
				pRotRend.RotationField = "";
				pRotRend.RotationType = esriSymbolRotationType.esriRotateSymbolArithmetic; // default (?)
			}

            //ITransparencyRenderer pTransRend = null;
            //ITransparencyRenderer pFormTransRend = null;
            //pTransRend = pMultiRend as ITransparencyRenderer;
            //if (chkTransparency.CheckState == System.Windows.Forms.CheckState.Checked)
            //{
            //    pFormTransRend = m_pRend as ITransparencyRenderer;
            //    pTransRend.TransparencyField = pFormTransRend.TransparencyField;
            //}
            //else
            //    pTransRend.TransparencyField = "";


		}

		private void PropPageForm_Load(object sender, System.EventArgs e)
		{
			// initialize form controls from data members

			IEnumLayer pGeoLayers = null;
			UID pUID = new UID();
			pUID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}";
			pGeoLayers = m_pMap.get_Layers(pUID, true);
			pGeoLayers.Reset();
			IGeoFeatureLayer pGeoLayer = null;
			IFeatureRenderer pFeatRend = null;
			int iColor = 0;
			int iShapePattern = 0;
			int iSize = 0;

			pGeoLayer = pGeoLayers.Next() as IGeoFeatureLayer;
			string sColor1 = "";
			string sColor2 = "";
			string sShapePattern = "";
			string sSize = "";

			while (pGeoLayer != null)
			{
				// to keep things simple, filter for layers with same feat class geometry (point, line, poly) as current layer
				if ((pGeoLayer.FeatureClass.ShapeType) == (m_pCurrentLayer.FeatureClass.ShapeType))
				{
					// filter out the current layer
					if (! (pGeoLayer == m_pCurrentLayer))
					{
						pFeatRend = pGeoLayer.Renderer;

						// filter for only layers currently assigned a renderer that is valid for each renderer type (shape, color, size)
						if (RendererIsValidForType(pFeatRend, eRendererType.eColor))
						{
							iColor = iColor + 1;
							System.Array.Resize(ref m_pColorRendList, iColor + 1);
							m_pColorRendList[iColor - 1] = pFeatRend;

							cboHue.Items.Add(pGeoLayer.Name);
							cboSatValue.Items.Add(pGeoLayer.Name);
							cboPrimaryColor.Items.Add(pGeoLayer.Name);
							cboSecondaryColor.Items.Add(pGeoLayer.Name);


							if (CompareRenderers(pGeoLayer.Renderer, m_pColorRend1))
									sColor1 = pGeoLayer.Name;
							if (CompareRenderers(pGeoLayer.Renderer, m_pColorRend2))
									sColor2 = pGeoLayer.Name;
						}

						if (RendererIsValidForType(pFeatRend, eRendererType.eShapePattern))
						{
							iShapePattern = iShapePattern + 1;
							System.Array.Resize(ref m_pShapePatternRendList, iShapePattern + 1);
							m_pShapePatternRendList[iShapePattern - 1] = pFeatRend;

							cboShapePattern.Items.Add(pGeoLayer.Name);

                            //if (pGeoLayer.Renderer == m_pShapePatternRend)
                            //        sShapePattern = pGeoLayer.Name;
                            if (CompareRenderers(pGeoLayer.Renderer, m_pShapePatternRend))
                                sShapePattern = pGeoLayer.Name;
						}

						if (RendererIsValidForType(pFeatRend, eRendererType.eSize))
						{
							iSize = iSize + 1;
							System.Array.Resize(ref m_pSizeRendList, iSize + 1);
							m_pSizeRendList[iSize - 1] = pFeatRend;

							cboSize1.Items.Add(pGeoLayer.Name);

                            //if (pGeoLayer.Renderer == m_pSizeRend)
                            //        sSize = pGeoLayer.Name;
                            if (CompareRenderers(pGeoLayer.Renderer, m_pSizeRend))
                                sSize = pGeoLayer.Name;
						}

					}
				}
				pGeoLayer = pGeoLayers.Next() as IGeoFeatureLayer;
			}

			// select correct items in combos
			cboShapePattern.Text = sShapePattern;
			if (radComponents.Checked)
			{
				cboHue.Text = sColor1;
				cboSatValue.Text = sColor2;
			}
			else
			{
				cboPrimaryColor.Text = sColor1;
				cboSecondaryColor.Text = sColor2;
			}
			cboSize1.Text = sSize;

			// disable if there aren't any layers in the map of the correct type
			if (iShapePattern <= 0)
					cboShapePattern.Enabled = false;

			if (iColor <= 0)
			{
				if (radComponents.Checked)
				{
					cboHue.Enabled = false;
					cboSatValue.Enabled = false;
				}
				else
				{
					cboPrimaryColor.Enabled = false;
					cboSecondaryColor.Enabled = false;
				}
			}

			if (iSize <= 0)
					cboSize1.Enabled = false;
		}

		private void cboShapePattern_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_pShapePatternRend = m_pShapePatternRendList[cboShapePattern.SelectedIndex];
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

		private void cboHue_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_pColorRend1 = m_pColorRendList[cboHue.SelectedIndex];
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

		private void cboSatValue_selectedIndexChanged(object sender, System.EventArgs e)
		{
			m_pColorRend2 = m_pColorRendList[cboSatValue.SelectedIndex];
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

		private void cboPrimaryColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_pColorRend1 = m_pColorRendList[cboPrimaryColor.SelectedIndex];
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

		private void cboSecondaryColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_pColorRend2 = m_pColorRendList[cboSecondaryColor.SelectedIndex];
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

		private void cboSize1_selectedindexchanged(object sender, System.EventArgs e)
		{
			m_pSizeRend = m_pSizeRendList[cboSize1.SelectedIndex];
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

		private void radComponents_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateColorComb();
		}

		private void radCombination_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateColorComb();
		}

		private void UpdateColorComb()
		{

			if (radComponents.Checked)
			{
				m_eColorCombinationMethod = EColorCombinationType.enuComponents;

				cboHue.Enabled = true;
				cboSatValue.Enabled = true;
				cboPrimaryColor.Enabled = false;
				cboSecondaryColor.Enabled = false;
			}
			else
			{
				m_eColorCombinationMethod = EColorCombinationType.enuCIELabMatrix;

				cboHue.Enabled = false;
				cboSatValue.Enabled = false;
				cboPrimaryColor.Enabled = true;
				cboSecondaryColor.Enabled = true;
			}

			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;

		}

		private bool RendererIsValidForType(IFeatureRenderer pFeatRend, eRendererType eMultiRendType)
		{
			// indicates whether or not pFeatRend is valid for the eMultiRendType for the current layer
			// e.g. if pFeatRend is an IProportionalSymbolRenderer, then it's valid for eMultiRendType = eSize 

			ILegendInfo pLegendInfo = null;

			if (eMultiRendType == eRendererType.eShapePattern)
				return pFeatRend is IUniqueValueRenderer;
			else if (eMultiRendType == eRendererType.eColor)
			{
				pLegendInfo = pFeatRend as ILegendInfo;
				return (pFeatRend is IUniqueValueRenderer) | (pFeatRend is IClassBreaksRenderer & ! pLegendInfo.SymbolsAreGraduated);
			}
			else // size
			{
				pLegendInfo = pFeatRend as ILegendInfo;
				return (pFeatRend is IClassBreaksRenderer & pLegendInfo.SymbolsAreGraduated) | (pFeatRend is IProportionalSymbolRenderer);
			}


		}

		private void butRotation_Click(object sender, System.EventArgs e)
		{
			IRendererUIDialog2 pRendUIDlg2 = null;
			pRendUIDlg2 = new MarkerRotationDialog() as IRendererUIDialog2;
			pRendUIDlg2.FeatureLayer = m_pCurrentLayer;
			pRendUIDlg2.Renderer = m_pRend;
			SecondaryForm pMyForm = null;
			pMyForm = new SecondaryForm();
			pRendUIDlg2.DoModal(pMyForm.Handle.ToInt32());

		}

		private void chkShapePattern_CheckedChanged(object sender, System.EventArgs e)
		{
			cboShapePattern.Enabled = (chkShapePattern.CheckState == System.Windows.Forms.CheckState.Checked);
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

		private void chkColor_CheckedChanged(object sender, System.EventArgs e)
		{
			radComponents.Enabled = (chkColor.CheckState == System.Windows.Forms.CheckState.Checked);
            //radCombination.Enabled = (chkColor.CheckState == System.Windows.Forms.CheckState.Checked);
            radCombination.Enabled = false;

			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

		private void chkSize_CheckedChanged(object sender, System.EventArgs e)
		{
			cboSize1.Enabled = (chkSize.CheckState == System.Windows.Forms.CheckState.Checked);
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}


		private void chkRotation_CheckedChanged(object sender, System.EventArgs e)
		{
			butRotation.Enabled = (chkRotation.CheckState == System.Windows.Forms.CheckState.Checked);
			if (m_pSite != null)
					m_pSite.PageChanged();
			m_PageIsDirty = true;
		}

        //private void chkTransparency_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    butTransparency.Enabled = (chkTransparency.CheckState == System.Windows.Forms.CheckState.Checked);
        //    if (m_pSite != null)
        //            m_pSite.PageChanged();
        //    m_PageIsDirty = true;
        //}

		private void radComponents_EnabledChanged(object sender, System.EventArgs e)
		{

			cboHue.Enabled = radComponents.Enabled & radComponents.Checked;
			cboSatValue.Enabled = radComponents.Enabled & radComponents.Checked;
		}

		private void radCombination_EnabledChanged(object sender, System.EventArgs e)
		{
            //disabled
            //cboPrimaryColor.Enabled = radCombination.Enabled & radCombination.Checked;
            //cboSecondaryColor.Enabled = radCombination.Enabled & radCombination.Checked;
            cboPrimaryColor.Enabled = false;
            cboSecondaryColor.Enabled = false;
		}

		private void cboHue_EnabledChanged(object sender, System.EventArgs e)
		{

			lblHue.Enabled = cboHue.Enabled;
		}

		private void cboSatValue_EnabledChanged(object sender, System.EventArgs e)
		{

			lblSatValue.Enabled = cboSatValue.Enabled;
		}

		private void cboPrimaryColor_EnabledChanged(object sender, System.EventArgs e)
		{

			lblPrimaryColor.Enabled = cboPrimaryColor.Enabled;
		}

		private void cboSecondaryColor_EnabledChanged(object sender, System.EventArgs e)
		{

			lblSecondaryColor.Enabled = cboSecondaryColor.Enabled;
		}

		private bool CompareRenderers(IFeatureRenderer pRend, IFeatureRenderer pCheckRend)
		{

			if (pRend is IClassBreaksRenderer)
			{
				// type
				if (! (pCheckRend is IClassBreaksRenderer))
						return false;

				IClassBreaksRenderer pCBRend = null;
				pCBRend = pRend as IClassBreaksRenderer;
				IClassBreaksRenderer pCBCheckRend = null;
				pCBCheckRend = pCheckRend as IClassBreaksRenderer;

				// break count
				if (pCBRend.BreakCount != pCBCheckRend.BreakCount)
						return false;

				// field
				if (pCBRend.Field != pCBCheckRend.Field)
						return false;

			}

			return true;

		}
	}
} //end of root namespace