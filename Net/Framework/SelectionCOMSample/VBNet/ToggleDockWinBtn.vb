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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

Namespace SelectionCOMSample
  ''' <summary>
  ''' Summary description for ToggleDockWinBtn.
  ''' </summary>
  <Guid("26cd6ddc-c2d0-4102-a853-5f7043c6e797"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.ToggleDockWinBtn")> _
  Public NotInheritable Class ToggleDockWinBtn
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
	Private m_mainExtension As SelectionExtension
	Private m_dockWindow As IDockableWindow

	Public Sub New()
	  MyBase.m_category = "Developer Samples"
      MyBase.m_caption = "Toggle Dockable Window VB.NET"
      MyBase.m_message = "Toggle dockable window VB.NET."
      MyBase.m_toolTip = "Toggle dockable window VB.NET." & Constants.vbCrLf & "Selection Sample Extension needs to be turned on in Extensions dialog."
	  MyBase.m_name = "ESRI_SelectionCOMSample_ToggleDockWinBtn"

      Try
        MyBase.m_bitmap = New Bitmap(Me.GetType().Assembly.GetManifestResourceStream("ToggleDockWinBtn.png"))
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

            m_application = TryCast(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If

            m_mainExtension = SelectionExtension.GetExtension()

            If m_mainExtension IsNot Nothing Then
                m_dockWindow = m_mainExtension.GetSelectionCountWindow
            End If

        End Sub

        ''' <summary>
        ''' Occurs when this command is clicked
        ''' </summary>
        Public Overrides Sub OnClick()
            If m_dockWindow Is Nothing Then
                Return
            End If

            m_dockWindow.Show((Not m_dockWindow.IsVisible()))

        End Sub

        Public Overrides ReadOnly Property Enabled() As Boolean
            Get
                If m_mainExtension Is Nothing OrElse m_dockWindow Is Nothing Then
                    Return False
                Else
                    Return m_mainExtension.IsExtensionEnabled
                End If
            End Get
        End Property
#End Region
  End Class
End Namespace
