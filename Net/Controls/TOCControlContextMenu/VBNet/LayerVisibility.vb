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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.SystemUI

Public NotInheritable Class LayerVisibility

    Inherits BaseCommand
    Implements ICommandSubType

    Private m_pHookHelper As New HookHelperClass
    Private m_lSubType As Long

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pHookHelper.Hook = hook
    End Sub

    Public Overrides Sub OnClick()
        Dim i As Integer
        For i = 0 To m_pHookHelper.FocusMap.LayerCount - 1
            If (m_lSubType = 1) Then
                m_pHookHelper.FocusMap.Layer(i).Visible = True
            ElseIf (m_lSubType = 2) Then
                m_pHookHelper.FocusMap.Layer(i).Visible = False
            End If
        Next
        m_pHookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)
    End Sub

    Public Function GetCount() As Integer Implements ESRI.ArcGIS.SystemUI.ICommandSubType.GetCount
        Return 2
    End Function

    Public Sub SetSubType(ByVal SubType As Integer) Implements ESRI.ArcGIS.SystemUI.ICommandSubType.SetSubType
        m_lSubType = SubType
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            If (m_lSubType = 1) Then
                Return "Turn All Layers On"
            Else
                Return "Turn All Layers Off"
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            Dim i As Integer
            Dim bEnabled As Boolean

            bEnabled = False
            If (m_lSubType = 1) Then
                For i = 0 To m_pHookHelper.FocusMap.LayerCount - 1
                    If (m_pHookHelper.FocusMap.Layer(i).Visible = False) Then
                        bEnabled = True
                        Exit For
                    End If
                Next
            ElseIf (m_lSubType = 2) Then
                For i = 0 To m_pHookHelper.FocusMap.LayerCount - 1
                    If (m_pHookHelper.FocusMap.Layer(i).Visible = True) Then
                        bEnabled = True
                        Exit For
                    End If
                Next
            End If
            Return bEnabled
        End Get
    End Property
End Class


