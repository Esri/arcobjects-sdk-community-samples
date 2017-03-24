/*

   Copyright 2016 Esri

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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;

namespace Controls
{
    public partial class MapViewer : Form
    {
        //The popup menu
        private IToolbarMenu m_ToolbarMenu; 
        //The envelope drawn on the MapControl
        private IEnvelope m_Envelope;
        //The symbol used to draw the envelope on the MapControl
        private Object m_FillSymbol;
        //The PageLayoutControl's focus map events 
        private ITransformEvents_Event m_transformEvents;
        private ITransformEvents_VisibleBoundsUpdatedEventHandler visBoundsUpdatedE;
        //The CustomizeDialog used by the ToolbarControl
        private ICustomizeDialog m_CustomizeDialog; 
        //The CustomizeDialog start event
        private ICustomizeDialogEvents_OnStartDialogEventHandler startDialogE;
        //The CustomizeDialog close event 
        private ICustomizeDialogEvents_OnCloseDialogEventHandler closeDialogE;

        public MapViewer()
        {
            InitializeComponent();
        }

        private void MapViewer_Load(object sender, EventArgs e)
        {
            //Create the customize dialog for the ToolbarControl
            CreateCustomizeDialog();

            //Create symbol used on the MapControl
            CreateOverviewSymbol();

            //Set label editing to manual
            axTOCControl1.LabelEdit = esriTOCControlEdit.esriTOCControlManual;

            //Get file name used to persist the ToolbarControl  
            String filePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("Controls.exe", "") + @"\PersistedItems.txt";
            if (System.IO.File.Exists(filePath))
                LoadToolbarControlItems(filePath);
            else
            {
                //Add generic commands
                axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsAddDataCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                //Add page layout navigation commands
                axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsPagePanTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                //Add map navigation commands
                axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsMapPanTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                //Add map inquiry commands
                axToolbarControl1.AddItem("esriControls.ControlsMapIdentifyTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsMapFindCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsMapMeasureTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                //Add custom AddDateTool
                axToolbarControl1.AddItem("Commands.AddDateTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);

                //Create a new ToolbarPalette
                IToolbarPalette toolbarPalette = new ToolbarPaletteClass();
                //Add commands and tools to the ToolbarPalette
                toolbarPalette.AddItem("esriControls.ControlsNewMarkerTool", -1, -1);
                toolbarPalette.AddItem("esriControls.ControlsNewLineTool", -1, -1);
                toolbarPalette.AddItem("esriControls.ControlsNewCircleTool", -1, -1);
                toolbarPalette.AddItem("esriControls.ControlsNewEllipseTool", -1, -1);
                toolbarPalette.AddItem("esriControls.ControlsNewRectangleTool", -1, -1);
                toolbarPalette.AddItem("esriControls.ControlsNewPolygonTool", -1, -1);
                //Add the ToolbarPalette to the ToolbarControl
                axToolbarControl1.AddItem(toolbarPalette, 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            }

            //Create a new ToolbarMenu
            m_ToolbarMenu = new ToolbarMenuClass();
            //Share the ToolbarControl's command pool
            m_ToolbarMenu.CommandPool = axToolbarControl1.CommandPool;
            //Set the hook to the PageLayoutControl
            m_ToolbarMenu.SetHook(axPageLayoutControl1);
            //Add commands to the ToolbarMenu
            m_ToolbarMenu.AddItem("esriControls.ControlsPageZoomInFixedCommand", -1, -1,false,esriCommandStyles.esriCommandStyleIconAndText);
            m_ToolbarMenu.AddItem("esriControls.ControlsPageZoomOutFixedCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_ToolbarMenu.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_ToolbarMenu.AddItem("esriControls.ControlsPageZoomPageToLastExtentBackCommand", -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            m_ToolbarMenu.AddItem("esriControls.ControlsPageZoomPageToLastExtentForwardCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);

            //Set buddy controls
            axTOCControl1.SetBuddyControl(axPageLayoutControl1);
            axToolbarControl1.SetBuddyControl(axPageLayoutControl1);

            //Load a pre-authored map document into the PageLayoutControl using relative paths
            string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ArcGIS\data\GulfOfStLawrence\Gulf_of_St._Lawrence.mxd");
            if (axPageLayoutControl1.CheckMxFile(fileName))
                axPageLayoutControl1.LoadMxFile(fileName, "");
        }

        private void axPageLayoutControl1_OnPageLayoutReplaced(object sender, IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
        {
            //Get the IActiveView of the focus map in the PageLayoutControl
            IActiveView activeView = axPageLayoutControl1.ActiveView.FocusMap as IActiveView;
            //Trap the ITranformEvents of the PageLayoutCntrol's focus map 
            visBoundsUpdatedE = new ITransformEvents_VisibleBoundsUpdatedEventHandler(OnVisibleBoundsUpdated);
            IDisplayTransformation displayTransformation = activeView.ScreenDisplay.DisplayTransformation;
            //Start listening to the transform events interface
            m_transformEvents = (ITransformEvents_Event) displayTransformation;
            //Start listening to the VisibleBoundsUpdated method on ITransformEvents interface
            m_transformEvents.VisibleBoundsUpdated += visBoundsUpdatedE;
            //Get the extent of the focus map
            m_Envelope = activeView.Extent;

            //Load the same pre-authored map document into the MapControl
            axMapControl1.LoadMxFile(axPageLayoutControl1.DocumentFilename, null, null);
            //Set the extent of the MapControl to the full extent of the data
            axMapControl1.Extent = axMapControl1.FullExtent;
        }

        private void MapViewer_ResizeBegin(object sender, EventArgs e)
        {
            //Suppress data redraw and draw bitmap instead
            axMapControl1.SuppressResizeDrawing(true, 0);
            axPageLayoutControl1.SuppressResizeDrawing(true, 0);
        }

        private void MapViewer_ResizeEnd(object sender, EventArgs e)
        {
            //Stop bitmap draw and draw data
            axMapControl1.SuppressResizeDrawing(false, 0);
            axPageLayoutControl1.SuppressResizeDrawing(false, 0);
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            //Popup the ToolbarMenu
            if (e.button == 2)
                m_ToolbarMenu.PopupMenu(e.x, e.y, axPageLayoutControl1.hWnd);
        }

        private void axTOCControl1_OnEndLabelEdit(object sender, ITOCControlEvents_OnEndLabelEditEvent e)
        {
            //If the new label is an empty string then prevent the edit
            if (e.newLabel.Trim() == "") e.canEdit = false;
        }

        private void CreateOverviewSymbol()
        {
            //Get the IRGBColor interface
            IRgbColor color = new RgbColorClass();
            //Set the color properties
            color.RGB = 255;
            //Get the ILine symbol interface
            ILineSymbol outline = new SimpleLineSymbolClass();
            //Set the line symbol properties
            outline.Width = 1.5;
            outline.Color = color;
            //Get the IFillSymbol interface
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            //Set the fill symbol properties
            simpleFillSymbol.Outline = outline;
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;
            m_FillSymbol = simpleFillSymbol;
        }

        private void OnVisibleBoundsUpdated(IDisplayTransformation sender, bool sizeChanged)
        {
            //Set the extent to the new visible extent
            m_Envelope = sender.VisibleBounds;
            //Refresh the MapControl's foreground phase
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
        }

        private void axMapControl1_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
        {
            if (m_Envelope == null) return;

            //If the foreground phase has drawn      
            esriViewDrawPhase viewDrawPhase = (esriViewDrawPhase)e.viewDrawPhase;
            if (viewDrawPhase == esriViewDrawPhase.esriViewForeground)
            {
                IGeometry geometry = m_Envelope;
                axMapControl1.DrawShape(geometry, ref m_FillSymbol);
            }
        }

        private void CreateCustomizeDialog()
        {
            //Create new customize dialog 
            m_CustomizeDialog = new CustomizeDialogClass();
            //Set the title
            m_CustomizeDialog.DialogTitle = "Customize ToolbarControl Items";
            //Show the 'Add from File' button
            m_CustomizeDialog.ShowAddFromFile = true;
            //Set the ToolbarControl that new items will be added to
            m_CustomizeDialog.SetDoubleClickDestination(axToolbarControl1);

            //Set the customize dialog events 
            startDialogE = new ICustomizeDialogEvents_OnStartDialogEventHandler(OnStartDialog);
            ((ICustomizeDialogEvents_Event)m_CustomizeDialog).OnStartDialog += startDialogE;
            closeDialogE = new ICustomizeDialogEvents_OnCloseDialogEventHandler(OnCloseDialog);
            ((ICustomizeDialogEvents_Event)m_CustomizeDialog).OnCloseDialog += closeDialogE;
        }

        private void chkCustomize_CheckedChanged(object sender, EventArgs e)
        {
            //Show or hide the customize dialog
            if (chkCustomize.Checked == false)
                m_CustomizeDialog.CloseDialog();
            else
                m_CustomizeDialog.StartDialog(axToolbarControl1.hWnd);
        }

        private void OnStartDialog()
        {
            axToolbarControl1.Customize = true;
        }

        private void OnCloseDialog()
        {
            axToolbarControl1.Customize = false;
            chkCustomize.Checked = false;
        }

        private void SaveToolbarControlItems(string filePath)
        {
            //Create a MemoryBlobStream
            IBlobStream blobStream = new MemoryBlobStreamClass(); 
            //Get the IStream interface
            IStream stream = blobStream; 

            //Save the ToolbarControl into the stream
            axToolbarControl1.SaveItems(stream);
            //Save the stream to a file
            blobStream.SaveToFile(filePath);
        }

        private void LoadToolbarControlItems(string filePath)
        {   
            //Create a MemoryBlobStream
            IBlobStream blobStream = new MemoryBlobStreamClass();
            //Get the IStream interface
            IStream stream = blobStream;

            //Load the stream from the file
            blobStream.LoadFromFile(filePath);
            //Load the stream into the ToolbarControl
            axToolbarControl1.LoadItems(stream);
        }

        private void MapViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Get file name to persist the ToolbarControl 
            String filePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("Controls.exe", "") + @"\PersistedItems.txt";
            //Persist ToolbarControl contents
            SaveToolbarControlItems(filePath);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit if there is no system default printer
            if (axPageLayoutControl1.Printer == null)
            {
                MessageBox.Show("Unable to print!", "No default printer");
                return;
            }

            //Set printer papers orientation to that of the Page
            axPageLayoutControl1.Printer.Paper.Orientation = axPageLayoutControl1.Page.Orientation;
            //Scale to the page
            axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale;
            //Send the pagelayout to the printer
            axPageLayoutControl1.PrintPageLayout();
        }

    }
}