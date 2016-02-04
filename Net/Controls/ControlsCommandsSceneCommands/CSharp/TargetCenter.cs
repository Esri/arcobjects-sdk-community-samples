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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace sceneTools
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("0FA87A1D-1C7E-4458-AE1C-DF85243A2793")]

	public sealed class TargetCenter : BaseTool
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

		public TargetCenter()
		{
			base.m_category = "Sample_SceneControl(C#)";
			base.m_caption = "Target Center";
			base.m_toolTip = "Center on Target";
			base.m_name = "Sample_SceneControl(C#)/TargetCenter";
			base.m_message = "Center view at selected target";

			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.TargetCenter.bmp"));
			}
			m_pCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.targetcenter.cur"));
		
			m_pSceneHookHelper = new SceneHookHelperClass ();
		}

		public override bool Enabled
		{
			get
			{
				if(m_pSceneHookHelper.Scene == null)
					return false;
				else
					return true;
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

			IPoint pNewTgt;
			object pOwner, pObject;

			//Translate screen coordinates into a 3D point
			pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickAll, true,
				out pNewTgt, out pOwner, out pObject);

			if(pNewTgt == null) return;

			//Get the scene viewer's camera
			ICamera pCamera = (ICamera) m_pSceneHookHelper.Camera;

			//If orthographic (2D) view
			if(pCamera.ProjectionType == esri3DProjectionType.esriOrthoProjection )
			{
				//Set the camera's new target
				pCamera.Target = pNewTgt;

				//Redraw the scene viewer
				pSceneGraph.ActiveViewer.Redraw(true);
			}
			else
			{
				//Get the camera's old target and observer
				IPoint pOldTgt, pOldObs;
				pOldTgt = pCamera.Target;
				pOldObs = pCamera.Observer;

				//Set the camera's new target and get the new observer
				pCamera.Target = pNewTgt;
				pCamera.PolarUpdate(1, 0, 0, true);

				IPoint pNewObs = (IPoint) pCamera.Observer;

				//Get the duration in seconds of last redraw
				//and the average number of frames per second
				double dlastFrameDuration, dMeanFrameRate;
				pSceneGraph.GetDrawingTimeInfo(out dlastFrameDuration, out dMeanFrameRate);
				
				if(dlastFrameDuration < 0.01)
					dlastFrameDuration = 0.01;

				int iSteps;
				iSteps = (int) (2 / dlastFrameDuration);
				if (iSteps < 1)
					iSteps = 1;

				if(iSteps > 60)
					iSteps = 60;

				double dxObs, dyObs, dzObs;
				double dxTgt, dyTgt, dzTgt;

				dxObs = (pNewObs.X - pOldObs.X) / iSteps;
				dyObs = (pNewObs.Y - pOldObs.Y) / iSteps;
				dzObs = (pNewObs.Z - pOldObs.Z) / iSteps;

				dxTgt = (pNewTgt.X - pOldTgt.X) / iSteps;
				dyTgt = (pNewTgt.Y - pOldTgt.Y) / iSteps;
				dzTgt = (pNewTgt.Z - pOldTgt.Z) / iSteps;

				//Loop through each step moving the camera's observer and target from the
				//old positions to the new positions, refreshing the scene viewer each time
				for(int i=0; i < iSteps; i++)
				{
					pNewObs.X = pOldObs.X + (i * dxObs);
					pNewObs.Y = pOldObs.Y + (i * dyObs);
					pNewObs.Z = pOldObs.Z + (i * dzObs);

					pNewTgt.X = pOldTgt.X + (i * dxTgt);
					pNewTgt.Y = pOldTgt.Y + (i * dyTgt);
					pNewTgt.Z = pOldTgt.Z + (i * dzTgt);

					pCamera.Observer = pNewObs;
					pCamera.Target = pNewTgt;
					pSceneGraph.ActiveViewer.Redraw(true);
				}
			}
		}
	}
}
