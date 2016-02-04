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
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Controls
 
Namespace Core
  
  <Guid("C219533C-2B2C-4DC9-9D85-17656C1EAD63"), ClassInterface(ClassInterfaceType.None), ProgId("SnapCommands_VB.SnapSettingsCommand")> _
  Public Class SnapSettingsCommand
    Inherits BaseCommand

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType)
    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType)
    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    '/ <summary>
    '/ Required method for ArcGIS Component Category registration -
    '/ Do not modify the contents of this method with the code editor.
    '/ </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      ControlsCommands.Register(regKey)
    End Sub
    '/ <summary>
    '/ Required method for ArcGIS Component Category unregistration -
    '/ Do not modify the contents of this method with the code editor.
    '/ </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      ControlsCommands.Unregister(regKey)
    End Sub
#End Region
#End Region

    Dim snapEditor As SnapEditor

    Public Sub New()
    End Sub


#Region "ICommand Members"

    Public Overrides Sub OnClick()
      'The snap editor form requires an edit session (e.g. in order to read the target layer and to set the snap tips)
      Dim editor As IEngineEditor = New EngineEditorClass()
      If editor.EditState <> esriEngineEditState.esriEngineStateEditing Then
        System.Windows.Forms.MessageBox.Show("Please start an edit session")
        Return
      End If

      'Show the snap editor form
      If ((snapEditor Is Nothing) OrElse (snapEditor.IsDisposed)) Then
        snapEditor = New SnapEditor()
      End If
      snapEditor.Show()
      snapEditor.BringToFront()
    End Sub

    Public Overrides ReadOnly Property Message() As String
      Get
        Return "SnapSettingsCommand"
      End Get
    End Property

    Public Overrides ReadOnly Property Bitmap() As Integer
      Get
        Return 0
      End Get
    End Property

    '/ <summary>
    '/ Occurs when this command is created
    '/ </summary>
    '/ <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
      If hook Is Nothing Then
        Return
      End If
    End Sub

    Public Overrides ReadOnly Property Caption() As String
      Get
        Return "SnapSettingsCommandVB"
      End Get
    End Property

    Public Overrides ReadOnly Property Tooltip() As String
      Get
        Return "SnapSettingsCommand"
      End Get
    End Property

    Public Overrides ReadOnly Property HelpContextID() As Integer
      Get
        Return 0
      End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
      Get
        Return "SnapCommands_VB_SnapSettingsCommand"
      End Get
    End Property

    Public Overrides ReadOnly Property Checked() As Boolean
      Get
        Return False
      End Get
    End Property

    Public Overrides ReadOnly Property Enabled() As Boolean
      Get
        Return True
      End Get
    End Property

    Public Overrides ReadOnly Property HelpFile() As String
      Get
        Return Nothing
      End Get
    End Property

    Public Overrides ReadOnly Property Category() As String
      Get
        Return "Developer Samples"
      End Get
    End Property
#End Region
  End Class
End Namespace

'----------------------------------------------------------------
' Converted from C# to VB .NET using CSharpToVBConverter(1.2).
' Developed by: Kamal Patel (http://www.KamalPatel.net) 
'----------------------------------------------------------------
