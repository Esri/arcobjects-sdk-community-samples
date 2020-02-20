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

Public NotInheritable Class LayerSelectable

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
        Dim pLayer As IFeatureLayer
        pLayer = m_pMapControl.CustomProperty
        If (m_lSubType = 1) Then pLayer.Selectable = True
        If (m_lSubType = 2) Then pLayer.Selectable = False
    End Sub

    Public Function GetCount() As Integer Implements ESRI.ArcGIS.SystemUI.ICommandSubType.GetCount
        Return 2
    End Function

    Public Sub SetSubType(ByVal SubType As Integer) Implements ESRI.ArcGIS.SystemUI.ICommandSubType.SetSubType
        m_lSubType = SubType
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            Dim pLayer As ILayer
            pLayer = m_pMapControl.CustomProperty

            If (TypeOf pLayer Is IFeatureLayer) Then
                Dim pFeatureLayer As IFeatureLayer
                pFeatureLayer = pLayer
                If (m_lSubType = 1) Then Return Not pFeatureLayer.Selectable
                If (m_lSubType = 2) Then Return pFeatureLayer.Selectable
            Else
                Return False
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Caption() As String
        Get
            If (m_lSubType = 1) Then
                Return "Layer Selectable"
            Else
                Return "Layer Unselectable"
            End If
        End Get
    End Property
End Class

