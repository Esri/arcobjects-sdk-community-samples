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
Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Collections

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geometry

  ''' <summary>
  ''' This call demonstrated an implementation of a hybrid colnable
  ''' class which has both .NET members as well as COM members.
  ''' </summary>
<Guid(ClonableObjClass.GUIDVAL), ClassInterface(ClassInterfaceType.None), ProgId("ClonableObject.ClonableObjClass")> _
Public NotInheritable Class ClonableObjClass
  Implements IClone
#Region "different types of class members (COM and .NET)"

  Public Const GUIDVAL As String = "0678ecf9-4066-4a61-94fb-45d6c4753826"

  Private m_version As Integer = 1
  Private m_spatialRef As ISpatialReference = Nothing
  Private m_point As IPoint = Nothing
  Private m_name As String = String.Empty
  Private m_arr As ArrayList = Nothing
  Private m_ID As Guid
#End Region

#Region "class constructor"
  Public Sub New()
    m_ID = Guid.NewGuid()

    m_spatialRef = New UnknownCoordinateSystemClass()
  End Sub
#End Region

#Region "public class properties"
  Public Property Name() As String
    Get
      Return m_name
    End Get
    Set(ByVal value As String)
      m_name = value
    End Set
  End Property

  Public ReadOnly Property Version() As Integer
    Get
      Return m_version
    End Get
  End Property

  Public Property SpatialReference() As ISpatialReference
    Get
      Return m_spatialRef
    End Get
    Set(ByVal value As ISpatialReference)
      m_spatialRef = value
    End Set
  End Property

  Public ReadOnly Property ID() As Guid
    Get
      Return m_ID
    End Get
  End Property

  Public Property Point() As IPoint
    Get
      Return m_point
    End Get
    Set(ByVal value As IPoint)
      m_point = value
    End Set
  End Property

  Public Property ManagedArray() As ArrayList
    Get
      Return m_arr
    End Get
    Set(ByVal value As ArrayList)
      m_arr = value
    End Set
  End Property
#End Region

#Region "IClone Members"

  ''' <summary>
  ''' Assigns the properties of src to the receiver.
  ''' </summary>
  ''' <param name="src"></param>
  Public Sub Assign(ByVal src As IClone) Implements IClone.Assign
    '1. make sure that src is pointing to a valid object
    If Nothing Is src Then
      Throw New COMException("Invalid objact.")
    End If

    '2. make sure that the type of src is of type 'ClonableObjClass'
    If Not (TypeOf src Is ClonableObjClass) Then
      Throw New COMException("Bad object type.")
    End If

    '3. assign the properties of src to the current instance
    Dim srcClonable As ClonableObjClass = CType(src, ClonableObjClass)
    m_name = srcClonable.Name
    m_version = srcClonable.Version
    m_ID = New Guid(srcClonable.ID.ToString())

    'don't clone the spatial reference, since in this case we want both object to 
    'reference the same spatial reference (for example like features in a featureclass 
    'which share the same spatial reference)
    m_spatialRef = srcClonable.SpatialReference

    'clone the point. Use deep cloning 
    If Nothing Is srcClonable.Point Then
      m_point = Nothing
    Else
      Dim objectCopy As IObjectCopy = New ObjectCopyClass()
      Dim obj As Object = objectCopy.Copy(CObj(srcClonable.Point))
      m_point = CType(obj, IPoint)
    End If

    m_arr = CType(srcClonable.ManagedArray.Clone(), ArrayList)
  End Sub

  ''' <summary>
  ''' Clones the receiver and assigns the result to clonee.
  ''' <returns></returns>
  Public Function Clone() As IClone Implements IClone.Clone
    'create a new instance of the object
    Dim obj As ClonableObjClass = New ClonableObjClass()
    'assign the properties of the new object with the current object's properties.
    'according to each 'Ref' property, the user need to decide whether to use deep cloning
    'or shallow cloning. 
    obj.Assign(Me)

    Return CType(obj, IClone)
  End Function

  ''' <summary>
  ''' Returns TRUE when the receiver and the other object have the same properties.
  ''' </summary>
  ''' <param name="other"></param>
  ''' <returns></returns>
  Public Function IsEqual(ByVal other As IClone) As Boolean Implements IClone.IsEqual
    '1. make sure that the 'other' object is pointing to a valid object
    If Nothing Is other Then
      Throw New COMException("Invalid objact.")
    End If

    '2. verify the type of 'other'
    If Not (TypeOf other Is ClonableObjClass) Then
      Throw New COMException("Bad object type.")
    End If

    Dim otherClonable As ClonableObjClass = CType(other, ClonableObjClass)

    'test that all ot the object's properties are the same.
    'please note the usage of IsEqual when using arcobjects components that
    'supports cloning
    If otherClonable.Version = m_version AndAlso otherClonable.Name = m_name AndAlso otherClonable.ID = m_ID AndAlso otherClonable.ManagedArray Is m_arr AndAlso (CType(otherClonable.SpatialReference, IClone)).IsEqual(CType(m_spatialRef, IClone)) AndAlso (CType(otherClonable.Point, IClone)).IsEqual(CType(m_point, IClone)) Then

      Return True
    End If

    Return False
  End Function

  Public Function IsIdentical(ByVal other As IClone) As Boolean Implements IClone.IsIdentical
    '1. make sure that the 'other' object is pointing to a valid object
    If Nothing Is other Then
      Throw New COMException("Invalid objact.")
    End If

    '2. verify the type of 'other'
    If Not (TypeOf other Is ClonableObjClass) Then
      Throw New COMException("Bad object type.")
    End If

    '3. test if the other is the 'this'
    If CType(other, ClonableObjClass) Is Me Then
      Return True
    End If

    Return False
  End Function

#End Region
End Class
