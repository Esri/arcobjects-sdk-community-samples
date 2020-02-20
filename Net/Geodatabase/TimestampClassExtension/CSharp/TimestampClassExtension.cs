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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Timestamper.Properties;

namespace Timestamper
{
	/// <summary>
	/// A feature class extension for timestamping features with creation dates, modification dates, and
	/// the name of the user who created or last modified the feature.
	/// </summary>
	[Guid("31b0b791-3606-4c58-b4d9-940c157dca4c")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("Timestamper.TimestampClassExtension")]
	[ComVisible(true)]
	public class TimestampClassExtension : IClassExtension, IObjectClassExtension, IFeatureClassExtension,
		IObjectClassEvents, IObjectClassInfo
	{
		#region Member Variables
		/// <summary>
		/// Provides a reference to the extension's class.
		/// </summary>
		private IClassHelper classHelper = null;

		/// <summary>
		/// The extension properties.
		/// </summary>
		private IPropertySet extensionProperties = null;

		/// <summary>
		/// The name of the "created" date field.
		/// </summary>
		private String createdFieldName = Resources.DefaultCreatedField;

		/// <summary>
		/// The position of the "created" date field.
		/// </summary>
		private int createdFieldIndex = -1;

		/// <summary>
		/// The name of the "modified" date field.
		/// </summary>
		private String modifiedFieldName = Resources.DefaultModifiedField;

		/// <summary>
		/// The position of the "modified" date field.
		/// </summary>
		private int modifiedFieldIndex = -1;

		/// <summary>
		/// The name of the "user" text field.
		/// </summary>
		private String userFieldName = Resources.DefaultUserField;

		/// <summary>
		/// The position of the "user" text field.
		/// </summary>
		private int userFieldIndex = -1;

		/// <summary>
		/// The length of the "user" text field.
		/// </summary>
		private int userFieldLength = 0;

		/// <summary>
		/// The name of the current user.
		/// </summary>
		private String userName = String.Empty;
		#endregion

		#region IClassExtension Methods
		/// <summary>
		/// Initializes the extension.
		/// </summary>
		/// <param name="classHelper">Provides a reference to the extension's class.</param>
		/// <param name="extensionProperties">A set of properties unique to the extension.</param>
		public void Init(IClassHelper classHelper, IPropertySet extensionProperties)
		{
			// Store the class helper as a member variable.
			this.classHelper = classHelper;
			IClass baseClass = classHelper.Class;

			// Get the names of the created and modified fields, if they exist.
			if (extensionProperties != null)
			{
				this.extensionProperties = extensionProperties;

				object createdObject = extensionProperties.GetProperty(Resources.CreatedFieldKey);
				object modifiedObject = extensionProperties.GetProperty(Resources.ModifiedFieldKey);
				object userObject = extensionProperties.GetProperty(Resources.UserFieldKey);

				// Make sure the properties exist and are strings.
				if (createdObject != null && createdObject is String)
				{
					createdFieldName = Convert.ToString(createdObject);
				}
				if (modifiedObject != null && modifiedObject is String)
				{
					modifiedFieldName = Convert.ToString(modifiedObject);
				}
				if (userObject != null && userObject is String)
				{
					userFieldName = Convert.ToString(userObject);
				}
			}
			else
			{
				// First time the extension has been run. Initialize with default values.
				InitNewExtension();
			}

			// Set the positions of the fields.
			SetFieldIndexes();

			// Set the current user name.
			userName = GetCurrentUser();
		}

		/// <summary>
		/// Informs the extension that the class is being disposed of.
		/// </summary>
		public void Shutdown()
		{
			classHelper = null;
		}
		#endregion

		#region IObjectClassEvents Methods
		/// <summary>
		/// Fired when an object's attributes or geometry is updated.
		/// </summary>
		/// <param name="obj">The updated object.</param>
		public void OnChange(IObject obj)
		{
			// Set the modified field's value to the current date and time.
			if (modifiedFieldIndex != -1)
			{
				obj.set_Value(modifiedFieldIndex, DateTime.Now);

				// Set the user field's value to the current user.
				if (userFieldIndex != -1)
				{
					obj.set_Value(userFieldIndex, userName);
				}
			}
		}

		/// <summary>
		/// Fired when a new object is created.
		/// </summary>
		/// <param name="obj">The new object.</param>
		public void OnCreate(IObject obj)
		{
			// Set the created field's value to the current date and time.
			if (createdFieldIndex != -1)
			{
				obj.set_Value(createdFieldIndex, DateTime.Now);
			}

			// Set the user field's value to the current user.
			if (userFieldIndex != -1)
			{
				obj.set_Value(userFieldIndex, userName);
			}
		}

		/// <summary>
		/// Fired when an object is deleted.
		/// </summary>
		/// <param name="obj">The deleted object.</param>
		public void OnDelete(IObject obj)
		{}
		#endregion

		#region IObjectClassInfo Methods
		/// <summary>
		/// Indicates if updates to objects can bypass the Store method and OnChange notifications for efficiency.
		/// </summary>
		/// <returns>False; this extension requires Store to be called.</returns>
		public Boolean CanBypassStoreMethod()
		{
			return false;
		}
		#endregion

		#region Public Members
		/// <summary>
		/// Changes the member variables and extension properties to store the provided field names
		/// as the created, modified and user fields (positions are also refreshed). Empty strings
		/// indicate the values should not be saved in a field.
		/// </summary>
		/// <param name="createdField">The name of the "created" field.</param>
		/// <param name="modifiedField">The name of the "modified" field.</param>
		/// <param name="userField">The name of the "user" field.</param>
		public void SetTimestampFields(String createdField, String modifiedField, String userField)
		{
			IClass baseClass = classHelper.Class;
			ISchemaLock schemaLock = (ISchemaLock)baseClass;
			try
			{
				// Get an exclusive lock. We want to do this prior to making any changes
				// to ensure the member variables and extension properties remain synchronized.
				schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);

				// Set the name member variables.
				createdFieldName = createdField;
				modifiedFieldName = modifiedField;
				userFieldName = userField;

				// Set the positions of the fields.
				SetFieldIndexes();

				// Modify the extension properties.
				extensionProperties.SetProperty(Resources.CreatedFieldKey, createdFieldName);
				extensionProperties.SetProperty(Resources.ModifiedFieldKey, modifiedFieldName);
				extensionProperties.SetProperty(Resources.UserFieldKey, userFieldName);

				// Change the properties.
				IClassSchemaEdit2 classSchemaEdit = (IClassSchemaEdit2)baseClass;
				classSchemaEdit.AlterClassExtensionProperties(extensionProperties);
			}
			catch (COMException comExc)
			{
				throw new Exception(Resources.FailedToSavePropertiesMsg, comExc);
			}
			finally
			{
				schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
			}
		}

