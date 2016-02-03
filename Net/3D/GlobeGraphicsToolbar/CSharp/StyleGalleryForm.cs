using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;


namespace GlobeGraphicsToolbar
{
    public partial class StyleGalleryForm : Form
    {
        private IStyleGalleryItem _styleGalleryItem = null;

        public StyleGalleryForm()
        {
            InitializeComponent();
        }

        private void axSymbologyControl1_OnItemSelected(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            _styleGalleryItem = e.styleGalleryItem as IStyleGalleryItem;
        }

        public IStyleGalleryItem StyleGalleryItem
        {
            get
            {
                return _styleGalleryItem;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }


    }
}
