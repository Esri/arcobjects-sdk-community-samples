using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;


namespace RSSWeatherLayer
{
	/// <summary>
	/// Add a new weather item given a zipCode.
	/// </summary>
  /// <remarks>Should the weather item exist, it will be updated</remarks>
	[ClassInterface(ClassInterfaceType.None)]
  [Guid("D19CA1E0-FC77-4d2a-8FAA-EC74683FA991")]
  [ProgId("RSSWeatherLayer.AddWeatherItemCmd")]
  [ComVisible(true)]
	public sealed class AddWeatherItemCmd: BaseCommand
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
      MxCommands.Register(regKey);
      ControlsCommands.Register(regKey);
		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);
		}

		#endregion
		#endregion

    //class members
    private IHookHelper               m_hookHelper = null;
		private RSSWeatherLayerClass	    m_weatherLayer		= null;

    /// <summary>
    /// CTor
    /// </summary>
		public AddWeatherItemCmd()
		{
      base.m_category = "Weather";
			base.m_caption = "Add Weather item by zipcode";
			base.m_message = "Add weather item by zipcode";
			base.m_toolTip = "Add by zipcode";
			base.m_name = base.m_category + "_" + base.m_caption;

			try
			{
        base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "AddWeatherItemCmd.bmp"));
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
      if (m_hookHelper == null)
        m_hookHelper = new HookHelperClass();

      //set the hook
      m_hookHelper.Hook = hook;
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			try
			{
        if (0 == m_hookHelper.FocusMap.LayerCount)
          return;

				//get the weather layer
        IEnumLayer layers = m_hookHelper.FocusMap.get_Layers(null, false);
				layers.Reset();
				ILayer layer = layers.Next();
				while(layer != null)
				{
					if(layer is RSSWeatherLayerClass)
					{
						m_weatherLayer = (RSSWeatherLayerClass)layer;
						break;
					}
					layer = layers.Next();
				}

        //in case that the layer exists
				if(null != m_weatherLayer)
				{
          //launch the zipCode input dialog
					ZipCodeDlg dlg = new ZipCodeDlg();
					if(DialogResult.OK == dlg.ShowDialog())
					{
						long zipCode = dlg.ZipCode;
						if(0 != zipCode)
						{
							//add the weather item to the layer
							m_weatherLayer.AddItem(zipCode);		

              //if the user checked the 'ZoomTo' checkbox, zoom to the item
							if(dlg.ZoomToItem)
							{
								m_weatherLayer.ZoomTo(zipCode);
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}
		}

		#endregion
	}
}
