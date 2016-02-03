Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports ESRI.ArcGIS.Geodatabase

Namespace ArcDataBinding
    '<summary>
    'This class provides a wrapper for an ITable that allows it to be bound to
    'a .NET control.
    '</summary>
    '<remarks>
    'This class inherits from <see cref="BindingList"/> to provide a default
    'implementation of a list of objects that can be bound to a .NET control.
    'For the purposes of this sample, it is easier to use BindingList and add
    'IRows to it than it is to implement all the interfaces required for a 
    'bindable list. A more correct implementation would allow direct access to
    'the wrapped ITable rather than simply adding all of its rows to a list.
    'The class also implements <see cref="ITypedList"/> to allow a control to
    'query it for any properties required to correctly display the data in a 
    'control. Normally properties are determined by using reflection. We want
    'the individual fields in the given ITable to look like properties of an
    'IRow. As this is not the case, we need to create a collection of 'fake'
    'properties with one for each field in the ITable. This is contained in the
    'fakePropertiesList member and is used by the ITypedList implementation.
    '</remarks>
    <Guid("5a239147-b06a-49e5-aa1c-e47f81adc10e")> _
    <ClassInterface(ClassInterfaceType.None)> _
    <ProgId("ArcDataBinding.TableWrapper")> _
    Public Class TableWrapper
        Inherits BindingList(Of IRow)
        Implements ITypedList
#Region "Private Members"
        '<summary>
        'Reference to the table we are wrapping
        '</summary>
        Private wrappedTable As ITable

        '<summary>
        'This is a list of <see cref="PropertyDescriptor"/> instances with each one
        'representing one field of the wrapped ITable.
        '</summary>
        Private fakePropertiesList As List(Of PropertyDescriptor) = New List(Of PropertyDescriptor)

        '<summary>
        'Used to start and stop editing when adding/updating/deleting rows
        '</summary>
        Private wkspcEdit As IWorkspaceEdit
#End Region

#Region "Construction/Destruction"
        '<summary>
        'This constructor stores a reference to the wrapped ITable and uses it to
        'generate a list of properties before adding the ITable's data to the binding
        'list.
        '</summary>
        '<param name="tableToWrap">ITable that we wish to bind to .NET controls</param>
        Public Sub New(ByVal tableToWrap As ITable)
            wrappedTable = tableToWrap
            GenerateFakeProperties()
            AddData()
            wkspcEdit = DirectCast((DirectCast(wrappedTable, IDataset)).Workspace, IWorkspaceEdit)
            AllowNew = True
            AllowRemove = True
        End Sub
#End Region

#Region "ITypedList Members"

        '<summary>
        'Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection"></see> 
        'that represents the properties on each item used to bind data.
        '</summary>
        '<param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor"></see> 
        'objects to find in the collection as bindable. This can be null.</param>
        '<returns>
        'The <see cref="T:System.ComponentModel.PropertyDescriptorCollection"></see> 
        'that represents the properties on each item used to bind data.
        '</returns>
        Public Function GetItemProperties(ByVal listAccessors As PropertyDescriptor()) As PropertyDescriptorCollection Implements ITypedList.GetItemProperties
            Dim propCollection As PropertyDescriptorCollection = Nothing
            If (Nothing Is listAccessors) Then
                ' Return all properties
                propCollection = New PropertyDescriptorCollection(fakePropertiesList.ToArray())
            Else
                ' Return the requested properties by checking each item in listAccessors
                ' to make sure it exists in our property collection.
                Dim tempList As List(Of PropertyDescriptor) = New List(Of PropertyDescriptor)
                Dim curPropDesc As PropertyDescriptor
                For Each curPropDesc In listAccessors
                    If (fakePropertiesList.Contains(curPropDesc)) Then
                        tempList.Add(curPropDesc)
                    End If
                Next
                propCollection = New PropertyDescriptorCollection(tempList.ToArray())
            End If

            Return propCollection
        End Function

        '<summary>
        'Returns the name of the list.
        '</summary>
        '<param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor"></see> 
        'objects, the list name for which is returned. This can be null.</param>
        '<returns>The name of the list.</returns>
        Public Function GetListName(ByVal listAccessors As PropertyDescriptor()) As String Implements ITypedList.GetListName
            GetListName = (DirectCast(wrappedTable, IDataset)).Name
        End Function

#End Region

        Public WriteOnly Property UseCVDomains() As Boolean
            Set(ByVal Value As Boolean)
                Dim curPropDesc As FieldPropertyDescriptor
                For Each curPropDesc In fakePropertiesList
                    If (curPropDesc.HasCVDomain) Then
                        ' Field has a coded value domain so turn the usage of this on or off
                        ' as requested
                        curPropDesc.SetUseCVDomain = Value
                    End If
                Next
            End Set
        End Property

