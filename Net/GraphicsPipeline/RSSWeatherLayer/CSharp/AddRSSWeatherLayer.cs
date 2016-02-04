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
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace RSSWeatherLayer
{
  /// <summary>
  /// Add a new weather item given a zipCode.
  /// </summary>
  /// <remarks>Should the weather item exist, it will be updated</remarks>
	[ClassInterface(ClassInterfaceType.None)]
  [Guid("C9260965-D3AA-4c28-B55A-023C41F1CA39")]
	[ProgId("RSSWeatherLayer.AddRSSWeatherLayer")]
  [ComVisible(true)]
	public sealed class AddRSSWeatherLayer: BaseCommand
	{
		#region COM Registration Function(s)
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType);

			//
			// TODO: Add any COM registration code here
			//
		}

		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryUnregistration(registerType);

			//
			// TODO: Add any COM unregistration code here
			//
		}

		#region ArcGIS Component Category Registrar generated code
		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ControlsCommands.Register(regKey);
      MxCommands.Register(regKey);

		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ControlsCommands.Unregister(regKey);
      MxCommands.Unregister(regKey);

		}

		#endregion
		#endregion

    private sealed class InvokeHelper : Control
    {
      //delegate used to pass the invoked method to the main thread
      public delegate void RefreshHelper(WKSEnvelope invalidateExtent);
      public delegate void RefreshWeatherItemHelper(WeatherItemEventArgs weatherItemInfo);
      
      private IActiveView m_activeView = null;
      private RSSWeatherLayerClass m_weatherLayer = null;
      private IEnvelope m_invalidateExtent = null;
      private IPoint m_point;

      public InvokeHelper (IActiveView activeView, RSSWeatherLayerClass weatherLayer)
      {
        m_activeView    = activeView;
        m_weatherLayer  = weatherLayer;

        CreateHandle();
        CreateControl();

        m_invalidateExtent = new EnvelopeClass();
        m_invalidateExtent.SpatialReference = activeView.ScreenDisplay.DisplayTransformation.SpatialReference;

        m_point = new PointClass();
        m_point.SpatialReference = activeView.ScreenDisplay.DisplayTransformation.SpatialReference;
      }

      public void RefreshWeatherItem(WeatherItemEventArgs weatherItemInfo)
      {
        try
        {
          // Invoke the RefreshInternal through its delegate
          if (!this.IsDisposed && this.IsHandleCreated)
            Invoke(new RefreshWeatherItemHelper(RefreshWeatherItemInvoked), new object[] { weatherItemInfo });
        }
        catch (Exception ex)
        {
          System.Diagnostics.Trace.WriteLine(ex.Message);
        }
      }

      public void Refresh (WKSEnvelope invalidateExtent)
      {
        try
        {
          // Invoke the RefreshInternal through its delegate
          if (!this.IsDisposed && this.IsHandleCreated)
            Invoke(new RefreshHelper(RefreshInvoked), new object[] { invalidateExtent });
        }
        catch (Exception ex)
        {
          System.Diagnostics.Trace.WriteLine(ex.Message);
        }
      }

      private void RefreshWeatherItemInvoked(WeatherItemEventArgs weatherItemInfo)
      {
        ITransformation transform = m_activeView.ScreenDisplay.DisplayTransformation as ITransformation;
        if (transform == null)
          return;

        double[] iconDimensions = new double[2];
        iconDimensions[0] = (double)weatherItemInfo.IconWidth; 
        iconDimensions[1] = (double)weatherItemInfo.IconHeight;

        double[] iconDimensionsMap = new double[2];

        transform.TransformMeasuresFF(esriTransformDirection.esriTransformReverse, 1, ref iconDimensionsMap[0], ref iconDimensions[0]);

        m_invalidateExtent.PutCoords(0, 0, iconDimensionsMap[0], iconDimensionsMap[0]);
        m_point.PutCoords(weatherItemInfo.mapX, weatherItemInfo.mapY);
        m_invalidateExtent.CenterAt(m_point);

        m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_weatherLayer, m_invalidateExtent);
        m_activeView.ScreenDisplay.UpdateWindow();
      }
      
      private void RefreshInvoked(WKSEnvelope invalidateExtent)
      {
        m_invalidateExtent.PutWKSCoords(ref invalidateExtent);
        
        if (!m_invalidateExtent.IsEmpty)
          m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_weatherLayer, m_invalidateExtent);
        else
          m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_weatherLayer, null);        
        
        m_activeView.ScreenDisplay.UpdateWindow();
      }
    }

    private InvokeHelper              m_invokeHelper    = null;

    //class members
		private IHookHelper               m_pHookHelper     = null;
		private RSSWeatherLayerClass		  m_weatherLayer		= null;
		private bool											m_bOnce						= true;
    private bool                      m_bConnected      = false;

		public AddRSSWeatherLayer()
		{
			base.m_category = "Weather";
			base.m_caption = "Add RSS Weather Layer";
			base.m_message = "Add RSS Weather Layer";
			base.m_toolTip = "Add RSS Weather Layer";
			base.m_name = base.m_category + "_" + base.m_caption;

			try
			{
				string bitmapResourceName = GetType().Name + ".bmp";
				base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
			}
		}

		#region Overriden Class Methods

		/// <summary>
		/// Occurs when this command is created
		/// </summary>
		/// <param name="hook">Instance of the application</param>
		public override void OnCreate(object hook)
		{
      //Instantiate the hook helper
      if (m_pHookHelper == null)
				m_pHookHelper = new HookHelperClass ();

      //set the hook
			m_pHookHelper.Hook = hook;

      //set verbose events in order to listen to ItemDeleted event
      ((IViewManager)m_pHookHelper.FocusMap).VerboseEvents = true;
      
      //hook the ItemDeleted event in order to track removal of the layer from the TOC
      ((IActiveViewEvents_Event)m_pHookHelper.FocusMap).ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(OnItemDeleted);
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			try
			{				
				if(!m_bConnected)
				{
          //check first that the layer was added to the globe
					if(m_bOnce == true)
					{	
			      //instantiate the layer
						m_weatherLayer = new RSSWeatherLayerClass();
            m_invokeHelper = new InvokeHelper(m_pHookHelper.ActiveView, m_weatherLayer);

						m_bOnce = false;
					}
          //test whether the layer has been added to the map
					bool bLayerHasBeenAdded = false;
					ILayer layer = null;

					if(m_pHookHelper.FocusMap.LayerCount != 0)
					{
						IEnumLayer layers = m_pHookHelper.FocusMap.get_Layers(null, false);
						layers.Reset();
						layer = layers.Next();
						while(layer != null)
						{
							if(layer is RSSWeatherLayerClass)
							{
								bLayerHasBeenAdded = true;
								break;
							}
							layer = layers.Next();
						}
					}

          //add the layer to the map
					if(!bLayerHasBeenAdded)
					{
						layer = (ILayer)m_weatherLayer;
						layer.Name = "RSS Weather Layer";
						try
						{
							m_pHookHelper.FocusMap.AddLayer(layer);
							//wires layer's events
							m_weatherLayer.OnWeatherItemAdded += new WeatherItemAdded(OnWeatherItemAdded);
							m_weatherLayer.OnWeatherItemsUpdated += new WeatherItemsUpdated(OnWeatherItemsUpdated);
						}
						catch(Exception ex)
						{
							System.Diagnostics.Trace.WriteLine("Failed" + ex.Message);
						}
					}

					//connect to the service
          m_weatherLayer.Connect();
				}
				else
				{
					//disconnect from the service
					m_weatherLayer.Disconnect();

          //un-wires layer's events
          m_weatherLayer.OnWeatherItemAdded -= new WeatherItemAdded(OnWeatherItemAdded);
          m_weatherLayer.OnWeatherItemsUpdated -= new WeatherItemsUpdated(OnWeatherItemsUpdated);

					//delete the layer
					m_pHookHelper.FocusMap.DeleteLayer(m_weatherLayer);

					//dispose the layer
					m_weatherLayer = null;
					m_bOnce = true;
				}


				m_bConnected = !m_bConnected;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}
		}

		/// <summary>
    /// Indicates whether or not this command is checked. 
		/// </summary>
    public override bool Checked
		{
			get
			{
                m_bConnected = false;
				return m_bConnected;
			}
		}

		#endregion

    /// <summary>
    /// weather layer ItemAdded event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <remarks>gets fired when an item is added to the table</remarks>
		private void OnWeatherItemAdded(object sender, WeatherItemEventArgs args)
		{
			// use the invoke helper since this event gets fired on a different thread
      m_invokeHelper.RefreshWeatherItem(args);
		}

    /// <summary>
    /// Weather layer ItemsUpdated event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <remarks>gets fired when the update thread finish updating all the items in the table</remarks>
		private void OnWeatherItemsUpdated(object sender, EventArgs args)
		{
			//refresh the display
      WKSEnvelope emptyEnv;
      emptyEnv.XMax = emptyEnv.XMin = emptyEnv.YMax = emptyEnv.YMin = 0;
      m_invokeHelper.Refresh(emptyEnv);
		}

    /// <summary>
    /// Listen to ActiveViewEvents.ItemDeleted in order to track whether the layer has been 
    /// removed from the TOC
    /// </summary>
    /// <param name="Item"></param>
    void OnItemDeleted(object Item)
    {
      //test that the deleted layer is RSSWeatherLayerClass
      if (Item is RSSWeatherLayerClass)
      {
        if (m_bConnected && null != m_weatherLayer)
        {
          m_bConnected = false;

          //disconnect from the service
          m_weatherLayer.Disconnect();

          //dispose the layer
          m_weatherLayer = null;
          m_bOnce = true;
        }
      }
    }
	}
}
