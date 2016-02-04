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
     [System.Runtime.InteropServices.GuidAttribute(
        "BF1FB8A1-EFD0-49AF-860F-2D766B6B424E")]
    public abstract class SOEPropertyPage: ESRI.ArcGIS.Framework.IComPropertyPage,
        ESRI.ArcGIS.CatalogUI.IAGSSOEParameterPage
    {
        // Automatically register with ArcCatalog as a component, use categories.exe -> 
        // AGS Extension Parameter Pages to view. 
        [System.Runtime.InteropServices.ComRegisterFunction()]
        static void RegisterFunction
            (String regKey)
        {
            Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(regKey.Substring(18) +
                "\\Implemented Categories\\" + "{A585A585-B58B-4560-80E3-87A411859379}");
        }

        [System.Runtime.InteropServices.ComUnregisterFunction()]
        static void
            UnregisterFunction(String regKey)
        {
            Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(regKey.Substring(18));
        }

        public abstract ESRI.ArcGIS.Framework.IComPropertyPageSite PageSite
        {
            set;
        }

        public abstract int Activate();
        public abstract void Show();
        public abstract void Hide();


        public virtual int Height
        {
            get
            {
                return 0;
            }
        }

        public virtual void Deactivate() { }

        public bool IsPageDirty
        {
            get
            {
                return false;
            }
        }

        public string Title
        {
            get
            {
                return null;
            }
            set { }
        }

        public int Priority
        {
            get
            {
                return 0;
            }
            set { }
        }

        public string HelpFile
        {
            get
            {
                return null;
            }
        }

        public virtual int Width
        {
            get
            {
                return 0;
            }
        }

        public void Apply() { }
        public void Cancel() { }
        public int get_HelpContextID(int controlID)
        {
            return 0;
        }

        public void SetObjects(ESRI.ArcGIS.esriSystem.ISet objects) { }
        public bool Applies(ESRI.ArcGIS.esriSystem.ISet objects)
        {
            return false;
        }

        public abstract ESRI.ArcGIS.esriSystem.IPropertySet ServerObjectProperties
        {
            get;
            set;
        }

        public abstract ESRI.ArcGIS.esriSystem.IPropertySet ExtensionProperties
        {
            get;
            set;
        }

        public abstract string ServerObjectExtensionType
        {
            get;
        }

        public abstract string ServerObjectType
        {
            get;
        }
    }
}
