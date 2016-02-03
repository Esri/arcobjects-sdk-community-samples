using System.Windows.Forms;
using System.Drawing;

namespace GlobeGraphicsToolbar
{
    public class ColorPalette
    {
        private ColorDialog _colorDialog;

        public ColorPalette()
        {
            _colorDialog = new ColorDialog();

            InitializeUI();

            SetDefaultColor();
        }

        private void InitializeUI()
        {
            _colorDialog.FullOpen = true;
        }

        private void SetDefaultColor()
        {
            _colorDialog.Color = Color.Yellow;
        }

        public bool IsColorSelected()
        {
            return _colorDialog.ShowDialog() == DialogResult.OK;
        }

        public int Red
        {
            get
            {
                return (int)_colorDialog.Color.R;
            }
        }

        public int Green
        {
            get
            {
                return (int)_colorDialog.Color.G;
            }
        }

        public int Blue
        {
            get
            {
                return (int)_colorDialog.Color.B;
            }
        }
    }
}