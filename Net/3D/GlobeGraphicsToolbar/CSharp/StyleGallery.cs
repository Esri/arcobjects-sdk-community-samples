using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using GlobeGraphicsToolbar;
namespace GlobeGraphicsToolbar
{
    public class StyleGallery
    {
        private StyleGalleryForm _styleForm;

        public StyleGallery()
        {
            _styleForm = new StyleGalleryForm();

            InitializeUI();
        }

        private void InitializeUI()
        {
            _styleForm.AutoSize = true;
            _styleForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            _styleForm.MaximizeBox = false;
            _styleForm.MinimizeBox = false;
            _styleForm.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            _styleForm.Text = "Style Gallery";
        }

        public bool IsStyleSelected()
        {
            return _styleForm.ShowDialog() == DialogResult.OK;
        }

        public IStyleGalleryItem StyleGalleryItem
        {
            get
            {
                return _styleForm.StyleGalleryItem;
            }
        }
    }
}