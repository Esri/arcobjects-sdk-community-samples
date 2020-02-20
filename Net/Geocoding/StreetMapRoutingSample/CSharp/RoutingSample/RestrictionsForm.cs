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
using System;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;

namespace RoutingSample
{
	public partial class RestrictionsForm
	{

	#region Public methods

		public RestrictionsForm() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			m_arrRestrictions = new System.Collections.ArrayList();
		}

        // Receives restriction from route and inits controls 
		public void Init(ISMRouter objRouter)
		{
			// clear controls
			ClearAll();

			try
			{
				this.SuspendLayout();
				m_pnlRestrictions.SuspendLayout();

				// Get Net attributes
				ISMNetAttributesCollection objAttrColl = null;
				objAttrColl = objRouter.NetAttributes;

				// attributes count
				int nCount = objAttrColl.Count;

				for (int i = 0; i < nCount; i++)
				{
					// get attribute
					SMNetAttribute objAttr = null;
					objAttr = objAttrColl.get_Item(i);

					if ((objAttr) is ISMNetAttribute2)
					{
						ISMNetAttribute2 objAttr2 = null;
						objAttr2 = objAttr as ISMNetAttribute2;

						// If Usage type is restriction
						esriSMNetAttributeUsageType eType = 0;
						eType = objAttr2.UsageType;

						if (eType == esriSMNetAttributeUsageType.esriSMNAUTRestrictionBoolean 
							|| eType == esriSMNetAttributeUsageType.esriSMNAUTRestrictionMinAllowed 
							|| eType == esriSMNetAttributeUsageType.esriSMNAUTRestrictionMaxAllowed)
						{

							// create control for restriction
							RestrictControl ctrlRestriction = null;
							ctrlRestriction = new RestrictControl();

							// create restriction info
							RestrictionsInfo objRestriction = null;
							objRestriction = new RestrictionsInfo();

							// Init info
							objRestriction.m_objAttr2 = objAttr2;
							objRestriction.m_strName = objAttr2.Name;
							objRestriction.m_bChecked = false;
							objRestriction.m_eType = RestrictControl.ERSType.eStrict;
							objRestriction.m_bUseParameter = false;

							if (eType != esriSMNetAttributeUsageType.esriSMNAUTRestrictionBoolean)
							{
								objRestriction.m_bUseParameter = true;
								objRestriction.m_dParameter = 0.0;
							}

							// Add controls (reverse order)
							ctrlRestriction.Dock = DockStyle.Top;
							m_pnlRestrictions.Controls.Add(ctrlRestriction);

							m_arrRestrictions.Add(objRestriction);
						}
					}
				}

				// Set restriction info to controls
				UpdateControls();

			}
			catch (Exception ex)
			{
				ClearAll();
			}
			finally
			{
				m_pnlRestrictions.ResumeLayout(false);
				this.ResumeLayout(false);
			}
		}

		// Setups router restrictions
		public void SetupRouter(ISMRouter objRouter)
		{
			ISMRouterSetup2 objRouterSetup = null;
			objRouterSetup = objRouter as ISMRouterSetup2;

			try
			{
				// Clear all previous changes
				objRouterSetup.ClearAllRestrictions();

				foreach (RestrictionsInfo objInfo in m_arrRestrictions)
				{
					if (objInfo.m_bChecked)
					{
						// New restriction
						ISMRestriction objRestr = new SMRestrictionClass();

						objRestr.Attribute = objInfo.m_objAttr2 as SMNetAttribute;

						// restriction type
						if (objInfo.m_eType == RestrictControl.ERSType.eStrict)
							objRestr.Type = esriSMRestrictionType.esriSMRTStrict;
						else
							objRestr.Type = esriSMRestrictionType.esriSMRTRelaxed;

                        // Parameter
						if (objInfo.m_bUseParameter)
							objRestr.Param = objInfo.m_dParameter;

						// Add restriction to router
						objRouterSetup.SetRestriction(objRestr as SMRestriction);
					}
				}
			}
			catch (Exception ex)
			{
				objRouterSetup.ClearAllRestrictions();
				MessageBox.Show("Cannot set restrictions.");
			}
		}

	#endregion

	#region Private methods

		// Clears all
		private void ClearAll()
		{
			m_arrRestrictions.Clear();
			m_pnlRestrictions.Controls.Clear();
		}

		// Updates controls values
		private void UpdateControls()
		{
			int nCount = m_arrRestrictions.Count;
			for (int i = 0; i < nCount; i++)
			{
				RestrictControl ctrlRestriction = null;
				ctrlRestriction = m_pnlRestrictions.Controls[nCount - 1 - i] as RestrictControl;

				ctrlRestriction.TabIndex = i;
				RestrictionsInfo objRestriction = m_arrRestrictions[i] as RestrictionsInfo;
				ctrlRestriction.RSName = objRestriction.m_strName;
				ctrlRestriction.RSChecked = objRestriction.m_bChecked;
				ctrlRestriction.RSType = objRestriction.m_eType;
				ctrlRestriction.RSUseParameter = objRestriction.m_bUseParameter;

				if (objRestriction.m_bUseParameter)
					ctrlRestriction.RSParameter = objRestriction.m_dParameter;
			}
		}

		// Updates restrictions info 
		private void UpdateInfo()
		{
			int nCount = m_arrRestrictions.Count;
			for (int i = 0; i < nCount; i++)
			{
				RestrictControl ctrlRestriction = null;
				ctrlRestriction = m_pnlRestrictions.Controls[nCount - 1 - i] as RestrictControl;

				RestrictionsInfo objRestriction = m_arrRestrictions[i] as RestrictionsInfo;
				objRestriction.m_strName = ctrlRestriction.RSName;
				objRestriction.m_bChecked = ctrlRestriction.RSChecked;
				objRestriction.m_eType = ctrlRestriction.RSType;

				if (objRestriction.m_bUseParameter)
					objRestriction.m_dParameter = ctrlRestriction.RSParameter;
			}
		}

		// Updates restrictions info from controls
		private void m_btnOK_Click(object sender, System.EventArgs e)
		{
			UpdateInfo();
		}

		// Updates controls from restrictions info 
		private void m_btnCancel_Click(object sender, System.EventArgs e)
		{
			UpdateControls();
		}

	#endregion

	#region Private members
		// Restrictions data for Router.
		// NOTE: Restriction controls in reverse order
		private System.Collections.ArrayList m_arrRestrictions;
	#endregion

	}

// Info for one restriction
	public class RestrictionsInfo
	{
		public ISMNetAttribute2 m_objAttr2;
		public string m_strName;
		public bool m_bChecked;
		public RestrictControl.ERSType m_eType;
		public bool m_bUseParameter;
		public double m_dParameter;
	}

} //end of root namespace