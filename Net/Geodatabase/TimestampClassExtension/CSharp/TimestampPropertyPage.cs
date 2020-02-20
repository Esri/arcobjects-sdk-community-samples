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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using Timestamper.Properties;

namespace Timestamper
{
	/// <summary>
	/// A property page for object classes extended with the TimestampClassExtension.
	/// </summary>
	[Guid("a77c1137-bbca-4e6b-863a-0fad7e3fc4a3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("Timestamper.TimestampPropertyPage")]
	[ComVisible(true)]
	public partial class TimestampPropertyPage : UserControl, IComPropertyPage
	{
		#region Member Variables
		/// <summary>
		/// Indicates whether the combo boxes have been changed.
		/// </summary>
		private Boolean dirtyFlag = false;

		/// <summary>
		/// The dialog box containing the page.
		/// </summary>
		private IComPropertyPageSite pageSite = null;

		/// <summary>
		/// The extension the property page refers to.
		/// </summary>
		private TimestampClassExtension timestampClassExtension = null;

		/// <summary>
		/// The class the extension is associated with.
		/// </summary>
		private IObjectClass objectClass = null;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		public TimestampPropertyPage()
		{
			InitializeComponent();

			// Set all combo boxes to "Not Used" by default.
			cmbCreatedField.SelectedIndex = 0;
			cmbModifiedField.SelectedIndex = 0;
			cmbUserField.SelectedIndex = 0;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Fired by a change to the combo boxes.
		/// </summary>
		/// <param name="eventSource">The source of the event.</param>
		/// <param name="arguments">Event arguments.</param>
		private void SetDirty(object eventSource, EventArgs arguments)
		{
			dirtyFlag = true;

			if (pageSite != null && this.Visible)
			{
				pageSite.PageChanged();
			}
		}

		/// <summary>
		/// Load valid date and text fields from the class.
		/// </summary>
		private void LoadValidFields()
		{
			// Create lists for storing Date and Text fields.
			List<String> dateFieldNames = new List<String>();
			List<String> textFieldNames = new List<String>();

			// Iterate through the class' fields.
			IFields fields = objectClass.Fields;
			for (int i = 0; i < fields.FieldCount; i++)
			{
				IField field = fields.get_Field(i);
				if (field.Type == esriFieldType.esriFieldTypeDate)
				{
					dateFieldNames.Add(field.Name);
				}
				if (field.Type == esriFieldType.esriFieldTypeString)
				{
					textFieldNames.Add(field.Name);
				}
			}

			// Add the valid fields to each combo box.
			foreach (String dateFieldName in dateFieldNames)
			{
				cmbCreatedField.Items.Add(dateFieldName);
				cmbModifiedField.Items.Add(dateFieldName);
			}
			foreach (String textFieldName in textFieldNames)
			{
				cmbUserField.Items.Add(textFieldName);
			}
		}
		#endregion

		#region IComPropertyPage Members
		/// <summary>
		/// Occurs on page creation.
		/// </summary>
		/// <returns>The handle of the page.</returns>
		int IComPropertyPage.Activate()
		{
			if (timestampClassExtension != null)
			{
				// Load the potential date/text fields from the class.
				LoadValidFields();

				// Set the combo boxes selected indexes to the correct positions.
				if (cmbCreatedField.Items.Contains(timestampClassExtension.CreatedField))
				{
					cmbCreatedField.SelectedItem = timestampClassExtension.CreatedField;
				}
				if (cmbModifiedField.Items.Contains(timestampClassExtension.ModifiedField))
				{
					cmbModifiedField.SelectedItem = timestampClassExtension.ModifiedField;
				}
				if (cmbUserField.Items.Contains(timestampClassExtension.UserField))
				{
					cmbUserField.SelectedItem = timestampClassExtension.UserField;
				}

				// Register the event handler with the combo boxes.
				cmbCreatedField.SelectedIndexChanged += new EventHandler(SetDirty);
				cmbModifiedField.SelectedIndexChanged += new EventHandler(SetDirty);
				cmbUserField.SelectedIndexChanged += new EventHandler(SetDirty);
			}

			return this.Handle.ToInt32();
		}

		/// <summary>
		/// Indicates if the page applies to the specified objects.
		/// Do not hold on to the objects here.
		/// </summary>
		bool IComPropertyPage.Applies(ISet objects)
		{
			// Should only apply if a single class is selected.
			if (objects == null || objects.Count != 1)
				return false;

			// Check whether the provided object is an object class with the TimestampClassExtension.
			Boolean isApplicable = false;
			objects.Reset();
			object providedObject = objects.Next();
			IObjectClass objectClass = providedObject as IObjectClass;
			if (objectClass != null)
			{
				// Get the object class' extension.
				object classExtension = objectClass.Extension;
				if (classExtension is TimestampClassExtension)
				{
					isApplicable = true;
				}
			}

			return isApplicable;
		}

		/// <summary>
		/// Applies any changes to the class extension.
		/// </summary>
		void IComPropertyPage.Apply()
		{
			if (dirtyFlag)
			{
				try
				{
					if (timestampClassExtension != null)
					{
						// Get the field names from the combo boxes.
						String createdField = Convert.ToString(cmbCreatedField.SelectedItem);
						String modifiedField = Convert.ToString(cmbModifiedField.SelectedItem);
						String userField = Convert.ToString(cmbUserField.SelectedItem);
						
						// Check if any of the fields are set to "Not Used".
						if (cmbCreatedField.SelectedIndex == 0)
						{
							createdField = "";
						}
						if (cmbModifiedField.SelectedIndex == 0)
						{
							modifiedField = "";
						}
						if (cmbUserField.SelectedIndex == 0)
						{
							userField = "";
						}

						// Set the field names on the extension.
						timestampClassExtension.SetTimestampFields(createdField, modifiedField, userField);
					}
				}
				catch (Exception exc)
				{
					MessageBox.Show(String.Format("{0}{1}{2}", Resources.PropertyPageApplyErrorMsg,
						Environment.NewLine, exc.Message));
				}
			}
		}

		/// <summary>
		/// Cancels any changes to the class extension.
		/// </summary>
		void IComPropertyPage.Cancel()
		{
			dirtyFlag = false;
		}

		/// <summary>
		/// Destroys the page.
		/// </summary>
		void IComPropertyPage.Deactivate()
		{
			this.Dispose(true);
		}

		/// <summary>
		/// The height of the page in pixels.
		/// </summary>
		int IComPropertyPage.Height
		{
			get { return this.Height; }
		}

		/// <summary>
		/// The help context ID for the specified control on the page.
		/// </summary>
		/// <param name="controlID">The control ID.</param>
		/// <returns>In this case, 0, indicating no help context.</returns>
		int IComPropertyPage.get_HelpContextID(int controlID)
		{
			return 0;
		}

		/// <summary>
		/// The help file for the page (none).
		/// </summary>
		String IComPropertyPage.HelpFile
		{
			get { return string.Empty; }
		}

		/// <summary>
		/// Hides the page.
		/// </summary>
		void IComPropertyPage.Hide()
		{
			// No need to do anything here.
		}

		/// <summary>
		/// Indicates if any changes have been made to the page.
		/// </summary>
		Boolean IComPropertyPage.IsPageDirty
		{
			get { return dirtyFlag; }
		}

		/// <summary>
		/// The sheet that contains the page.
		/// </summary>
		IComPropertyPageSite IComPropertyPage.PageSite
		{
			set
			{
				pageSite = value;
			}
		}

		/// <summary>
		/// The page priority.
		/// </summary>
		int IComPropertyPage.Priority
		{
			get
			{
				// Low-priority.
				return 0;
			}
			set
			{
				// Do nothing.
			}
		}

		/// <summary>
		/// Supplies the page with the object(s) to be edited.
		/// </summary>
		/// <param name="objects">The object(s) this page applies to.</param>
		void IComPropertyPage.SetObjects(ISet objects)
		{
			if (objects == null || objects.Count != 1)
				return;

			// Store the provided object class in a member variable.
			objects.Reset();
			object providedObject = objects.Next();
			objectClass = providedObject as IObjectClass;
			if (objectClass != null)
			{
				// Get the object class' extension.
				timestampClassExtension = objectClass.Extension as TimestampClassExtension;
			}
		}

		/// <summary>
		/// Shows the page.
		/// </summary>
		void IComPropertyPage.Show()
		{
			// No need to do anything here.
		}

		/// <summary>
		/// The title of the property page.
		/// </summary>
		string IComPropertyPage.Title
		{
			get
			{
				return Resources.PropertyPageTitle;
			}
			set
			{
				// Do nothing.
			}
		}

		/// <summary>
		/// The width of the page in pixels.
		/// </summary>
		int IComPropertyPage.Width
		{
			get { return this.Width; }
		}
		#endregion

		#region COM Registration Functions
		/// <summary>
		/// Registers the property page in the appropriate component categories.
		/// </summary>
		/// <param name="registerType">The class description's type.</param>
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			TablePropertyPages.Register(regKey);
			FeatureClassPropertyPages.Register(regKey);
		}

		/// <summary>
		/// Removes the property page from the appropriate component categories.
		/// </summary>
		/// <param name="registerType">The class description's type.</param>
		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			TablePropertyPages.Unregister(regKey);
			FeatureClassPropertyPages.Unregister(regKey);
		}
		#endregion
	}
}
