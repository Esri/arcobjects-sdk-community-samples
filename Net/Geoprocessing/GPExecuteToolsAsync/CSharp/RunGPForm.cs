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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataManagementTools;

namespace RunGPAsync
{
  public partial class RunGPForm : Form
  {
    private Geoprocessor _gp = null;

    /// <summary>
    /// A Dictionary contains the layers loaded into the application. The Keys are named appropriately.
    /// </summary>
    private Dictionary<string, IFeatureLayer> _layersDict = new Dictionary<string, IFeatureLayer>();

    /// <summary>
    /// A List of FeatureLayer objects each of which represents result layers that are added to the Map.
    /// In this example only one Layer is added to the List.
    /// </summary>
    private List<IFeatureLayer> _resultsList = new List<IFeatureLayer>();

    /// <summary>
    /// A Queue of GPProcess objects each of which represents a geoprocessing tool to be executed asynchronously.
    /// </summary>
    private Queue<IGPProcess> _myGPToolsToExecute = new Queue<IGPProcess>();


    /// <summary>
    /// Initializes a new instance of the RunGPForm class which represents the ArcGIS Engine application.
    /// The constructor is used to perform setup: the ListViewControl is configured, a new Geoprocessor
    /// object is created, the output result directory is specified and event handlers created in order
    /// to listen to geoprocessing events. A helper method called SetupMap is called to create, add and
    /// symbolize the layers used by the application.
    /// </summary>
    public RunGPForm()
    {
      InitializeComponent();

      //Set up ListView control
      listView1.Columns.Add("Event", 200, HorizontalAlignment.Left);  
      listView1.Columns.Add("Message", 1000, HorizontalAlignment.Left);
      //The image list component contains the icons used in the ListView control
      listView1.SmallImageList = imageList1;

      //Create a Geoprocessor object and associated event handlers which will be fired during tool execution
      _gp = new Geoprocessor();
      //All results will be written to the users TEMP folder
      string outputDir = System.Environment.GetEnvironmentVariable("TEMP");
      _gp.SetEnvironmentValue("workspace", outputDir);
      _gp.OverwriteOutput = true;
      _gp.ToolExecuted += new EventHandler<ToolExecutedEventArgs>(_gp_ToolExecuted);
      _gp.ProgressChanged += new EventHandler<ESRI.ArcGIS.Geoprocessor.ProgressChangedEventArgs>(_gp_ProgressChanged);
      _gp.MessagesCreated += new EventHandler<MessagesCreatedEventArgs>(_gp_MessagesCreated);
      _gp.ToolExecuting += new EventHandler<ToolExecutingEventArgs>(_gp_ToolExecuting);

      //Add layers to the map, select a city, zoom in on it
      SetupMap();
    }

    /// <summary>
    /// Handles the click event of the Button and starts asynchronous geoprocessing.
    /// </summary>
    private void btnRunGP_Click(object sender, EventArgs e)
    {
      try
      {
        #region tidy up any previous gp runs

        //Clear the ListView control
        listView1.Items.Clear();

        //Remove any result layers present in the map
        IMapLayers mapLayers = axMapControl1.Map as IMapLayers;
        foreach (IFeatureLayer resultLayer in _resultsList)
        {
          mapLayers.DeleteLayer(resultLayer);
        }

        axTOCControl1.Update();
        
        //Empty the results layer list
        _resultsList.Clear();

        //make sure that my GP tool queue is empty
        _myGPToolsToExecute.Clear();

        #endregion

        //Buffer the selected cities by the specified distance
        ESRI.ArcGIS.AnalysisTools.Buffer bufferTool = new ESRI.ArcGIS.AnalysisTools.Buffer();
        bufferTool.in_features = _layersDict["Cities"];
        bufferTool.buffer_distance_or_field = txtBufferDistance.Text + " Miles";
        bufferTool.out_feature_class = "city_buffer.shp";
       
        //Clip the zip codes layer with the result of the buffer tool
        ESRI.ArcGIS.AnalysisTools.Clip clipTool = new ESRI.ArcGIS.AnalysisTools.Clip();
        clipTool.in_features = _layersDict["ZipCodes"];
        clipTool.clip_features = bufferTool.out_feature_class;
        clipTool.out_feature_class = "city_buffer_clip.shp";

        //To run multiple GP tools asynchronously, all tool inputs must exist before ExecuteAsync is called.
        //The output from the first buffer operation is used as an input to the second clip
        //operation. To deal with this restriction a Queue is created and all tools added to it. The first tool
        //is executed from this method, whereas the second is executed from the ToolExecuted event. This approach
        //is scalable - any additional geoprocessing tools added to this queue will also be executed in turn.
        _myGPToolsToExecute.Enqueue(bufferTool);
        _myGPToolsToExecute.Enqueue(clipTool);
        _gp.ExecuteAsync(_myGPToolsToExecute.Dequeue());
      }
      catch (Exception ex)
      {
        listView1.Items.Add(new ListViewItem(new string[2] { "N/A", ex.Message }, "error")); 
      }
    }

