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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
namespace ESRI.ArcGIS.Samples.SimplePointPlugin
{
	/// <summary>
	/// Summary description for SimplePointDataset.
	/// </summary>
	[ComVisible(false)]
	internal class SimplePointDataset: IPlugInDatasetHelper, IPlugInDatasetInfo
	{
		private string m_wkspString, m_datasetString;
		private IEnvelope m_bound;
		private string m_fullPath;

		public SimplePointDataset(string wkspString, string datasetString)
		{
			//HIGHLIGHT: constructor checks valid workspace string path and dataset name
			m_wkspString = wkspString;
			m_fullPath = System.IO.Path.Combine(wkspString, datasetString);
			if (System.IO.Path.HasExtension(datasetString))
				m_datasetString = System.IO.Path.GetFileNameWithoutExtension(datasetString);
			else
			{
				m_datasetString = datasetString;	
				m_fullPath += ".csp";	//add the extension
			}
		}

	
		#region IPlugInDatasetHelper Members

		public IEnvelope Bounds
		{
			get
			{
				if (this.DatasetType == esriDatasetType.esriDTTable)
					return null;
				if (m_bound == null)
				{
					#region use cursor go through records, or we can parse the file directly
					m_bound = new EnvelopeClass();
					m_bound.SpatialReference = this.spatialReference;

					
					IFields fields = this.get_Fields(0);
					int[] fieldMapArray = new int[fields.FieldCount];
					for (int i = 0; i < fields.FieldCount; i++)
						fieldMapArray[i] = -1;	//shape field always ignored?
					
					double x1 = 999999, y1 = 999999, x2 = 0, y2 = 0;	//assumes all positive value in the file

					//Set with appropriate geometry
					IGeometry workGeom;
					IPlugInCursorHelper cursor;
					if (this.DatasetType == esriDatasetType.esriDTFeatureDataset)
					{
						workGeom = new PolygonClass();
						cursor = this.FetchAll(2, null, fieldMapArray);
					}
					else
					{
						workGeom = new PointClass();
						cursor = this.FetchAll(0, null, fieldMapArray);
					}

					workGeom.SpatialReference = this.spatialReference;
					while (true)
					{
						try
						{
							cursor.QueryShape(workGeom);
							if (workGeom.Envelope.XMin < x1) 
								x1 = workGeom.Envelope.XMin;
							if (workGeom.Envelope.XMax > x2)
								x2 = workGeom.Envelope.XMax;

							if (workGeom.Envelope.YMin < y1) 
								y1 = workGeom.Envelope.YMin;
							if (workGeom.Envelope.YMax > y2)
								y2 = workGeom.Envelope.YMax;					

							cursor.NextRecord();
						}
						catch (COMException comEx)
						{
							System.Diagnostics.Debug.WriteLine(comEx.Message);
							break;	//catch E_FAIL when cursor reaches the end, exit loop
						}
						catch (Exception ex)
						{
							System.Diagnostics.Debug.WriteLine(ex.Message);
						}
					}

					m_bound.PutCoords(x1, y1, x2, y2);

					#endregion
				}

				//HIGHLIGHT: return clone envelope for bound
				IClone cloneEnv = (IClone)m_bound;
				return (IEnvelope)cloneEnv.Clone();
			}
		}

		public int get_ShapeFieldIndex(int ClassIndex)
		{
			//add table to ArcMap via add data dialog calls this method, so if it's a table
			//you must return -1 or else ArcMap crashes
			if (this.DatasetType == esriDatasetType.esriDTTable)
				return -1;

			return 1;
		}

		public IFields get_Fields(int ClassIndex)
		{
			IFieldEdit fieldEdit;
			IFields fields;
			IFieldsEdit fieldsEdit;
			IObjectClassDescription fcDesc;
			if (this.DatasetType == esriDatasetType.esriDTTable)
				fcDesc = new ObjectClassDescriptionClass();
			else
				fcDesc = new FeatureClassDescriptionClass();

			fields = fcDesc.RequiredFields;
			fieldsEdit = (IFieldsEdit)fields;

			fieldEdit = new FieldClass();
			fieldEdit.Length_2 = 1;
			fieldEdit.Name_2 = "ColumnOne";
			fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
			fieldsEdit.AddField((IField)fieldEdit);

			//HIGHLIGHT: Add extra int column
			fieldEdit = new FieldClass();
			fieldEdit.Name_2 = "Extra";
			fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
			fieldsEdit.AddField((IField)fieldEdit);

			//HIGHLIGHT: Set shape field geometry definition
			if (this.DatasetType != esriDatasetType.esriDTTable)
			{
				IField field = fields.get_Field(fields.FindField("Shape"));
				fieldEdit = (IFieldEdit)field;
				IGeometryDefEdit geomDefEdit = (IGeometryDefEdit)field.GeometryDef;
				geomDefEdit.GeometryType_2 = geometryTypeByID(ClassIndex);
				ISpatialReference shapeSRef = this.spatialReference;

				#region M & Z
				//M
				if ((ClassIndex >= 3 && ClassIndex <=5) || ClassIndex >= 9)
				{
					geomDefEdit.HasM_2 = true;
					shapeSRef.SetMDomain(0, 1000);
				}
				else
					geomDefEdit.HasM_2 = false;

				//Z
				if (ClassIndex >= 6)
				{
					geomDefEdit.HasZ_2 = true;
					shapeSRef.SetZDomain(0, 1000);
				}
				else
					geomDefEdit.HasZ_2 = false;
				#endregion

				geomDefEdit.SpatialReference_2 = shapeSRef;
			}

			return fields;
		}

