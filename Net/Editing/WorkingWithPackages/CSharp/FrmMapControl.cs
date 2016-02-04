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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;
using System.Net.Sockets;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using System.IO;


/// note: need to implement test is connected
/// 
namespace WorkingWithPackages
{


  public partial class FrmMapControl : Form
  {
    private string packageLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ArcGIS\Packages";
    private string webMapLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ArcGIS\Web Maps";
    private string myLayerPackageName = @"Layer Package";
    private string myMapPackageName = "Events";
    private string myWebMapName = @"USA Topo Maps";
    private enum PackageType { LayerPackage, MapPackage };
    private FileInfo[] layerFiles;

    public FrmMapControl()
    {
      InitializeComponent();
    }

    private void btnLoadlpk_Click(object sender, EventArgs e)
    {
      try
      {
        ILayerFile layerFile = new LayerFileClass();
        // Test to see if we can connect to ArcGIS.com
        if (IsConnected())
        {
          // If so, open the Layer Package from ArcGIS.com, this will get the most recent data.
          // for the package.  If there is no change, the data will not get re-downloaded, 
          // just use what is stored on disk.
          layerFile.Open(txtLayerPackage.Text.ToString());
        }
        else
        {
          // If we cannot connect to ArcGIS.com use what was previously downloaded.
          if (DoesPackageExist(PackageType.LayerPackage))
          {
            foreach (FileInfo layerpackage in layerFiles)
            {
              // Layer packages can have multiple layers included in them.  However,
              // the LayerFile and MapDocument classes will only get the first one.
              // Here the sample is using an array so we can get all the layer files
              // and not worry about the name.
              layerFile.Open(layerpackage.FullName);
            }
          }
        }
        ILayer layer = layerFile.Layer;
        axMapControl1.AddLayer(layer);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Failed to open Layer Package!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnLoadmpk_Click(object sender, EventArgs e)
    {
      try
      {
        IMapDocument mapDocument = new MapDocumentClass();
        if (IsConnected())
        {
          mapDocument.Open(txtMapPackage.Text, "");
        }
        else
        {
          if (DoesPackageExist(PackageType.MapPackage))
          {
            mapDocument.Open(packageLocation, "");
          }

        }
        axMapControl1.Map = mapDocument.get_Map(0);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Failed to open Map Package!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    private void btnWebMap_Click(object sender, EventArgs e)
    {
      if (!(IsConnected()))
      {
        // Since Web Maps require and internet connection anyways
        // just fail if there isn't one.
        MessageBox.Show("Cannot Connect to ArcGIS.com!", "Failed to open Web Map.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        IMapDocument mapDocument = new MapDocumentClass();
        string webMapMxd = webMapLocation + @"\" + myWebMapName + @"\" + myWebMapName + ".mxd";
        if (File.Exists(webMapMxd))
          mapDocument.Open(webMapMxd, "");
        else
          mapDocument.Open(txtWebMap.Text, "");
        axMapControl1.Map = mapDocument.get_Map(0);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Failed to open Web Map!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private bool DoesPackageExist(PackageType packageType)
    {
      // Packages and Web Maps are handled by the FileHandler in Common Files\ArcGIS\bin
      // The default package location can be controled be the user.
      RegistryKey regKey = Registry.CurrentUser;
      RegistryKey subKey = regKey.OpenSubKey(@"Software\ESRI\ArcGIS File Handler\Settings");
      int option = (int)subKey.GetValue("PackageLocationOption", 0);
      if (1 == option)
      {
        // Change the package location if different otherwise use the default.
        string fileHandlerUserSetting = subKey.GetValue(@"PackageLocation", "").ToString();
        if ("" != fileHandlerUserSetting )
        {
          packageLocation = fileHandlerUserSetting;
        }
      }
      switch (packageType)
      {
        case PackageType.LayerPackage:
          {
            // Layer Packages can have two different directories, depending on if the 
            // package contains a single layer or multiple layers.
            DirectoryInfo directoryInfo; 
            string layerPackageLocation = packageLocation + @"\" + myLayerPackageName.Replace(" ", "_");
            if (Directory.Exists(layerPackageLocation))
            {
              directoryInfo = new DirectoryInfo(layerPackageLocation);
              layerFiles = directoryInfo.GetFiles("*.lyr");
            }
            else
            {
              layerPackageLocation = layerPackageLocation + @".lpk";
              if (Directory.Exists(layerPackageLocation))
              {
                directoryInfo = new DirectoryInfo(layerPackageLocation);
                layerFiles = directoryInfo.GetFiles("*.lyr");
              }
            }

            if (layerFiles.Length > 0)
              {
                packageLocation = layerPackageLocation;
                return true;
              }
            break;
          }
        case PackageType.MapPackage:
          {
            packageLocation = packageLocation + @"\" + myMapPackageName.Replace(" ", "_") + @"\v10\" + myMapPackageName + ".mxd";
            return File.Exists(packageLocation);
          }
        default:
          {
            return false;
          }
      }
      return false;
    }

    private bool IsConnected()
    {
      try
      {
        // Test to see if we can connect to ArcGIS.com
        TcpClient tcpClient = new TcpClient("www.arcgis.com", 80);
        tcpClient.Close();
        return true;
      }
      catch (System.Exception ex)
      {
        return false;
      }

    }

  }
}
