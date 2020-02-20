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
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Animation;

namespace AnimationDeveloperSamples
{
    public partial class frmPropertyPage : Form
    {
        private bool pageDirty;
        IAGAnimationTrack targetTrack;

        public frmPropertyPage()
        {
            InitializeComponent();
            pageDirty = false;
        }
        public void Init(IAGAnimationTrack track)
        {
            targetTrack = track;

            IAGAnimationTrackExtensions trackExtensions = (IAGAnimationTrackExtensions)targetTrack;
            IMapGraphicTrackExtension trackExtension;
            if (trackExtensions.ExtensionCount == 0) //if there is no extension, add one
            {
                trackExtension = new MapGraphicTrackExtension();
                trackExtensions.AddExtension(trackExtension);
            }
            else
            {
                trackExtension = (IMapGraphicTrackExtension)trackExtensions.get_Extension(0);
            }
            this.checkBoxTrace.Checked = trackExtension.ShowTrace;
        }
        public CheckBox CheckBoxShowTrace
        {
            get {
                return checkBoxTrace;
            }
        }
        public bool PageDirty
        {
            get {
                return pageDirty;
            }
        }
        private void checkBoxTrace_Click(object sender, EventArgs e)
        {
            pageDirty = true;
        }

        private void frmPropertyPage_Load(object sender, EventArgs e)
        {
            helpProvider1.SetHelpString(this.checkBoxTrace, "Check to show the trace of the moving graphic in the animation. ");
        }
    }
}