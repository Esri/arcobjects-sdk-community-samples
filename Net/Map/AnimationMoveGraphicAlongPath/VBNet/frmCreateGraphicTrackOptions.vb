Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

Partial Public Class frmCreateGraphicTrackOptions : Inherits Form
    Public createTrackOptions As ICreateGraphicTrackOptions
    Public AnimationExtension As IAnimationExtension
    Public lineFeature As IGeometry
    Public lineGraphic As ILineElement
    Public pointGraphic As IElement

    Public Sub New()
        InitializeComponent()
        createTrackOptions = New CreateGraphicTrackOptions()
        lineFeature = Nothing
        lineGraphic = Nothing
        pointGraphic = Nothing
    End Sub

    Private Sub buttonCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles buttonCancel.Click
        Me.Close()
    End Sub

    Private Sub buttonImport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles buttonImport.Click
        createTrackOptions.OverwriteTrack = Me.checkBoxOverwriteTrack.Checked
        createTrackOptions.ReverseOrder = Me.checkBoxReverseOrder.Checked
        createTrackOptions.TrackName = Me.textBoxTrackName.Text
        createTrackOptions.SimplificationFactor = CDbl(Me.trackBar1.Value) / 100.0
        createTrackOptions.PointElement = pointGraphic
        createTrackOptions.LineElement = lineGraphic
        createTrackOptions.AnimatePath = Me.checkBoxTracePath.Checked

        If Me.radioButtonLineFeature.Checked Then
            createTrackOptions.PathGeometry = lineFeature
        ElseIf Me.radioButtonLineGraphic.Checked Then
            Dim temp As IElement = CType(lineGraphic, IElement)
            createTrackOptions.PathGeometry = temp.Geometry
        End If

        Dim tracks As IAGAnimationTracks = AnimationExtension.AnimationTracks
        Dim pContainer As IAGAnimationContainer = tracks.AnimationObjectContainer

        AnimationUtils.CreateMapGraphicTrack(createTrackOptions, tracks, pContainer)

        AnimationExtension.AnimationContentsModified()
        Me.Close()
    End Sub

    Private Sub frmCreateGraphicTrackOptions_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim tracks As IAGAnimationTracks = AnimationExtension.AnimationTracks
        Dim i As Integer = 1
        Dim recommendedTrackName As String = "Map Graphic track " & i
        Do While CheckTrackName(tracks, recommendedTrackName)
            i += 1
            recommendedTrackName = "Map Graphic track " & i
        Loop
        Me.textBoxTrackName.Text = recommendedTrackName
        Me.checkBoxOverwriteTrack.Checked = False
        Me.checkBoxReverseOrder.Checked = False
        Me.checkBoxTracePath.Checked = False
        Me.trackBar1.Minimum = 0
        Me.trackBar1.Maximum = 100

        RefreshPathSourceOptions()

        helpProvider1.SetHelpString(Me.radioButtonLineFeature, "Use a selected line feature as the path source.")
        helpProvider1.SetHelpString(Me.radioButtonLineGraphic, "Use a selected line graphic as the path source.")
        helpProvider1.SetHelpString(Me.checkBoxOverwriteTrack, "Check to overwrite existing tracks that have the same name as specified.")
        helpProvider1.SetHelpString(Me.checkBoxReverseOrder, "Check to create a track that moves the graphic in a reversed direction.")
        helpProvider1.SetHelpString(Me.checkBoxTracePath, "Check to show the trace of the moving point graphic in the animation. By default, the trace will be shown as a red dashed line following the path of the point graphic. The symbology of the trace can be changed in the display window after you play or preview the animation once.")
        helpProvider1.SetHelpString(Me.trackBar1, "With a non-zero simplification factor, the line will be simplified and smoother.")
        helpProvider1.SetHelpString(Me.textBoxTrackName, "Type a name for the track.")
    End Sub

    'The following function check if a track name exists
    Private Function CheckTrackName(ByVal pTracks As IAGAnimationTracks, ByVal name As String) As Boolean
        Dim trackArray As IArray = pTracks.AGTracks
        Dim pTrack As IAGAnimationTrack
        Dim count As Integer = pTracks.TrackCount
        Dim trackExist As Boolean = False
        Dim i As Integer = 0
        Do While i < count
            pTrack = CType(trackArray.Element(i), IAGAnimationTrack)
            If name = pTrack.Name Then
                trackExist = True
                Exit Do
            End If
            i += 1
        Loop
        Return trackExist
    End Function

    Public Sub RefreshPathSourceOptions()
        If Not lineFeature Is Nothing Then
            Me.radioButtonLineFeature.Enabled = True
        Else
            Me.radioButtonLineFeature.Enabled = False
        End If

        If Not lineGraphic Is Nothing Then
            Me.radioButtonLineGraphic.Enabled = True
        Else
            Me.radioButtonLineGraphic.Enabled = False
        End If

        If Me.radioButtonLineFeature.Enabled Then
            Me.radioButtonLineFeature.Checked = True
        Else
            Me.radioButtonLineGraphic.Checked = True
        End If
    End Sub
