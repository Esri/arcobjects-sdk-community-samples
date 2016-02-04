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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase

  ''' <summary>
  ''' This command 
  ''' </summary>
<Guid("b358ec88-8304-4d19-a7af-80d63566e4d1"), ClassInterface(ClassInterfaceType.None), ProgId("SimplePointPlugin.AddSimplePointLayerCmd")> _
Public NotInheritable Class AddSimplePointLayerCmd
  Inherits BaseCommand
#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisible(False)> _
  Private Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)

    '
    ' TODO: Add any COM registration code here
    '
  End Sub

  <ComUnregisterFunction(), ComVisible(False)> _
  Private Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)

    '
    ' TODO: Add any COM unregistration code here
    '
  End Sub

#Region "ArcGIS Component Category Registrar generated code"
  ''' <summary>
  ''' Required method for ArcGIS Component Category registration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    MxCommands.Register(regKey)
    ControlsCommands.Register(regKey)
  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    MxCommands.Unregister(regKey)
    ControlsCommands.Unregister(regKey)
  End Sub

#End Region
#End Region

  Private m_hookHelper As IHookHelper = Nothing
  Public Sub New()
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Add SimplePoint layer"
    MyBase.m_message = "Add SimplePoint layer data source to the map"
    MyBase.m_toolTip = "Add SimplePoint layer"
    MyBase.m_name = "SimplePointPlugin_AddSimplePointLayerCmd"

    Try
      Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try
  End Sub

#Region "Overridden Class Methods"

    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If hook Is Nothing Then
            Return
        End If

        Try
            m_hookHelper = New HookHelperClass()
            m_hookHelper.Hook = hook
            If m_hookHelper.ActiveView Is Nothing Then
                m_hookHelper = Nothing
            End If
        Catch
            m_hookHelper = Nothing
        End Try

        If m_hookHelper Is Nothing Then
            MyBase.m_enabled = False
        Else
            MyBase.m_enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Try
            Dim dlg As OpenSimplePointDlg = New OpenSimplePointDlg(m_hookHelper)
            dlg.Show()

            '//get the type using the ProgID
            'Type t = Type.GetTypeFromProgID("esriGeoDatabase.SimplePointPluginWorkspaceFactory");
            '//Use activator in order to create an instance of the workspace factory
            'IWorkspaceFactory workspaceFactory = Activator.CreateInstance(t) as IWorkspaceFactory;

            'string path = GetFileName();
            'if (string.Empty == path)
            '  return;

            '//open the workspace
            'IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(@"C:\Data\Data", 0);

            '//get a featureclass from the workspace
            'IFeatureClass featureClass = featureWorkspace.OpenFeatureClass("points");

            '//create a new feature layer and add it to the map
            'IFeatureLayer featureLayer = new FeatureLayerClass();
            'featureLayer.Name = featureClass.AliasName;
            'featureLayer.FeatureClass = featureClass;
            'm_hookHelper.FocusMap.AddLayer((ILayer)featureLayer);
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message)
        End Try
    End Sub

#End Region
End Class
