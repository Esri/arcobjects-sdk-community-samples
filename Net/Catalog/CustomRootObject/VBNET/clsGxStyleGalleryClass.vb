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
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices

<ComClass(clsGxStyleGalleryClass.ClassId, clsGxStyleGalleryClass.InterfaceId, clsGxStyleGalleryClass.EventsId), _
 ProgId("CustomRootObjectVBNET.clsGxStyleGalleryClass")> _
Public Class clsGxStyleGalleryClass
    Implements ESRI.ArcGIS.Catalog.IGxObject
    Implements ESRI.ArcGIS.Catalog.IGxObjectContainer


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "42d4d338-936c-4a71-a02d-577047740007"
    Public Const InterfaceId As String = "afa9eff2-7195-48ba-b025-d7daf973c453"
    Public Const EventsId As String = "a971370b-baf7-4634-b71d-9839eeb9ea00"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

#Region "Member Variable"
    Private m_pParent As clsGxStyleGallery
    Private m_pCatalog As IGxCatalog
    Private m_pClass As IStyleGalleryClass
    Private m_pChildren As IGxObjectArray
    Private m_childrenLoaded As Boolean
#End Region

    Public Sub New()
        MyBase.New()
        m_pChildren = New GxObjectArray
        m_childrenLoaded = False
    End Sub
    Public WriteOnly Property StyleGalleryClass() As IStyleGalleryClass
        Set(ByVal value As IStyleGalleryClass)
            m_pClass = value
        End Set
    End Property
    Public Sub PreviewItem(ByVal pItem As IStyleGalleryItem, ByVal hDC As Long, ByVal r As tagRECT)
        'Draw a representation of the item to the given DC.
        m_pClass.Preview(pItem.Item, hDC, r)
    End Sub
    Public Sub Attach(ByVal Parent As ESRI.ArcGIS.Catalog.IGxObject, ByVal pCatalog As ESRI.ArcGIS.Catalog.IGxCatalog) Implements ESRI.ArcGIS.Catalog.IGxObject.Attach
        m_pParent = Parent
        m_pCatalog = pCatalog
    End Sub

    Public ReadOnly Property BaseName() As String Implements ESRI.ArcGIS.Catalog.IGxObject.BaseName
        Get
            BaseName = m_pClass.Name
        End Get
    End Property

    Public ReadOnly Property Category() As String Implements ESRI.ArcGIS.Catalog.IGxObject.Category
        Get
            Category = "Style Gallery Class"
        End Get
    End Property

    Public ReadOnly Property ClassID1() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.Catalog.IGxObject.ClassID
        Get
            ClassID1 = Nothing
        End Get
    End Property

    Public Sub Detach() Implements ESRI.ArcGIS.Catalog.IGxObject.Detach
        'It is our responsibility to detach all our children before deleting them.
        'This is to avoid circular referencing problems.
        Dim i As Long
        For i = 0 To m_pChildren.Count
            m_pChildren.Item(i).Detach()
        Next
        m_pParent = Nothing
        m_pCatalog = Nothing
    End Sub

    Public ReadOnly Property FullName() As String Implements ESRI.ArcGIS.Catalog.IGxObject.FullName
        Get
            FullName = m_pClass.Name
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
            Name = m_pClass.Name
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

        'Our children are GxContainer objects that represent the actual style items
        'of a certain type.

        Dim pEnumItems As IEnumStyleGalleryItem
        Dim pItem As IStyleGalleryItem
        Try
            pEnumItems = m_pParent.StyleGallery.Items(m_pClass.Name, "ESRI.style", "")
            pItem = pEnumItems.Next
            While (Not pItem Is Nothing)
                Dim pGxItem As clsGxStyleGalleryItem
                pGxItem = New clsGxStyleGalleryItem
                pGxItem.StyleGalleryItem = pItem

                Dim pGxObject As IGxObject
                pGxObject = pGxItem
                pGxObject.Attach(Me, m_pCatalog)

                m_pChildren.Insert(-1, pGxObject)
                pItem = pEnumItems.Next
            End While
            m_childrenLoaded = True
        Catch ex As Exception

        Finally
            pEnumItems = Nothing
            pItem = Nothing
        End Try
    End Sub

End Class


