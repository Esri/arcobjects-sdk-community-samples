Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.SystemUI

  ''' <summary>
  ''' Summary description for CreateNewDocument.
  ''' </summary>
  Public Class CreateNewDocument : Inherits BaseCommand
	Private m_hookHelper As IHookHelper = Nothing

	'Ctor
	Public Sub New()
	  'update the base properties
	  MyBase.m_category = ".NET Samples"
	  MyBase.m_caption = "NewDocument"
	  MyBase.m_message = "Create a new map"
	  MyBase.m_toolTip = "Create a new map"
	  MyBase.m_name = MyBase.m_category & "_" & MyBase.m_caption
	End Sub

#Region "Overridden Class Methods"

    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If m_hookHelper Is Nothing Then
            m_hookHelper = New HookHelperClass()
        End If

        m_hookHelper.Hook = hook
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Dim mapControl As IMapControl3 = Nothing

        'get the MapControl from the hook in case the container is a ToolbatControl
        If TypeOf m_hookHelper.Hook Is IToolbarControl Then
            mapControl = CType((CType(m_hookHelper.Hook, IToolbarControl)).Buddy, IMapControl3)
            'In case the container is MapControl
        ElseIf TypeOf m_hookHelper.Hook Is IMapControl3 Then
            mapControl = CType(m_hookHelper.Hook, IMapControl3)
        Else
            MessageBox.Show("Active control must be MapControl!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        'allow the user to save the current document
        Dim res As DialogResult = MessageBox.Show("Would you like to save the current document?", "AoView", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If res = DialogResult.Yes Then
            'launch the save command (why work hard!?)
            Dim command As ICommand = New ControlsSaveAsDocCommandClass()
            command.OnCreate(m_hookHelper.Hook)
            command.OnClick()
        End If

        'create a new Map
        Dim map As IMap = New MapClass()
        map.Name = "Map"

        'assign the new map to the MapControl
        mapControl.DocumentFilename = String.Empty
        mapControl.Map = map
    End Sub

#End Region
  End Class
