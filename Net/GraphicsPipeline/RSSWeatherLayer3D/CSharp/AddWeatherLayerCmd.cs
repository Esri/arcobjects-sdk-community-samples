using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.GlobeCore;

namespace RSSWeatherLayer3D
{
	/// <summary>
	/// Connects and disconnects from the RSS weather service.
	/// </summary>
	[ClassInterface(ClassInterfaceType.None)]
  [Guid("4484FB2E-9E79-4642-8B14-32DA6AE2EAF3")]
	[ProgId("RSSWeatherLayer3D.AddWeatherLayerCmd")]
  [ComVisible(true)]
	public sealed class AddWeatherLayerCmd: BaseCommand
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
			GMxCommands.Register(regKey);
      ControlsCommands.Register(regKey);
		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			GMxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);
		}

		#endregion
		#endregion

    //Class members
    private IGlobeHookHelper          m_globeHookHelper = null;
    private RSSWeatherLayer3DClass	m_weatherLayer		= null;
		private IScene							      m_scene						= null;	
		//private bool								      m_bOnce						= true;
		private bool								      m_bConnected      = false;

    /// <summary>
    /// CTor
    /// </summary>
		public AddWeatherLayerCmd()
		{
      base.m_category = "Weather3D";
			base.m_caption = "Load Layer";
			base.m_message = "Connect to the RSS weather service";
			base.m_toolTip = "Connect to weather service";
			base.m_name = base.m_category + "_" + base.m_caption;

			try
			{
        base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "Bitmaps.AddWeatherLayerCmd.bmp"));
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
      if (null == m_globeHookHelper)
        m_globeHookHelper = new GlobeHookHelperClass();

      //set the hook
      m_globeHookHelper.Hook = hook;

			m_bConnected = false;
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			//test whether connected to the service
      if (!m_bConnected) //in case not connected.
			{
        IGlobe globe = m_globeHookHelper.Globe;
        m_scene = globe as IScene;

				//create the instance of the layer
        if (null == m_weatherLayer)
				{				
					m_weatherLayer = new RSSWeatherLayer3DClass();
				}

        //test whether the layer has been added to the globe (allow for only one instance of the layer)
				bool bLayerHasBeenAdded = false;
				IEnumLayer layers = m_scene.get_Layers(null, false);
				layers.Reset();
				ILayer layer = layers.Next();
				while(layer != null)
				{
					if(layer is RSSWeatherLayer3DClass)
					{
						bLayerHasBeenAdded = true;
						break;
					}
					layer = layers.Next();
				}

				//in case that the layer hasn't been added
        if(!bLayerHasBeenAdded)
				{
					layer = (ILayer)m_weatherLayer;
					layer.Name = "RSS Weather Layer";
					try
					{
            //add the layer to the globe
						globe.AddLayerType(layer, esriGlobeLayerType.esriGlobeLayerTypeDraped, false);
					}
					catch(Exception ex)
					{
						System.Diagnostics.Trace.WriteLine("Failed" + ex.Message);
					}
				}

				//connect to the RSS weather service
				m_weatherLayer.Connect();
			}
			else
			{
				//disconnect from the service
				m_weatherLayer.Disconnect();
				
				//delete the layer from the globe
				m_scene.DeleteLayer(m_weatherLayer);

				//dispose the layer
				m_weatherLayer.Dispose();
				m_weatherLayer = null;
			}

      //set the connectionflag
			m_bConnected = !m_bConnected;
    }

    /// <summary>
    /// set the state of the button (acts like a check button)
    /// </summary>
		public override bool Checked
		{
			get
			{
				return m_bConnected;
			}
		}
		#endregion
	}
}
