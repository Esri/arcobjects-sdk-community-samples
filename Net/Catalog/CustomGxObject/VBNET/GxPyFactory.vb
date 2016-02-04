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
Imports ESRI.ArcGIS.ADF.CATIDs

Imports System
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.esriSystem

' This sample demonstrates how to create a custom GxObject and the 
' GxObject factory to go with it. With a custom object and factory, you 
' can browse for the specified file types within ArcCatalog. 
'
' Guid attribute for the GxPyFactory class.
' ProgID attribute - otherwise the ProgID will appear as <Namespace>.<Class>.
' InterfaceType attribute to indicate custom interface.
<ComClass(GxPyFactoryVBNET.ClassId, GxPyFactoryVBNET.InterfaceId, GxPyFactoryVBNET.EventsId)> _
 Public NotInheritable Class GxPyFactoryVBNET
    Implements IGxObjectFactory
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
        GxObjectFactory.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxObjectFactory.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_catalog As IGxCatalog

#Region "  COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "73241D64-80B9-4B34-9CE3-83DBC5F0FA22"
    Public Const InterfaceId As String = "5FE3BCBD-17F3-49A9-BBD2-CA2D5BE9C74D"
    Public Const EventsId As String = "F76DDCA2-1232-42EB-BA4F-B03318398A6D"
#End Region

    Public Sub New()
        m_catalog = Nothing
    End Sub

    Public Function HasChildren(ByVal parentDir As String, ByVal FileNames As IFileNames) As Boolean Implements IGxObjectFactory.HasChildren

        If (Not FileNames Is Nothing) Then
            FileNames.Reset()
            Dim fileName As String = FileNames.Next()
            While ((Not fileName Is Nothing) And (fileName.Length > 0))
                If (fileName.ToUpper().EndsWith(".PY")) Then
                    Return True
                End If
                fileName = FileNames.Next()
            End While
        End If
        Return False
    End Function

    Public Function GetChildren(ByVal parentDir As String, ByVal FileNames As IFileNames) As IEnumGxObject Implements IGxObjectFactory.GetChildren
        Dim gxChildren As IGxObjectArray = New GxObjectArray()

        If (Not FileNames Is Nothing) Then

            FileNames.Reset()

            Dim fileName As String = FileNames.Next()
            While (Not fileName Is Nothing)
                If (fileName.Length > 0) Then
                    If (Not FileNames.IsDirectory()) Then
                        If (fileName.ToUpper().EndsWith(".PY")) Then
                            Dim gxChild As GxPyObjectVBNET = New GxPyObjectVBNET(fileName)
                            gxChildren.Insert(-1, gxChild)
                            gxChild = Nothing
                            ' Remove file name from the list for other GxObjectFactories to search.
                            FileNames.Remove()
                        End If
                    End If
                End If
                fileName = FileNames.Next()
            End While
        End If

        If (gxChildren.Count > 0) Then
            Dim enumChildren As IEnumGxObject = CType(gxChildren, IEnumGxObject)
            enumChildren.Reset()
            Return enumChildren
        Else
            Return Nothing
        End If
    End Function

    Public WriteOnly Property Catalog() As IGxCatalog Implements IGxObjectFactory.Catalog
        Set(ByVal Value As IGxCatalog)
            If (Not Value Is Nothing) Then
                ' Store incoming value of Catalog.
                m_catalog = Value
            End If
        End Set
    End Property

    Public ReadOnly Property Name() As String Implements IGxObjectFactory.Name
        Get
            Return "Python Files"
        End Get
    End Property

End Class