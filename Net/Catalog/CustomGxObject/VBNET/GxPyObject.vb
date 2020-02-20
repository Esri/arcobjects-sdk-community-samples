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
Imports ESRI.ArcGIS.ADF.CATIDs

Imports System
Imports System.IO
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.esriSystem

' This sample code demonstrates how to create a custom object factory
' for use in ArcCatalog. The object factory allows you to browse files 
' with the *.PY extension. 
' 
' Guid attribute for the GxPYObject class.
' ProgID attribute - otherwise the ProgID will appear as <Namespace>.<Class>.
' InterfaceType attribute to indicate custom interface.
<ComClass(GxPyObjectVBNET.ClassId, GxPyObjectVBNET.InterfaceId, GxPyObjectVBNET.EventsId)> _
Public NotInheritable Class GxPyObjectVBNET
    Implements IGxObject
    Implements IGxObjectUI
    Implements IGxObjectEdit
    Implements IGxObjectProperties
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

    <DllImport("gdi32.dll")> _
    Private Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

#Region "  Member Variables"
    Private m_gxParent As IGxObject = Nothing
    Private m_gxCatalog As IGxCatalog = Nothing

    Private m_names() As String = {"", "", ""}   '0:FullName; 1:Name; 2:BaseName
    Private m_bitmaps(2) As System.Drawing.Bitmap    ' = New System.Drawing.Bitmap(2)
    Private m_hBitmap(2) As IntPtr

    Private m_sCategory As String = "PY File"
#End Region

#Region "  COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "EDBAE284-F590-4FE3-9FA9-5025EBE284AC"
    Public Const InterfaceId As String = "23693B53-6F0C-469B-AADD-BC8DF20F9DE3"
    Public Const EventsId As String = "C8226E4C-6F6D-4927-9408-BA5AA318F60C"
#End Region

