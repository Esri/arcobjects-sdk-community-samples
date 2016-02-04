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
using System.Runtime.InteropServices;
using System.Messaging;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace RSSWeatherLayer
{
  /// <summary>
  /// Summary description for RSSLayerProperties.
  /// </summary>
  [Guid("C8442468-53EE-40d7-A241-896FB8B2E027")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("RSSWeatherLayer.RSSLayerProperties")]
  [ComVisible(true)]
  public sealed class RSSLayerProperties : BaseCommand
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

    public RSSLayerProperties()
    {
      base.m_category = "Weather";
      base.m_caption = "Weather Layer properties";
      base.m_message = "Show RSS Weather Layer properties";
      base.m_toolTip = "Show RSS Weather Layer properties";
      base.m_name = base.m_category + "_" + base.m_caption;

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
      if (m_pHookHelper == null)
        m_pHookHelper = new HookHelperClass();

      m_pHookHelper.Hook = hook;

      // TODO:  Add RSSLayerProperties.OnCreate implementation
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      try
      {
        //search for the weatherLayer first
        ILayer layer = null;
        RSSWeatherLayerClass RSSLayer = null;

        if (m_pHookHelper.FocusMap.LayerCount == 0)
          return;

        IEnumLayer layers = m_pHookHelper.FocusMap.get_Layers(null, false);
        layers.Reset();
        layer = layers.Next();
        while (layer != null)
        {
          if (layer is RSSWeatherLayerClass)
          {
            RSSLayer = (RSSWeatherLayerClass)layer;
            break;
          }
          layer = layers.Next();
        }

        //In case that the weather layer wasn't found,just return
        if (null == RSSLayer)
          return;


        //Launch the layer's properties
        Type typ;
        object obj;
        Guid[] g;

        // METHOD 1: Instantiating a COM object and displaying its property pages
        // ONLY WORKS ON TRUE COM OBJECTS!  .NET objects that have rolled their own
        // ISpecifyPropertyPages implementation will error out when you try to cast
        // the instantiated object to your own ISpecifyPropertyPages implementation.

        // Get the typeinfo for the ActiveX common dialog control
        typ = Type.GetTypeFromProgID("MSComDlg.CommonDialog");

        // Create an instance of the control.  We pass it to the property frame function
        // so the property pages have an object from which to get current settings and apply
        // new settings.
        obj = Activator.CreateInstance(typ);
        // This handy function calls IPersistStreamInit->New on COM objects to initialize them
        ActiveXMessageFormatter.InitStreamedObject(obj);

        // Get the property pages for the control using the direct CAUUID method
        // This only works for true COM objects and I demonstrate it here only
        // to show how it is done.  Use the static method
        // PropertyPage.GetPagesForType() method for real-world use.
        ISpecifyPropertyPages pag = (ISpecifyPropertyPages)obj;
        CAUUID cau = new CAUUID(0);
        pag.GetPages(ref cau);
        g = cau.GetPages();

        // Instantiating a .NET object and displaying its property pages
        // WORKS ON ALL OBJECTS, .NET or COM    

        // Create an instance of the .NET control, MyUserControl
        typ = Type.GetTypeFromProgID("RSSWeatherLayer.PropertySheet");

        // Retrieve the pages for the control
        g = PropertyPage.GetPagesForType(typ);

        // Create an instance of the control that we can give to the property pages
        obj = Activator.CreateInstance(typ);

        //add the RSS layer to the property-sheet control
        ((PropertySheet)obj).RSSWatherLayer = RSSLayer;

        // Display the OLE Property page for the control
        object[] items = new object[] { obj };

        PropertyPage.CreatePropertyFrame(IntPtr.Zero, 500, 500, "RSS Layer properties", items, g);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    #endregion
  }
}