    /// <summary>
    /// Handles the ProgressChanged event.
    /// </summary>
    void _gp_ProgressChanged(object sender, ESRI.ArcGIS.Geoprocessor.ProgressChangedEventArgs e)
    {
      IGeoProcessorResult2 gpResult = (IGeoProcessorResult2)e.GPResult;

      if (e.ProgressChangedType == ProgressChangedType.Message)
      {
        listView1.Items.Add(new ListViewItem(new string[2] {"ProgressChanged", e.Message}, "information"));
      }
    }

    /// <summary>
    /// Handles the ToolExecuting event. All tools start by firing this event.
    /// </summary>
    void _gp_ToolExecuting(object sender, ToolExecutingEventArgs e)
    {   
      IGeoProcessorResult2 gpResult = (IGeoProcessorResult2)e.GPResult;
      listView1.Items.Add(new ListViewItem(new string[2] { "ToolExecuting", gpResult.Process.Tool.Name + " " + gpResult.Status.ToString() }, "information"));
    }

    /// <summary>
    /// Handles the ToolExecuted event. All tools end by firing this event. If the tool executed successfully  
    /// the next tool in the queue is removed and submitted for execution.  If unsuccessful, geoprocessing is terminated,
    /// an error message is written to the ListViewControl and the is queue cleared. After the final tool has executed
    /// the result layer is added to the Map.
    /// </summary>
    void _gp_ToolExecuted(object sender, ToolExecutedEventArgs e)
    {   
      IGeoProcessorResult2 gpResult = (IGeoProcessorResult2)e.GPResult;

      try
      {
        //The first GP tool has completed, if it was successful process the others
        if (gpResult.Status == esriJobStatus.esriJobSucceeded)
        {
          listView1.Items.Add(new ListViewItem(new string[2] { "ToolExecuted", gpResult.Process.Tool.Name }, "success"));

          //Execute next tool in the queue
          if (_myGPToolsToExecute.Count > 0)
          {
            _gp.ExecuteAsync(_myGPToolsToExecute.Dequeue());
          }
          //If last tool has executed add the output layer to the map
          else
          {
            IFeatureClass resultFClass = _gp.Open(gpResult.ReturnValue) as IFeatureClass;
            IFeatureLayer resultLayer = new FeatureLayerClass();
            resultLayer.FeatureClass = resultFClass;
            resultLayer.Name = resultFClass.AliasName;

            //Add the result to the map
            axMapControl1.AddLayer((ILayer)resultLayer, 2);
            axTOCControl1.Update();

            //add the result layer to the List of result layers
            _resultsList.Add(resultLayer);
          }
        }
        //If the GP process failed, do not try to process any more tools in the queue
        else if (gpResult.Status == esriJobStatus.esriJobFailed)
        {
          //The actual GP error message will be output by the MessagesCreated event handler
          listView1.Items.Add(new ListViewItem(new string[2] { "ToolExecuted", gpResult.Process.Tool.Name + " failed, any remaining processes will not be executed." }, "error"));
          //Empty the queue
          _myGPToolsToExecute.Clear();
        }
      }
      catch (Exception ex)
      {
        listView1.Items.Add(new ListViewItem(new string[2] { "ToolExecuted", ex.Message }, "error")); 
      } 
    }

