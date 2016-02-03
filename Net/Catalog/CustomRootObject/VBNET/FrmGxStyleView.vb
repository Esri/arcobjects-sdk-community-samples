Option Explicit On

Imports System.Drawing
Imports ESRI.ArcGIS.Display

Public Class FrmGxStyleView
    Private g_pGxView As clsGxStyleView
    Private bmpPreview As Bitmap

    Public Sub RefreshView()
        GeneratePreview()
        If (picStylePreview Is Nothing) Then picStylePreview.Refresh()
    End Sub
    Public Property GxStyleView() As clsGxStyleView
        Get
            Return g_pGxView
        End Get
        Set(ByVal value As clsGxStyleView)
            g_pGxView = value
        End Set
    End Property

    Private Sub GeneratePreview()
        Dim r As New tagRECT
        Try
            If ((picStylePreview Is Nothing) Or (g_pGxView Is Nothing)) Then Exit Sub
            With r
                .bottom = picStylePreview.ClientSize.Height
                .top = 0
                .right = picStylePreview.ClientSize.Width
                .left = 0
            End With

            bmpPreview = New Bitmap(r.right, r.bottom)
            Dim GrpObj As System.Drawing.Graphics = Graphics.FromImage(bmpPreview)
            Try
                g_pGxView.PreviewItem(GrpObj.GetHdc(), r)
                GrpObj.ReleaseHdc()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                GrpObj.Dispose()
            End Try
            ' draw image
            picStylePreview.Image = bmpPreview
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub
    Private Sub picStylePreview_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picStylePreview.Paint
        If (bmpPreview Is Nothing) Then GeneratePreview()
    End Sub

    Private Sub picStylePreview_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles picStylePreview.SizeChanged
        GeneratePreview()
    End Sub
End Class