Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Catalog
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Display

<ComClass(clsGxStyleGalleryItem.ClassId, clsGxStyleGalleryItem.InterfaceId, clsGxStyleGalleryItem.EventsId), _
 ProgId("CustomRootObjectVBNET.clsGxStyleGalleryItem")> _
Public Class clsGxStyleGalleryItem
    Implements ESRI.ArcGIS.Catalog.IGxObject

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "f53c9b52-95ae-4dc7-a9d7-50c9664d7f14"
    Public Const InterfaceId As String = "da2071a5-a1ec-416e-8915-1b240c38ef82"
    Public Const EventsId As String = "5d9bb0db-7924-4a1c-8d18-9e43ef75c2f0"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

#Region "Member Variables"
    Private m_pParent As clsGxStyleGalleryClass
    Private m_pCatalog As IGxCatalog
    Private m_pItem As IStyleGalleryItem
#End Region

    Public Sub New()
        MyBase.New()
    End Sub

    Public WriteOnly Property StyleGalleryItem() As IStyleGalleryItem
        Set(ByVal value As IStyleGalleryItem)
            m_pItem = value
        End Set
    End Property

    Public Sub PreviewItem(ByVal hDC As Long, ByVal r As tagRECT)
        m_pParent.PreviewItem(m_pItem, hDC, r)
    End Sub
    Public Sub Attach(ByVal Parent As ESRI.ArcGIS.Catalog.IGxObject, ByVal pCatalog As ESRI.ArcGIS.Catalog.IGxCatalog) Implements ESRI.ArcGIS.Catalog.IGxObject.Attach
        m_pParent = Parent
        m_pCatalog = pCatalog
    End Sub

    Public ReadOnly Property BaseName() As String Implements ESRI.ArcGIS.Catalog.IGxObject.BaseName
        Get
            BaseName = m_pItem.Name
        End Get
    End Property

    Public ReadOnly Property Category() As String Implements ESRI.ArcGIS.Catalog.IGxObject.Category
        Get
            Category = m_pItem.Category
        End Get
    End Property

    Public ReadOnly Property ClassID1() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.Catalog.IGxObject.ClassID
        Get
            ClassID1 = Nothing
        End Get
    End Property

    Public Sub Detach() Implements ESRI.ArcGIS.Catalog.IGxObject.Detach
        m_pParent = Nothing
        m_pCatalog = Nothing
    End Sub

    Public ReadOnly Property FullName() As String Implements ESRI.ArcGIS.Catalog.IGxObject.FullName
        Get
            FullName = m_pItem.Name
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
            Name = m_pItem.Name
        End Get
    End Property

    Public ReadOnly Property Parent() As ESRI.ArcGIS.Catalog.IGxObject Implements ESRI.ArcGIS.Catalog.IGxObject.Parent
        Get
            Parent = m_pParent
        End Get
    End Property

    Public Sub Refresh() Implements ESRI.ArcGIS.Catalog.IGxObject.Refresh

    End Sub
End Class


