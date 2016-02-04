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
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.SystemUI


<ComClass(RecentFilesCombo.ClassId, RecentFilesCombo.InterfaceId, RecentFilesCombo.EventsId), _
 ProgId("RecentFilesCommandsVB.RecentFilesCombo")> _
Public NotInheritable Class RecentFilesCombo
  Inherits BaseCommand
  Implements IComboBox


#Region "COM GUIDs"
  ' These  GUIDs provide the COM identity for this class 
  ' and its COM interfaces. If you change them, existing 
  ' clients will no longer be able to access the class.
  Public Const ClassId As String = "3b7bd3f6-246d-4761-8996-aa1409281cef"
  Public Const InterfaceId As String = "e88f97f3-afc2-41a0-a6a0-6375f1a3d6cf"
  Public Const EventsId As String = "077db9cd-d208-4452-8266-26b4fbae5ff0"
#End Region

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
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    MxCommands.Register(regKey)

  End Sub
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    MxCommands.Unregister(regKey)

  End Sub

#End Region
#End Region


  Private m_application As IApplication
  Private m_itemMap As System.Collections.Generic.Dictionary(Of Integer, String)
  Private m_strWidth As String = "c:\documents\map documents"

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()

    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Recent Documents: "
    MyBase.m_message = "Recent Documents"
    MyBase.m_toolTip = "Recent Documents"
    MyBase.m_name = "RecentDocsCombo"

    m_itemMap = New System.Collections.Generic.Dictionary(Of Integer, String)()

  End Sub


  Public Overrides Sub OnCreate(ByVal hook As Object)
    Dim comboHook As IComboBoxHook = TryCast(hook, IComboBoxHook)
    If comboHook Is Nothing Then
      m_enabled = False
      Return
    End If

    m_application = TryCast(comboHook.Hook, IApplication)

    Dim cookie As Integer = 0

    For Each fileName As String In RecentFilesRegistryHelper.GetRecentFiles(m_application)
      If File.Exists(fileName) Then
        'Add item to list 
        cookie = comboHook.Add(fileName)
        m_itemMap.Add(cookie, fileName)
      End If
    Next

  End Sub

  Public Overrides Sub OnClick()
  End Sub

  Public ReadOnly Property DropDownHeight() As Integer Implements ESRI.ArcGIS.SystemUI.IComboBox.DropDownHeight
    Get
      Return 50
    End Get
  End Property

  Public ReadOnly Property DropDownWidth() As String Implements ESRI.ArcGIS.SystemUI.IComboBox.DropDownWidth
    Get
      Return m_strWidth
    End Get
  End Property

  Public ReadOnly Property Editable() As Boolean Implements ESRI.ArcGIS.SystemUI.IComboBox.Editable
    Get
      Return False
    End Get
  End Property

  Public ReadOnly Property HintText() As String Implements ESRI.ArcGIS.SystemUI.IComboBox.HintText
    Get
      Return "Select document"
    End Get
  End Property

  Public Sub OnEditChange(ByVal editString As String) Implements ESRI.ArcGIS.SystemUI.IComboBox.OnEditChange

  End Sub

  Public Sub OnEnter() Implements ESRI.ArcGIS.SystemUI.IComboBox.OnEnter

  End Sub

  Public Sub OnFocus(ByVal [set] As Boolean) Implements ESRI.ArcGIS.SystemUI.IComboBox.OnFocus

  End Sub

  Public Sub OnSelChange(ByVal cookie As Integer) Implements ESRI.ArcGIS.SystemUI.IComboBox.OnSelChange
    Dim selectedPath As String = m_itemMap(cookie)
    m_application.OpenDocument(selectedPath)
  End Sub

  Public ReadOnly Property ShowCaption() As Boolean Implements ESRI.ArcGIS.SystemUI.IComboBox.ShowCaption
    Get
      Return False
    End Get
  End Property

  Public ReadOnly Property Width() As String Implements ESRI.ArcGIS.SystemUI.IComboBox.Width
    Get
      Return m_strWidth
    End Get
  End Property
End Class



