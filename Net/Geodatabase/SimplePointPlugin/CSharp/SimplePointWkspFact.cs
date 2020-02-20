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
using System.Runtime.InteropServices;
using Microsoft.Win32;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace ESRI.ArcGIS.Samples.SimplePointPlugin
{
	/// <summary>
	/// Summary description for SimplePointWkspFact.
	/// </summary>
	/// 
  [ClassInterface(ClassInterfaceType.None)]
  [Guid("6e167940-6d49-485b-a2b8-061c144d805f")]
  [ProgId("SimplePointPlugin.SimplePointWFHelper")]
	[ComVisible(true)]
  public sealed class SimplePointWkspFact : IPlugInWorkspaceFactoryHelper
  {
    #region "Component Category Registration"

    [ComRegisterFunction()]
    public static void RegisterFunction(String regKey)
    {
      PlugInWorkspaceFactoryHelpers.Register(regKey);
    }

    [ComUnregisterFunction()]
    public static void UnregisterFunction(String regKey)
    {
      PlugInWorkspaceFactoryHelpers.Unregister(regKey);
    }
    #endregion


    #region class constructor
    public SimplePointWkspFact()
    {
    }
    #endregion

    #region IPlugInWorkspaceFactoryHelper Members

    public string get_DatasetDescription(esriDatasetType DatasetType)
    {
      switch (DatasetType)
      {
        case esriDatasetType.esriDTTable:
          return "SimplePoint Table";
        case esriDatasetType.esriDTFeatureClass:
          return "SimplePoint Feature Class";
        case esriDatasetType.esriDTFeatureDataset:
          return "SimplePoint Feature Dataset";
        default:
          return null;
      }
    }

    public string get_WorkspaceDescription(bool plural)
    {
      if (plural)
        return "Simple Points";
      else
        return "Simple Point";
    }

    public bool CanSupportSQL
    {
      get { return false; }
    }

    public string DataSourceName
    {
      //HIGHLIGHT: ProgID = esriGeoDatabase.<DataSourceName>WorkspaceFactory
      get { return "SimplePointPlugin"; }
    }

    public bool ContainsWorkspace(string parentDirectory, IFileNames fileNames)
    {
      if (fileNames == null)
        return this.IsWorkspace(parentDirectory);

      if (!System.IO.Directory.Exists(parentDirectory))
        return false;

      string sFileName;
      while ((sFileName = fileNames.Next()) != null)
      {
        if (fileNames.IsDirectory())
          continue;

        if (System.IO.Path.GetExtension(sFileName).Equals(".csp"))
          return true;
      }

      return false;
    }

    public UID WorkspaceFactoryTypeID
    {
      //HIGHLIGHT: Generate a new GUID to identify the workspace factory
      get
      {
        UID wkspFTypeID = new UIDClass();
        wkspFTypeID.Value = "{b8a25f89-2adc-43c0-ac2e-16b3a88e3915}";	//proxy
        return wkspFTypeID;
      }
    }

    public bool IsWorkspace(string wksString)
    {
      //TODO: IsWorkspace is True when folder contains csp files
      if (System.IO.Directory.Exists(wksString))
        return System.IO.Directory.GetFiles(wksString, "*.csp").Length > 0;
      return false;
    }

    public esriWorkspaceType WorkspaceType
    {
      //HIGHLIGHT: WorkspaceType - FileSystem type strongly recommended
      get
      {
        return esriWorkspaceType.esriFileSystemWorkspace;
      }
    }

    public IPlugInWorkspaceHelper OpenWorkspace(string wksString)
    {
      //HIGHLIGHT: OpenWorkspace
      //Don't have to check if wksString contains valid data file. 
      //Any valid folder path is fine since we want paste to work in any folder
      if (System.IO.Directory.Exists(wksString))
      {
        SimplePointWksp openWksp = new SimplePointWksp(wksString);
        return (IPlugInWorkspaceHelper)openWksp;
      }
      return null;
    }

    public string GetWorkspaceString(string parentDirectory, IFileNames fileNames)
    {
      //return the path to the workspace location if 
      if (!System.IO.Directory.Exists(parentDirectory))
        return null;

      if (fileNames == null)	//don't have to check .csp file
        return parentDirectory;

      //HIGHLIGHT: GetWorkspaceString - claim and remove file names from list
      string sFileName;
      bool fileFound = false;
      while ((sFileName = fileNames.Next()) != null)
      {
        if (fileNames.IsDirectory())
          continue;

        if (System.IO.Path.GetExtension(sFileName).Equals(".csp"))
        {
          fileFound = true;
          fileNames.Remove();
        }
      }

      if (fileFound)
        return parentDirectory;
      else
        return null;
    }

    #endregion


  }
}