		/// <summary>
		/// The field storing the creation date of features.
		/// </summary>
		public String CreatedField
		{
			get
			{
				return createdFieldName;
			}
		}

		/// <summary>
		/// The field storing the modification date of features.
		/// </summary>
		public String ModifiedField
		{
			get
			{
				return modifiedFieldName;
			}
		}

		/// <summary>
		/// The field storing the user who created or last modified the feature.
		/// </summary>
		public String UserField
		{
			get
			{
				return userFieldName;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// This method should be called the first time the extension is initialized, when the
		/// extension properties are null. This will create a new set of properties with the default
		/// field names.
		/// </summary>
		private void InitNewExtension()
		{
			// First time the extension has been run, initialize the extension properties.
			extensionProperties = new PropertySetClass();
			extensionProperties.SetProperty(Resources.CreatedFieldKey, createdFieldName);
			extensionProperties.SetProperty(Resources.ModifiedFieldKey, modifiedFieldName);
			extensionProperties.SetProperty(Resources.UserFieldKey, userFieldName);

			// Store the properties.
			IClass baseClass = classHelper.Class;
			IClassSchemaEdit2 classSchemaEdit = (IClassSchemaEdit2)baseClass;
			classSchemaEdit.AlterClassExtensionProperties(extensionProperties);
		}

		/// <summary>
		/// Gets the name of the extension's user. For local geodatabases, this is the username as known
		/// by the operating system (in a domain\username format). For remote geodatabases, the
		/// IDatabaseConnectionInfo interface is utilized.
		/// </summary>
		/// <returns>The name of the current user.</returns>
		private String GetCurrentUser()
		{
			// Get the base class' workspace.
			IClass baseClass = classHelper.Class;
			IDataset dataset = (IDataset)baseClass;
			IWorkspace workspace = dataset.Workspace;

			// If supported, use the IDatabaseConnectionInfo interface to get the username.
			IDatabaseConnectionInfo databaseConnectionInfo = workspace as IDatabaseConnectionInfo;
			if (databaseConnectionInfo != null)
			{
				String connectedUser = databaseConnectionInfo.ConnectedUser;

				// If the user name is longer than the user field allows, shorten it.
				if (connectedUser.Length > userFieldLength)
				{
					connectedUser = connectedUser.Substring(0, userFieldLength);
				}

				return connectedUser;
			}

			// Get the current Windows user.
			String userDomain = Environment.UserDomainName;
			String userName = Environment.UserName;
			String qualifiedUserName = String.Format(@"{0}\{1}", userDomain, userName);

			// If the user name is longer than the user field allows, shorten it.
			if (qualifiedUserName.Length > userFieldLength)
			{
				qualifiedUserName = qualifiedUserName.Substring(0, userFieldLength);
			}

			return qualifiedUserName;
		}

		/// <summary>
		/// Finds the positions of the created, modified and user fields, and verifies that
		/// the specified field has the correct data type.
		/// </summary>
		private void SetFieldIndexes()
		{
			// Get the base class from the class helper.
			IClass baseClass = classHelper.Class;

			// Find the indexes of the fields.
			createdFieldIndex = baseClass.FindField(createdFieldName);
			modifiedFieldIndex = baseClass.FindField(modifiedFieldName);
			userFieldIndex = baseClass.FindField(userFieldName);

			// Verify that the field data types are correct.
			IFields fields = baseClass.Fields;
			if (createdFieldIndex != -1)
			{
				IField createdField = fields.get_Field(createdFieldIndex);

				// If the "created" field is not a date field, do not use it.
				if (createdField.Type != esriFieldType.esriFieldTypeDate)
				{
					createdFieldIndex = -1;
				}
			}
			if (modifiedFieldIndex != -1)
			{
				IField modifiedField = fields.get_Field(modifiedFieldIndex);

				// If the "modified" field is not a date field, do not use it.
				if (modifiedField.Type != esriFieldType.esriFieldTypeDate)
				{
					modifiedFieldIndex = -1;
				}
			}
			if (userFieldIndex != -1)
			{
				IField userField = fields.get_Field(userFieldIndex);

				// If the "user" field is not a text field, do not use it.
				if (userField.Type != esriFieldType.esriFieldTypeString)
				{
					userFieldIndex = -1;
				}
				else
				{
					// Get the length of the text field.
					userFieldLength = userField.Length;
				}
			}
		}
		#endregion

		#region COM Registration Function(s)
		/// <summary>
		/// Registers the class extension in the appropriate component category.
		/// </summary>
		/// <param name="registerType">The class description's type.</param>
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			GeoObjectClassExtensions.Register(regKey);
		}

		/// <summary>
		/// Removes the class extension from the appropriate component category.
		/// </summary>
		/// <param name="registerType">The class description's type.</param>
		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			GeoObjectClassExtensions.Unregister(regKey);
		}
		#endregion
	}
}
