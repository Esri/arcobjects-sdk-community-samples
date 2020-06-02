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
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI

Public Class RubberBandZoom
    Inherits ESRI.ArcGIS.Desktop.AddIns.Tool

    Private m_RubberBand As IRubberBand
    Private docGeometry As IGeometry

    Public Sub New()

    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

    Protected Overrides Sub OnActivate()
        docGeometry = Nothing
        m_RubberBand = Nothing
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal Args As MouseEventArgs)
        m_RubberBand = New RubberRectangularPolygon()

        If (Args.Button.ToString = "Left") Then
            'on a left-click, create a new rubber polygon and zoom to its' final extent
            docGeometry = m_RubberBand.TrackNew(My.ArcMap.Document.ActiveView.ScreenDisplay, Nothing)

            'if it is non-zero, zoom to the envelope just captured
            If (docGeometry.IsEmpty = False) Then
                My.ArcMap.Document.ActiveView.Extent = docGeometry.Envelope
            End If


            'and refresh
            My.ArcMap.Document.ActiveView.Refresh()

        ElseIf (Args.Button.ToString = "Right") Then
            'on a right-click, zoom out to the previous extent, or to full extent if no previous extent exists
            If (My.ArcMap.Document.ActiveView.ExtentStack.CanUndo) Then
                'if possible, go back to the previous extent
                My.ArcMap.Document.ActiveView.ExtentStack.Undo()

            Else
                'otherwise, zoom to the active view's full extent
                My.ArcMap.Document.ActiveView.Extent = My.ArcMap.Document.ActiveView.FullExtent.Envelope

            End If

            'and refresh
            My.ArcMap.Document.ActiveView.Refresh()

        End If



    End Sub

    Protected Overrides Function OnContextMenu(ByVal X As Integer, ByVal Y As Integer) As Boolean
        'return true so that the context menu does not come up.
        Return True
    End Function

End Class
