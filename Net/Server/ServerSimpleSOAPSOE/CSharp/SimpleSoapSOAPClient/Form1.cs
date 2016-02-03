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
