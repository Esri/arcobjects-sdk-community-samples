using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;

namespace ArcDataBinding
{
  /// <summary>
  /// This class provides a wrapper for an ITable that allows it to be bound to
  /// a .NET control.
  /// </summary>
  /// <remarks>
  /// This class inherits from <see cref="BindingList"/> to provide a default
  /// implementation of a list of objects that can be bound to a .NET control.
  /// For the purposes of this sample, it is easier to use BindingList and add
  /// IRows to it than it is to implement all the interfaces required for a 
  /// bindable list. A more correct implementation would allow direct access to
  /// the wrapped ITable rather than simply adding all of its rows to a list.
  /// The class also implements <see cref="ITypedList"/> to allow a control to
  /// query it for any properties required to correctly display the data in a 
  /// control. Normally properties are determined by using reflection. We want
  /// the individual fields in the given ITable to look like properties of an
  /// IRow. As this is not the case, we need to create a collection of 'fake'
  /// properties with one for each field in the ITable. This is contained in the
  /// fakePropertiesList member and is used by the ITypedList implementation.
  /// </remarks>
  [Guid("5a239147-b06a-49e5-aa1c-e47f81adc10e")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("ArcDataBinding.TableWrapper")]
  public class TableWrapper: BindingList<IRow>, ITypedList
  {
    #region Private Members
    /// <summary>
    /// Reference to the table we are wrapping
    /// </summary>
    private ITable wrappedTable;
    
    /// <summary>
    /// This is a list of <see cref="PropertyDescriptor"/> instances with each one
    /// representing one field of the wrapped ITable.
    /// </summary>
    private List<PropertyDescriptor> fakePropertiesList = new List<PropertyDescriptor>();
    
    /// <summary>
    /// Used to start and stop editing when adding/updating/deleting rows
    /// </summary>
    private IWorkspaceEdit wkspcEdit;
    #endregion Private Members

    #region Construction/Destruction
    /// <summary>
    /// This constructor stores a reference to the wrapped ITable and uses it to
    /// generate a list of properties before adding the ITable's data to the binding
    /// list.
    /// </summary>
    /// <param name="tableToWrap">ITable that we wish to bind to .NET controls</param>
    public TableWrapper(ITable tableToWrap)
    {
      wrappedTable = tableToWrap;
      GenerateFakeProperties();
      AddData();
      wkspcEdit = ((IDataset)wrappedTable).Workspace as IWorkspaceEdit;
      AllowNew = true;
      AllowRemove = true;
    }
    #endregion Construction/Destruction

    #region ITypedList Members

    /// <summary>
    /// Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection"></see> 
    /// that represents the properties on each item used to bind data.
    /// </summary>
    /// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor"></see> 
    /// objects to find in the collection as bindable. This can be null.</param>
    /// <returns>
    /// The <see cref="T:System.ComponentModel.PropertyDescriptorCollection"></see> 
    /// that represents the properties on each item used to bind data.
    /// </returns>
    public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
    {
      PropertyDescriptorCollection propCollection = null;
      if (null == listAccessors)
      {
        // Return all properties
        propCollection = new PropertyDescriptorCollection(fakePropertiesList.ToArray());
      }
      else
      {
        // Return the requested properties by checking each item in listAccessors
        // to make sure it exists in our property collection.
        List<PropertyDescriptor> tempList = new List<PropertyDescriptor>();
        foreach (PropertyDescriptor curPropDesc in listAccessors)
        {
          if (fakePropertiesList.Contains(curPropDesc))
          {
            tempList.Add(curPropDesc);
          }
        }
        propCollection = new PropertyDescriptorCollection(tempList.ToArray());
      }

      return propCollection;
    }

    /// <summary>
    /// Returns the name of the list.
    /// </summary>
    /// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor"></see> 
    /// objects, the list name for which is returned. This can be null.</param>
    /// <returns>The name of the list.</returns>
    public string GetListName(PropertyDescriptor[] listAccessors)
    {
      return ((IDataset)wrappedTable).Name;
    }

    #endregion ITypedList Members

    public bool UseCVDomains
    {
      set
      {
        foreach (FieldPropertyDescriptor curPropDesc in fakePropertiesList)
        {
          if (curPropDesc.HasCVDomain)
          {
            // Field has a coded value domain so turn the usage of this on or off
            // as requested
            curPropDesc.UseCVDomain = value;
          }
        }
      }
    }