    /// <summary>
    /// Handles the MessagesCreated event.
    /// </summary>
    void _gp_MessagesCreated(object sender, MessagesCreatedEventArgs e)
    {     
      IGPMessages gpMsgs = e.GPMessages;

      if (gpMsgs.Count > 0)
      {
        for (int count = 0; count < gpMsgs.Count; count++)
        {                 
          IGPMessage msg = gpMsgs.GetMessage(count);
          string imageToShow = "information";

          switch (msg.Type)
          {
            case esriGPMessageType.esriGPMessageTypeAbort:
              imageToShow = "warning";
              break;
            case esriGPMessageType.esriGPMessageTypeEmpty:
              imageToShow = "information";
              break;
            case esriGPMessageType.esriGPMessageTypeError:
              imageToShow = "error";
              break;
            case esriGPMessageType.esriGPMessageTypeGDBError:
              imageToShow = "error";
              break;
            case esriGPMessageType.esriGPMessageTypeInformative:
              imageToShow = "information";    
              break;
            case esriGPMessageType.esriGPMessageTypeProcessDefinition:
              imageToShow = "information";
              break;
            case esriGPMessageType.esriGPMessageTypeProcessStart:
              imageToShow = "information";
              break;
            case esriGPMessageType.esriGPMessageTypeProcessStop:
              imageToShow = "information";    
              break;
            case esriGPMessageType.esriGPMessageTypeWarning:
              imageToShow = "warning";    
              break;
            default:
              break;
          }

          listView1.Items.Add(new ListViewItem(new string[2]{"MessagesCreated", msg.Description}, imageToShow));   
        }
      }
    }

    #region Helper methods for setting up map and layers and validating buffer distance input

