using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Geodatabase;

namespace AddInEditorExtension
{
    /// <summary>
    /// ValidateFeaturesExtension class implementing custom ESRI Editor Extension functionalities.
    /// </summary>
    public class ValidateFeaturesExtension : ESRI.ArcGIS.Desktop.AddIns.Extension
    {
        public ValidateFeaturesExtension()
        {
        }
        //Invoked when the Editor Extension is loaded
        protected override void OnStartup()
        {
            Events.OnStartEditing += new IEditEvents_OnStartEditingEventHandler(Events_OnStartEditing);
            Events.OnStopEditing += new IEditEvents_OnStopEditingEventHandler(Events_OnStopEditing);
            
        }
        //Invoked at the Start of editor session
        void Events_OnStartEditing()
        {
            //Since features of shapefiles, coverages etc cannot be validated, ignore wiring events for them
            if (ArcMap.Editor.EditWorkspace.Type != esriWorkspaceType.esriFileSystemWorkspace)
            {
                //wire OnCreateFeature Edit event
                Events.OnCreateFeature += new IEditEvents_OnCreateFeatureEventHandler(Events_OnCreateChangeFeature);
                //wire onChangeFeature Edit Event
                Events.OnChangeFeature += new IEditEvents_OnChangeFeatureEventHandler(Events_OnCreateChangeFeature);
            }
        }
        //Invoked at the end of Editor session (Editor->Stop Editing)
        void Events_OnStopEditing(bool Save)
        {
            //Since features of shapefiles, coverages etc cannot be validated, ignore wiring events for them
            if (ArcMap.Editor.EditWorkspace.Type != esriWorkspaceType.esriFileSystemWorkspace)
            {
                //unwire OnCreateFeature Edit event
                Events.OnCreateFeature -= new IEditEvents_OnCreateFeatureEventHandler(Events_OnCreateChangeFeature);
                //unwire onChangeFeature Edit Event
                Events.OnChangeFeature -= new IEditEvents_OnChangeFeatureEventHandler(Events_OnCreateChangeFeature);
            }
        }

        void Events_OnCreateChangeFeature(ESRI.ArcGIS.Geodatabase.IObject obj)
        {
            IFeature inFeature = (IFeature)obj;
            if (inFeature.Class is IValidation)
            {
                IValidate validate = (IValidate)inFeature;
                string errorMessage;
                //Validates connectivity rules, relationship rules, topology rules etc
                bool bIsvalid = validate.Validate(out errorMessage);
                if (!bIsvalid)
                {
                    System.Windows.Forms.MessageBox.Show("Invalid Feature\n\n" + errorMessage);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Valid Feature");
                }
            }       
        }

        protected override void OnShutdown()
        {
        }
        #region Editor Events

        #region Shortcut properties to the various editor event interfaces
        private IEditEvents_Event Events
        {
            get { return ArcMap.Editor as IEditEvents_Event; }
        }
        private IEditEvents2_Event Events2
        {
            get { return ArcMap.Editor as IEditEvents2_Event; }
        }
        private IEditEvents3_Event Events3
        {
            get { return ArcMap.Editor as IEditEvents3_Event; }
        }
        private IEditEvents4_Event Events4
        {
            get { return ArcMap.Editor as IEditEvents4_Event; }
        }
        #endregion

        void WireEditorEvents()
        {
            //
            //  TODO: Sample code demonstrating editor event wiring
            //
            Events.OnCurrentTaskChanged += delegate
            {
                if (ArcMap.Editor.CurrentTask != null)
                    System.Windows.Forms.MessageBox.Show(ArcMap.Editor.CurrentTask.Name);
            };
            Events2.BeforeStopEditing += delegate(bool save) { OnBeforeStopEditing(save); };
        }

        void OnBeforeStopEditing(bool save)
        {
        }
        #endregion

    }
}
