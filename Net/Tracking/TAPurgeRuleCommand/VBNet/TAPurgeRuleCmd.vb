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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto

Namespace TAPurgeRuleCommand
	''' <summary>
	''' Command that works in ArcMap/Map/PageLayout, ArcScene/SceneControl
	''' or ArcGlobe/GlobeControl
	''' </summary>
	<Guid("4a9ae2c3-dfdb-4b55-922d-558a1a9ccfe1"), ClassInterface(ClassInterfaceType.None), ProgId("TAPurgeRuleCommand.TAPurgeRuleCmd")> _
	Public NotInheritable Class TAPurgeRuleCmd : Inherits BaseCommand
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
			GMxCommands.Register(regKey)
			MxCommands.Register(regKey)
			SxCommands.Register(regKey)
			ControlsCommands.Register(regKey)
		End Sub
		''' <summary>
		''' Required method for ArcGIS Component Category unregistration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			GMxCommands.Unregister(regKey)
			MxCommands.Unregister(regKey)
			SxCommands.Unregister(regKey)
			ControlsCommands.Unregister(regKey)
		End Sub

		#End Region
		#End Region

		Private m_hookHelper As IHookHelper = Nothing
		Private m_globeHookHelper As IGlobeHookHelper = Nothing
		Private Const TEMPORALLAYERCLSID As String = "{78C7430C-17CF-11D5-B7CF-00010265ADC5}" 'CLSID for ITemporalLayer
		Private m_PRForm As PurgeRuleForm

		Public Sub New()
			'
			' TODO: Define values for the public properties
			'
			MyBase.m_category = ".NET Samples" 'localizable text
			MyBase.m_caption = "Change the purge rule for temporal layers" 'localizable text
			MyBase.m_message = "Change the purge rule for temporal layers" 'localizable text
			MyBase.m_toolTip = "Change the purge rule for temporal layers" 'localizable text
			MyBase.m_name = "TAPurgeRuleCommand_TAPurgeRuleCmd" 'unique id, non-localizable (e.g. "MyCategory_MyCommand")

			m_PRForm = New PurgeRuleForm()

			Try
				'
				' TODO: change bitmap name if necessary
				'
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

            ' Test the hook that calls this command and disable if nothing is valid
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
                'Can be globe
                Try
                    m_globeHookHelper = New GlobeHookHelperClass()
                    m_globeHookHelper.Hook = hook
                    If m_globeHookHelper.ActiveViewer Is Nothing Then
                        m_globeHookHelper = Nothing
                    End If
                Catch
                    m_globeHookHelper = Nothing
                End Try
            End If

            If m_globeHookHelper Is Nothing AndAlso m_hookHelper Is Nothing Then
                MyBase.m_enabled = False
            Else
                MyBase.m_enabled = True
            End If

            'TODO: Add other initialization code
        End Sub

        ''' <summary>
        ''' Occurs when this command is clicked
        ''' </summary>
        Public Overrides Sub OnClick()
            'Show the dialog, pass it the temporal layers from the map, have it initialize the dialog
            If (Not m_PRForm.Visible) Then
                m_PRForm.Show()
                m_PRForm.TrackingLayers = GetAllTrackingLayers()
                m_PRForm.PopulateDialog()
            Else
                m_PRForm.Hide()
            End If
        End Sub

        'Show the command as depressed when the dialog is visible
        Public Overrides ReadOnly Property Checked() As Boolean
            Get
                Return m_PRForm.Visible
            End Get
        End Property
#End Region

		'Query the map for all the tracking layers in it
		Private Function GetAllTrackingLayers() As IEnumLayer
			Dim eLayers As IEnumLayer = Nothing
			Try
				Dim basicMap As IBasicMap = Nothing
				Dim uidTemoralLayer As IUID = New UIDClass()
				uidTemoralLayer.Value = TEMPORALLAYERCLSID

				If Not m_hookHelper Is Nothing Then

					basicMap = TryCast(m_hookHelper.FocusMap, IBasicMap)
				ElseIf Not m_globeHookHelper Is Nothing Then
					basicMap = TryCast(m_globeHookHelper.Globe, IBasicMap)
				End If

				'This call throws an E_FAIL exception if the map has no layers, caught below
				If Not basicMap Is Nothing Then
                    eLayers = basicMap.Layers(CType(uidTemoralLayer, UID), True)
				End If
			Catch
			End Try

			Return eLayers
		End Function

	End Class
End Namespace
