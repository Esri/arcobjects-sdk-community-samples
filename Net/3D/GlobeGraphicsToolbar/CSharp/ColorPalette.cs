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