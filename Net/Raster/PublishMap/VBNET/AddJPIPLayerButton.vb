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

