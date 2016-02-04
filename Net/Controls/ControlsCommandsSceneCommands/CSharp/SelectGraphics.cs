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
	[Guid("0BE878DB-6A85-4e40-BEB7-3488A64A51A7")]

	public sealed class SelectGraphics : BaseTool
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

		public SelectGraphics()
		{
			base.m_category = "Sample_SceneControl(C#)";
			base.m_caption = "Select Graphics";
			base.m_toolTip = "Select Graphics";
			base.m_name = "Sample_SceneControl(C#)/SelectGraphics";
			base.m_message = "Select graphics by clicking";

			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.select.bmp"));
			}
			m_pCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.selectGraphics.cur"));
		
			m_pSceneHookHelper = new SceneHookHelperClass ();
		}

		public override bool Enabled
		{
			get
			{
				if(m_pSceneHookHelper.Hook == null || m_pSceneHookHelper.Scene == null)
					return false;
				else
				{
					//Determine if there is a 3D graphics container
					IScene pScene = (IScene) m_pSceneHookHelper.Scene;

					if(pScene.ActiveGraphicsLayer is IGraphicsContainer3D)
						return true;
					else
						return false;
				}
			}
		}
	
		public override void OnCreate(object hook)
		{
			m_pSceneHookHelper.Hook = hook;
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
			ISceneGraph pSceneGraph = (ISceneGraph) m_pSceneHookHelper.SceneGraph;

			IPoint pPoint;
			object pOwner, pObject;

			//Translate screen coordinates into a 3D point
			pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickGraphics, false, 
				out pPoint, out pOwner, out pObject);
			
			IGraphicsSelection pGraphicsSelection;
			
			if(pObject == null)
			{
				if(Shift == 0)
				{
					//Unselect selected graphics from the
					//basic graphics layer and each layer
					pGraphicsSelection = pSceneGraph.Scene.BasicGraphicsLayer as IGraphicsSelection ;
					pGraphicsSelection.UnselectAllElements();

					for(int i = 0; i < pSceneGraph.Scene.LayerCount - 1; i++)
					{
						ILayer pLayer = (ILayer) pSceneGraph.Scene.get_Layer(i);
						pGraphicsSelection = pLayer as IGraphicsSelection ;
						pGraphicsSelection.UnselectAllElements();
					}
				}
			}
			else
			{
				pGraphicsSelection = pOwner as IGraphicsSelection ;

				//Unselect any selected graphics
				if(Shift == 0)
					pGraphicsSelection.UnselectAllElements();

				//Select element
				IElement pElement = (IElement) pObject;
				pGraphicsSelection.SelectElement(pElement);
			}

			//Refresh the scene viewer
			pSceneGraph.ActiveViewer.Redraw(false);
		}
	}
}
