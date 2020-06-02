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
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesGDB;


namespace VisualizeCameraPath
{
    public class VisualizeCameraPath : ESRI.ArcGIS.Desktop.AddIns.Button
    {

        #region Member Variables

        private ESRI.ArcGIS.GlobeCore.IGlobe globe;
        private ESRI.ArcGIS.GlobeCore.IGlobeDisplay globeDisplay;
        private ESRI.ArcGIS.Carto.IGraphicsContainer graphicsLayer;
        private ESRI.ArcGIS.GlobeCore.IGlobeCamera globeCamera;
        private ESRI.ArcGIS.Animation.IAGAnimationTracks animationTracks;
        private ESRI.ArcGIS.Animation.IAGAnimationTrack animationTrack;
        private VisualizeCameraPathForm theCamForm;
        private ESRI.ArcGIS.Animation.IAGAnimationUtils animUtils;
        private ESRI.ArcGIS.Animation.IAnimationEvents_Event animEvent;
        private ESRI.ArcGIS.Animation.IAGAnimationEnvironment animEnv;
        private ESRI.ArcGIS.Animation.IAGAnimationPlayer animPlayer;
        private ESRI.ArcGIS.GlobeCore.IGlobeDisplayEvents_Event globeDispEvent;
        private bool toolIsInitialized = false;
        private double animationDuration = 0;

        #endregion

        #region DLLImportFunction

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        static extern int ShowWindow(int hwnd, int nCmdShow);


        #endregion

        public VisualizeCameraPath()
        {
            globe = ArcGlobe.Globe;
            globeDisplay = globe.GlobeDisplay;
            globeCamera = globeDisplay.ActiveViewer.Camera as IGlobeCamera;
        }


        ~VisualizeCameraPath()
        {
            if (theCamForm != null)
            {
                theCamForm.Dispose();
            }
        }

        protected override void OnClick()
        {
            //The first time button is clicked
			if(toolIsInitialized == false)
			{
				theCamForm = new VisualizeCameraPathForm();
			
				//Add event handlers for form's button click events
				theCamForm.playButton.Click+= new System.EventHandler(formPlayButtonClickEventHandler);
				theCamForm.stopButton.Click+= new System.EventHandler(formStopButtonClickEventHandler);
				theCamForm.generatePathButton.Click+= new System.EventHandler(formGeneratePathButtonClickEventHandler);
	
				theCamForm.generateCamPathCheckBox.CheckedChanged += new EventHandler(formCheckbox1CheckedChanged);

				theCamForm.Closing += new CancelEventHandler(theCamForm_Closing);

				animEnv = new AGAnimationEnvironmentClass();

				//True = Button has been already clicked
				toolIsInitialized = true;
            }
            //If the main form is already open - do not open another one
            else if (toolIsInitialized == true)
            {
                //Clear the list of animation tracks
                theCamForm.animTracksListBox.Items.Clear();
            }
            //Get the list of animation tracks
            this.getCameraAnimationTracksFromGlobe();
            theCamForm.Show();

        }

        protected override void OnUpdate()
        {
            Enabled = ArcGlobe.Application != null;
        }


        #region Custom Functions and Event Handlers

        //function for getting camera animation tracks
        public void getCameraAnimationTracksFromGlobe()
        {
            ESRI.ArcGIS.Animation.IAGAnimationType animationType = new AnimationTypeGlobeCameraClass();
            animationTracks = (ESRI.ArcGIS.Animation.IAGAnimationTracks)globe;
            int animCounter = 0;
            while (animCounter < animationTracks.AGTracks.Count)
            {
                animationTrack = (ESRI.ArcGIS.Animation.IAGAnimationTrack)animationTracks.AGTracks.get_Element(animCounter);
                if (animationTrack.AnimationType == animationType)
                {
                    theCamForm.animTracksListBox.Items.Add(animationTrack.Name);
                }
                animCounter = animCounter + 1;
            }
        }

