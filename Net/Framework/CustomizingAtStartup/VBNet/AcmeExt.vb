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
Imports System.Runtime.InteropServices 
Imports ESRI.ArcGIS.Framework 
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem 
Imports ESRI.ArcGIS.ADF.CATIDs 


Namespace ACME.GIS.SampleExt

  <Guid("CC11AED1-C9AD-4a01-B138-EA7BB0487FF0")> _
  <ClassInterface(ClassInterfaceType.None)> _
  <ProgId("ACME.ExtensionVB")> _
  Public Class AcmeExt
    Implements IExtension
    Implements IExtensionConfig
    Implements IPersistVariant


#Region "COM Registration Functions"
    ' Register the Extension in the ESRI MxExtensions Component Category 
    <ComRegisterFunction()> _
    <ComVisible(False)> _
    Private Shared Sub RegisterFunction(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      MxExtension.Register(regKey)
    End Sub

    <ComUnregisterFunction()> _
    <ComVisible(False)> _
    Private Shared Sub UnregisterFunction(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      MxExtension.Unregister(regKey)
    End Sub
#End Region

    Private m_application As IApplication
    Private m_appStatus As IApplicationStatus
    Private m_extensionState As esriExtensionState = esriExtensionState.esriESDisabled
    Private m_isMenuPresent As Boolean = False

    Private m_docEvents As IDocumentEvents_Event
    Private m_mapStatusEvents As IApplicationStatusEvents_Event

    Private Const c_extName As String = "ACME Extension (VB)"
    Private Const c_menuID As String = "ACME.MainMenuVB"
    Private Const c_mainMenuID As String = "{1E739F59-E45F-11D1-9496-080009EEBECB}" ' Main menubar 

#Region "IExtension Members"

    Public ReadOnly Property Name() As String Implements IExtension.Name
      Get
        Return c_extName
      End Get
    End Property

    Public Sub Shutdown() Implements IExtension.Shutdown
      m_docEvents = Nothing
      m_application = Nothing
    End Sub

    Public Sub Startup(ByRef initializationData As Object) Implements IExtension.Startup
      m_application = TryCast(initializationData, IApplication)
      m_appStatus = TryCast(m_application, IApplicationStatus)
      ' Wireup the events 
      SetupEvents()
    End Sub

#End Region

#Region "IExtensionConfig Members"

    Public ReadOnly Property Description() As String Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.Description
      Get
        Return "Sample extension showing IApplicationStatusEvents"
      End Get
    End Property

    Public ReadOnly Property ProductName() As String Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.ProductName
      Get
        Return c_extName
      End Get
    End Property

    Public Property State() As ESRI.ArcGIS.esriSystem.esriExtensionState Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.State
      Get
        Return m_extensionState
      End Get
      Set(ByVal value As esriExtensionState)
        ' Bail if requested state matches current state 
        If value = m_extensionState Then
          Return
        End If

        ' Cache the state 
        m_extensionState = value

        If m_application Is Nothing Then
          Return
          ' Don't assume Startup has been called (JIT extensions) 
        End If

        ' Enable, add the menu 
        If m_extensionState = esriExtensionState.esriESEnabled Then
          If Not m_isMenuPresent Then
            CheckMenuValidity()
          End If
          Return
        End If

        ' Disable, remove the menu 
        If m_extensionState = esriExtensionState.esriESDisabled Then
          If m_isMenuPresent Then
            UnLoadCustomizations()
          End If
          Return
        End If
      End Set
    End Property

#End Region

#Region "IPersistVariant Members"

    Public ReadOnly Property ID() As UID Implements IPersistVariant.ID
      Get
        Dim extUID As UID = New UIDClass()
        extUID.Value = [GetType]().GUID.ToString("B")
        Return extUID


      End Get
    End Property

    Public Sub Load(ByVal Stream As IVariantStream) Implements IPersistVariant.Load
    End Sub

    Public Sub Save(ByVal Stream As IVariantStream) Implements IPersistVariant.Save
    End Sub

#End Region

#Region "Events"

    Private Sub SetupEvents()
      ' Make sure we're dealing with ArcMap 
      If TypeOf m_application.Document.Parent Is IMxApplication Then
        m_docEvents = TryCast(m_application.Document, IDocumentEvents_Event)
        AddHandler m_docEvents.NewDocument, AddressOf OnNewDocument
        AddHandler m_docEvents.OpenDocument, AddressOf OnOpenDocument

        m_mapStatusEvents = TryCast(m_application.Document.Parent, IApplicationStatusEvents_Event)
        AddHandler m_mapStatusEvents.Initialized, AddressOf OnInitialized
      End If
    End Sub

    Private Sub OnOpenDocument()
      CheckMenuValidity()
    End Sub

    Private Sub OnNewDocument()
      CheckMenuValidity()
    End Sub

    ' Called when the framework is fully initialized 
    ' After this event fires, it is safe to make UI customizations 
    Private Sub OnInitialized()
      CheckMenuValidity()
    End Sub
#End Region

    Private Sub CheckMenuValidity()
      ' Wait for framework to initialize before making ui customizations 
      ' Check framework initialization flag 
      If Not m_appStatus.Initialized Then
        Return
      End If

      ' Make sure the extension is enabled 
      If m_extensionState <> esriExtensionState.esriESEnabled Then
        Return
      End If

      ' Perform the customization 
      LoadCustomizations()
    End Sub

    Private Sub LoadCustomizations()
      Dim topMenuBar As ICommandBar = GetMainBar()

      If topMenuBar Is Nothing Then
        Return
      End If

      ' Add AcmeMenu 
      Dim uid As UID = New UIDClass()
      uid.Value = c_menuID
      Dim indexObj As Object = Type.Missing
      Dim myMenu As ICommandBar = TryCast(topMenuBar.Add(uid, indexObj), ICommandBar)
      DirectCast(topMenuBar, ICommandItem).Refresh()

      m_isMenuPresent = True
    End Sub

    Private Sub UnLoadCustomizations()
      Dim topMenuBar As ICommandBar = GetMainBar()

      If topMenuBar Is Nothing Then
        Return
      End If

      ' Remove AcmeMenu 
      Dim uid As UID = New UIDClass()
      uid.Value = c_menuID
      Dim myMenu As ICommandBar = TryCast(topMenuBar.Find(uid, False), ICommandBar)
      Dim myMenuItem As ICommandItem = TryCast(myMenu, ICommandItem)

      myMenuItem.Delete()

      DirectCast(topMenuBar, ICommandItem).Refresh()

      m_isMenuPresent = False
    End Sub

    Private Function GetMainBar() As ICommandBar
      Try
        'Grab the root menu bar 
        Dim uid As UID = New UIDClass()
        uid.Value = c_mainMenuID
        Dim mx As MxDocument = DirectCast(m_application.Document, MxDocument)
        Dim cmdBars As ICommandBars = mx.CommandBars
        Dim x As ICommandItem = cmdBars.Find(uid, False, False)
        Return TryCast(cmdBars.Find(uid, False, False), ICommandBar)
      Catch
        Return Nothing
      End Try
    End Function
  End Class
End Namespace


