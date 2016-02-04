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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(AddShapefile.ClassId, AddShapefile.InterfaceId, AddShapefile.EventsId), _
 ProgId("CustomMenus.AddShapefile")> _
Public NotInheritable Class AddShapefile
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "6f8f48fe-bfbc-4b2a-b1e7-25eced548743"
    Public Const InterfaceId As String = "8390d0e1-54f5-4ec3-b20f-3ff96f1f37f6"
    Public Const EventsId As String = "d87cda80-8257-4b7f-abb0-3c08d6b0ddde"
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
    MyBase.m_caption = "Add Shapefile"
    MyBase.m_message = "Adds a shapefile to the focus map"
    MyBase.m_toolTip = "Add Shapefile"
    MyBase.m_name = "CustomMenus_AddShapefile"

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
    Dim mxDocument As IMxDocument = TryCast(m_application.Document, IMxDocument)
    AddShapefileUsingOpenFileDialog(mxDocument.ActiveView)
  End Sub


    '##### ArcGIS Snippets #####

#Region "Add Shapefile Using OpenFileDialog"
    ' ArcGIS Snippet Title:
    ' Add Shapefile Using OpenFileDialog
    ' 
    ' Long Description:
    ' Add a shapefile to the ActiveView using the Windows.Forms.OpenFileDialog control.
    ' 
    ' Add the following references to the project:
    ' ESRI.ArcGIS.Carto
    ' ESRI.ArcGIS.DataSourcesFile
    ' ESRI.ArcGIS.Geodatabase
    ' System.Windows.Forms
    ' 
    ' Intended ArcGIS Products for this snippet:
    ' ArcGIS Desktop (Standard, Advanced, Basic)
    ' ArcGIS Engine
    ' 
    ' Applicable ArcGIS Product Versions:
    ' 9.2
    ' 9.3
    ' 
    ' Required ArcGIS Extensions:
    ' (NONE)
    ' 
    ' Notes:
    ' This snippet is intended to be inserted at the base level of a Class.
    ' It is not intended to be nested within an existing Function or Sub.
    ' 

    '''<summary>Add a shapefile to the ActiveView using the Windows.Forms.OpenFileDialog control.</summary>
    '''
    '''<param name="activeView">An IActiveView interface</param>
    ''' 
    '''<remarks></remarks>
    Public Sub AddShapefileUsingOpenFileDialog(ByVal activeView As ESRI.ArcGIS.Carto.IActiveView)

        'parameter check
        If activeView Is Nothing Then

            Return

        End If

        ' Use the OpenFileDialog Class to choose which shapefile to load.
        Dim openFileDialog As System.Windows.Forms.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        openFileDialog.InitialDirectory = "c:\"
        openFileDialog.Filter = "Shapefiles (*.shp)|*.shp"
        openFileDialog.FilterIndex = 2
        openFileDialog.RestoreDirectory = True
        openFileDialog.Multiselect = False

        If openFileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then

            ' The user chose a particular shapefile.

            ' The returned string will be the full path, filename and file-extension for the chosen shapefile. Example: "C:\test\cities.shp"
            Dim shapefileLocation As String = openFileDialog.FileName

            If shapefileLocation <> "" Then

                ' Ensure the user chooses a shapefile

                ' Create a new ShapefileWorkspaceFactory CoClass to create a new workspace
                Dim workspaceFactory As ESRI.ArcGIS.Geodatabase.IWorkspaceFactory = New ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass

                ' System.IO.Path.GetDirectoryName(shapefileLocation) returns the directory part of the string. Example: "C:\test\"
                Dim featureWorkspace As ESRI.ArcGIS.Geodatabase.IFeatureWorkspace = CType(workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(shapefileLocation), 0), ESRI.ArcGIS.Geodatabase.IFeatureWorkspace) ' Explicit Cast

                ' System.IO.Path.GetFileNameWithoutExtension(shapefileLocation) returns the base filename (without extension). Example: "cities"
                Dim featureClass As ESRI.ArcGIS.Geodatabase.IFeatureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(shapefileLocation))

                Dim featureLayer As ESRI.ArcGIS.Carto.IFeatureLayer = New ESRI.ArcGIS.Carto.FeatureLayerClass
                featureLayer.FeatureClass = featureClass
                featureLayer.Name = featureClass.AliasName
                featureLayer.Visible = True
                activeView.FocusMap.AddLayer(featureLayer)

                ' Zoom the display to the full extent of all layers in the map
                activeView.Extent = activeView.FullExtent
                activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, Nothing, Nothing)

            Else

                ' The user did not choose a shapefile.
                ' Do whatever remedial actions as necessary
                ' System.Windows.Forms.MessageBox.Show("No shapefile chosen", "No Choice #1", System.Windows.Forms.MessageBoxButtons.OK,  System.Windows.Forms.MessageBoxIcon.Exclamation)

            End If

        Else

            ' The user did not choose a shapefile. They clicked Cancel or closed the dialog by the "X" button.
            ' Do whatever remedial actions as necessary.
            ' System.Windows.Forms.MessageBox.Show("No shapefile chosen", "No Choice #2", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation)

        End If

    End Sub
#End Region

End Class



