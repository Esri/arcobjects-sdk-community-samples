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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.esriSystem;

namespace CustomRootObject_CS
{
    public partial class FrmGxStyleView : Form
    {
        private clsGxStyleView g_pGxView;
        private Bitmap bmpPreview = null;

        public FrmGxStyleView()
        {
            InitializeComponent();
            picStylePreview.Paint += new PaintEventHandler(picStylePreview_Paint);
        }

        public void RefreshView()
        {
            GeneratePreview();
            if (picStylePreview != null)
                picStylePreview.Refresh();
        }

        public clsGxStyleView GxStyleView
        {
            get { return g_pGxView; }
            set { g_pGxView = value; }
        }

        private void GeneratePreview()
        {
            if ((picStylePreview == null) || (g_pGxView == null))
                return;

            tagRECT r = new tagRECT();
            r.bottom = picStylePreview.ClientSize.Height;
            r.top = 0;
            r.right = picStylePreview.ClientSize.Width;
            r.left = 0;

            bmpPreview = new Bitmap(r.right, r.bottom);
            System.Drawing.Graphics GrpObj = Graphics.FromImage(bmpPreview);
            try
            {
                g_pGxView.PreviewItem((Int64)GrpObj.GetHdc(), r);
                GrpObj.ReleaseHdc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                GrpObj.Dispose();
            }
            picStylePreview.Image = bmpPreview;
        }

        private void picStylePreview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (bmpPreview == null)
                GeneratePreview();
        }
        private void picStylePreview_SizeChanged(object sender, EventArgs e)
        {
            GeneratePreview();
        }
    }
}
