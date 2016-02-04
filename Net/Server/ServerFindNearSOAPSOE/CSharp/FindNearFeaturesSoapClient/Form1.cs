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
// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.4/userestrictions.txt.
// 

using NetFindNearFeaturesSOAPClient.localhost;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetFindNearFeaturesSOAPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private object GetProperty(PropertySet props, string key)
        {
            foreach (PropertySetProperty prop in props.PropertyArray)
            {
                if (string.Compare(prop.Key, key, true) == 0)
                    return prop.Value;
            }
            return null;
        }

        private void GetCenterPointAndDistance(EnvelopeN extent, out PointN center, out double distance)
        {
            center = new PointN();
            center.SpatialReference = extent.SpatialReference;
            center.X = extent.XMin + (Math.Abs(extent.XMax - extent.XMin)/2);
            center.Y = extent.YMin + (Math.Abs(extent.YMax - extent.YMin)/2);

            distance = Math.Abs(extent.XMax - extent.XMin)/10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //create instance of proxy
                var nearFeatsService = new localhost.USA_NetFindNearFeaturesSoapSOE();
                nearFeatsService.Url ="http://localhost:6080/arcgis/services/USA/MapServer/NetFindNearFeaturesSoapSOE";

                //getLayerInfos
                CustomLayerInfo[] layerInfos = nearFeatsService.GetLayerInfos();

                foreach (CustomLayerInfo layerInfo in layerInfos)
                {
                    EnvelopeN extent = (EnvelopeN) layerInfo.Extent;

                    debug(
                        string.Format("Layer {0} has ID: {1} and extent: {2},{3},{4},{5}",
                                      layerInfo.Name, layerInfo.ID, extent.XMin, extent.YMin, extent.XMax, extent.YMax));
                }


                //findNearFeatures
                CustomLayerInfo aLayerInfo = layerInfos[0];

                PointN location;
                double distance;
                GetCenterPointAndDistance((EnvelopeN) aLayerInfo.Extent, out location, out distance);

                RecordSet feats = nearFeatsService.FindNearFeatures(aLayerInfo.ID, location, distance);

                foreach (Record record in feats.Records)
                {
                    foreach (object o in record.Values)
                        if (o != null)
                            debug(o.ToString() + ", ");
                    debug("\n");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void debug(string info)
        {
            richTextBox1.AppendText(info + "\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            var info = new CustomLayerInfo
                           {
                               Extent = new EnvelopeN
                                            {
                                                XMin = 0,
                                                YMin = 0,
                                                XMax = 100,
                                                YMax = 999
                                            },
                               ID = 123,
                               Name = "Custom Layer Object"
                           };

            var nearFeatsService = new localhost.USA_NetFindNearFeaturesSoapSOE();
            nearFeatsService.Url ="http://localhost:6080/arcgis/services/USA/MapServer/NetFindNearFeaturesSoapSOE";


            var result = nearFeatsService.DemoCustomObjectInput(info);

            var extent = (EnvelopeN) result.Extent;

            debug(
                string.Format("Layer {0} has ID: {1} and extent: {2},{3},{4},{5}",
                              result.Name, result.ID, extent.XMin, extent.YMin, extent.XMax, extent.YMax));

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            var infos = new List<CustomLayerInfo>()
            {
                new CustomLayerInfo
                {
                    Extent = new EnvelopeN
                    {
                        XMin = 0,
                        YMin = 0,
                        XMax = 100,
                        YMax = 999
                    },
                    ID = 123,
                    Name = "Custom Layer Object 1"
                }
                ,new CustomLayerInfo
                {
                    Extent = new EnvelopeN
                    {
                        XMin = 101,
                        YMin = 101,
                        XMax = 202,
                        YMax = 202
                    },
                    ID = 456,
                    Name = "Custom Layer Object 2"
                }
            };

            var nearFeatsService = new localhost.USA_NetFindNearFeaturesSoapSOE();
            nearFeatsService.Url = "http://localhost:6080/arcgis/services/USA/MapServer/NetFindNearFeaturesSoapSOE";

            var result = nearFeatsService.DemoArrayOfCustomObjectsInput(infos.ToArray());

            foreach (var info in result)
            {
                var extent = (EnvelopeN)info.Extent;

                debug(
                    string.Format("Layer {0} has ID: {1} and extent: {2},{3},{4},{5}",
                                  info.Name, info.ID, extent.XMin, extent.YMin, extent.XMax, extent.YMax));
            }
        }
    }
}
