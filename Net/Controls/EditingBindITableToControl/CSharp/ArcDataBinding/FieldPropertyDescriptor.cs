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
using System.Text;
using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ArcDataBinding
{
  /// <summary>
  /// This class provides a PropertyDescriptor for a single field of an IRow
  /// </summary>
  /// <remarks>
  /// This class can be used by an ITypedList implementation to provide a property
  /// description for a single field in an ITable.
  /// </remarks>
  internal class FieldPropertyDescriptor: PropertyDescriptor
  {
    #region Private Members
    /// <summary>
    /// Store the index of the IField that this property descriptor describes
    /// </summary>
    private int wrappedFieldIndex;
    
    /// <summary>
    /// Store the .NET type of the value stored in the IField this property
    /// represents
    /// </summary>
    private Type netType;

    /// <summary>
    /// This is used to store the actual .NET type of a field that uses a CV
    /// domain. It retains the type allowing as to restore it when the UseCVDomain
    /// property is false;
    /// </summary>
    private Type actualType;
    
    /// <summary>
    /// Store the esri type of the value stored in the IField this property
    /// represents
    /// </summary>
    private esriFieldType esriType;
    
    /// <summary>
    /// Indicates whether this field is editable or not.
    /// </summary>
    /// <remarks>
    /// This will determined by looking at the Editable property of the IField
    /// and the type of the field. We currently don't support the editing of
    /// blob or geometry fields.
    /// </remarks>
    bool isEditable = true;

    /// <summary>
    /// Used to start and stop editing when adding/updating/deleting rows
    /// </summary>
    private IWorkspaceEdit wkspcEdit;

    /// <summary>
    /// The coded value domain for the field this instance represents, if any
    /// </summary>
    private ICodedValueDomain cvDomain;

    /// <summary>
    /// This will be true if we are currently using the string values for the
    /// coded value domain and false if we are using the numeric values.
    /// </summary>
    private bool useCVDomain;

    /// <summary>
    /// This type converter is used when the field this instance represents has
    /// a coded value domain and we are displaying the actual domain values
    /// </summary>
    private TypeConverter actualValueConverter;

    /// <summary>
    /// This type converter is used when the field this instance represents has
    /// a coded value domain and we are displaying the names of the domain values
    /// </summary>
    private TypeConverter cvDomainValDescriptionConverter;
    #endregion Private Members

    #region Construction/Destruction
    /// <summary>
    /// Initializes a new instance of the <see cref="FieldPropertyDescriptor"/> class.
    /// </summary>
    /// <param name="wrappedTable">The wrapped table.</param>
    /// <param name="fieldName">Name of the field within wrappedTable.</param>
    /// <param name="fieldIndex">Index of the field within wrappedTable.</param>
    public FieldPropertyDescriptor(ITable wrappedTable, string fieldName, int fieldIndex)
      : base(fieldName, null)
    {
      wrappedFieldIndex = fieldIndex;

      // Get the field this property will represent. We will use it to
      // get the field type and determine whether it can be edited or not. In
      // this case, editable means the field's editable property is true and it
      // is not a blob, geometry or raster field.
      IField wrappedField = wrappedTable.Fields.get_Field(fieldIndex);
      esriType = wrappedField.Type;
      isEditable = wrappedField.Editable && 
        (esriType != esriFieldType.esriFieldTypeBlob) &&
        (esriType != esriFieldType.esriFieldTypeRaster) &&
        (esriType != esriFieldType.esriFieldTypeGeometry);
      netType = actualType = EsriFieldTypeToSystemType(wrappedField);
      wkspcEdit = ((IDataset)wrappedTable).Workspace as IWorkspaceEdit;
    } 
    #endregion Construction/Destruction

    /// <summary>
    /// Gets a value indicating whether the field represented by this property 
    /// has a CV domain.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has a CV domain; otherwise, <c>false</c>.
    /// </value>
    public bool HasCVDomain
    {
      get
      {
        return null != cvDomain;
      }
    }

    /// <summary>
    /// Sets a value indicating whether [use CV domain].
    /// </summary>
    /// <value><c>true</c> if [use CV domain]; otherwise, <c>false</c>.</value>
    public bool UseCVDomain
    {
      set
      {
        useCVDomain = value;
        if (value)
        {
          // We want the property type for this field to be string
          netType = typeof(string);
        }
        else
        {
          // Restore the original type
          netType = actualType;
        }
      }
    }

    #region Public Overrides
    /// <summary>
    /// Gets the type converter for this property.
    /// </summary>
    /// <remarks>
    /// We need to override this property as the base implementation sets the
    /// converter once and reuses it as required. We can't do this if the field
    /// this instance represents has a coded value domain and we change from
    /// using the value to using the name or vice versa. The reason for this is
    /// that if we are displaying the domain name, we need a string converter and
    /// if we are displaying the domain value, we will need one of the numeric
    /// converters.
    /// </remarks>
    /// <returns>A <see cref="T:System.ComponentModel.TypeConverter"></see> 
    /// that is used to convert the <see cref="T:System.Type"></see> of this 
    /// property.</returns>
    /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, 
    /// mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" 
    /// version="1" Flags="UnmanagedCode"/></PermissionSet>
    public override TypeConverter Converter
    {
      get
      {
        TypeConverter retVal = null;

        if (null != cvDomain)
        {
          if (useCVDomain)
          {
            if (null == cvDomainValDescriptionConverter)
            {
              // We want a string converter
              cvDomainValDescriptionConverter = TypeDescriptor.GetConverter(typeof(string));
            }

            retVal = cvDomainValDescriptionConverter;
          }
          else
          {
            if (null == actualValueConverter)
            {
              // We want a converter for the type of this field's actual value
              actualValueConverter = TypeDescriptor.GetConverter(actualType);
            }

            retVal = actualValueConverter;
          }
        }
        else
        {
          // This field doesn't have a coded value domain, the base implementation
          // works fine.
          retVal = base.Converter;
        }

        return retVal;
      }
    }

    /// <summary>
    /// Returns whether resetting an object changes its value.
    /// </summary>
    /// <param name="component">The component to test for reset capability.
    /// This will be an IRow</param>
    /// <returns>
    /// true if resetting the component changes its value; otherwise, false.
    /// </returns>
    public override bool CanResetValue(object component)
    {
      return false;
    }

    /// <summary>
    /// Gets the type of the component this property is bound to.
    /// </summary>
    /// <value></value>
    /// <returns>A <see cref="T:System.Type"></see> that represents the type of 
    /// component this property is bound to. When the 
    /// <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)"></see> 
    /// or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)"></see> 
    /// methods are invoked, the object specified might be an instance of this type.</returns>
    public override Type ComponentType
    {
      get { return typeof(IRow); }
    }

    /// <summary>
    /// Gets the current value of the property on a component.
    /// </summary>
    /// <param name="component">The component (an IRow) with the property for 
    /// which to retrieve the value.</param>
    /// <remarks>
    /// This will return the field value for all fields apart from geometry, raster and Blobs.
    /// These fields will return the string equivalent of the geometry type.
    /// </remarks>
    /// <returns>
    /// The value of a property for a given component. This will be the value of
    /// the field this class instance represents in the IRow passed in the component
    /// parameter.
    /// </returns>
    public override object GetValue(object component)
    {
      object retVal = null;

      IRow givenRow = (IRow)component;
      try
      {
        // Get value
        object value = givenRow.get_Value(wrappedFieldIndex);

        if ((null != cvDomain) && useCVDomain)
        {
          value = cvDomain.get_Name(Convert.ToInt32(value));
        }

        switch (esriType)
        {
            case esriFieldType.esriFieldTypeBlob:
                retVal = "Blob";
                break;
            
            case esriFieldType.esriFieldTypeGeometry:
                retVal = GetGeometryTypeAsString(value);
                break;
            
            case esriFieldType.esriFieldTypeRaster:
                retVal = "Raster";
                break;
            
            default:
                retVal = value;
                break;
        }
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
      }

      return retVal;
    }

    /// <summary>
    /// Gets a value indicating whether this property is read-only or not.
    /// </summary>
    /// <value></value>
    /// <returns>true if the property is read-only; otherwise, false.</returns>
    public override bool IsReadOnly
    {
      get { return !isEditable; }
    }

    /// <summary>
    /// Gets the type of the property.
    /// </summary>
    /// <value></value>
    /// <returns>A <see cref="T:System.Type"></see> that represents the type 
    /// of the property.</returns>
    public override Type PropertyType
    {
      get { return netType; }
    }

    /// <summary>
    /// Resets the value for this property of the component to the default value.
    /// </summary>
    /// <param name="component">The component (an IRow) with the property value 
    /// that is to be reset to the default value.</param>
    public override void ResetValue(object component)
    {

    }

    /// <summary>
    /// Sets the value of the component to a different value.
    /// </summary>
    /// <remarks>
    /// If the field this instance represents does not have a coded value domain,
    /// this method simply sets the given value and stores the row within an edit
    /// operation. If the field does have a coded value domain, the method first
    /// needs to check that the given value is valid. If we are displaying the 
    /// coded values, the value passed to this method will be a string and we will
    /// need to see if it is one of the names in the cv domain. If we are not
    /// displaying the coded values, we will still need to check that the given
    /// value is within the domain. If the value is not within the domain, an
    /// error will be displayed and the method will return.
    /// Note that the string comparison is case sensitive.
    /// </remarks>
    /// <param name="component">The component (an IRow) with the property value 
    /// that is to be set.</param>
    /// <param name="value">The new value.</param>
    public override void SetValue(object component, object value)
    {
      IRow givenRow = (IRow)component;

      if (null != cvDomain)
      {
        // This field has a coded value domain
        if (!useCVDomain)
        {
          // Check value is valid member of the domain
          if (!((IDomain)cvDomain).MemberOf(value))
          {
            System.Windows.Forms.MessageBox.Show(string.Format(
              "Value {0} is not valid for coded value domain {1}", value.ToString(), ((IDomain)cvDomain).Name));
            return;
          }
        }
        else
        {
          // We need to convert the string value to one of the cv domain values
          // Loop through all the values until we, hopefully, find a match
          bool foundMatch = false;
          for (int valueCount = 0; valueCount < cvDomain.CodeCount; valueCount++)
          {
            if (value.ToString() == cvDomain.get_Name(valueCount))
            {
              foundMatch = true;
              value = valueCount;
              break;
            }
          }

          // Did we find a match?
          if (!foundMatch)
          {
            System.Windows.Forms.MessageBox.Show(string.Format(
              "Value {0} is not valid for coded value domain {1}", value.ToString(), ((IDomain)cvDomain).Name));
            return;
          }
        }
      }
      givenRow.set_Value(wrappedFieldIndex, value);

      // Start editing if we aren't already editing
      bool weStartedEditing = false;
      if (!wkspcEdit.IsBeingEdited())
      {
        wkspcEdit.StartEditing(false);
        weStartedEditing = true;
      }

      // Store change in an edit operation
      wkspcEdit.StartEditOperation();
      givenRow.Store();
      wkspcEdit.StopEditOperation();

      // Stop editing if we started here
      if (weStartedEditing)
      {
        wkspcEdit.StopEditing(true);
      }

    }

    /// <summary>
    /// When overridden in a derived class, determines a value indicating whether 
    /// the value of this property needs to be persisted.
    /// </summary>
    /// <param name="component">The component (an IRow) with the property to be examined for 
    /// persistence.</param>
    /// <returns>
    /// true if the property should be persisted; otherwise, false.
    /// </returns>
    public override bool ShouldSerializeValue(object component)
    {
      return false;
    } 
    #endregion Public Overrides

    #region Private Methods
    /// <summary>
    /// Converts the specified ESRI field type to a .NET type.
    /// </summary>
    /// <param name="esriType">The ESRI field type to be converted.</param>
    /// <returns>The appropriate .NET type.</returns>
    private Type EsriFieldTypeToSystemType(IField field)
    {
      esriFieldType esriType = field.Type;

      // Does this field have a domain?
      cvDomain = field.Domain as ICodedValueDomain;
      if ((null != cvDomain) && useCVDomain)
      {
        return typeof(string);
      }

      try
      {
        switch (esriType)
        {
          case esriFieldType.esriFieldTypeBlob:
            //beyond scope of sample to deal with blob fields
            return typeof(string);
          case esriFieldType.esriFieldTypeDate:
            return typeof(DateTime);
          case esriFieldType.esriFieldTypeDouble:
            return typeof(double);
          case esriFieldType.esriFieldTypeGeometry:
            return typeof(string);
          case esriFieldType.esriFieldTypeGlobalID:
            return typeof(string);
          case esriFieldType.esriFieldTypeGUID:
            return typeof(Guid);
          case esriFieldType.esriFieldTypeInteger:
            return typeof(Int32);
          case esriFieldType.esriFieldTypeOID:
            return typeof(Int32);
          case esriFieldType.esriFieldTypeRaster:
            //beyond scope of sample to correctly display rasters
            return typeof(string);
          case esriFieldType.esriFieldTypeSingle:
            return typeof(Single);
          case esriFieldType.esriFieldTypeSmallInteger:
            return typeof(Int16);
          case esriFieldType.esriFieldTypeString:
            return typeof(string);
          default:
            return typeof(string);
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return typeof(string);
      }
    }

    /// <summary>
    /// Gets the geometry type as string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The string equivalent of the geometry type</returns>
    private string GetGeometryTypeAsString(object value)
    {
      string retVal = "";
      IGeometry geometry = value as IGeometry;
      if (geometry != null)
      {
        retVal = geometry.GeometryType.ToString();
      }
      return retVal;
    }
    #endregion Private Methods
  }
}
