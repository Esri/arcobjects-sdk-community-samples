using ESRI.ArcGIS.esriSystem;

namespace ZoomFactorExtensionCSharp
{
	public sealed class ZoomExtension : IExtension, IExtensionConfig, IZoomExtension
	{
		private double m_zoomFactor;
		private esriExtensionState m_extensionState; 

		public ZoomExtension()
		{

		}

		public void Shutdown()
		{
			//Not implemented
		}
	
		public void Startup(ref object initializationData)
		{
			//Default zoom factor
			m_zoomFactor = 2;
			//Default extension state is disabled
			m_extensionState = esriExtensionState.esriESDisabled;
		}
	
		public string Name
		{
			get
			{
				return "Zoom Factor Extension";
			}
		}
	
		public string Description
		{
			get
			{
				return "Variable ZoomExtension Sample";
			}
		}
	
		public string ProductName
		{
			get
			{
				return "ZoomExtension Sample";
			}
		}
	
		public esriExtensionState State
		{
			get
			{
				return m_extensionState;
			}
			set
			{
				m_extensionState = value;
			}
		}
	
		public double ZoomFactor
		{
			get
			{
				return m_zoomFactor;
			}
			set
			{
				m_zoomFactor = value;
			}
		}
	}
}
