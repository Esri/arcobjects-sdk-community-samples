/*

   Copyright 2019 Esri

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;

namespace AnimationDeveloperSamples
{
    [Guid("EC7FC44A-3516-4872-8F93-944259B34662")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomMapAnimation1.MapGraphicTrackPropPage")]
    public class MapGraphicTrackPropPage : IComPropertyPage
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
            AGAnimationTrackPropertyPages.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            AGAnimationTrackPropertyPages.Unregister(regKey);

        }

        #endregion
        #endregion
        IAGAnimationTrack targetTrack;
        int priority;
        IComPropertyPageSite pageSite;
        frmPropertyPage propPage;

        public MapGraphicTrackPropPage()
        {
            propPage = new frmPropertyPage();
            priority = 0;
        }

        #region IComPropertyPage members
        public int Activate()
        {
            return propPage.Handle.ToInt32();
        }

        public bool Applies(ISet Objects)
        {
            object obj;
            IAGAnimationTrack track;
            int count = Objects.Count;
            Objects.Reset();
            bool appl = false;
            for (int i = 0; i < count; i++)
            {
                obj = Objects.Next();
                track = (IAGAnimationTrack)obj;
                if (track != null)
                {
                    if (track.AnimationType.Name == "Map Graphic")
                    {
                        appl = true;
                        break;
                    }
                }
            }
            return appl;        
        }

        public void SetObjects(ISet Objects)
        {
            int count = Objects.Count;
            Objects.Reset();
            for (int i = 0; i < count; i++)
            {
                object obj = Objects.Next();
                targetTrack = (IAGAnimationTrack)obj;
                if (targetTrack != null)
                {                    
                    break;
                }
            }
            propPage.Init(targetTrack);
        }

        public void Apply()
        {
            IAGAnimationTrackExtensions trackExtensions = (IAGAnimationTrackExtensions)targetTrack;
            IMapGraphicTrackExtension trackExtension;
            if (trackExtensions.ExtensionCount == 0) //if there is no extension, add one
            {
                trackExtension = new MapGraphicTrackExtension();
                trackExtensions.AddExtension(trackExtension);
            }
            else
            {
                trackExtension = (IMapGraphicTrackExtension)trackExtensions.get_Extension(0);
            }

            trackExtension.ShowTrace = propPage.CheckBoxShowTrace.Checked;
        }

        public void Cancel()
        {

        }
        public void Deactivate()
        {
            targetTrack = null;
            propPage.Dispose();
        }
        public int Height
        {
            get
            {
                return propPage.Height;
            }
        }
        public int Width
        {
            get
            {
                return propPage.Width;
            }
        }

        public int get_HelpContextID(int controlID)
        {
            return 0;
        }
        public string HelpFile
        {
            get
            {
                return "";
            }
        }
        public void Hide()
        {
            propPage.Hide();
        }

        public bool IsPageDirty
        {
            get
            {
                return propPage.PageDirty;
            }
        }
        public IComPropertyPageSite PageSite
        {
            set
            {
                pageSite = value;
            }
        }
        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

        public void Show()
        {
            propPage.Visible = true;
        }

        public string Title
        {
            get
            {
                return propPage.Text;
            }
            set
            {
                propPage.Text = value;
            }
        }
        #endregion
    }
}
