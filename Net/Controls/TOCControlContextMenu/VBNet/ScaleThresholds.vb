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

Public NotInheritable Class ScaleThresholds

    Inherits BaseCommand
    Implements ICommandSubType

    Private m_pMapControl As IMapControl3
    Private m_lSubType As Long

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pMapControl = hook
    End Sub

    Public Overrides Sub OnClick()
        Dim pLayer As ILayer
        pLayer = m_pMapControl.CustomProperty
        If (m_lSubType = 1) Then pLayer.MaximumScale = m_pMapControl.MapScale
        If (m_lSubType = 2) Then pLayer.MinimumScale = m_pMapControl.MapScale
        If (m_lSubType = 3) Then
            pLayer.MaximumScale = 0
            pLayer.MinimumScale = 0
        End If
        m_pMapControl.Refresh(esriViewDrawPhase.esriViewGeography)
    End Sub

    Public Function GetCount() As Integer Implements ESRI.ArcGIS.SystemUI.ICommandSubType.GetCount
        Return 3
    End Function

    Public Sub SetSubType(ByVal SubType As Integer) Implements ESRI.ArcGIS.SystemUI.ICommandSubType.SetSubType
        m_lSubType = SubType
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            If (m_lSubType = 1) Then
                Return "Set Maximum Scale"
            ElseIf (m_lSubType = 2) Then
                Return "Set Minimum Scale"
            Else
                Return "Remove Scale Thresholds"
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            Dim bEnabled As Boolean
            bEnabled = True

            Dim pLayer As ILayer
            pLayer = m_pMapControl.CustomProperty

            If (m_lSubType = 3) Then
                If (pLayer.MaximumScale = 0) And (pLayer.MinimumScale = 0) Then bEnabled = False
            End If
            Return bEnabled
        End Get
    End Property
End Class


