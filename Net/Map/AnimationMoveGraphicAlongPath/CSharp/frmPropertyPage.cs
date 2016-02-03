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