        //function for enabling selected animation track
        public void enableSelectedTrack()
        {
            if (theCamForm.animTracksListBox.SelectedItem != null)
            {
                string selectedTrack = theCamForm.animTracksListBox.SelectedItem.ToString();
                int animCounter = 0;
                while (animCounter < animationTracks.AGTracks.Count)
                {
                    animationTrack = (IAGAnimationTrack)animationTracks.AGTracks.get_Element(animCounter);
                    if (animationTrack.Name != selectedTrack)
                    {
                        IAGAnimationTrack trackToDisable;
                        animationTracks.FindTrack(animationTrack.Name, out trackToDisable);
                        trackToDisable.IsEnabled = false;
                    }
                    else if (animationTrack.Name == selectedTrack)
                    {
                        animationTrack.IsEnabled = true;
                    }
                    animCounter = animCounter + 1;
                }
            }

            else if (theCamForm.animTracksListBox.SelectedItem == null)
            {
                MessageBox.Show("No Track Selected - All enabled tracks will be played");
            }
        }

        //function for playing animation
        public void playAnimation()
        {
            animUtils = new AGAnimationUtilsClass();
            //register/unregister events for tracing camera path based on selection
            animEvent = (IAnimationEvents_Event)animUtils;

            //set animation duration
            if (theCamForm.animDurationTextBox.Text != "" & theCamForm.animDurationTextBox.Text != "Optional")
            {
                animEnv.AnimationDuration = Convert.ToDouble(theCamForm.animDurationTextBox.Text);
            }
            else
            {
                MessageBox.Show("Please enter animation duration", "Error");
                return;
            }

            //register animation event handler
            animEvent.StateChanged += new IAnimationEvents_StateChangedEventHandler(myAnimationEventHandler);

            //enable/disable other buttons
            theCamForm.stopButton.Enabled = true;
            theCamForm.generatePathButton.Enabled = false;
            theCamForm.playButton.Enabled = false;

            animPlayer = (IAGAnimationPlayer)animUtils;

            animationDuration = animEnv.AnimationDuration;

            animPlayer.PlayAnimation(animationTracks, animEnv, null);
        }

