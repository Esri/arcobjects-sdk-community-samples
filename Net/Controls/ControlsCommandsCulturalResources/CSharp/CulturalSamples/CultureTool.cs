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
using System.Resources;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Globalization;

using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;

namespace VBCSharpCultureSample
{
    /// <summary>
    /// Summary description for Tool1.
    /// </summary>
    [Guid("490b461a-f596-4177-9b5d-411cbb13a684")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("VBCSharpCultureSample.CultureTool")]
    public sealed class CultureTool : BaseTool
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

        private IHookHelper m_pHookHelper;

        public CultureTool()
        {
            ResourceManager rm = new ResourceManager("VBCSharpCultureSample.Resources", Assembly.GetExecutingAssembly());
            base.m_bitmap = (System.Drawing.Bitmap)rm.GetObject("ToolImage");

            base.m_message = (string)rm.GetString("ToolMessage");
            base.m_toolTip = (string)rm.GetString("ToolToolTip");
            base.m_caption = (string)rm.GetString("ToolCaption");
            base.m_category = "CustomCommands";
            base.m_name = "CustomCommands_CultureTool";

        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_pHookHelper == null)
                m_pHookHelper = new HookHelperClass();

            m_pHookHelper.Hook = hook;

            // TODO:  Add Tool1.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add Tool1.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            //With this tool the user may place the current Date and Time onto the Page Layout
            //using the Timestamp format defined by the UI Culture of the current thread

            //Get the active view
            IActiveView pActiveView;
            pActiveView = m_pHookHelper.ActiveView;

            //Create a new text element
            ITextElement pTextElement = new TextElementClass();

            //Create a text symbol
            ESRI.ArcGIS.Display.ITextSymbol pTextSymbol = new ESRI.ArcGIS.Display.TextSymbolClass(); 
            pTextSymbol.Size = 10;

            //Create a page point
            IPoint pPoint;
            pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);

            //Get the FullDateTimePattern from the CurrentUICulture of the thread
            string pDateTimePattern;
            pDateTimePattern = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern.ToString();

            //Set the text element properties
            pTextElement.Symbol = pTextSymbol;
            pTextElement.Text = System.DateTime.Now.ToString(pDateTimePattern);

            //QI for IElement
            IElement pElement;
            pElement = (IElement)pTextElement;
            //Set the elements geometry
            pElement.Geometry = pPoint;

            //Add the element to the graphics container
            pActiveView.GraphicsContainer.AddElement(pElement, 0);
            //Refresh the graphics
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);


        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add Tool1.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add Tool1.OnMouseUp implementation
        }
        #endregion
    }
}
