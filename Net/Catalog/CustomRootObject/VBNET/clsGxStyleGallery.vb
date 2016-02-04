'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Display

Imports System.Runtime.InteropServices


<ComClass(clsGxStyleGallery.ClassId, clsGxStyleGallery.InterfaceId, clsGxStyleGallery.EventsId), _
 ProgId("CustomRootObjectVBNET.clsGxStyleGallery")> _
Public Class clsGxStyleGallery
    Implements ESRI.ArcGIS.Catalog.IGxObject
    Implements ESRI.ArcGIS.Catalog.IGxObjectContainer
#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxRootObjects.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxRootObjects.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "1b4e3074-1177-45c0-9742-88b69bb0db61"
    Public Const InterfaceId As String = "59584479-1363-46bf-9209-5a238a16f89c"
    Public Const EventsId As String = "1c844126-7976-400b-9d3e-412d17c7dcea"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

#Region "Private m_pParent As IGxObject"
    Private m_pParent As IGxObject
    Private m_pCatalog As IGxCatalog
    Public m_pGallery As IStyleGallery
    Private m_pChildren As IGxObjectArray
    Private m_childrenLoaded As Boolean
#End Region
    Public Sub New()
        MyBase.New()
        m_pChildren = New GxObjectArray
        m_childrenLoaded = False
        m_pGallery = New ESRI.ArcGIS.Framework.StyleGallery
    End Sub
    Public ReadOnly Property StyleGallery() As IStyleGallery
        Get
            StyleGallery = m_pGallery
        End Get
    End Property


    Public Sub Attach(ByVal Parent As ESRI.ArcGIS.Catalog.IGxObject, ByVal pCatalog As ESRI.ArcGIS.Catalog.IGxCatalog) Implements ESRI.ArcGIS.Catalog.IGxObject.Attach
        m_pParent = Parent
        m_pCatalog = pCatalog
    End Sub

    Public ReadOnly Property BaseName() As String Implements ESRI.ArcGIS.Catalog.IGxObject.BaseName
        Get
            BaseName = "Style Gallery"
        End Get
    End Property

    Public ReadOnly Property Category() As String Implements ESRI.ArcGIS.Catalog.IGxObject.Category
        Get
            Category = "Style Gallery Manager"
        End Get
    End Property

    Public ReadOnly Property ClassID1() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.Catalog.IGxObject.ClassID
        Get
            Dim pUID As IUID
            pUID = New UID
            pUID.Value = "CustomRootObjectVBNET.clsGxStyleGallery"
            ClassID1 = pUID
        End Get
    End Property

    Public Sub Detach() Implements ESRI.ArcGIS.Catalog.IGxObject.Detach
        'It is our responsibility to detach all of our children before deleting
        'them.  This is to avoid circular referencing problems.
        Dim i As Long
        For i = 0 To m_pChildren.Count
            m_pChildren.Item(i).Detach()
        Next
        m_pParent = Nothing
        m_pCatalog = Nothing
    End Sub

    Public ReadOnly Property FullName() As String Implements ESRI.ArcGIS.Catalog.IGxObject.FullName
        Get
            FullName = "Style Gallery"
        End Get
    End Property

    Public ReadOnly Property InternalObjectName() As ESRI.ArcGIS.esriSystem.IName Implements ESRI.ArcGIS.Catalog.IGxObject.InternalObjectName
        Get
            InternalObjectName = Nothing
        End Get
    End Property

    Public ReadOnly Property IsValid() As Boolean Implements ESRI.ArcGIS.Catalog.IGxObject.IsValid
        Get
            IsValid = True
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.Catalog.IGxObject.Name
        Get
            Name = "Style Gallery"
        End Get
    End Property

    Public ReadOnly Property Parent() As ESRI.ArcGIS.Catalog.IGxObject Implements ESRI.ArcGIS.Catalog.IGxObject.Parent
        Get
            Parent = m_pParent
        End Get
    End Property

    Public Sub Refresh() Implements ESRI.ArcGIS.Catalog.IGxObject.Refresh
        'Unload and reload the children.
        m_pChildren.Empty()
        m_childrenLoaded = False
        LoadChildren()
    End Sub

    Public Function AddChild(ByVal child As ESRI.ArcGIS.Catalog.IGxObject) As ESRI.ArcGIS.Catalog.IGxObject Implements ESRI.ArcGIS.Catalog.IGxObjectContainer.AddChild

    End Function

    Public ReadOnly Property AreChildrenViewable() As Boolean Implements ESRI.ArcGIS.Catalog.IGxObjectContainer.AreChildrenViewable
        Get
            AreChildrenViewable = True
        End Get
    End Property

    Public ReadOnly Property Children() As ESRI.ArcGIS.Catalog.IEnumGxObject Implements ESRI.ArcGIS.Catalog.IGxObjectContainer.Children
        Get
            LoadChildren()
            Children = m_pChildren
        End Get
    End Property

    Public Sub DeleteChild(ByVal child As ESRI.ArcGIS.Catalog.IGxObject) Implements ESRI.ArcGIS.Catalog.IGxObjectContainer.DeleteChild

    End Sub

    Public ReadOnly Property HasChildren() As Boolean Implements ESRI.ArcGIS.Catalog.IGxObjectContainer.HasChildren
        Get
            HasChildren = True
        End Get
    End Property

    Private Sub LoadChildren()
        If m_childrenLoaded Then Exit Sub

        'Our children are GxContainer objects that represent class folders
        'for all the different types of styles.  Loop over each of these
        'types, and create a clsGxStyleGalleryClass object for it, and attach it to the
        'tree correctly.

        Dim i As Long
        For i = 0 To m_pGallery.ClassCount - 1
            Dim pGxClass As clsGxStyleGalleryClass
            pGxClass = New clsGxStyleGalleryClass
            pGxClass.StyleGalleryClass = m_pGallery.Class(i)

            Dim pGxObject As IGxObject
            pGxObject = pGxClass
            pGxObject.Attach(Me, m_pCatalog)
            m_pChildren.Insert(-1, pGxObject)
        Next
        m_childrenLoaded = True
    End Sub
End Class


