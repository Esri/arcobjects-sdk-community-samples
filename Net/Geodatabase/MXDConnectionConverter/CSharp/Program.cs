/*

   Copyright 2020 Esri

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
using System.IO;
using System.Text;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;

namespace MXDconnection_converter
{
    class Program
    {
        private static LicenseInitializer m_AOLicenseInitializer = new MXDconnection_converter.LicenseInitializer();
    
        [STAThread()]
        static void Main(string[] args)
        {
            // Check and assign arguments
            String mxdFolder = ""; // @"C:\TEMP\testconns\";
            String newMxdStringToAdd = "DC";
            Boolean verbose = false;

            if (args == null || args.Length == 0)
            {
                mxdFolder = Directory.GetCurrentDirectory().ToString();
            }
            else if (args.Length == 1)
            {
                if ((args[0] == "?") || (args[0] == "--help"))
                {
                    Console.WriteLine("Usage: MXDconnection_converter.exe [{Current Directory | <PathToMxdDirectory>}] [{DC | <newMxdStringToAdd>}] [VERBOSE] [{? | --help}]");
                    return;
                }
                else
                    mxdFolder = args[0];
            }
            else if (args.Length == 2)
            {
                mxdFolder = args[0];
                newMxdStringToAdd = args[1];
            }
            else if (args.Length == 3)
            {
                mxdFolder = args[0];
                newMxdStringToAdd = args[1];
                if (args[2].ToLower() == "true")
                    verbose = true;
            }

            // ESRI License Initializer generated code.
            // Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB },
                                                         new esriLicenseExtensionCode[] { });
 
            // Put all the mxd files in the specified folder into a list
            String[] arrMxdFiles = Directory.GetFiles(mxdFolder, "*.mxd");

            Console.WriteLine("============================================");
            Console.WriteLine(" MXDconnection_converter");
            Console.WriteLine("     Checking MXD's at: {0}", mxdFolder);
            Console.WriteLine("============================================");

            int m = 1;
            // Run operation for all mxd's in the list
            foreach (String mxdFile in arrMxdFiles)
            {                
                try
                {
                    // Open the map document
                    IMapDocument mpdoc = new MapDocument();
                    String mxdName = "";
                    mxdName = Path.GetFileNameWithoutExtension(mxdFile);
                    
                    // Check if mxd file already has the supplied extension. If it does skip, since we believe it has already been converted.
                    // Very basic check, does NOT consider different languages/cultures
                    if (mxdName.EndsWith(String.Format("_{0}", newMxdStringToAdd)))
                    {
                        if (verbose)
                        {
                            Console.WriteLine(" Skipping {0}.mxd...", mxdName);
                            Console.WriteLine("============================================");
                        }
                        
                        continue;
                    }

                    Console.WriteLine(" Converting {0}.mxd...", mxdName);

                    mpdoc.Open(String.Format(@"{0}", mxdFile));                   

                    // Get the first data frame; would need to interrogate all data frames in production
                    IMap map = mpdoc.get_Map(0);
                    
                    // Walk the layer collection (this should work for stand alone rasters too).
                    for (int i = 0; i < map.LayerCount; i++)
                    {                        
                        ILayer l = map.get_Layer(i);

                        // Other layer types may be required in production (e.g. IRaster, etc.)
                        if (verbose)
                        {
                            Console.WriteLine("----------------");
                            Console.WriteLine(" Layer: {0}", l.Name);
                        }
                        // Update the dataLayer info
                        ITable table = (ITable)l;
                        UpdateDatalayerInfo(table, verbose);
                    }

                    // Walk the stand alone table collection
                    IStandaloneTableCollection saTableColl = (IStandaloneTableCollection)map;                    
                    for (int i = 0; i < saTableColl.StandaloneTableCount; i++)
                    {
                        IStandaloneTable t = saTableColl.get_StandaloneTable(i);

                        // Other layer types may be required in production (e.g. IRaster, etc.)
                        if (verbose)
                        {
                            Console.WriteLine("----------------");
                            Console.WriteLine(" Stand Alone Table: {0}", t.Name);
                        }

                        // Update the dataLayer info
                        UpdateDatalayerInfo((ITable)t, verbose);

                    }                                

                    // Save as a new mxd
                    String newMxdFile = String.Format(@"{0}\{1}_{2}.{3}", mxdFolder, mxdName, newMxdStringToAdd, "mxd");
                    mpdoc.SaveAs(newMxdFile);
                    
                    String newMxdName = Path.GetFileNameWithoutExtension(newMxdFile);
                    if (verbose)
                    {
                        Console.WriteLine(" Saved {0}.mxd.", newMxdName);
                        Console.WriteLine("============================================");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                m++;

            } // End foreach MXD

            Console.WriteLine(" Done.");
            Console.WriteLine("============================================");

            m_AOLicenseInitializer.ShutdownApplication();

        } // Main
        
        public static IWorkspace ConnectToGDB(String server, String dbclient, String instance, String user, String password, String database, String version)
        {
            // how to use this:
            //connect to GDB
            //IWorkspace myWorkspace = ConnectToGDB("server1", "Oracle", "sde:oracle:server1/orcl", "map", "map", "none", "SDE.DEFAULTS");

            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("SERVER", server);
            //propertySet.SetProperty("INSTANCE", instance);  //Use this one for old connection syntax
            propertySet.SetProperty("DBCLIENT", dbclient);  //new connection syntax
            propertySet.SetProperty("DB_CONNECTION_PROPERTIES", instance);  //new connection syntax
            propertySet.SetProperty("DATABASE", database);
            propertySet.SetProperty("USER", user);
            propertySet.SetProperty("PASSWORD", password);
            propertySet.SetProperty("VERSION", version);

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");   //Use this one for geodatabase
            //Type factoryType2 = Type.GetTypeFromProgID("esriDataSourcesGDB.SqlWorkspaceFactory");  //Use this one for database
           
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return workspaceFactory.Open(propertySet, 0);

        } // ConnectToGDB

        private static void UpdateDatalayerInfo(ITable table, Boolean verbose)
        {
            String server = "";
            String dbclient = "";
            String instanceOrig = "";
            String instanceNew = "";
            IDataLayer dataLayer = null;
            IRasterLayer rasterLayer = null;

            if (table is IRasterLayer)
            {
                rasterLayer = (IRasterLayer)table;
                dataLayer = (IDataLayer)rasterLayer;
            }
            else
                dataLayer = (IDataLayer)table;

            IName n = dataLayer.DataSourceName;
            IDatasetName dsn = (IDatasetName)n;
            IWorkspaceName2 wsn = (IWorkspaceName2)dsn.WorkspaceName;

            IWorkspaceFactory2 wsFact = (IWorkspaceFactory2)wsn.WorkspaceFactory;
            IWorkspace2 workspace = (IWorkspace2)wsFact.OpenFromString(wsn.ConnectionString.ToString(), 0);

            // Access connection properties, update, and re-apply
            IPropertySet propertySet = wsn.ConnectionProperties;

            // Confirm original
            server = propertySet.GetProperty("SERVER").ToString();
            instanceOrig = propertySet.GetProperty("INSTANCE").ToString();

            if (verbose)
                Console.WriteLine("Original: {0}, {1}", server, instanceOrig);

            // Only need to set if currently app server, check if "instance" string has a colon in it
            // Is there a way to check this based on the list by source so that we don't have to check every layer?
            if (instanceOrig.Contains(":") == false)
            {
                // Determine what kind of DBMS we are working with (Oracle, SqlServer, etc.)
                IDatabaseConnectionInfo3 dbmsConnInfo = (IDatabaseConnectionInfo3)workspace;

                // Set the DBCLIENT to the appropriate dbms
                switch (dbmsConnInfo.ConnectionDBMS)
                {
                    case esriConnectionDBMS.esriDBMS_Oracle:
                        dbclient = "Oracle";
                        instanceNew = String.Format("{0}/{1}", dbmsConnInfo.ConnectionServer, dbmsConnInfo.ConnectedDatabaseEx);
                        break;
                    case esriConnectionDBMS.esriDBMS_SQLServer:
                        dbclient = "SQLServer";
                        instanceNew = dbmsConnInfo.ConnectionServer;
                        break;
                    case esriConnectionDBMS.esriDBMS_PostgreSQL:
                        dbclient = "PostgreSQL";
                        instanceNew = dbmsConnInfo.ConnectionServer;
                        break;
                    default:
                        dbclient = "";
                        break;
                }

                // Set to new target
                propertySet.SetProperty("DBCLIENT", dbclient);
                propertySet.SetProperty("DB_CONNECTION_PROPERTIES", instanceNew);
                
                // INSTANCE needs to be cleared out for Raster layers
                if (table is IRasterLayer)
                    propertySet.SetProperty("INSTANCE", "");//propertySet.SetProperty("INSTANCE", String.Format("sde:{0}",dbclient.ToLower()));

                wsn.ConnectionProperties = propertySet;

                // Confirm target was set
                propertySet = null;
                propertySet = wsn.ConnectionProperties;

                if (verbose)
                {
                    Console.WriteLine("NEW: {0}, {1}", propertySet.GetProperty("DBCLIENT"), propertySet.GetProperty("DB_CONNECTION_PROPERTIES"));
                    Console.WriteLine("----------------");
                }

            } // End if

        }  // UpdateDatalayerInfo  
            
    } // Program

} // Namespace
