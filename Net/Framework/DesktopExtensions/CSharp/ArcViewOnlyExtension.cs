using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;

namespace DesktopExtensionsCS
{
    [Guid("1ce76e5e-261c-48d9-a3f5-3ae8f3c3eac7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DesktopExtensionsCS.ArcViewOnlyExtension")]
    public class ArcViewOnlyExtension : IExtension, IExtensionConfig
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
            MxExtension.Register(regKey);
            SxExtensions.Register(regKey);
            GxExtensions.Register(regKey);
            GMxExtensions.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxExtension.Unregister(regKey);
            SxExtensions.Unregister(regKey);
            GxExtensions.Unregister(regKey);
            GMxExtensions.Unregister(regKey);

        }

        #endregion
        #endregion
        private IApplication m_application;
        private esriExtensionState m_enableState;

        private const esriLicenseProductCode RequiredProductCode = esriLicenseProductCode.esriLicenseProductCodeBasic;
        
        #region IExtension Members

        /// <summary>
        /// Name of extension. Do not exceed 31 characters
        /// </summary>
        public string Name
        {
            get
            {
                return "ArcViewOnlyExtension_CS";
            }
        }

        public void Shutdown()
        {
            m_application = null;
        }

        public void Startup(ref object initializationData)
        {
            m_application = initializationData as IApplication;
            if (m_application == null)
                return;
        }

        #endregion

        #region IExtensionConfig Members

        public string Description
        {
            get
            {
                return "Basic Only Extension (C# Sample)\r\n" +
                    "Copyright © ESRI 2006\r\n\r\n" +
                    "Only available with an Basic product license.";
            }
        }

        /// <summary>
        /// Friendly name shown in the Extension dialog
        /// </summary>
        public string ProductName
        {
            get
            {
                return "Basic Extension (C#)";
            }
        }

        public esriExtensionState State
        {
            get
            {
                return m_enableState;
            }
            set
            {
                if (m_enableState != 0 && value == m_enableState)
                    return;

                //Check if ok to enable or disable extension
                esriExtensionState requestState = value;
                if (requestState == esriExtensionState.esriESEnabled)
                {
                    //Cannot enable if it's already in unavailable state
                    if (m_enableState == esriExtensionState.esriESUnavailable)
                    {
                        throw new COMException("Not running the appropriate product license.");
                    }

                    //Determine if state can be changed
                    esriExtensionState checkState = StateCheck(true);
                    m_enableState = checkState;
                    if (m_enableState == esriExtensionState.esriESUnavailable)
                        throw new COMException("Not running the appropriate product license.");
                }
                else if (requestState == 0 || requestState == esriExtensionState.esriESDisabled)
                {
                    //Determine if state can be changed
                    esriExtensionState checkState = StateCheck(false);
                    if (checkState != m_enableState)
                        m_enableState = checkState;
                }

            }
        }

        #endregion

        /// <summary>
        /// Determine extension state 
        /// </summary>
        /// <param name="requestEnable">true if to enable; false to disable</param>
        private esriExtensionState StateCheck(bool requestEnable)
        {
            //Turn on or off extension directly
            if (requestEnable)
            {
                //Check if the correct product is licensed
                IAoInitialize aoInitTestProduct = new AoInitializeClass();
                esriLicenseProductCode prodCode = aoInitTestProduct.InitializedProduct();
                if (prodCode == RequiredProductCode)
                    return esriExtensionState.esriESEnabled;

                return esriExtensionState.esriESUnavailable;
            }
            else
                return esriExtensionState.esriESDisabled;
        }

    }
}