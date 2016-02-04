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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.SystemUI

Namespace SelectionCOMSample
  ''' <summary>
  ''' Summary description for SelectionToolPalette.
  ''' </summary>
  <Guid("23a0177f-f011-434a-b7f3-80d718d93fd0"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.SelectionToolPalette")> _
  Public NotInheritable Class SelectionToolPalette
	  Inherits BaseCommand
    Implements IToolPalette



#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisible(False)> _
    Private Shared Sub RegisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType)

      '
      ' TODO: Add any COM registration code here
      ''
    End Sub

    <ComUnregisterFunction(), ComVisible(False)> _
    Private Shared Sub UnregisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType)

      '
      ' TODO: Add any COM unregistration code here
      ''
    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      MxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_application As IApplication
    Public Sub New()
      MyBase.m_category = "Developer Samples"
      MyBase.m_caption = "Selection Palette"
      MyBase.m_name = "ESRI_SelectionCOMSample_ToolPalette"
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

            m_application = TryCast(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If
        End Sub

        ''' <summary>
        ''' Occurs when this command is clicked
        ''' </summary>
        Public Overrides Sub OnClick()
        End Sub

#End Region

#Region "IToolPalette Members"

    Public ReadOnly Property MenuStyle() As Boolean Implements ESRI.ArcGIS.SystemUI.IToolPalette.MenuStyle
      Get
        Return False
      End Get
    End Property

    Public ReadOnly Property PaletteColumns() As Integer Implements ESRI.ArcGIS.SystemUI.IToolPalette.PaletteColumns
      Get
        Return 2
      End Get
    End Property

    Public ReadOnly Property PaletteItemCount() As Integer Implements ESRI.ArcGIS.SystemUI.IToolPalette.PaletteItemCount
      Get
        Return 3
      End Get
    End Property

    Public ReadOnly Property TearOff() As Boolean Implements ESRI.ArcGIS.SystemUI.IToolPalette.TearOff
      Get
        Return False
      End Get
    End Property

    Public ReadOnly Property PaletteItem(ByVal pos As Integer) As String Implements ESRI.ArcGIS.SystemUI.IToolPalette.PaletteItem
      Get
        Select Case pos
          Case 0
            Return "esriArcMapUI.SelectByPolygonTool"
          Case 1
            Return "esriArcMapUI.SelectByLayerCommand"
          Case 2
            Return "SelectionCOMSample.SelectByLineTool"
          Case Else
            Return ""
        End Select
      End Get
    End Property
#End Region

  End Class
End Namespace
