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
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.esriSystem
<ComClass(AddEXTCLSID.ClassId, AddEXTCLSID.InterfaceId, AddEXTCLSID.EventsId), _
 ProgId("TabbedFeatureInspectorVBNet.AddEXTCLSID")> _
Public NotInheritable Class AddEXTCLSID
  Inherits BaseCommand

#Region "COM GUIDs"
  ' These  GUIDs provide the COM identity for this class 
  ' and its COM interfaces. If you change them, existing 
  ' clients will no longer be able to access the class.
  Public Const ClassId As String = "6d73c609-8f19-4231-81a1-ba1c6b40ff12"
  Public Const InterfaceId As String = "98a7646a-d5ff-4424-a279-9626f1b83d81"
  Public Const EventsId As String = "5a687ef0-b670-4b4b-b072-199612f862f9"
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

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()
    MyBase.m_category = "Developer Samples"
    MyBase.m_caption = "Add EXTCLSID"
    MyBase.m_message = "This command adds the GUID of the project to the EXTCLSID"
    MyBase.m_toolTip = "Adds EXTCLSID to feature class"
    MyBase.m_name = "TabbedFeatureInspectorVBNet_AddEXTCLSID"
   
    Try
      Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try

  End Sub


  Public Overrides Sub OnCreate(ByVal hook As Object)
    If Not hook Is Nothing Then
      m_application = CType(hook, IApplication)

      'Disable if it is not ArcMap
      If TypeOf hook Is IMxApplication Then
        MyBase.m_enabled = True
      Else
        MyBase.m_enabled = False
      End If
    End If
  End Sub

  Public Overrides Sub OnClick()
    Try
      Dim pGxdialog As IGxDialog = New GxDialogClass()
      Dim mdbFilter As IGxObjectFilter = New GxFilterPGDBFeatureClasses
      pGxdialog.Title = "Pick the feature class you want to add the ext clsid to"
      Dim pEnumGx As IEnumGxObject
      pGxdialog.ObjectFilter = mdbFilter
      pGxdialog.StartingLocation = "c:\"
      If Not pGxdialog.DoModalOpen(0, pEnumGx) Then
        Exit Sub
      End If

      Dim gdbObj As IGxObject = pEnumGx.Next
      'Make sure there was only one GxObject in the enum.
      If Not pEnumGx.Next Is Nothing Then
        Return
      End If
      'Get the Name for the internal object that this GxObject represents.
      Dim fcName As IName = gdbObj.InternalObjectName
      'Opens the object referred to by this name
      Dim featClass As IFeatureClass = fcName.Open
      'Procedure to add the class id to the feature class internally.
      IClassSchemaEdit_Example(featClass)
    Catch Ex As AccessViolationException
            MessageBox.Show("Attempt to read or write protected memory. Original error: " & Ex.Message)
    Catch sEx As Exception
      MessageBox.Show("Cannot read file from disk. Original error: " & sEx.Message)
    End Try
  End Sub

  Public Sub IClassSchemaEdit_Example(ByVal featClass As IObjectClass)
    'This function shows how you can use the IClassSchemaEdit   
    'interface to alter the COM class extension for an object class.    
    'cast for the IClassSchemaEdit      
    Dim classSchemaEdit As IClassSchemaEdit = CType(featClass, IClassSchemaEdit)
    'set and exclusive lock on the class     
    Dim schemaLock As ISchemaLock = CType(featClass, ISchemaLock)
    schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock)
    Dim classUID As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass()
    'GUID for the VBNet project.
    classUID.Value = "{2cfe1569-8732-4e73-ac8b-31b87be9631b}"
    classSchemaEdit.AlterClassExtensionCLSID(classUID, Nothing)
    'release the exclusive lock     
    schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock)
    MessageBox.Show("Successfully added the extension class id to the feature class")
  End Sub
End Class



