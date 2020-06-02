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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

  ''' <summary>
  ''' This command demonstrates saving of a layer file in an ArcGIS Engine application
  ''' </summary>
  <Guid("294563e7-b475-43db-a2d1-a6b5f95c7113"), ClassInterface(ClassInterfaceType.None), ProgId("MapControlSaveLayerFile.SaveLayerFileCmd")> _
  Public NotInheritable Class SaveLayerFileCmd : Inherits BaseCommand
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
	  MyBase.m_caption = "Create Layer File"
	  MyBase.m_message = "Save the current layer as a layer file"
	  MyBase.m_toolTip = "Create As Layer File"
	  MyBase.m_name = "MapControlSaveLayerFile_SaveLayerFileCmd"

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
        'need to get the layer from the custom-property of the map
        If Nothing Is m_hookHelper Then
            Return
        End If

        'get the mapControl hook
        Dim hook As Object = Nothing
        If TypeOf m_hookHelper.Hook Is IToolbarControl2 Then
            hook = (CType(m_hookHelper.Hook, IToolbarControl2)).Buddy
        Else
            hook = m_hookHelper.Hook
        End If

        'get the custom property from which is supposed to be the layer to be saved
        Dim customProperty As Object = Nothing
        Dim mapControl As IMapControl3 = Nothing
        If TypeOf hook Is IMapControl3 Then
            mapControl = CType(hook, IMapControl3)
            customProperty = mapControl.CustomProperty
        Else
            Return
        End If

        If Nothing Is customProperty OrElse Not (TypeOf customProperty Is ILayer) Then
            Return
        End If

        'ask the user to set a name for the new layer file
        Dim saveFileDialog As SaveFileDialog = New SaveFileDialog()
        saveFileDialog.Filter = "Layer File|*.lyr|All Files|*.*"
        saveFileDialog.Title = "Create Layer File"
        saveFileDialog.RestoreDirectory = True
        saveFileDialog.FileName = System.IO.Path.Combine(saveFileDialog.InitialDirectory, (CType(customProperty, ILayer)).Name & ".lyr")

        'get the layer name from the user
        Dim dr As DialogResult = saveFileDialog.ShowDialog()
        If saveFileDialog.FileName <> "" AndAlso dr = System.Windows.Forms.DialogResult.OK Then
            If System.IO.File.Exists(saveFileDialog.FileName) Then
                'try to delete the existing file
                System.IO.File.Delete(saveFileDialog.FileName)
            End If

            'create a new LayerFile instance
            Dim layerFile As ILayerFile = New LayerFileClass()
            'create a new layer file
            layerFile.New(saveFileDialog.FileName)
            'attach the layer file with the actual layer
            layerFile.ReplaceContents(CType(customProperty, ILayer))
            'save the layer file
            layerFile.Save()

            'ask the user whether he'd like to add the layer to the map
            If DialogResult.Yes = MessageBox.Show("Would you like to add the layer to the map?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                mapControl.AddLayerFromFile(saveFileDialog.FileName, 0)
            End If
        End If
    End Sub

#End Region
  End Class
