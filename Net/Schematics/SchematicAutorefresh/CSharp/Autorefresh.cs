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
// Copyright 2011 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.2/userestrictions.txt.
// 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.SchematicControls;
using ESRI.ArcGIS.SchematicUI;

namespace Autorefresh
{
	public class Autorefresh : ESRI.ArcGIS.Desktop.AddIns.Button
	{
		IApplication m_application;                        // Valid upon create
		ISchematicInMemoryDiagram m_schematicInMemoryDiagram;
		ISchematicTarget m_schematicTarget = null;
		protected FormAutoRefresh m_formAuto;

		public Autorefresh()
		{
			m_application = ArcMap.Application;
		}

		protected override void OnClick()
		{
			if (m_schematicTarget != null && m_schematicTarget.SchematicTarget != null && m_schematicTarget.SchematicTarget.IsEditingSchematicDiagram())
			{
				if (m_schematicInMemoryDiagram == null)
					m_schematicInMemoryDiagram = m_schematicTarget.SchematicTarget.SchematicInMemoryDiagram;

				if (m_schematicInMemoryDiagram != null)
				{
					if (m_formAuto == null)
					{
						m_formAuto = new FormAutoRefresh();
						try
						{
							m_formAuto.InitializeSecond();
							m_formAuto.InitializeMinute();
						}
						catch (Exception e)
						{
							System.Windows.Forms.MessageBox.Show(e.Message);
						}
					}
					m_formAuto.SetSchematicInmemoryDiagram(m_schematicInMemoryDiagram);
					m_formAuto.Appli = m_application;

					m_formAuto.Show();
				}
			}
		}

		protected override void OnUpdate()
		{
			if (m_schematicTarget == null)
			{
				IExtension extention = null;
				IExtensionManager extensionManager;

				extensionManager = (IExtensionManager)m_application;
				for (int i = 0; i < extensionManager.ExtensionCount; i++)
				{
					extention = extensionManager.get_Extension(i);
                    if (extention.Name.ToLower() == "esri schematic extension")
                        break;
				}

				if (extention != null)
				{
					SchematicExtension schematicExtension = extention as SchematicExtension;
					m_schematicTarget = schematicExtension as ISchematicTarget;
				}
			}

			if (m_schematicTarget != null)
			{
				ISchematicLayer schematicLayer;
				schematicLayer = m_schematicTarget.SchematicTarget;

				if (schematicLayer == null)
				{
					Enabled = false;
					if (m_formAuto != null) m_formAuto.SetAutoOff(true);
				}
				else if (schematicLayer.IsEditingSchematicDiagram())
					Enabled = true;
				else
				{
					Enabled = false;
					if (m_formAuto != null) m_formAuto.SetAutoOff(true);
				}
			}
			else
			{
				Enabled = false;
				if (m_formAuto != null) m_formAuto.SetAutoOff(true);
			}
		}
	}
}

