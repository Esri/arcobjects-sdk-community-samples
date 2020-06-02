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
// Copyright 2010 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at &lt;your ArcGIS install location&gt;/DeveloperKit10.0/userestrictions.txt.
// namespace SchematicCreateBasicSettingsAddIn

using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.ArcCatalogUI;
using ESRI.ArcGIS.ArcCatalog;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.NetworkAnalysis;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace SchematicCreateBasicSettingsAddIn
{
	public class GenerateSchematicTemplate : ESRI.ArcGIS.Desktop.AddIns.Button
	{
		#region Variables
		public frmDatasetTemplateName formNames;
		ESRI.ArcGIS.Geodatabase.IWorkspace m_pWS;
		String m_sfn;
		ESRI.ArcGIS.Schematic.ISchematicBuilder m_pB;
		ESRI.ArcGIS.Schematic.ISchematicDataset m_pSDS;
		ESRI.ArcGIS.Schematic.ISchematicStandardBuilder m_pSB;
		ESRI.ArcGIS.Schematic.ISchematicDiagramClass m_pSDT;

		ESRI.ArcGIS.Schematic.ISchematicDatasetImport m_pSDI;

		NameEvents templateInfo;
		public frmSelectItemsToReduce formReduce;
		private bool blnCancel;
		public frmAdvanced formAdvanced;
		private string strLayers;
		private string strNodeLayers;
		NameValueCollection m_myCol = new NameValueCollection();
		private IGxObject m_SelectedObject = null;
		#endregion Attributes

		public GenerateSchematicTemplate()
		{

		}

		private IEnumLayer GetLayers()
		{
			//now get the map document to parse out the feature classes
			GxDialog pGxDialog = new GxDialogClass();
			IEnumGxObject pEnumGxObject;
			bool pResult;

			pGxDialog.ObjectFilter = new GxFilterMapsClass();
			pGxDialog.Title = "Select a map document";
			try
			{
				pResult = pGxDialog.DoModalOpen(0, out pEnumGxObject);
				//check to see if the user canceled the dialog
				if (pResult == false) return null;
				IGxObject pGxObject = pEnumGxObject.Next();
				IMapReader pMapReader = new MapReaderClass();
				pMapReader.Open(pGxObject.FullName.ToString());
				IMap pMap = pMapReader.get_Map(0);
				ESRI.ArcGIS.esriSystem.UID pUID = new ESRI.ArcGIS.esriSystem.UIDClass();
				pUID.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";  //feature layer

				IEnumLayer pLayers = pMap.get_Layers(pUID, true);
                return pLayers;
			}
			catch
			{
				//error getting layers
				return null;
			}

		}

		private Dictionary<string, IFeatureClass> ProcessFCs(IEnumFeatureClass fcComplexEdge, IEnumFeatureClass fcComplexNode, IEnumFeatureClass fcSimpleEdge, IEnumFeatureClass fcSimpleNode)
		{
			Dictionary<string, IFeatureClass> pDictionary = new Dictionary<string, IFeatureClass>();

			//handle complex edge
			IFeatureClass fc = fcComplexEdge.Next();
			if (fc != null)
			{
				do
				{
					try
					{
						pDictionary.Add(fc.AliasName, fc);
					}
					catch
					{
						//do nothing
					}
					fc = fcComplexEdge.Next();
				} while (fc != null);
			}

			//handle complex node
			fc = fcComplexNode.Next();
			if (fc != null)
			{
				do
				{
					try
					{
						pDictionary.Add(fc.AliasName, fc);
					}
					catch
					{
						//do nothing
					}
					fc = fcComplexNode.Next();
				} while (fc != null);
			}

			//handle simple edge
			fc = fcSimpleEdge.Next();
			if (fc != null)
			{
				do
				{
					try
					{
						pDictionary.Add(fc.AliasName, fc);
					}
					catch
					{
						//do nothing
					}
					fc = fcSimpleEdge.Next();
				} while (fc != null);
			}

			//handle simple node
			fc = fcSimpleNode.Next();
			if (fc != null)
			{
				do
				{
					try
					{
						pDictionary.Add(fc.AliasName, fc);
					}
					catch
					{
						//do nothing
					}
					fc = fcSimpleNode.Next();
				} while (fc != null);
			}
			return pDictionary;
		}

		private string CreateSchLayers(IEnumLayer pLayers)
		{
			if (pLayers == null) return "";
			ILayer pLayer = pLayers.Next();
			IFeatureLayer featureLayer;
			IFeatureClass featureClass;
			string pStrLayerNames = "";
			IDataset pDataset;
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			m_pSDS.DesignMode = true;
			m_pSDI = (ESRI.ArcGIS.Schematic.ISchematicDatasetImport)m_pSDS;

			Dictionary<string, IFeatureClass> myDictionary = new Dictionary<string, IFeatureClass>();
			IGeometricNetwork gn = null;
			do
			{
				featureLayer = (IFeatureLayer)pLayer;
				featureClass = featureLayer.FeatureClass;
				pDataset = (IDataset)featureClass;

				if (featureClass.FeatureType == esriFeatureType.esriFTSimpleJunction || featureClass.FeatureType == esriFeatureType.esriFTSimpleEdge || featureClass.FeatureType == esriFeatureType.esriFTComplexEdge || featureClass.FeatureType == esriFeatureType.esriFTComplexJunction)
				{

					//The FeatureType property of feature classes that implement this interface will be esriFTSimpleJunction, esriDTSimpleEdge, esriFTComplexJunction, or esriFTComplexEdge.
					INetworkClass networkClass = (INetworkClass)featureLayer.FeatureClass;

					if (networkClass.GeometricNetwork != null)
					{
						//we have a network class
						if ((gn == null) || (gn != networkClass.GeometricNetwork))
						{
							//need to process all the classes
							Dictionary<string, IFeatureClass> localDictionary = new Dictionary<string, IFeatureClass>();
							gn = networkClass.GeometricNetwork;
							IEnumFeatureClass fcComplexEdge = networkClass.GeometricNetwork.get_ClassesByType(esriFeatureType.esriFTComplexEdge);
							IEnumFeatureClass fcComplexNode = networkClass.GeometricNetwork.get_ClassesByType(esriFeatureType.esriFTComplexJunction);
							IEnumFeatureClass fcSimpleEdge = networkClass.GeometricNetwork.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
							IEnumFeatureClass fcSimpleNode = networkClass.GeometricNetwork.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
							localDictionary = ProcessFCs(fcComplexEdge, fcComplexNode, fcSimpleEdge, fcSimpleNode);
							if (myDictionary.Count == 0)  //just copy it
							{
								myDictionary = localDictionary;
							}
							else //merge
							{
								Dictionary<string, IFeatureClass>.KeyCollection keyColl = localDictionary.Keys;

								foreach (string s in keyColl)
								{
									IFeatureClass fc;
									bool bln = localDictionary.TryGetValue(s, out fc);
									myDictionary.Add(s, fc);
								}
							}
						}
						//Build up the string that will go to the select items to reduce form
						pStrLayerNames += pDataset.Name.ToString();
						pStrLayerNames += ";";

						//Build up the string for just the node feature classes
						if (featureClass.FeatureType == esriFeatureType.esriFTSimpleJunction || featureClass.FeatureType == esriFeatureType.esriFTComplexJunction)
						{
							strNodeLayers += pDataset.Name.ToString();
							strNodeLayers += ";";
						}

						//create the fields collections to be used by the frmAdvanced form
						IFields pFields = featureClass.Fields;
						if (pFields.FieldCount > 0)
						{
							for (int i = 0; i < pFields.FieldCount; i++)
							{
								//don't mess with objectid or shape or GlobalID
								if ((pFields.get_Field(i).Name.ToString() != "OBJECTID") && (pFields.get_Field(i).Name.ToString() != "SHAPE") && (pFields.get_Field(i).Name.ToString() != "GlobalID") && (pFields.get_Field(i).Name.ToString() != featureClass.OIDFieldName.ToString()) && (pFields.get_Field(i).Name.ToString() != featureClass.ShapeFieldName.ToString()))
								{
									m_myCol.Add(pDataset.Name.ToString(), pFields.get_Field(i).Name.ToString());
								}
							}
						}

						//remove the layer from the list of dictionary classes
						if (myDictionary.ContainsKey(featureClass.AliasName))
						{
							myDictionary.Remove(featureClass.AliasName);
						}

						m_pSDI.ImportFeatureLayer(featureLayer, m_pSDT, true, true, true);
					}
				}
				pLayer = pLayers.Next();
			} while (pLayer != null);

			//handle any feature classes that were not in the map
			if (myDictionary.Count > 0)
			{
				Dictionary<string, IFeatureClass>.KeyCollection keyColl = myDictionary.Keys;
				foreach (string s in keyColl)
				{
					IFeatureClass fc;
					bool bln = myDictionary.TryGetValue(s, out fc);
					IObjectClass o = (IObjectClass)fc;
					pDataset = (IDataset)fc;

					pStrLayerNames += pDataset.Name.ToString();
					pStrLayerNames += ";";

					//Build up the string for just the node feature classes
					if (fc.FeatureType == esriFeatureType.esriFTSimpleJunction || featureClass.FeatureType == esriFeatureType.esriFTComplexJunction)
					{
						strNodeLayers += pDataset.Name.ToString();
						strNodeLayers += ";";
					}

					//create the fields collections to be used by the frmAdvanced form
					IFields pFields = fc.Fields;
					if (pFields.FieldCount > 0)
					{
						for (int i = 0; i < pFields.FieldCount; i++)
						{
							//don't mess with objectid or shape or GlobalID
							if ((pFields.get_Field(i).Name.ToString() != "OBJECTID") && (pFields.get_Field(i).Name.ToString() != "SHAPE") && (pFields.get_Field(i).Name.ToString() != "GlobalID") && (pFields.get_Field(i).Name.ToString() != fc.OIDFieldName.ToString()) && (pFields.get_Field(i).Name.ToString() != fc.ShapeFieldName.ToString()))
							{
								m_myCol.Add(pDataset.Name.ToString(), pFields.get_Field(i).Name.ToString());
							}
						}
					}
					if ((fc.FeatureType == esriFeatureType.esriFTComplexJunction) || (fc.FeatureType == esriFeatureType.esriFTSimpleJunction))
					{
						//node
						m_pSDI.ImportObjectClass(o, m_pSDT, true, esriSchematicElementType.esriSchematicNodeType);
					}
					else
					{
						//link
						m_pSDI.ImportObjectClass(o, m_pSDT, true, esriSchematicElementType.esriSchematicLinkType);
					}
				}
			}

			m_pSDS.Save(ESRI.ArcGIS.esriSystem.esriArcGISVersion.esriArcGISVersionCurrent, true);
			m_pSDS.DesignMode = false;
			return pStrLayerNames;
		}

		protected override void OnClick()
		{
			blnCancel = false;
			formNames = new frmDatasetTemplateName();
			formNames.cancelFormEvent += new EventHandler(formNames_cancelFormEvent);
			formNames.nextFormEvent += new EventHandler<NameEvents>(formNames_nextFormEvent);
			m_SelectedObject = ArcCatalog.ThisApplication.SelectedObject;

			if ((m_SelectedObject.Category == "Schematic Dataset")
					|| (m_SelectedObject.Category.ToLower().Contains("database")))
			{
				if (m_SelectedObject.Category.ToLower().Contains("database"))
				{
					//get dataset and template names, then create the objects
					formNames.blnNewDataset = true;
				}
				else
				{
					//dataset, just get template names, then create objects
					formNames.blnNewDataset = false;
				}
				//show the first form of the wizard 
				if (formNames.ShowDialog() == DialogResult.Cancel)
				{
					formNames = null;
					return;
				}
			}
			else
			{
				//we are not on a database or a schematic dataset
				blnCancel = true;
			}

			if (blnCancel == true)
			{
				System.Windows.Forms.MessageBox.Show("The name of the dataset or template already exists.  Please try again with valid names.");
			}

			if (blnCancel != true)  //only true if the user cancels the first form formNames_cancelFormEvent 
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				IEnumLayer pEnumLayer = GetLayers();
				if (pEnumLayer == null)
				{
					//should only happen if the user clicks cancel on the gxdialog
					blnCancel = true;
				}
				else
				{
					strLayers = CreateSchLayers(pEnumLayer);
                    if (strLayers.Length > 0) //make sure we get something back
                    {
                        //find out if we need to create node reduction rules
                        formReduce = new frmSelectItemsToReduce();
                        formReduce.doneFormEvent += new EventHandler<ReduceEvents>(formReduce_doneFormEvent);
                        formReduce.cancelFormEvent += new EventHandler(formReduce_cancelFormEvent);

                        formReduce.itemList = strNodeLayers;
                        System.Windows.Forms.Cursor.Current = Cursors.Default;
                        formReduce.ShowDialog();
                    }
                    else
                    {
                        //this can happen if the map document didn't have any
                        //layers corresponding to a geometric network
                        blnCancel = true;
                    }
				}
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}

			if (blnCancel != true)  //could have cancelled on either frmDatasetTemplateName or frmSelectItemsToReduce
			{
				//Advanced Form
				formAdvanced = new frmAdvanced();
				formAdvanced.doneFormEvent += new EventHandler<AdvancedEvents>(formAdvanced_doneFormEvent);
				formAdvanced.strLayers = this.strLayers;
				formAdvanced.strNodeLayers = this.strNodeLayers;
				formAdvanced.m_myCol = this.m_myCol;
				formAdvanced.ShowDialog();
			}

			ArcCatalog.ThisApplication.Refresh(m_sfn);
			try
			{
				ArcCatalog.ThisApplication.Location = m_SelectedObject.FullName.ToString();
			}
			catch { }

			cleanUp();
		}

		void formReduce_cancelFormEvent(object sender, EventArgs e)
		{
			blnCancel = true;
			formReduce.Close();
		}

		void formAdvanced_doneFormEvent(object sender, AdvancedEvents e)
		{
			m_pSDS.DesignMode = true;
			formAdvanced.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			//process the algorithm if there is one
			if (e.AlgorithmName != "")
			{
				ISchematicAlgoSmartTree a = new SchematicAlgoSmartTreeClass();
				if (e.AlgorithmParams.Count > 0)
				{
					Dictionary<string, string>.KeyCollection keys = e.AlgorithmParams.Keys;
					string strValue = "";
					foreach (string s in keys)
					{
						if (s == "Direction")
						{
							e.AlgorithmParams.TryGetValue(s, out strValue);

							if (strValue == "Top to Bottom")
							{
								a.Direction = esriSchematicAlgoDirection.esriSchematicAlgoTopDown;
							}
							else if (strValue == "Bottom to Top")
							{
								a.Direction = esriSchematicAlgoDirection.esriSchematicAlgoBottomUp;
							}
							else if (strValue == "Left to Right")
							{
								a.Direction = esriSchematicAlgoDirection.esriSchematicAlgoLeftRight;
							}
							else
							{
								a.Direction = esriSchematicAlgoDirection.esriSchematicAlgoRightLeft;
							}
						}
					}
					if (e.RootClass != "")
					{
						ISchematicElementClassContainer pECC = (ISchematicElementClassContainer)m_pSDS;
						ISchematicElementClass pEC = pECC.GetSchematicElementClass(e.RootClass);
						ESRI.ArcGIS.esriSystem.UID u = new ESRI.ArcGIS.esriSystem.UID();
						u.Value = "{3AD9D8B8-0A1D-4F32-ABB5-54B848A46F85}";

						ISchematicAttributeConstant pAttrConst = (ISchematicAttributeConstant)pEC.CreateSchematicAttribute("RootFlag", u);
						ISchematicAttributeManagement pAttrMgmt = (ISchematicAttributeManagement)pAttrConst;
						pAttrMgmt.StorageMode = esriSchematicAttributeStorageMode.esriSchematicAttributeFieldStorage;
						pAttrConst.ConstantValue = "-1";
					}
				}

				m_pSDT.SchematicAlgorithm = (ISchematicAlgorithm)a;

			}

			//check to see if we need to add associated fields
			if (e.FieldsToCreate != null)
			{
				if (e.FieldsToCreate.Count > 0)
				{
					ISchematicElementClassContainer pECC = (ISchematicElementClassContainer)m_pSDS;

					//create the associated field attributes
					string[] keys = e.FieldsToCreate.AllKeys;
					foreach (string s in keys)
					{
						//get the feature class
						ISchematicElementClass pEC = pECC.GetSchematicElementClass(s);
						if (pEC != null)
						{
							string strName = "";
							string[] values = e.FieldsToCreate.GetValues(s);
							foreach (string v in values)
							{
								//create the field
								ESRI.ArcGIS.esriSystem.UID u = new ESRI.ArcGIS.esriSystem.UID();
								u.Value = "{7DE3A19D-32D0-41CD-B896-37CA3AFBD88A}";

								IClass pClass = (IClass)pEC;
								//only handle names that don't already exist in the schematic tables
								if (pClass.FindField(v) == -1)
								{
									strName = v.ToString();

									ISchematicAttributeAssociatedField pFieldAttr = (ISchematicAttributeAssociatedField)pEC.CreateSchematicAttribute(strName, u);
									pFieldAttr.AssociatedFieldName = v;
									ISchematicAttributeManagement pAttrMgmt = (ISchematicAttributeManagement)pFieldAttr;
									pAttrMgmt.StorageMode = esriSchematicAttributeStorageMode.esriSchematicAttributeFieldStorage;
								}
							}
						}
					}
				}
			}

			m_pSDS.Save(ESRI.ArcGIS.esriSystem.esriArcGISVersion.esriArcGISVersionCurrent, true);
			m_pSDS.DesignMode = false;
			formAdvanced.Cursor = System.Windows.Forms.Cursors.Default;
			formAdvanced.Close();
		}

		private Boolean CreateTemplate(NameEvents templateInfo)
		{
			//need to get everything first
			IGxDatabase pDatabase = null;
			ISchematicDiagramClassContainer pDiagramClassContainer = null;

			if (m_SelectedObject.Category == "Schematic Dataset")
			{
				pDatabase = (IGxDatabase)m_SelectedObject.Parent;
			}
			else  //on the database already
			{
				pDatabase = (IGxDatabase)m_SelectedObject;
			}
			m_pWS = pDatabase.Workspace;

			ESRI.ArcGIS.Schematic.ISchematicWorkspaceFactory pSWF = new SchematicWorkspaceFactory();
			ESRI.ArcGIS.Schematic.ISchematicWorkspace pSW = pSWF.Open(m_pWS);

			m_pSDS = pSW.get_SchematicDatasetByName(templateInfo.DatasetName);

			//check to see if the template name already exists
			pDiagramClassContainer = (ISchematicDiagramClassContainer)m_pSDS;
			m_pSDT = pDiagramClassContainer.GetSchematicDiagramClass(templateInfo.TemplateName.ToString());
			if (m_pSDT != null) return false;

			//create the schematic template
			m_pSDT = m_pSDS.CreateSchematicDiagramClass(templateInfo.TemplateName);

			if ((templateInfo.AutoCreate == true) || (templateInfo.UseVertices == true))
			{
				m_pB = (ESRI.ArcGIS.Schematic.ISchematicBuilder)m_pSDT;
				m_pSB = (ESRI.ArcGIS.Schematic.ISchematicStandardBuilder)m_pSDT.SchematicBuilder;
				m_pSB.InitializeLinksVertices = templateInfo.UseVertices;
				m_pSB.AutoCreateElementClasses = templateInfo.AutoCreate;
			}
			m_pSDS.Save(ESRI.ArcGIS.esriSystem.esriArcGISVersion.esriArcGISVersion10, false);
			return true;
		}

		private Boolean CreateDataset(NameEvents templateInfo)
		{
			try
			{
				IGxDatabase pDatabase = (IGxDatabase)m_SelectedObject;

				m_pWS = pDatabase.Workspace;

				ESRI.ArcGIS.Schematic.ISchematicWorkspaceFactory pSWF = new SchematicWorkspaceFactory();
				ESRI.ArcGIS.Schematic.ISchematicWorkspace pSW = pSWF.Open(m_pWS);
				//check to see if this dataset name is already used
				m_pSDS = pSW.get_SchematicDatasetByName(templateInfo.DatasetName.ToString());
				if (m_pSDS != null) return false;

				m_pSDS = pSW.CreateSchematicDataset(templateInfo.DatasetName, "");
				return true;
			}
			catch
			{
				//nothing
				return false;
			}
		}

		void formNames_cancelFormEvent(object sender, EventArgs e)
		{
			//user is canceling the wizard
			formNames.Close();
			formNames = null;
			blnCancel = true;
		}

		void formReduce_doneFormEvent(object sender, ReduceEvents e)
		{
			//user click the done button on the reduce form
			ISchematicBuilderRule pIsbr;
			ISchematicBuilderRuleContainer pIsbrc = (ISchematicBuilderRuleContainer)m_pSDT;
			ISchematicBuilderRuleContainerEdit pIsbrce = (ISchematicBuilderRuleContainerEdit)pIsbrc;

			formReduce.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			string[] selectedItems = e.SelectedObjects;
			m_pSDS.DesignMode = true;
			foreach (string s in selectedItems)
			{
				//setup rule properties
				ISchematicNodeReductionRuleByPriority pRule = new SchematicNodeReductionRuleByPriorityClass();
				pRule.NodeDegreeConstraint = true;
				pRule.ReduceNodeDegree0 = true;
				pRule.ReduceNodeDegree2 = true;
				pRule.ReduceNodeDegree1 = false;
				pRule.ReduceNodeDegreeSup3 = false;

				//set the name and class to reduce
				ISchematicNodeReductionRule pNR = (ISchematicNodeReductionRule)pRule;
				pNR.Description = "Remove " + s.ToString();
				pNR.NodeClassName = s.ToString();

				//add it to the template
				pIsbr = pIsbrce.AddSchematicBuilderRule();
				pIsbr.SchematicRule = (ISchematicRule)pRule;
			}

			//save and close
			m_pSDS.Save(ESRI.ArcGIS.esriSystem.esriArcGISVersion.esriArcGISVersion10, false);
			m_pSDS.DesignMode = false;
			formReduce.Cursor = System.Windows.Forms.Cursors.Default;
			formReduce.Close();
		}

		void formNames_nextFormEvent(object sender, NameEvents e)
		{
			Boolean blnCheck = false;
			//check if we need to create a new dataset
			templateInfo = new NameEvents(e.NewDataset, e.DatasetName, e.TemplateName, e.UseVertices);
			formNames.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			if (templateInfo.NewDataset == true)
			{
				blnCheck = CreateDataset(templateInfo);
				if (blnCheck == false)
				{
					//name already exists
					blnCancel = true;
				}
				else
				{
					blnCheck = CreateTemplate(templateInfo);
					if (blnCheck == false)
					{
						//name already exists
						blnCancel = true;
					}
				}
			}
			else //just create a new template
			{
				blnCheck = CreateTemplate(templateInfo);
				if (blnCheck == false)
				{
					//name already exists
					blnCancel = true;
				}
			}
			formNames.Cursor = System.Windows.Forms.Cursors.Default;
			formNames.Close();
		}

		protected override void OnUpdate()
		{
			Enabled = ArcCatalog.Application != null;
			if ((ArcCatalog.ThisApplication.SelectedObject.Category == "File Geodatabase")
					|| (ArcCatalog.ThisApplication.SelectedObject.Category == "Personal Geodatabase")
					|| (ArcCatalog.ThisApplication.SelectedObject.Category == "Schematic Dataset")
					|| (ArcCatalog.ThisApplication.SelectedObject.Category == "Spatial Database Connection"))
			{
				Enabled = true;
			}
			else
			{
				Enabled = false;
			}

		}

		void cleanUp()
		{
			//m_pWSF = null;
			m_pWS = null;
			m_pSDT = null;
			m_pSDS = null;
			m_pSB = null;
			m_pB = null;
			m_SelectedObject = null;
			templateInfo = null;
			m_pSDI = null;
			formNames = null;
			formReduce = null;
			m_sfn = "";
			blnCancel = false;
			strLayers = "";
			strNodeLayers = "";
		}

	}
}

