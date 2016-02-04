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
Imports ESRI.ArcGIS.Controls

Namespace TAUpdateControlSample
	''' <summary>
	''' Command that works in ArcMap/Map/PageLayout
	''' </summary>
	<Guid("943e3b92-a090-485f-9a5d-2d0dddc35409"), ClassInterface(ClassInterfaceType.None), ProgId("TAUpdateControlSample.TAUpdateControlCommand")> _
	Public NotInheritable Class TAUpdateControlCommand : Inherits BaseCommand
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

		Private m_TAUpdateControlForm As TAUpdateControlForm = Nothing
		Private m_hookHelper As IHookHelper = Nothing
		Public Sub New()
			'
			' TODO: Define values for the public properties
			'
			MyBase.m_category = ".NET Samples" 'localizable text
			MyBase.m_caption = "Demonstrates the use of the TAUpdateControl." 'localizable text
			MyBase.m_message = "Demonstrates the use of the TAUpdateControl." 'localizable text
			MyBase.m_toolTip = "Demonstrates the use of the TAUpdateControl." 'localizable text
			MyBase.m_name = "TAUpdateControlSample_TAUpdateControlCommand" 'unique id, non-localizable (e.g. "MyCategory_MyCommand")

			Try
				'
				' TODO: change bitmap name if necessary
				'
				Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
				MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
			Catch ex As Exception
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
			End Try

			m_TAUpdateControlForm = New TAUpdateControlForm()
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

			' TODO:  Add other initialization code
		End Sub

		''' <summary>
		''' Occurs when this command is clicked
		''' </summary>
		Public Overrides Sub OnClick()
			If (Not m_TAUpdateControlForm.Visible) Then
				m_TAUpdateControlForm.Show()
				m_TAUpdateControlForm.PopulateDialog()
			Else
				m_TAUpdateControlForm.Hide()
			End If
		End Sub

		Public Overrides ReadOnly Property Checked() As Boolean
			Get
				Return m_TAUpdateControlForm.Visible
			End Get
		End Property
		#End Region
	End Class
End Namespace
