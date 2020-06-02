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

Public NotInheritable Class ZoomToLayer

    Inherits BaseCommand

    Private m_pMapControl As imapcontrol3

    Public Sub New()
        MyBase.New()
        MyBase.m_caption = "Zoom To Layer"
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pMapControl = hook
    End Sub

    Public Overrides Sub OnClick()
        Dim pLayer As ILayer
        pLayer = m_pMapControl.CustomProperty
        m_pMapControl.Extent = pLayer.AreaOfInterest
    End Sub
End Class