End Class

Public Interface ICreateGraphicTrackOptions
    Property SimplificationFactor() As Double

    Property TrackName() As String

    Property OverwriteTrack() As Boolean

    Property PathGeometry() As IGeometry

    Property ReverseOrder() As Boolean

    Property PointElement() As IElement

    Property LineElement() As ILineElement

    Property AnimatePath() As Boolean
End Interface

Public Class CreateGraphicTrackOptions : Implements ICreateGraphicTrackOptions
    Private simpFactor As Double
    Private showTrace As Boolean
    Private importTrackName As String
    Private overwriteTrack_Renamed As Boolean
    Private pathGeo As IGeometry
    Private reverseOrder_Renamed As Boolean
    Private element As IElement
    Private lineElement_Renamed As ILineElement

    Public Property SimplificationFactor() As Double Implements ICreateGraphicTrackOptions.SimplificationFactor
        Get
            Return simpFactor
        End Get
        Set(ByVal value As Double)
            simpFactor = Value
        End Set
    End Property

    Public Property TrackName() As String Implements ICreateGraphicTrackOptions.TrackName
        Get
            Return importTrackName
        End Get
        Set(ByVal value As String)
            importTrackName = Value
        End Set
    End Property

    Public Property OverwriteTrack() As Boolean Implements ICreateGraphicTrackOptions.OverwriteTrack
        Get
            Return overwriteTrack_Renamed
        End Get
        Set(ByVal value As Boolean)
            overwriteTrack_Renamed = Value
        End Set
    End Property

    Public Property PathGeometry() As IGeometry Implements ICreateGraphicTrackOptions.PathGeometry
        Get
            Return pathGeo
        End Get
        Set(ByVal value As IGeometry)
            pathGeo = Value
        End Set
    End Property

    Public Property ReverseOrder() As Boolean Implements ICreateGraphicTrackOptions.ReverseOrder
        Get
            Return reverseOrder_Renamed
        End Get
        Set(ByVal value As Boolean)
            reverseOrder_Renamed = Value
        End Set
    End Property

    Public Property PointElement() As IElement Implements ICreateGraphicTrackOptions.PointElement
        Get
            Return element
        End Get
        Set(ByVal value As IElement)
            element = Value
        End Set
    End Property

    Public Property LineElement() As ILineElement Implements ICreateGraphicTrackOptions.LineElement
        Get
            Return lineElement_Renamed
        End Get
        Set(ByVal value As ILineElement)
            lineElement_Renamed = Value
        End Set
    End Property

    Public Property AnimatePath() As Boolean Implements ICreateGraphicTrackOptions.AnimatePath
        Get
            Return showTrace
        End Get
        Set(ByVal value As Boolean)
            showTrace = Value
        End Set
    End Property
End Class