        //function for creating specified number of graphics per second
        public void generatePathPerSecond()
        {
            //set animation duration
            if (theCamForm.animDurationTextBox.Text != "" & theCamForm.animDurationTextBox.Text != "Optional")
            {
                animEnv.AnimationDuration = Convert.ToDouble(theCamForm.animDurationTextBox.Text);
            }
            else
            {
                MessageBox.Show("Please enter animation duration", "Error");
                return;
            }
            animationDuration = animEnv.AnimationDuration;

            int numPtsPerSecond = 0;
            if (theCamForm.numPtsPerSecTextBox.Text != "")
            {
                numPtsPerSecond = Convert.ToInt32(theCamForm.numPtsPerSecTextBox.Text);
            }

            addGraphicLayer();

            string selectedTrack = theCamForm.animTracksListBox.SelectedItem.ToString();

            animationTracks.FindTrack(selectedTrack, out animationTrack);

            IAGAnimationTrackKeyframes kFrames = (IAGAnimationTrackKeyframes)animationTrack;

            int kFrameCount = kFrames.KeyframeCount;

            //total number of points to be created
            int totalPts = (int)(numPtsPerSecond * animationDuration);

            //this is the from point for the lines connecting the interpolated point graphics
            IPoint previousPt = new PointClass();
            IZAware prevPtZAware = (IZAware)previousPt;
            prevPtZAware.ZAware = true;
            previousPt.PutCoords(0, 0);

            //this is the line connecting the interpolated camera positions
            IPolyline connectingLine = new PolylineClass();
            IZAware lineZAware = (IZAware)connectingLine;
            lineZAware.ZAware = true;

            //disable all buttons
            theCamForm.playButton.Enabled = false;
            theCamForm.stopButton.Enabled = false;
            theCamForm.generatePathButton.Enabled = false;

            //loop over the keyframes in the selected camera track
            for (int i = 0; i < kFrameCount; i++)
            {
                IAGKeyframe currentKeyframe = kFrames.get_Keyframe(i);
                IAGKeyframe prevKeyframe;
                IAGKeyframe nextKeyframe;
                IAGKeyframe afterNextKeyframe;

                //if else statements to determine the keyframe arguments to the interpolate method
                //this is needed because the first, second-last and the last keyframes should be handled differently
                //than the middle keyframes
                if (i > 0)
                {
                    prevKeyframe = kFrames.get_Keyframe(i - 1);
                }

                else
                {
                    prevKeyframe = kFrames.get_Keyframe(i);
                }

                if (i < kFrameCount - 1)
                {
                    nextKeyframe = kFrames.get_Keyframe(i + 1);
                }
                else
                {
                    nextKeyframe = kFrames.get_Keyframe(i);
                }

                if (i < kFrameCount - 2)
                {
                    afterNextKeyframe = kFrames.get_Keyframe(i + 2);
                }
                else
                {
                    //this should be equal to the nextKeyFrame for the last keyframe
                    afterNextKeyframe = nextKeyframe;//kFrames.get_Keyframe(i); 
                }

                double origCamLat, origCamLong, origCamAlt;
                double interLat, interLong, interAlt;
                double tarLat, tarLong, tarAlt;
                double interTarLat, interTarLong, interTarAlt;

                globeCamera.GetObserverLatLonAlt(out origCamLat, out origCamLong, out origCamAlt);
                globeCamera.GetTargetLatLonAlt(out tarLat, out tarLong, out tarAlt);

                IAGAnimationContainer pAnimContainer = animationTracks.AnimationObjectContainer;

                object objToInterpolate = (object)globeCamera;

                double timeDiff = nextKeyframe.TimeStamp - currentKeyframe.TimeStamp;

                int numPtsToInterpolateNow;
                numPtsToInterpolateNow = Convert.ToInt32((timeDiff * totalPts));

                //interpolate positions between keyframes and draw the graphics

                //for 0 to n-1 keyframes
                if (i < kFrameCount - 1)
                {
                    for (int j = 0; j < numPtsToInterpolateNow; j++)
                    {
                        double timeToInterpolate;
                        timeToInterpolate = currentKeyframe.TimeStamp + j * (timeDiff / (numPtsToInterpolateNow));

                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 1, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 2, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 3, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 4, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 5, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 6, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);

                        //get observer and target lat, long, alt after interpolation
                        globeCamera.GetObserverLatLonAlt(out interLat, out interLong, out interAlt);
                        globeCamera.GetTargetLatLonAlt(out interTarLat, out interTarLong, out interTarAlt);

                        //set observer and target lat, long, alt to original values before interpolation
                        globeCamera.SetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt);
                        globeCamera.SetTargetLatLonAlt(tarLat, tarLong, tarAlt);

                        IPoint pObs = new PointClass();
                        IZAware obsZAware = (IZAware)pObs;
                        obsZAware.ZAware = true;
                        pObs.X = interLong;
                        pObs.Y = interLat;
                        pObs.Z = interAlt * 1000;

                        double symbolSize = 10000;

                        //change the symbol size based on distance to ground
                        if (pObs.Z >= 10000) symbolSize = 10000 + pObs.Z / 10;
                        else symbolSize = pObs.Z;

                        //add graphics - keyframes (j=0) are colored differently
                        if (j == 0) addPointGraphicElements(pObs, 2552550, symbolSize);
                        else addPointGraphicElements(pObs, 16732415, symbolSize);

                        connectingLine.FromPoint = previousPt;
                        connectingLine.ToPoint = pObs;

                        //barring the first keyframe, create the line connecting the interpolated points
                        if (i == 0 & j == 0) { }
                        else
                        {
                            addLineGraphicElements(connectingLine, 150150150);
                        }

                        //update the previous point
                        previousPt.PutCoords(pObs.X, pObs.Y);
                        previousPt.Z = pObs.Z;

                        //add camera to target direction
                        if (theCamForm.camToTargetDirectionCheckBox.Checked == true)
                        {
                            cameraToTargetDirection(interLat, interLong, interAlt, interTarLat, interTarLong, interTarAlt);
                        }

                        globeDisplay.RefreshViewers();
                    }
                }

