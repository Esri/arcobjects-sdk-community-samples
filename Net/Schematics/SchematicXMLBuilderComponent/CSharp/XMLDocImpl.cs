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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Schematic;
using System.Windows.Forms;

namespace MyExtXmlComponentCS
{
	[Guid("0CE2EC0B-975A-4795-A7C2-EF31978D92A2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("MyExtXmlComponentCS.XMLDocImpl")]
	public class XMLDocImpl : ISchematicXmlGenerate, ISchematicXmlUpdate
	{
		//
		#region Variables
		//This class must implement the ISchematicXMLGenerate and ISchematicXMLUpdate interfaces

		private ESRI.ArcGIS.ArcMap.Application m_application;         //ArcMap application
		private ESRI.ArcGIS.ArcMapUI.IMxDocument m_mxDocument;     //ArcMap document

		//The following arrays will be used to create the wished propertyset properties in the XML DOMDocument
		private string[] m_stationsPropertiesArray = { "Name", "Capacity", "Type", "Feeder" };
		private string[] m_feedersPropertiesArray = { "Feeder_Description" };
		private string[] m_LVLinesPropertiesArray = { "Category" };
		private const string DatasetName = "ElectricDataSet";

		~XMLDocImpl()
		{
			m_application = null;
			m_mxDocument = null; ;
		}
		#endregion

		#region ISchematicXmlGenerate Members

		public void GenerateXmlData(string diagramName, string diagramClassName, ref object xmlSource, ref bool cancel)
		{
			MSXML2.DOMDocument xmlDOMDocument = new MSXML2.DOMDocument();
			ESRI.ArcGIS.Carto.IMaps maps;
			ESRI.ArcGIS.Carto.IMap currentMap;
			ESRI.ArcGIS.Geodatabase.IEnumFeature enumFeature;
			ESRI.ArcGIS.Geodatabase.IFeature feature;
			MSXML2.IXMLDOMProcessingInstruction xmlProcInstr;
			MSXML2.IXMLDOMElement xmlDiagrams;
			MSXML2.IXMLDOMElement xmlDiagram;
			MSXML2.IXMLDOMElement xmlFeatures;
			MSXML2.IXMLDOMElement xmlDataSources;
			MSXML2.IXMLDOMElement xmlDataSource;
			MSXML2.IXMLDOMElement xmlDataSource_Namestring;
			MSXML2.IXMLDOMElement xmlDataSource_WorkspaceInfo;
			MSXML2.IXMLDOMElement xmlWorkspaceInfo_PathName;
			MSXML2.IXMLDOMElement xmlWorkspaceInfo_WorkspaceFactoryProgID;
			MSXML2.IXMLDOMAttribute rootAtt1;
			MSXML2.IXMLDOMAttribute rootAtt2;
			ESRI.ArcGIS.Geodatabase.IEnumFeatureSetup enumFeatureSetup;
			string xmlDatabase;

			// Retrieving the selected set of features
			enumFeature = null;
			feature = null;

			m_mxDocument = (ESRI.ArcGIS.ArcMapUI.IMxDocument)m_application.Document;
			maps = m_mxDocument.Maps;
			int i = 0;
			while (i < maps.Count)
			{
				currentMap = maps.get_Item(i);
				enumFeature = (ESRI.ArcGIS.Geodatabase.IEnumFeature)currentMap.FeatureSelection;
				enumFeatureSetup = (ESRI.ArcGIS.Geodatabase.IEnumFeatureSetup)enumFeature;
				enumFeatureSetup.AllFields = true;
				feature = enumFeature.Next();
				if (feature != null) break;

				i += 1;
			}

			// if (there is no selected feature in the MxDocument, the procedure is interrupted
			if (feature == null)
			{
				MessageBox.Show("There is no feature selected. Select a set of features.", "Generate/Update XML diagrams", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cancel = true;
				return;
			}

			//Checking the feature dataset related to the selected features
			ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass;
			string featureDatasetName;
			featureClass = (ESRI.ArcGIS.Geodatabase.IFeatureClass)feature.Class;
			featureDatasetName = featureClass.FeatureDataset.BrowseName;
			xmlDatabase = featureClass.FeatureDataset.Workspace.PathName;

			// if the selected features come from another feature dataset than the expected one, the procedure is interrupted
			if (featureDatasetName != DatasetName)
			{
				//More restrictive condition: if (xmlDatabase != "c:\Mybase.gdb" ) 
				MessageBox.Show("This component doesn't work from the selected set of features.");
				cancel = true;
				return;
			}

			// Writing the XML heading items in the DOMDocument
			xmlProcInstr = xmlDOMDocument.createProcessingInstruction("xml", "version='1.0'");
			xmlDOMDocument.appendChild(xmlProcInstr);
			xmlProcInstr = null;

			//-------- Diagrams Section START --------
			// Creating the root Diagrams element
			xmlDiagrams = xmlDOMDocument.createElement("Diagrams");
			xmlDOMDocument.documentElement = xmlDiagrams;
			rootAtt1 = xmlDOMDocument.createAttribute("xmlns:xsi");
			rootAtt1.text = "http://www.w3.org/2001/XMLSchema-instance";
			xmlDiagrams.attributes.setNamedItem(rootAtt1);

			// Creating the Diagram element for the diagram which is going to be generated
			xmlDiagram = xmlDOMDocument.createElement("Diagram");
			xmlDiagrams.appendChild(xmlDiagram);
			rootAtt1 = xmlDOMDocument.createAttribute("EnforceDiagramTemplateName");
			rootAtt1.text = "false";
			xmlDiagram.attributes.setNamedItem(rootAtt1);
			rootAtt2 = xmlDOMDocument.createAttribute("EnforceDiagramName");
			rootAtt2.text = "false";
			xmlDiagram.attributes.setNamedItem(rootAtt2);

			//-------- DataSources Section START --------
			// Creating the DataSources element 
			xmlDataSources = xmlDOMDocument.createElement("Datasources");
			xmlDiagram.appendChild(xmlDataSources);
			xmlDataSource = xmlDOMDocument.createElement("Datasource");
			xmlDataSources.appendChild(xmlDataSource);

			// Specifying the Namestring for the related Datasource element
			xmlDataSource_Namestring = xmlDOMDocument.createElement("NameString");
			xmlDataSource.appendChild(xmlDataSource_Namestring);
			xmlDataSource_Namestring.nodeTypedValue = "XMLDataSource";

			// Specifying the WorkspaceInfo for the related Datasource element
			xmlDataSource_WorkspaceInfo = xmlDOMDocument.createElement("WorkSpaceInfo");
			xmlDataSource.appendChild(xmlDataSource_WorkspaceInfo);
			xmlWorkspaceInfo_PathName = xmlDOMDocument.createElement("PathName");
			xmlDataSource_WorkspaceInfo.appendChild(xmlWorkspaceInfo_PathName);
			xmlWorkspaceInfo_PathName.nodeTypedValue = xmlDatabase;
			xmlWorkspaceInfo_WorkspaceFactoryProgID = xmlDOMDocument.createElement("WorkspaceFactoryProgID");
			xmlDataSource_WorkspaceInfo.appendChild(xmlWorkspaceInfo_WorkspaceFactoryProgID);
			xmlWorkspaceInfo_WorkspaceFactoryProgID.nodeTypedValue = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
			//-------- DataSources Section END --------

			//-------- Features Section START --------
			xmlFeatures = xmlDOMDocument.createElement("Features");
			xmlDiagram.appendChild(xmlFeatures);
			while (feature != null)
			{
				switch (feature.FeatureType)
				{
					case ESRI.ArcGIS.Geodatabase.esriFeatureType.esriFTSimpleJunction:
						CreateXMLNodeElt(feature, ref xmlDOMDocument, ref xmlFeatures, feature.Class.AliasName);
						break;
					case ESRI.ArcGIS.Geodatabase.esriFeatureType.esriFTSimpleEdge:
						CreateXMLLinkElt(feature, ref xmlDOMDocument, ref xmlFeatures, feature.Class.AliasName);
						break;
				}
				feature = enumFeature.Next();

			}

			// output the XML we created
			xmlSource = xmlDOMDocument;
			cancel = false;
			//-------- Features Section END --------
			//-------- Diagrams Section END --------

		}


		#endregion

		#region ISchematicXmlUpdate Members

		public void UpdateXmlData(string diagramName, string diagramClassName, string updateInformation, ref    object xmlSource, ref  bool cancel)
		{
			GenerateXmlData(diagramName, diagramClassName, ref xmlSource, ref  cancel);
		}

		#endregion

		#region "private functions"
		// The following CreateXMLLNodeElt private procedure is used to create all the expected 
		// XML items for a XML NodeFeature related to a Station or Feeder simple junction feature
		private void CreateXMLNodeElt(ESRI.ArcGIS.Geodatabase.IFeature inFeature, ref MSXML2.DOMDocument outDOMDoc, ref MSXML2.IXMLDOMElement outXMLElements, string inNodeTypeName)
		{

			if (!inFeature.HasOID)
			{
				MessageBox.Show("No OID");
				return;
			}

			MSXML2.IXMLDOMElement xmlNode;
			MSXML2.IXMLDOMElement xmlNode_XCoord;
			MSXML2.IXMLDOMElement xmlNode_YCoord;
			MSXML2.IXMLDOMElement xmlNode_RelatedContainerID;
			bool relatedContainer;
			MSXML2.IXMLDOMNodeList xmlNodeList;
			MSXML2.IXMLDOMElement xmlDrawing;
			MSXML2.IXMLDOMElement xmlDrawing_EltTypeName;
			MSXML2.IXMLDOMElement xmlDrawing_ExternalUID;

			//-------- Feature Section START related to the "infeature" --------
			// Creating the NodeFeature element
			xmlNode = outDOMDoc.createElement("NodeFeature");
			outXMLElements.appendChild(xmlNode);
			// Specifying basic XML items for this NodeFeature
			CreateBasicXMLItemsForSchematicElt(inFeature, ref outDOMDoc, ref xmlNode, inNodeTypeName);

			// Specifying its X && Y when they exist
			if ((inFeature.Fields.FindField("X") > 0) && (inFeature.Fields.FindField("Y") > 0))
			{
				// Specifying InitialX
				xmlNode_XCoord = outDOMDoc.createElement("InitialX");
				xmlNode.appendChild(xmlNode_XCoord);
				xmlNode_XCoord.nodeTypedValue = inFeature.get_Value(inFeature.Fields.FindField("X"));
				// Specifying InitialY
				xmlNode_YCoord = outDOMDoc.createElement("InitialY");
				xmlNode.appendChild(xmlNode_YCoord);
				xmlNode_YCoord.nodeTypedValue = inFeature.get_Value(inFeature.Fields.FindField("Y"));
			}
			else
			{
				// Retrieving initial position from Geometry
				ESRI.ArcGIS.Geometry.IPoint oPoint = (ESRI.ArcGIS.Geometry.IPoint)inFeature.ShapeCopy;

				if (oPoint != null)
				{
					// Specifying InitialX
					xmlNode_XCoord = outDOMDoc.createElement("InitialX");
					xmlNode.appendChild(xmlNode_XCoord);
					xmlNode_XCoord.nodeTypedValue = oPoint.X;
					// Specifying InitialY
					xmlNode_YCoord = outDOMDoc.createElement("InitialY");
					xmlNode.appendChild(xmlNode_YCoord);
					xmlNode_YCoord.nodeTypedValue = oPoint.Y;
				}
			}

			xmlNode_RelatedContainerID = outDOMDoc.createElement("RelatedContainerID");
			xmlNode.appendChild(xmlNode_RelatedContainerID);

			// Specifying its properties 
			switch (inFeature.Class.AliasName)
			{
				case "Station":
					{
						xmlNode_RelatedContainerID.nodeTypedValue = "Container-" + System.Convert.ToString(inFeature.get_Value(inFeature.Fields.FindField("Feeder")));
						// For Station feature, the field contained in the StationsPropertiesArray will be exported
						CompleteXMLEltByProperties(inFeature, ref outDOMDoc, ref xmlNode, m_stationsPropertiesArray);
						break;
					}
				case "Feeder":
					{
						xmlNode_RelatedContainerID.nodeTypedValue = "Container-" + inFeature.OID.ToString();
						// For Feeder feature, the field contained in the StationsPropertiesArray will be exported          
						CompleteXMLEltByProperties(inFeature, ref outDOMDoc, ref xmlNode, m_feedersPropertiesArray);
						break;
					}
			}
			//-------- Feature Section END related to the "infeature" --------

			// Checking the existence of the related container 
			xmlNodeList = outXMLElements.selectNodes("NodeFeature/ExternalUniqueID");
			relatedContainer = false;

			foreach (MSXML2.IXMLDOMNode node in xmlNodeList)
			{
				if (node.text == xmlNode_RelatedContainerID.nodeTypedValue.ToString())
				{
					relatedContainer = true;
					break;
				}
			} // pNode

			// Creating the related container when it doesn//t already exist
			if (!relatedContainer)
			{
				xmlDrawing = outDOMDoc.createElement("NodeFeature");
				outXMLElements.appendChild(xmlDrawing);
				// Specifying its FeatureClassName
				xmlDrawing_EltTypeName = outDOMDoc.createElement("FeatureClassName");
				xmlDrawing.appendChild(xmlDrawing_EltTypeName);
				xmlDrawing_EltTypeName.nodeTypedValue = "Containers";
				// Specifying its ExternalUniqueID
				xmlDrawing_ExternalUID = outDOMDoc.createElement("ExternalUniqueID");
				xmlDrawing.appendChild(xmlDrawing_ExternalUID);
				xmlDrawing_ExternalUID.nodeTypedValue = xmlNode_RelatedContainerID.nodeTypedValue;
			}
		}

		// The following CreateXMLLinkElt private procedure is used to create all the expected XML items for a XML LinkFeature related to a HV_Line or LV_Line simple edge feature
		private void CreateXMLLinkElt(ESRI.ArcGIS.Geodatabase.IFeature inFeature, ref MSXML2.DOMDocument outDOMDoc, ref MSXML2.IXMLDOMElement outXMLElements, string inLinkTypeName)
		{
			if (!inFeature.HasOID)
			{
				MessageBox.Show("No OID");
				return;
			}

			MSXML2.IXMLDOMElement xmlLink;
			MSXML2.IXMLDOMElement xmlLink_FromNode;
			MSXML2.IXMLDOMElement xmlLink_ToNode;
			int indexListPoints;
			string listPoints;
			int nbVertices;
			string vertices;
			MSXML2.IXMLDOMElement xmlLink_Vertices;
			MSXML2.IXMLDOMElement xmlLink_Vertex;
			MSXML2.IXMLDOMElement xmlLink_XVertex;
			MSXML2.IXMLDOMElement xmlLink_YVertex;
			string xValue;
			string yValue;

			//-------- Feature Section START related to the "infeature" --------
			// Creating the LinkFeature Feature
			xmlLink = outDOMDoc.createElement("LinkFeature");
			outXMLElements.appendChild(xmlLink);

			// Specifying basic XML items for this LinkFeature
			CreateBasicXMLItemsForSchematicElt(inFeature, ref outDOMDoc, ref xmlLink, inLinkTypeName);
			// Specifying its FromNode
			xmlLink_FromNode = outDOMDoc.createElement("FromNode");
			xmlLink.appendChild(xmlLink_FromNode);
			xmlLink_FromNode.nodeTypedValue = inFeature.get_Value(inFeature.Fields.FindField("FromJunctionType")) + "-" + inFeature.get_Value(inFeature.Fields.FindField("FromJunctionOID"));
			// Specifying its ToNode
			xmlLink_ToNode = outDOMDoc.createElement("ToNode");
			xmlLink.appendChild(xmlLink_ToNode);
			xmlLink_ToNode.nodeTypedValue = inFeature.get_Value(inFeature.Fields.FindField("ToJunctionType")) + "-" + inFeature.get_Value(inFeature.Fields.FindField("ToJunctionOID"));

			//Add Vertices to LinkFeature ---- NEED TO BE COMPLETED
			indexListPoints = inFeature.Fields.FindField("ListPoints");
			if (indexListPoints > 0)
			{
				listPoints = "";
				listPoints = inFeature.get_Value(indexListPoints).ToString();
				if (listPoints != "")
				{
					int foundChar = listPoints.IndexOf(";", 1);
					nbVertices = System.Convert.ToInt32(listPoints.Substring(0, foundChar));
					vertices = listPoints.Substring(foundChar + 1);
					if (nbVertices > 0)
					{
						// Specifying its Vertices
						xmlLink_Vertices = outDOMDoc.createElement("Vertices");
						xmlLink.appendChild(xmlLink_Vertices);

						int iLoc;
						for (int i = 1; i <= nbVertices; i++)
						{
							xValue = "";
							yValue = "";
							iLoc = vertices.IndexOf(";", 1);
							if (vertices != "" && (iLoc) > 0)
							{
								xValue = vertices.Substring(0, iLoc);
							}
							vertices = vertices.Substring(iLoc + 1);
							iLoc = vertices.IndexOf(";", 1);
							if (vertices != ";" && (iLoc) > 0)
							{
								yValue = vertices.Substring(0, iLoc);
							}

							if (xValue != "" && yValue != "")
							{
								xmlLink_Vertex = outDOMDoc.createElement("Vertex");
								xmlLink_Vertices.appendChild(xmlLink_Vertex);
								xmlLink_XVertex = outDOMDoc.createElement("X");
								xmlLink_Vertex.appendChild(xmlLink_XVertex);
								xmlLink_XVertex.nodeTypedValue = xValue;
								xmlLink_YVertex = outDOMDoc.createElement("Y");
								xmlLink_Vertex.appendChild(xmlLink_YVertex);
								xmlLink_YVertex.nodeTypedValue = yValue;
								if (vertices.Length - iLoc > 0)
								{
									vertices = vertices.Substring(iLoc + 1); //sVertices.Length - iLoc)
								}
								else
								{
									break;
								}
							}
							else
							{
								break;
							}
						}
					}
				}
			}
			else
			{// Retrieving ListPoint from geometry
				ESRI.ArcGIS.Geometry.IPolyline oPoly = (ESRI.ArcGIS.Geometry.IPolyline)inFeature.ShapeCopy;
				ESRI.ArcGIS.Geometry.IPointCollection colLink = (ESRI.ArcGIS.Geometry.IPointCollection)oPoly;
				if (colLink != null && colLink.PointCount > 2)
				{
					ESRI.ArcGIS.Geometry.IPoint oPoint;

					xmlLink_Vertices = outDOMDoc.createElement("Vertices");
					xmlLink.appendChild(xmlLink_Vertices);
					for (int i = 1; i < colLink.PointCount - 1; i++)
					{
						oPoint = colLink.get_Point(i);

						xmlLink_Vertex = outDOMDoc.createElement("Vertex");
						xmlLink_Vertices.appendChild(xmlLink_Vertex);
						xmlLink_XVertex = outDOMDoc.createElement("X");
						xmlLink_Vertex.appendChild(xmlLink_XVertex);
						xmlLink_XVertex.nodeTypedValue = oPoint.X;
						xmlLink_YVertex = outDOMDoc.createElement("Y");
						xmlLink_Vertex.appendChild(xmlLink_YVertex);
						xmlLink_YVertex.nodeTypedValue = oPoint.Y;
					}
				}
			}
			
			//Specifying its properties
			switch (inFeature.Class.AliasName)
			{
				case "LV_Line":
					{
						CompleteXMLEltByProperties(inFeature, ref outDOMDoc, ref  xmlLink, m_LVLinesPropertiesArray);
						break;
					}
			}
			//-------- Feature Section END related to the "infeature" --------
		}


		// The following CreateBasicXMLItmesForSchematicElt private procedure is used to create the first expected XML items for a XML NodeFeature or LinkFeature
		private void CreateBasicXMLItemsForSchematicElt(ESRI.ArcGIS.Geodatabase.IFeature inFeature,
																										ref MSXML2.DOMDocument outDOMDoc,
																										ref MSXML2.IXMLDOMElement outXMLElement,
																										string inEltTypeName)
		{
			MSXML2.IXMLDOMElement xmlElt_EltTypeName;
			MSXML2.IXMLDOMElement xmlElt_ExternalUID;
			MSXML2.IXMLDOMElement xmlElt_DatasourceName;
			MSXML2.IXMLDOMElement xmlElt_UCID;
			MSXML2.IXMLDOMElement xmlElt_UOID;

			// Specifying its FeatureClassName
			xmlElt_EltTypeName = outDOMDoc.createElement("FeatureClassName");
			outXMLElement.appendChild(xmlElt_EltTypeName);
			if (inFeature.Fields.FindField("Feeder") != -1)
			{
				xmlElt_EltTypeName.nodeTypedValue = inEltTypeName + "sFeeder" + inFeature.get_Value(inFeature.Fields.FindField("Feeder")).ToString();
			}
			else
			{
				xmlElt_EltTypeName.nodeTypedValue = inEltTypeName + "s";
			}

			// Specifying its ExternalUniqueID
			xmlElt_ExternalUID = outDOMDoc.createElement("ExternalUniqueID");
			outXMLElement.appendChild(xmlElt_ExternalUID);
			xmlElt_ExternalUID.nodeTypedValue = inEltTypeName + "-" + inFeature.OID.ToString();

			// Specifying its DatasourceName
			xmlElt_DatasourceName = outDOMDoc.createElement("DatasourceName");
			outXMLElement.appendChild(xmlElt_DatasourceName);
			xmlElt_DatasourceName.nodeTypedValue = "XMLDataSource";

			// Specifying its UCID
			xmlElt_UCID = outDOMDoc.createElement("UCID");
			outXMLElement.appendChild(xmlElt_UCID);
			xmlElt_UCID.nodeTypedValue = inFeature.Class.ObjectClassID;

			// Add UOID to NodeElement
			xmlElt_UOID = outDOMDoc.createElement("UOID");
			outXMLElement.appendChild(xmlElt_UOID);
			xmlElt_UOID.nodeTypedValue = inFeature.OID;
		}

		// The following CompleteXMLEltByProperties private procedure is used to create all the expected propertyset properties listed in the input PropertiesArray array
		private void CompleteXMLEltByProperties(ESRI.ArcGIS.Geodatabase.IFeature inFeature,
																						ref MSXML2.DOMDocument outDOMDoc,
																						ref MSXML2.IXMLDOMElement outXMLElement,
																						string[] propertiesArray)
		{
			int i = 0;
			MSXML2.IXMLDOMElement xmlPropertySet;
			MSXML2.IXMLDOMElement xmlPropertyArray;
			MSXML2.IXMLDOMElement xmlPropertySetProperty;
			MSXML2.IXMLDOMElement xmlProperty_Key;
			MSXML2.IXMLDOMElement xmlProperty_Value;

			if (propertiesArray.Length > 0)
			{
				//-------- PropertySet Section START --------
				// Creating the PropertySet element for the input outXMLElement
				xmlPropertySet = outDOMDoc.createElement("PropertySet");
				outXMLElement.appendChild(xmlPropertySet);
				// Creating the PropertyArray element
				xmlPropertyArray = outDOMDoc.createElement("PropertyArray");
				xmlPropertySet.appendChild(xmlPropertyArray);

				while (i < propertiesArray.Length)
				{
					// Creating the i PropertySetProperty
					xmlPropertySetProperty = outDOMDoc.createElement("PropertySetProperty");
					xmlPropertyArray.appendChild(xmlPropertySetProperty);
					// Specifying the key && value field related to that i PropertySetProperty
					xmlProperty_Key = outDOMDoc.createElement("Key");
					xmlPropertySetProperty.appendChild(xmlProperty_Key);
					xmlProperty_Key.nodeTypedValue = propertiesArray[i].ToString();
					xmlProperty_Value = outDOMDoc.createElement("Value");
					xmlPropertySetProperty.appendChild(xmlProperty_Value);
					xmlProperty_Value.nodeTypedValue = inFeature.get_Value(inFeature.Fields.FindField(propertiesArray[i].ToString()));
					i += 1;
				}
			}
			//-------- PropertySet Section END --------
		}
		#endregion

		public object ApplicationHook
		{
			get
			{
				return (object)m_application;
			}
			set
			{
				m_application = (ESRI.ArcGIS.ArcMap.Application)value;
			}
		}

	}
}