    /// <summary>
    /// Loads and symbolizes data used by the application.  Selects a city and zooms to it
    /// </summary>
    private void SetupMap()
    {
      try
      {           
        //Relative path to the sample data from EXE location
        string dirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ArcGIS\data\USZipCodeData");
        
        //Create the cities layer
        IFeatureClass cities = _gp.Open(dirPath + @"\ZipCode_Boundaries_US_Major_Cities.shp") as IFeatureClass;
        IFeatureLayer citiesLayer = new FeatureLayerClass();
        citiesLayer.FeatureClass = cities;
        citiesLayer.Name = "Major Cities";
        
        //Create he zip code boundaries layer
        IFeatureClass zipBndrys = _gp.Open(dirPath + @"\US_ZipCodes.shp") as IFeatureClass;
        IFeatureLayer zipBndrysLayer = new FeatureLayerClass();
        zipBndrysLayer.FeatureClass = zipBndrys;
        zipBndrysLayer.Name = "Zip Code boundaries";
        
        //Create the highways layer
        dirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ArcGIS\data\USAMajorHighways");
        IFeatureClass highways = _gp.Open(dirPath + @"\usa_major_highways.shp") as IFeatureClass;
        IFeatureLayer highwaysLayer = new FeatureLayerClass();
        highwaysLayer.FeatureClass = highways;
        highwaysLayer.Name = "Highways";

        //***** Important code *********
        //Add the layers to a dictionary. Layers can then easily be returned by their 'key'
        _layersDict.Add("ZipCodes", zipBndrysLayer);
        _layersDict.Add("Highways", highwaysLayer);
        _layersDict.Add("Cities", citiesLayer);
        
        #region Symbolize and set additional properties for each layer
        //Setup and symbolize the cities layer
        citiesLayer.Selectable = true;
        citiesLayer.ShowTips = true;
        ISimpleMarkerSymbol markerSym = CreateSimpleMarkerSymbol(CreateRGBColor(0, 92, 230), esriSimpleMarkerStyle.esriSMSCircle);
        markerSym.Size = 9;
        ISimpleRenderer simpleRend = new SimpleRendererClass();
        simpleRend.Symbol = (ISymbol)markerSym;
        ((IGeoFeatureLayer)citiesLayer).Renderer = (IFeatureRenderer)simpleRend;

        //Setup and symbolize the zip boundaries layer
        zipBndrysLayer.Selectable = false;
        ISimpleFillSymbol fillSym = CreateSimpleFillSymbol(CreateRGBColor(0, 0, 0), esriSimpleFillStyle.esriSFSHollow, CreateRGBColor(204, 204, 204), esriSimpleLineStyle.esriSLSSolid, 0.5);
        ISimpleRenderer simpleRend2 = new SimpleRendererClass();
        simpleRend2.Symbol = (ISymbol)fillSym;
        ((IGeoFeatureLayer)zipBndrysLayer).Renderer = (IFeatureRenderer)simpleRend2;

        //Setup and symbolize the highways layer
        highwaysLayer.Selectable = false;
        ISimpleLineSymbol lineSym = CreateSimpleLineSymbol(CreateRGBColor(250, 52, 17), 3.4, esriSimpleLineStyle.esriSLSSolid);
        ISimpleRenderer simpleRend3 = new SimpleRendererClass();
        simpleRend3.Symbol = (ISymbol)lineSym;
        ((IGeoFeatureLayer)highwaysLayer).Renderer = (IFeatureRenderer)simpleRend3;
        #endregion
  
        //Add the layers to the Map
        foreach (IFeatureLayer layer in _layersDict.Values)
        {
          axMapControl1.AddLayer((ILayer)layer);
        }

        #region select city and set map extent
        //Select and zoom in on  Los Angeles city
        IQueryFilter qf = new QueryFilterClass();
        qf.WhereClause = "NAME='Los Angeles'";
        IFeatureSelection citiesLayerSelection = (IFeatureSelection)citiesLayer;
        citiesLayerSelection.SelectFeatures(qf, esriSelectionResultEnum.esriSelectionResultNew, true);
        IFeature laFeature = cities.GetFeature(citiesLayerSelection.SelectionSet.IDs.Next());
        IEnvelope env = laFeature.Shape.Envelope;
        env.Expand(0.5, 0.5, false);
        axMapControl1.Extent = env;
        axMapControl1.Refresh();
        axTOCControl1.Update();
        #endregion

        //Enable GP analysis button
        btnRunGP.Enabled = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("There was an error loading the data used by this sample: " + ex.Message);
      }
    }


    #region"Create Simple Fill Symbol"
    // ArcGIS Snippet Title:
    // Create Simple Fill Symbol
    // 
    // Long Description:
    // Create a simple fill symbol by specifying a color, outline color and fill style.
    // 
    // Add the following references to the project:
    // ESRI.ArcGIS.Display
    // ESRI.ArcGIS.System
    // 
    // Intended ArcGIS Products for this snippet:
    // ArcGIS Desktop (Standard, Advanced, Basic)
    // ArcGIS Engine
    // ArcGIS Server
    // 
    // Applicable ArcGIS Product Versions:
    // 9.2
    // 9.3
    // 9.3.1
    // 10.0
    // 
    // Required ArcGIS Extensions:
    // (NONE)
    // 
    // Notes:
    // This snippet is intended to be inserted at the base level of a Class.
    // It is not intended to be nested within an existing Method.
    // 

