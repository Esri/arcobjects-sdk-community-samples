using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using CustomFunction;
using System.Runtime.InteropServices;

namespace NDVICustomFunctionUI
{
    [Guid("6DAD598C-D4D5-466A-9754-BE78CA4C41ED")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomFunctionUI.NDVICustomFunctionUIClass")]
    [ComVisible(true)]
    public class NDVICustomFunctionUIClass : IComPropertyPage
    {
        #region Private Members
        NDVICustomFunctionUIForm myForm; // The UI form object.
        INDVICustomFunctionArguments myArgs; // The NDVI Custom function arguments object.
        int myPriority; // Priority for the UI page.
        IComPropertyPageSite myPageSite;
        string myHelpFile; // Location for the help file if needed.
        UID mySupportedID; // UID for the Raster function supported by the property page.
        bool templateMode; // Flag to specify template mode.
        bool isFormReadOnly; // Flag to specify whether the UI is in Read-Only Mode.

        IRasterFunctionVariable myRasterVar; // Variable for the Raster property.
        IRasterFunctionVariable myBandIndicesVar; // Variable for the Band Indices property.
        #endregion

        public NDVICustomFunctionUIClass()
        {
            myForm = new NDVICustomFunctionUIForm();
            myArgs = null;
            myPriority = 100;
            myPageSite = null;
            myHelpFile = "";
            mySupportedID = new UIDClass();
            // The UID of the NDVICustomFunction object.
            mySupportedID.Value = "{" + "652642F3-9106-4EB3-9262-A4C39E03BC56" + "}";
            templateMode = false;

            myRasterVar = null;
            myBandIndicesVar = null;
        }

        #region IComPropertyPage Members

        /// <summary>
        /// Activate the form. 
        /// </summary>
        /// <returns>Handle to the form</returns>
        public int Activate()
        {
            if (templateMode)
            {
                // In template mode, set the form values using the RasterFunctionVariables
                myBandIndicesVar = null;
                myForm.InputRaster = myRasterVar;
                myForm.BandIndices = (string)myBandIndicesVar.Value;
            }
            else
            {
                // Otherwise use the arguments object to update the form values.
                myForm.InputRaster = myArgs.Raster;
                myForm.BandIndices = myArgs.BandIndices;
            }
            myForm.UpdateUI();
            myForm.Activate();
            return myForm.Handle.ToInt32();
        }

        /// <summary>
        /// Check if the form is applicable to the given set of objects. In this case
        /// only the Raster Function object is used to check compatibility.
        /// </summary>
        /// <param name="objects">Set of object to check against.</param>
        /// <returns>Flag to specify whether the form is applicable.</returns>
        public bool Applies(ESRI.ArcGIS.esriSystem.ISet objects)
        {
            objects.Reset();
            for (int i = 0; i < objects.Count; i++)
            {
                object currObject = objects.Next();
                if (currObject is IRasterFunction)
                {
                    IRasterFunction rasterFunction = (IRasterFunction)currObject;
                    if (rasterFunction is IPersistVariant)
                    {
                        IPersistVariant myVariantObject = (IPersistVariant)rasterFunction;
                        // Compare the ID from the function object with the ID's supported by this UI page.
                        if (myVariantObject.ID.Compare(mySupportedID))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Apply the properties set in the form to the arguments object.
        /// </summary>
        public void Apply()
        {
            if (!isFormReadOnly) // If the form is read only, do not update.
            {
                if (templateMode)
                {
                    // If in template mode, use the values from the page to
                    // update the variables.
                    if (myForm.InputRaster != null)
                    {
                        if (myForm.InputRaster is IRasterFunctionVariable)
                            myRasterVar = (IRasterFunctionVariable)myForm.InputRaster;
                        else
                            myRasterVar.Value = myForm.InputRaster;
                    }
                    myBandIndicesVar.Value = myForm.BandIndices;
                    // Then set the variables on the arguments object.
                    IRasterFunctionArguments rasterFunctionArgs =
                        (IRasterFunctionArguments)myArgs;
                    rasterFunctionArgs.PutValue("BandIndices", myBandIndicesVar);
                    rasterFunctionArgs.PutValue("Raster", myRasterVar);
                }
                else if (myArgs != null)
                {
                    // If not in template mode, update the arguments object
                    // with the values from the form.
                    myArgs.BandIndices = myForm.BandIndices;
                    if (myForm.InputRaster != null)
                        myArgs.Raster = myForm.InputRaster;
                }
            }

            myForm.IsFormDirty = false;
        }

        /// <summary>
        /// Do not set any properties set in the form
        /// </summary>
        public void Cancel()
        {
            myForm.IsFormDirty = false;
        }

        /// <summary>
        /// Shut down the form and destroy the object.
        /// </summary>
        public void Deactivate()
        {
            myForm.Close();
            myForm.Dispose();
            myForm = null;
        }

        /// <summary>
        /// Return the height of the form.
        /// </summary>
        public int Height
        {
            get { return myForm.Height; }
        }

        /// <summary>
        /// Returns the path to the helpfile associated with the form.
        /// </summary>
        public string HelpFile
        {
            get { return myHelpFile; }
        }

        /// <summary>
        /// Hide the form.
        /// </summary>
        public void Hide()
        {
            myForm.Hide();
        }

        /// <summary>
        /// Flag to specify if the form has been changed.
        /// </summary>
        public bool IsPageDirty
        {
            get { return myForm.IsFormDirty; }
        }

        /// <summary>
        /// Set the pagesite for the form.
        /// </summary>
        public IComPropertyPageSite PageSite
        {
            set { myPageSite = value; }
        }

        /// <summary>
        /// Get or set the priority for the form.
        /// </summary>
        public int Priority
        {
            get
            {
                return myPriority;
            }
            set
            {
                myPriority = value;
            }
        }

        /// <summary>
        /// Set the necessary objects required for the form. In this case
        /// the form is given an arguments object in edit mode, or is required 
        /// to create one in create mode. After getting or creating the arguments
        /// object, template mode is checked for and handled. The template mode 
        /// requires all parameters of the arguments object to converted to variables.
        /// </summary>
        /// <param name="objects">Set of objects required for the form.</param>
        public void SetObjects(ESRI.ArcGIS.esriSystem.ISet objects)
        {
            try
            {
                // Recurse through the objects
                objects.Reset();
                for (int i = 0; i < objects.Count; i++)
                {
                    object currObject = objects.Next();
                    // Find the properties to be set.
                    if (currObject is IPropertySet)
                    {
                        IPropertySet uiParameters = (IPropertySet)currObject;
                        object names, values;
                        uiParameters.GetAllProperties(out names, out values);

                        bool disableForm = false;
                        try { disableForm = Convert.ToBoolean(uiParameters.GetProperty("RFxPropPageIsReadOnly")); }
                        catch (Exception) { }

                        if (disableForm)
                            isFormReadOnly = true;
                        else
                            isFormReadOnly = false;

                        // Check if the arguments object exists in the property set.
                        object functionArgument = null;
                        try { functionArgument = uiParameters.GetProperty("RFxArgument"); }
                        catch (Exception) { }
                        // If not, the form is in create mode.
                        if (functionArgument == null)
                        {
                            #region Create Mode
                            // Create a new arguments object.
                            myArgs = new NDVICustomFunctionArguments();
                            // Create a new property and set the arguments object on it.
                            uiParameters.SetProperty("RFxArgument", myArgs);
                            // Check if a default raster is supplied.
                            object defaultRaster = null;
                            try { defaultRaster = uiParameters.GetProperty("RFxDefaultInputRaster"); }
                            catch (Exception) { }
                            if (defaultRaster != null) // If it is, set it to the raster property.
                                myArgs.Raster = defaultRaster;
                            // Check if the form is in template mode.
                            templateMode = (bool)uiParameters.GetProperty("RFxTemplateEditMode");
                            if (templateMode)
                            {
                                // Since we are in create mode already, new variables have to be 
                                // created for each property of the arguments object.
                                #region Create Variables
                                if (defaultRaster != null)
                                {
                                    // If a default raster is supplied and it is a variable,
                                    // there is no need to create one.
                                    if (defaultRaster is IRasterFunctionVariable)
                                        myRasterVar = (IRasterFunctionVariable)defaultRaster;
                                    else
                                    {
                                        // Create variable object for the InputRaster property.
                                        myRasterVar = new RasterFunctionVariableClass();
                                        myRasterVar.Value = defaultRaster;
                                        myRasterVar.Name = "InputRaster";
                                        myRasterVar.IsDataset = true;
                                    }
                                }

                                // Create a variable for the BandIndices property.
                                myBandIndicesVar = new RasterFunctionVariableClass();
                                myBandIndicesVar.Name = "BandIndices";
                                // Use the default value from the arguments object
                                myBandIndicesVar.Value = myArgs.BandIndices;

                                // Set the variables created as properties on the arguments object.
                                IRasterFunctionArguments rasterFunctionArgs =
                                    (IRasterFunctionArguments)myArgs;
                                rasterFunctionArgs.PutValue("Raster", myRasterVar);
                                rasterFunctionArgs.PutValue("BandIndices", myBandIndicesVar);
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region  Edit Mode
                            // Get the arguments object from the property set.
                            myArgs = (INDVICustomFunctionArguments)functionArgument;
                            // Check if the form is in template mode.
                            templateMode = (bool)uiParameters.GetProperty("RFxTemplateEditMode");
                            if (templateMode)
                            {
                                #region Edit Template
                                // In template edit mode, the variables from the arguments object
                                // are extracted.
                                IRasterFunctionArguments rasterFunctionArgs =
                                    (IRasterFunctionArguments)myArgs;
                                object raster = rasterFunctionArgs.GetValue("Raster");

                                // Create or Open the Raster variable.
                                if (raster is IRasterFunctionVariable)
                                    myRasterVar = (IRasterFunctionVariable)raster;
                                else
                                {
                                    myRasterVar = new RasterFunctionVariableClass();
                                    myRasterVar.Name = "InputRaster";
                                    myRasterVar.Value = raster;
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                string errorMsg = exc.Message;
            }
        }

        /// <summary>
        /// Show the form.
        /// </summary>
        public void Show()
        {
            if (isFormReadOnly)
                myForm.Enabled = false;
            myForm.Show();
        }

        /// <summary>
        /// Get or set the title of the form
        /// </summary>
        public string Title
        {
            get
            {
                return myForm.Text;
            }
            set
            {
                myForm.Text = value;
            }
        }

        /// <summary>
        /// Get the width of the form.
        /// </summary>
        public int Width
        {
            get { return myForm.Width; }
        }

        /// <summary>
        /// Return the help context ID of the form if it exists.
        /// </summary>
        /// <param name="controlID">Control ID for the sheet.</param>
        /// <returns>The context ID.</returns>
        public int get_HelpContextID(int controlID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region COM Registration Function(s)

        /// <summary>
        /// Register the Property Page with the Raster Function Property Pages
        /// </summary>
        /// <param name="regKey">Key to register.</param>
        [ComRegisterFunction()]
        static void Reg(string regKey)
        {
            RasterFunctionPropertyPages.Register(regKey);
        }

        /// <summary>
        /// Unregister the Property Page with the Raster Function Property Pages
        /// </summary>
        /// <param name="regKey">Key to unregister.</param>
        [ComUnregisterFunction()]
        static void Unreg(string regKey)
        {
            RasterFunctionPropertyPages.Unregister(regKey);
        }

        #endregion
    }
}
