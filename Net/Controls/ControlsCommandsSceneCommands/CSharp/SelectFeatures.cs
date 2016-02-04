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
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace sceneTools
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("7B9ECCAC-4B67-4795-9E1E-1EAF7CC64CE4")]

	public sealed class SelectFeatures : BaseTool
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

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion
    
		private System.Windows.Forms.Cursor m_pCursor;
		private ISceneHookHelper m_pSceneHookHelper;

		public SelectFeatures()
		{
			base.m_category = "Sample_SceneControl(C#)";
			base.m_caption = "Select Features";
			base.m_toolTip = "Select Features";
			base.m_name = "Sample_SceneControl(C#)/SelectFeatures";
			base.m_message = "Select features by clicking";

			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.SelectFeatures.bmp"));
			}
			m_pCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.SelectFeatures.cur"));
		
			m_pSceneHookHelper = new SceneHookHelperClass ();
		}
        
		public override void OnCreate(object hook)
		{
			m_pSceneHookHelper.Hook = hook;
		}
	
		public override bool Enabled
		{
			get
			{
				if(m_pSceneHookHelper.Hook == null || m_pSceneHookHelper.Scene == null)
					return false;
				else
				{
					IScene pScene = (IScene) m_pSceneHookHelper.Scene;

					//Disable if no layer
					if(pScene.LayerCount == 0)
						return false;

					//Enable if any selectable layers
					bool bSelectable = false;

					IEnumLayer pEnumLayer;
					pEnumLayer = pScene.get_Layers(null, true);
					pEnumLayer.Reset();

					ILayer pLayer = (ILayer) pEnumLayer.Next();

					//Loop through the scene layers
					do
					{
						//Determine if there is a selectable feature layer
						if(pLayer is IFeatureLayer)
						{
							IFeatureLayer pFeatureLayer = (IFeatureLayer) pLayer;
							if(pFeatureLayer.Selectable == true)
							{
								bSelectable = true;
								break;
							}
						}
						pLayer = pEnumLayer.Next();
					}
					while(pLayer != null);
				
					return bSelectable;
				}
			}
		}
	
		public override int Cursor
		{
			get
			{
				return m_pCursor.Handle.ToInt32();
			}
		}
	
		public override bool Deactivate()
		{
			return true;
		}
	
		public override void OnMouseUp(int Button, int Shift, int X, int Y)
		{
			//Get the scene graph
			ISceneGraph pSceneGraph = m_pSceneHookHelper.SceneGraph;

			//Get the scene
			IScene pScene = (IScene) m_pSceneHookHelper.Scene;

			IPoint pPoint;
			object pOwner, pObject;

			//Translate screen coordinates into a 3D point
			pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickGeography, true, out pPoint, out pOwner, out pObject);

			//Get a selection environment
			ISelectionEnvironment pSelectionEnv;
			pSelectionEnv = new SelectionEnvironmentClass();
			
			if(Shift == 0)
			{
				pSelectionEnv.CombinationMethod = ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew;

				//Clear previous selection
				if(pOwner == null)
				{
					pScene.ClearSelection();
					return;
				}
			}
			else
				pSelectionEnv.CombinationMethod = ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultAdd;

			//If the layer is a selectable feature layer
			if(pOwner is IFeatureLayer)
			{
				IFeatureLayer pFeatureLayer = (IFeatureLayer) pOwner;

				if(pFeatureLayer.Selectable == true)
				{
					//Select by Shape
					pScene.SelectByShape(pPoint, pSelectionEnv, false);
				}
			}

			//Refresh the scene viewer
			pSceneGraph.RefreshViewers();
		}
	}
}
