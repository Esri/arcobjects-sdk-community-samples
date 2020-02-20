'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.GeomeTry

Namespace ArcDataBinding
    '<summary>
    'This class provides a PropertyDescriptor for a single field of an IRow
    '</summary>
    '<remarks>
    'This class can be used by an ITypedList implementation to provide a property
    'description for a single field in an ITable.
    '</remarks>
    Friend Class FieldPropertyDescriptor
        Inherits PropertyDescriptor
#Region "Private Members"
        '<summary>
        'Store the index of the IField that this property descriptor describes
        '</summary>
        Private wrappedFieldIndex As Integer

        '<summary>
        'Store the .NET type of the value stored in the IField this property
        'represents
        '</summary>
        Private netType As Type

        '<summary>
        'This is used to store the actual .NET type of a field that uses a CV
        'domain. It retains the type allowing as to restore it when the UseCVDomain
        'property is false;
        '</summary>
        Private actualType As Type

        '<summary>
        'Store the esri type of the value stored in the IField this property
        'represents
        '</summary>
        Private esriType As esriFieldType

        '<summary>
        'Indicates whether this field is editable or not.
        '</summary>
        '<remarks>
        'This will determined by looking at the Editable property of the IField
        'and the type of the field. We currently don't support the editing of
        'blob or geometry fields.
        '</remarks>
        Dim isEditable As Boolean = True

        '<summary>
        'Used to start and stop editing when adding/updating/deleting rows
        '</summary>
        Private wkspcEdit As IWorkspaceEdit

        '<summary>
        'The coded value domain for the field this instance represents, if any
        '</summary>
        Private cvDomain As ICodedValueDomain

        '<summary>
        'This will be true if we are currently using the string values for the
        'coded value domain and false if we are using the numeric values.
        '</summary>
        Private useCVDomain As Boolean

        '<summary>
        'This type converter is used when the field this instance represents has
        'a coded value domain and we are displaying the actual domain values
        '</summary>
        Private actualValueConverter As TypeConverter

        '<summary>
        'This type converter is used when the field this instance represents has
        'a coded value domain and we are displaying the names of the domain values
        '</summary>
        Private cvDomainValDescriptionConverter As TypeConverter
#End Region

#Region "Construction/Destruction"
        '<summary>
        'Initializes a new instance of the <see cref="FieldPropertyDescriptor"/> class.
        '</summary>
        '<param name="wrappedTable">The wrapped table.</param>
        '<param name="fieldName">Name of the field within wrappedTable.</param>
        '<param name="fieldIndex">Index of the field within wrappedTable.</param>
        Public Sub New(ByVal wrappedTable As ITable, ByVal fieldName As String, ByVal fieldIndex As Integer)
            : MyBase.New(fieldName, Nothing)

            wrappedFieldIndex = fieldIndex

            'Get the field this property will represent. We will use it to
            'get the field type and determine whether it can be edited or not. In
            'this case, editable means the field's editable property is true and it
            'is not a blob, geometry or raster field.
            Dim wrappedField As IField = DirectCast(wrappedTable.Fields.Field(fieldIndex), IField)
            esriType = wrappedField.Type
            isEditable = wrappedField.Editable AndAlso _
              (esriType <> esriFieldType.esriFieldTypeBlob) AndAlso _
              (esriType <> esriFieldType.esriFieldTypeRaster) AndAlso _
              (esriType <> esriFieldType.esriFieldTypeGeometry)
            actualType = EsriFieldTypeToSystemType(wrappedField)
            netType = actualType
            wkspcEdit = DirectCast((DirectCast(wrappedTable, IDataset)).Workspace, IWorkspaceEdit)
        End Sub

