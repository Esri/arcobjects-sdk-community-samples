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
