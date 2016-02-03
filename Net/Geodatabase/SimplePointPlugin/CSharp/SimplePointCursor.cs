using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ESRI.ArcGIS.Samples.SimplePointPlugin
{
	/// <summary>
	/// Summary description for SimplePointCursor.
	/// </summary>
	[ComVisible(false)]
	internal class SimplePointCursor : IPlugInCursorHelper
	{
		private bool m_bIsFinished = false;
		private int m_iInterate = -1;

		private string m_sbuffer;
		private System.IO.StreamReader m_pStreamReader;
		private int m_iOID = -1;
		
		private System.Array m_fieldMap;
		private IFields m_fields;
		private IEnvelope m_searchEnv;
		private IGeometry m_wkGeom;
		private IPoint[] m_workPts;

		#region HRESULTs definitions
		private const int E_FAIL = unchecked((int)0x80004005);
		private const int S_FALSE = 1;
		#endregion

		private bool m_bM, m_bZ;
	
		public SimplePointCursor(string filePath, IFields fields, int OID, 
			System.Array fieldMap, IEnvelope queryEnv, esriGeometryType geomType)	
		{
			//HIGHLIGHT: 0 - Set up cursor
			m_bIsFinished = false;
			m_pStreamReader = new System.IO.StreamReader(filePath);
			m_fields = fields;
			m_iOID = OID;
			m_fieldMap = fieldMap;
			m_searchEnv = queryEnv;
			switch (geomType)
			{
				case esriGeometryType.esriGeometryPolygon:
					m_wkGeom = new Polygon() as IGeometry;
					m_workPts = new PointClass[5];
					for (int i = 0; i < m_workPts.Length; i++)
						m_workPts[i] = new PointClass();
					break;
				case esriGeometryType.esriGeometryPolyline:
					m_wkGeom = new PolylineClass() as IGeometry;
					m_workPts = new PointClass[5];
					for (int i = 0; i < m_workPts.Length; i++)
						m_workPts[i] = new PointClass();
					break;
				
				case esriGeometryType.esriGeometryPoint:
					m_wkGeom = new PointClass() as IGeometry;
					break;
				default:	//doesn't need to set worker geometry if it is table 
					break;
			}

			//advance cursor so data is readily available
			this.NextRecord();
		}



		#region IPlugInCursorHelper Members

		#region Queries... //HIGHLIGHT: 2 - Query & read data
		public int QueryValues(IRowBuffer Row)
		{
			try
			{
				if (m_sbuffer == null)
					return -1;
	
				for (int i = 0; i < m_fieldMap.GetLength(0); i++)
				{
					//HIGHLIGHT: 2.2 QueryValues - field map interpretation
					if (m_fieldMap.GetValue(i).Equals(-1))
						continue;

          IField valField = m_fields.get_Field(i);
					char parse = m_sbuffer[m_sbuffer.Length - 1];
					switch (valField.Type)
					{
						case esriFieldType.esriFieldTypeInteger:
						case esriFieldType.esriFieldTypeDouble:
						case esriFieldType.esriFieldTypeSmallInteger:
						case esriFieldType.esriFieldTypeSingle:
							Row.set_Value(i, Convert.ToInt32(parse));	//get ascii code # for the character
							break;
						case esriFieldType.esriFieldTypeString:
							Row.set_Value(i, parse.ToString());
							break;
					}
				}
				return m_iInterate;	//HIGHLIGHT: 2.3 QueryValues - return OID
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return -1;
			}
			
		}

		public void QueryShape(IGeometry pGeometry)
		{
			if (pGeometry == null)
				return;

			try
			{
				double x, y;
				x = Convert.ToDouble(m_sbuffer.Substring(0, 6));
				y = Convert.ToDouble(m_sbuffer.Substring(6, 6));

				#region set M and Z aware
				if (m_bZ)
					((IZAware)pGeometry).ZAware = true;
				if (m_bM)
					((IMAware)pGeometry).MAware = true;
				#endregion

				//HIGHLIGHT: 2.1 QueryShape - (advanced) geometry construction
				if (pGeometry is IPoint)
				{
					((IPoint)pGeometry).PutCoords(x, y);
					if (m_bM)
						((IPoint)pGeometry).M = m_iInterate;
					if (m_bZ)
						((IPoint)pGeometry).Z = m_iInterate * 100;
				}
				else if (pGeometry is IPolyline)	
					buildPolyline((IPointCollection)pGeometry, x, y);
				else if (pGeometry is IPolygon)
					buildPolygon((IPointCollection)pGeometry, x, y);
				else
					pGeometry.SetEmpty();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(" Error: " + ex.Message);
				pGeometry.SetEmpty();
			}
		}

		#endregion

		#region Next... //HIGHLIGHT: 1 - Looping mechanism
		public bool IsFinished()
		{
			return m_bIsFinished;
		}

		public void NextRecord()
		{
			if (m_bIsFinished)	//error already thrown once
				return;

			//OID search has been performed
			if (m_iOID > -1 && m_sbuffer != null)
			{
				m_pStreamReader.Close();
				m_bIsFinished = true;	

				throw new COMException("End of SimplePoint Plugin cursor", E_FAIL);
			}
			else
			{
				//HIGHLIGHT: 1.1 Next - Read the file for text
				m_sbuffer = ReadFile(m_pStreamReader, m_iOID);
				if (m_sbuffer == null)
				{
					//finish reading, close the stream reader so resources will be released
					m_pStreamReader.Close();
					m_bIsFinished = true;	

					//HIGHLIGHT: 1.2 Next - Raise E_FAIL to notify end of cursor
					throw new COMException("End of SimplePoint Plugin cursor", E_FAIL);
				}
				//HIGHLIGHT: 1.3 Next - Search by envelope; or return all records and let post-filtering do 
				//the work for you (performance overhead)
				else if (m_searchEnv != null && !(m_searchEnv.IsEmpty))	
				{
					this.QueryShape(m_wkGeom);
					IRelationalOperator pRelOp = (IRelationalOperator)m_wkGeom;					
					if (!pRelOp.Disjoint((IGeometry)m_searchEnv))
						return;	//HIGHLIGHT: 1.4 Next - valid record within search geometry - stop advancing
					else
						this.NextRecord();
				}
			}
			
		}
		#endregion

		#endregion

		#region Geometry construction

		public bool HasM
		{
			set 
			{
				m_bM = value;
			}
		}

		public bool HasZ
		{
			set
			{
				m_bZ = value;
			}
		}

		private void buildPolygon(IPointCollection pGonColl, double x, double y)
		{
			m_workPts[0].PutCoords(x - 500, y - 500);
			m_workPts[1].PutCoords(x + 500, y - 500);
			m_workPts[2].PutCoords(x + 500, y + 500);
			m_workPts[3].PutCoords(x - 500, y + 500);
			m_workPts[4].PutCoords(x - 500, y - 500);
			try
			{
				bool add = (pGonColl.PointCount == 0);
				object missingVal = System.Reflection.Missing.Value;
					
				for (int i = 0; i< m_workPts.Length; i++)
				{
					((IZAware)m_workPts[i]).ZAware = m_bZ;
					((IMAware)m_workPts[i]).MAware = m_bM;

					if (m_bM)
						m_workPts[i].M = i % 4;
					if (m_bZ)
						m_workPts[i].Z = (i % 4) * 100;	//match start and end points
						
					if (add)
						pGonColl.AddPoint(m_workPts[i], ref missingVal, ref missingVal);	//The Add method only accepts either a before index or an after index.	
					else
						pGonColl.UpdatePoint(i, m_workPts[i]);
				}
			}

			catch (Exception Ex)
			{System.Diagnostics.Debug.WriteLine(Ex.Message);}	
			//Attempted to store an element of the incorrect type into the array.
		}

		private void buildPolyline(IPointCollection pGonColl, double x, double y)
		{
			m_workPts[0].PutCoords(x - 500, y - 500);
			m_workPts[1].PutCoords(x + 500, y - 500);
			m_workPts[2].PutCoords(x + 500, y + 500);
			m_workPts[3].PutCoords(x - 500, y + 500);
			m_workPts[4].PutCoords(x, y);

			try
			{
				bool add = (pGonColl.PointCount == 0);
			
					object missingVal = System.Reflection.Missing.Value;
					for (int i = 0; i< m_workPts.Length; i++)
					{
						((IZAware)m_workPts[i]).ZAware = m_bZ;
						((IMAware)m_workPts[i]).MAware = m_bM;

						if (m_bM)
							m_workPts[i].M =  i;
						if (m_bZ)
							m_workPts[i].Z = i * 100;
						//add it point by point - .Net IDL limitation to do batch update?
						if (add)	//pGonColl.AddPoints(5, ref m_workPts[0]);//strange error of type mismatch
							pGonColl.AddPoint(m_workPts[i], ref missingVal, ref missingVal);	//The Add method only accepts either a before index or an after index.	
						else
							pGonColl.UpdatePoint(i, m_workPts[i]);
					}

				//Can I user replace point collection or addPointcollection?
			}

			catch (Exception Ex)
			{System.Diagnostics.Debug.WriteLine(Ex.Message);}	
			//Attempted to store an element of the incorrect type into the array.
		}

		#endregion

		private string ReadFile(System.IO.StreamReader sr, int lineNumber)
		{
			m_iInterate++;
			string buffer = sr.ReadLine();

			if (buffer == null)
				return null;

			if (lineNumber > -1 && lineNumber != m_iInterate)
				buffer = ReadFile(sr, lineNumber);
			//System.Diagnostics.Debug.WriteLine(buffer);
			return buffer;
		}

	}
}