#Region " Constructor/Destructor code"
    Public Sub New()
        Me.SetBitmaps()
    End Sub

    Public Sub New(ByVal name As String)
        Me.SetBitmaps()
        Me.SetNames(name)
    End Sub

    Private Sub SetNames(ByVal newName As String)
        If (Not newName Is Nothing) Then
            ' Set the FullName, Name, and BaseName, based on the specified string.
            m_names(0) = newName
            Dim indx As Integer = newName.LastIndexOf("\")
            If (indx > -1) Then
                m_names(1) = newName.Substring(indx + 1)
            Else
                m_names(1) = newName
            End If

            indx = m_names(1).LastIndexOf(".")
            If (indx > -1) Then
                m_names(2) = m_names(1).Remove(indx, m_names(1).Length - indx)
            Else
                m_names(1) = newName
            End If
        End If
    End Sub

    Private Sub SetBitmaps()
        Try

            Dim myRes As String() = Me.GetType().Assembly.GetManifestResourceNames()
            Dim i As Integer
            Dim count As Integer = myRes.GetUpperBound(0)
            For i = 0 To count
                System.Diagnostics.Debug.WriteLine(myRes(i))
            Next i

            ' Initialize the icons to use.
            m_bitmaps(0) = New System.Drawing.Bitmap(GetType(GxPyObjectVBNET).Assembly.GetManifestResourceStream("GxObjectVBNET.LargeIcon.bmp"))
            m_bitmaps(1) = New System.Drawing.Bitmap(GetType(GxPyObjectVBNET).Assembly.GetManifestResourceStream("GxObjectVBNET.SmallIcon.bmp"))
            If (Not m_bitmaps(0) Is Nothing) Then
                m_bitmaps(0).MakeTransparent(m_bitmaps(0).GetPixel(1, 1))
                m_hBitmap(0) = m_bitmaps(0).GetHbitmap()
            End If
            If (Not m_bitmaps(1) Is Nothing) Then
                m_bitmaps(1).MakeTransparent(m_bitmaps(1).GetPixel(1, 1))
                m_hBitmap(1) = m_bitmaps(1).GetHbitmap()
            End If
        Catch Ex As System.ArgumentException
            If (Ex.TargetSite.ToString() = "Void .ctor(System.IO.Stream)") Then
                System.Diagnostics.Debug.WriteLine(Ex.Message)
                ' Error accessing the bitmap embedded resource.
                m_bitmaps(0) = Nothing
                m_bitmaps(1) = Nothing
            End If
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        If Not (m_hBitmap(0).ToInt32() = 0) Then
            DeleteObject(m_hBitmap(0))
        End If
        If Not (m_hBitmap(1).ToInt32() = 0) Then
            DeleteObject(m_hBitmap(1))
        End If
    End Sub
#End Region

#Region "  Implementation of IGxObject"

    Private Sub Attach(ByVal Parent As IGxObject, ByVal pCatalog As IGxCatalog) Implements IGxObject.Attach
        m_gxParent = Parent
        m_gxCatalog = pCatalog
    End Sub

    Private Sub Detach() Implements IGxObject.Detach
        m_gxParent = Nothing
        m_gxCatalog = Nothing
    End Sub

    Private Sub Refresh() Implements IGxObject.Refresh
        ' No impl.		
    End Sub

    Private ReadOnly Property InternalObjectName() As IName Implements IGxObject.InternalObjectName
        Get
            Dim fileName As IFileName = CType(New FileName, IFileName)
      fileName.Path = m_names(0)

            Return CType(fileName, IName)
        End Get
    End Property

    Private ReadOnly Property IsValid() As Boolean Implements IGxObject.IsValid
        Get
            Dim Info As New FileInfo(m_names(0))
            Return Info.Exists
        End Get
    End Property

    Private ReadOnly Property FullName() As String Implements IGxObject.FullName
        Get
            Return m_names(0)
        End Get
    End Property

    Private ReadOnly Property BaseName() As String Implements IGxObject.BaseName
        Get
            Return m_names(2)
        End Get
    End Property

    Private ReadOnly Property Name() As String Implements IGxObject.Name
        Get
            Return m_names(1)
        End Get
    End Property

    Private ReadOnly Property GxClassID() As UID Implements IGxObject.ClassID
        Get
            Dim clsID As UID = New UIDClass
            clsID.Value = "{0E63CDC4-7E13-422f-8B2D-F5DF853F9CA1}"
            Return clsID
        End Get
    End Property

    Private ReadOnly Property Parent() As IGxObject Implements IGxObject.Parent
        Get
            Return m_gxParent
        End Get
    End Property

    Private ReadOnly Property Category() As String Implements IGxObject.Category
        Get
            Return Me.m_sCategory
        End Get
    End Property

#End Region

#Region "  Implementation of IGxObjectUI"
    Public ReadOnly Property NewMenu() As UID Implements IGxObjectUI.NewMenu
        Get
            ' If you have created a class of New Menu for this object, you can implement it here
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property SmallImage() As Integer Implements IGxObjectUI.SmallImage
        Get
            If (Not m_bitmaps(1) Is Nothing) Then
                Return m_bitmaps(1).GetHbitmap().ToInt32()
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property LargeSelectedImage() As Integer Implements IGxObjectUI.LargeSelectedImage
        Get
            If (Not m_bitmaps(0) Is Nothing) Then
                Return m_bitmaps(0).GetHbitmap().ToInt32()
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property SmallSelectedImage() As Integer Implements IGxObjectUI.SmallSelectedImage
        Get
            If (Not m_bitmaps(1) Is Nothing) Then
                Return m_bitmaps(1).GetHbitmap().ToInt32()
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property ContextMenu() As UID Implements IGxObjectUI.ContextMenu
        Get
            ' If you have created a class of context menu of this object, you can implement it here
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property LargeImage() As Integer Implements IGxObjectUI.LargeImage
        Get
            If (Not m_bitmaps(0) Is Nothing) Then
                Return m_bitmaps(0).GetHbitmap().ToInt32()
            Else
                Return 0
            End If
        End Get
    End Property
#End Region

#Region "  Implementation of IGxObjectEdit"
    Public Function CanCopy() As Boolean Implements ESRI.ArcGIS.Catalog.IGxObjectEdit.CanCopy
        Return True
    End Function

    Public Function CanDelete() As Boolean Implements ESRI.ArcGIS.Catalog.IGxObjectEdit.CanDelete
        'This file should exist and not readonly
        Dim Info As New FileInfo(m_names(0))
        Return Info.Exists And (Info.Attributes <> 1)
    End Function

    Public Function CanRename() As Boolean Implements ESRI.ArcGIS.Catalog.IGxObjectEdit.CanRename
        Return True
    End Function

    Public Sub Delete() Implements ESRI.ArcGIS.Catalog.IGxObjectEdit.Delete
        'Delete
        File.Delete(m_names(0))

        'Tell parent the object is gone
        Dim pGxObjectContainer As IGxObjectContainer = CType(m_gxParent, IGxObjectContainer)
        pGxObjectContainer.DeleteChild(Me)
    End Sub

    Public Sub EditProperties(ByVal hParent As Integer) Implements ESRI.ArcGIS.Catalog.IGxObjectEdit.EditProperties
        'Add implementation if you have defined property page
    End Sub

    Public Sub Rename(ByVal newShortName As String) Implements ESRI.ArcGIS.Catalog.IGxObjectEdit.Rename

        'Trim PY extension
        If UCase(Right(newShortName, 3)) = ".PY" Then
            newShortName = Left(newShortName, Len(newShortName) - 3)
        End If

        'Construct new name
        Dim pos As Integer = InStrRev(m_names(0), "\")
        Dim newName As String = Left(m_names(0), pos) & newShortName & ".PY"

        'Rename
        File.Move(m_names(0), newName)

        'Tell parent that name is changed
        m_gxParent.Refresh()
    End Sub

#End Region

#Region "  Implementation of IGxObjectProperties"
    Public Function GetProperty(ByVal Name As String) As Object Implements IGxObjectProperties.GetProperty
        If (Not Name Is Nothing) Then
            Select Case (Name)
                Case "ESRI_GxObject_Name"
                    Return Me.Name
                Case "ESRI_GxObject_Type"
                    Return Me.Category
            End Select
        End If
        Return Nothing
    End Function

    Public Sub GetPropByIndex(ByVal Index As Integer, ByRef pName As String, ByRef pValue As Object) Implements IGxObjectProperties.GetPropByIndex
        Select Case (Index)
            Case 0
                pName = "ESRI_GxObject_Name"
                pValue = CType(Me.Name, System.Object)
                Return
            Case 1
                pName = "ESRI_GxObject_Type"
                pValue = CType(Me.Category, System.Object)
                Return
            Case Else
                pName = Nothing
                pValue = Nothing
                Return
        End Select

    End Sub

    Public Sub SetProperty(ByVal Name As String, ByVal Value As Object) Implements IGxObjectProperties.SetProperty
        'No implementation
    End Sub

    Public ReadOnly Property PropertyCount() As Integer Implements IGxObjectProperties.PropertyCount
        Get
            Return 2
        End Get
    End Property
#End Region

End Class

