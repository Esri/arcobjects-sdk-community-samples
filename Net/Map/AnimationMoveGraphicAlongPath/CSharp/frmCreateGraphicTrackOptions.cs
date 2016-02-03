using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace AnimationDeveloperSamples
{
    public partial class frmCreateGraphicTrackOptions : Form
    {
        public ICreateGraphicTrackOptions createTrackOptions;
        public IAnimationExtension AnimationExtension;
        public IGeometry lineFeature;
        public ILineElement lineGraphic;
        public IElement pointGraphic;

        public frmCreateGraphicTrackOptions()
        {
            InitializeComponent();
            createTrackOptions = new CreateGraphicTrackOptions();
            lineFeature = null;
            lineGraphic = null;
            pointGraphic = null;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            createTrackOptions.OverwriteTrack = this.checkBoxOverwriteTrack.Checked;
            createTrackOptions.ReverseOrder = this.checkBoxReverseOrder.Checked;
            createTrackOptions.TrackName = this.textBoxTrackName.Text;
            createTrackOptions.SimplificationFactor = (double)this.trackBar1.Value / 100.0;
            createTrackOptions.PointElement = pointGraphic;
            createTrackOptions.LineElement = lineGraphic;
            createTrackOptions.AnimatePath = this.checkBoxTracePath.Checked;

            if (this.radioButtonLineFeature.Checked)
            {
                createTrackOptions.PathGeometry = lineFeature;
            }
            else if (this.radioButtonLineGraphic.Checked)
            {
                IElement temp = (IElement)lineGraphic;
                createTrackOptions.PathGeometry = temp.Geometry;
            }            

            IAGAnimationTracks tracks = AnimationExtension.AnimationTracks;
            IAGAnimationContainer pContainer = tracks.AnimationObjectContainer;

            AnimationUtils.CreateMapGraphicTrack(createTrackOptions, tracks, pContainer);
                                    
            AnimationExtension.AnimationContentsModified();
            this.Close();
        }

        private void frmCreateGraphicTrackOptions_Load(object sender, EventArgs e)
        {
            IAGAnimationTracks tracks = AnimationExtension.AnimationTracks;
            int i = 1;
            string recommendedTrackName = "Map Graphic track " + i;
            while (CheckTrackName(tracks, recommendedTrackName))
            {
                i++;
                recommendedTrackName = "Map Graphic track " + i;
            }
            this.textBoxTrackName.Text = recommendedTrackName;
            this.checkBoxOverwriteTrack.Checked = false;
            this.checkBoxReverseOrder.Checked = false;
            this.checkBoxTracePath.Checked = false;
            this.trackBar1.Minimum = 0;
            this.trackBar1.Maximum = 100;

            RefreshPathSourceOptions();

            helpProvider1.SetHelpString(this.radioButtonLineFeature,"Use a selected line feature as the path source.");
            helpProvider1.SetHelpString(this.radioButtonLineGraphic, "Use a selected line graphic as the path source.");
            helpProvider1.SetHelpString(this.checkBoxOverwriteTrack, "Check to overwrite existing tracks that have the same name as specified.");
            helpProvider1.SetHelpString(this.checkBoxReverseOrder, "Check to create a track that moves the graphic in a reversed direction.");
            helpProvider1.SetHelpString(this.checkBoxTracePath, "Check to show the trace of the moving point graphic in the animation. By default, the trace will be shown as a red dashed line following the path of the point graphic. The symbology of the trace can be changed in the display window after you play or preview the animation once.");
            helpProvider1.SetHelpString(this.trackBar1, "With a non-zero simplification factor, the line will be simplified and smoother.");
            helpProvider1.SetHelpString(this.textBoxTrackName, "Type a name for the track.");
        }

        //The following function check if a track name exists
        private bool CheckTrackName(IAGAnimationTracks pTracks, string name)
        {
            IArray trackArray = pTracks.AGTracks;
            IAGAnimationTrack pTrack;
            int count = pTracks.TrackCount;
            bool trackExist = false;
            for (int i = 0; i < count; i++)
            {
                pTrack = (IAGAnimationTrack)trackArray.get_Element(i);
                if (name == pTrack.Name)
                {
                    trackExist = true;
                    break;
                }
            }
            return trackExist;
        }

        public void RefreshPathSourceOptions()
        {
            if (lineFeature != null)
                this.radioButtonLineFeature.Enabled = true;
            else
                this.radioButtonLineFeature.Enabled = false;

            if (lineGraphic != null)
            {
                this.radioButtonLineGraphic.Enabled = true;
            }
            else
            {
                this.radioButtonLineGraphic.Enabled = false;
            }

            if (this.radioButtonLineFeature.Enabled)
                this.radioButtonLineFeature.Checked = true;
            else
                this.radioButtonLineGraphic.Checked = true;
        }
    }

    public interface ICreateGraphicTrackOptions
    {
        double SimplificationFactor
        {
            get;
            set;
        }

        string TrackName
        {
            get;
            set;
        }

        bool OverwriteTrack
        {
            get;
            set;
        }

        IGeometry PathGeometry
        {
            get;
            set;
        }

        bool ReverseOrder
        {
            get;
            set;
        }

        IElement PointElement
        {
            get;
            set;
        }

        ILineElement LineElement
        {
            get;
            set;
        }

        bool AnimatePath
        {
            get;
            set;
        }
    }

    public class CreateGraphicTrackOptions : ICreateGraphicTrackOptions
    {
        private double simpFactor;
        private bool showTrace;
        private string importTrackName;
        private bool overwriteTrack;
        private IGeometry pathGeo;
        private bool reverseOrder;
        private IElement element;
        private ILineElement lineElement;

        public double SimplificationFactor 
        {
            get {
                return simpFactor;
            }
            set {
                simpFactor = value;
            }
        }

        public string TrackName
        {
            get {
                return importTrackName;
            }
            set {
                importTrackName = value;
            }
        }

        public bool OverwriteTrack
        {
            get {
                return overwriteTrack;
            }
            set {
                overwriteTrack = value;
            }
        }

        public IGeometry PathGeometry
        {
            get {
                return pathGeo;
            }
            set {
                pathGeo = value;
            }
        }

        public bool ReverseOrder
        {
            get {
                return reverseOrder;
            }
            set {
                reverseOrder = value;
            }
        }

        public IElement PointElement
        {
            get {
                return element;
            }
            set {
                element = value;
            }
        }

        public ILineElement LineElement
        {
            get {
                return lineElement;
            }
            set {
                lineElement = value;
            }
        }

        public bool AnimatePath
        {
            get {
                return showTrace;
            }
            set {
                showTrace = value;
            }
        }
    }
}