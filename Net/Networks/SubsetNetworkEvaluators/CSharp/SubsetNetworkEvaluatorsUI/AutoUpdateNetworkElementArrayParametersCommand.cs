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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.NetworkAnalystUI;
using SubsetNetworkEvaluators;

namespace SubsetNetworkEvaluatorsUI
{
	/// <summary>
	/// This command toggles on and off the event based behavior to update the relevant subset parameter arrays
	/// automatically by listening to selection change events and graphic element change events.  When the command
	/// is toggled off, the subset parameter arrays are cleared out and the results may not match the current selection
	/// or graphic element geometries in this case.
	/// </summary>
	[Guid("f213e01f-3a45-44c7-a350-397a794e9084")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("SubsetNetworkEvaluatorsUI.AutoUpdateNetworkElementArrayParametersCommand")]
	public sealed class AutoUpdateNetworkElementArrayParametersCommand : BaseCommand
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

		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommands.Unregister(regKey);

		}

		#endregion
		#endregion

		private IApplication m_application = null;
		private INetworkAnalystExtension m_nax = null;

		private INAWindow m_naWindowEventSource = null;
		private IMap m_mapEventSource = null;
		private IGraphicsContainer m_graphicsEventSource = null;

		private ESRI.ArcGIS.NetworkAnalystUI.INAWindowEvents_OnActiveAnalysisChangedEventHandler m_ActiveAnalysisChanged;
		private ESRI.ArcGIS.Carto.IActiveViewEvents_SelectionChangedEventHandler m_ActiveViewEventsSelectionChanged;
		private ESRI.ArcGIS.Carto.IGraphicsContainerEvents_AllElementsDeletedEventHandler m_AllGraphicsDeleted;
		private ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementAddedEventHandler m_GraphicAdded;
		private ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementDeletedEventHandler m_GraphicDeleted;
		private ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementsAddedEventHandler m_GraphicsAdded;
		private ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementUpdatedEventHandler m_GraphicUpdated;

		public AutoUpdateNetworkElementArrayParametersCommand()
		{
			base.m_category = "Network Analyst Samples"; //localizable text
			base.m_caption = "Auto Update Network Element Array Parameters";  //localizable text
			base.m_message = "Auto Update Network Element Array Parameters";  //localizable text 
			base.m_toolTip = "Auto Update Network Element Array Parameters";  //localizable text 
			base.m_name = "NASamples_AutoUpdateNetworkElementArrayParameters";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

			try
			{
				//
				// TODO: change bitmap name if necessary
				//
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
			if (hook == null)
				return;

			m_application = hook as IApplication;

			m_nax = null;
			m_naWindowEventSource = null;
			m_mapEventSource = null;
			m_graphicsEventSource = null;

			m_nax = SubsetHelperUI.GetNAXConfiguration(m_application) as INetworkAnalystExtension;
		}

		public override bool Enabled
		{
			get
			{
				bool naxEnabled = false;
				IExtensionConfig naxConfig = m_nax as IExtensionConfig;
				if (naxConfig != null)
					naxEnabled = naxConfig.State == esriExtensionState.esriESEnabled;

				INALayer naLayer = null;
				INetworkDataset nds = null;

				if (naxEnabled)
				{
					INAWindow naWindow = m_nax.NAWindow;
					naLayer = naWindow.ActiveAnalysis;

					INAContext naContext = null;
					if (naLayer != null)
						naContext = naLayer.Context;

					if (naContext != null)
						nds = naContext.NetworkDataset;
				}

				bool enable = naxEnabled && (naLayer != null) && (nds != null);
				if (!enable && m_naWindowEventSource != null)
					UnWireEvents();

				base.m_enabled = enable;
				return base.Enabled;
			}
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			if (m_naWindowEventSource != null)
				UnWireEvents();
			else
				WireEvents();
		}

		public override bool Checked
		{
			get
			{
				return (m_naWindowEventSource != null);
			}
		}

		private void WireEvents()
		{
			try
			{
				if (m_naWindowEventSource != null)
					UnWireEvents();

				m_naWindowEventSource = ((m_nax != null) ? m_nax.NAWindow : null) as INAWindow;
				if (m_naWindowEventSource == null)
					return;

				//Create an instance of the delegate, add it to OnActiveAnalysisChanged event
				m_ActiveAnalysisChanged = new ESRI.ArcGIS.NetworkAnalystUI.INAWindowEvents_OnActiveAnalysisChangedEventHandler(OnActiveAnalysisChanged);
				((ESRI.ArcGIS.NetworkAnalystUI.INAWindowEvents_Event)(m_naWindowEventSource)).OnActiveAnalysisChanged += m_ActiveAnalysisChanged;

				WireSelectionEvent();
				WireGraphicsEvents();
			}
			catch (Exception ex)
			{
				string msg = SubsetHelperUI.GetFullExceptionMessage(ex);
				MessageBox.Show(msg, "Wire Events");
			}
		}

		private void UnWireEvents()
		{
			try
			{
				if (m_naWindowEventSource == null)
					return;

				UnWireSelectionEvent();
				UnWireGraphicsEvents();

				((ESRI.ArcGIS.NetworkAnalystUI.INAWindowEvents_Event)(m_naWindowEventSource)).OnActiveAnalysisChanged -= m_ActiveAnalysisChanged;
				m_naWindowEventSource = null;
			}
			catch (Exception ex)
			{
				string msg = SubsetHelperUI.GetFullExceptionMessage(ex);
				MessageBox.Show(msg, "UnWire Events");
			}
		}

		private void WireSelectionEvent()
		{
			try
			{
				if (m_naWindowEventSource == null)
					return;

				if (m_mapEventSource != null)
					UnWireSelectionEvent();

				m_mapEventSource = ActiveMap;
				if (m_mapEventSource == null)
					return;

				UpdateSelectionEIDArrayParameterValues();

				//Create an instance of the delegate, add it to SelectionChanged event
				m_ActiveViewEventsSelectionChanged = new ESRI.ArcGIS.Carto.IActiveViewEvents_SelectionChangedEventHandler(OnActiveViewEventsSelectionChanged);
				((ESRI.ArcGIS.Carto.IActiveViewEvents_Event)(m_mapEventSource)).SelectionChanged += m_ActiveViewEventsSelectionChanged;
			}
			catch (Exception ex)
			{
				string msg = SubsetHelperUI.GetFullExceptionMessage(ex);
				MessageBox.Show(msg, "Wire Selection Event");
			}
		}

		private void UnWireSelectionEvent()
		{
			try
			{
				if (m_naWindowEventSource == null)
					return;

				if (m_mapEventSource == null)
					return;

				((ESRI.ArcGIS.Carto.IActiveViewEvents_Event)(m_mapEventSource)).SelectionChanged -= m_ActiveViewEventsSelectionChanged;
				m_mapEventSource = null;

				SubsetHelperUI.ClearEIDArrayParameterValues(m_nax, SubsetHelperUI.SelectionEIDArrayBaseName);
			}
			catch (Exception ex)
			{
				string msg = SubsetHelperUI.GetFullExceptionMessage(ex);
				MessageBox.Show(msg, "UnWire Selection Event");
			}
		}

		private void WireGraphicsEvents()
		{
			try
			{
				if (m_naWindowEventSource == null)
					return;

				if (m_graphicsEventSource != null)
					UnWireGraphicsEvents();

				IMap activeMap = ActiveMap;
				IGraphicsLayer graphicsLayer = null;
				if (activeMap != null)
					graphicsLayer = activeMap.BasicGraphicsLayer;

				if (graphicsLayer != null)
					m_graphicsEventSource = (IGraphicsContainer)graphicsLayer;

				if (m_graphicsEventSource == null)
					return;

				UpdateGraphicsEIDArrayParameterValues();

				//Create an instance of the delegate, add it to AllElementsDeleted event
				m_AllGraphicsDeleted = new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_AllElementsDeletedEventHandler(OnAllGraphicsDeleted);
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).AllElementsDeleted += m_AllGraphicsDeleted;

				//Create an instance of the delegate, add it to ElementAdded event
				m_GraphicAdded = new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementAddedEventHandler(OnGraphicAdded);
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).ElementAdded += m_GraphicAdded;

				//Create an instance of the delegate, add it to ElementDeleted event
				m_GraphicDeleted = new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementDeletedEventHandler(OnGraphicDeleted);
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).ElementDeleted += m_GraphicDeleted;

				//Create an instance of the delegate, add it to ElementsAdded event
				m_GraphicsAdded = new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementsAddedEventHandler(OnGraphicsAdded);
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).ElementsAdded += m_GraphicsAdded;

				//Create an instance of the delegate, add it to ElementUpdated event
				m_GraphicUpdated = new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementUpdatedEventHandler(OnGraphicUpdated);
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).ElementUpdated += m_GraphicUpdated;
			}
			catch (Exception ex)
			{
				string msg = SubsetHelperUI.GetFullExceptionMessage(ex);
				MessageBox.Show(msg, "Wire Graphics Events");
			}
		}

		private void UnWireGraphicsEvents()
		{
			try
			{
				if (m_naWindowEventSource == null)
					return;

				if (m_graphicsEventSource == null)
					return;

				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).AllElementsDeleted -= m_AllGraphicsDeleted;
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).ElementAdded -= m_GraphicAdded;
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).ElementDeleted -= m_GraphicDeleted;
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).ElementsAdded -= m_GraphicsAdded;
				((ESRI.ArcGIS.Carto.IGraphicsContainerEvents_Event)(m_graphicsEventSource)).ElementUpdated -= m_GraphicUpdated;

				m_graphicsEventSource = null;

				SubsetHelperUI.ClearEIDArrayParameterValues(m_nax, SubsetHelperUI.GraphicsEIDArrayBaseName);
			}
			catch (Exception ex)
			{
				string msg = SubsetHelperUI.GetFullExceptionMessage(ex);
				MessageBox.Show(msg, "UnWire Graphics Events");
			}
		}
		#endregion

		private void UpdateSelectionEIDArrayParameterValues()
		{
			IMap map = ActiveMap;
			if (map == null)
				return;

			INAWindow naWindow = m_nax.NAWindow;
			INALayer naLayer = null;
			INAContext naContext = null;
			INetworkDataset nds = null;

			naLayer = naWindow.ActiveAnalysis;
			if (naLayer != null)
				naContext = naLayer.Context;

			if (naContext != null)
				nds = naContext.NetworkDataset;

			if (nds == null)
				return;

			string baseName = SubsetHelperUI.SelectionEIDArrayBaseName;
			VarType vt = SubsetHelperUI.GetEIDArrayParameterType();

			List<string> sourceNames = SubsetHelperUI.FindParameterizedSourceNames(nds, baseName, vt);
			Dictionary<string, ILongArray> oidArraysBySourceName = SubsetHelperUI.GetOIDArraysBySourceNameFromMapSelection(map, sourceNames);
			SubsetHelperUI.UpdateEIDArrayParameterValuesFromOIDArrays(m_nax, oidArraysBySourceName, baseName);
		}

		private void UpdateGraphicsEIDArrayParameterValues()
		{
			IGraphicsContainer graphics = ActiveGraphics;
			if (graphics == null)
				return;

			INAWindow naWindow = m_nax.NAWindow;
			INALayer naLayer = null;
			INAContext naContext = null;
			INetworkDataset nds = null;

			naLayer = naWindow.ActiveAnalysis;
			if (naLayer != null)
				naContext = naLayer.Context;

			if (naContext != null)
				nds = naContext.NetworkDataset;

			if (nds == null)
				return;

			string baseName = SubsetHelperUI.GraphicsEIDArrayBaseName;
			VarType vt = SubsetHelperUI.GetEIDArrayParameterType();

			List<string> sourceNames = SubsetHelperUI.FindParameterizedSourceNames(nds, baseName, vt);
			IGeometry searchGeometry = SubsetHelperUI.GetSearchGeometryFromGraphics(graphics);
			SubsetHelperUI.UpdateEIDArrayParameterValuesFromGeometry(m_nax, searchGeometry, baseName);
		}

		private IMap ActiveMap
		{
			get
			{
				IDocument doc = m_application.Document;
				IMxDocument mxdoc = doc as IMxDocument;
				return (IMap)mxdoc.FocusMap;
			}
		}

		private IGraphicsContainer ActiveGraphics
		{
			get
			{
				IMap activeMap = ActiveMap;
				IGraphicsContainer graphics = null;
				if (activeMap != null)
					graphics = activeMap.BasicGraphicsLayer as IGraphicsContainer;

				return graphics;
			}
		}

		#region Event Handlers

		#region NAWindow Event Handlers
		private void OnActiveAnalysisChanged()
		{
			if (m_mapEventSource != null)
				WireSelectionEvent();

			if (m_graphicsEventSource != null)
				WireGraphicsEvents();
		}

		#endregion

		#region Selection Event Handler
		private void OnActiveViewEventsSelectionChanged()
		{
			UpdateSelectionEIDArrayParameterValues();
		}
		#endregion

		#region Graphics Event Handlers
		private void OnAllGraphicsDeleted()
		{
			UpdateGraphicsEIDArrayParameterValues();
		}

		private void OnGraphicAdded(IElement element)
		{
			UpdateGraphicsEIDArrayParameterValues();
		}

		private void OnGraphicDeleted(IElement element)
		{
			UpdateGraphicsEIDArrayParameterValues();
		}

		private void OnGraphicsAdded(IElementCollection elements)
		{
			UpdateGraphicsEIDArrayParameterValues();
		}

		private void OnGraphicUpdated(IElement element)
		{
			UpdateGraphicsEIDArrayParameterValues();
		}
		#endregion

		#endregion
	}
}
