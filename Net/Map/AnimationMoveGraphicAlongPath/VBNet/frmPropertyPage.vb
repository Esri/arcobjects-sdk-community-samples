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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Animation

Partial Public Class frmPropertyPage : Inherits Form
    Private bPageDirty As Boolean
    Private targetTrack As IAGAnimationTrack

    Public Sub New()
        InitializeComponent()
        bPageDirty = False
    End Sub
    Public Sub Init(ByVal track As IAGAnimationTrack)
        targetTrack = track

        Dim trackExtensions As IAGAnimationTrackExtensions = CType(targetTrack, IAGAnimationTrackExtensions)
        Dim trackExtension As IMapGraphicTrackExtension
        If trackExtensions.ExtensionCount = 0 Then 'if there is no extension, add one
            trackExtension = New MapGraphicTrackExtension()
            trackExtensions.AddExtension(trackExtension)
        Else
            trackExtension = CType(trackExtensions.Extension(0), IMapGraphicTrackExtension)
        End If
        Me.checkBoxTrace.Checked = trackExtension.ShowTrace
    End Sub
    Public ReadOnly Property CheckBoxShowTrace() As CheckBox
        Get
            Return checkBoxTrace
        End Get
    End Property
    Public ReadOnly Property PageDirty() As Boolean
        Get
            Return bPageDirty
        End Get
    End Property
    Private Sub checkBoxTrace_Click(ByVal sender As Object, ByVal e As EventArgs) Handles checkBoxTrace.Click
        bPageDirty = True
    End Sub

    Private Sub frmPropertyPage_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        helpProvider1.SetHelpString(Me.checkBoxTrace, "Check to show the trace of the moving graphic in the animation. ")
    End Sub
End Class
