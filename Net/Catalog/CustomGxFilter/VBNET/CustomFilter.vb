Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Catalog
Imports System.Runtime.InteropServices

<ComClass(CustomFilter.ClassId, CustomFilter.InterfaceId, CustomFilter.EventsId), _
 ProgId("CustomGxFilterVBNET.CustomFilter")> _
Public Class CustomFilter
    Implements ESRI.ArcGIS.Catalog.IGxObjectFilter
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
        GxObjectFilters.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxObjectFilters.Unregister(regKey)

    End Sub

#End Region
#End Region


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "06d63ee7-1166-4a25-bedf-ae7c35e53419"
    Public Const InterfaceId As String = "9c10af78-ac04-4bc3-9165-02b13d4c72dd"
    Public Const EventsId As String = "43acd42e-b0c2-4bd6-916b-8b91ae38f3ad"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

#Region "Member Variables"
    Private m_pBasicFilter As IGxObjectFilter
#End Region

    Public Sub New()
        MyBase.New()
        m_pBasicFilter = New GxFilterBasicTypes
    End Sub

    Public Function CanChooseObject(ByVal obj As ESRI.ArcGIS.Catalog.IGxObject, ByRef result As ESRI.ArcGIS.Catalog.esriDoubleClickResult) As Boolean Implements ESRI.ArcGIS.Catalog.IGxObjectFilter.CanChooseObject
        'Set whether the selected object can be chosen
        Dim bCanChoose As Boolean
        bCanChoose = False
        If TypeOf obj Is IGxFile Then
            Dim sExt As String
            sExt = GetExtension(obj.Name)
            If LCase(sExt) = ".py" Then bCanChoose = True
        End If
        CanChooseObject = bCanChoose
    End Function

    Public Function CanDisplayObject(ByVal obj As ESRI.ArcGIS.Catalog.IGxObject) As Boolean Implements ESRI.ArcGIS.Catalog.IGxObjectFilter.CanDisplayObject
        'Check objects can be displayed
        Try
            'Check objects can be displayed
            If m_pBasicFilter.CanDisplayObject(obj) Then
                Return True
            ElseIf TypeOf obj Is IGxFile Then
                Dim sExt As String
                sExt = GetExtension(obj.Name)
                If LCase(sExt) = ".py" Then Return True
            End If

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Function

    Public Function CanSaveObject(ByVal Location As ESRI.ArcGIS.Catalog.IGxObject, ByVal newObjectName As String, ByRef objectAlreadyExists As Boolean) As Boolean Implements ESRI.ArcGIS.Catalog.IGxObjectFilter.CanSaveObject

    End Function

    Public ReadOnly Property Description() As String Implements ESRI.ArcGIS.Catalog.IGxObjectFilter.Description
        Get
            'Set filet description
            Description = "Python file (.py)"
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.Catalog.IGxObjectFilter.Name
        Get
            'Set filter name
            Name = "Python filter"
        End Get
    End Property

    Private Function GetExtension(ByVal sFileName As String) As String
        'Get extension
        Dim pExtPos As Long
        pExtPos = InStrRev(sFileName, ".")
        If pExtPos > 0 Then
            GetExtension = Mid(sFileName, pExtPos)
        Else
            GetExtension = ""
        End If
    End Function
End Class