    ///<summary>Create a simple fill symbol by specifying a color, outline color and fill style.</summary>
    ///  
    ///<param name="fillColor">An IRGBColor interface. The color for the inside of the fill symbol.</param>
    ///<param name="fillStyle">An esriSimpleLineStyle enumeration for the inside fill symbol. Example: esriSFSSolid.</param>
    ///<param name="borderColor">An IRGBColor interface. The color for the outside line border of the fill symbol.</param>
    ///<param name="borderStyle">An esriSimpleLineStyle enumeration for the outside line border. Example: esriSLSSolid.</param>
    ///<param name="borderWidth">A System.Double that is the width of the outside line border in points. Example: 2</param>
    ///   
    ///<returns>An ISimpleFillSymbol interface.</returns>
    ///  
    ///<remarks></remarks>
    public ESRI.ArcGIS.Display.ISimpleFillSymbol CreateSimpleFillSymbol(ESRI.ArcGIS.Display.IRgbColor fillColor, ESRI.ArcGIS.Display.esriSimpleFillStyle fillStyle, ESRI.ArcGIS.Display.IRgbColor borderColor, ESRI.ArcGIS.Display.esriSimpleLineStyle borderStyle, System.Double borderWidth)
    {

      ESRI.ArcGIS.Display.ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbolClass();
      simpleLineSymbol.Width = borderWidth;
      simpleLineSymbol.Color = borderColor;
      simpleLineSymbol.Style = borderStyle;

      ESRI.ArcGIS.Display.ISimpleFillSymbol simpleFillSymbol = new ESRI.ArcGIS.Display.SimpleFillSymbolClass();
      simpleFillSymbol.Outline = simpleLineSymbol;
      simpleFillSymbol.Style = fillStyle;
      simpleFillSymbol.Color = fillColor;

      return simpleFillSymbol;
    }
    #endregion


    #region"Create Simple Line Symbol"
    // ArcGIS Snippet Title:
    // Create Simple Line Symbol
    // 
    // Long Description:
    // Create a simple line symbol by specifying a color, width and line style.
    // 
    // Add the following references to the project:
    // ESRI.ArcGIS.Display
    // ESRI.ArcGIS.System
    // 
    // Intended ArcGIS Products for this snippet:
    // ArcGIS Desktop (Standard, Advanced, Basic)
    // ArcGIS Engine
    // ArcGIS Server
    // 
    // Applicable ArcGIS Product Versions:
    // 9.2
    // 9.3
    // 9.3.1
    // 10.0
    // 
    // Required ArcGIS Extensions:
    // (NONE)
    // 
    // Notes:
    // This snippet is intended to be inserted at the base level of a Class.
    // It is not intended to be nested within an existing Method.
    // 

    ///<summary>Create a simple line symbol by specifying a color, width and line style.</summary>
    ///  
    ///<param name="rgbColor">An IRGBColor interface.</param>
    ///<param name="inWidth">A System.Double that is the width of the line symbol in points. Example: 2</param>
    ///<param name="inStyle">An esriSimpleLineStyle enumeration. Example: esriSLSSolid.</param>
    ///   
    ///<returns>An ISimpleLineSymbol interface.</returns>
    ///  
    ///<remarks></remarks>
    public ESRI.ArcGIS.Display.ISimpleLineSymbol CreateSimpleLineSymbol(ESRI.ArcGIS.Display.IRgbColor rgbColor, System.Double inWidth, ESRI.ArcGIS.Display.esriSimpleLineStyle inStyle)
    {
      if (rgbColor == null)
      {
        return null;
      }

      ESRI.ArcGIS.Display.ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbolClass();
      simpleLineSymbol.Style = inStyle;
      simpleLineSymbol.Color = rgbColor;
      simpleLineSymbol.Width = inWidth;

      return simpleLineSymbol;
    }
    #endregion


    #region"Create Simple Marker Symbol"
    // ArcGIS Snippet Title:
    // Create Simple Marker Symbol
    // 
    // Long Description:
    // Create a simple marker symbol by specifying and input color and marker style.
    // 
    // Add the following references to the project:
    // ESRI.ArcGIS.Display
    // ESRI.ArcGIS.System
    // 
    // Intended ArcGIS Products for this snippet:
    // ArcGIS Desktop (Standard, Advanced, Basic)
    // ArcGIS Engine
    // ArcGIS Server
    // 
    // Applicable ArcGIS Product Versions:
    // 9.2
    // 9.3
    // 9.3.1
    // 10.0
    // 
    // Required ArcGIS Extensions:
    // (NONE)
    // 
    // Notes:
    // This snippet is intended to be inserted at the base level of a Class.
    // It is not intended to be nested within an existing Method.
    // 

