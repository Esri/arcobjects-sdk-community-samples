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

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.SchematicControls;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Display;

namespace CurrentDigitTool
{
    internal static class CurrentTool
    {
        public static DigitTool.DigitTool currentDigit;
        public static ESRI.ArcGIS.Framework.IDockableWindow currentDockableWindow;
        public static DigitTool.DigitDockableWindow digitDockableWindow;
    }
}


namespace DigitTool
{
    public class DigitTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        ESRI.ArcGIS.Framework.IApplication m_app;
        ESRI.ArcGIS.esriSystem.IExtension m_schematicExtension;
        ISchematicLayer m_schematicLayer;
        ISchematicLayer m_schematicLayerForLink;
        ISchematicFeature m_schematicFeature1;
        ISchematicFeature m_schematicFeature2;
        INewLineFeedback m_linkFbk;
        private string m_messageFromOK = "\n" + "Complete missing data and click on OK button";

        private int m_x;
        private int m_y;

        public DigitDockableWindow m_dockableDigit;
        private ESRI.ArcGIS.Framework.IDockableWindow m_dockableWindow;
        private bool m_fromDeactivate = false;
        private bool m_DeactivatedFromDock = false;

        public DigitTool()
        {
            m_app = ArcMap.Application;
        }

        protected override void OnUpdate()
        {
            // the tool is enable only if the diagram is in memory
            try
            {
                if (ArcMap.Application == null)
                {
                    Enabled = false;
                    return;
                }

                SetTargetLayer();

                if (m_schematicLayer == null)
                {
                    Enabled = false;
                    return;
                }

                if (m_schematicLayer.IsEditingSchematicDiagram() == false)
                {
                    Enabled = false;
                    if (m_dockableWindow != null)
                        m_dockableWindow.Show(false);

                    return;
                }

                ISchematicInMemoryDiagram inMemoryDiagram = m_schematicLayer.SchematicInMemoryDiagram;

                if (inMemoryDiagram == null)
                    Enabled = false;
                else
                    Enabled = true;
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            return;
        }

        protected override void OnMouseMove(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            try
            {
                if (m_schematicFeature1 != null)
                {
                    ESRI.ArcGIS.ArcMapUI.IMxApplication mxApp = (ESRI.ArcGIS.ArcMapUI.IMxApplication)m_app;

                    if (mxApp == null)
                        return;

                    ESRI.ArcGIS.Display.IAppDisplay appDisplay = mxApp.Display;

                    if (appDisplay == null)
                        return;

                    IScreenDisplay screenDisplay = appDisplay.FocusScreen;

                    if (screenDisplay == null)
                        return;

                    //Move the Feedback to the current mouse location
                    IPoint pnt = screenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y);

                    if (pnt != null && m_linkFbk != null)
                        m_linkFbk.MoveTo(pnt);
                }
            }    
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            return;
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            bool abortOperation = false;
            ESRI.ArcGIS.Schematic.ISchematicOperation schematicOperation = null;

            try
            {
                if (m_dockableDigit == null)
                    return;

                if (arg != null)
                {
                    m_x = arg.X;
                    m_y = arg.Y;
                }

                if (m_dockableWindow == null)
                    return;

                if (m_dockableWindow.IsVisible() == false)
                {
                    m_dockableWindow.Show(true);
                }

                ESRI.ArcGIS.SchematicControls.ISchematicTarget target = (ESRI.ArcGIS.SchematicControls.ISchematicTarget)m_schematicExtension;

                if (target != null)
                    m_schematicLayer = target.SchematicTarget;

                if (m_schematicLayer == null)
                {
                    System.Windows.Forms.MessageBox.Show("No target Layer");
                    return;
                }

                ISchematicInMemoryDiagram inMemoryDiagram;
                ISchematicInMemoryFeatureClass schematicInMemoryFeatureClass;
                ISchematicInMemoryFeatureClassContainer schematicInMemoryFeatureClassContainer;

                //Get the point
                ESRI.ArcGIS.Geometry.Point point = new ESRI.ArcGIS.Geometry.Point();

                ESRI.ArcGIS.ArcMapUI.IMxApplication mxApp;
                ESRI.ArcGIS.Display.IAppDisplay appDisplay;
                IScreenDisplay screenDisplay;
                IDisplay display;
                IDisplayTransformation transform;
                ISpatialReference spatialReference;

                inMemoryDiagram = m_schematicLayer.SchematicInMemoryDiagram;
                schematicInMemoryFeatureClassContainer = (ISchematicInMemoryFeatureClassContainer)inMemoryDiagram;

                if (schematicInMemoryFeatureClassContainer == null)
                    return;

                mxApp = (ESRI.ArcGIS.ArcMapUI.IMxApplication)m_app;

                if (mxApp == null)
                    return;

                appDisplay = mxApp.Display;

                if (appDisplay == null)
                    return;

                screenDisplay = appDisplay.FocusScreen;
                display = screenDisplay;

                if (display == null)
                    return;

                transform = display.DisplayTransformation;

                if (transform == null)
                    return;

                spatialReference = transform.SpatialReference;

                WKSPoint mapPt = new WKSPoint();
                ESRI.ArcGIS.Display.tagPOINT devPoint;
                devPoint.x = m_x;
                devPoint.y = m_y;
                transform.TransformCoords(ref mapPt, ref devPoint, 1, 1); //'esriTransformToMap

                point.SpatialReference = spatialReference;
                point.Project(spatialReference);
                point.X = mapPt.X;
                point.Y = mapPt.Y;

                schematicInMemoryFeatureClass = schematicInMemoryFeatureClassContainer.GetSchematicInMemoryFeatureClass(m_dockableDigit.FeatureClass());

                if (schematicInMemoryFeatureClass == null)
                {
                    System.Windows.Forms.MessageBox.Show("Invalid Type.");
                    return;
                }

                if (m_dockableDigit.CreateNode())
                {
                    //TestMandatoryField
                    m_dockableDigit.btnOKPanel1.Visible = false;

                    if (m_dockableDigit.ValidateFields() == false)
                    {
                        m_dockableDigit.x(m_x);
                        m_dockableDigit.y(m_y);

                        System.Windows.Forms.MessageBox.Show(m_dockableDigit.ErrorProvider1.GetError(m_dockableDigit.btnOKPanel1) + m_messageFromOK);
                        return;
                    }

                    ESRI.ArcGIS.Geometry.IGeometry geometry;

                    ISchematicInMemoryFeature schematicInMemoryFeatureNode;

                    geometry = point;

                    schematicOperation = (ESRI.ArcGIS.Schematic.ISchematicOperation) new ESRI.ArcGIS.SchematicControls.SchematicOperation();

                    //digit operation is undo(redo)able we add it in the stack
                    IMxDocument doc  = (IMxDocument)m_app.Document;
                    ESRI.ArcGIS.SystemUI.IOperationStack operationStack; 
                    operationStack = doc.OperationStack;
                    operationStack.Do(schematicOperation);
                    schematicOperation.StartOperation("Digit", m_app, m_schematicLayer, true);

                    //do abort operation
                    abortOperation = true;

                    schematicInMemoryFeatureNode = schematicInMemoryFeatureClass.CreateSchematicInMemoryFeatureNode(geometry, "");
                    //schematicInMemoryFeatureNode.UpdateStatus = esriSchematicUpdateStatus.esriSchematicUpdateStatusNew; if we want the node deleted after update
                    schematicInMemoryFeatureNode.UpdateStatus = esriSchematicUpdateStatus.esriSchematicUpdateStatusLocked;

                    abortOperation = false;
                    schematicOperation.StopOperation();

                    ISchematicFeature schematicFeature = schematicInMemoryFeatureNode;
                    m_dockableDigit.FillValue(ref schematicFeature);

                    if (m_dockableDigit.AutoClear())
                        m_dockableDigit.SelectionChanged();
                }
                else
                {
                    m_dockableDigit.btnOKPanel2.Visible = false;

                    //Get the Tolerance of ArcMap
                    Double tolerance;
                    IMxDocument mxDocument = (ESRI.ArcGIS.ArcMapUI.IMxDocument)m_app.Document;
                    ESRI.ArcGIS.esriSystem.WKSPoint point2 = new WKSPoint();
                    ESRI.ArcGIS.Display.tagPOINT devPt;

                    tolerance = mxDocument.SearchTolerancePixels;
                    devPt.x = (int)tolerance;
                    devPt.y = (int)tolerance;

                    transform.TransformCoords(ref point2, ref devPt, 1, 2);//2 <-> esriTransformSize 4 <-> esriTransformToMap

                    tolerance = point2.X * 5;//increase the tolerance value

                    IEnumSchematicFeature schematicFeatures = m_schematicLayer.GetSchematicFeaturesAtPoint(point, tolerance, false, true);

                    ISchematicFeature schematicFeatureSelected = null;
                    double distancetmp;
                    double distance = 0;
                    schematicFeatures.Reset();

                    if (schematicFeatures.Count <= 0)
                        return;

                    //pSchematicFeatures may contain several features, we are choosing the closest node.
                    ISchematicFeature schematicFeature2 = schematicFeatures.Next();

                    double dX;
                    double dY;
                    ISchematicInMemoryFeatureNode schematicInMemoryFeatureNode =  null;
                    if (schematicFeature2 != null)
                    {
                        if (schematicFeature2.SchematicElementClass.SchematicElementType == ESRI.ArcGIS.Schematic.esriSchematicElementType.esriSchematicNodeType)
                            schematicInMemoryFeatureNode = (ISchematicInMemoryFeatureNode)schematicFeature2;
                    }
                                        
                    ISchematicInMemoryFeatureNodeGeometry schematicInMemoryFeatureNodeGeometry = (ISchematicInMemoryFeatureNodeGeometry)schematicInMemoryFeatureNode;
                    dX = schematicInMemoryFeatureNodeGeometry.Position.X;
                    dY = schematicInMemoryFeatureNodeGeometry.Position.Y;
                    schematicFeatureSelected = schematicFeature2;
                    distance = SquareDistance(dX - point.X, dY - point.Y);

                    while (schematicFeature2 != null)
                    {
                        //find the closest featureNode...
                        if (schematicInMemoryFeatureNode != null)
                        {
                            schematicInMemoryFeatureNodeGeometry = (ISchematicInMemoryFeatureNodeGeometry) schematicInMemoryFeatureNode;

                            if (schematicInMemoryFeatureNodeGeometry == null)
                                continue;

                            dX = schematicInMemoryFeatureNodeGeometry.Position.X;
                            dY = schematicInMemoryFeatureNodeGeometry.Position.Y;
                            distancetmp = SquareDistance(dX - point.X, dY - point.Y);
                            
                            if (distancetmp < distance)
                            {
                                distance = distancetmp;
                                schematicFeatureSelected = schematicFeature2;
                            }
                        }

                        schematicFeature2 = schematicFeatures.Next();
                        
                        if (schematicFeature2 != null)
                        {
                            if (schematicFeature2.SchematicElementClass.SchematicElementType == ESRI.ArcGIS.Schematic.esriSchematicElementType.esriSchematicNodeType)
                                schematicInMemoryFeatureNode = (ISchematicInMemoryFeatureNode)schematicFeature2;
                        }
                    }
                    
                    if (schematicFeatureSelected == null)
                        return;

                    if (schematicFeatureSelected.SchematicElementClass.SchematicElementType != esriSchematicElementType.esriSchematicNodeType)
                        return;

                    if (m_schematicFeature1 == null)
                    {
                        m_schematicFeature1 = schematicFeatureSelected;
                        m_dockableDigit.SchematicFeature1(m_schematicFeature1);

                        if (!m_dockableDigit.CheckValidFeature(true))
                        {
                            m_schematicFeature1 = null;
                            m_dockableDigit.SchematicFeature1(m_schematicFeature1);
                            throw new Exception("Invalid starting node for this link type");
                        }

                        //Begin Feedback 
                        m_linkFbk = new NewLineFeedback();
                        m_linkFbk.Display = screenDisplay;

                        //symbol
                        ISimpleLineSymbol sLnSym;
                        IRgbColor rGB = new RgbColor();

                        sLnSym = (ESRI.ArcGIS.Display.ISimpleLineSymbol)m_linkFbk.Symbol;

                        //Make a color
                        rGB.Red = 255;
                        rGB.Green = 0;
                        rGB.Blue = 0;

                        // Setup the symbol with color and style
                        sLnSym.Color = rGB;

                        m_linkFbk.Start(point);
                        //End Feedback

                        //To know if we are in the same diagram.
                        m_schematicLayerForLink = m_schematicLayer;
                    }
                    else
                    {
                        if (m_schematicLayerForLink != m_schematicLayer)
                        {
                            System.Windows.Forms.MessageBox.Show("wrong Target layer");
                            m_schematicLayerForLink = null;
                            EndFeedBack();
                            return;
                        }
                        m_schematicFeature2 = schematicFeatureSelected;
                        m_dockableDigit.SchematicFeature2(m_schematicFeature2);

                        //TestMandatoryField
                        if (m_dockableDigit.ValidateFields() == false)
                        {
                            m_dockableDigit.x(m_x);
                            m_dockableDigit.y(m_y);

                            System.Windows.Forms.MessageBox.Show(m_dockableDigit.ErrorProvider1.GetError(m_dockableDigit.btnOKPanel2) + m_messageFromOK);
                            return;
                        }

                        if (!m_dockableDigit.CheckValidFeature(false))
                        {
                            m_schematicFeature2 = null;
                            m_dockableDigit.SchematicFeature2(m_schematicFeature2);
                            throw new Exception("Invalid End node for this link type");
                        }

                        //CreateLink
                        ISchematicInMemoryFeature schematicInMemoryFeatureLink;

                        schematicOperation = (ESRI.ArcGIS.Schematic.ISchematicOperation) new ESRI.ArcGIS.SchematicControls.SchematicOperation();

                        //digit operation is undo(redo)able we add it in the stack
                        IMxDocument doc  = (IMxDocument)m_app.Document;
                        ESRI.ArcGIS.SystemUI.IOperationStack operationStack; 
                        operationStack = doc.OperationStack;
                        operationStack.Do(schematicOperation);
                        schematicOperation.StartOperation("Digit", m_app, m_schematicLayer, true);

                        //do abort operation
                        abortOperation = true;

                        schematicInMemoryFeatureLink = schematicInMemoryFeatureClass.CreateSchematicInMemoryFeatureLink((ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureNode)m_schematicFeature1, (ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureNode)m_schematicFeature2, "");
                        //schematicInMemoryFeatureLink.UpdateStatus = esriSchematicUpdateStatus.esriSchematicUpdateStatusNew; if we want the node deleted after update
                        schematicInMemoryFeatureLink.UpdateStatus = esriSchematicUpdateStatus.esriSchematicUpdateStatusLocked;

                        abortOperation = false;
                        schematicOperation.StopOperation();

                        ISchematicFeature schematicFeature = schematicInMemoryFeatureLink;
                        m_dockableDigit.FillValue(ref schematicFeature);

                        //End Feedback
                        EndFeedBack();

                        m_schematicLayerForLink = null;

                        if (m_dockableDigit.AutoClear())
                            m_dockableDigit.SelectionChanged();

                    }
                }

                //Refresh the view and viewer windows
                RefreshView();
            }
            catch (System.Exception e)
            {
                if (abortOperation && (schematicOperation != null))
                    schematicOperation.AbortOperation();

                EndFeedBack();
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

            return;
        }

        protected override bool OnDeactivate()
        {
            try
            {
                CurrentDigitTool.CurrentTool.currentDigit = null;

                if (m_dockableWindow != null && !m_DeactivatedFromDock)
                {
                    m_fromDeactivate = true;
                    m_dockableWindow.Dock(ESRI.ArcGIS.Framework.esriDockFlags.esriDockUnPinned);
                        m_dockableWindow.Show(false);
                }
                else
                    m_DeactivatedFromDock = false;
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

            return true;
        }

        protected override void OnActivate()
        {
            try
            {
                this.Cursor = Cursors.Cross;

                CurrentDigitTool.CurrentTool.currentDigit = this;

                SetTargetLayer();

                ESRI.ArcGIS.Framework.IDockableWindowManager dockWinMgr = ArcMap.DockableWindowManager;
                UID u = new UID();
                u.Value = "DigitTool_DockableWindowCS";

                if (dockWinMgr == null)
                    return;

                m_dockableWindow = dockWinMgr.GetDockableWindow(u);

                if (m_dockableDigit == null)
                    m_dockableDigit = CurrentDigitTool.CurrentTool.digitDockableWindow;

                if (m_dockableDigit != null)
                    m_dockableDigit.Init(m_schematicLayer);

                m_dockableWindow.Show(true);

                CurrentDigitTool.CurrentTool.currentDockableWindow = m_dockableWindow;
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        private void RefreshView()
        {
            IMxDocument mxDocument2 = (IMxDocument)m_app.Document;

            if (mxDocument2 == null)
                return;

            IMap map = mxDocument2.FocusMap;
            IActiveView activeView = (IActiveView)map;

            if (activeView != null)
                activeView.Refresh();

            //refresh viewer window
            IApplicationWindows applicationWindows = m_app as IApplicationWindows;

            ISet mySet = applicationWindows.DataWindows;
            if (mySet != null)
            {
                mySet.Reset();
                IMapInsetWindow dataWindow = (IMapInsetWindow)mySet.Next();
                while (dataWindow != null)
                {
                    dataWindow.Refresh();
                    dataWindow = (IMapInsetWindow)mySet.Next();
                }
            }
        }

        public void EndFeedBack()
        {
            m_schematicFeature1 = null;
            m_schematicFeature2 = null;
            
            if (m_dockableDigit != null)
            {
                m_dockableDigit.SchematicFeature1(m_schematicFeature1);
                m_dockableDigit.SchematicFeature2(m_schematicFeature2);
            }

            if (m_linkFbk != null)
            {
                m_linkFbk.Stop();
                m_linkFbk = null;
            }
        }

        public void SchematicFeature1(ISchematicFeature value)
        {
            m_schematicFeature1 = value;
            return;
        }

        public void SchematicFeature2(ISchematicFeature value)
        {
            m_schematicFeature2 = value;
            return;
        }

        public void DeactivatedFromDock(bool value)
        {
            m_DeactivatedFromDock = value;
        }

        
        public void FromDeactivate(bool value)
        {
            m_fromDeactivate = value;
        }

        public bool FromDeactivate()
        {
            return m_fromDeactivate;
        }

        public void MyMouseUp(int x, int y)
        {
            m_x = x;
            m_y = y;

            m_messageFromOK = "";
            OnMouseUp(null);
            m_messageFromOK = "\n" + "Complete missing data and click on OK button";
        }

        private double SquareDistance(double dx, double dy)
        {
            return (dx * dx + dy * dy);
        }


        private void SetTargetLayer()
        {
            try
            {
                if (m_schematicLayer == null)
                {
                    IExtension extention = null;
                    IExtensionManager extensionManager;

                    extensionManager = (IExtensionManager)m_app;
                    extention = extensionManager.FindExtension("SchematicUI.SchematicExtension");
                    
                    if (extention == null)
                        Enabled = false;
                    else
                    {
                        m_schematicExtension = extention;
                        ISchematicTarget target = m_schematicExtension as ISchematicTarget;
                        if (target != null)
                            m_schematicLayer = target.SchematicTarget;
                    }
                }
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }
    }

}
