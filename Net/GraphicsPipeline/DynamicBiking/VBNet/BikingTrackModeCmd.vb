Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI

	''' <summary>
	''' Summary description for BikingTrackModeCmd.
	''' </summary>
	<Guid("5a26e262-9e4c-498f-b77c-a6fdeee0dd4b"), ClassInterface(ClassInterfaceType.None), ProgId("BikingTrackModeCmd")> _
	Public NotInheritable Class BikingTrackModeCmd : Inherits BaseCommand
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

		Private m_hookHelper As IHookHelper = Nothing

		Private m_dynamicBikingCmd As DynamicBikingCmd = Nothing

		Public Sub New()
			MyBase.m_category = ".NET Samples"
			MyBase.m_caption = "Dynamic biking track"
			MyBase.m_message = "Dynamic biking track mode"
			MyBase.m_toolTip = "Dynamic biking track mode"
			MyBase.m_name = "BikingTrackModeCmd"

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
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        If Not m_dynamicBikingCmd Is Nothing Then
            m_dynamicBikingCmd.TrackMode = Not m_dynamicBikingCmd.TrackMode
        End If
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            m_dynamicBikingCmd = GetBikingCmd()
            If Not m_dynamicBikingCmd Is Nothing Then
                Return m_dynamicBikingCmd.IsPlaying
            End If

            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property Checked() As Boolean
        Get
            If Not m_dynamicBikingCmd Is Nothing Then
                Return m_dynamicBikingCmd.TrackMode
            End If
            Return False
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