		public string get_ClassName(int Index)
		{
			if (Index % 3 == 0)
				m_datasetString = "Point";
			if (Index % 3 == 1)
				m_datasetString = "Polyline";
			if (Index % 3 == 2)
				m_datasetString = "Polygon";

			if ((Index >= 3 && Index < 6) || Index >= 9)
				m_datasetString += "M";

			if (Index >= 6)
				m_datasetString += "Z";

			return m_datasetString;
		}

		public int get_OIDFieldIndex(int ClassIndex)
		{
			return 0;
		}

		public int ClassCount
		{
			get
			{
				if (this.DatasetType == esriDatasetType.esriDTFeatureDataset)
					return 12;
				return 1;
			}
		}

		public int get_ClassIndex(string Name)
		{
			for (int i = 0; i < this.ClassCount; i++)
			{
				if (Name.Equals(this.get_ClassName(i)))
					return i;
			}
			return -1;
		}

		
		#region Fetching - returns cursor //HIGHLIGHT: Fetching
		public IPlugInCursorHelper FetchAll(int ClassIndex, string WhereClause, object FieldMap)
		{
			try
			{
				SimplePointCursor allCursor = 
					new SimplePointCursor(m_fullPath, this.get_Fields(ClassIndex), -1, 
					(System.Array)FieldMap, null, this.geometryTypeByID(ClassIndex));
				setMZ(allCursor, ClassIndex);
				return (IPlugInCursorHelper)allCursor;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public IPlugInCursorHelper FetchByID(int ClassIndex, int ID, object FieldMap)
		{
			try
			{
				SimplePointCursor idCursor = 
					new SimplePointCursor(m_fullPath, this.get_Fields(ClassIndex), ID, 
					(System.Array)FieldMap, null, this.geometryTypeByID(ClassIndex));

				setMZ(idCursor, ClassIndex);
				return (IPlugInCursorHelper)idCursor;
			}
			catch (Exception ex)	//will catch NextRecord error if it reaches EOF without finding a record
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public IPlugInCursorHelper FetchByEnvelope(int ClassIndex, IEnvelope env, bool strictSearch, string WhereClause, object FieldMap)
		{
			if (this.DatasetType == esriDatasetType.esriDTTable)
				return null;

			//env passed in always has same spatial reference as the data
			//for identify, it will check if search geometry intersect dataset bound
			//but not ITable.Search(pSpatialQueryFilter, bRecycle) etc
			//so here we should check if input env falls within extent
			IEnvelope boundEnv = this.Bounds;
			boundEnv.Project(env.SpatialReference);
			if (boundEnv.IsEmpty)
				return null;	//or raise error?
			try
			{
				SimplePointCursor spatialCursor = new SimplePointCursor(m_fullPath, 
					this.get_Fields(ClassIndex), -1, 
					(System.Array)FieldMap, env, this.geometryTypeByID(ClassIndex));
				setMZ(spatialCursor, ClassIndex);

				return (IPlugInCursorHelper)spatialCursor;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}

		#endregion

		
		#endregion

		#region IPlugInDatasetInfo Members	
		//HIGHLIGHT: IPlugInDatasetInfo - lightweight!
		public string LocalDatasetName
		{
			get
			{
				return m_datasetString;
			}
		}

		public string ShapeFieldName
		{
			get
			{
				if (this.DatasetType == esriDatasetType.esriDTTable)
					return null;
				return "Shape";
			}
		}

		public esriDatasetType DatasetType
		{
			get
			{
//				return esriDatasetType.esriDTTable;
//				return esriDatasetType.esriDTFeatureClass;
				return esriDatasetType.esriDTFeatureDataset;
			}
		}

		public esriGeometryType GeometryType
		{
			get
			{
				return geometryTypeByID(-1);	//might not be always easy to get
			}
		}

		#endregion

		#region internal helper methods
		private esriGeometryType geometryTypeByID(int ClassIndex)
		{
			if (this.DatasetType == esriDatasetType.esriDTTable)
				return esriGeometryType.esriGeometryNull;

			if (ClassIndex % 3 == 0)
				return esriGeometryType.esriGeometryPoint;
			else if (ClassIndex % 3 == 1)
				return esriGeometryType.esriGeometryPolyline;
			else
				return esriGeometryType.esriGeometryPolygon;
		}

		private ISpatialReference spatialReference
		{
			get
			{
				if (this.DatasetType == esriDatasetType.esriDTTable)
					return null;

				//singleton
				ISpatialReferenceFactory2 srefFact = new SpatialReferenceEnvironmentClass();
				return srefFact.CreateProjectedCoordinateSystem
					(Convert.ToInt32(esriSRProjCSType.esriSRProjCS_World_Robinson));	// WGS1984UTM_10N));
			}
		}
	
		private void setMZ(SimplePointCursor sptCursor, int Index)
		{
				sptCursor.HasM = ((Index >= 3 && Index < 6) || Index >= 9);
				sptCursor.HasZ = (Index >= 6);
		}
		#endregion

	}
}