#Region "Protected Overrides"
        '<summary>
        'Raises the <see cref="E:System.ComponentModel.BindingList`1.AddingNew"></see> event.
        '</summary>
        '<remarks>
        'This override sets the NewObject property of the event arguments parameter
        'to be a new IRow.
        '</remarks>
        '<param name="e">An <see cref="T:System.ComponentModel.AddingNewEventArgs"></see> 
        'that contains the event data.</param>
        Protected Overrides Sub OnAddingNew(ByVal e As AddingNewEventArgs)
            ' Check that we can still add rows, this property could have been changed
            If (AllowNew) Then
                ' Need to create a new IRow
                Dim NewRow As IRow = wrappedTable.CreateRow()
                e.NewObject = NewRow

                ' Loop through fields and set default values
                Dim fieldCount As Integer
                For fieldCount = 0 To NewRow.Fields.FieldCount - 1 Step fieldCount + 1
                    Dim curField As IField = NewRow.Fields.Field(fieldCount)
                    If (curField.Editable) Then
                        NewRow.Value(fieldCount) = curField.DefaultValue
                    End If
                Next

                ' Save default values
                Dim weStartedEditing As Boolean = StartEditOp()
                NewRow.Store()
                StopEditOp(weStartedEditing)

                MyBase.OnAddingNew(e)
            End If
        End Sub

        '<summary>
        'Removes the item at the specified index.
        '</summary>
        '<remarks>
        'This override calls the Delete method of the IRow that is being removed
        '</remarks>
        '<param name="index">The zero-based index of the item to remove.</param>
        Protected Overrides Sub RemoveItem(ByVal index As Integer)
            ' Check that we can still delete rows, this property could have been changed
            If (AllowRemove) Then
                ' Get the corresponding IRow
                Dim itemToRemove As IRow = Items(index)

                Dim weStartedEditing As Boolean = StartEditOp()

                ' Delete the row
                itemToRemove.Delete()

                StopEditOp(weStartedEditing)

                MyBase.RemoveItem(index)
            End If
        End Sub
#End Region

#Region "Private Methods"
        '<summary>
        'Generates 'fake' properties.
        '</summary>
        '<remarks>
        'We need this method to create a list of properties for each field in the
        'ITable as an IRow does not have a property for each field.
        '</remarks>
        Private Sub GenerateFakeProperties()
            ' Loop through fields in wrapped table
            Dim fieldCount As Integer
            For fieldCount = 0 To wrappedTable.Fields.FieldCount - 1 Step fieldCount + 1
                ' Create a new property descriptor to represent the field
                Dim fieldName As String = wrappedTable.Fields.Field(fieldCount).Name.ToString()
                Dim newPropertyDesc As FieldPropertyDescriptor = New FieldPropertyDescriptor( _
                wrappedTable, fieldName, fieldCount)
                fakePropertiesList.Add(newPropertyDesc)
            Next
        End Sub

        '<summary>
        'Adds the data to the binding list.
        '</summary>
        '<remarks>
        'Note that this is a pretty inefficient way of accessing the data to be
        'bound to a control. If we implemented each of the interfaces required for
        'a bindable list rather than using BindingList, we could write code that
        'only reads rows from the ITable as they need to be displayed rather than
        'reading all of them.
        '</remarks>
        Private Sub AddData()
            ' Get a search cursor that returns all rows. Note we do not want to recycle
            ' the returned IRow otherwise all rows in the bound control will be identical
            ' to the last row read...
            Dim cur As ICursor = wrappedTable.Search(Nothing, False)
            Dim curRow As IRow = cur.NextRow()
            While Not (Nothing Is curRow)
                Add(curRow)
                curRow = cur.NextRow()
            End While
        End Sub

        '<summary>
        'Starts an edit operation.
        '</summary>
        '<remarks>
        'This method is used to start an edit operation before changing any data.
        'It checks to see if we are in an edit session or not and starts a new
        'one if appropriate. If we do start an edit session, the method will return
        'true to indicate that we started an edit session and should therefore also
        'stop it.
        '</remarks>
        '<returns>True if we started an edit session, false if we didn't</returns>
        Private Function StartEditOp() As Boolean
            StartEditOp = False

            ' Check to see if we're editing
            If (Not wkspcEdit.IsBeingEdited()) Then
                ' Not being edited so start here
                wkspcEdit.StartEditing(False)
                StartEditOp = True
            End If

            ' Start operation
            wkspcEdit.StartEditOperation()
        End Function

        '<summary>
        'Stops the edit operation.
        '</summary>
        '<remarks>
        'This method stops an edit operation started with a call to 
        '<see cref="StartEditOp"/>. If the weStartedEditing parameter is true, this
        'method will also end the edit session.
        '</remarks>
        '<param name="weStartedEditing">if set to <c>true</c> [we started editing].</param>
        Private Sub StopEditOp(ByVal weStartedEditing As Boolean)
            ' Stop edit operation
            wkspcEdit.StopEditOperation()

            If (weStartedEditing) Then
                ' We started the edit session so stop it here
                wkspcEdit.StopEditing(True)
            End If
        End Sub
#End Region
    End Class
End Namespace