#End Region

        '<summary>
        'Gets a value indicating whether the field represented by this property 
        'has a CV domain.
        '</summary>
        '<value>
        '	<c>true</c> if this instance has a CV domain; otherwise, <c>false</c>.
        '</value>
        Public ReadOnly Property HasCVDomain() As Boolean
            Get
                HasCVDomain = Not (Nothing Is cvDomain)
            End Get
        End Property

        '<summary>
        'Sets a value indicating whether [use CV domain].
        '</summary>
        '<value><c>true</c> if [use CV domain]; otherwise, <c>false</c>.</value>
        Public WriteOnly Property SetUseCVDomain() As Boolean
            Set(ByVal Value As Boolean)
                useCVDomain = Value
                If (Value) Then
                    ' We want the property type for this field to be string
                    netType = GetType(String)
                Else
                    ' Restore the original type
                    netType = actualType
                End If
            End Set
        End Property

#Region "Public Overrides"
        '<summary>
        'Gets the type converter for this property.
        '</summary>
        '<remarks>
        'We need to override this property as the base implementation sets the
        'converter once and reuses it as required. We can't do this if the field
        'this instance represents has a coded value domain and we change from
        'using the value to using the name or vice versa. The reason for this is
        'that if we are displaying the domain name, we need a string converter and
        'if we are displaying the domain value, we will need one of the numeric
        'converters.
        '</remarks>
        '<returns>A <see cref="T:System.ComponentModel.TypeConverter"></see> 
        'that is used to convert the <see cref="T:System.Type"></see> of this 
        'property.</returns>
        '<PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, 
        'mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" 
        'version="1" Flags="UnmanagedCode"/></PermissionSet>
        Public Overrides ReadOnly Property Converter() As TypeConverter
            Get
                Dim retVal As TypeConverter = Nothing

                If Not (Nothing Is cvDomain) Then

                    If (useCVDomain) Then
                        If (Nothing Is cvDomainValDescriptionConverter) Then
                            ' We want a string converter
                            cvDomainValDescriptionConverter = TypeDescriptor.GetConverter(GetType(String))
                        End If

                        retVal = cvDomainValDescriptionConverter
                    Else
                        If (Nothing Is actualValueConverter) Then
                            ' We want a converter for the type of this field's actual value
                            actualValueConverter = TypeDescriptor.GetConverter(actualType)
                        End If

                        retVal = actualValueConverter
                    End If

                Else

                    ' This field doesn't have a coded value domain, the base implementation
                    ' works fine.
                    retVal = MyBase.Converter
                End If

                Converter = retVal
            End Get
        End Property

        '<summary>
        'Returns whether resetting an object changes its value.
        '</summary>
        '<param name="component">The component to test for reset capability.
        'This will be an IRow</param>
        '<returns>
        'true if resetting the component changes its value; otherwise, false.
        '</returns>
        Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
            CanResetValue = False
        End Function

        '<summary>
        'Gets the type of the component this property is bound to.
        '</summary>
        '<value></value>
        '<returns>A <see cref="T:System.Type"></see> that represents the type of 
        'component this property is bound to. When the 
        '<see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)"></see> 
        'or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)"></see> 
        'methods are invoked, the object specified might be an instance of this type.</returns>
        Public Overrides ReadOnly Property ComponentType() As Type
            Get
                ComponentType = GetType(IRow)
            End Get
        End Property

        '<summary>
        'Gets the current value of the property on a component.
        '</summary>
        '<param name="component">The component (an IRow) with the property for 
        'which to retrieve the value.</param>
        '<remarks>
        'This will return the field value for all fields apart from geometry, raster and Blobs.
        'These fields will return the string equivalent of the geometry type.
        '</remarks>
        '<returns>
        'The value of a property for a given component. This will be the value of
        'the field this class instance represents in the IRow passed in the component
        'parameter.
        '</returns>
        Public Overrides Function GetValue(ByVal component As Object) As Object
            Dim retVal As Object = Nothing

            Dim givenRow As IRow = DirectCast(component, IRow)
            Try
                ' Get value
                Dim value As Object = givenRow.Value(wrappedFieldIndex)

                If (Not (Nothing Is cvDomain) AndAlso useCVDomain) Then
                    value = cvDomain.Name(Convert.ToInt32(value))
                End If


                Select Case esriType
                    Case esriFieldType.esriFieldTypeBlob
                        retVal = "Blob"
                        Exit Select
                    Case esriFieldType.esriFieldTypeGeometry

                        retVal = GetGeomeTryTypeAsString(value)
                        Exit Select
                    Case esriFieldType.esriFieldTypeRaster

                        retVal = "Raster"
                        Exit Select
                    Case Else

                        retVal = value
                        Exit Select
                End Select


            Catch e As Exception
                System.Diagnostics.Debug.WriteLine(e.Message)
            End Try

            GetValue = retVal
        End Function

        '<summary>
        'Gets a value indicating whether this property is read-only or not.
        '</summary>
        '<value></value>
        '<returns>true if the property is read-only; otherwise, false.</returns>
        Public Overrides ReadOnly Property IsReadOnly() As Boolean
            Get
                IsReadOnly = Not isEditable
            End Get
        End Property

        '<summary>
        'Gets the type of the property.
        '</summary>
        '<value></value>
        '<returns>A <see cref="T:System.Type"></see> that represents the type 
        'of the property.</returns>
        Public Overrides ReadOnly Property PropertyType() As Type
            Get
                PropertyType = netType
            End Get
        End Property

        '<summary>
        'Resets the value for this property of the component to the default value.
        '</summary>
        '<param name="component">The component (an IRow) with the property value 
        'that is to be reset to the default value.</param>
        Public Overrides Sub ResetValue(ByVal component As Object)
        End Sub

        '<summary>
        'Sets the value of the component to a different value.
        '</summary>
        '<remarks>
        'If the field this instance represents does not have a coded value domain,
        'this method simply sets the given value and stores the row within an edit
        'operation. If the field does have a coded value domain, the method first
        'needs to check that the given value is valid. If we are displaying the 
        'coded values, the value passed to this method will be a string and we will
        'need to see if it is one of the names in the cv domain. If we are not
        'displaying the coded values, we will still need to check that the given
        'value is within the domain. If the value is not within the domain, an
        'error will be displayed and the method will return.
        'Note that the string comparison is case sensitive.
        '</remarks>
        '<param name="component">The component (an IRow) with the property value 
        'that is to be set.</param>
        '<param name="value">The new value.</param>
        Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
            Dim givenRow As IRow = DirectCast(component, IRow)

            If Not (Nothing Is cvDomain) Then
                ' This field has a coded value domain
                If (Not useCVDomain) Then
                    ' Check value is valid member of the domain
                    If (Not (DirectCast(cvDomain, IDomain)).MemberOf(value)) Then _
                      System.Windows.Forms.MessageBox.Show(String.Format( _
                        "Value {0} is not valid for coded value domain {1}", value.ToString(), (DirectCast(cvDomain, IDomain)).Name))
                    Return
                Else
                    ' We need to convert the string value to one of the cv domain values
                    ' Loop through all the values until we, hopefully, find a match
                    Dim foundMatch As Boolean = False
                    Dim valueCount As Integer
                    For valueCount = 0 To cvDomain.CodeCount - 1 Step valueCount + 1
                        If (value.ToString() = cvDomain.Name(valueCount)) Then
                            foundMatch = True
                            value = valueCount
                        End If
                        Exit For
                    Next


                    ' Did we find a match?
                    If (Not foundMatch) Then
                        System.Windows.Forms.MessageBox.Show(String.Format( _
                        "Value {0} is not valid for coded value domain {1}", value.ToString(), (DirectCast(cvDomain, IDomain)).Name))
                        Return
                    End If
                End If
            End If

            givenRow.Value(wrappedFieldIndex) = value

            ' Start editing if we aren't already editing
            Dim weStartedEditing As Boolean = False
            If (Not wkspcEdit.IsBeingEdited()) Then
                wkspcEdit.StartEditing(False)
                weStartedEditing = True
                ' Store change in an edit operation
                wkspcEdit.StartEditOperation()
                givenRow.Store()
                wkspcEdit.StopEditOperation()
            End If
            ' Stop editing if we started here
            If (weStartedEditing) Then
                wkspcEdit.StopEditing(True)
            End If

        End Sub

        '<summary>
        'When overridden in a derived class, determines a value indicating whether 
        'the value of this property needs to be persisted.
        '</summary>
        '<param name="component">The component (an IRow) with the property to be examined for 
        'persistence.</param>
        '<returns>
        'true if the property should be persisted; otherwise, false.
        '</returns>
        Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            ShouldSerializeValue = False
        End Function

