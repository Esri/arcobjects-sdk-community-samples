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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Timestamper.Properties;

namespace Timestamper
{
	/// <summary>
	/// A custom feature class description for use with the TimestampClassExtension. The properties return
	/// nearly the same values as the FeatureClassDescription class, with the exception of the Name
	/// ("Timestamped Class") and DefaultFields (returns three additional fields, Date fields for features'
	/// creation and modification dates and a text field for a user name).
	/// </summary>
	[Guid("2198329f-69ba-43fa-a3ed-85ee11237bf2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("Timestamper.TimestampClassDescription")]
	[ComVisible(true)]
	public class TimestampedClassDescription : IObjectClassDescription, IFeatureClassDescription
	{
		#region IObjectClassDescription Members
		/// <summary>
		/// The alias of the described class.
		/// </summary>
		public String AliasName
		{
			get
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// The described class' extension UID.
		/// </summary>
		public UID ClassExtensionCLSID
		{
			get
			{
				// Return the UID of TimestampClassExtension.
				UID uid = new UIDClass();
				uid.Value = "{31b0b791-3606-4c58-b4d9-940c157dca4c}";
				return uid;
			}
		}

		/// <summary>
		/// The described class' instance UID.
		/// </summary>
		public UID InstanceCLSID
		{
			get
			{
				// Return the UID of Feature.
				UID uid = new UIDClass();
				uid.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
				return uid;
			}
		}

		/// <summary>
		/// The model name of the described class.
		/// </summary>
		public String ModelName
		{
			get
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Indicates if the model name of the described class is unique.
		/// </summary>
		public Boolean ModelNameUnique
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// The name of the described class.
		/// </summary>
		public String Name
		{
			get
			{
				return Resources.TimestampedClassName;
			}
		}

		/// <summary>
		/// The set of required fields for the described class.
		/// </summary>
		public IFields RequiredFields
		{
			get
			{
				// Get the feature class required fields.
				IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
				IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;
				IFields requiredFields = ocDescription.RequiredFields;
				IFieldsEdit requiredFieldsEdit = (IFieldsEdit)requiredFields;

				// Add a created date field.
				IField createdField = new FieldClass();
				IFieldEdit createdFieldEdit = (IFieldEdit)createdField;
				createdFieldEdit.Name_2 = Resources.DefaultCreatedField;
				createdFieldEdit.Required_2 = false;
				createdFieldEdit.IsNullable_2 = true;
				createdFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
				requiredFieldsEdit.AddField(createdField);

				// Add a modified date field.
				IField modifiedField = new FieldClass();
				IFieldEdit modifiedFieldEdit = (IFieldEdit)modifiedField;
				modifiedFieldEdit.Name_2 = Resources.DefaultModifiedField;
				modifiedFieldEdit.Required_2 = false;
				modifiedFieldEdit.IsNullable_2 = true;
				modifiedFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
				requiredFieldsEdit.AddField(modifiedField);

				// Add a user text field.
				IField userField = new FieldClass();
				IFieldEdit userFieldEdit = (IFieldEdit)userField;
				userFieldEdit.Name_2 = Resources.DefaultUserField;
				userFieldEdit.Required_2 = false;
				userFieldEdit.IsNullable_2 = true;
				userFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
				userFieldEdit.Length_2 = 100;
				requiredFieldsEdit.AddField(userField);

				return requiredFields;
			}
		}
		#endregion

		#region IFeatureClassDescription Members
		/// <summary>
		/// The esriFeatureType for the instances of the described class.
		/// </summary>
		public esriFeatureType FeatureType
		{
			get
			{
				return esriFeatureType.esriFTSimple;
			}
		}

		/// <summary>
		/// The name of the described class' geometry field.
		/// </summary>
		public String ShapeFieldName
		{
			get
			{
				// Use the feature class default.
				IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
				return fcDescription.ShapeFieldName;
			}
		}
		#endregion

		#region COM Registration Functions
		/// <summary>
		/// Registers the class description in the appropriate component category.
		/// </summary>
		/// <param name="registerType">The class description's type.</param>
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			String regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			GeoObjectClassDescriptions.Register(regKey);
		}

		/// <summary>
		/// Removes the class description from the appropriate component category.
		/// </summary>
		/// <param name="registerType">The class description's type.</param>
		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			String regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			GeoObjectClassDescriptions.Unregister(regKey);
		}
		#endregion
	}
}
