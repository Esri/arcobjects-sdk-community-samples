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
// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.4/userestrictions.txt.
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSimpleRESTSOEWithProperties.Cat
{
    [System.Runtime.InteropServices.Guid("49AA3B86-D6D9-44CA-B6A1-68573DD543AD")]
    public class PropertyPage : SOEPropertyPage
    {

        private ESRI.ArcGIS.esriSystem.IPropertySet m_extensionProperties;
        private ESRI.ArcGIS.esriSystem.IPropertySet m_serverObjectProperties;
        private string m_extensionType;
        private string m_serverObjectType;
        private PropertyForm propertyPage;

        public PropertyPage()
        {
            propertyPage = new PropertyForm();
            m_serverObjectType = "MapServer";
            // Must be the same name used when registering the SOE.
            m_extensionType = "NetSimpleRESTSOEWithProperties";
        }

        ~PropertyPage()
        {
            propertyPage.Dispose();
            propertyPage = null;
        }

        /// <summary>
        /// PageSite permits accessed to the PageChanged method, which, when fired, enables the Apply button on the property page
        /// </summary>
        public override ESRI.ArcGIS.Framework.IComPropertyPageSite PageSite
        {
            set { propertyPage.PageSite = value; }
        }

        /// <summary>
        /// Displays the form defining the property page's UI
        /// </summary>
        public override void Show()
        {
            propertyPage.Show();
        }

        /// <summary>
        /// Fired when the property page is activated
        /// </summary>
        /// <returns>the handle of the form containing the property page's UI</returns>
        public override int Activate()
        {
            return propertyPage.getHWnd();
        }

        /// <summary>
        /// Hides the form containing the property page's UI
        /// </summary>
        public override void Hide()
        {
            propertyPage.Hide();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }


        public override ESRI.ArcGIS.esriSystem.IPropertySet ServerObjectProperties
        {
            get
            {
                return m_serverObjectProperties;
            }
            set
            {
                // Pass the location of the map document underlying the current 
                //  server object to the property form.
                m_serverObjectProperties = value;
            }
        }

        public override ESRI.ArcGIS.esriSystem.IPropertySet ExtensionProperties
        {
            get
            {
                m_extensionProperties.SetProperty("LayerType", propertyPage.LayerType);
                m_extensionProperties.SetProperty("MaxNumFeatures", propertyPage.MaxNumFeatures);
                m_extensionProperties.SetProperty("ReturnFormat", propertyPage.ReturnFormat);
                m_extensionProperties.SetProperty("IsEditable", propertyPage.IsEditable);

                return m_extensionProperties;
            }
            set
            {
                m_extensionProperties = value;
                propertyPage.LayerType = m_extensionProperties.GetProperty("LayerType").ToString();
                propertyPage.MaxNumFeatures = m_extensionProperties.GetProperty("MaxNumFeatures").ToString();
                propertyPage.ReturnFormat = m_extensionProperties.GetProperty("ReturnFormat").ToString();
                propertyPage.IsEditable = m_extensionProperties.GetProperty("IsEditable").ToString();
            }
        }

        public override string ServerObjectExtensionType
        {
            get
            {
                return m_extensionType;
            }
        }

        public override string ServerObjectType
        {
            get
            {
                return m_serverObjectType;
            }
        }


         public override int Height
         {
           get
           {
             return propertyPage.Height;
           }
         }

         public override int Width
         {
           get
           {
             return propertyPage.Width;
           }
         }
    }
}
