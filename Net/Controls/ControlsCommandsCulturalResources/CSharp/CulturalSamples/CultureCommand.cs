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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Resources;
using System.Reflection;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;

namespace VBCSharpCultureSample
{
    /// <summary>
    /// Summary description for Command1.
    /// </summary>
    [Guid("9a57ce53-8bd2-43a7-b8ff-db07d6d925f6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("VBCSharpCultureSample.CultureCommand")]
    public sealed class CultureCommand : BaseCommand
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

        public CultureCommand()
        {

            ResourceManager rm = new ResourceManager("VBCSharpCultureSample.Resources", Assembly.GetExecutingAssembly());
            base.m_bitmap = (System.Drawing.Bitmap)rm.GetObject("CommandImage");

            base.m_message = (string)rm.GetString("CommandMessage");
            base.m_toolTip = (string)rm.GetString("CommandToolTip");
            base.m_caption = (string)rm.GetString("CommandCaption");
            base.m_category = "CustomCommands";
            base.m_name = "CustomCommands_CultureCommand";

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

        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            //This button simply tells the user which culture and regional language the application
            //employs when it is running.

            //Acquire the Thread Culture Name
            string m_culture;
            m_culture = "Culture = " + System.Threading.Thread.CurrentThread.CurrentUICulture.DisplayName.ToString();

            //Acquire the Regional Language string
            string m_language;
            m_language = "The culture of this application employs the '"
            + System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToString()
            + "' regional and language code " + Environment.NewLine;

            //Acquire the Timestamp format
            string m_datetime;
            m_datetime = "with the following timestamp format: "
            + System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern.ToString();

            //Write the message to the MessageBox
            string m_message;
            m_message = m_language + m_datetime;
            System.Windows.Forms.MessageBox.Show(m_message, m_culture);
        }

        #endregion
    }
}