#End Region

#Region "Private Methods"
        '<summary>
        'Converts the specified ESRI field type to a .NET type.
        '</summary>
        '<param name="esriType">The ESRI field type to be converted.</param>
        '<returns>The appropriate .NET type.</returns>
        Function EsriFieldTypeToSystemType(ByVal field As IField) As Type
            Dim esriType As esriFieldType = field.Type

            ' Does this field have a domain?
            cvDomain = TryCast(field.Domain, ICodedValueDomain)
            If (Not (Nothing Is cvDomain) AndAlso useCVDomain) Then
                EsriFieldTypeToSystemType = GetType(String)
                Exit Function
            End If

            Try
                Select Case esriType
                    Case esriFieldType.esriFieldTypeBlob
                        'beyond scope of sample to deal with blob fields
                        EsriFieldTypeToSystemType = GetType(String)
                    Case esriFieldType.esriFieldTypeDate
                        EsriFieldTypeToSystemType = GetType(DateTime)
                    Case esriFieldType.esriFieldTypeDouble
                        EsriFieldTypeToSystemType = GetType(Double)
                    Case esriFieldType.esriFieldTypeGeometry
                        EsriFieldTypeToSystemType = GetType(String)
                    Case esriFieldType.esriFieldTypeGlobalID
                        EsriFieldTypeToSystemType = GetType(String)
                    Case esriFieldType.esriFieldTypeGUID
                        EsriFieldTypeToSystemType = GetType(Guid)
                    Case esriFieldType.esriFieldTypeInteger
                        EsriFieldTypeToSystemType = GetType(Int32)
                    Case esriFieldType.esriFieldTypeOID
                        EsriFieldTypeToSystemType = GetType(Int32)
                    Case esriFieldType.esriFieldTypeRaster
                        'beyond scope of sample to correctly display rasters
                        EsriFieldTypeToSystemType = GetType(String)
                    Case esriFieldType.esriFieldTypeSingle
                        EsriFieldTypeToSystemType = GetType(Single)
                    Case esriFieldType.esriFieldTypeSmallInteger
                        EsriFieldTypeToSystemType = GetType(Int16)
                    Case esriFieldType.esriFieldTypeString
                        EsriFieldTypeToSystemType = GetType(String)
                    Case Else
                        EsriFieldTypeToSystemType = GetType(String)
                End Select
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine(ex.Message)
                EsriFieldTypeToSystemType = GetType(String)
            End Try
        End Function

        '<summary>
        'Gets the geometry type as string.
        '</summary>
        '<param name="value">The value.</param>
        '<returns>The string equivalent of the geometry type</returns>
        Private Function GetGeomeTryTypeAsString(ByVal value As Object) As String
            Dim retVal As String = ""
            Dim geomeTry As IGeometry = TryCast(value, IGeometry)
            If Not (geomeTry Is Nothing) Then
                retVal = geomeTry.GeometryType.ToString()
            End If
            GetGeomeTryTypeAsString = retVal
        End Function
#End Region
    End Class
End Namespace