    ///<summary>Create a simple marker symbol by specifying and input color and marker style.</summary>
    ///  
    ///<param name="rgbColor">An IRGBColor interface.</param>
    ///<param name="inputStyle">An esriSimpleMarkerStyle enumeration. Example: esriSMSCircle.</param>
    ///   
    ///<returns>An ISimpleMarkerSymbol interface.</returns>
    ///  
    ///<remarks></remarks>
    public ESRI.ArcGIS.Display.ISimpleMarkerSymbol CreateSimpleMarkerSymbol(ESRI.ArcGIS.Display.IRgbColor rgbColor, ESRI.ArcGIS.Display.esriSimpleMarkerStyle inputStyle)
    {
      
      ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
      simpleMarkerSymbol.Color = rgbColor;
      simpleMarkerSymbol.Style = inputStyle;

      return simpleMarkerSymbol;
    }
    #endregion


    #region"Create RGBColor"
    // ArcGIS Snippet Title:
    // Create RGBColor
    // 
    // Long Description:
    // Generate an RgbColor by specifying the amount of Red, Green and Blue.
    // 
    // Add the following references to the project:
    // ESRI.ArcGIS.Display
    // ESRI.ArcGIS.System
    // 
    // Intended ArcGIS Products for this snippet:
    // ArcGIS Desktop (Standard, Advanced, Basic)
    // ArcGIS Engine
    // ArcGIS Server
    // 
    // Applicable ArcGIS Product Versions:
    // 9.2
    // 9.3
    // 9.3.1
    // 10.0
    // 
    // Required ArcGIS Extensions:
    // (NONE)
    // 
    // Notes:
    // This snippet is intended to be inserted at the base level of a Class.
    // It is not intended to be nested within an existing Method.
    // 

    ///<summary>Generate an RgbColor by specifying the amount of Red, Green and Blue.</summary>
    /// 
    ///<param name="myRed">A byte (0 to 255) used to represent the Red color. Example: 0</param>
    ///<param name="myGreen">A byte (0 to 255) used to represent the Green color. Example: 255</param>
    ///<param name="myBlue">A byte (0 to 255) used to represent the Blue color. Example: 123</param>
    ///  
    ///<returns>An IRgbColor interface</returns>
    ///  
    ///<remarks></remarks>
    public ESRI.ArcGIS.Display.IRgbColor CreateRGBColor(System.Byte myRed, System.Byte myGreen, System.Byte myBlue)
    {
      ESRI.ArcGIS.Display.IRgbColor rgbColor = new ESRI.ArcGIS.Display.RgbColorClass();
      rgbColor.Red = myRed;
      rgbColor.Green = myGreen;
      rgbColor.Blue = myBlue;
      rgbColor.UseWindowsDithering = true;
      return rgbColor;
    }
    #endregion

    private void txtBufferDistance_TextChanged(object sender, EventArgs e)
    {

      string txtToCheck = txtBufferDistance.Text;

      if (((IsDecimal(txtToCheck)) | (IsInteger(txtToCheck))) && (txtToCheck != "0"))
      {
        btnRunGP.Enabled = true;
      }
      else
      {
        btnRunGP.Enabled = false;
      }
    }

    private bool IsDecimal(string theValue)
    {
      try
      {
        Convert.ToDouble(theValue);
        return true;
      }
      catch
      {
        return false;
      }
    } //IsDecimal

    private bool IsInteger(string theValue)
    {
      try
      {
        Convert.ToInt32(theValue);
        return true;
      }
      catch
      {
        return false;
      }
    } //IsInteger


    #endregion

  }
}