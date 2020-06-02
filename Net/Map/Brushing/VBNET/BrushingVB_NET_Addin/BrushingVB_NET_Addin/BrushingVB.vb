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
Option Explicit On
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.GeoDatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Desktop

Public Class BrushingVB
  Inherits ESRI.ArcGIS.Desktop.AddIns.Tool

  Private m_gSelectTool As Object
  Private m_bAction As Boolean

  Public Sub New()
    m_bAction = False
  End Sub

  Protected Overrides Sub OnMouseDown(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
    MyBase.OnMouseDown(arg)

    If m_bAction = True Then Exit Sub

        ' create and initialize SelectTool command
    m_gSelectTool = CreateObject("esriArcMapUI.SelectTool")
    Dim pCommand As ESRI.ArcGIS.SystemUI.ICommand
    pCommand = m_gSelectTool
    pCommand.OnCreate(My.ArcMap.Application)

    ' emulate mouse click for m_gSelectTool
    Dim pTool As ESRI.ArcGIS.SystemUI.ITool
    pTool = m_gSelectTool
    pTool.OnMouseDown(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y)
    pTool.OnMouseUp(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y)
    m_bAction = SelectFromGraphics()

    ' if there's selected graphics then start moving it
    If m_bAction = True Then
      pTool.OnMouseDown(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y)
    Else
      m_gSelectTool = Nothing
    End If

  End Sub

  Protected Overrides Sub OnMouseUp(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
    MyBase.OnMouseUp(arg)

    If m_bAction = False Then Exit Sub

    Dim pTool As ESRI.ArcGIS.SystemUI.ITool
    pTool = m_gSelectTool
    pTool.OnMouseUp(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y)
    Call SelectFromGraphics()
    ' release object
    m_gSelectTool = Nothing
    m_bAction = False

    Dim cursor As MouseCursor
    cursor = New MouseCursor
    cursor.SetCursor(ESRI.ArcGIS.SystemUI.esriSystemMouseCursor.esriSystemMouseCursorDefault)

  End Sub

  Protected Overrides Sub OnMouseMove(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
    MyBase.OnMouseMove(arg)

    If m_bAction = False Then Exit Sub
    ' 1 move graphics
    ' 2 set new position
    ' 3 continue moving
    ' 4 update selection
    Dim pTool As ESRI.ArcGIS.SystemUI.ITool
    pTool = m_gSelectTool
    pTool.OnMouseMove(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y)

    ' comment out the next 3 line to speed up, but selection only gets updated by mouse up
    pTool.OnMouseUp(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y)
    pTool.OnMouseDown(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y)
    Call SelectFromGraphics()

  End Sub

Function SelectFromGraphics() As Boolean

    Dim pMxApp As IMxApplication
    Dim pMxDoc As IMxDocument
    pMxApp = My.ArcMap.Application
    pMxDoc = My.ArcMap.Document

    Dim pGC As IGraphicsContainerSelect
    pGC = pMxDoc.FocusMap

    ' find first selected graphic object
    If pGC.ElementSelectionCount > 0 Then
        Dim pElem As IElement
        pElem = pGC.SelectedElement(0)
        Dim pGeometry As IGeometry
        pGeometry = pElem.Geometry
        pMxDoc.FocusMap.SelectByShape(pGeometry, Nothing, False)
        pMxDoc.ActivatedView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        SelectFromGraphics = True
    Else
        SelectFromGraphics = False
    End If
End Function

Protected Function GetButtonCode(ByVal pArg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs) As Integer

   Dim intButtonCode As Integer = -1
   Dim pButton As System.Windows.Forms.MouseButtons
   pButton = pArg.Button

    Select Case pButton.ToString()
      Case "Left"
        intButtonCode = 1
      Case "Right"
        intButtonCode = 2
      Case "Middle"
        intButtonCode = 4
    End Select

   Return intButtonCode
End Function


  Protected Overrides Sub OnUpdate()
    'Enabled = My.ArcMap.Application IsNot Nothing
  End Sub
End Class