                //for last keyframe
                if (i == kFrameCount - 1)
                {
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 4, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 5, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 6, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 1, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 2, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 3, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);

                    globeCamera.GetObserverLatLonAlt(out interLat, out interLong, out interAlt);
                    globeCamera.GetTargetLatLonAlt(out interTarLat, out interTarLong, out interTarAlt);

                    globeCamera.SetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt);
                    globeCamera.SetTargetLatLonAlt(tarLat, tarLong, tarAlt);

                    IPoint pObs = new PointClass();
                    IZAware obsZAware = (IZAware)pObs;
                    obsZAware.ZAware = true;
                    pObs.X = interLong;
                    pObs.Y = interLat;
                    pObs.Z = interAlt * 1000;

                    double symbolSize = 10000;

                    if (pObs.Z >= 10000) symbolSize = 10000 + pObs.Z / 10;
                    else symbolSize = pObs.Z;

                    connectingLine.FromPoint = previousPt;
                    connectingLine.ToPoint = pObs;

                    addPointGraphicElements(pObs, 2552550, symbolSize);
                    addLineGraphicElements(connectingLine, 150150150);

                    //add camera to target orientation
                    if (theCamForm.camToTargetDirectionCheckBox.Checked == true)
                    {
                        cameraToTargetDirection(interLat, interLong, interAlt, interTarLat, interTarLong, interTarAlt);
                    }

