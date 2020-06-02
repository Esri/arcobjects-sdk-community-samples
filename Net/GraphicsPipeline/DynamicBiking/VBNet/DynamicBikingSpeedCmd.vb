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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.SystemUI

	''' <summary>
	''' Summary description for DynamicBikingSpeedCmd.
	''' </summary>
	<Guid("bbf77b3c-a5c1-4911-90ca-78961238fef0"), ClassInterface(ClassInterfaceType.None), ProgId("DynamicBikingSpeedCmd")> _
 Public NotInheritable Class DynamicBikingSpeedCmd : Inherits BaseCommand : Implements IToolControl
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
			ControlsCommands.Register(regKey)

		End Sub
		''' <summary>
		''' Required method for ArcGIS Component Category unregistration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			ControlsCommands.Unregister(regKey)

		End Sub

		#End Region
		#End Region

		Private m_hookHelper As IHookHelper
		Private m_bikingSpeedCtrl As DynamicBikingSpeedCtrl = Nothing
		Private m_dynamicBikingCmd As DynamicBikingCmd = Nothing

		Public Sub New()
			MyBase.m_category = ".NET Samples"
			MyBase.m_caption = "Dynamic Biking Speed"
			MyBase.m_message = "Dynamic Biking Speed"
			MyBase.m_toolTip = "Dynamic Biking Speed"
			MyBase.m_name = "DynamicBiking_DynamicBikingSpeedCmd"

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

        If m_hookHelper Is Nothing Then
            m_hookHelper = New HookHelperClass()
        End If

        m_hookHelper.Hook = hook

        'make sure that the usercontrol has been initialized
        If Nothing Is m_bikingSpeedCtrl Then
            m_bikingSpeedCtrl = New DynamicBikingSpeedCtrl()
            m_bikingSpeedCtrl.CreateControl()
        End If
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()

    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            m_dynamicBikingCmd = GetBikingCmd()
            If Not m_dynamicBikingCmd Is Nothing Then
                Dim bEnabled As Boolean = m_dynamicBikingCmd.IsPlaying
                m_bikingSpeedCtrl.Enabled = bEnabled
                m_bikingSpeedCtrl.SetDynamicBikingCmd(m_dynamicBikingCmd)

                Return bEnabled
            End If

            Return False
        End Get
    End Property

#End Region

		#Region "IToolControl Members"

		Public Function OnDrop(ByVal barType As esriCmdBarType) As Boolean Implements IToolControl.OnDrop
			Return True
		End Function

		Public Sub OnFocus(ByVal complete As ICompletionNotify) Implements IToolControl.OnFocus

		End Sub

		Public ReadOnly Property hWnd() As Integer Implements IToolControl.hWnd
			Get
				'pass the handle of the usercontrol
				If Nothing Is m_bikingSpeedCtrl Then
					m_bikingSpeedCtrl = New DynamicBikingSpeedCtrl()
					m_bikingSpeedCtrl.CreateControl()
				End If

				Return m_bikingSpeedCtrl.Handle.ToInt32()
			End Get
		End Property

		#End Region

		Private Function GetBikingCmd() As DynamicBikingCmd
			If m_hookHelper.Hook Is Nothing Then
				Return Nothing
			End If

			Dim dynamicBikingCmd As DynamicBikingCmd = Nothing
			If TypeOf m_hookHelper.Hook Is IToolbarControl2 Then
				Dim toolbarCtrl As IToolbarControl2 = CType(m_hookHelper.Hook, IToolbarControl2)
				Dim commandPool As ICommandPool2 = TryCast(toolbarCtrl.CommandPool, ICommandPool2)
				Dim commantCount As Integer = commandPool.Count
				Dim command As ICommand = Nothing
				Dim i As Integer = 0
				Do While i < commantCount
					command = commandPool.Command(i)
					If TypeOf command Is DynamicBikingCmd Then
						dynamicBikingCmd = CType(command, DynamicBikingCmd)
					End If
					i += 1
				Loop
			End If

			Return dynamicBikingCmd
		End Function
	End Class

