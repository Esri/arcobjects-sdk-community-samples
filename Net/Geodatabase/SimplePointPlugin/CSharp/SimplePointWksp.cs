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

namespace ESRI.ArcGIS.Samples.SimplePointPlugin
{
	/// <summary>
	/// Summary description for SimplePointWksp.
	/// </summary>
	
	[ComVisible(false)]
	internal class SimplePointWksp: IPlugInWorkspaceHelper, IPlugInMetadataPath
	{
		private string m_sWkspPath;
	
		public SimplePointWksp(string wkspPath)
		{
			//HIGHLIGHT: set up workspace path
			if (System.IO.Directory.Exists(wkspPath))
				m_sWkspPath = wkspPath;
			else
				m_sWkspPath = null;
		}

		#region IPlugInWorkspaceHelper Members
		public bool OIDIsRecordNumber
		{
			get
			{
				return true;	//OID's are continuous
			}
		}

		public IArray get_DatasetNames(esriDatasetType DatasetType)
		{
			if (m_sWkspPath == null)
				return null;

			//HIGHLIGHT: get_DatasetNames - Go through wksString to look for csp files
			if (DatasetType != esriDatasetType.esriDTAny && 
				DatasetType != esriDatasetType.esriDTTable)
				return null;

			string[] sFiles = System.IO.Directory.GetFiles(m_sWkspPath, "*.csp");
			if (sFiles == null || sFiles.Length == 0)
				return null;

			IArray datasets = new ArrayClass();
			foreach (string sFileName in sFiles)
			{
				SimplePointDataset ds = new SimplePointDataset(m_sWkspPath, System.IO.Path.GetFileNameWithoutExtension(sFileName));
				datasets.Add(ds);
			}

			return datasets;
		}

		public IPlugInDatasetHelper OpenDataset(string localName)
		{
			//HIGHLIGHT: OpenDataset - give workspace path and local file name
			if (m_sWkspPath == null)
				return null;

			SimplePointDataset ds = new SimplePointDataset(m_sWkspPath, localName);
			return (IPlugInDatasetHelper)ds;
		}

		public INativeType get_NativeType(esriDatasetType DatasetType, string localName)
		{
			return null;
		}

		public bool RowCountIsCalculated
		{
			get
			{
				return true;
			}
		}

		#endregion

		#region IPlugInMetadataPath Members

		//HIGHLIGHT: implement metadata so export data in arcmap works correctly
		public string get_MetadataPath(string localName)
		{
			return System.IO.Path.Combine(m_sWkspPath, localName + ".csp.xml");
		}

		#endregion
	}
}
