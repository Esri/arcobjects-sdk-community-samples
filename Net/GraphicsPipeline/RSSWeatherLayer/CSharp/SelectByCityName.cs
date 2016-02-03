using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace RSSWeatherLayer 
{
  /// <summary>
  /// Selects an item by a given city name
  /// </summary>
	[ClassInterface(ClassInterfaceType.None)]
  [Guid("B44F2830-4116-42c2-8A2C-A7097CD7F7BE")]
	[ProgId("RSSWeatherLayer.SelectByCityName")]
  [ComVisible(true)]
	public sealed class SelectByCityName: BaseCommand
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

    //Class members
		private IHookHelper								m_pHookHelper		= null;	
		private RSSWeatherLayerClass		m_weatherLayer	= null;
		private WeatherItemSelectionDlg		m_selectionDlg	= null;

		public SelectByCityName()
		{
			base.m_category = "Weather";
			base.m_caption = "Select Weather item By Cityname";
			base.m_message = "Select By Cityname";
			base.m_toolTip = "Select By Cityname";
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
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			try
			{
				if(m_pHookHelper.FocusMap.LayerCount == 0)
					return;

        //get the weather layer
				IEnumLayer layers = m_pHookHelper.FocusMap.get_Layers(null, false);
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
				
				if(m_weatherLayer != null)
				{
					if(null == m_selectionDlg || m_selectionDlg.IsDisposed)
					{
						m_selectionDlg = new WeatherItemSelectionDlg(m_weatherLayer, m_pHookHelper.ActiveView);
					}
					
					m_selectionDlg.Show();
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