    #region Protected Overrides
    /// <summary>
    /// Raises the <see cref="E:System.ComponentModel.BindingList`1.AddingNew"></see> event.
    /// </summary>
    /// <remarks>
    /// This override sets the NewObject property of the event arguments parameter
    /// to be a new IRow.
    /// </remarks>
    /// <param name="e">An <see cref="T:System.ComponentModel.AddingNewEventArgs"></see> 
    /// that contains the event data.</param>
    protected override void OnAddingNew(AddingNewEventArgs e)
    {
      // Check that we can still add rows, this property could have been changed
      if (AllowNew)
      {
        // Need to create a new IRow
        IRow newRow = wrappedTable.CreateRow();
        e.NewObject = newRow;

        // Loop through fields and set default values
        for (int fieldCount = 0; fieldCount < newRow.Fields.FieldCount; fieldCount++)
        {
          IField curField = newRow.Fields.get_Field(fieldCount);
          if (curField.Editable)
          {
            newRow.set_Value(fieldCount, curField.DefaultValue);
          }
        }

        // Save default values
        bool weStartedEditing = StartEditOp();
        newRow.Store();
        StopEditOp(weStartedEditing);

        base.OnAddingNew(e);
      }
    }

    /// <summary>
    /// Removes the item at the specified index.
    /// </summary>
    /// <remarks>
    /// This override calls the Delete method of the IRow that is being removed
    /// </remarks>
    /// <param name="index">The zero-based index of the item to remove.</param>
    protected override void RemoveItem(int index)
    {
      // Check that we can still delete rows, this property could have been changed
      if (AllowRemove)
      {
        // Get the corresponding IRow
        IRow itemToRemove = Items[index];

        bool weStartedEditing = StartEditOp();

        // Delete the row
        itemToRemove.Delete();

        StopEditOp(weStartedEditing);

        base.RemoveItem(index);
      }
    }
    #endregion Protected Overrides

    #region Private Methods
    /// <summary>
    /// Generates 'fake' properties.
    /// </summary>
    /// <remarks>
    /// We need this method to create a list of properties for each field in the
    /// ITable as an IRow does not have a property for each field.
    /// </remarks>
    private void GenerateFakeProperties()
    {
      // Loop through fields in wrapped table
      for (int fieldCount = 0; fieldCount < wrappedTable.Fields.FieldCount; fieldCount++)
      {
        // Create a new property descriptor to represent the field
        FieldPropertyDescriptor newPropertyDesc = new FieldPropertyDescriptor(
          wrappedTable, wrappedTable.Fields.get_Field(fieldCount).Name, fieldCount);
        fakePropertiesList.Add(newPropertyDesc);
      }

    }

    /// <summary>
    /// Adds the data to the binding list.
    /// </summary>
    /// <remarks>
    /// Note that this is a pretty inefficient way of accessing the data to be
    /// bound to a control. If we implemented each of the interfaces required for
    /// a bindable list rather than using BindingList, we could write code that
    /// only reads rows from the ITable as they need to be displayed rather than
    /// reading all of them.
    /// </remarks>
    private void AddData()
    {
      // Get a search cursor that returns all rows. Note we do not want to recycle
      // the returned IRow otherwise all rows in the bound control will be identical
      // to the last row read...
      ICursor cur = wrappedTable.Search(null, false);
      IRow curRow = cur.NextRow();
      while (null != curRow)
      {
        Add(curRow);
        curRow = cur.NextRow();
      }
    }

    /// <summary>
    /// Starts an edit operation.
    /// </summary>
    /// <remarks>
    /// This method is used to start an edit operation before changing any data.
    /// It checks to see if we are in an edit session or not and starts a new
    /// one if appropriate. If we do start an edit session, the method will return
    /// true to indicate that we started an edit session and should therefore also
    /// stop it.
    /// </remarks>
    /// <returns>True if we started an edit session, false if we didn't</returns>
    private bool StartEditOp()
    {
      bool retVal = false;

      // Check to see if we're editing
      if (!wkspcEdit.IsBeingEdited())
      {
        // Not being edited so start here
        wkspcEdit.StartEditing(false);
        retVal = true;
      }

      // Start operation
      wkspcEdit.StartEditOperation();
      return retVal;
    }

    /// <summary>
    /// Stops the edit operation.
    /// </summary>
    /// <remarks>
    /// This method stops an edit operation started with a call to 
    /// <see cref="StartEditOp"/>. If the weStartedEditing parameter is true, this
    /// method will also end the edit session.
    /// </remarks>
    /// <param name="weStartedEditing">if set to <c>true</c> [we started editing].</param>
    private void StopEditOp(bool weStartedEditing)
    {
      // Stop edit operation
      wkspcEdit.StopEditOperation();

      if (weStartedEditing)
      {
        // We started the edit session so stop it here
        wkspcEdit.StopEditing(true);
      }
    }
    #endregion Private Methods
  }
}
