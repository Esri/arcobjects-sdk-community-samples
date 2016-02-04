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
// Copyright 2013 ESRI
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSimpleSoapSOAPClient.localhost;

namespace NetSimpleSoapSOAPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //create instance of proxy
                USA_NetSimpleSoapSOE echoService = new USA_NetSimpleSoapSOE();             
          
                
                echoService.Url = "http://localhost:6080/arcgis/services/USA/MapServer/NetSimpleSoapSOE";

                label2.Text = echoService.EchoInput(textBox1.Text);
            }
            catch (Exception ex)
            {
                label2.Text = ex.Message;
            }
        }
    }
}
