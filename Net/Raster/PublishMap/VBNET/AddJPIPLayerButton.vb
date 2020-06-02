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
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports System.Windows.Forms
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Geodatabase
Public Class AddJPIPLayerButton
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button
    Dim frmAddJPIP As FrmAddJPIP
    Public Sub New()
    End Sub

    Protected Overrides Sub OnClick()
        If ((frmAddJPIP Is Nothing) OrElse (Not frmAddJPIP.Visible)) Then
            My.ArcMap.Application.CurrentTool = Nothing
            frmAddJPIP = New FrmAddJPIP()
            frmAddJPIP.Show()
        ElseIf Not frmAddJPIP.Focus Then
            frmAddJPIP.Show()
        End If
    End Sub
    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub
End Class

