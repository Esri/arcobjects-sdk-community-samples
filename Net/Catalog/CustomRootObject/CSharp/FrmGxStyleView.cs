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