                    globeDisplay.RefreshViewers();
                }
            }

            //enable buttons
            theCamForm.playButton.Enabled = true;
            theCamForm.generatePathButton.Enabled = true;
        }

        //function for creating specified number of graphics between keyframe positions
        public void generatePathBtwnKFrames()
        {
            int numPtsBtwnKFrames = 0;

            //this is the from point for the lines connecting the interpolated point graphics
            IPoint previousPt = new PointClass();
            IZAware prevPtZAware = (IZAware)previousPt;
            prevPtZAware.ZAware = true;
            previousPt.PutCoords(0, 0);

            //this is the line connecting the interpolated camera positions
            IPolyline connectingLine = new PolylineClass();
            IZAware lineZAware = (IZAware)connectingLine;
            lineZAware.ZAware = true;

            if (theCamForm.ptsBtwnKframeTextBox.Text != "")
            {
                numPtsBtwnKFrames = Convert.ToInt32(theCamForm.ptsBtwnKframeTextBox.Text);
            }
            else
            {
                MessageBox.Show("Please enter the number of points to be created");
                return;
            }
            theCamForm.playButton.Enabled = false;
            theCamForm.stopButton.Enabled = false;
            theCamForm.generatePathButton.Enabled = false;

            addGraphicLayer();

            string selectedTrack = theCamForm.animTracksListBox.SelectedItem.ToString();

            animationTracks.FindTrack(selectedTrack, out animationTrack);

            IAGAnimationTrackKeyframes kFrames = (IAGAnimationTrackKeyframes)animationTrack;

            int kFrameCount = kFrames.KeyframeCount;

            //loop over the keyframes in the selected camera track
            for (int i = 0; i < kFrameCount; i++)
            {
                IAGKeyframe currentKeyframe = kFrames.get_Keyframe(i);
                IAGKeyframe prevKeyframe;
                IAGKeyframe nextKeyframe;
                IAGKeyframe afterNextKeyframe;

                //if else statements to determine the keyframe arguments to the interpolate method
                //this is needed because the first and the last keyframes should be handled differently
                //than the middle keyframes
                if (i > 0)
                {
                    prevKeyframe = kFrames.get_Keyframe(i - 1);
                }

                else
                {
                    prevKeyframe = kFrames.get_Keyframe(i);
                }

                if (i < kFrameCount - 1)
                {
                    nextKeyframe = kFrames.get_Keyframe(i + 1);
                }
                else
                {
                    nextKeyframe = kFrames.get_Keyframe(i);
                }

                if (i < kFrameCount - 2)
                {
                    afterNextKeyframe = kFrames.get_Keyframe(i + 2);
                }
                else
                {
                    //this should be equal to the nextKeyFrame for the last keyframe
                    afterNextKeyframe = nextKeyframe;//kFrames.get_Keyframe(i); 
                }

                double origCamLat, origCamLong, origCamAlt;
                double interLat, interLong, interAlt;
                double tarLat, tarLong, tarAlt;
                double interTarLat, interTarLong, interTarAlt;

                globeCamera.GetObserverLatLonAlt(out origCamLat, out origCamLong, out origCamAlt);
                globeCamera.GetTargetLatLonAlt(out tarLat, out tarLong, out tarAlt);


                IAGAnimationContainer pAnimContainer = animationTracks.AnimationObjectContainer;

                object objToInterpolate = (object)globeCamera;

                double timeDiff = nextKeyframe.TimeStamp - currentKeyframe.TimeStamp;

                //interpolate positions between keyframes and draw the graphics
                for (int j = 0; j < numPtsBtwnKFrames + 1; j++)
                {
                    double timeToInterpolate = currentKeyframe.TimeStamp + j * (timeDiff / (numPtsBtwnKFrames + 1));

                    //for 0 to n-1 keyframes
                    if (i >= 0 & i < kFrameCount - 1)
                    {
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 4, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 5, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 6, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 1, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 2, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 3, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe);

                        globeCamera.GetObserverLatLonAlt(out interLat, out interLong, out interAlt);
                        globeCamera.GetTargetLatLonAlt(out interTarLat, out interTarLong, out interTarAlt);

                        globeCamera.SetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt);
                        globeCamera.SetTargetLatLonAlt(tarLat, tarLong, tarAlt);

                        IPoint pObs = new PointClass();
                        IZAware obsZAware = (IZAware)pObs;
                        obsZAware.ZAware = true;
                        pObs.X = interLong;
                        pObs.Y = interLat;
                        pObs.Z = interAlt * 1000;

                        double symbolSize = 10000;

                        if (pObs.Z >= 10000) symbolSize = 10000 + pObs.Z / 10;
                        else symbolSize = pObs.Z;

                        if (j == 0) addPointGraphicElements(pObs, 2552550, symbolSize);
                        else addPointGraphicElements(pObs, 16732415, symbolSize);

                        connectingLine.FromPoint = previousPt;
                        connectingLine.ToPoint = pObs;

                        if (i == 0 & j == 0) { }
                        else
                        {
                            addLineGraphicElements(connectingLine, 150150150);
                        }

                        previousPt.PutCoords(pObs.X, pObs.Y);
                        previousPt.Z = pObs.Z;

                        //add camera to target orientation
                        if (theCamForm.camToTargetDirectionCheckBox.Checked == true)
                        {
                            cameraToTargetDirection(interLat, interLong, interAlt, interTarLat, interTarLong, interTarAlt);
                        }

                        globeDisplay.RefreshViewers();
                    }

                    //for last keyframe
                    else if (i == kFrameCount - 1)
                    {
                        if (j == 0)
                        {
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 4, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 5, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 6, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 1, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 2, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 3, 1, nextKeyframe, prevKeyframe, afterNextKeyframe);

                            globeCamera.GetObserverLatLonAlt(out interLat, out interLong, out interAlt);
                            globeCamera.GetTargetLatLonAlt(out interTarLat, out interTarLong, out interTarAlt);

                            globeCamera.SetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt);
                            globeCamera.SetTargetLatLonAlt(tarLat, tarLong, tarAlt);

                            IPoint pObs = new PointClass();
                            IZAware obsZAware = (IZAware)pObs;
                            obsZAware.ZAware = true;
                            pObs.X = interLong;
                            pObs.Y = interLat;
                            pObs.Z = interAlt * 1000;

                            double symbolSize = 10000;

                            if (pObs.Z >= 10000) symbolSize = 10000 + pObs.Z / 10;
                            else symbolSize = pObs.Z;

                            connectingLine.FromPoint = previousPt;
                            connectingLine.ToPoint = pObs;

                            addPointGraphicElements(pObs, 2552550, symbolSize);
                            addLineGraphicElements(connectingLine, 150150150);

                            //add camera to target orientation
                            if (theCamForm.camToTargetDirectionCheckBox.Checked == true)
                            {
                                cameraToTargetDirection(interLat, interLong, interAlt, interTarLat, interTarLong, interTarAlt);
                            }

                            globeDisplay.RefreshViewers();
                        }
                    }
                }
            }

            //enable buttons
            theCamForm.playButton.Enabled = true;
            theCamForm.generatePathButton.Enabled = true;
        }

        //function for generating camera to target direction
        public void cameraToTargetDirection(double camLat, double camLong, double camAlt, double tarLat, double tarLong, double tarAlt)
        {
            IPoint camPosition = new PointClass();
            IPoint targetPosition = new PointClass();

            ICamera pCamera = (ICamera)globeCamera;

            IZAware obsZAware = (IZAware)camPosition;
            obsZAware.ZAware = true;

            camPosition.PutCoords(camLong, camLat);
            camPosition.Z = camAlt * 1000;

            IZAware targetZAware = (IZAware)targetPosition;
            targetZAware.ZAware = true;

            targetPosition.PutCoords(tarLong, tarLat);
            targetPosition.Z = tarAlt;

            IPolyline directionLine = new PolylineClass();
            IZAware zAwareLine = (IZAware)directionLine;
            zAwareLine.ZAware = true;

            directionLine.FromPoint = camPosition;
            directionLine.ToPoint = targetPosition;


            addLineGraphicElements(directionLine, 255);

        }

        //function for adding a graphics layer
        public void addGraphicLayer()
        {
            graphicsLayer = new GlobeGraphicsLayerClass();
            ILayer pLayer;
            pLayer = (ILayer)graphicsLayer;
            pLayer.Name = "CameraPathGraphicsLayer";
            globe.AddLayerType(pLayer, esriGlobeLayerType.esriGlobeLayerTypeDraped, true);
        }

        //function for adding point markers
        public void addPointGraphicElements(ESRI.ArcGIS.Geometry.IPoint inPoint, int symbolColor, double symbolSize)
        {
            IElement pElement = new MarkerElementClass();
            ISimpleMarker3DSymbol symbol3d = new SimpleMarker3DSymbolClass();

            string markerStyle = "";

            if (theCamForm.symbolTypeListBox.SelectedItem != null)
            {
                markerStyle = theCamForm.symbolTypeListBox.SelectedItem.ToString();
            }

            if (markerStyle == "Cone") symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSCone;
            else if (markerStyle == "Sphere") symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSSphere;
            else if (markerStyle == "Cylinder") symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSCylinder;
            else if (markerStyle == "Cube") symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSCube;
            else if (markerStyle == "Diamond") symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSDiamond;
            else if (markerStyle == "Tetrahedron") symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSTetra;
            else symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSCone;

            symbol3d.ResolutionQuality = 1;
            IColor pColor = new RgbColorClass();
            pColor.RGB = symbolColor; //16732415;

            IMarkerSymbol pMarkerSymbol;
            pMarkerSymbol = (IMarkerSymbol)symbol3d;
            pMarkerSymbol.Color = pColor;

            if (symbolSize < 0) symbolSize = Math.Abs(symbolSize);
            if (symbolSize == 0) symbolSize = 5000;
            pMarkerSymbol.Size = symbolSize;

            pElement.Geometry = inPoint;
            IMarkerElement pMarkerElement;
            pMarkerElement = (IMarkerElement)pElement;
            pMarkerElement.Symbol = pMarkerSymbol;

            graphicsLayer.AddElement(pElement, 1);

        }

        //function for adding line graphics elements
        public void addLineGraphicElements(ESRI.ArcGIS.Geometry.IPolyline inLine, int symbolColor)
        {
            IElement pElement = new LineElementClass();// MarkerElementClass();
            ISimpleLine3DSymbol symbol3d = new SimpleLine3DSymbolClass();

            string markerStyle = "";

            if (theCamForm.symbolTypeListBox.SelectedItem != null)
            {
                markerStyle = theCamForm.symbolTypeListBox.SelectedItem.ToString();
            }

            if (markerStyle == "Strip") symbol3d.Style = esriSimple3DLineStyle.esriS3DLSStrip;
            else if (markerStyle == "Wall") symbol3d.Style = esriSimple3DLineStyle.esriS3DLSWall;
            else symbol3d.Style = esriSimple3DLineStyle.esriS3DLSTube;

            symbol3d.ResolutionQuality = 1;
            IColor pColor = new RgbColorClass();
            pColor.RGB = symbolColor;

            ILineSymbol pLineSymbol;
            pLineSymbol = (ILineSymbol)symbol3d;
            pLineSymbol.Color = pColor;
            pLineSymbol.Width = 1;

            pElement.Geometry = inLine;
            ILineElement pLineElement;
            pLineElement = (ILineElement)pElement;
            pLineElement.Symbol = pLineSymbol;

            graphicsLayer.AddElement(pElement, 1);
        }


        //event handlers
        public void formPlayButtonClickEventHandler(object sender, System.EventArgs e)
        {
            if (theCamForm.animTracksListBox.SelectedItem != null)
            {
                enableSelectedTrack();
                //play the animation
                this.playAnimation();
            }
            else
            {
                MessageBox.Show("Please select a camera track", "Error");
            }
        }

        public void formStopButtonClickEventHandler(object sender, System.EventArgs e)
        {
            animPlayer.StopAnimation();
            theCamForm.stopButton.Enabled = false;
            if (theCamForm.generateCamPathCheckBox.Checked == true) theCamForm.generatePathButton.Enabled = true;
        }

        public void formGeneratePathButtonClickEventHandler(object sender, System.EventArgs e)
        {
            if (theCamForm.animTracksListBox.SelectedItem != null)
            {
                if (theCamForm.ptsPerSecRadioButton.Checked == true)
                {
                    if (theCamForm.numPtsPerSecTextBox.Text == "")
                    {
                        MessageBox.Show("Please enter number of points to be created per second", "Error");
                        return;
                    }
                    generatePathPerSecond();
                }
                else if (theCamForm.ptsBtwnKframeRadioButton.Checked == true)
                {
                    if (theCamForm.ptsBtwnKframeTextBox.Text == "")
                    {
                        MessageBox.Show("Please enter number of points to be created between keyframes", "Error");
                        return;
                    }
                    generatePathBtwnKFrames();
                }
            }
            else
            {
                MessageBox.Show("Please select a camera track");
            }
        }

        public void formCheckbox1CheckedChanged(object sender, System.EventArgs e)
        {
            if (theCamForm.generateCamPathCheckBox.Checked == true) theCamForm.generatePathButton.Enabled = true;
            else theCamForm.generatePathButton.Enabled = false;
        }

        private void theCamForm_Closing(object sender, CancelEventArgs e)
        {
            theCamForm.animTracksListBox.Items.Clear();
            toolIsInitialized = false;
        }

        public void myAnimationEventHandler(esriAnimationState animState)
        {
            globeDispEvent = (IGlobeDisplayEvents_Event)globeDisplay;

            if (animState == esriAnimationState.esriAnimationStopped)
            {
                theCamForm.playButton.Enabled = true;
                theCamForm.generatePathButton.Enabled = true;
                theCamForm.stopButton.Enabled = false;
            }
        }

        #endregion		

    }
}
