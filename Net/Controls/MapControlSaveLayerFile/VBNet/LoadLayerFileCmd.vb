Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls

  ''' <summary>
  ''' Summary description for LoadLayerFileCmd.
  ''' </summary>
  <Guid("c45d8097-5c9b-443c-8859-1663b7b2c540"), ClassInterface(ClassInterfaceType.None), ProgId("MapControlSaveLayerFile.LoadLayerFileCmd")> _
  Public NotInheritable Class LoadLayerFileCmd : Inherits BaseCommand
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

	Public Sub New()
	  MyBase.m_category = ".NET Samples"
	  MyBase.m_caption = "Load Layer File"
	  MyBase.m_message = "Load Layer File from disk"
	  MyBase.m_toolTip = "Load Layer File"
	  MyBase.m_name = "MapControlSaveLayerFile_LoadLayerFileCmd"

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

        If m_hookHelper Is Nothing Then
            m_hookHelper = New HookHelperClass()
        End If

        m_hookHelper.Hook = hook

        ' TODO:  Add other initialization code
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

        Dim ofdlg As OpenFileDialog = New OpenFileDialog()
        ofdlg.Multiselect = True
        ofdlg.RestoreDirectory = True
        ofdlg.Title = "Load Layer Files"
        ofdlg.Filter = "Layer File|*.lyr|All Files|*.*"
        Dim dr As DialogResult = ofdlg.ShowDialog()
        If System.Windows.Forms.DialogResult.OK = dr Then
            Dim files As String() = ofdlg.FileNames

            For Each file As String In files
                If System.IO.File.Exists(file) Then
                    mapControl.AddLayerFromFile(file, 0)
                End If
            Next file
        End If
    End Sub

#End Region
  End Class
